namespace Entities.DTOs
{
    public class MoneyReceivedMarket
    {
        public int Id { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public decimal ReceivedAmount { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
