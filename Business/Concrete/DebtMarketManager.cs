using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class DebtMarketManager : IDebtMarketService
    {


        IDebtMarketDal _debtMarketDal;
        
        public DebtMarketManager(IDebtMarketDal debtMarketDal)
        {
            _debtMarketDal = debtMarketDal;  
        }

        public void Add(DebtMarket debtMarket)
        {
            _debtMarketDal.Add(debtMarket);
        }

        public void DeleteById(int id)
        {
            _debtMarketDal.DeleteById(id);
        }

        public void Delete(DebtMarket debtMarket)
        {
            _debtMarketDal.Delete(debtMarket);
        }
        public List<DebtMarket> GetAll()
        {
           return _debtMarketDal.GetAll();
        }

        public DebtMarket GetById(int id)
        {
            return _debtMarketDal.Get(d => d.Id == id);
        }

        public void Update(DebtMarket debtMarket)
        {
            _debtMarketDal.Update(debtMarket);
        }

        public int GetDebtIdByDateAndMarketId(DateTime date, int marketId)
        {
            DebtMarket debtMarket = _debtMarketDal.Get(d => d.Date.Date == date.Date && d.MarketId == marketId && d.Amount>0);
            return debtMarket == null ? 0 : debtMarket.Id;
        }

        public bool IsExist(int id)
        {
            return _debtMarketDal.IsExist(id);
        }

        public List<DebtMarket> GetDebtByMarketId(int marketId)
        {
            return _debtMarketDal.GetAll(d=> d.MarketId == marketId);
        }
    }
}
