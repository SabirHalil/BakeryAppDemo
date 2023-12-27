using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IMarketDal : IEntityRepository<Market>
    {
        void DeleteById(int id);
    }
}
