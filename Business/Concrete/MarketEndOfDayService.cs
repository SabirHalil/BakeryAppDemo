using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using static Business.Concrete.MarketEndOfDayService;

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

                var result = CalculateTotalAmountAndBread(date, moneyReceivedFromMarkets[i].MarketId);

                paymentMarket.TotalAmount = result.TotalAmount;
                paymentMarket.GivenBread = result.TotalBread;
                paymentMarket.StaleBread = _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketId(paymentMarket.MarketId, date);
                paymentMarkets.Add(paymentMarket);
            }

            return paymentMarkets;
        }

        public List<MarketBreadDetails> MarketsEndOfDayCalculationWithDetail(DateTime date)
        {

            List<MoneyReceivedFromMarket> moneyReceivedFromMarkets = _moneyReceivedFromMarketService.GetByDate(date);

            List<MarketBreadDetails> marketsBreadDetails = new();

            for (int i = 0; i < moneyReceivedFromMarkets.Count; i++)
            {
                MarketBreadDetails marketBreadDetails = new();
                marketBreadDetails.MarketId = moneyReceivedFromMarkets[i].MarketId;
                marketBreadDetails.id = moneyReceivedFromMarkets[i].Id;
                marketBreadDetails.Amount = moneyReceivedFromMarkets[i].Amount;
                marketBreadDetails.MarketName = _marketService.GetNameById(moneyReceivedFromMarkets[i].MarketId);

                var result = CalculateTotalAmountAndBread(date, moneyReceivedFromMarkets[i].MarketId);

                marketBreadDetails.TotalAmount = result.TotalAmount;
                marketBreadDetails.GivenBread = result.TotalBread;
                marketBreadDetails.StaleBread = _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketId(marketBreadDetails.MarketId, date);

                marketBreadDetails.BreadGivenByEachService = BreadGivenByEachService(date, moneyReceivedFromMarkets[i].MarketId);
                marketsBreadDetails.Add(marketBreadDetails);
            }

            return marketsBreadDetails;
        }

        private Dictionary<string, int> BreadGivenByEachService(DateTime date, int marketId)
        {
            Dictionary<string, int> breadGivenByEachService = new();
            List<ServiceList> serviceLists = _serviceListService.GetByDate(date);
            for (int i = 0; i < serviceLists.Count; i++)
            {
                ServiceListDetail serviceListDetail = _serviceListDetailService.GetByServiceListIdAndMarketContractId(serviceLists[i].Id, _marketContractService.GetIdByMarketId(marketId));
                string KeyName = $"{i + 1}. Servis";
                breadGivenByEachService[KeyName] = serviceListDetail != null ? serviceListDetail.Quantity : 0;
            }
            return breadGivenByEachService;
        }
        private (decimal TotalAmount, int TotalBread) CalculateTotalAmountAndBread(DateTime date, int marketId)
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


            return (TotalAmount, TotalBread);
        }

        public class MarketBreadDetails
        {
            public int id { get; set; }
            public decimal Amount { get; set; }
            public int MarketId { get; set; }
            public string? MarketName { get; set; }
            public decimal TotalAmount { get; set; }
            public int StaleBread { get; set; }
            public int GivenBread { get; set; }
            public Dictionary<string, int>? BreadGivenByEachService { get; set; }
        }
    }
}
