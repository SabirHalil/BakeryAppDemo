using Entities.DTOs;

namespace Business.Abstract
{
    public interface IMarketEndOfDayService
    {
        List<PaymentMarket> CalculateMarketEndOfDay(DateTime date);
    }

}
