namespace InventoryOrderAPI.Models.Order
{
    public class UpdateOrderRequestModel
    {
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
