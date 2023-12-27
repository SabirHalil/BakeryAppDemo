using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDoughFactoryListService
    {
        List<DoughFactoryList> GetAll();
        int Add(DoughFactoryList doughFactoryList);
        void Delete(DoughFactoryList doughFactoryList);
        void Update(DoughFactoryList doughFactoryList);
        DoughFactoryList GetById(int id);
        List<DoughFactoryList> GetByDate(DateTime date);
    }
}
