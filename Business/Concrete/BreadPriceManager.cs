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
            return _breadPriceDal.GetAll().OrderByDescending(p => p.Date.Date).ToList();
        }

        public BreadPrice GetById(int id)
        {
            return _breadPriceDal.Get(d => d.Id == id);
        }

        public void Update(BreadPrice breadPrice)
        {
            _breadPriceDal.Update(breadPrice);
        }

        public decimal BreadPriceByDate(DateTime date)
        {

            BreadPrice searchedPrice = _breadPriceDal.Get(p => p.Date.Date == date.Date);

            if (searchedPrice != null)
            {
                return searchedPrice.Price;
            }
            else
            {
                BreadPrice? previousPrice = _breadPriceDal
                                    .GetAll(p => p.Date.Date < date.Date)
                                    .OrderByDescending(p => p.Date.Date)
                                    .FirstOrDefault();

                if (previousPrice != null)
                {
                    return previousPrice.Price;
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool IsExistByDate(DateTime date)
        {
            BreadPrice searchedPrice = _breadPriceDal.Get(p => p.Date.Date == date.Date);
            return searchedPrice != null ? true : false;
        }
    }
}
