namespace InventoryOrderAPI.Models.Product
{
    public class CreateProductRequestModel
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
