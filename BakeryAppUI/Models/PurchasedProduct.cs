namespace BakeryAppUI.Models
{
    public class PurchasedProduct
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
    }
}
