using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ServiceProductManager : IServiceProductService
    {


        IServiceProductDal _serviceProductDal;
        
        public ServiceProductManager(IServiceProductDal serviceProductDal)
        {
            _serviceProductDal = serviceProductDal;  
        }

        public void Add(ServiceProduct serviceProduct)
        {
            _serviceProductDal.Add(serviceProduct);
        }

        public void DeleteById(int id)
        {
            _serviceProductDal.DeleteById(id);
        }

        public void Delete(ServiceProduct serviceProduct)
        {
            _serviceProductDal.Delete(serviceProduct);
        }
        public List<ServiceProduct> GetAll()
        {
           return _serviceProductDal.GetAll();
        }

        public ServiceProduct GetById(int id)
        {
            return _serviceProductDal.Get(d => d.Id == id);
        }

        public void Update(ServiceProduct serviceProduct)
        {
            _serviceProductDal.Update(serviceProduct);
        }
    }
}
