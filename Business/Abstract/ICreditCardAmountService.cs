using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICreditCardAmountService
    {
        List<CreditCardAmount> GetAll();
        void Add(CreditCardAmount creditCardAmount);
        void DeleteById(int id);
        void Delete(CreditCardAmount creditCardAmount);
        void Update(CreditCardAmount creditCardAmount);
        CreditCardAmount GetById(int id);
    }
}
