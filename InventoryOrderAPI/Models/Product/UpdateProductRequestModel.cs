namespace InventoryOrderAPI.Models.Product
{
    public class UpdateProductRequestModel
    {
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
