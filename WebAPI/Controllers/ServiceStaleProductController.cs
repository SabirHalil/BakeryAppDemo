using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceStaleProductController : ControllerBase
    {

        private IServiceStaleProductService _serviceStaleProductService;


        public ServiceStaleProductController(IServiceStaleProductService serviceStaleProductService)
        {
            _serviceStaleProductService = serviceStaleProductService; ;
        }

        [HttpGet("GetStaleBreadListByDate")]
        public ActionResult GetStaleBreadListByDate(DateTime date, int serviceTypeId)
        {
            try
            {
                var result = _serviceStaleProductService.GetAllByDate(date, serviceTypeId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        

        [HttpPost("AddServiceStaleProduct")]
        public ActionResult AddServiceStaleProduct(ServiceStaleProduct serviceStaleProduct)
        {

            try
            {

                if (serviceStaleProduct == null || serviceStaleProduct.Quantity < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                _serviceStaleProductService.Add(serviceStaleProduct);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteServiceStaleProduct")]
        public ActionResult DeleteServiceStaleProduct(int id)
        {
            try
            {
                _serviceStaleProductService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateServiceStaleProduct")]
        public ActionResult UpdateServiceStaleProduct(ServiceStaleProduct serviceStaleProduct)
        {
            try
            {
                _serviceStaleProductService.Update(serviceStaleProduct);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}
