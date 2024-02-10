using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class EndOfDayAccountController : Controller
    {
        private readonly ApiService _apiService;
        private readonly EndOfDayAccountService _endOfDayAccountService;
        private static Date _date = new Date { date = DateTime.Now };
        public EndOfDayAccountController(ApiService apiService, EndOfDayAccountService endOfDayAccountService)
        {
            _apiService = apiService;
            _endOfDayAccountService = endOfDayAccountService;   
        }

        // static decimal purchasedProductRevenue;

        public async Task<IActionResult> Index()
        {
            decimal PastaneTotalRevenue = 0;
            
            PastaneTotalRevenue += await _apiService.GetApiResponse<decimal>
               (ApiUrl.url + "/api/EndOfDayAccount/GetProductsSoldInTheBakery?date=" + _date.date.ToString("yyyy-MM-dd"));

           //ViewBag.Pastane = await _endOfDayAccountService.GetTotalRevenue(_date.date);
            ViewBag.Pastane = PastaneTotalRevenue;


            decimal breadPrice =
               await _apiService.GetApiResponse<decimal>
               (ApiUrl.url + "/api/BreadPrice/GetBreadPriceByDate?date=" + _date.date.ToString("yyyy-MM-dd"));
            ViewBag.breadPrice = breadPrice;

            decimal totalExpenseAmount = 0;
            List<Expense> expense =
            await _apiService.GetApiResponse<List<Expense>>
            (ApiUrl.url + "/api/Expense/GetExpensesByDate?date=" + _date.date.ToString("yyyy-MM-dd"));
            foreach (var item in expense)
            {
                totalExpenseAmount += item.Amount;
            }

            EndOfDayResult endOfDayResult =
                        await _apiService.GetApiResponse<EndOfDayResult>
                        (ApiUrl.url + "/api/EndOfDayAccount/GetEndOfDayAccountDetail?date=" + _date.date.ToString("yyyy-MM-dd"));

            ViewBag.EndOfDayAccount = endOfDayResult.EndOfDayAccount;
            ViewBag.Account = endOfDayResult.Account;

           
            
            ViewBag.expense = expense;
            ViewBag.totalExpenseAmount = totalExpenseAmount;
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

        public class EndOfDayResult
        {
            public EndOfDayAccountForBread EndOfDayAccount { get; set; }
            public Account Account { get; set; }
        }
    }
}
