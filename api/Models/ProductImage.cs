using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ProductImage
    {
        public int Id { get; set; } // Unique identifier for the image
        public string Url { get; set; } // URL of the product image
        public string AltText { get; set; } // Alternative text for the image (for accessibility)
        public bool IsPrimary { get; set; } // Flag for primary display image
        public int ProductId { get; set; } // Foreign key to associate image with a product
        public Product Product { get; set; } // Navigation property for the product
    }
}