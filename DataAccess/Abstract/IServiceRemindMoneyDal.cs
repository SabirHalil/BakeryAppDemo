using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IServiceRemindMoneyDal : IEntityRepository<ServiceRemindMoney>
    {
        void DeleteById(int id);
    }
}
