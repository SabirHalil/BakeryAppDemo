using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IDeliveryDal : IEntityRepository<Delivery>
    {
        void DeleteById(int id);

        Delivery GetLatestDelivery();
    }
}
