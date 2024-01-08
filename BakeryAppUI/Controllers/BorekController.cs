using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class BorekController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
