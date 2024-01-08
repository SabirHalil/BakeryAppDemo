using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        void DeleteById(int id);

        List<Product> GetNotAddedProductsByListAndCategoryId(int listId, int categoryId);
    }
}
