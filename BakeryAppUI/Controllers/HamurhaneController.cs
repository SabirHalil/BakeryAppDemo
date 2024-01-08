using BakeryAppUI.Controllers;
using BakeryAppUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

            List<DoughFactoryListDto> doughFactoryListDto =
                await _apiService.GetApiResponse<List<DoughFactoryListDto>>
                ("https://localhost:7207/api/DoughFactory/GetByDateDoughFactoryList?date=" + _date.date.ToString("yyyy-MM-dd"));

            List<DoughFactoryListAndDetailDto> doughFactoryListAndDetailDtos = new();

            for (int i = 0; i < doughFactoryListDto.Count; i++)
            {
                List<GetAddedDoughFactoryListDetailDto> getAddedDoughFactoryListDetailDto
                    = await _apiService.GetApiResponse<List<GetAddedDoughFactoryListDetailDto>>
                    ("https://localhost:7207/api/DoughFactory/GetAddedDoughFactoryListDetailByListId?doughFactoryListId=" + doughFactoryListDto[i].Id);
                      
                DoughFactoryListAndDetailDto doughFactoryListAndDetailDto = new();

                doughFactoryListAndDetailDto.getAddedDoughFactoryListDetailDto = getAddedDoughFactoryListDetailDto;
                doughFactoryListAndDetailDto.Name = doughFactoryListDto[i].UserName;

                doughFactoryListAndDetailDtos.Add(doughFactoryListAndDetailDto);

            }

            ViewBag.doughFactoryListAndDetailDtos = doughFactoryListAndDetailDtos;
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










        public class DoughFactoryListAndDetailDto
        {
            public List<GetAddedDoughFactoryListDetailDto> getAddedDoughFactoryListDetailDto { get; set; }
            public string Name { get; set; }

        }
    }
}
