using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IServiceProductDal : IEntityRepository<ServiceProduct>
    {
        void DeleteById(int id);
    }
}
