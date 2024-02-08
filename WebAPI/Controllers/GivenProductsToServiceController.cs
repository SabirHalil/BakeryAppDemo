using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GivenProductsToServiceController : ControllerBase
    {

        private IGivenProductsToServiceService _givenProductsToServiceService;
        

        public GivenProductsToServiceController(IGivenProductsToServiceService givenProductsToServiceService)
        {
            _givenProductsToServiceService = givenProductsToServiceService; ;            
        }

        [HttpGet("GetGivenProductsToServiceByDateAndServisTypeId")]
        public ActionResult GetGivenProductsToServiceByDateAndServisTypeId(DateTime date, int servisTypeId)
        {
            var result = _givenProductsToServiceService.GetAllByDateAndServisTypeId(date,servisTypeId);
            return Ok(result);
        }

        [HttpGet("GetGivenProductsToServiceDayResultByDateAndServiceType")]
        public ActionResult GetGivenProductsToServiceDayResult(DateTime date)
        {
            var result = _givenProductsToServiceService.GetTotalQuantityByDate(date);
            return Ok(result);
        }

        [HttpGet("GetByIdGivenProductsToService")]
        public ActionResult GetByIdGivenProductsToService(int id)
        {
            var result = _givenProductsToServiceService.GetById(id);
            return Ok(result);
        }

        [HttpPost("AddGivenProductsToService")]
        public ActionResult AddGivenProductsToService(GivenProductsToService givenProductsToService)
        {
            _givenProductsToServiceService.Add(givenProductsToService);
            return Ok();
        }

        [HttpDelete("DeleteGivenProductsToService")]
        public ActionResult DeleteGivenProductsToService(GivenProductsToService givenProductsToService)
        {
            _givenProductsToServiceService.Delete(givenProductsToService);
            return Ok();
        }

        [HttpPut("UpdateGivenProductsToService")]
        public ActionResult UpdateGivenProductsToService(GivenProductsToService givenProductsToService)
        {
            _givenProductsToServiceService.Update(givenProductsToService);
            return Ok();
        }
    }
}