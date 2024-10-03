using api.Helpers;
using api.Interfaces;
using api.Mappers;
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

    }
}