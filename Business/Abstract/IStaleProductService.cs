using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IStaleProductService
    {
        List<StaleProduct> GetAll();
        void Add(StaleProduct staleProduct);
        void DeleteById(int id);
        void Delete(StaleProduct staleProduct);
        void Update(StaleProduct staleProduct);
        StaleProduct GetById(int id);
        List<StaleProductDto> GetByDateAndCategory(DateTime date, int categoryId);
        List<Product> GetProductsNotAddedToStale(DateTime date, int categoryId);
    }
}
