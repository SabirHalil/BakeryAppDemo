using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceListDetailService
    {
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
        bool IsExist(int id);
    }
}
