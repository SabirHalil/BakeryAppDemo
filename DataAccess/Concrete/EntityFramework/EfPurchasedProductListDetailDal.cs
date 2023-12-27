using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPurchasedProductListDetailDal : EfEntityRepositoryBase<PurchasedProductListDetail, BakeryAppContext>, IPurchasedProductListDetailDal
    {

        public void DeleteById(int id, int userId)
        {
            //using (BakeryAppContext context = new())
            //{
            //    var deletedEntity = context.Entry(context.Set<PurchasedProductListDetail>().Where(p=>p.Id==id &&p.UserId==userId));
            //    deletedEntity.State = EntityState.Deleted;
            //    context.SaveChanges();

            //}
        }

    }
}
