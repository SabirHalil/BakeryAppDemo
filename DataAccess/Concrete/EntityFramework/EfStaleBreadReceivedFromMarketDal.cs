using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStaleBreadReceivedFromMarketDal : EfEntityRepositoryBase<StaleBreadReceivedFromMarket, BakeryAppContext>, IStaleBreadReceivedFromMarketDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<StaleBreadReceivedFromMarket>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public void DeleteByDateAndMarketId(DateTime date, int marketId)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<StaleBreadReceivedFromMarket>().FirstOrDefault(s => s.Date.Date == date.Date && s.MarketId == marketId));
                if (deletedEntity != null)
                {
                    deletedEntity.State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public bool IsExist(int marketId, DateTime date)
        {
            using (BakeryAppContext context = new())
            {
                return context.Set<StaleBreadReceivedFromMarket>()
                    .Any(s => s.MarketId == marketId && s.Date.Date == date.Date);
            }
        }
    }
}
