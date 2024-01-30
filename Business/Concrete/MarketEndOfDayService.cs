using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class MarketEndOfDayService : IMarketEndOfDayService
    {
        private readonly IMoneyReceivedFromMarketService _moneyReceivedFromMarketService;
        private readonly IMarketService _marketService;
        private readonly IStaleBreadReceivedFromMarketService _staleBreadReceivedFromMarketService;

        private readonly IServiceListDetailService _serviceListDetailService;
        private readonly IServiceListService _serviceListService;
        private readonly IMarketContractService _marketContractService;

        
        public MarketEndOfDayService(
            IMarketContractService marketContractService, 
            IMoneyReceivedFromMarketService moneyReceivedFromMarketService,
            IMarketService marketService,
            IStaleBreadReceivedFromMarketService staleBreadReceivedFromMarketService,
            IServiceListService serviceListService, IServiceListDetailService serviceListDetailService)
        {
            _serviceListDetailService = serviceListDetailService;
            _serviceListService = serviceListService;

            _marketContractService = marketContractService;
            _moneyReceivedFromMarketService = moneyReceivedFromMarketService;
            _marketService = marketService;
            _staleBreadReceivedFromMarketService = staleBreadReceivedFromMarketService;
        }

        public List<PaymentMarket> CalculateMarketEndOfDay(DateTime date)
        {

            List<MoneyReceivedFromMarket> moneyReceivedFromMarkets = _moneyReceivedFromMarketService.GetByDate(date);
            List<PaymentMarket> paymentMarkets = new();

            for (int i = 0; i < moneyReceivedFromMarkets.Count; i++)
            {
                PaymentMarket paymentMarket = new();
                paymentMarket.MarketId = moneyReceivedFromMarkets[i].MarketId;
                paymentMarket.id = moneyReceivedFromMarkets[i].Id;
                paymentMarket.Amount = moneyReceivedFromMarkets[i].Amount;
                paymentMarket.MarketName = _marketService.GetNameById(moneyReceivedFromMarkets[i].MarketId);
                paymentMarket.TotalAmount = TotalAmout(date, moneyReceivedFromMarkets[i].MarketId);
                paymentMarket.StaleBread = _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketId(paymentMarket.MarketId, date);
                paymentMarkets.Add(paymentMarket);
            }

            return paymentMarkets;
        }



        private decimal TotalAmout(DateTime date, int marketId)
        {

            List<ServiceList> serviceLists = _serviceListService.GetByDate(date);

            int TotalBread = 0;
            decimal Price = 0;
            for (int i = 0; i < serviceLists.Count; i++)
            {

                ServiceListDetail serviceListDetail = _serviceListDetailService.GetByServiceListIdAndMarketContractId(serviceLists[i].Id, _marketContractService.GetIdByMarketId(marketId));
                if (serviceListDetail != null)
                {
                    TotalBread += serviceListDetail.Quantity;
                    Price = serviceListDetail.Price;
                }
            }

            int StaleBreadCount = _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketId(marketId, date);

            decimal TotalAmount = (TotalBread - StaleBreadCount) * Price;

            return TotalAmount;
        }
    }
}
