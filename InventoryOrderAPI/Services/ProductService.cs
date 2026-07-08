using InventoryOrderAPI.Data;
using InventoryOrderAPI.Helpers;
using InventoryOrderAPI.Entities;
using InventoryOrderAPI.Models.Product;
using Microsoft.AspNetCore.Mvc;
using GrupoMadero.Common.Results;
using InventoryOrderAPI.Interfaces;

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
                if (request.Price < 0)
                    return ErrorMessages.InvalidPrice(request.Price);

                if(request.StockQuantity <= 0)
                    return ErrorMessages.InvalidQuantity(request.StockQuantity);

                if (string.IsNullOrWhiteSpace(request.ProductName))
                    return ErrorMessages.InvalidName(request.ProductName);

                var product = new Product()
                {
                    Price = request.Price,
                    StockQuantity = request.StockQuantity,
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

        public Task<Result> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<Product>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<Product>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Product>> UpdateAsync(int id, UpdateProductRequestModel request)
        {
            throw new NotImplementedException();
        }
    }
}
