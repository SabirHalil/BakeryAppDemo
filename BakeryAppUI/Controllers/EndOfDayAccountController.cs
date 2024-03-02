using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class EndOfDayAccountController : Controller
    {
        private readonly ApiService _apiService;
        private readonly EndOfDayAccountService _endOfDayAccountService;
        private static Date _date = new Date { date = DateTime.Now };
        public EndOfDayAccountController(ApiService apiService, EndOfDayAccountService endOfDayAccountService)
        {
            _apiService = apiService;
            _endOfDayAccountService = endOfDayAccountService;
        }

        // static decimal purchasedProductRevenue;

        public async Task<IActionResult> Index()
        {
            decimal PastaneTotalRevenue = 0;

            PastaneTotalRevenue += await _apiService.GetApiResponse<decimal>
               (ApiUrl.url + "/api/EndOfDayAccount/GetProductsSoldInTheBakery?date=" + _date.date.ToString("yyyy-MM-dd"));

            //ViewBag.Pastane = await _endOfDayAccountService.GetTotalRevenue(_date.date);
            ViewBag.Pastane = PastaneTotalRevenue;


            decimal breadPrice =
               await _apiService.GetApiResponse<decimal>
               (ApiUrl.url + "/api/BreadPrice/GetBreadPriceByDate?date=" + _date.date.ToString("yyyy-MM-dd"));
            ViewBag.breadPrice = breadPrice;

            decimal totalExpenseAmount = 0;
            List<Expense> expense =
            await _apiService.GetApiResponse<List<Expense>>
            (ApiUrl.url + "/api/Expense/GetExpensesByDate?date=" + _date.date.ToString("yyyy-MM-dd"));
            foreach (var item in expense)
            {
                totalExpenseAmount += item.Amount;
            }

            EndOfDayResult endOfDayResult =
                        await _apiService.GetApiResponse<EndOfDayResult>
                        (ApiUrl.url + "/api/EndOfDayAccount/GetEndOfDayAccountDetail?date=" + _date.date.ToString("yyyy-MM-dd"));



            ViewBag.EndOfDayAccount = endOfDayResult.EndOfDayAccount;
            ViewBag.Account = endOfDayResult.Account;



            ViewBag.expense = expense;
            ViewBag.totalExpenseAmount = totalExpenseAmount;
            ViewBag.date = _date.date;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DevretAction(decimal NetElden, decimal KrediKard)
        {
            try
            {

                NetEldenAmount netEldenAmount = new NetEldenAmount() { Id = 0,Date = DateTime.Now, Amount = NetElden };
                string endpointNetElden = ApiUrl.url + "/api/NetEldenAmount/AddNetEldenAmount";

                await _apiService.PostApiResponse<NetEldenAmount>(endpointNetElden, netEldenAmount);



                CreditCardAmount creditCardAmount = new CreditCardAmount() { Id = 0, Date = DateTime.Now, Amount = KrediKard };
                string endpointKrediKard = ApiUrl.url + "/api/CreditCardAmount/AddCreditCardAmount";

                await _apiService.PostApiResponse<CreditCardAmount>(endpointKrediKard, creditCardAmount);

                

               


                return Json(new { success = true, message = "Devret işlemi başarıyla gerçekleştirildi." });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // return Json(new { success = false, message = "Bir hata oluştu. Daha sonra tekrar deneyin." });

                return Json(new { success = false, message = e });
            }

        }

        public async Task<IActionResult> CreatePdfAsync()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string dateFormat = "yyyy-MM-dd";
                    string currentDate = _date.date.ToString(dateFormat);

                    string Url = $"{ApiUrl.url}/api/EndOfDayAccount/GetEndOfDayAccountDetailPdf?date={currentDate}";

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

        public class EndOfDayResult
        {
            public EndOfDayAccountForBread EndOfDayAccount { get; set; }
            public Account Account { get; set; }
        }
    }
}
