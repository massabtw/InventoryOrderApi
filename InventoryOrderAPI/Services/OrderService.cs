using GrupoMadero.Common.Results;
using InventoryOrderAPI.Data;
using InventoryOrderAPI.Entities;
using InventoryOrderAPI.Helpers;
using InventoryOrderAPI.Interfaces;
using InventoryOrderAPI.Models.Order;

namespace InventoryOrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(OrderRepository orderRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Result<Order>> CreateAsync(CreateOrderRequestModel request)
        {
            try
            {
                if (request.ProductId < 0)
                    return ErrorMessages.ProductNotFound(request.ProductId);

                if (request.Quantity <= 0)
                    return ErrorMessages.InvalidQuantity(request.Quantity);

                if (request.ProductPrice <= 0)
                    return ErrorMessages.InvalidPrice(request.ProductPrice);

                var order = new Order()
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    TotalValue = request.ProductPrice * request.Quantity,
                    OrderDate = DateTime.UtcNow

                };
                await _orderRepository.AddSync(order);
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnknownError");
                return ErrorMessages.UnknownError(ex.Message);
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var existing = await _orderRepository.GetByIdAsync(id);

            try
            {
                if (existing == null)
                {
                    _logger.LogInformation("Order {OrderId} NotFound", id);
                    return ErrorMessages.OrderNotFound(id);
                }
                await _orderRepository.DeleteAsync(id);
                return Result.Succeeded();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnknownError");
                return ErrorMessages.UnknownError(ex.Message);
            }
        }

        public async Task<Result<List<Order>>> GetAllAsync()
        {
            try
            {
                var result = await _orderRepository.GetAllAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnknownError");
                return ErrorMessages.UnknownError(ex.Message);
            }
        }

        public async Task<Result<Order>> GetByIdAsync(int id)
        {
            var result = await _orderRepository.GetByIdAsync(id);
            try
            {
                if (result == null)
                {
                    return ErrorMessages.OrderNotFound(id);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnknownError");
                return ErrorMessages.UnknownError(ex.Message);
            }
        }

        public async Task<Result<Order>> UpdateAsync(int id, UpdateOrderRequestModel request)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(id);
                if (order == null)
                    return ErrorMessages.OrderNotFound(id);

                if (request.Quantity <= 0)
                    return ErrorMessages.InvalidQuantity(request.Quantity);

                if (request.ProductPrice <= 0)
                    return ErrorMessages.InvalidPrice(request.ProductPrice);

                order.Quantity = request.Quantity;
                order.TotalValue = request.ProductPrice * request.Quantity;

                await _orderRepository.UpdateAsync(order);
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnknownError");
                return ErrorMessages.UnknownError(ex.Message);

            }
        }
    }
}
