using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreadPriceController : ControllerBase
    {


        private IBreadPriceService _breadPriceService;


        public BreadPriceController(IBreadPriceService breadPriceService)
        {
            _breadPriceService = breadPriceService; ;
        }

        [HttpGet("GetBreadPriceByDate")]
        public ActionResult GetBreadPriceByDate(DateTime date)
        {
            try
            {
                decimal price = _breadPriceService.BreadPriceByDate(date);
                return Ok(price);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddBreadPrice")]
        public ActionResult AddBreadPrice(BreadPrice breadPrice)
        {

            try
            {
                if (_breadPriceService.IsExistByDate(breadPrice.Date))
                {
                    return BadRequest(Messages.OncePerDay);
                }

                if (breadPrice == null || breadPrice.Price <= 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                _breadPriceService.Add(breadPrice);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteBreadPriceById")]
        public ActionResult DeleteBreadPriceById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                _breadPriceService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateBreadPrice")]
        public ActionResult UpdateBreadPrice(BreadPrice breadPrice)
        {
            if (breadPrice == null || breadPrice.Price <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                if (breadPrice.Price == 0)
                {
                    _breadPriceService.DeleteById(breadPrice.Id);
                }
                else
                {
                    _breadPriceService.Update(breadPrice);

                }


                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}
