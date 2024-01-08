using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBreadCountingService
    {
        List<BreadCounting> GetAll();
        BreadCounting GetBreadCountingByDate(DateTime date);
        void Add(BreadCounting breadCounting);
        void AddList(List<BreadCounting> breadCounting);
        void DeleteById(int id);
        void Delete(BreadCounting breadCounting);
        void Update(BreadCounting breadCounting);
        BreadCounting GetById(int id);
    }
}
