using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Product
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string SKU { get; set; }
        public int SizeId { get; set; }
        public api.Models.Size Size { get; set; }
        public int ColourId { get; set; }
        public api.Models.Colour Colour { get; set; }
        public int StockQuantity { get; set; }
    } 
}