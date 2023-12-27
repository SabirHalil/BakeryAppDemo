using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebAppDemo.Controllers
{
    public class KullanicilarController : Controller
    {
        public class UserListViewModel
        {
            public UserViewModel User { get; set; }
            public List<AllUser> AllUsers { get; set; }
        }


        public async Task<IActionResult> IndexAsync()
        {
            // Veritabanından tüm kullanıcıları çekmek için servisi kullanın
            List<AllUser> allUsers = await GetUsersFromDatabaseAsync(); // Bu fonksiyonu uygulamanıza göre güncelleyin

            // ViewModel oluşturun
            var userListViewModel = new UserListViewModel
            {
                AllUsers = allUsers,
                User = new UserViewModel() // Formdan gelen model
            };

            return View(userListViewModel);
        }

        private async Task<List<AllUser>> GetUsersFromDatabaseAsync()
        {
            string url = $"https://localhost:7207/api/User/GetUsers";

            using (HttpClient client = new HttpClient())
            {
                // API'den verileri çek
                HttpResponseMessage response = await client.GetAsync(url);

                // İstek başarılı olduysa
                if (response.IsSuccessStatusCode)
                {
                    // JSON verisini çek
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    // JSON verisini AllUser listesine dönüştür
                    List<AllUser> users = JsonConvert.DeserializeObject<List<AllUser>>(jsonResult);

                    // AllUser listesini döndür
                    return users;
                }
                else
                {
                //    // İstek başarısız olduysa, hata mesajını logla veya farklı bir işlem yapabilirsiniz.
                //    Console.WriteLine($"API'den veri çekerken hata oluştu. Hata Kodu: {response.StatusCode}");
                    return null;
                }
            }

         
        }


        private readonly string apiUrl = "https://localhost:7207/api/Auth/register"; // API'nin gerçek URL'si

        [HttpPost]
        public async Task<ActionResult> AddUser(UserViewModel user)
        {
            

            using (var httpClient = new HttpClient())
            {
                // API'ye gönderilecek veriyi hazırla
                var jsonContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                // API'ye HTTP POST isteği yap
                var response = await httpClient.PostAsync(apiUrl, jsonContent);

                // API'den dönen cevabı kontrol et
                if (!response.IsSuccessStatusCode)
                {                   
                    return Content("Kullanıcı eklenirken hata oluştu!");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        private readonly string deleteUrl = "https://localhost:7207/api/User/DeleteById/";

        [HttpPost]
        public async Task<ActionResult> DeleteById(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // API'ye DELETE isteği yap
                    var response = await httpClient.DeleteAsync($"{deleteUrl}{id}");

                    // API'den dönen cevabı kontrol et
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Content("Kullanıcı silinirken hata oluştu!");
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda uygun bir şekilde işleyebilirsiniz.
                return Content($"Bir hata oluştu: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(UserViewModel model)
        {
            try
            {
                // model içinde gelen verileri kullanarak kullanıcıyı güncelle
                // _userService.UpdateUser(model) veya benzeri bir metod kullanılabilir

                // Başarılı bir şekilde güncellendiyse
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Hata durumunda uygun bir şekilde işleyebilirsiniz.
                return Content($"Bir hata oluştu: {ex.Message}");
            }
        }

    }
}
