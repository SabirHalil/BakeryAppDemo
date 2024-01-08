using Microsoft.AspNetCore.Mvc;

namespace WebAppDemo.Controllers
{
    public class KalanEkmekController : Controller
    {
        public IActionResult Index()
        {
         //   https://localhost:7207/api/BreadCounting/GetBreadCountingByDate?date=2024-01-07


            return View();
        }
    }
}
