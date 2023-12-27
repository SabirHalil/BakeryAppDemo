using Core.Entities;

namespace Entities.Concrete
{
    public class MoneyReceivedFromMarket : IEntity
    {
        public int Id { get; set; }
        public int MarketId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
