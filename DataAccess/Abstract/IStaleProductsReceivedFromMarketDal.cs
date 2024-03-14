using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IStaleProductsReceivedFromMarketDal : IEntityRepository<StaleProductsReceivedFromMarket>
    {
        void DeleteById(int id);
    }
}
