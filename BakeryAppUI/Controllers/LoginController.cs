using BakeryAppUI.Controllers;
using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace WebAppNew.Controllers
{
    public class LoginController : Controller
    {

        private readonly ApiService _apiService;

        public LoginController(ApiService apiService)
        {
            _apiService = apiService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(User user)
        {
            string apiUrl = ApiUrl.url;
            string loginEndpoint = "/api/Auth/login";

            string queryString = $"userName={Uri.EscapeDataString(user.email)}&password={Uri.EscapeDataString(user.password)}";
            string fullUrl = $"{apiUrl}{loginEndpoint}?{queryString}";

            Login login = await _apiService.GetApiResponse<Login>(fullUrl);

            if (login != null)
            {                
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre yanlış.");
            return View();
        }
    }
}
