using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IStaleProductDal : IEntityRepository<StaleProduct>
    {
        void DeleteById(int id);

        List<StaleProductDto> GetByDateAndCategory(DateTime date, int categoryId);
        List<Product> GetProductsNotAddedToStale(DateTime date, int categoryId);
    }
}
