namespace InventoryOrderAPI.Models.Order
{
    public class CreateOrderRequestModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
