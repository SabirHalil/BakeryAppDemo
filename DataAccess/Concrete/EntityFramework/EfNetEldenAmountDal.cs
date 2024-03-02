using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNetEldenAmountDal : EfEntityRepositoryBase<NetEldenAmount, BakeryAppContext>, INetEldenAmountDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<NetEldenAmount>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }


        public decimal TotalAmountBetweenDates(DateTime startDateTime, DateTime endDateTime)
        {
            using (BakeryAppContext context = new BakeryAppContext())
            {
                decimal totalAmount = context.Set<NetEldenAmount>()
                    .Where(e => e.Date >= startDateTime && e.Date <= endDateTime)
                    .Sum(e => e.Amount);

                return totalAmount;
            }
        }




    }
}
