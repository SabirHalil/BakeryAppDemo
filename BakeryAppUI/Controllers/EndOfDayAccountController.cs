using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class EndOfDayAccountController : Controller
    {
        private readonly ApiService _apiService;
        private static Date _date = new Date { date = DateTime.Now };
        public EndOfDayAccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // static decimal purchasedProductRevenue;

        public async Task<IActionResult> Index()
        {
            decimal totalExpenseAmount = 0;
            List<Expense> expense =
            await _apiService.GetApiResponse<List<Expense>>
            (ApiUrl.url + "/api/Expense/GetExpensesByDate?date=" + _date.date.ToString("yyyy-MM-dd"));
            foreach (var item in expense)
            {
                totalExpenseAmount += item.Amount;
            }


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
