using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ProductColour
    {
        public int ProductId { get; set; } // Foreign key for Product
        public Product Product { get; set; } // Navigation property for Product

        public int ColourId { get; set; } // Foreign key for Colour
        public Colour Colour { get; set; } // Navigation property for Color
    }
}