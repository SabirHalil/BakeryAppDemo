using BakeryAppUI.Controllers;
using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAppDemo.Controllers
{
    public class DisaridanAlinanUrunlerController : Controller
    {
       
        private readonly ApiService _apiService;
        private static Date _date = new Date { date = DateTime.Now };
        public DisaridanAlinanUrunlerController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {

            List<PurchasedProduct> purchasedProduct =
                await _apiService.GetApiResponse<List<PurchasedProduct>>
                ("https://localhost:7207/api/PurchasedProduct/GetAddedPurchasedProductByDate?date=" + _date.date.ToString("yyyy-MM-dd"));

            

            ViewBag.purchasedProduct = purchasedProduct;
            ViewBag.Tezgahtar = "Ahmet Yıldız";
            ViewBag.date = _date.date;

            return View();
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
