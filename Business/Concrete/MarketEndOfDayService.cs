using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using static Business.Concrete.MarketEndOfDayService;

namespace Business.Concrete
{
    public class MarketEndOfDayService : IMarketEndOfDayService
    {
        private readonly IMoneyReceivedFromMarketService _moneyReceivedFromMarketService;
        private readonly IMarketService _marketService;

        private readonly IStaleProductsReceivedFromMarketService _staleProductsReceivedFromMarketService;

        private readonly IServiceListDetailService _serviceListDetailService;
        private readonly IServiceListService _serviceListService;
        private readonly IMarketContractService _marketContractService;


        public MarketEndOfDayService(
            IStaleProductsReceivedFromMarketService staleProductsReceivedFromMarketService,
            IMarketContractService marketContractService,
            IMoneyReceivedFromMarketService moneyReceivedFromMarketService,
            IMarketService marketService,
            IServiceListService serviceListService, IServiceListDetailService serviceListDetailService)
        {
            _staleProductsReceivedFromMarketService = staleProductsReceivedFromMarketService;
            _serviceListDetailService = serviceListDetailService;
            _serviceListService = serviceListService;

            _marketContractService = marketContractService;
            _moneyReceivedFromMarketService = moneyReceivedFromMarketService;
            _marketService = marketService;
        }


        public (List<StaleProductsReceivedFromMarket> staleProductsReceivedFromMarkets, Dictionary<int, List<ServiceProductInfo>> serviceDetail)
            MarketEndOfDayDetail(DateTime date, int marketId)
        {

            List<StaleProductsReceivedFromMarket> staleProductsReceivedFromMarkets = _staleProductsReceivedFromMarketService.GetByDateAndMarketId(marketId, date);


            List<ServiceList> serviceLists = _serviceListService.GetByDate(date).OrderBy(service => service.Date).ToList();

            Dictionary<int, List<ServiceProductInfo>> serviceDetail = new();

            if (serviceLists != null)
            {
                for (int i = 0; i < serviceLists.Count; i++)
                {
                    List<ServiceProductInfo> GetServiceProductInfoList = _serviceListDetailService.GetServiceProductInfoList(serviceLists[i].Id, marketId);
                    if (GetServiceProductInfoList != null)
                    {
                        serviceDetail.Add(i + 1, GetServiceProductInfoList);
                    }
                }
            }

            return (staleProductsReceivedFromMarkets, serviceDetail);
        }


        public decimal MarketEndOfDayAccount(DateTime date, int marketId)
        {
            decimal TotalAmount = 0;

            var result = MarketEndOfDayDetail(date, marketId);

            var ServiceProducts = _marketContractService.ServiceProductsIdsByMarketId(marketId);

            for (int i = 0; i < ServiceProducts.Count; i++)
            {
                int total = 0;
                decimal price = 0;

                foreach (var serviceDetail in result.serviceDetail)
                {
                    var serviceProductInfo = serviceDetail.Value.FirstOrDefault(s => s.ServiceProductId == ServiceProducts[i]);

                    total += serviceProductInfo == null ? 0 : serviceProductInfo.Quantity;

                    if (price <= 0)
                    {
                        price = serviceProductInfo == null ? 0 : serviceProductInfo.Price;
                    }
                }


                var stale = result.staleProductsReceivedFromMarkets.FirstOrDefault(s => s.ServiceProductId == ServiceProducts[i]);
                if (stale != null)
                {
                    total -= stale.Quantity;
                }

                TotalAmount += (total * price);
            }
            return TotalAmount;

        }
        public List<ServiceProductForMarketEndOfDayDetail> MarketEndOfDayAccountDetail(DateTime date, int marketId)
        {
            List< ServiceProductForMarketEndOfDayDetail > serviceProductsDetail = new List<ServiceProductForMarketEndOfDayDetail> ();

            decimal TotalAmount = 0;

            var result = MarketEndOfDayDetail(date, marketId);

            var ServiceProducts = _marketContractService.ServiceProductsIdsByMarketId(marketId);

            for (int i = 0; i < ServiceProducts.Count; i++)
            {
                ServiceProductForMarketEndOfDayDetail serviceProductDetail = new();

                int total = 0;
                decimal price = 0;
                string serviceProductName = "";

                foreach (var serviceDetail in result.serviceDetail)
                {
                    var serviceProductInfo = serviceDetail.Value.FirstOrDefault(s => s.ServiceProductId == ServiceProducts[i]);

                    total += serviceProductInfo == null ? 0 : serviceProductInfo.Quantity;

                    if(serviceProductInfo != null)
                    {
                        serviceProductName = serviceProductInfo.ServiceProductName;
                    }
                   
                    if (price <= 0)
                    {
                        price = serviceProductInfo == null ? 0 : serviceProductInfo.Price;
                    }
                }

                serviceProductDetail.GivenProduct = total;
                serviceProductDetail.Price = price;

                var stale = result.staleProductsReceivedFromMarkets.FirstOrDefault(s => s.ServiceProductId == ServiceProducts[i]);

                serviceProductDetail.StaleProduct = stale ==null ? 0 : stale.Quantity;

                serviceProductDetail.MarketId = marketId;   
                serviceProductDetail.ServiceProductName = serviceProductName;   
                serviceProductsDetail.Add(serviceProductDetail);
                //if (stale != null)
                //{
                //    //total -= stale.Quantity;
                //}

                //TotalAmount += (total * price);
            }

            return serviceProductsDetail;

        }

