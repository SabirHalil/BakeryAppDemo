using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IServiceListDal : IEntityRepository<ServiceList>
    {
        void DeleteById(int id);
    }
}
