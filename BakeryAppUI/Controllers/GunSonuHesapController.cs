﻿using BakeryAppUI.Controllers;
using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAppNew.Controllers
{
    public class GunSonuHesapController : Controller
    {
        private readonly ApiService _apiService;
        private static Date _date = new Date { date = DateTime.Now };
        public GunSonuHesapController(ApiService apiService)
        {
            _apiService = apiService;
        }

       static decimal purchasedProductRevenue;

        public async Task<IActionResult> Index()
        {
            try
            {
                BreadSold breadSold =
                                await _apiService.GetApiResponse<BreadSold>
                                ("https://localhost:7207/api/EndOfDayAccount/GetBreadSold?date=" + _date.date.ToString("yyyy-MM-dd"));


                List<PurchasedProductSoldInTheBakery> purchasedProductSoldInTheBakerys =
                    await _apiService.GetApiResponse<List<PurchasedProductSoldInTheBakery>>
                    ("https://localhost:7207/api/EndOfDayAccount/GetPurchasedProductsSoldInTheBakery?date=" + _date.date.ToString("yyyy-MM-dd"));

                //ProductSoldInTheBakery productSoldInTheBakery =
                //    await _apiService.GetApiResponse<ProductSoldInTheBakery>
                //    ("https://localhost:7207/api/EndOfDayAccount/GetProductsSoldInTheBakery?date=" + _date.date.ToString("yyyy-MM-dd"));


                foreach (var item in purchasedProductSoldInTheBakerys)
                {
                    purchasedProductRevenue += item.Revenue;
                }


                ViewBag.date = _date.date;

                ViewBag.breadSold = breadSold;
                ViewBag.purchasedProductSoldInTheBakery = purchasedProductSoldInTheBakerys;
                ViewBag.purchasedProductRevenue = purchasedProductRevenue;


                
              
                //ViewBag.productSoldInTheBakery = productSoldInTheBakery;

                return View();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }            
        }


        [HttpPost]
        public async Task<ActionResult> PostDate(Date adate)
        {
            // 'model.SelectedDate' üzerinden tarih bilgisini alabilirsiniz.
            // Burada istediğiniz işlemleri gerçekleştirebilirsiniz.
            _date.date = adate.date;

            return RedirectToAction("Index"); // İsteğe bağlı olarak başka bir sayfaya yönlendirme yapabilirsiniz.
        }



        public class ProductSoldInTheBakery
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
        public class PurchasedProductSoldInTheBakery
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
        public class BreadSold
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
