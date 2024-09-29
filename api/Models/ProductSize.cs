using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ProductSize
    {
        public int ProductId { get; set; } // Foreign key for Product
        public Product Product { get; set; } // Navigation property for Product

        public int SizeId { get; set; } // Foreign key for Size
        public Size Size { get; set; } // Navigation property for Size
    }

}