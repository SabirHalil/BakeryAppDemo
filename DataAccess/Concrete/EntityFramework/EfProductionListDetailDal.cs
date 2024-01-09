using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductionListDetailDal : EfEntityRepositoryBase<ProductionListDetail, BakeryAppContext>, IProductionListDetailDal
    {

        public void AddList(List<ProductionListDetail> productionListDetail)
        {
            using (BakeryAppContext context = new())
            {
                foreach (var production in productionListDetail)
                {
                    var deletedEntity = context.Entry(production);
                    deletedEntity.State = EntityState.Added;
                }
                context.SaveChanges();

            }
        }

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<ProductionListDetail>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public bool IsExist(int id, int listId)
        {
            using (var context = new BakeryAppContext())
            {
                return (context.ProductionListDetails?.Any(p => p.ProductId == id && p.ProductionListId == listId)).GetValueOrDefault();
            }
        }

    }
}
