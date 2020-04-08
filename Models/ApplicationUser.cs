using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euromonitor.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Subscribtion> Subscribtions { get; set; }
    }
}
