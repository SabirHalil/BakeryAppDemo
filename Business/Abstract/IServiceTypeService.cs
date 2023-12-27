using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceTypeService
    {
        List<ServiceType> GetAll();
        void Add(ServiceType serviceType);
        void DeleteById(int id);
        void Delete(ServiceType serviceType);
        void Update(ServiceType serviceType);
        ServiceType GetById(int id);
    }
}
