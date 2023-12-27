using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IDoughFactoryListDetailDal : IEntityRepository<DoughFactoryListDetail>
    {
        void DeleteById(int id);
        bool IsExist(int id);
    }
}
