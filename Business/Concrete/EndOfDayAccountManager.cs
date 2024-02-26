using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class EndOfDayAccountManager : IEndOfDayAccountService
    {
        private IPurchasedProductListDetailService _purchasedProductListDetailService;
        private IProductService _productService;
        private IProductionListDetailService _productionListDetailService;
        private IProductsCountingService _productsCountingService;
        private IStaleProductService _staleProductService;

        private IBreadCountingService _breadCountingService;

        private IGivenProductsToServiceService _givenProductsToServiceService;

        private IDoughFactoryListService _doughFactoryListService;
        private IDoughFactoryListDetailService _doughFactoryListDetailService;
        private IDoughFactoryProductService _doughFactoryProductService;

        private IBreadPriceService _breadPriceService;

        private IStaleBreadService _staleBreadService;

        private IMarketEndOfDayService _marketEndOfDayService;
        private IMoneyReceivedFromMarketService _moneyReceivedFromMarketService;

        private IServiceStaleProductService _serviceStaleProductService;

        private ICashCountingService _cashCountingService;
        public EndOfDayAccountManager(IMarketEndOfDayService marketEndOfDayService, IStaleBreadService staleBreadService, IDoughFactoryListService doughFactoryListService, IDoughFactoryListDetailService doughFactoryListDetailService, IDoughFactoryProductService doughFactoryProductService,
            IGivenProductsToServiceService givenProductsToServiceService, IBreadCountingService breadCountingService,
            IPurchasedProductListDetailService purchasedProductListDetailService, IStaleProductService staleProductService,
            IProductsCountingService productsCountingService, IProductService productService, IProductionListDetailService productionListDetailService,
            IBreadPriceService breadPriceService,
            IMoneyReceivedFromMarketService moneyReceivedFromMarketService,
            IServiceStaleProductService serviceStaleProductService,
            ICashCountingService cashCountingService)
        {
            _serviceStaleProductService = serviceStaleProductService;
            _cashCountingService = cashCountingService;

            _moneyReceivedFromMarketService = moneyReceivedFromMarketService;

            _marketEndOfDayService = marketEndOfDayService;
            _purchasedProductListDetailService = purchasedProductListDetailService;
            _productService = productService;
            _productionListDetailService = productionListDetailService;
            _productsCountingService = productsCountingService;
            _staleProductService = staleProductService;
            _breadCountingService = breadCountingService;
            _givenProductsToServiceService = givenProductsToServiceService;

            _doughFactoryProductService = doughFactoryProductService;
            _doughFactoryListService = doughFactoryListService;
            _doughFactoryListDetailService = doughFactoryListDetailService;

            _staleBreadService = staleBreadService;
            _breadPriceService = breadPriceService;

        }

        
        public EndOfDayResult GetEndOfDayAccountDetail(DateTime date)
        {
                Account account = new();
                account.ServisGelir = _marketEndOfDayService.TotalAmountFromMarkets(date);
                account.KasaDevir = _cashCountingService.GetOneCashCountingByDate(date.AddDays(-1))?.RemainedMoney ?? 0;
                account.Devir = _cashCountingService.GetOneCashCountingByDate(date)?.RemainedMoney ?? 0;
                account.KasaSayım = _cashCountingService.GetOneCashCountingByDate(date)?.TotalMoney ?? 0;

                //-------------------------------------

                double AllBreadProduced = 0;
                List<DoughFactoryListDto> doughFactoryListDto = _doughFactoryListService.GetByDate(date.Date);

                for (int i = 0; i < doughFactoryListDto.Count; i++)
                {

                    List<DoughFactoryListDetail> doughFactoryListDetails = _doughFactoryListDetailService.GetByDoughFactoryList(doughFactoryListDto[i].Id);

                    for (int j = 0; j < doughFactoryListDetails.Count; j++)
                    {
                        DoughFactoryProduct doughFactoryProduct = _doughFactoryProductService.GetById(doughFactoryListDetails[j].DoughFactoryProductId);
                        AllBreadProduced += doughFactoryProduct.BreadEquivalent * doughFactoryListDetails[j].Quantity;
                    }
                }

                List<StaleBreadDto> staleBreadDtos = _staleBreadService.GetAllByDate(date);
                double StaleBread = 0;

                for (int i = 0; i < staleBreadDtos.Count; i++)
                {
                    DoughFactoryProduct doughFactoryProduct = _doughFactoryProductService.GetById(staleBreadDtos[i].DoughFactoryProductId);
                    StaleBread += doughFactoryProduct.BreadEquivalent * staleBreadDtos[i].Quantity;
                }

                //--------------------------------------------------------------

                EndOfDayAccountForBread endOfDayAccountForBread = new();

                endOfDayAccountForBread.Price = _breadPriceService.BreadPriceByDate(date);
                endOfDayAccountForBread.ProductedToday = AllBreadProduced;
                endOfDayAccountForBread.RemainingYesterday = _breadCountingService.GetBreadCountingByDate(date.Date.AddDays(-1))?.Quantity ?? 0;
                endOfDayAccountForBread.RemainingToday = _breadCountingService.GetBreadCountingByDate(date.Date)?.Quantity ?? 0;
                endOfDayAccountForBread.StaleBreadToday = StaleBread;

                // Değerler değişebilir
                endOfDayAccountForBread.PurchasedBread = 0;
                endOfDayAccountForBread.EatenBread = 10;
                //

                List<GivenProductsToServiceTotalResultDto> GivenProductsToServiceTotal = _givenProductsToServiceService.GetTotalQuantityByDate(date);
                endOfDayAccountForBread.TotalBreadGivenToGetir = GivenProductsToServiceTotal
                    .FirstOrDefault(item => item.ServiceTypeName == "Getir")?.TotalQuantity ?? 0;

                endOfDayAccountForBread.TotalBreadGivenToService = GivenProductsToServiceTotal
                    .FirstOrDefault(item => item.ServiceTypeName == "Marketler")?.TotalQuantity ?? 0;

                endOfDayAccountForBread.TotalStaleBreadFromService = 0;
                List<ServiceStaleProduct> serviceStaleProduct = _serviceStaleProductService.GetAllByDate(date, 1);
                foreach (var service in serviceStaleProduct)
                {
                    endOfDayAccountForBread.TotalStaleBreadFromService += service.Quantity;
                }


                EndOfDayResult result = new EndOfDayResult
                {
                    EndOfDayAccount = endOfDayAccountForBread,
                    Account = account
                };

               return result;
         
        }



        
        public decimal GetProductsSoldInTheBakery(DateTime date)
        {
            
                decimal TotalRevenue = 0;
                List<Product> products = _productService.GetAllByCategoryId(2);
                List<Product> products2 = _productService.GetAllByCategoryId(1);
                List<Product> products3 = _productService.GetAllByCategoryId(3);

                products.AddRange(products2);
                products.AddRange(products3);

                // List<ProductSoldInTheBakery> productsSoldInTheBakery = new();

                for (int i = 0; i < products.Count; i++)
                {
                    ProductSoldInTheBakery productSoldInTheBakery = new();
                    productSoldInTheBakery.ProductId = products[i].Id;
                    productSoldInTheBakery.ProductName = products[i].Name;

                    ProductionListDetail productionListDetail = _productionListDetailService.GetProductionListDetailByDateAndProductId((date), products[i]);

                    productSoldInTheBakery.ProductedToday = productionListDetail.Quantity;
                    productSoldInTheBakery.Price = productionListDetail.Price;


                    for (int j = 1; j < 6 && productSoldInTheBakery.Price == 0; j++)
                    {
                        productSoldInTheBakery.Price = _productionListDetailService.GetProductionListDetailByDateAndProductId((date.Date.AddDays(-j)), products[i]).Price;
                    }

                    productSoldInTheBakery.RemainingYesterday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.Date.AddDays(-1)), products[i].Id);
                    productSoldInTheBakery.RemainingToday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.Date), products[i].Id);

                    productSoldInTheBakery.StaleProductToday = _staleProductService.GetQuantityStaleProductByDateAndProductId((date.Date), products[i].Id);

                    productSoldInTheBakery.Revenue = productSoldInTheBakery.Price * (productSoldInTheBakery.RemainingYesterday + productSoldInTheBakery.ProductedToday - productSoldInTheBakery.RemainingToday - productSoldInTheBakery.StaleProductToday);

                    TotalRevenue += productSoldInTheBakery.Revenue;

                    //productsSoldInTheBakery.Add(productSoldInTheBakery);

                }



                // return Ok(productsSoldInTheBakery);
                return TotalRevenue;
           

        }

    }
}
