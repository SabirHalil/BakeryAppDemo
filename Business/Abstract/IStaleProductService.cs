using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IStaleProductService
    {
        List<StaleProduct> GetAll();
        Dictionary<int, int> GetStaleProductsByDateAndCategory(DateTime date, int categoryId);
        void Add(StaleProduct staleProduct);
        void DeleteById(int id);
        void Delete(StaleProduct staleProduct);
        void Update(StaleProduct staleProduct);
        StaleProduct GetById(int id);
        int GetQuantityStaleProductByDateAndProductId(DateTime date, int productId);
        List<StaleProductDto> GetByDateAndCategory(DateTime date, int categoryId);
        List<Product> GetProductsNotAddedToStale(DateTime date, int categoryId);

        bool IsExist(int productId, DateTime date);
    }
}
