using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IBreadPriceDal : IEntityRepository<BreadPrice>
    {
        void DeleteById(int id);
    }
}
