using Dapper;
using InventoryOrderAPI.Entities;

namespace InventoryOrderAPI.Data
{
    public class ProductRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ProductRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task AddAsync(Product product)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"INSERT INTO Product (ProductName, Price, Quantity) 
                        VALUES (@ProductName, @Price, @Quantity);";
            await connection.ExecuteAsync(sql, product);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"select ProductId, ProductName,Price, Quantity from Product";
            var result = await connection.QueryAsync<Product>(sql);
            return result.ToList();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"select ProductId, ProductName, Price, Quantity from Product where ProductId = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"DELETE FROM Product WHERE ProductId = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task UpdateAsync(Product product)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"UPDATE PRODUCT
                        SET ProductName = @ProductName,
                            Price = @Price,
                            Quantity = @Quantity
                        WHERE ProductId = @ProductId";
            await connection.ExecuteAsync(sql, product);
        }
    }
}
