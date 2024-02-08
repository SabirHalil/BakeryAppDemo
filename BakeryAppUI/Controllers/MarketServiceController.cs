using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class MarketServiceController : Controller
    {
        private readonly ApiService _apiService;
        private static Date _date = new Date { date = DateTime.Now };
        public MarketServiceController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {          
            string marketBreadDetailsUrl = $"{ApiUrl.url}/api/MoneyReceivedFromMarket/GetMarketsEndOfDayCalculationWithDetail?date={_date.date.ToString("yyyy-MM-dd")}";
        
            List<MarketBreadDetails> marketBreadDetails =
                await _apiService.GetApiResponse<List<MarketBreadDetails>>
                (marketBreadDetailsUrl);

            decimal TotalAmount =0;
            decimal TotalReceivedMoney = 0;

            foreach (var item in marketBreadDetails)
            {
                TotalAmount += item.TotalAmount;
                TotalReceivedMoney += item.Amount;
            }

            ViewBag.TotalAmount = TotalAmount;
            ViewBag.TotalReceivedMoney = TotalReceivedMoney;
            
            ViewBag.MarketBreadDetails = marketBreadDetails;
            ViewBag.date = _date.date;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PostDate(Date adate)
        {
            // 'model.SelectedDate' üzerinden tarih bilgisini alabilirsiniz.
            // Burada istediğiniz işlemleri gerçekleştirebilirsiniz.
            _date.date = adate.date;

            return RedirectToAction("Index"); // İsteğe bağlı olarak başka bir sayfaya yönlendirme yapabilirsiniz.
        }
    }
}
