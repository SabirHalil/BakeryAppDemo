using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IMoneyReceivedFromMarketDal : IEntityRepository<MoneyReceivedFromMarket>
    {
        void DeleteById(int id);
        bool IsExist(int marketId, DateTime date);
        List<MoneyReceivedMarket> MoneyReceivedMarkets(DateTime date);
        List<Market> ServiceProductsDeliveredMarkets(DateTime date);

    }
}
