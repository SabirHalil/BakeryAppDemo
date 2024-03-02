using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICreditCardAmountDal : IEntityRepository<CreditCardAmount>
    {
        void DeleteById(int id);
    }
}
