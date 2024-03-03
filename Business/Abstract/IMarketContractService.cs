using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMarketContractService
    {
        List<MarketContract> GetAll();
        void Add(MarketContract marketContract);
        void DeleteById(int id);
        void Delete(MarketContract marketContract);
        void Update(MarketContract marketContract);
        MarketContract GetById(int id);
        int GetIdByMarketId(int id);

        int GetIdByMarketIdAndServiceProductId(int marketId, int serviceProductId);

        int GetMarketIdById(int id);
        decimal GetPriceById(int id);
    }
}
