using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceRemindMoneyService
    {
        List<ServiceRemindMoney> GetAll();
        void Add(ServiceRemindMoney serviceRemindMoney);
        void DeleteById(int id);
        void Delete(ServiceRemindMoney serviceRemindMoney);
        void Update(ServiceRemindMoney serviceRemindMoney);
        ServiceRemindMoney GetById(int id);
    }
}
