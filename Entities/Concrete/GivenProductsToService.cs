using Core.Entities;

namespace Entities.Concrete
{
    public class GivenProductsToService : IEntity
    {

        public int Id { get; set; }
        public int UserId { get; set; } 
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int ServiceProductId { get; set; }
        public int ServiceTypeId { get; set; }

    }
}
