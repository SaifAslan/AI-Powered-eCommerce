using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Product;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Data;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RequestResult<CreateProductDto>> AddProductAsync(CreateProductDto createProductDto)
        {
            try
            {
                var product = new Product
                {
                    Name = createProductDto.Name,
                    Description = createProductDto.Description,
                    Price = createProductDto.Price,
                    IsFeatured = createProductDto.IsFeatured,
                    BrandId = createProductDto.BrandId,
                    SubCategoryId = createProductDto.SubCategoryId,
                    GenderId = createProductDto.GenderId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ProductMaterials = createProductDto.ProductMaterialDtos.Select(PMDto => new ProductMaterial
                    {
                        MaterialId = PMDto.MaterialId,
                        Percentage = PMDto.Percentage,
                    }).ToList(),
                    Variants = createProductDto.Variants.Select(variantDto => new ProductVariant
                    {
                        SKU = variantDto.SKU,
                        SizeId = variantDto.SizeId,
                        ColourId = variantDto.ColourId,
                        StockQuantity = variantDto.StockQuantity,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }

                    ).ToList()
                };
                await _context.AddAsync(product);
                await _context.SaveChangesAsync();
                return RequestResult<CreateProductDto>.Success(createProductDto);
            }
            catch (Exception e)
            {
                return RequestResult<CreateProductDto>.Failure(e);
            }
        }

        public Task<ProductVariant> AddProductVariantAsync(ProductVariant variant)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductVariantAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProductsAsync(ProductQueryObject query)
        {
            try
            {
                var result = _context.Product.AsQueryable();

                if (query.GenderId.HasValue)
                {
                    result = result.Where(p => p.GenderId == query.GenderId);
                }
                if (query.SubCategoryId.HasValue)
                {
                    result = result.Where(p => p.SubCategoryId == query.SubCategoryId);
                }
                if (query.BrandId.HasValue)
                {
                    result = result.Where(p => p.BrandId == query.BrandId);
                }
                if (query.MaterialId.HasValue)
                {
                    result = result.Where(p => p.ProductMaterials.Any(p => p.MaterialId == query.MaterialId));
                }

                if (!string.IsNullOrEmpty(query.SortBy))
                {
                    switch (query.SortBy.ToLower())
                    {
                        case "price":
                            result = query.IsDecending ? result.OrderByDescending(p => p.Price) : result.OrderBy(p => p.Price);
                            break;
                        case "createdat":
                            result = query.IsDecending ? result.OrderByDescending(p => p.CreatedAt) : result.OrderBy(p => p.CreatedAt);
                            break;
                        default:
                            result = result.OrderBy(p => p.Name);
                            break;

                    }
                }

                result = result.Include(p => p.Variants.Where(v => v.StockQuantity > 0))
                .ThenInclude(v => v.Size)
                .Include(p => p.Variants.Where(v => v.StockQuantity > 0))
                .ThenInclude(v => v.Colour);


                if (query.ColourId.HasValue)
                {
                    result = result.Where(p => p.Variants.Any(v =>
                        v.ColourId == query.ColourId.Value));
                }
                if (query.SizeId.HasValue)
                {
                    result = result.Where(p => p.Variants.Any(v =>
                        v.SizeId == query.SizeId.Value));
                }

                var skipNumber = (query.PageNumber - 1) * query.PageSize;
                result = result.Skip(skipNumber).Take(query.PageSize);

                return await result.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred while getting products: {e.Message}");
            }


        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            return await _context.Product.AnyAsync(p => p.Id == id);
        }

        public Task UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductVariantAsync(ProductVariant variant)
        {
            throw new NotImplementedException();
        }
    }
}