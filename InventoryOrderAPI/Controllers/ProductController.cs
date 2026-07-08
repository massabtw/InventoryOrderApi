using InventoryOrderAPI.Data;
using InventoryOrderAPI.Extensions;
using InventoryOrderAPI.Interfaces;
using InventoryOrderAPI.Models.Product;
using InventoryOrderAPI.Services;
using Microsoft.AspNetCore.Mvc;



namespace InventoryOrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(ProductRepository productRepository, IProductService productService )
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
            try
            {
                var result = await _productRepository.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            var result = await _productRepository.GetByIdAsync(id);

            try
            {
                if(result == null)
                {
                    return NotFound("Não encontrado");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productRepository.GetByIdAsync(id);
            if (result == null)
                return NotFound("Erro 404");
            try
            {
                await _productRepository.DeleteAsync(id);
                return Ok("Pedido deletado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            try
            {
            var result = await _productRepository.GetByIdAsync(id);
            if (result == null)
                return NotFound("Pedido não encontrado");

            result.UpdateDetails(request.Name, request.Price, request.Stock);
            await _productRepository.UpdateAsync(result);
            return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
