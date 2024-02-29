using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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

        public Dictionary<int, int> GetDictionaryProductsCountingByDateAndCategory(DateTime date, int categoryId)
        {
            using (BakeryAppContext context = new BakeryAppContext())
            {
                var productsCountingQuantities = context.ProductsCountings
                    .Where(pc => pc.Date.Date == date.Date)
                    .Join(context.Products,
                          counting => counting.ProductId,
                          product => product.Id,
                          (counting, product) => new { Counting = counting, Product = product })
                    .Where(pair => pair.Product != null && pair.Product.CategoryId == categoryId)
                    .ToDictionary(pair => pair.Counting.ProductId, pair => pair.Counting.Quantity);

                return productsCountingQuantities;
            }
        }

        public List<ProductsCountingDto> GetProductsCountingByDateAndCategory(DateTime date, int categoryId)
        {
            using (BakeryAppContext context = new BakeryAppContext())
            {
                var productsCountingList = context.ProductsCountings
                    .Where(pc => pc.Date.Date == date.Date)
                    .Join(context.Products,
                          counting => counting.ProductId,
                          product => product.Id,
                          (counting, product) => new { Counting = counting, Product = product })
                    .Where(pair => pair.Product != null && pair.Product.CategoryId == categoryId)
                    .Select(pair => new ProductsCountingDto
                    {
                        Id = pair.Counting.Id,
                        ProductId = pair.Counting.ProductId,
                        Quantity = pair.Counting.Quantity,
                        ProductName = pair.Product.Name,
                        Date = pair.Counting.Date,
                    })
                    .ToList();

                return productsCountingList;
            }
        }
    }
}
