using BakeryAppUI.Controllers;
using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Xml.Linq;
using static WebAppDemo.Controllers.HamurhaneController;



namespace WebAppDemo.Controllers
{
    public class HamurhaneController : Controller
    {

        private readonly ApiService _apiService;
        private static Date _date = new Date { date = DateTime.Now };
        public HamurhaneController(ApiService apiService)
        {
            _apiService = apiService;
        }



        public async Task<IActionResult> Index()
        {
            decimal breadPrice =
               await _apiService.GetApiResponse<decimal>
               (ApiUrl.url + "/api/BreadPrice/GetBreadPriceByDate?date=" + _date.date.ToString("yyyy-MM-dd"));


            List<DoughFactoryListDto> doughFactoryListDto =
                await _apiService.GetApiResponse<List<DoughFactoryListDto>>
                (ApiUrl.url + "/api/DoughFactory/GetByDateDoughFactoryList?date=" + _date.date.ToString("yyyy-MM-dd"));


            List<GetAddedDoughFactoryListDetailDto> AllDoughFactoryProducts = new();
            for (int i = 0; i < doughFactoryListDto.Count; i++)
            {
                List<GetAddedDoughFactoryListDetailDto> getAddedDoughFactoryListDetailDto
                    = await _apiService.GetApiResponse<List<GetAddedDoughFactoryListDetailDto>>
                    (ApiUrl.url + "/api/DoughFactory/GetAddedDoughFactoryListDetailByListId?doughFactoryListId=" + doughFactoryListDto[i].Id);

                AllDoughFactoryProducts.AddRange(getAddedDoughFactoryListDetailDto);
            }


            List<int> uniqueProductIds = AllDoughFactoryProducts.Select(dto => dto.DoughFactoryProductId).Distinct().ToList();
            //Dictionary<int, string> idToNameDictionary = new Dictionary<int, string>();
            //foreach (var product in AllDoughFactoryProducts)
            //{
            //    // Her bir id'yi sadece bir kez ekleyelim.
            //    if (!idToNameDictionary.ContainsKey(product.DoughFactoryProductId))
            //    {
            //        idToNameDictionary.Add(product.DoughFactoryProductId, product.DoughFactoryProductName);
            //    }
            //}


            List<DoughFactoryListAndDetailDto> doughFactoryListAndDetailDto = new();

            decimal TotalRevenue = 0;
            foreach (var u in uniqueProductIds)
            {
                DoughFactoryListAndDetailDto d = new();
                d.DoughFactoryProductQuantity = new Dictionary<string, int>();

                int TotalQuantity =0;

                for (int j = 0; j < doughFactoryListDto.Count; j++)
                {
                    string dynamicName = "Hamur" + (j + 1).ToString();

                    var result = AllDoughFactoryProducts.FirstOrDefault(dto => dto.DoughFactoryProductId == u && dto.DoughFactoryListId == doughFactoryListDto[j].Id);

                    d.DoughFactoryProductQuantity[dynamicName] = result == null ? 0 : result.Quantity;
                    TotalQuantity += d.DoughFactoryProductQuantity[dynamicName];
                }


                DoughFactoryProduct doughFactoryProduct =
                   await _apiService.GetApiResponse<DoughFactoryProduct>
                   (ApiUrl.url + "/api/DoughFactoryProduct/GetByDoughFactoryProductId?doughFactoryProductId=" + u.ToString());


                d.Name = doughFactoryProduct.Name;
                d.UnitPrice = (breadPrice * (decimal)doughFactoryProduct.BreadEquivalent);
                d.TotalQuantity = TotalQuantity;

                TotalRevenue += d.UnitPrice * d.TotalQuantity;
                doughFactoryListAndDetailDto.Add(d);
            }

            ViewBag.doughFactoryListAndDetailDtos = doughFactoryListAndDetailDto;
            ViewBag.TotalRevenue = TotalRevenue;
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

                    string Url = $"{ApiUrl.url}/api/CreatePdf/CreatePdfForHamurhane?date={currentDate}";

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

       

     

        //public class DoughFactoryListAndDetailDto
        //{
        //    public List<GetAddedDoughFactoryListDetailDto> getAddedDoughFactoryListDetailDto { get; set; }
        //    public string Name { get; set; }

        //}

        public class DoughFactoryListAndDetailDto
        {
            public Dictionary<string, int> DoughFactoryProductQuantity { get; set; }
            public string Name { get; set; }
            public decimal UnitPrice { get; set; }
            public int TotalQuantity { get; set; }
        }




    }
}
