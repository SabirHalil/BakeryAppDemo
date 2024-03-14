using Entities.Concrete;
using Entities.DTOs;
using static Business.Concrete.MarketEndOfDayService;

namespace Business.Abstract
{
    public interface IMarketEndOfDayService
    {
        List<PaymentMarket> CalculateMarketEndOfDay(DateTime date, int serviceProductId);
        List<MarketProductDetails> MarketsEndOfDayCalculationWithDetail(DateTime date , int serviceProductId);

        decimal MarketEndOfDayAccount(DateTime date, int marketId);

        List<ServiceProductForMarketEndOfDayDetail> MarketEndOfDayAccountDetail(DateTime date, int marketId);


        //List<PaymentMarket> CalculateMarketEndOfDay(DateTime date);
        //List<MarketBreadDetails> MarketsEndOfDayCalculationWithDetail(DateTime date);

        (List<StaleProductsReceivedFromMarket> staleProductsReceivedFromMarkets, Dictionary<int, List<ServiceProductInfo>> serviceDetail)
            MarketEndOfDayDetail(DateTime date, int marketId);
        decimal TotalAmountFromMarkets(DateTime date);
       // decimal TotalAmountFromMarkets(DateTime date, int serviceProductId);
    }

}
