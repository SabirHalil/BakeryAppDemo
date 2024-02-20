using Entities.Concrete;

namespace Business.Abstract
{
    public interface IReceivedMoneyFromServiceService
    {
        List<ReceivedMoneyFromService> GetAll();
        void Add(ReceivedMoneyFromService allService);
        void DeleteById(int id);
        void Delete(ReceivedMoneyFromService allService);
        void Update(ReceivedMoneyFromService allService);
        ReceivedMoneyFromService GetByDate(DateTime date, int serviceType);
    }
}
