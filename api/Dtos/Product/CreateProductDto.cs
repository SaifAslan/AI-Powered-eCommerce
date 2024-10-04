using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ProductMaterial;
using api.Dtos.ProductVariant;

namespace api.Dtos.Product
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]

        public decimal Price { get; set; }
        [Required]

        public bool IsFeatured { get; set; }
        [Required]

        public int BrandId { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        public int GenderId { get; set; }
        [Required]
        public List<CreateProductVariantDto> Variants { get; set; }
        [Required]
        public List<AddProductMaterialDto> ProductMaterialDtos { get; set; }
    }
}