using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{


    public class EfStaleBreadDal : EfEntityRepositoryBase<StaleBread, BakeryAppContext>, IStaleBreadDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<StaleBread>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public bool IsExist(int doughFactoryProductId, DateTime date)
        {
            using (BakeryAppContext context = new BakeryAppContext())
            {

                bool exists = context.StaleBread
                    .Any(sp => sp.Date.Date == date.Date && sp.DoughFactoryProductId == doughFactoryProductId);

                return exists;
            }
        }

        public List<StaleBreadDto> GetAllByDate(DateTime date)
        {
            using (BakeryAppContext context = new())
            {
                var doughFactoryProducts = context.StaleBread
                    .Where(x => x.Date.Date == date.Date)
                    .Join(context.DoughFactoryProducts,
                    x => x.DoughFactoryProductId,
                    df => df.Id,
                    (x, df) => new StaleBreadDto
                    {
                        Id = x.Id,
                        Quantity = x.Quantity,
                        Date = x.Date,
                        DoughFactoryProductId = x.DoughFactoryProductId,
                        DoughFactoryProductName = df.Name
                    }
                    )
                    .ToList();

                return doughFactoryProducts;
            }
        }

        public List<DoughFactoryProduct> GetDoughFactoryProductsByDate(DateTime date)
        {
            using (BakeryAppContext context = new())
            {
                var doughFactoryProducts = context.DoughFactoryProducts
                    .Where(df => !context.StaleBread.Any(sb => sb.DoughFactoryProductId == df.Id && sb.Date.Date == date.Date) && df.Status == true)
                    .ToList();

                return doughFactoryProducts;
            }
        }

        public double GetReport(DateTime date)
        {
            using (BakeryAppContext context = new())
            {
                var result = context.StaleBread
                    .Where(sb => sb.Date.Date == date.Date)
                    .Join(context.DoughFactoryProducts,
                        sb => sb.DoughFactoryProductId,
                        dfp => dfp.Id,
                        (sb, dfp) => new
                        {
                            BreadEquivalent = dfp.BreadEquivalent,
                            Quantity = sb.Quantity
                        })
                    .Sum(item => item.BreadEquivalent * item.Quantity);

                return result;
            }
        }
    }
}
