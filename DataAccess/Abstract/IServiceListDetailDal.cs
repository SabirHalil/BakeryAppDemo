using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IServiceListDetailDal : IEntityRepository<ServiceListDetail>
    {
        void DeleteById(int id);
        bool IsExist(int id);

        void DeleteByServiceListIdAndMarketContracId(int serviceListId, int marketContracId);

    }
}
