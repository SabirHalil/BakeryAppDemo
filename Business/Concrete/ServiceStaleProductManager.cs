using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ServiceStaleProductManager : IServiceStaleProductService
    {


        IServiceStaleProductDal _serviceStaleProductDal;
        
        public ServiceStaleProductManager(IServiceStaleProductDal serviceStaleProductDal)
        {
            _serviceStaleProductDal = serviceStaleProductDal;  
        }

        public void Add(ServiceStaleProduct serviceStaleProduct)
        {
            _serviceStaleProductDal.Add(serviceStaleProduct);
        }

        public void DeleteById(int id)
        {
            _serviceStaleProductDal.DeleteById(id);
        }

        public void Delete(ServiceStaleProduct serviceStaleProduct)
        {
            _serviceStaleProductDal.Delete(serviceStaleProduct);
        }
        public List<ServiceStaleProduct> GetAll()
        {
           return _serviceStaleProductDal.GetAll();
        }

        public ServiceStaleProduct GetById(int id)
        {
            return _serviceStaleProductDal.Get(d => d.Id == id);
        }

        public void Update(ServiceStaleProduct serviceStaleProduct)
        {
            _serviceStaleProductDal.Update(serviceStaleProduct);
        }
    }
}
