using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class GiderlerController : Controller
    {
        private readonly ApiService _apiService;
        private static Date _date = new Date { date = DateTime.Now };
        public GiderlerController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {

            List<Expense> expense =
                await _apiService.GetApiResponse<List<Expense>>
                ("https://localhost:7207/api/Expense/GetExpensesByDate?date=" + _date.date.ToString("yyyy-MM-dd"));


            ViewBag.expense = expense;           
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
