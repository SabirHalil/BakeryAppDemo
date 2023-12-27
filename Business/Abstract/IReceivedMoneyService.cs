using Entities.Concrete;

namespace Business.Abstract
{
    public interface IReceivedMoneyService
    {
        List<ReceivedMoney> GetAll();
        void Add(ReceivedMoney receivedMoney);
        void DeleteById(int id);
        void Delete(ReceivedMoney receivedMoney);
        void Update(ReceivedMoney receivedMoney);
        ReceivedMoney GetById(int id);
    }
}
