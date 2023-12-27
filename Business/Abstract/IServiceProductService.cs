using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceProductService
    {
        List<ServiceProduct> GetAll();
        void Add(ServiceProduct serviceProduct);
        void DeleteById(int id);
        void Delete(ServiceProduct serviceProduct);
        void Update(ServiceProduct serviceProduct);
        ServiceProduct GetById(int id);
    }
}
