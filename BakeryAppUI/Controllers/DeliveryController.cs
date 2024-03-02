using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class DeliveryController : Controller
    {
        private readonly ApiService _apiService;

        public DeliveryController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {

            string dateFormat = "yyyy-MM-dd";
            string currentDate = DateTime.Now.ToString(dateFormat);

            string paymentAmountDueUrl = $"{ApiUrl.url}/api/Delivery/GetPaymentAmountDue?date={currentDate}";

            decimal paymentAmountDue =
                await _apiService.GetApiResponse<decimal>
                (paymentAmountDueUrl);



            ViewBag.paymentAmountDue = paymentAmountDue;
            //ViewBag.paymentAmountDue = 1000;

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> SaveDelivery(string toDeliver, string delivered)
        {
     
            string postDeliveryUrl = $"{ApiUrl.url}/api/Delivery/AddDelivery";
       

            Delivery delivery = new();
            delivery.DeliveryDate = DateTime.Now;
            delivery.DeliveredAmount = decimal.Parse(delivered);
            delivery.TotalAccumulatedAmount = decimal.Parse(toDeliver);
            
    
            _apiService.PostApiResponse<Delivery>(postDeliveryUrl, delivery);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeliveryAll(DateTime startDate, DateTime endDate)
        {
            string dateFormat = "yyyy-MM-dd";
           

            string listDeliveryUrl = $"{ApiUrl.url}/api/Delivery/GetBetweenDates?startDate={startDate.ToString(dateFormat)}&endDate={endDate.ToString(dateFormat)}";

            List<Delivery> listDelivery =
               await _apiService.GetApiResponse<List<Delivery>>
               (listDeliveryUrl);

            return Json(listDelivery);
        }


    }

    public class ProductViewModel
    {
        public string ProductName { get; set; }
        public int YesterdayRemaining { get; set; }
        public int Produced { get; set; }
        public int TotalNet { get; set; }
    }

}
