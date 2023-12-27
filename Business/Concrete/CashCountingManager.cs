using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CashCountingManager : ICashCountingService
    {


        ICashCountingDal _cashCountingDal;
        
        public CashCountingManager(ICashCountingDal cashCountingDal)
        {
            _cashCountingDal = cashCountingDal;  
        }

        public void Add(CashCounting cashCounting)
        {
            _cashCountingDal.Add(cashCounting);
        }

        public void DeleteById(int id)
        {
            _cashCountingDal.DeleteById(id);
        }

        public void Delete(CashCounting cashCounting)
        {
            _cashCountingDal.Delete(cashCounting);
        }
        public List<CashCounting> GetAll()
        {
           return _cashCountingDal.GetAll();
        }

        public CashCounting GetById(int id)
        {
            return _cashCountingDal.Get(d => d.Id == id);
        }

        public void Update(CashCounting cashCounting)
        {
            _cashCountingDal.Update(cashCounting);
        }

        public List<CashCounting> GetCashCountingByDate(DateTime date)
        {
            return _cashCountingDal.GetAll(c=>c.Date.Date == date.Date);
        }
    }
}
