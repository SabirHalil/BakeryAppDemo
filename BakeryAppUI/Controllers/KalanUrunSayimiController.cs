using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class KalanUrunSayimiController : Controller
    {
        private readonly ApiService _apiService;
        private static Date _date = new Date { date = DateTime.Now };
        public KalanUrunSayimiController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {

            List<ProductCounting> productCounting =
                await _apiService.GetApiResponse<List<ProductCounting>>
                ("https://localhost:7207/api/ProductsCounting/GetAddedProductsCountingByDate?date=" + _date.date.ToString("yyyy-MM-dd"));

            var breadCounting =
                await _apiService.GetApiResponse<BreadCounting>
                ("https://localhost:7207/api/BreadCounting/GetBreadCountingByDate?date=" + _date.date.ToString("yyyy-MM-dd"));


            ViewBag.productCounting = productCounting;
            ViewBag.breadCounting = breadCounting;
      
            ViewBag.date = _date.date;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PostDate(Date adate)
        {            
            _date.date = adate.date;
            return RedirectToAction("Index");
        }
    }
}
