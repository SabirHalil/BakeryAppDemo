using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ServiceListManager : IServiceListService
    {


        IServiceListDal _serviceListDal;
        
        public ServiceListManager(IServiceListDal serviceListDal)
        {
            _serviceListDal = serviceListDal;  
        }

        public int Add(ServiceList serviceList)
        {
            _serviceListDal.Add(serviceList);
            return serviceList.Id;
        }

        public void DeleteById(int id)
        {
            _serviceListDal.DeleteById(id);
        }

        public void Delete(ServiceList serviceList)
        {
            _serviceListDal.Delete(serviceList);
        }
        public List<ServiceList> GetAll()
        {
           return _serviceListDal.GetAll();
        }

        public ServiceList GetById(int id)
        {
            return _serviceListDal.Get(d => d.Id == id);
        }

        public void Update(ServiceList serviceList)
        {
            _serviceListDal.Update(serviceList);
        }

        public List<ServiceList> GetByDate(DateTime date)
        {
            return _serviceListDal.GetAll(d => d.Date.Date == date.Date);
        }
    }
}
