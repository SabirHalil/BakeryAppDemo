namespace BakeryAppUI.Models
{
    public class MarketProductDetails
    {
        public int id { get; set; }
        public decimal Amount { get; set; }
        public int MarketId { get; set; }
        public string? MarketName { get; set; }
        public decimal TotalAmount { get; set; }
        public int StaleProduct { get; set; }
        public int GivenProduct { get; set; }
        public Dictionary<string, int>? ProductGivenByEachService { get; set; }
    }

}
