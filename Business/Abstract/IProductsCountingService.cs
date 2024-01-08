using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductsCountingService
    {
        List<ProductsCounting> GetAll();
        List<ProductsCounting> GetProductsCountingByDate(DateTime date);

        void Add(ProductsCounting productsCounting);
        void DeleteById(int id);
        void Delete(ProductsCounting productsCounting);
        void Update(ProductsCounting productsCounting);
        ProductsCounting GetById(int id);
        int GetQuantityProductsCountingByDateAndProductId(DateTime date,int productId);
        bool IsExist(int productId);
    }
}
