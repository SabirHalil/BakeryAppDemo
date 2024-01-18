using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductsCountingDal : EfEntityRepositoryBase<ProductsCounting, BakeryAppContext>, IProductsCountingDal
    {
        public void AddList(List<ProductsCounting> productsCountings)
        {
            using (BakeryAppContext context = new BakeryAppContext())
            {
                context.ProductsCountings.AddRange(productsCountings);
                context.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<ProductsCounting>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

    }
}
