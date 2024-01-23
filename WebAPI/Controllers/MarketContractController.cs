using Business.Abstract;
using Entities.Concrete;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketContractController : ControllerBase
    {

        private IMarketContractService _marketContractService;
        

        public MarketContractController(IMarketContractService marketContractService)
        {
            _marketContractService = marketContractService;             
        }

        

        [HttpGet("GetAllMarketContract")]
        public ActionResult GetMarketContract()
        {
            var result = _marketContractService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByIdMarketContract")]
        public ActionResult GetByIdMarketContract(int id)
        {
            var result = _marketContractService.GetById(id);
            return Ok(result);
        }

        [HttpPost("AddMarketContract")]
        public ActionResult AddMarketContract(MarketContract marketContract)
        {
            _marketContractService.Add(marketContract);
            return Ok();
        }

        [HttpDelete("DeleteMarketContractById")]
        public ActionResult DeleteMarketContractById(int  id)
        {
            _marketContractService.DeleteById(id);
            return Ok();
        }

        [HttpPut("UpdateMarketContract")]
        public ActionResult UpdateMarketContract(MarketContract marketContract)
        {
            _marketContractService.Update(marketContract);
            return Ok();
        }
    }
}