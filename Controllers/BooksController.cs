using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Euromonitor.Data;
using Euromonitor.Models;
using Euromonitor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Euromonitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public BooksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<BookViewModel> Get()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return _context.Books.Select(x => new BookViewModel
                    {
                        Id = x.Id,
                        Price = x.Price,
                        Title = x.Title
                    }).ToList();
            }
            else
            {
                var userId = _userManager.GetUserId(User);
                return _context.Books.Select(x => new BookViewModel
                {
                    Id = x.Id,
                    Price = x.Price,
                    Title = x.Title,
                    Subscribed = x.Subscribtions
                        .Any(x => x.ApplicationUser.Id == userId)
                }).ToList();
            }
        }

        [Authorize]
        [HttpPost]
        [Route("subscribe/{id?}")]
        public IActionResult Subscribe(int id)
        {
            var book = _context.Books.Find(id);
            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;

            var sub = new Subscribtion
            {
                ApplicationUser = user,
                Book = book,
            };

            _context.Subscribtions.Add(sub);
            _context.SaveChanges();
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("unsubscribe/{id?}")]
        public IActionResult Unsubscribe(int id)
        {
            var userId = _userManager.GetUserId(User);
            var sub = _context.Subscribtions.First(x => 
                x.Book.Id == id && x.ApplicationUser.Id == userId);
            _context.Subscribtions.Remove(sub);
            _context.SaveChanges();
            return Ok();
        }
    }
}