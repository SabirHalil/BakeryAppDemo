namespace Entities.DTOs
{
    public class PurchasedProductSoldInTheBakery
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal Revenue { get; set; }
        public int RemainingYesterday { get; set; }
        public int PurchasedToday { get; set; }
        public int RemainingToday { get; set; }
        public int StaleProductToday { get; set; }

    }
}
