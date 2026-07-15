using GrupoMadero.Common.Results;

namespace InventoryOrderAPI.Helpers
{
    public static class ErrorMessages
    {
        public static ErrorModel OrderNotFound(int id) => new ErrorModel()
        {
            Code = "OrderNotFound",
            Message = $"Order {id} NotFound",
            MessageCustomer = $"Pedido {id} não encontrado"
        };

        public static ErrorModel ProductNotFound(int productId) => new ErrorModel()
        {
            Code = "ProductNotFound",
            Message = $"Product {productId} NotFound",
            MessageCustomer = $"Produto {productId} não encontrado"
        };

        public static ErrorModel InvalidQuantity(int quantity) => new ErrorModel()
        {
            Code = "InvalidQuantity",
            Message = $"Quantity {quantity} Invalid",
            MessageCustomer = $"Quantidade {quantity} invalida"
        };

        public static ErrorModel InvalidPrice(decimal price) => new ErrorModel()
        {
            Code = "InvalidPrice",
            Message = $"Invalid {price} Price",
            MessageCustomer = $"Preço {price} invalido"
        };

        public static ErrorModel UnknownError(string message) => new ErrorModel()
        {
            Code = "UnknownError",
            Message = message,
            MessageCustomer = "Erro desconhecido"
        };

        public static ErrorModel InvalidName(string name) => new ErrorModel()
        {
            Code = "InvalidName",
            Message = $"Invalid{name} Name",
            MessageCustomer = "Nome invalido"
        };
    }
}
