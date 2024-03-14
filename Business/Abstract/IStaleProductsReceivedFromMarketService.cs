using Entities.Concrete;

namespace Business.Abstract
{
    public interface IStaleProductsReceivedFromMarketService
    {
        List<StaleProductsReceivedFromMarket> GetAll();
        List<StaleProductsReceivedFromMarket> GetByDateAndMarketId(int marketId, DateTime date);
        int GetByDateAndMarketIdAndServiceProductId(int serviceProductId,int marketId, DateTime date);
        void Add(StaleProductsReceivedFromMarket staleProductsReceivedFromMarket);
        void DeleteById(int id);
        void Delete(StaleProductsReceivedFromMarket staleProductsReceivedFromMarket);
        void Update(StaleProductsReceivedFromMarket staleProductsReceivedFromMarket);
        StaleProductsReceivedFromMarket GetById(int id);
    }
}
