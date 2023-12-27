using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ServiceRemindMoneyManager : IServiceRemindMoneyService
    {


        IServiceRemindMoneyDal _serviceRemindMoneyDal;
        
        public ServiceRemindMoneyManager(IServiceRemindMoneyDal serviceRemindMoneyDal)
        {
            _serviceRemindMoneyDal = serviceRemindMoneyDal;  
        }

        public void Add(ServiceRemindMoney serviceRemindMoney)
        {
            _serviceRemindMoneyDal.Add(serviceRemindMoney);
        }

        public void DeleteById(int id)
        {
            _serviceRemindMoneyDal.DeleteById(id);
        }

        public void Delete(ServiceRemindMoney serviceRemindMoney)
        {
            _serviceRemindMoneyDal.Delete(serviceRemindMoney);
        }
        public List<ServiceRemindMoney> GetAll()
        {
           return _serviceRemindMoneyDal.GetAll();
        }

        public ServiceRemindMoney GetById(int id)
        {
            return _serviceRemindMoneyDal.Get(d => d.Id == id);
        }

        public void Update(ServiceRemindMoney serviceRemindMoney)
        {
            _serviceRemindMoneyDal.Update(serviceRemindMoney);
        }
    }
}
