using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICashCountingService
    {
        List<CashCounting> GetAll();
        List<CashCounting> GetCashCountingByDate(DateTime date);
        CashCounting GetOneCashCountingByDate(DateTime date);
        void Add(CashCounting cashCounting);
        void DeleteById(int id);
        void Delete(CashCounting cashCounting);
        void Update(CashCounting cashCounting);
        CashCounting GetById(int id);
    }
}
