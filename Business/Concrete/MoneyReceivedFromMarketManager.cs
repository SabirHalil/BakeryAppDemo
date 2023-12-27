using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MoneyReceivedFromMarketManager : IMoneyReceivedFromMarketService
    {


        IMoneyReceivedFromMarketDal _moneyReceivedFromMarketDal;
        
        public MoneyReceivedFromMarketManager(IMoneyReceivedFromMarketDal moneyReceivedFromMarketDal)
        {
            _moneyReceivedFromMarketDal = moneyReceivedFromMarketDal;  
        }

        public void Add(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            _moneyReceivedFromMarketDal.Add(moneyReceivedFromMarket);
        }

        public void DeleteById(int id)
        {
            _moneyReceivedFromMarketDal.DeleteById(id);
        }

        public void Delete(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            _moneyReceivedFromMarketDal.Delete(moneyReceivedFromMarket);
        }
        public List<MoneyReceivedFromMarket> GetAll()
        {
           return _moneyReceivedFromMarketDal.GetAll();
        }

        public MoneyReceivedFromMarket GetById(int id)
        {
            return _moneyReceivedFromMarketDal.Get(d => d.Id == id);
        }

        public void Update(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            _moneyReceivedFromMarketDal.Update(moneyReceivedFromMarket);
        }

        public List<MoneyReceivedFromMarket> GetByMarketId(int id)
        {
            return _moneyReceivedFromMarketDal.GetAll(s => s.MarketId == id);
        }

        public List<MoneyReceivedFromMarket> GetByDate(DateTime date)
        {
            return _moneyReceivedFromMarketDal.GetAll(d => d.Date.Date == date.Date);
        }
    }
}
