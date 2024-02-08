using Business.Abstract;
using Business.Constants;
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

        [HttpGet("GetStaleProductsByDateAndCategory")]
        public ActionResult GetStaleProductsByDateAndCategory(DateTime date, int categoryId)
        {
            try
            {
                var result = _staleProductService.GetStaleProductsByDateAndCategory(date, categoryId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
        
        [HttpGet("GetByDateAndCategory")]
        public ActionResult GetByDateAndCategory(DateTime date, int categoryId)
        {
            try
            {
                var result = _staleProductService.GetByDateAndCategory(date, categoryId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetProductsNotAddedToStale")]
        public ActionResult GetProductsNotAddedToStale(DateTime date, int categoryId)
        {
            try
            {
                var result = _staleProductService.GetProductsNotAddedToStale(date, categoryId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddStaleProduct")]
        public ActionResult AddStaleProduct(StaleProduct staleProduct)
        {
            try
            {
                if (_staleProductService.IsExist(staleProduct.ProductId, staleProduct.Date))
                {
                    return BadRequest(Messages.OncePerDay);
                }
                if (staleProduct == null || staleProduct.Quantity < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                _staleProductService.Add(staleProduct);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteStaleProduct")]
        public ActionResult DeleteStaleProduct(int id)
        {
            try
            {
                _staleProductService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateStaleProduct")]
        public ActionResult UpdateStaleProduct(StaleProduct staleProduct)
        {
            try
            {
                _staleProductService.Update(staleProduct);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}