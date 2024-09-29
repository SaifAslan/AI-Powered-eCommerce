using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; } // Example: "Men's", "Women's", "Unisex"
        public ICollection<Product> Products { get; set; } // One-to-many relationship
    }

}