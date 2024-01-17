using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class GiderlerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
