using Dapper;
using InventoryOrderAPI.Entities;

namespace InventoryOrderAPI.Data
{
    public class OrderRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public OrderRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AddSync(Order order)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"INSERT INTO [Order] (OrderDate, ProductId, Quantity, TotalValue)
                       VALUES (@OrderDate, @ProductId, @Quantity, @TotalValue);";
            await connection.ExecuteAsync(sql, order);

        }
        public async Task<List<Order>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"select OrderID, OrderDate, ProductId, Quantity, TotalValue from [Order] ";
            var result = await connection.QueryAsync<Order>(sql);
            return result.ToList();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"select OrderID, OrderDate, ProductId, Quantity, TotalValue from [Order] WHERE OrderId = @Id";
            var result = await connection.QueryFirstOrDefaultAsync<Order>(sql, new { Id = id });
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"DELETE FROM [Order] WHERE OrderId = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task UpdateAsync(Order order)
        {
            using var connection = _connectionFactory.CreateConnection();
            var sql = @"UPDATE [ORDER]
                        SET OrderDate = @OrderDate,
                            ProductId = @ProductId,
                            Quantity = @Quantity,
                            TotalValue =  @TotalValue
                        WHERE OrderId = @OrderId";
            await connection.ExecuteAsync(sql, order);
        }
    }
}