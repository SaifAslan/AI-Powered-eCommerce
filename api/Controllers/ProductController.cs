using api.Dtos.Product;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPorducts(ProductQueryObject productQuery)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var products = await _productRepo.GetProductsAsync(productQuery);
                var productsDto = products.Select(p => p.ToGetProductsDto(productQuery)).ToList();

                return Ok(productsDto);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepo.GetProductByIdAsync(id);
            if (product == null) return NotFound("Product not found");
            return Ok(product);
        }

        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productRepo.AddProductAsync(createProductDto);

            if(result.IsSuccess){
                return Ok("Product created successfully");
            }
            return StatusCode(500, result.Error);

        }

    }
}