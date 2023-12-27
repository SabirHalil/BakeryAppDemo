using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IBreadCountingDal : IEntityRepository<BreadCounting>
    {
        void DeleteById(int id);
    }
}
