using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IProductionListDetailService
    {
        List<ProductionListDetail> GetAll();
        List<GetAddedProductsDto > GetProductsByListId(int id);
        bool IsExist(int id, int listId);
        void Add(ProductionListDetail productionListDetail);
        void AddList(List<ProductionListDetail> productionListDetail);
        void DeleteById(int id);
        void Delete(ProductionListDetail productionListDetail);
        void Update(ProductionListDetail productionListDetail);
        ProductionListDetail GetById(int id);
        ProductionListDetail GetProductionListDetailByDateAndProductId(DateTime date, int productId, int c);
    }
}
