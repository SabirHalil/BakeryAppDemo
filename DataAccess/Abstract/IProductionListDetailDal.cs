using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IProductionListDetailDal : IEntityRepository<ProductionListDetail>
    {
        void DeleteById(int id);

        bool IsExist(int id);
        void AddList(List<ProductionListDetail> productionListDetail);
    }
}
