using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BakeryAppUI.Controllers
{
    public class PastaneController : Controller
    {

        private readonly ApiService _apiService;
        private readonly ProductionListDetailService _productionListDetailService;
        private static Date _date = new Date { date = DateTime.Now };
        public PastaneController(ApiService apiService, ProductionListDetailService productionListDetailService)
        {
            _apiService = apiService;
            _productionListDetailService = productionListDetailService;
        }

        public async Task<IActionResult> Index()
        {
            
            ViewBag.productionListDetailDto = await _productionListDetailService.productionListDetailAsync(_date, 1); ;
            ViewBag.date = _date.date;

            return View();
        }

        //public ActionResult Guncelle(int id)
        //{
        //    VeriModel veri = new VeriModel()
        //    {
        //        Id = id
        //    }
        //    ;

        //    return PartialView("_GuncelleModal", veri);
        //}

        //[HttpPost]
        //public ActionResult Guncelle(VeriModel veri)
        //{


        //    return RedirectToAction("Index");
        //}

        //public ActionResult Update(int id)
        //{
        //    var dd = id;
        //    // Id'ye göre veriyi çekme işlemleri
        //    //VeriModel veri = VeriServisi.VeriGetir(id);
        //    // return PartialView("_GuncelleModal", veri);
        //    return PartialView("_GuncelleModal");
        //}

        //[HttpPost]
        //public ActionResult Update(VeriModel veri)
        //{
        //    // Veriyi güncelleme işlemleri
        //    VeriServisi.VeriGuncelle(veri);
        //    return RedirectToAction("Index");
        //}



        public async Task<IActionResult> CreatePdfAsync()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string dateFormat = "yyyy-MM-dd";
                    string currentDate = _date.date.ToString(dateFormat);

                    string Url = $"{ApiUrl.url}/api/CreatePdf/CreatePdf?date={currentDate}&categoryId=1";

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
            // 'model.SelectedDate' üzerinden tarih bilgisini alabilirsiniz.
            // Burada istediğiniz işlemleri gerçekleştirebilirsiniz.
            _date.date = adate.date;

            return RedirectToAction("Index"); // İsteğe bağlı olarak başka bir sayfaya yönlendirme yapabilirsiniz.
        }

    }
}
