using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BreadPriceManager : IBreadPriceService
    {


        IBreadPriceDal _breadPriceDal;
        
        public BreadPriceManager(IBreadPriceDal breadPriceDal)
        {
            _breadPriceDal = breadPriceDal;  
        }

        public void Add(BreadPrice breadPrice)
        {
            _breadPriceDal.Add(breadPrice);
        }

        public void DeleteById(int id)
        {
            _breadPriceDal.DeleteById(id);
        }

        public void Delete(BreadPrice breadPrice)
        {
            _breadPriceDal.Delete(breadPrice);
        }
        public List<BreadPrice> GetAll()
        {
           return _breadPriceDal.GetAll();
        }

        public BreadPrice GetById(int id)
        {
            return _breadPriceDal.Get(d => d.Id == id);
        }

        public void Update(BreadPrice breadPrice)
        {
            _breadPriceDal.Update(breadPrice);
        }
    }
}
