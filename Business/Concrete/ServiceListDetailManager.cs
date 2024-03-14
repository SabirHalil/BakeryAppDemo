using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;

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

        public List<MarketAddedToServiceDto> GetMarketAddedToServiceList(int serviceListId)
        {
            return _serviceListDetailDal.GetMarketAddedToServiceList(serviceListId);
        }

        public List<ProductsAddedToServiceListDetailDto> GetProductsAddedToServiceListDetail(int serviceListId, int marketId)
        {
            return _serviceListDetailDal.GetProductsAddedToServiceListDetail(serviceListId, marketId);
        }

        public List<ProductsNotAddedToServiceListDetailDto> GetProductsNotAddedToServiceListDetail(int serviceListId, int marketId)
        {
            return _serviceListDetailDal.GetProductsNotAddedToServiceListDetail(serviceListId, marketId);
        }

        public void UpdateQuantity(int entityId, int newQuantity)
        {
            _serviceListDetailDal.UpdateQuantity(entityId, newQuantity);
        }

        public bool IsExistByServiceListId(int serviceListId)
        {
            return _serviceListDetailDal.IsExistByServiceListId(serviceListId);
        }

        public List<ServiceProductInfo> GetServiceProductInfoList(int serviceListId, int marketId)
        {
            return _serviceListDetailDal.GetServiceProductInfoList(serviceListId, marketId);
        }
    }
}
