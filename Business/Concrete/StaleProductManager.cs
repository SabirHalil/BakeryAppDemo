using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class StaleProductManager : IStaleProductService
    {


        IStaleProductDal _staleProductDal;

        public StaleProductManager(IStaleProductDal staleProductDal)
        {
            _staleProductDal = staleProductDal;
        }

        public void Add(StaleProduct staleProduct)
        {
            _staleProductDal.Add(staleProduct);
        }

        public void DeleteById(int id)
        {
            _staleProductDal.DeleteById(id);
        }

        public void Delete(StaleProduct staleProduct)
        {
            _staleProductDal.Delete(staleProduct);
        }
        public List<StaleProduct> GetAll()
        {
            return _staleProductDal.GetAll();
        }

        public StaleProduct GetById(int id)
        {
            return _staleProductDal.Get(d => d.Id == id);
        }

        public void Update(StaleProduct staleProduct)
        {
            _staleProductDal.Update(staleProduct);
        }

        public List<StaleProductDto> GetByDateAndCategory(DateTime date, int categoryId)
        {
            return _staleProductDal.GetByDateAndCategory(date, categoryId);
        }

        public List<Product> GetProductsNotAddedToStale(DateTime date, int categoryId)
        {
            return _staleProductDal.GetProductsNotAddedToStale(date, categoryId);
        }
    }
}