        public List<PaymentMarket> CalculateMarketEndOfDay(DateTime date, int serviceProductId)
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

                var result = CalculateTotalAmountAndProduct(date, moneyReceivedFromMarkets[i].MarketId, serviceProductId);

                paymentMarket.TotalAmount = result.TotalAmount;
                paymentMarket.GivenBread = result.TotalProduct;
                paymentMarket.StaleBread = result.StaleProductQuantity;
                paymentMarkets.Add(paymentMarket);
            }

            return paymentMarkets;
        }

        public List<MarketProductDetails> MarketsEndOfDayCalculationWithDetail(DateTime date, int serviceProductId)
        {

            List<MoneyReceivedFromMarket> moneyReceivedFromMarkets = _moneyReceivedFromMarketService.GetByDate(date);

            List<MarketProductDetails> marketProductDetails = new();

            for (int i = 0; i < moneyReceivedFromMarkets.Count; i++)
            {
                MarketProductDetails marketProductDetail = new();

                marketProductDetail.ProductGivenByEachService = ProductGivenByEachService(date, moneyReceivedFromMarkets[i].MarketId, serviceProductId);

                if (!marketProductDetail.ProductGivenByEachService.Values.All(value => value == 0))
                {
                    marketProductDetail.MarketId = moneyReceivedFromMarkets[i].MarketId;
                    marketProductDetail.id = moneyReceivedFromMarkets[i].Id;
                    marketProductDetail.Amount = moneyReceivedFromMarkets[i].Amount;
                    marketProductDetail.MarketName = _marketService.GetNameById(moneyReceivedFromMarkets[i].MarketId);

                    var result = CalculateTotalAmountAndProduct(date, moneyReceivedFromMarkets[i].MarketId, serviceProductId);

                    marketProductDetail.TotalAmount = result.TotalAmount;
                    marketProductDetail.GivenProduct = result.TotalProduct;
                    marketProductDetail.StaleProduct = result.StaleProductQuantity;

                    marketProductDetails.Add(marketProductDetail);
                }

            }

            return marketProductDetails;
        }

        public decimal TotalAmountFromMarkets(DateTime date)
        {
            List<MoneyReceivedFromMarket> moneyReceivedFromMarkets = _moneyReceivedFromMarketService.GetByDate(date);
            decimal TotalAmount = 0;

            for (int i = 0; i < moneyReceivedFromMarkets.Count; i++)
            {
                //var result = CalculateTotalAmountAndProduct(date, moneyReceivedFromMarkets[i].MarketId ,serviceProductId);
                //TotalAmount += result.TotalAmount;
            }
            return TotalAmount;
        }      
        private Dictionary<string, int> ProductGivenByEachService(DateTime date, int marketId, int serviceProductId)
        {
            Dictionary<string, int> productGivenByEachService = new();
            List<ServiceList> serviceLists = _serviceListService.GetByDate(date);
            for (int i = 0; i < serviceLists.Count; i++)
            {
                ServiceListDetail serviceListDetail = _serviceListDetailService.GetByServiceListIdAndMarketContractId(serviceLists[i].Id, _marketContractService.GetIdByMarketIdAndServiceProductId(marketId, serviceProductId));
                string KeyName = $"{i + 1}. Servis";
                productGivenByEachService[KeyName] = serviceListDetail != null ? serviceListDetail.Quantity : 0;
            }
            return productGivenByEachService;
        }

        private (decimal TotalAmount, int TotalProduct, int StaleProductQuantity)
            CalculateTotalAmountAndProduct(DateTime date, int marketId, int serviceProductId)
        {

            List<ServiceList> serviceLists = _serviceListService.GetByDate(date);

            int TotalProduct = 0;
            decimal Price = 0;
            for (int i = 0; i < serviceLists.Count; i++)
            {

                ServiceListDetail serviceListDetail = _serviceListDetailService.GetByServiceListIdAndMarketContractId(serviceLists[i].Id, _marketContractService.GetIdByMarketIdAndServiceProductId(marketId, serviceProductId));
                if (serviceListDetail != null)
                {
                    TotalProduct += serviceListDetail.Quantity;
                    Price = serviceListDetail.Price;
                }
            }

            var staleProductCount = _staleProductsReceivedFromMarketService.GetByDateAndMarketIdAndServiceProductId(serviceProductId, marketId, date);

            decimal TotalAmount = (TotalProduct - staleProductCount) * Price;


            return (TotalAmount, TotalProduct, staleProductCount);
        }

        public class ServiceProductForMarketEndOfDayDetail
        {
            public string ServiceProductName { get; set; }
            public int MarketId { get; set; }                      
            public int StaleProduct { get; set; }
            public int GivenProduct { get; set; }
            public decimal Price { get; set; }
        }

        public class MarketProductDetails
        {
            public int id { get; set; }
            public decimal Amount { get; set; }
            public int MarketId { get; set; }
            public string? MarketName { get; set; }
            public decimal TotalAmount { get; set; }
            public int StaleProduct { get; set; }
            public int GivenProduct { get; set; }
            public Dictionary<string, int>? ProductGivenByEachService { get; set; }
        }

    }
}
