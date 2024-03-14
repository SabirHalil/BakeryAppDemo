using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IMoneyReceivedFromMarketService
    {
        List<Market> GetNotMoneyReceivedMarketListByDate(DateTime date);
        List<MoneyReceivedMarket> GetMoneyReceivedMarketListByDate(DateTime date);
        List<MoneyReceivedFromMarket> GetAll();
        List<MoneyReceivedFromMarket> GetByMarketId(int id);
        MoneyReceivedFromMarket GetByMarketIdAndDate(int id, DateTime date);
        List<MoneyReceivedFromMarket> GetByDate(DateTime date);
        void Add(MoneyReceivedFromMarket moneyReceivedFromMarket);
        void DeleteById(int id);
        void Delete(MoneyReceivedFromMarket moneyReceivedFromMarket);
        void Update(MoneyReceivedFromMarket moneyReceivedFromMarket);
        MoneyReceivedFromMarket GetById(int id);
        bool IsExist(int marketId, DateTime date);
    }
}
