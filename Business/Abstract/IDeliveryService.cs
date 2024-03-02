using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDeliveryService
    {

        decimal PaymentAmountDue(DateTime date);
        List<Delivery> GetAll();
        List<Delivery> GetBetweenDates(DateTime startDate, DateTime endDate);
        void Add(Delivery delivery);
        void DeleteById(int id);
        void Delete(Delivery delivery);
        void Update(Delivery delivery);
        Delivery GetById(int id);
    }
}
