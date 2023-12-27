using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICashCountingDal : IEntityRepository<CashCounting>
    {
        void DeleteById(int id);
    }
}
