using GrupoMadero.Common.Results;
using InventoryOrderAPI.Entities;
using InventoryOrderAPI.Models.Order;

namespace InventoryOrderAPI.Interfaces
{
    public interface IOrderService
    {
        Task<Result<List<Order>>> GetAllAsync();
        Task<Result<Order>> GetByIdAsync(int id);
        Task<Result<Order>> CreateAsync(CreateOrderRequestModel request);
        Task<Result<Order>> UpdateAsync(int id, UpdateOrderRequestModel request);
        Task<Result> DeleteAsync(int id);

    }
}
