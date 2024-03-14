using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class StaleProductsReceivedFromMarketManager : IStaleProductsReceivedFromMarketService
    {


        IStaleProductsReceivedFromMarketDal _staleProductsReceivedFromMarketDal;

        public StaleProductsReceivedFromMarketManager(IStaleProductsReceivedFromMarketDal staleProductsReceivedFromMarketDal)
        {
            _staleProductsReceivedFromMarketDal = staleProductsReceivedFromMarketDal;
        }

        public void Add(StaleProductsReceivedFromMarket staleProductsReceivedFromMarket)
        {
            _staleProductsReceivedFromMarketDal.Add(staleProductsReceivedFromMarket);
        }

        public void DeleteById(int id)
        {
            _staleProductsReceivedFromMarketDal.DeleteById(id);
        }

        public void Delete(StaleProductsReceivedFromMarket staleProductsReceivedFromMarket)
        {
            _staleProductsReceivedFromMarketDal.Delete(staleProductsReceivedFromMarket);
        }
        public List<StaleProductsReceivedFromMarket> GetAll()
        {
            return _staleProductsReceivedFromMarketDal.GetAll();
        }

        public StaleProductsReceivedFromMarket GetById(int id)
        {
            return _staleProductsReceivedFromMarketDal.Get(d => d.Id == id);
        }

        public void Update(StaleProductsReceivedFromMarket staleProductsReceivedFromMarket)
        {
            _staleProductsReceivedFromMarketDal.Update(staleProductsReceivedFromMarket);
        }

        public List<StaleProductsReceivedFromMarket> GetByDateAndMarketId(int marketId, DateTime date)
        {
            return _staleProductsReceivedFromMarketDal.GetAll(s => s.MarketId == marketId && s.Date.Date == date.Date);
        }

        public int GetByDateAndMarketIdAndServiceProductId(int serviceProductId, int marketId, DateTime date)
        {
            var result = _staleProductsReceivedFromMarketDal.Get(s => s.Date.Date == date.Date && s.ServiceProductId == serviceProductId && s.MarketId == marketId);
            
            if (result != null)
            {
                return result.Quantity;
            }
            return 0;
        }
    }
}
