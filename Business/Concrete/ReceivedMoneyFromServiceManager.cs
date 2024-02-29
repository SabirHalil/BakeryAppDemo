using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ReceivedMoneyFromServiceManager : IReceivedMoneyFromServiceService
    {


        IReceivedMoneyFromServiceDal _receivedMoneyFromServiceDal;
        
        public ReceivedMoneyFromServiceManager(IReceivedMoneyFromServiceDal receivedMoneyFromServiceDal)
        {
            _receivedMoneyFromServiceDal = receivedMoneyFromServiceDal;  
        }

        public void Add(ReceivedMoneyFromService receivedMoneyFromService)
        {
            _receivedMoneyFromServiceDal.Add(receivedMoneyFromService);
        }

        public void DeleteById(int id)
        {
            _receivedMoneyFromServiceDal.DeleteById(id);
        }

        public void Delete(ReceivedMoneyFromService receivedMoneyFromService)
        {
            _receivedMoneyFromServiceDal.Delete(receivedMoneyFromService);
        }
        public List<ReceivedMoneyFromService> GetAll()
        {
           return _receivedMoneyFromServiceDal.GetAll();
        }

        public ReceivedMoneyFromService? GetByDate(DateTime date, int serviceType)
        {
            return _receivedMoneyFromServiceDal.Get(d => d.Date.Date == date.Date && d.ServiceTypeId == serviceType);
        }

        public void Update(ReceivedMoneyFromService receivedMoneyFromService)
        {
            _receivedMoneyFromServiceDal.Update(receivedMoneyFromService);
        }
    }
}
