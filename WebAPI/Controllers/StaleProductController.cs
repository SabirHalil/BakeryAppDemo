using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaleProductController : ControllerBase
    {

        private IStaleProductService _staleProductService;


        public StaleProductController(IStaleProductService staleProductService)
        {
            _staleProductService = staleProductService;
        }

        [HttpGet("GetByDateAndCategory")]
        public ActionResult GetByDateAndCategory(DateTime date, int categoryId)
        {
            var result = _staleProductService.GetByDateAndCategory(date, categoryId);
            return Ok(result);
        }

        [HttpGet("GetProductsNotAddedToStale")]
        public ActionResult GetProductsNotAddedToStale(DateTime date, int categoryId)
        {
            var result = _staleProductService.GetProductsNotAddedToStale(date, categoryId);
            return Ok(result);
        }

        [HttpPost("AddStaleProduct")]
        public ActionResult AddStaleProduct(StaleProduct staleProduct)
        {
            _staleProductService.Add(staleProduct);
            return Ok();
        }

        [HttpDelete("DeleteStaleProduct")]
        public ActionResult DeleteStaleProduct(int id)
        {
            _staleProductService.DeleteById(id);
            return Ok();
        }

        [HttpPut("UpdateStaleProduct")]
        public ActionResult UpdateStaleProduct(StaleProduct staleProduct)
        {
            _staleProductService.Update(staleProduct);
            return Ok();
        }
    }
}