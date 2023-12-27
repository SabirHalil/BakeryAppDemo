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
            var result = _marketService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByIdMarket")]
        public ActionResult GetByIdMarket(int id)
        {
            var result = _marketService.GetById(id);
            return Ok(result);
        }

        [HttpPost("AddProduct")]
        public ActionResult AddMarket(Market market)
        {
            _marketService.Add(market);
            return Ok();
        }

        [HttpDelete("DeleteMarket")]
        public ActionResult DeleteMarket(Market market)
        {
            _marketService.Delete(market);
            return Ok();
        }

        [HttpPut("UpdateMarket")]
        public ActionResult UpdateMarket(Market market)
        {
            _marketService.Update(market);
            return Ok();
        }
    }
}