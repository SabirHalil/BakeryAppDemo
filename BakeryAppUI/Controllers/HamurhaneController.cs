using Microsoft.AspNetCore.Mvc;

namespace WebAppDemo.Controllers
{
    public class HamurhaneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
