using Entities.DTOs;

namespace Business.Abstract
{
    public interface IEndOfDayAccountService
    {

        public EndOfDayResult GetEndOfDayAccountDetail(DateTime date);

        public decimal GetProductsSoldInTheBakery(DateTime date);

    }

}
