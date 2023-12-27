using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IReceivedMoneyDal : IEntityRepository<ReceivedMoney>
    {
        void DeleteById(int id);
    }
}
