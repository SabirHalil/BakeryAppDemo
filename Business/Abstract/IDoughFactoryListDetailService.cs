using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDoughFactoryListDetailService
    {
        List<DoughFactoryListDetail> GetAll();
        List<DoughFactoryListDetail> GetByDoughFactoryList(int id);
        void Add(DoughFactoryListDetail doughFactoryListDetail);
        void Delete(DoughFactoryListDetail doughFactoryListDetail);
        void DeleteById(int id);
        void Update(DoughFactoryListDetail doughFactoryListDetail);
        DoughFactoryListDetail GetById(int id);
        bool IsExist(int id);
    }
}
