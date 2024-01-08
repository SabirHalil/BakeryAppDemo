using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAppDemo.Controllers;
using static BakeryAppUI.Controllers.MarketeVerilenEkmekController;

namespace BakeryAppUI.Controllers
{
    public class MarketeVerilenEkmekController : Controller
    {
        private readonly ApiService _apiService;
        private static Date _date = new Date { date = DateTime.Now };
        public MarketeVerilenEkmekController(ApiService apiService)
        {
            _apiService = apiService;
        }



        public async Task<IActionResult> Index()
        {

            List<ServiceList> serviceList =
                await _apiService.GetApiResponse<List<ServiceList>>
                ("https://localhost:7207/api/Service/GetByDateServiceList?date=" + _date.date.ToString("yyyy-MM-dd"));

            List<ServiceListAndDetailDto> serviceListAndDetailDtos = new();

            for (int i = 0; i < serviceList.Count; i++)
            {
                List<ServiceListDetail> serviceListDetail
                    = await _apiService.GetApiResponse<List<ServiceListDetail>>
                    ("https://localhost:7207/api/Service/GetAddedMarketByServiceListId?listId=" + serviceList[i].Id);

                ServiceListAndDetailDto serviceListAndDetailDto = new();

                serviceListAndDetailDto.serviceListDetail = serviceListDetail;
                serviceListAndDetailDto.Name = "Servis";

                serviceListAndDetailDtos.Add(serviceListAndDetailDto);

            }
 
            List < PaymentMarket > paymentMarket =
                await _apiService.GetApiResponse<List<PaymentMarket>>
                ("https://localhost:7207/api/MoneyReceivedFromMarket/GetMoneyReceivedMarketListByDate?date=" + _date.date.ToString("yyyy-MM-dd"));

            ViewBag.serviceListAndDetailDtos = serviceListAndDetailDtos;
            ViewBag.date = _date.date;

            ViewBag.paymentMarket = paymentMarket;  

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



        public class ServiceListAndDetailDto
        {
            public List<ServiceListDetail> serviceListDetail { get; set; }
            public string Name { get; set; }
        }
    }
}
