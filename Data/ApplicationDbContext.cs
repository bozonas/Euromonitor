using Euromonitor.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Euromonitor.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Pride and Prejudice",
                    Price = 20
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Price = 12
                },
                new Book
                {
                    Id = 3,
                    Title = "The Great Gatsby",
                    Price = 200
                },
                new Book
                {
                    Id = 4,
                    Title = "One Hundred Years of Solitude",
                    Price = 36
                },
                new Book
                {
                    Id = 5,
                    Title = "In Cold Blood",
                    Price = 42
                },
                new Book
                {
                    Id = 6,
                    Title = "Wide Sargasso Sea",
                    Price = 89
                },
                new Book
                {
                    Id = 7,
                    Title = "Brave New World",
                    Price = 14
                },
                new Book
                {
                    Id = 8,
                    Title = "I Capture The Castle",
                    Price = 51
                },
                new Book
                {
                    Id = 9,
                    Title = "Jane Eyre",
                    Price = 65
                },
                new Book
                {
                    Id = 10,
                    Title = "Crime and Punishment",
                    Price = 33
                },
                new Book
                {
                    Id = 11,
                    Title = "The Secret History",
                    Price = 55
                }
            );
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Subscribtion> Subscribtions { get; set; }
    }
}
