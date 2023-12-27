using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IServiceStaleProductDal : IEntityRepository<ServiceStaleProduct>
    {
        void DeleteById(int id);
    }
}
