
using api.Dtos;
using api.Dtos.Colour;
using api.Dtos.Product;
using api.Dtos.ProductMaterial;
using api.Dtos.ProductVariant;
using api.Dtos.Size;
using api.Helpers;
using api.Models;

namespace api.Mappers
{
    public static class ProductMappers
    {
        public static ProductDto ToGetProductsDto(this Product product, ProductQueryObject filter)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsFeatured = product.IsFeatured,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                BrandId = product.BrandId,
                SubCategoryId = product.SubCategoryId,
                GenderId = product.GenderId,
                Variants = product.Variants
                    .Where(v =>
                        (!filter.ColourId.HasValue || v.ColourId == filter.ColourId.Value) &&
                        (!filter.SizeId.HasValue || v.SizeId == filter.SizeId.Value) &&
                        v.StockQuantity > 0)
                    .Select(ToDto) // Use the variant mapper
                    .ToList()
            };
        }

        public static ProductChatpotDto ToGetProductsChatpotDto(this Product product)
        {
            return new ProductChatpotDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsFeatured = product.IsFeatured,
                SubCategory = product.SubCategory != null ? new SubCategoryDto
                {
                    Name = product.SubCategory.Name
                } : null,
                Category = product.SubCategory?.Category != null ? new CategoryDto
                {
                    Name = product.SubCategory.Category.Name
                } : null,
                Gender = product.Gender != null ? new GenderDto
                {
                    Name = product.Gender.Name
                } : null,
                Brand = product.Brand != null ? new BrandDto
                {
                    Name = product.Brand.Name
                } : null,
                Variants = product.Variants?.Select(ToChatpotDto).ToList(),
                Materials = product.ProductMaterials?.Select(ToProductMaterialChatpotDto).ToList()

            };
        }



        public static ProductVariantDto ToDto(ProductVariant variant)
        {
            return new ProductVariantDto
            {
                Id = variant.Id,
                ProductId = variant.ProductId,
                SKU = variant.SKU,
                SizeId = variant.SizeId,
                Size = new Size
                {
                    Id = variant.Size.Id,
                    Name = variant.Size.Name
                },
                ColourId = variant.ColourId,
                Colour = new Colour
                {
                    Id = variant.Colour.Id,
                    Name = variant.Colour.Name
                },
                StockQuantity = variant.StockQuantity
            };
        }

        public static ProductVariantChatpotDto ToChatpotDto(ProductVariant variant)
        {
            return new ProductVariantChatpotDto
            {
                Id = variant.Id,
                SKU = variant.SKU,
                Size = new SizeDto
                {
                    Name = variant.Size.Name
                },
                Colour = new ColourDto
                {
                    Name = variant.Colour.Name
                }
            };
        }

        public static ProductMaterialChatpotDto ToProductMaterialChatpotDto(ProductMaterial productMaterial)
        {
            return new ProductMaterialChatpotDto
            {
                Name = productMaterial.Material.Name,
                Percentage = productMaterial.Percentage
            };
        }
    }
}