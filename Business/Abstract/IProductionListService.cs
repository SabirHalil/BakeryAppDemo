using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductionListService
    {
        List<ProductionList> GetAll();
        int Add(ProductionList productionList);
        void DeleteById(int id);
        int GetByDateAndCategoryId(DateTime date, int categoryId);
        void Delete(ProductionList productionList);
        void Update(ProductionList productionList);
        ProductionList GetById(int id);
    }
}
