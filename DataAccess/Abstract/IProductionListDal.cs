using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IProductionListDal : IEntityRepository<ProductionList>
    {
        void DeleteById(int id);
    }
}
