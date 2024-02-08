using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IProductsCountingDal : IEntityRepository<ProductsCounting>
    {
        void DeleteById(int id);
        void AddList(List<ProductsCounting> productsCountings);
        Dictionary<int, int> GetProductsCountingByDateAndCategory(DateTime date, int categoryId);
    }
}
