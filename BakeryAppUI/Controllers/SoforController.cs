using Microsoft.AspNetCore.Mvc;

namespace WebAppDemo.Controllers
{
    public class SoforController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
