namespace BakeryAppUI.Models
{
    public class PaymentMarket
    {
        public int id { get; set; }
        public decimal Amount { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public decimal TotalAmount { get; set; }
        public int StaleBread { get; set; }
    }
}
