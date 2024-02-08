using Business.Abstract;
using Entities.Concrete;

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
            try
            {
                var result = _givenProductsToServiceService.GetAllByDateAndServisTypeId(date, servisTypeId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetGivenProductsToServiceDayResultByDateAndServiceType")]
        public ActionResult GetGivenProductsToServiceDayResult(DateTime date)
        {
            try
            {
                var result = _givenProductsToServiceService.GetTotalQuantityByDate(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetByIdGivenProductsToService")]
        public ActionResult GetByIdGivenProductsToService(int id)
        {
            try
            {
                var result = _givenProductsToServiceService.GetById(id);
                return Ok(result);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddGivenProductsToService")]
        public ActionResult AddGivenProductsToService(GivenProductsToService givenProductsToService)
        {
            try
            {
                _givenProductsToServiceService.Add(givenProductsToService);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteGivenProductsToServiceById")]
        public ActionResult DeleteGivenProductsToServiceById(int id)
        {
            try
            {
                _givenProductsToServiceService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateGivenProductsToService")]
        public ActionResult UpdateGivenProductsToService(GivenProductsToService givenProductsToService)
        {
            try
            {
                _givenProductsToServiceService.Update(givenProductsToService);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}