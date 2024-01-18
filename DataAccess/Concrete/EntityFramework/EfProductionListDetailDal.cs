using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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

        public List<GetAddedProductsDto> GetAddedProducts(int id)
        {
            using(BakeryAppContext context = new())
            {
                var addedProducts = (from plDetail in context.Set<ProductionListDetail>()
                                     join product in context.Set<Product>() on plDetail.ProductId equals product.Id
                                     where plDetail.ProductionListId == id
                                     select new GetAddedProductsDto
                                     {
                                         Id = plDetail.Id,
                                         ProductId = plDetail.ProductId,
                                         ProductName = product.Name, // Assuming you want the product name
                                         Price = plDetail.Price,
                                         ProductionListId = plDetail.ProductionListId,
                                         Quantity = plDetail.Quantity
                                     }).ToList();
                return addedProducts;
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
