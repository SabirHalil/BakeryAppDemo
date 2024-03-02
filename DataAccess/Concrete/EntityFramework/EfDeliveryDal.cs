using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfDeliveryDal : EfEntityRepositoryBase<Delivery, BakeryAppContext>, IDeliveryDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<Delivery>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public Delivery GetLatestDelivery()
        {
            using (BakeryAppContext context = new BakeryAppContext())
            {
                return context.Set<Delivery>()
                    .OrderByDescending(d => d.DeliveryDate)
                    .FirstOrDefault();
            }
        }


    }
}
