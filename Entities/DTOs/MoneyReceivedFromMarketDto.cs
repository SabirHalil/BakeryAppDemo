using Core.Entities;

namespace Entities.DTOs
{
    public class MoneyReceivedFromMarketDto : IDto
    {        
        public int Id { get; set; }
        public int MarketId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ReceivedAmount { get; set; }
    }
}
