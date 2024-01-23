using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class StaleBreadReceivedFromMarketManager : IStaleBreadReceivedFromMarketService
    {


        IStaleBreadReceivedFromMarketDal _staleBreadReceivedFromMarketDal;
        
        public StaleBreadReceivedFromMarketManager(IStaleBreadReceivedFromMarketDal staleBreadReceivedFromMarketDal)
        {
            _staleBreadReceivedFromMarketDal = staleBreadReceivedFromMarketDal;  
        }

        public void Add(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {
            _staleBreadReceivedFromMarketDal.Add(staleBreadReceivedFromMarket);
        }

        public void DeleteById(int id)
        {
            _staleBreadReceivedFromMarketDal.DeleteById(id);
        }

        public void Delete(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {
            _staleBreadReceivedFromMarketDal.Delete(staleBreadReceivedFromMarket);
        }
        public List<StaleBreadReceivedFromMarket> GetAll()
        {
           return _staleBreadReceivedFromMarketDal.GetAll();
        }

        public StaleBreadReceivedFromMarket GetById(int id)
        {
            return _staleBreadReceivedFromMarketDal.Get(d => d.Id == id);
        }

        public void Update(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {
            _staleBreadReceivedFromMarketDal.Update(staleBreadReceivedFromMarket);
        }

        public StaleBreadReceivedFromMarket GetByMarketId(int id, DateTime date)
        {
            return _staleBreadReceivedFromMarketDal.Get(s=>s.MarketId == id && s.Date.Date == date.Date);
        }

        public void DeleteByDateAndMarketId(DateTime date, int marketId)
        {
             _staleBreadReceivedFromMarketDal.DeleteByDateAndMarketId(date, marketId);
        }

        public int GetStaleBreadCountByMarketId(int MarketId, DateTime date)
        {
            StaleBreadReceivedFromMarket staleBreadReceivedFromMarket = _staleBreadReceivedFromMarketDal.Get(s => s.MarketId == MarketId && s.Date.Date == date.Date);


            return staleBreadReceivedFromMarket == null ? 0 : staleBreadReceivedFromMarket.Quantity;
        }

        public List<StaleBreadReceivedFromMarket> GetByDate(DateTime date)
        {
            return _staleBreadReceivedFromMarketDal.GetAll(s=>s.Date.Date == date.Date);
        }

        public bool IsExist(int marketId, DateTime date)
        {
            return _staleBreadReceivedFromMarketDal.IsExist(marketId, date);    
        }
    }
}
