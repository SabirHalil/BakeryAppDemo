using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IDebtMarketDal : IEntityRepository<DebtMarket>
    {
        void DeleteById(int id);
        bool IsExist(int id);
        decimal GetTotalAmountForMarket(int marketId);
    }
}
