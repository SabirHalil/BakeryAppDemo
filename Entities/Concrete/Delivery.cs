using Core.Entities;

namespace Entities.Concrete
{
    public class Delivery :IEntity
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal DeliveredAmount { get; set; }
        public decimal TotalAccumulatedAmount { get; set; }
    }
}
