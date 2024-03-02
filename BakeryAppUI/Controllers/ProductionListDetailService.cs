using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class ProductionListDetailService : Controller
    {
        private readonly ApiService _apiService;  
        public ProductionListDetailService(ApiService apiService)
        {
            _apiService = apiService;
        }
       
        public async Task<List<ProductionListDetailDto>> productionListDetailAsync(Date _date, int categoryId)
        {
            string dateFormat = "yyyy-MM-dd";
            string currentDate = _date.date.ToString(dateFormat);
            string yesterdayDate = _date.date.AddDays(-1).ToString(dateFormat);

            string productionListUrl = $"{ApiUrl.url}/api/ProductionList/GetAddedProductsByDateAndCategoryId?date={currentDate}&categoryId={categoryId}";
            string productsCountingTodayUrl = $"{ApiUrl.url}/api/ProductsCounting/GetDictionaryProductsCountingByDateAndCategory?date={currentDate}&categoryId={categoryId}";
            string productsCountingYesterdayUrl = $"{ApiUrl.url}/api/ProductsCounting/GetDictionaryProductsCountingByDateAndCategory?date={yesterdayDate}&categoryId={categoryId}";
            string staleProductsUrl = $"{ApiUrl.url}/api/StaleProduct/GetStaleProductsByDateAndCategory?date={currentDate}&categoryId={categoryId}";


            List<ProductionListDetail> productionListDetail =
                await _apiService.GetApiResponse<List<ProductionListDetail>>
                (productionListUrl);

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
            //productIds.AddRange(productsCountingYesterday.Keys.Except(productIds));


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
                };
            }).ToList();


            string productsUrl;
            List<int> productIdsNotProducedTodayButRemainingFromYesterday = new();
            productIdsNotProducedTodayButRemainingFromYesterday.AddRange(productsCountingYesterday.Keys.Except(productIds));


            if (productIdsNotProducedTodayButRemainingFromYesterday.Count > 0)
            {
                for (int i = 0; i < productIdsNotProducedTodayButRemainingFromYesterday.Count; i++)
                {
                    productsUrl = $"{ApiUrl.url}/api / Product / GetProductById ? id ={productIdsNotProducedTodayButRemainingFromYesterday[0]}";

                    Product product = await _apiService.GetApiResponse<Product>(productsUrl);

                    productionListDetailDto.Add(new ProductionListDetailDto
                    {
                        ProductId = productIdsNotProducedTodayButRemainingFromYesterday[0],
                        ProductName = product.Name,
                        ProductedToday = 0,
                        Price = product.Price,
                        RemainingToday = productsCountingToday.TryGetValue(productIdsNotProducedTodayButRemainingFromYesterday[0], out var todayValue) ? todayValue : 0,
                        RemainingYesterday = productsCountingYesterday.TryGetValue(productIdsNotProducedTodayButRemainingFromYesterday[0], out var yesterdayValue) ? yesterdayValue : 0,
                        StaleProductToday = staleProducts.TryGetValue(productIdsNotProducedTodayButRemainingFromYesterday[0], out var staleValue) ? staleValue : 0
                    });
                }
            }

            return productionListDetailDto;
        }
    }
}
