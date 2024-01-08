using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public EndOfDayAccountController(IGivenProductsToServiceService givenProductsToServiceService, IBreadCountingService breadCountingService,IPurchasedProductListDetailService purchasedProductListDetailService, IStaleProductService staleProductService, IProductsCountingService productsCountingService, IProductService productService, IProductionListDetailService productionListDetailService)
        {
            _purchasedProductListDetailService = purchasedProductListDetailService;
            _productService = productService;
            _productionListDetailService = productionListDetailService;
            _productsCountingService = productsCountingService;
            _staleProductService = staleProductService;
            _breadCountingService = breadCountingService;
            _givenProductsToServiceService = givenProductsToServiceService;

        }



        [HttpGet("GetProductsSoldInTheBakery")]
        public ActionResult GetProductsSoldInTheBakery(DateTime date)
        {
            List<Product> products = _productService.GetAllByCategoryId(2);
            List < ProductSoldInTheBakery> productsSoldInTheBakery = new();
            for (int i = 0; i < products.Count; i++)
            {
                ProductSoldInTheBakery productSoldInTheBakery = new();
                productSoldInTheBakery.ProductId = products[i].Id;
                productSoldInTheBakery.ProductName = products[i].Name;
                productSoldInTheBakery.Price = _productionListDetailService.GetProductionListDetailByDateAndProductId((date.Date), products[i].Id).Price;

                productSoldInTheBakery.RemainingYesterday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.Date.AddDays(-1)), products[i].Id);
                productSoldInTheBakery.RemainingToday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.Date), products[i].Id);

                productSoldInTheBakery.StaleProductToday = _staleProductService.GetQuantityStaleBreadByDateAndProductId((date.Date), products[i].Id);

                productSoldInTheBakery.ProducedToday = _productionListDetailService.GetProductionListDetailByDateAndProductId((date.Date), products[i].Id).Quantity;

                productSoldInTheBakery.Revenue = productSoldInTheBakery.Price * (productSoldInTheBakery.RemainingYesterday + productSoldInTheBakery.ProducedToday - productSoldInTheBakery.RemainingToday - productSoldInTheBakery.StaleProductToday);

                productsSoldInTheBakery.Add(productSoldInTheBakery);
            }

            return Ok(productsSoldInTheBakery);
        }

        [HttpGet("GetPurchasedProductsSoldInTheBakery")]
        public ActionResult GetPurchasedProductsSoldInTheBakery(DateTime date)
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

        [HttpGet("GetBreadSold")]
        public ActionResult GetBreadSold(DateTime date)
        {
            List<Product> products = _productService.GetAllByCategoryId(2);
            List<ProductSoldInTheBakery> productsSoldInTheBakery = new();
            for (int i = 0; i < products.Count; i++)
            {
                BreadSold breadSold = new();

                //productSoldInTheBakery.ProductId = products[i].Id;
                //productSoldInTheBakery.ProductName = products[i].Name;
                //productSoldInTheBakery.Price = _productionListDetailService.GetProductionListDetailByDateAndProductId((DateTime.Now), products[i].Id).Price;


                breadSold.RemainingYesterday = _breadCountingService.GetBreadCountingByDate(DateTime.Now.AddDays(-1)).Quantity;
                breadSold.RemainingToday = _breadCountingService.GetBreadCountingByDate(DateTime.Now).Quantity;

                //breadSold.StaleProductToday = _staleProductService.GetQuantityStaleBreadByDateAndProductId((DateTime.Now), products[i].Id);

                //breadSold.ProducedToday = _productionListDetailService.GetProductionListDetailByDateAndProductId((DateTime.Now), products[i].Id).Quantity;

                //productSoldInTheBakery.Revenue = productSoldInTheBakery.Price * (productSoldInTheBakery.RemainingYesterday + productSoldInTheBakery.ProducedToday - productSoldInTheBakery.RemainingToday - productSoldInTheBakery.StaleProductToday);

                //productsSoldInTheBakery.Add(productSoldInTheBakery);
            }

            List<GivenProductsToServiceTotalResultDto> givenProductsToServiceTotalResultDto = _givenProductsToServiceService.GetTotalQuantityByDate(date);
            for (int i = 0; i< givenProductsToServiceTotalResultDto.Count; i++)
            {

            }



            return Ok(productsSoldInTheBakery);
        }




        private class ProductSoldInTheBakery
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public decimal Revenue { get; set; }
            public int RemainingYesterday { get; set; }
            public int ProducedToday { get; set; }
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
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public decimal Revenue { get; set; }
            public int RemainingYesterday { get; set; }
            public int ProducedToday { get; set; }
            public int RemainingToday { get; set; }
            public int StaleProductToday { get; set; }
            public int GivenBreadsToServiceTotal { get; set; }

            
        }
    }
}
