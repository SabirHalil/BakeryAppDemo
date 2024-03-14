using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaleProductsReceivedFromMarketController : ControllerBase
    {

        private IStaleProductsReceivedFromMarketService _staleProductsReceivedFromMarketService;

        private IMarketContractService _marketContractService;
        private IServiceListDetailService _serviceListDetailService;
        private IServiceListService _serviceListService;
        private IMarketService _marketService;

        public StaleProductsReceivedFromMarketController(IMarketService marketService, IMarketContractService marketContractService, IServiceListService serviceListService, IServiceListDetailService serviceListDetailService, IStaleProductsReceivedFromMarketService staleProductsReceivedFromMarketService)
        {
            _staleProductsReceivedFromMarketService = staleProductsReceivedFromMarketService;

            _serviceListService = serviceListService;
            _serviceListDetailService = serviceListDetailService;
            _marketContractService = marketContractService;
            _marketService = marketService;
        }

 
        [HttpGet("GetAllStaleProductsReceivedFromMarket")]
        public ActionResult GetStaleProductsReceivedFromMarket()
        {
            var result = _staleProductsReceivedFromMarketService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByIdStaleProductsReceivedFromMarket")]
        public ActionResult GetByIdStaleProductsReceivedFromMarket(int id)
        {
            var result = _staleProductsReceivedFromMarketService.GetById(id);
            return Ok(result);
        }

        [HttpGet("GetStaleProductsReceivedFromMarketByDateAndMarketId")]
        public ActionResult GetStaleProductsReceivedFromMarketByDateAndMarketId(int marketId, DateTime date)
        {
            var result = _staleProductsReceivedFromMarketService.GetByDateAndMarketId(marketId,date);
            return Ok(result);
        }

        [HttpPost("AddStaleProductsReceivedFromMarket")]
        public ActionResult AddStaleProductsReceivedFromMarket(StaleProductsReceivedFromMarket staleProductsReceivedFromMarket)
        {
            _staleProductsReceivedFromMarketService.Add(staleProductsReceivedFromMarket);
            return Ok();
        }

        [HttpDelete("DeleteStaleProductsReceivedFromMarket")]
        public ActionResult DeleteStaleProductsReceivedFromMarket(StaleProductsReceivedFromMarket staleProductsReceivedFromMarket)
        {
            _staleProductsReceivedFromMarketService.Delete(staleProductsReceivedFromMarket);
            return Ok();
        }

        [HttpPut("UpdateStaleProductsReceivedFromMarket")]
        public ActionResult UpdateStaleProductsReceivedFromMarket(StaleProductsReceivedFromMarket staleProductsReceivedFromMarket)
        {
            _staleProductsReceivedFromMarketService.Update(staleProductsReceivedFromMarket);
            return Ok();
        }
    }
}