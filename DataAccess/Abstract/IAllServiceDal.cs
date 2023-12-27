using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IAllServiceDal : IEntityRepository<AllService>
    {
        void DeleteById(int id);
    }
}
