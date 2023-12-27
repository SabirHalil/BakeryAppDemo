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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(User user)
        {

            using var httpClient = new HttpClient();

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                "application/json"
            );
            var responseUser = await httpClient.PostAsync("https://localhost:7207/api/Auth/login", jsonContent);

            if (responseUser.StatusCode == HttpStatusCode.OK)
            {
                var responseData = await responseUser.Content.ReadAsStringAsync();
                var tupleResult = JsonConvert.DeserializeObject<Tuple<object, int, string, string>>(responseData);



                //if(! (tupleResult.Item3.ToString() == "ismet"))
                
                //{
                //    ModelState.AddModelError(string.Empty, "Sadece patron giriş yapabilir.");
                //    return View();
                //}





                JToken jsonToken = JToken.Parse(tupleResult.Item1.ToString());
                AccessToken.Token = jsonToken["token"].ToString();
                AccessToken.Expiration = DateTime.Parse(jsonToken["expiration"].ToString());

                //return RedirectToAction("Index", "Home", new { userID = tupleResult.Item2 });
                return RedirectToAction("Index", "Home");
            }


            ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre yanlış.");
            return View();          
        }
    }
}
