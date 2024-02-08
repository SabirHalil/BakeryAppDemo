using Entities.DTOs;
using static Business.Concrete.MarketEndOfDayService;

namespace Business.Abstract
{
    public interface IMarketEndOfDayService
    {
        List<PaymentMarket> CalculateMarketEndOfDay(DateTime date);
        List<MarketBreadDetails> MarketsEndOfDayCalculationWithDetail(DateTime date);
        decimal TotalAmountFromMarkets(DateTime date);
    }

}
