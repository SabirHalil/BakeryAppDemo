using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ReceivedMoneyManager : IReceivedMoneyService
    {


        IReceivedMoneyDal _receivedMoneyDal;
        
        public ReceivedMoneyManager(IReceivedMoneyDal receivedMoneyDal)
        {
            _receivedMoneyDal = receivedMoneyDal;  
        }

        public void Add(ReceivedMoney receivedMoney)
        {
            _receivedMoneyDal.Add(receivedMoney);
        }

        public void DeleteById(int id)
        {
            _receivedMoneyDal.DeleteById(id);
        }

        public void Delete(ReceivedMoney receivedMoney)
        {
            _receivedMoneyDal.Delete(receivedMoney);
        }
        public List<ReceivedMoney> GetAll()
        {
           return _receivedMoneyDal.GetAll();
        }

        public ReceivedMoney GetById(int id)
        {
            return _receivedMoneyDal.Get(d => d.Id == id);
        }

        public void Update(ReceivedMoney receivedMoney)
        {
            _receivedMoneyDal.Update(receivedMoney);
        }
    }
}
