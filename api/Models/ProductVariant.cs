using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ProductVariant
    {
        public int Id { get; set; } // Unique identifier for the variant
        public int ProductId { get; set; } // Foreign key for the product
        public Product Product { get; set; } // Navigation property for Product
        public string SKU { get; set; } // Stock Keeping Unit

        public int SizeId { get; set; } // Foreign key for Size
        public Size Size { get; set; } // Navigation property for Size

        public int ColorId { get; set; } // Foreign key for Color
        public Colour Colour { get; set; } // Navigation property for Color

        public int StockQuantity { get; set; } // Stock quantity for this specific variant
    }
}