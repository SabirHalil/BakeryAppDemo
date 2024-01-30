using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBreadPriceService
    {
        List<BreadPrice> GetAll();
        void Add(BreadPrice breadPrice);
        void DeleteById(int id);
        void Delete(BreadPrice breadPrice);
        void Update(BreadPrice breadPrice);
        BreadPrice GetById(int id);
    }

}
