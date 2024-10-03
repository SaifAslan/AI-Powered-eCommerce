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
        public Size Size { get; set; }
        public int ColourId { get; set; }
        public Colour Colour { get; set; }
        public int StockQuantity { get; set; }
    } 
}