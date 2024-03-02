using Core.Entities;

namespace Entities.Concrete
{
    public class CreditCardAmount :IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
