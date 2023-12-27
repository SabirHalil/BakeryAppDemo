using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProductController : ControllerBase
    {

        private IServiceProductService _serviceProductService;
        

        public ServiceProductController(IServiceProductService serviceProductService)
        {
            _serviceProductService = serviceProductService; ;            
        }

        

        [HttpGet("GetAllServiceProduct")]
        public ActionResult GetServiceProduct()
        {
            var result = _serviceProductService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByIdServiceProduct")]
        public ActionResult GetByIdServiceProduct(int id)
        {
            var result = _serviceProductService.GetById(id);
            return Ok(result);
        }

        [HttpPost("AddProduct")]
        public ActionResult AddServiceProduct(ServiceProduct serviceProduct)
        {
            _serviceProductService.Add(serviceProduct);
            return Ok();
        }

        [HttpDelete("DeleteServiceProduct")]
        public ActionResult DeleteServiceProduct(ServiceProduct serviceProduct)
        {
            _serviceProductService.Delete(serviceProduct);
            return Ok();
        }

        [HttpPut("UpdateServiceProduct")]
        public ActionResult UpdateServiceProduct(ServiceProduct serviceProduct)
        {
            _serviceProductService.Update(serviceProduct);
            return Ok();
        }
    }
}