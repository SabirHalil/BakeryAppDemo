using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {

        private IMarketService _marketService;


        public MarketController(IMarketService marketService)
        {
            _marketService = marketService;
        }



        [HttpGet("GetAllMarket")]
        public ActionResult GetMarket()
        {
            try
            {
                var result = _marketService.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetByIdMarket")]
        public ActionResult GetByIdMarket(int id)
        {
            try
            {
                var result = _marketService.GetById(id);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddMarket")]
        public ActionResult AddMarket(Market market)
        {
            try
            {
                _marketService.Add(market);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteMarketById")]
        public ActionResult DeleteMarketById(int id)
        {
            try
            {
                _marketService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateMarket")]
        public ActionResult UpdateMarket(Market market)
        {
            try
            {
                _marketService.Update(market);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}