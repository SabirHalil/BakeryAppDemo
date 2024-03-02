using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetEldenAmountController : ControllerBase
    {

        private INetEldenAmountService _netEldenAmountService;
        

        public NetEldenAmountController(INetEldenAmountService netEldenAmountService)
        {
            _netEldenAmountService = netEldenAmountService; ;            
        }

        

        [HttpGet("GetAllNetEldenAmount")]
        public ActionResult GetNetEldenAmount()
        {
            var result = _netEldenAmountService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByIdNetEldenAmount")]
        public ActionResult GetByIdNetEldenAmount(int id)
        {
            var result = _netEldenAmountService.GetById(id);
            return Ok(result);
        }

        [HttpPost("AddNetEldenAmount")]
        public ActionResult AddNetEldenAmount(NetEldenAmount netEldenAmount)
        {
            _netEldenAmountService.Add(netEldenAmount);
            return Ok();
        }

        [HttpDelete("DeleteNetEldenAmount")]
        public ActionResult DeleteNetEldenAmount(NetEldenAmount netEldenAmount)
        {
            _netEldenAmountService.Delete(netEldenAmount);
            return Ok();
        }

        [HttpPut("UpdateNetEldenAmount")]
        public ActionResult UpdateNetEldenAmount(NetEldenAmount netEldenAmount)
        {
            _netEldenAmountService.Update(netEldenAmount);
            return Ok();
        }
    }
}