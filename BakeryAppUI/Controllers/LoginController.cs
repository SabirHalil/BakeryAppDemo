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

            // Query string oluşturma
            string queryString = $"userName={Uri.EscapeDataString(user.email)}&password={Uri.EscapeDataString(user.password)}";

            // Tam URL oluşturma
            string fullUrl = $"{apiUrl}{loginEndpoint}?{queryString}";

            // HTTP Client oluşturma
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // POST isteği yapma
                    HttpResponseMessage response = await client.PostAsync(fullUrl, null);

                    // Başarılı bir cevap alındıysa
                    if (response.IsSuccessStatusCode)
                    {
                        // Cevabı okuma
                        string result = await response.Content.ReadAsStringAsync();

                        Login login = Newtonsoft.Json.JsonConvert.DeserializeObject<Login>(result);

                        // İlgili işlemleri gerçekleştirme
                        if (login != null && login.OperationClaimId == 6) // API'nin döndüğü başarı durumuna göre kontrol etmelisiniz
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre yanlış.");
                        return View();
                    }
                    else
                    {
                        // Başarısız bir cevap alındıysa hata mesajını yazdırma
                        Console.WriteLine($"Hata kodu: {response.StatusCode}, Hata mesajı: {response.ReasonPhrase}");
                        ModelState.AddModelError(string.Empty, "API ile iletişimde bir hata oluştu.");
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda istisna mesajını yazdırma
                    Console.WriteLine($"Hata: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "API ile iletişimde bir hata oluştu.");
                    return View();
                }
            }
        }


        public ActionResult Logout()
        {
            // Çıkış işlemleri

            // Yönlendirme
            return RedirectToAction("Index", "Login");
        }

    }
}
