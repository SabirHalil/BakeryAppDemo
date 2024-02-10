using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Security.Principal;
using System.Threading;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndOfDayAccountController : ControllerBase
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
        public EndOfDayAccountController(IMarketEndOfDayService marketEndOfDayService, IStaleBreadService staleBreadService, IDoughFactoryListService doughFactoryListService, IDoughFactoryListDetailService doughFactoryListDetailService, IDoughFactoryProductService doughFactoryProductService,
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

        [HttpGet("GetEndOfDayAccountDetail")]
        public ActionResult GetEndOfDayAccountDetail(DateTime date)
        {
            try
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

                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        //[HttpGet("GetServiceDetail")]
        //public ActionResult GetServiceDetail(DateTime date)
        //{
        //    try
        //    {
        //        var result = new
        //        {
        //            PaymentMarkets = _marketEndOfDayService.CalculateMarketEndOfDay(date),
        //            MoneyReceivedFromMarkets = _moneyReceivedFromMarketService.GetByDate(date)
        //        };
        //        //return Ok(_marketEndOfDayService.CalculateMarketEndOfDay(date), _moneyReceivedFromMarketService.GetByDate(date));
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, e.Message);
        //    }
        //}

        [HttpGet("GetProductsSoldInTheBakery")]
        public ActionResult GetProductsSoldInTheBakery(DateTime date)
        {
            try
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
                return Ok(TotalRevenue);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        //[HttpGet("GetPurchasedProductsSoldInTheBakery")]
        //public ActionResult GetPurchasedProductsSoldInTheBakery(DateTime date)
        //{
        //    try
        //    {
        //        decimal TotalRevenue = 0;

        //        List<Product> products = _productService.GetAllByCategoryId(3);
        //        //List<PurchasedProductSoldInTheBakery> purchasedProductsSoldInTheBakery = new();
        //        for (int i = 0; i < products.Count; i++)
        //        {
        //            PurchasedProductSoldInTheBakery purchasedProductSoldInTheBakery = new();
        //            purchasedProductSoldInTheBakery.ProductId = products[i].Id;
        //            purchasedProductSoldInTheBakery.ProductName = products[i].Name;


        //            purchasedProductSoldInTheBakery.Price = _purchasedProductListDetailService.GetPurchasedProductListDetailByDateAndProductId((date.Date), products[i].Id).Price;


        //            for (int j = 1; j < 6 && purchasedProductSoldInTheBakery.Price == 0; j++)
        //            {
        //                purchasedProductSoldInTheBakery.Price = _purchasedProductListDetailService.GetPurchasedProductListDetailByDateAndProductId((date.Date.AddDays(-j)), products[i].Id).Price;
        //            }

        //            purchasedProductSoldInTheBakery.RemainingYesterday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.Date.AddDays(-1)), products[i].Id);
        //            purchasedProductSoldInTheBakery.RemainingToday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.Date), products[i].Id);

        //            purchasedProductSoldInTheBakery.StaleProductToday = _staleProductService.GetQuantityStaleProductByDateAndProductId((date.Date), products[i].Id);

        //            purchasedProductSoldInTheBakery.PurchasedToday = _purchasedProductListDetailService.GetPurchasedProductListDetailByDateAndProductId((date.Date), products[i].Id)?.Quantity ?? 0;

        //            purchasedProductSoldInTheBakery.Revenue = purchasedProductSoldInTheBakery.Price * (purchasedProductSoldInTheBakery.RemainingYesterday + purchasedProductSoldInTheBakery.PurchasedToday - purchasedProductSoldInTheBakery.RemainingToday - purchasedProductSoldInTheBakery.StaleProductToday);

        //            TotalRevenue += purchasedProductSoldInTheBakery.Revenue;

        //           // purchasedProductsSoldInTheBakery.Add(purchasedProductSoldInTheBakery);
        //        }

        //        //return Ok(purchasedProductsSoldInTheBakery);
        //        return Ok(TotalRevenue);
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, "Daha sonra tekrar deneyin...");
        //    }

        //}

        //[HttpGet("GetBreadSold")]
        //public ActionResult GetBreadSold(DateTime date)
        //{

        //    try
        //    {
        //        double AllBreadProduced = 0;


        //        List<DoughFactoryListDto> doughFactoryListDto = _doughFactoryListService.GetByDate(date.Date);

        //        for (int i = 0; i < doughFactoryListDto.Count; i++)
        //        {

        //            List<DoughFactoryListDetail> doughFactoryListDetails = _doughFactoryListDetailService.GetByDoughFactoryList(doughFactoryListDto[i].Id);

        //            for (int j = 0; j < doughFactoryListDetails.Count; j++)
        //            {
        //                DoughFactoryProduct doughFactoryProduct = _doughFactoryProductService.GetById(doughFactoryListDetails[j].DoughFactoryProductId);
        //                AllBreadProduced += doughFactoryProduct.BreadEquivalent * doughFactoryListDetails[j].Quantity;
        //            }
        //        }

        //        List<StaleBreadDto> staleBreadDtos = _staleBreadService.GetAllByDate(date);
        //        double StaleBread = 0;

        //        for (int i = 0; i < staleBreadDtos.Count; i++)
        //        {
        //            DoughFactoryProduct doughFactoryProduct = _doughFactoryProductService.GetById(staleBreadDtos[i].DoughFactoryProductId);
        //            StaleBread += doughFactoryProduct.BreadEquivalent * staleBreadDtos[i].Quantity;
        //        }

        //        BreadSold breadSold = new();
        //        breadSold.RemainingYesterday = _breadCountingService.GetBreadCountingByDate(date.Date.AddDays(-1))?.Quantity ?? 0;
        //        breadSold.RemainingToday = _breadCountingService.GetBreadCountingByDate(date.Date)?.Quantity ?? 0;
        //        breadSold.ProductedToday = AllBreadProduced;
        //        breadSold.StaleProductToday = StaleBread;
        //        breadSold.ProductName = "Ekmek";
        //        breadSold.Price = 5;

        //        breadSold.Revenue = breadSold.Price * (breadSold.RemainingYesterday - breadSold.RemainingToday + breadSold.ProductedToday - breadSold.StaleProductToday);
        //        return Ok(breadSold);
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, "Daha sonra tekrar deneyin...");
        //    }


        //}




        private class EndOfDayAccountForBread
        {
            public decimal Price { get; set; }
            public double ProductedToday { get; set; }
            public int RemainingYesterday { get; set; }
            public int RemainingToday { get; set; }
            public double StaleBreadToday { get; set; }
            public int TotalBreadGivenToService { get; set; }
            public int TotalBreadGivenToGetir { get; set; }
            public int PurchasedBread { get; set; }
            public int TotalStaleBreadFromService { get; set; }
            public int EatenBread { get; set; }

        }

        private class Account
        {
            public decimal ServisGelir { get; set; }
            public decimal KasaDevir { get; set; }
            public decimal Devir { get; set; }
            public decimal KasaSayım { get; set; }
          
        }
        private class EndOfDayResult
        {
            public EndOfDayAccountForBread EndOfDayAccount { get; set; }
            public Account Account { get; set; }
            public decimal PastaneRevenue { get; set; }
        }

        private class ProductSoldInTheBakery
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public decimal Revenue { get; set; }
            public int RemainingYesterday { get; set; }
            public int ProductedToday { get; set; }
            public int RemainingToday { get; set; }
            public int StaleProductToday { get; set; }

        }
        private class PurchasedProductSoldInTheBakery
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public decimal Revenue { get; set; }
            public int RemainingYesterday { get; set; }
            public int PurchasedToday { get; set; }
            public int RemainingToday { get; set; }
            public int StaleProductToday { get; set; }

        }
        private class BreadSold
        {
            //public int ProductId { get; set; }
            public string ProductName { get; set; }
            //public decimal Price { get; set; }
            public double Price { get; set; }
            public double Revenue { get; set; }
            //  public decimal Revenue { get; set; }
            public int RemainingYesterday { get; set; }
            public double ProductedToday { get; set; }
            public int RemainingToday { get; set; }
            public double StaleProductToday { get; set; }
            //  public int GivenBreadsToServiceTotal { get; set; }


        }
    }
}
