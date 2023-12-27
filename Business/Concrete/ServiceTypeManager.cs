using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ServiceTypeManager : IServiceTypeService
    {


        IServiceTypeDal _serviceTypeDal;
        
        public ServiceTypeManager(IServiceTypeDal serviceTypeDal)
        {
            _serviceTypeDal = serviceTypeDal;  
        }

        public void Add(ServiceType serviceType)
        {
            _serviceTypeDal.Add(serviceType);
        }

        public void DeleteById(int id)
        {
            _serviceTypeDal.DeleteById(id);
        }

        public void Delete(ServiceType serviceType)
        {
            _serviceTypeDal.Delete(serviceType);
        }
        public List<ServiceType> GetAll()
        {
           return _serviceTypeDal.GetAll();
        }

        public ServiceType GetById(int id)
        {
            return _serviceTypeDal.Get(d => d.Id == id);
        }

        public void Update(ServiceType serviceType)
        {
            _serviceTypeDal.Update(serviceType);
        }
    }
}
