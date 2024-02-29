using Entities.Concrete;
using Entities.DTOs;

namespace Business.AbstractAPI
{
    public interface IDoughFactoryAPIService
    {
        List<DoughFactoryListDto> GetByDateDoughFactoryList(DateTime date);
        int AddDoughFactory(List<DoughFactoryListDetail> doughFactoryListDetail, int userId);
        List<GetAddedDoughFactoryListDetailDto> GetDoughFactoryListDetail(int doughFactoryListId);

        List<ProductNotAddedDto> GetMarketByServiceListId(int doughFactoryListId);

        void DeleteDoughFactoryListDetail(int detailId);

        void UpdateDoughFactoryListDetail(DoughFactoryListDetail doughFactoryListDetail);

    }







}
