using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ServiceListDetailManager : IServiceListDetailService
    {


        IServiceListDetailDal _serviceListDetailDal;
        
        public ServiceListDetailManager(IServiceListDetailDal serviceListDetailDal)
        {
            _serviceListDetailDal = serviceListDetailDal;  
        }

        public void Add(ServiceListDetail serviceListDetail)
        {
            _serviceListDetailDal.Add(serviceListDetail);
        }

        public void DeleteById(int id)
        {
            _serviceListDetailDal.DeleteById(id);
        }

        public void Delete(ServiceListDetail serviceListDetail)
        {
            _serviceListDetailDal.Delete(serviceListDetail);
        }
        public List<ServiceListDetail> GetAll()
        {
           return _serviceListDetailDal.GetAll();
        }

        public ServiceListDetail GetById(int id)
        {
            return _serviceListDetailDal.Get(d => d.Id == id);
        }

        public int GetIdByServiceListIdAndMarketContracId(int serviceListId, int marketContracId)
        {
            return _serviceListDetailDal.Get(d => d.ServiceListId == serviceListId && d.MarketContractId == marketContracId).Id;
        }

        public void Update(ServiceListDetail serviceListDetail)
        {
            _serviceListDetailDal.Update(serviceListDetail);
        }

        public List<ServiceListDetail> GetByListId(int id)
        {
            return _serviceListDetailDal.GetAll(p=>p.ServiceListId ==id);
        }

        public void DeleteByServiceListIdAndMarketContracId(int serviceListId, int marketContracId)
        {
            _serviceListDetailDal.DeleteByServiceListIdAndMarketContracId(serviceListId, marketContracId);
        }

        public ServiceListDetail GetByServiceListIdAndMarketContractId(int serviceListId, int marketContracId)
        {
            return _serviceListDetailDal.Get(p => p.ServiceListId == serviceListId && p.MarketContractId == marketContracId);
        }

        public bool IsExist(int serviceListId, int marketContractId)
        {
            return _serviceListDetailDal.IsExist(serviceListId,marketContractId);
        }
    }
}
