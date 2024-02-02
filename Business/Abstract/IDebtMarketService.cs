using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDebtMarketService
    {
        List<DebtMarket> GetAll();
        List<DebtMarket> GetDebtByMarketId(int marketId);
        decimal GetTotalAmountForMarket(int marketId);
        void Add(DebtMarket debtMarket);
        void DeleteById(int id);
        void Delete(DebtMarket debtMarket);
        void Update(DebtMarket debtMarket);
        DebtMarket GetById(int id);
        int GetDebtIdByDateAndMarketId(DateTime date,int marketId);
        bool IsExist(int id);
    }
}
