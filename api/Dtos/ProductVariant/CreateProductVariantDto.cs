using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ProductVariant
{
    public class CreateProductVariantDto
    {
        public string SKU { get; set; }
        public int SizeId { get; set; }
        public int ColourId { get; set; }
        public int StockQuantity { get; set; }
    }
}