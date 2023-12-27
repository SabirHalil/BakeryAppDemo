using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IMarketContractDal : IEntityRepository<MarketContract>
    {
        void DeleteById(int id);
    }
}
