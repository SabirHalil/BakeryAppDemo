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
        private static Date _date = new Date { date = DateTime.Now };
        //private static List<ProductionListDetailDto> _productionListDetailDto;
        public PastaneController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            string dateFormat = "yyyy-MM-dd";
            string currentDate = _date.date.ToString(dateFormat);
            string yesterdayDate = _date.date.AddDays(-1).ToString(dateFormat);

            //string productsCountingTodayUrl = $"{ApiUrl.url}/api/ProductsCounting/GetProductsCountingByDateAndCategory?date={currentDate}&categoryId=1";
            //string productsCountingYesterdayUrl = $"{ApiUrl.url}/api/ProductsCounting/GetProductsCountingByDateAndCategory?date={yesterdayDate}&categoryId=1";
            //string staleProductsUrl = $"{ApiUrl.url}/api/StaleProduct/GetByDateAndCategory?date={currentDate}&categoryId=1";


            string productionListUrl = $"{ApiUrl.url}/api/ProductionList/GetAddedProductsByDateAndCategoryId?date={currentDate}&categoryId=1";
            string productsCountingTodayUrl = $"{ApiUrl.url}/api/ProductsCounting/GetDictionaryProductsCountingByDateAndCategory?date={currentDate}&categoryId=1";
            string productsCountingYesterdayUrl = $"{ApiUrl.url}/api/ProductsCounting/GetDictionaryProductsCountingByDateAndCategory?date={yesterdayDate}&categoryId=1";
            string staleProductsUrl = $"{ApiUrl.url}/api/StaleProduct/GetStaleProductsByDateAndCategory?date={currentDate}&categoryId=1";


            List<ProductionListDetail> productionListDetail =
                await _apiService.GetApiResponse<List<ProductionListDetail>>
                (productionListUrl);

            //List<ProductsCountingDto> productsCountingToday =
            //    await _apiService.GetApiResponse<List<ProductsCountingDto>>
            //    (productsCountingTodayUrl);

            //List<ProductsCountingDto> productsCountingYesterday =
            //    await _apiService.GetApiResponse<List<ProductsCountingDto>>
            //    (productsCountingYesterdayUrl);

            //List<StaleProductDto> staleProducts =
            //    await _apiService.GetApiResponse<List<StaleProductDto>>
            //    (staleProductsUrl);


            Dictionary<int, int> productsCountingToday =
                await _apiService.GetApiResponse<Dictionary<int, int>>
                (productsCountingTodayUrl);

            Dictionary<int, int> productsCountingYesterday =
                 await _apiService.GetApiResponse<Dictionary<int, int>>
                 (productsCountingYesterdayUrl);

            Dictionary<int, int> staleProducts =
                await _apiService.GetApiResponse<Dictionary<int, int>>
                (staleProductsUrl);

            List<int> productIds = productionListDetail.Select(pd => pd.ProductId).ToList();

            productIds.AddRange(productsCountingYesterday.Keys.Except(productIds));
            //productIds.AddRange(productsCountingYesterday.Select(pc => pc.ProductId));

            var productionListDetailDto = productIds.Select(productId =>
            {
                var specificProduct = productionListDetail.FirstOrDefault(pd => pd.ProductId == productId);

                return new ProductionListDetailDto
                {
                    ProductId = productId,
                    ProductName = specificProduct?.ProductName,
                    ProductedToday = specificProduct?.Quantity ?? 0,
                    Price = specificProduct?.Price ?? 0,

                    RemainingToday = productsCountingToday.TryGetValue(productId, out var todayValue) ? todayValue : 0,
                    RemainingYesterday = productsCountingYesterday.TryGetValue(productId, out var yesterdayValue) ? yesterdayValue : 0,
                    StaleProductToday = staleProducts.TryGetValue(productId, out var staleValue) ? staleValue : 0

                    //RemainingToday = productsCountingToday.FirstOrDefault(item => item.ProductId == productId)?.Quantity ?? 0,
                    //RemainingYesterday = productsCountingYesterday.FirstOrDefault(item => item.ProductId == productId)?.Quantity ?? 0,
                    //StaleProductToday = staleProducts.FirstOrDefault(item => item.ProductId == productId)?.Quantity ?? 0
                };
            }).ToList();

           // _productionListDetailDto = productionListDetailDto;
            ViewBag.productionListDetailDto = productionListDetailDto;
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
