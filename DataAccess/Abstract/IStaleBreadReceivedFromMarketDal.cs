using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IStaleBreadReceivedFromMarketDal : IEntityRepository<StaleBreadReceivedFromMarket>
    {
        void DeleteById(int id);
        void DeleteByDateAndMarketId(DateTime date, int marketId);
        bool IsExist(int marketId, DateTime date);
    }
}
