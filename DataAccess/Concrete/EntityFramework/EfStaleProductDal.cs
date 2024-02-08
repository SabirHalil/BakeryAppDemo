using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStaleProductDal : EfEntityRepositoryBase<StaleProduct, BakeryAppContext>, IStaleProductDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<StaleProduct>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public List<StaleProductDto> GetByDateAndCategory(DateTime date, int categoryId)
        {
            using (BakeryAppContext context = new())
            {
                var staleProductDtos = context.StaleProducts
                    .Where(s => s.Date.Date == date.Date)
                    .Select(stale => new
                    {
                        Stale = stale,
                        Product = context.Products.FirstOrDefault(p => p.Id == stale.ProductId)
                    })
                    .Where(pair => pair.Product != null && pair.Product.CategoryId == categoryId)
                    .Select(pair => new StaleProductDto
                    {
                        Id = pair.Stale.Id,
                        ProductId = pair.Stale.ProductId,
                        ProductName = pair.Product.Name,
                        Quantity = pair.Stale.Quantity,
                        Date = pair.Stale.Date
                    })
                    .ToList();

                return staleProductDtos;
            }
        }

        public List<Product> GetProductsNotAddedToStale(DateTime date, int categoryId)
        {
            using (BakeryAppContext context = new())
            {
                var productsNotAddedToStale = context.Products
                    .Where(p => p.CategoryId == categoryId)
                    .Where(p => !context.StaleProducts.Any(sp => sp.Date.Date == date.Date && sp.ProductId == p.Id))
                    .ToList();
                return productsNotAddedToStale;
            }
        }

        public Dictionary<int, int> GetStaleProductsByDateAndCategory(DateTime date, int categoryId)
        {
            using (BakeryAppContext context = new BakeryAppContext())
            {
                var staleProductQuantities = context.StaleProducts
                    .Where(s => s.Date.Date == date.Date)
                    .Join(context.Products,
                          stale => stale.ProductId,
                          product => product.Id,
                          (stale, product) => new { Stale = stale, Product = product })
                    .Where(pair => pair.Product != null && pair.Product.CategoryId == categoryId)
                    .ToDictionary(pair => pair.Stale.ProductId, pair => pair.Stale.Quantity);

                return staleProductQuantities;
            }
        }

        public bool IsExist(int productId, DateTime date)
        {
            using (BakeryAppContext context = new BakeryAppContext())
            {

                bool exists = context.StaleProducts
                    .Any(sp => sp.Date.Date == date.Date && sp.ProductId == productId);

                return exists;
            }
        }
    }
}
