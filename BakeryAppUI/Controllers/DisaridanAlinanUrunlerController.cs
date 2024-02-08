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
            string dateFormat = "yyyy-MM-dd";
            string currentDate = _date.date.ToString(dateFormat);
            string yesterdayDate = _date.date.AddDays(-1).ToString(dateFormat);

            string purchasedProductListUrl = $"{ApiUrl.url}/api/PurchasedProduct/GetAddedPurchasedProductByDate?date={currentDate}";
            string productsCountingTodayUrl = $"{ApiUrl.url}/api/ProductsCounting/GetProductsCountingByDateAndCategory?date={currentDate}&categoryId=2";
            string productsCountingYesterdayUrl = $"{ApiUrl.url}/api/ProductsCounting/GetProductsCountingByDateAndCategory?date={yesterdayDate}&categoryId=2";
            string staleProductsUrl = $"{ApiUrl.url}/api/StaleProduct/GetStaleProductsByDateAndCategory?date={currentDate}&categoryId=2";


            List<PurchasedProduct> purchasedProduct =
                await _apiService.GetApiResponse<List<PurchasedProduct>>
                (purchasedProductListUrl);


            //List<ProductionListDetail> productionListDetail =
            //    await _apiService.GetApiResponse<List<ProductionListDetail>>
            //    (productionListUrl);

            Dictionary<int, int> productsCountingToday =
                await _apiService.GetApiResponse<Dictionary<int, int>>
                (productsCountingTodayUrl);

            Dictionary<int, int> productsCountingYesterday =
                 await _apiService.GetApiResponse<Dictionary<int, int>>
                 (productsCountingYesterdayUrl);

            Dictionary<int, int> staleProducts =
                await _apiService.GetApiResponse<Dictionary<int, int>>
                (staleProductsUrl);


            List<int> productIds = purchasedProduct.Select(pd => pd.ProductId).ToList();
            productIds.AddRange(productsCountingYesterday.Keys.Except(productIds));


            var productionListDetailDto = productIds.Select(productId =>
            {
                var specificProduct = purchasedProduct.FirstOrDefault(pd => pd.ProductId == productId);

                return new ProductionListDetailDto
                {
                    ProductId = productId,
                    ProductName = specificProduct?.ProductName,
                    ProductedToday = specificProduct?.Quantity ?? 0,
                   // Price = specificProduct?.Price ?? 0,
                    Price =1,
                    RemainingToday = productsCountingToday.TryGetValue(productId, out var todayValue) ? todayValue : 0,
                    RemainingYesterday = productsCountingYesterday.TryGetValue(productId, out var yesterdayValue) ? yesterdayValue : 0,
                    StaleProductToday = staleProducts.TryGetValue(productId, out var staleValue) ? staleValue : 0
                };
            }).ToList();


            ViewBag.productionListDetailDto = productionListDetailDto;
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
