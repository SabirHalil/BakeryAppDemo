using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class GivenProductsToServiceManager : IGivenProductsToServiceService
    {


        IGivenProductsToServiceDal _givenProductsToServiceDal;
        
        public GivenProductsToServiceManager(IGivenProductsToServiceDal givenProductsToServiceDal)
        {
            _givenProductsToServiceDal = givenProductsToServiceDal;  
        }

        public void Add(GivenProductsToService givenProductsToService)
        {
            _givenProductsToServiceDal.Add(givenProductsToService);
        }

        public void DeleteById(int id)
        {
            _givenProductsToServiceDal.DeleteById(id);
        }

        public void Delete(GivenProductsToService givenProductsToService)
        {
            _givenProductsToServiceDal.Delete(givenProductsToService);
        }
        public List<GivenProductsToService> GetAll()
        {
           return _givenProductsToServiceDal.GetAll();
        }

        public GivenProductsToService GetById(int id)
        {
            return _givenProductsToServiceDal.Get(d => d.Id == id);
        }

        public void Update(GivenProductsToService givenProductsToService)
        {
            _givenProductsToServiceDal.Update(givenProductsToService);
        }

       
        public List<GivenProductsToServiceTotalResultDto> GetTotalQuantityByDate(DateTime date)
        {
            return _givenProductsToServiceDal.GetTotalQuantityResultByDate(date);
        }

        public List<GivenProductsToService> GetAllByDateAndServisTypeId(DateTime date, int servisTypeId)
        {
            return _givenProductsToServiceDal.GetAllByDateAndServisTypeId(date,servisTypeId);     
        }
    }
}
