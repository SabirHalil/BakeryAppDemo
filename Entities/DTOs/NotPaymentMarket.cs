namespace Entities.DTOs
{
    public class NotPaymentMarket
    {
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public decimal TotalAmount { get; set; }
        public int StaleBread { get; set; }
    }
}
