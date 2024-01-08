using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, BakeryAppContext>, IProductDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<Product>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public List<Product> GetNotAddedProductsByListAndCategoryId(int listId, int categoryId)
        {
            using (BakeryAppContext context = new())
            {
                if (listId == 0)
                {
                    var getCategoryProducts = context.Products
                         .Where(p => p.CategoryId == categoryId)
                         .ToList();

                    return getCategoryProducts;
                }
                else
                {
                    var productIdsInProductionList = context.ProductionListDetails
                   .Where(m => m.ProductionListId == listId)
                   .Select(q => q.ProductId)
                   .ToList();

                    var productsNotInProductionList = context.Products
                        .Where(p => p.CategoryId == categoryId && !productIdsInProductionList.Contains(p.Id))
                        .ToList();

                    return productsNotInProductionList;
                }

            }
        }
    }
}
