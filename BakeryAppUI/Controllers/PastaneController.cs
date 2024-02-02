using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class PastaneController : Controller
    {

        private readonly ApiService _apiService;
        private static Date _date = new Date { date = DateTime.Now };
        public PastaneController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {

            List<ProductionListDetail> productionListDetail =
                await _apiService.GetApiResponse<List<ProductionListDetail>>
                (ApiUrl.url + "/api/ProductionList/GetAddedProductsByDateAndCategoryId?date=" + _date.date.ToString("yyyy-MM-dd") + "&categoryId=1");


            ViewBag.productionListDetail = productionListDetail;            
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

        //public class ProductionListDetailDto
        //{
        //    public ProductionListDetail productionListDetail { get; set; }
        //    public string ProductName { get; set; }
            
        //}



    }
}
