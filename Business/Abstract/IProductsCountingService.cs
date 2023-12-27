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
        bool IsExist(int productId);
    }
}
