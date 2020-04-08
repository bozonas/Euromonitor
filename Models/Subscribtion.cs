using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euromonitor.Models
{
    public class Subscribtion
    {
        public int Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Book Book { get; set; }
    }
}
