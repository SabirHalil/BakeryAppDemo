using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class StaleProductController : Controller
    {
        private readonly ApiService _apiService;
        private static Date _date = new Date { date = DateTime.Now };
        public StaleProductController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {

            List<StaleBread> staleBread =
                await _apiService.GetApiResponse<List<StaleBread>>
                ("https://localhost:7207/api/StaleBread/GetStaleBreadListByDate?date=" + _date.date.ToString("yyyy-MM-dd"));
            
            List<StaleProduct> staleBorek =
                await _apiService.GetApiResponse<List<StaleProduct>>
                ("https://localhost:7207/api/StaleProduct/GetByDateAndCategory?date=" + _date.date.ToString("yyyy-MM-dd") + "&categoryId=2");
            
            List<StaleProduct> stalePurchasedProduct =
                await _apiService.GetApiResponse<List<StaleProduct>>
                ("https://localhost:7207/api/StaleProduct/GetByDateAndCategory?date=" + _date.date.ToString("yyyy-MM-dd") + "&categoryId=3");
                
            List<StaleProduct> stalePasta =
                await _apiService.GetApiResponse<List<StaleProduct>>
                ("https://localhost:7207/api/StaleProduct/GetByDateAndCategory?date=" + _date.date.ToString("yyyy-MM-dd") + "&categoryId=1");

          

            ViewBag.stalePasta = stalePasta;
            ViewBag.staleBorek = staleBorek;
            ViewBag.stalePurchasedProduct = stalePurchasedProduct;
            ViewBag.staleBread = staleBread;

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
