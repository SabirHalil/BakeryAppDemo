using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IReceivedMoneyFromServiceDal : IEntityRepository<ReceivedMoneyFromService>
    {
        void DeleteById(int id);
    }
}
