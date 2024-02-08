using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IGivenProductsToServiceDal : IEntityRepository<GivenProductsToService>
    {
        void DeleteById(int id);
        List<GivenProductsToServiceTotalResultDto> GetTotalQuantityResultByDate(DateTime date);

        List<GivenProductsToService> GetAllByDateAndServisTypeId(DateTime date, int servisTypeId);
    }
}
