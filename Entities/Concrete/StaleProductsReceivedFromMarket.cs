using Core.Entities;

namespace Entities.Concrete
{
    public class StaleProductsReceivedFromMarket : IEntity
    {
        public int Id { get; set; }
        public int MarketId { get; set; }
        public int ServiceProductId { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
    }
}
