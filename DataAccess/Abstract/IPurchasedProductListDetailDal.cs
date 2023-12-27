using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IPurchasedProductListDetailDal : IEntityRepository<PurchasedProductListDetail>
    {
        void DeleteById(int id, int userId);
    }
}
