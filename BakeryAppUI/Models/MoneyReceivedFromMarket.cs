namespace BakeryAppUI.Models
{
    public class MoneyReceivedFromMarket
    {
        public int Id { get; set; }
        public int MarketId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
