using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IGivenProductsToServiceService
    {
        List<GivenProductsToService> GetAll();
        List<GivenProductsToService> GetAllByDate(DateTime date);
        List<GivenProductsToServiceTotalResultDto> GetTotalQuantityByDate(DateTime date);
        void Add(GivenProductsToService KDeneme);
        void DeleteById(int id);
        void Delete(GivenProductsToService KDeneme);
        void Update(GivenProductsToService KDeneme);
        GivenProductsToService GetById(int id);
    }
}
