namespace BakeryAppUI.Models
{
    public class MarketBreadDetails
    {
        public int id { get; set; }
        public decimal Amount { get; set; }
        public int MarketId { get; set; }
        public string? MarketName { get; set; }
        public decimal TotalAmount { get; set; }
        public int StaleBread { get; set; }
        public int GivenBread { get; set; }
        public Dictionary<string, int>? BreadGivenByEachService { get; set; }
    }
}
