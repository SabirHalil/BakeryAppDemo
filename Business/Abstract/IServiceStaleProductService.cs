using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceStaleProductService
    {
        List<ServiceStaleProduct> GetAll();
        void Add(ServiceStaleProduct serviceStaleProduct);
        void DeleteById(int id);
        void Delete(ServiceStaleProduct serviceStaleProduct);
        void Update(ServiceStaleProduct serviceStaleProduct);
        ServiceStaleProduct GetById(int id);
    }
}
