using GrupoMadero.Common.Results;
using InventoryOrderAPI.Data;
using InventoryOrderAPI.Entities;
using InventoryOrderAPI.Helpers;
using InventoryOrderAPI.Interfaces;
using InventoryOrderAPI.Models.Product;

namespace InventoryOrderAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ILogger<ProductService> logger, ProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<Result<Product>> CreateAsync(CreateProductRequestModel request)
        {
            try
            {
                if (request.Price <= 0)
                    return ErrorMessages.InvalidPrice(request.Price);

                if (request.Quantity <= 0)
                    return ErrorMessages.InvalidQuantity(request.Quantity);

                if (string.IsNullOrWhiteSpace(request.ProductName))
                    return ErrorMessages.InvalidName(request.ProductName);

                var product = new Product()
                {
                    Price = request.Price,
                    Quantity = request.Quantity,
                    ProductName = request.ProductName
                };
                await _productRepository.AddAsync(product);
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnknownError");
                return ErrorMessages.UnknownError(ex.Message);
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var result = await _productRepository.GetByIdAsync(id);
            if (result == null)
                return ErrorMessages.ProductNotFound(id);
            try
            {
                await _productRepository.DeleteAsync(id);
                return Result.Succeeded();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnknownError");
                return ErrorMessages.UnknownError(ex.Message);
            }
        }
        public async Task<Result<List<Product>>> GetAllAsync()
        {
            try
            {
                var result = await _productRepository.GetAllAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnknownError");
                return ErrorMessages.UnknownError(ex.Message);
            }
        }

        public async Task<Result<Product>> GetByIdAsync(int id)
        {
            var result = await _productRepository.GetByIdAsync(id);

            try
            {
                if (result == null)
                {
                    return ErrorMessages.ProductNotFound(id);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnknownError");
                return ErrorMessages.UnknownError(ex.Message);
            }
        }

        public async Task<Result<Product>> UpdateAsync(int id, UpdateProductRequestModel request)
        {
            try
            {
                var result = await _productRepository.GetByIdAsync(id);
                if (result == null)
                    return ErrorMessages.ProductNotFound(id);

                if (request.Quantity <= 0)
                    return ErrorMessages.InvalidQuantity(request.Quantity);

                if (request.Price <= 0)
                    return ErrorMessages.InvalidPrice(request.Price);

                result.Quantity = request.Quantity;
                result.Price = request.Price;

                await _productRepository.UpdateAsync(result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnknownError");
                return ErrorMessages.UnknownError(ex.Message);
            }
        }
    }
}
