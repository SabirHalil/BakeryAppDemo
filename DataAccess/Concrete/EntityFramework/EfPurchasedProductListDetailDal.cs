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
            using (BakeryAppContext context = new BakeryAppContext())
            {              
                var entityToDelete = context.Set<PurchasedProductListDetail>().FirstOrDefault(p => p.Id == id && p.UserId == userId);

                if (entityToDelete != null)
                {                
                    context.Entry(entityToDelete).State = EntityState.Deleted;
                    context.SaveChanges();
                }             
            }
        }

    }
}
