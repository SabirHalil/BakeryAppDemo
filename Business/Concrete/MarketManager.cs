using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MarketManager : IMarketService
    {


        IMarketDal _marketDal;
        
        public MarketManager(IMarketDal marketDal)
        {
            _marketDal = marketDal;  
        }

        public void Add(Market market)
        {
            _marketDal.Add(market);
        }

        public void DeleteById(int id)
        {
            _marketDal.DeleteById(id);
        }

        public void Delete(Market market)
        {
            _marketDal.Delete(market);
        }
        public List<Market> GetAll()
        {
           return _marketDal.GetAll();
        }

        public Market GetById(int id)
        {
            return _marketDal.Get(d => d.Id == id);
        }
        public string GetNameById(int id)
        {
            return _marketDal.Get(d => d.Id == id).Name;
        }

        public void Update(Market market)
        {
            _marketDal.Update(market);
        }
    }
}
