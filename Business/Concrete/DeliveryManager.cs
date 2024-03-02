using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class DeliveryManager : IDeliveryService
    {


        IDeliveryDal _deliveryDal;
        INetEldenAmountDal _netEldenAmountDal;

        public DeliveryManager(IDeliveryDal deliveryDal, INetEldenAmountDal netEldenAmountDal)
        {
            _deliveryDal = deliveryDal;
            _netEldenAmountDal = netEldenAmountDal;
        }

        public void Add(Delivery delivery)
        {
            _deliveryDal.Add(delivery);
        }

        public void DeleteById(int id)
        {
            _deliveryDal.DeleteById(id);
        }

        public void Delete(Delivery delivery)
        {
            _deliveryDal.Delete(delivery);
        }
        public List<Delivery> GetAll()
        {
            return _deliveryDal.GetAll();
        }

        public Delivery GetById(int id)
        {
            return _deliveryDal.Get(d => d.Id == id);
        }

        public void Update(Delivery delivery)
        {
            _deliveryDal.Update(delivery);
        }

        public decimal PaymentAmountDue(DateTime date)
        {
            var result = _deliveryDal.GetLatestDelivery();

            DateTime startDateTime = result == null ? new DateTime(2023, 1, 1) : result.DeliveryDate;

            decimal transferredAmount = result != null ? (result.TotalAccumulatedAmount - result.DeliveredAmount) : 0;
            decimal totalAmount = _netEldenAmountDal?.TotalAmountBetweenDates(startDateTime, date) ?? 0;

            return totalAmount + transferredAmount;
        }

        public List<Delivery> GetBetweenDates(DateTime startDate, DateTime endDate)
        {
            return _deliveryDal.GetAll(delivery => delivery.DeliveryDate >= startDate && delivery.DeliveryDate <= endDate.AddDays(1));
        }
    }
}
