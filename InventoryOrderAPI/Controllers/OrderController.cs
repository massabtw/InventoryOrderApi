using InventoryOrderAPI.Data;
using InventoryOrderAPI.Extensions;
using InventoryOrderAPI.Interfaces;
using InventoryOrderAPI.Models.Order;
using Microsoft.AspNetCore.Mvc;


namespace InventoryOrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(OrderRepository orderRepository, IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderRequestModel request)
        {
            var result = await _orderService.CreateAsync(request);
            return result.Ok();
        }

        [HttpGet]
        public async Task <IActionResult> Get()
        {
            var result = await _orderService.GetAllAsync();
            return result.Ok();
        }

        [HttpGet("{id}")]
        public async Task <IActionResult> GetById(int id)
        {
            var result = await _orderService.GetByIdAsync(id);
            return result.Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _orderService.DeleteAsync(id);
            return result.NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateOrderRequestModel request)
        {
            var result = await _orderService.UpdateAsync(id, request);
            return result.Ok();

        }

    }
}
