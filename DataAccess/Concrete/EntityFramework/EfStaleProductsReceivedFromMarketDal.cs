using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStaleProductsReceivedFromMarketDal : EfEntityRepositoryBase<StaleProductsReceivedFromMarket, BakeryAppContext>, IStaleProductsReceivedFromMarketDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<StaleProductsReceivedFromMarket>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

    }
}
