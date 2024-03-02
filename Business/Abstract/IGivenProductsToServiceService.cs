using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IGivenProductsToServiceService
    {
        List<GivenProductsToService> GetAll();
        List<GivenProductsToService> GetAllByDateAndServisTypeId(DateTime date, int servisTypeId);
        List<GivenProductsToServiceTotalResultDto> GetTotalQuantityByDate(DateTime date);
        void Add(GivenProductsToService delivery);
        void DeleteById(int id);
        void Delete(GivenProductsToService delivery);
        void Update(GivenProductsToService delivery);
        GivenProductsToService GetById(int id);
    }
}
