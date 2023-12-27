
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceListService 
    {
        List<ServiceList> GetAll();
        int Add(ServiceList serviceList);
        void DeleteById(int id);
        void Delete(ServiceList serviceList);
        void Update(ServiceList serviceList);
        ServiceList GetById(int id);

        List<ServiceList> GetByDate(DateTime date);
    }
}
