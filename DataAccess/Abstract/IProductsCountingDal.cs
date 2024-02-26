using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IProductsCountingDal : IEntityRepository<ProductsCounting>
    {
        void DeleteById(int id);
        void AddList(List<ProductsCounting> productsCountings);
        Dictionary<int, int> GetDictionaryProductsCountingByDateAndCategory(DateTime date, int categoryId);
        List<ProductsCountingDto> GetProductsCountingByDateAndCategory(DateTime date, int categoryId);
    }
}
