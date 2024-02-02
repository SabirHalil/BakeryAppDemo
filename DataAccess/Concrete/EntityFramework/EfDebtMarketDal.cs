using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfDebtMarketDal : EfEntityRepositoryBase<DebtMarket, BakeryAppContext>, IDebtMarketDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<DebtMarket>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public decimal GetTotalAmountForMarket(int marketId)
        {
            using (BakeryAppContext context = new BakeryAppContext())
            {
                decimal totalAmount = context.DebtMarkets
                    .Where(dm => dm.MarketId == marketId)
                    .Select(dm => dm.Amount)
                    .DefaultIfEmpty() // Bu satır koleksiyon boşsa varsayılan olarak "0" ekler  *** Önemli!!
                    .Sum();
                return totalAmount;
            }
        }

        public bool IsExist(int id)
        {
            using (var context = new BakeryAppContext())
            {
                var entity = context.DebtMarkets.FirstOrDefault(p => p.Id == id);
                return entity != null;
            }
        }

    }
}
