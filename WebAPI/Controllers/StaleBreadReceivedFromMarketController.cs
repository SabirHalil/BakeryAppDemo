using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaleBreadReceivedFromMarketController : ControllerBase
    {

        private IStaleBreadReceivedFromMarketService _staleBreadReceivedFromMarketService;


        public StaleBreadReceivedFromMarketController(IStaleBreadReceivedFromMarketService staleBreadReceivedFromMarketService)
        {
            _staleBreadReceivedFromMarketService = staleBreadReceivedFromMarketService;
        }



        [HttpGet("GetStaleBreadReceivedFromMarketByMarketId")]
        public ActionResult GetStaleBreadReceivedFromMarket(int marketId, DateTime date)
        {
            try
            {
                var result = _staleBreadReceivedFromMarketService.GetByMarketId(marketId, date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetByIdStaleBreadReceivedFromMarket")]
        public ActionResult GetByIdStaleBreadReceivedFromMarket(int id)
        {

            try
            {
                var result = _staleBreadReceivedFromMarketService.GetById(id);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("AddStaleBreadReceivedFromMarket")]
        public ActionResult AddStaleBreadReceivedFromMarket(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {
            if(staleBreadReceivedFromMarket == null|| staleBreadReceivedFromMarket.Quantity < 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                _staleBreadReceivedFromMarketService.Add(staleBreadReceivedFromMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteStaleBreadReceivedFromMarket")]
        public ActionResult DeleteStaleBreadReceivedFromMarket(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {

            try
            {
                _staleBreadReceivedFromMarketService.Delete(staleBreadReceivedFromMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteByDateAndMarketId")]
        public ActionResult DeleteByDateAndMarketId(DataForDeleteForStaleBreadReceivedFromMarket dataForDeleteForStaleBreadReceivedFromMarket)
        {

            try
            {
                _staleBreadReceivedFromMarketService.DeleteByDateAndMarketId(dataForDeleteForStaleBreadReceivedFromMarket.Date, dataForDeleteForStaleBreadReceivedFromMarket.MarketId);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("UpdateStaleBreadReceivedFromMarket")]
        public ActionResult UpdateStaleBreadReceivedFromMarket(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {


            try
            {
                _staleBreadReceivedFromMarketService.Update(staleBreadReceivedFromMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        public class DataForDeleteForStaleBreadReceivedFromMarket
        {
            public DateTime Date { get; set; }
            public int MarketId { get; set; }
        }
    }
}