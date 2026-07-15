using InventoryOrderAPI.Extensions;
using InventoryOrderAPI.Interfaces;
using InventoryOrderAPI.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace InventoryOrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductRequestModel request)
        {
            var result = await _productService.CreateAsync(request);
            return result.Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetAllAsync();
            return result.Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return result.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);
            return result.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateProductRequestModel request)
        {
            var result = await _productService.UpdateAsync(id, request);
            return result.Ok();
        }
    }
}
