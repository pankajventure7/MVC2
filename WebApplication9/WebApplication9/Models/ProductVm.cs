using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication9.Models
{
    public class ProductVm
    {
        public string Name { get; set; }
        public string type { get; set; }
        public decimal Price { get; set; }
        
        public string PaidBy { get; set; }
        public DateTime date { get; set; }

        
    }
}
