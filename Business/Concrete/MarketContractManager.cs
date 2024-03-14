using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MarketContractManager : IMarketContractService
    {


        IMarketContractDal _marketContractDal;
        
        public MarketContractManager(IMarketContractDal marketContractDal)
        {
            _marketContractDal = marketContractDal;  
        }

        public void Add(MarketContract marketContract)
        {
            _marketContractDal.Add(marketContract);
        }

        public void DeleteById(int id)
        {
            _marketContractDal.DeleteById(id);
        }

        public void Delete(MarketContract marketContract)
        {
            _marketContractDal.Delete(marketContract);
        }
        public List<MarketContract> GetAll()
        {
           return _marketContractDal.GetAll();
        }

        public MarketContract GetById(int id)
        {
            return _marketContractDal.Get(d => d.Id == id);
        }

        public void Update(MarketContract marketContract)
        {
            _marketContractDal.Update(marketContract);
        }

        public decimal GetPriceById(int id)
        {                            
            return _marketContractDal.Get(p => p.Id == id).Price;           
        }

        public int GetIdByMarketId(int id)
        {
            return _marketContractDal.Get(p => p.MarketId == id).Id;
        }

        public int GetMarketIdById(int id)
        {
            return _marketContractDal.Get(p => p.Id == id).MarketId;
        }

        public int GetIdByMarketIdAndServiceProductId(int marketId, int serviceProductId)
        {
            return _marketContractDal.Get(p => p.MarketId == marketId && p.ServiceProductId == serviceProductId).Id;
        }

        public List<int> ServiceProductsIdsByMarketId(int marketId)
        {
            return _marketContractDal.ServiceProductsIdsByMarketId(marketId);
        }
    }
}
