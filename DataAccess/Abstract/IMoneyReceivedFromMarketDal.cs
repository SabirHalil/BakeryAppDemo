using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IMoneyReceivedFromMarketDal : IEntityRepository<MoneyReceivedFromMarket>
    {
        void DeleteById(int id);
        bool IsExist(int marketId, DateTime date);
    }
}
