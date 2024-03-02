using BakeryAppUI.Controllers;
using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAppDemo.Controllers
{
    public class DisaridanAlinanUrunlerController : Controller
    {

        private readonly ApiService _apiService;
        private readonly ProductionListDetailService _productionListDetailService;
        private static Date _date = new Date { date = DateTime.Now };
        public DisaridanAlinanUrunlerController(ApiService apiService, ProductionListDetailService productionListDetailService)
        {
            _apiService = apiService;
            _productionListDetailService = productionListDetailService;
        }
        
        public async Task<IActionResult> Index()
        {

            ViewBag.productionListDetailDto = await _productionListDetailService.productionListDetailAsync(_date, 3); ;
            ViewBag.date = _date.date;

            return View();          
        }


        public async Task<IActionResult> CreatePdfAsync()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string dateFormat = "yyyy-MM-dd";
                    string currentDate = _date.date.ToString(dateFormat);

                    string Url = $"{ApiUrl.url}/api/CreatePdf/CreatePdf?date={currentDate}&categoryId=3";

                    var response = await httpClient.GetAsync(Url);

                    if (response.IsSuccessStatusCode)
                    {
                        var pdfData = await response.Content.ReadAsByteArrayAsync();
                        var base64Pdf = Convert.ToBase64String(pdfData);

                        return Json(base64Pdf);
                    }
                    else
                    {
                        // HTTP hata durumunu kontrol et
                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            // 404 Not Found durumunu ele al
                            return Json("Error: 404 Not Found");
                        }
                        else
                        {
                            // Diğer hata durumlarını ele al
                            return Json("Error: " + response.StatusCode);
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    // HTTP isteği sırasında genel bir hata durumu
                    return Json("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Genel bir hata durumu
                    return Json("Error: " + ex.Message);
                }
            }
        }



        [HttpPost]
        public async Task<ActionResult> PostDate(Date adate)
        {            
            _date.date = adate.date;
            return RedirectToAction("Index"); 
        }
    }
}
