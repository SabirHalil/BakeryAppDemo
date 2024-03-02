using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CreditCardAmountManager : ICreditCardAmountService
    {


        ICreditCardAmountDal _creditCardAmountDal;
        
        public CreditCardAmountManager(ICreditCardAmountDal creditCardAmountDal)
        {
            _creditCardAmountDal = creditCardAmountDal;  
        }

        public void Add(CreditCardAmount creditCardAmount)
        {
            _creditCardAmountDal.Add(creditCardAmount);
        }

        public void DeleteById(int id)
        {
            _creditCardAmountDal.DeleteById(id);
        }

        public void Delete(CreditCardAmount creditCardAmount)
        {
            _creditCardAmountDal.Delete(creditCardAmount);
        }
        public List<CreditCardAmount> GetAll()
        {
           return _creditCardAmountDal.GetAll();
        }

        public CreditCardAmount GetById(int id)
        {
            return _creditCardAmountDal.Get(d => d.Id == id);
        }

        public void Update(CreditCardAmount creditCardAmount)
        {
            _creditCardAmountDal.Update(creditCardAmount);
        }
    }
}
