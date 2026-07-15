using GrupoMadero.Common.Results;
using InventoryOrderAPI.Entities;
using InventoryOrderAPI.Models.Product;

namespace InventoryOrderAPI.Interfaces
{
    public interface IProductService
    {
        Task<Result<List<Product>>> GetAllAsync();
        Task<Result<Product>> GetByIdAsync(int id);
        Task<Result<Product>> CreateAsync(CreateProductRequestModel request);
        Task<Result<Product>> UpdateAsync(int id, UpdateProductRequestModel request);
        Task<Result> DeleteAsync(int id);
    }
}
