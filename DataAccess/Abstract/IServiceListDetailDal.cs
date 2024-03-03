using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IServiceListDetailDal : IEntityRepository<ServiceListDetail>
    {
        void DeleteById(int id);
        bool IsExist(int serviceListId, int marketContractId);
        bool IsExistByServiceListId(int serviceListId);

        void DeleteByServiceListIdAndMarketContracId(int serviceListId, int marketContracId);

        void UpdateQuantity(int entityId, int newQuantity);
        List<MarketAddedToServiceDto> GetMarketAddedToServiceList(int serviceListId);

        List<ProductsAddedToServiceListDetailDto> GetProductsAddedToServiceListDetail(int serviceListId, int marketId);

        List<ProductsNotAddedToServiceListDetailDto> GetProductsNotAddedToServiceListDetail(int serviceListId, int marketId);

    }
}
