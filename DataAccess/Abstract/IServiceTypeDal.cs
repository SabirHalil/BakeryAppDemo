using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IServiceTypeDal : IEntityRepository<ServiceType>
    {
        void DeleteById(int id);
    }
}
