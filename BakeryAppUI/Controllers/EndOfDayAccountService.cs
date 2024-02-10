using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class EndOfDayAccountService
    {       
        private readonly ApiService _apiService;
        public EndOfDayAccountService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<decimal> GetTotalRevenue(DateTime date)
        {
            string dateFormat = "yyyy-MM-dd";
            string currentDate = date.Date.ToString(dateFormat);
            string yesterdayDate = date.Date.AddDays(-1).ToString(dateFormat);

            List<PurchasedProduct> purchasedProduct3 = await GetPurchasedProductList(currentDate);
            Dictionary<int, int> productsCountingToday3 = await GetProductsCounting(currentDate,3);
            Dictionary<int, int> productsCountingYesterday3 = await GetProductsCounting(yesterdayDate,3);
            Dictionary<int, int> staleProducts3 = await GetStaleProducts(currentDate,3);
            List<ProductionListDetailDto> productionListDetailDto3 = GetProductionListDetail(purchasedProduct3, productsCountingToday3, productsCountingYesterday3, staleProducts3);
            
            List<ProductionListDetail> productionListDetail2 = await GetProductionListDetail(currentDate,2);
            Dictionary<int, int> productsCountingToday2 = await GetProductsCounting(currentDate,2);
            Dictionary<int, int> productsCountingYesterday2 = await GetProductsCounting(yesterdayDate,2);
            Dictionary<int, int> staleProducts2 = await GetStaleProducts(currentDate,2);
            List<ProductionListDetailDto> productionListDetailDto2 = GetProductionListDetail2(productionListDetail2, productsCountingToday2, productsCountingYesterday2, staleProducts2);
            
            List<ProductionListDetail> productionListDetail1 = await GetProductionListDetail(currentDate,1);
            Dictionary<int, int> productsCountingToday1 = await GetProductsCounting(currentDate,1);
            Dictionary<int, int> productsCountingYesterday1 = await GetProductsCounting(yesterdayDate,1);
            Dictionary<int, int> staleProducts1 = await GetStaleProducts(currentDate,1);
            List<ProductionListDetailDto> productionListDetailDto1 = GetProductionListDetail2(productionListDetail1, productsCountingToday1, productsCountingYesterday1, staleProducts1);


            decimal TotalRevenue = 0;
            TotalRevenue +=  CalculateTotalRevenue(productionListDetailDto3);
            TotalRevenue +=  CalculateTotalRevenue(productionListDetailDto2);
            TotalRevenue +=  CalculateTotalRevenue(productionListDetailDto1);

            return TotalRevenue;
        }
        private async Task<List<ProductionListDetail>> GetProductionListDetail(string currentDate, int categoryId)
        {
            string productionListUrl = $"{ApiUrl.url}/api/ProductionList/GetAddedProductsByDateAndCategoryId?date={currentDate}&categoryId=" + categoryId.ToString();
            return await _apiService.GetApiResponse<List<ProductionListDetail>>(productionListUrl);
        }        
        private async Task<List<PurchasedProduct>> GetPurchasedProductList(string currentDate)
        {
            string purchasedProductListUrl = $"{ApiUrl.url}/api/PurchasedProduct/GetAddedPurchasedProductByDate?date={currentDate}";
            return await _apiService.GetApiResponse<List<PurchasedProduct>>(purchasedProductListUrl);
        }

        private async Task<Dictionary<int, int>> GetProductsCounting(string date, int categoryId)
        {
            string productsCountingUrl = $"{ApiUrl.url}/api/ProductsCounting/GetProductsCountingByDateAndCategory?date={date}&categoryId="+ categoryId.ToString();
            return await _apiService.GetApiResponse<Dictionary<int, int>>(productsCountingUrl);
        }

        private async Task<Dictionary<int, int>> GetStaleProducts(string currentDate, int categoryId)
        {
            string staleProductsUrl = $"{ApiUrl.url}/api/StaleProduct/GetStaleProductsByDateAndCategory?date={currentDate}&categoryId=" + categoryId.ToString();
            return await _apiService.GetApiResponse<Dictionary<int, int>>(staleProductsUrl);
        }

        private List<ProductionListDetailDto> GetProductionListDetail(List<PurchasedProduct> purchasedProduct, Dictionary<int, int> productsCountingToday, Dictionary<int, int> productsCountingYesterday, Dictionary<int, int> staleProducts)
        {
            List<int> productIds = purchasedProduct.Select(pd => pd.ProductId).ToList();
            productIds.AddRange(productsCountingYesterday.Keys.Except(productIds));

            return productIds.Select(productId =>
            {
                var specificProduct = purchasedProduct.FirstOrDefault(pd => pd.ProductId == productId);

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
        }
        
        private List<ProductionListDetailDto> GetProductionListDetail2(List<ProductionListDetail> productionListDetail, Dictionary<int, int> productsCountingToday, Dictionary<int, int> productsCountingYesterday, Dictionary<int, int> staleProducts)
        {
            List<int> productIds = productionListDetail.Select(pd => pd.ProductId).ToList();
            productIds.AddRange(productsCountingYesterday.Keys.Except(productIds));

            return productIds.Select(productId =>
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
        }

        private decimal CalculateTotalRevenue(List<ProductionListDetailDto> productionListDetailDto)
        {
            return productionListDetailDto.Sum(product =>
                product.Price * (product.ProductedToday + product.RemainingYesterday - product.RemainingToday - product.StaleProductToday));
        }

    }
}
