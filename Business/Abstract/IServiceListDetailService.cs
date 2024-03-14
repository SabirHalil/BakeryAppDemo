using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IServiceListDetailService
    {
        void UpdateQuantity(int entityId, int newQuantity);
        List<ServiceProductInfo> GetServiceProductInfoList(int serviceListId, int marketId);
        List<MarketAddedToServiceDto> GetMarketAddedToServiceList(int serviceListId);
        List<ProductsAddedToServiceListDetailDto> GetProductsAddedToServiceListDetail(int serviceListId, int marketId);
        List<ProductsNotAddedToServiceListDetailDto> GetProductsNotAddedToServiceListDetail(int serviceListId, int marketId);
        List<ServiceListDetail> GetAll();
        List<ServiceListDetail> GetByListId(int id);
        ServiceListDetail GetByServiceListIdAndMarketContractId(int serviceListId, int marketContracId);
        int GetIdByServiceListIdAndMarketContracId(int serviceListId, int marketContracId);
        void Add(ServiceListDetail serviceListDetail);
        void DeleteById(int id);
        void DeleteByServiceListIdAndMarketContracId(int serviceListId, int marketContracId);
        void Delete(ServiceListDetail serviceListDetail);
        void Update(ServiceListDetail serviceListDetail);
        ServiceListDetail GetById(int id);
        //List<int> GetMarketContractById(int id);
        bool IsExist(int serviceListId, int marketContractId);
        bool IsExistByServiceListId(int serviceListId);
    }
}
