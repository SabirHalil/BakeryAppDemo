using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public EndOfDayAccountController(IStaleBreadService staleBreadService, IDoughFactoryListService doughFactoryListService, IDoughFactoryListDetailService doughFactoryListDetailService, IDoughFactoryProductService doughFactoryProductService,
            IGivenProductsToServiceService givenProductsToServiceService, IBreadCountingService breadCountingService,
            IPurchasedProductListDetailService purchasedProductListDetailService, IStaleProductService staleProductService,
            IProductsCountingService productsCountingService, IProductService productService, IProductionListDetailService productionListDetailService,
            IBreadPriceService breadPriceService)
        {
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



        [HttpGet("GetProductsSoldInTheBakery")]
        public ActionResult GetProductsSoldInTheBakery(DateTime date)
        {
            try
            {
                List<Product> products = _productService.GetAllByCategoryId(2);
                List<Product> products2 = _productService.GetAllByCategoryId(1);

                products.AddRange(products2);

                List<ProductSoldInTheBakery> productsSoldInTheBakery = new();
                for (int i = 0; i < products.Count; i++)
                {
                    ProductSoldInTheBakery productSoldInTheBakery = new();
                    productSoldInTheBakery.ProductId = products[i].Id;
                    productSoldInTheBakery.ProductName = products[i].Name;
                    productSoldInTheBakery.Price = _productionListDetailService.GetProductionListDetailByDateAndProductId((date.Date), products[i].Id).Price;

                    productSoldInTheBakery.RemainingYesterday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.Date.AddDays(-1)), products[i].Id);
                    productSoldInTheBakery.RemainingToday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.Date), products[i].Id);

                    productSoldInTheBakery.StaleProductToday = _staleProductService.GetQuantityStaleBreadByDateAndProductId((date.Date), products[i].Id);

                    productSoldInTheBakery.ProductedToday = _productionListDetailService.GetProductionListDetailByDateAndProductId((date.Date), products[i].Id).Quantity;

                    productSoldInTheBakery.Revenue = productSoldInTheBakery.Price * (productSoldInTheBakery.RemainingYesterday + productSoldInTheBakery.ProductedToday - productSoldInTheBakery.RemainingToday - productSoldInTheBakery.StaleProductToday);

                    productsSoldInTheBakery.Add(productSoldInTheBakery);
                }

                return Ok(productsSoldInTheBakery);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpGet("GetPurchasedProductsSoldInTheBakery")]
        public ActionResult GetPurchasedProductsSoldInTheBakery(DateTime date)
        {
            try
            {
                List<Product> products = _productService.GetAllByCategoryId(3);
                List<PurchasedProductSoldInTheBakery> purchasedProductsSoldInTheBakery = new();
                for (int i = 0; i < products.Count; i++)
                {
                    PurchasedProductSoldInTheBakery purchasedProductSoldInTheBakery = new();
                    purchasedProductSoldInTheBakery.ProductId = products[i].Id;
                    purchasedProductSoldInTheBakery.ProductName = products[i].Name;
                    purchasedProductSoldInTheBakery.Price = _purchasedProductListDetailService.GetPurchasedProductListDetailByDateAndProductId((date.Date), products[i].Id).Price;

                    purchasedProductSoldInTheBakery.RemainingYesterday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.Date.AddDays(-1)), products[i].Id);
                    purchasedProductSoldInTheBakery.RemainingToday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.Date), products[i].Id);

                    purchasedProductSoldInTheBakery.StaleProductToday = _staleProductService.GetQuantityStaleBreadByDateAndProductId((date.Date), products[i].Id);

                    purchasedProductSoldInTheBakery.PurchasedToday = _purchasedProductListDetailService.GetPurchasedProductListDetailByDateAndProductId((date.Date), products[i].Id).Quantity;

                    purchasedProductSoldInTheBakery.Revenue = purchasedProductSoldInTheBakery.Price * (purchasedProductSoldInTheBakery.RemainingYesterday + purchasedProductSoldInTheBakery.PurchasedToday - purchasedProductSoldInTheBakery.RemainingToday - purchasedProductSoldInTheBakery.StaleProductToday);

                    purchasedProductsSoldInTheBakery.Add(purchasedProductSoldInTheBakery);
                }

                return Ok(purchasedProductsSoldInTheBakery);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpGet("GetBreadSold")]
        public ActionResult GetBreadSold(DateTime date)
        {

            try
            {
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

                BreadSold breadSold = new();
                breadSold.RemainingYesterday = _breadCountingService.GetBreadCountingByDate(date.Date.AddDays(-1)).Quantity;
                breadSold.RemainingToday = _breadCountingService.GetBreadCountingByDate(date.Date).Quantity;
                breadSold.ProductedToday = AllBreadProduced;
                breadSold.StaleProductToday = StaleBread;
                breadSold.ProductName = "Ekmek";
                breadSold.Price = 5;

                breadSold.Revenue = breadSold.Price * (breadSold.RemainingYesterday - breadSold.RemainingToday + breadSold.ProductedToday - breadSold.StaleProductToday);
                return Ok(breadSold);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }


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
