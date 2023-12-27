using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaleBreadController : ControllerBase
    {

        private IStaleBreadService _staleBreadService;


        public StaleBreadController(IStaleBreadService staleBreadService)
        {
            _staleBreadService = staleBreadService; ;
        }



        [HttpGet("GetStaleBreadListByDate")]
        public ActionResult GetStaleBreadListByDate(DateTime date)
        {
            var result = _staleBreadService.GetAllByDate(date);
            return Ok(result);
        }

        [HttpGet("GetStaleBreadDailyReport")]
        public ActionResult GetStaleBreadDailyReport(DateTime date)
        {
            var result = _staleBreadService.GetStaleBreadDailyReport(date);
            return Ok(result);
        }

        [HttpGet("GetDoughFactoryProducts")]
        public ActionResult GetDoughFactoryProducts(DateTime date)
        {
            var result = _staleBreadService.GetDoughFactoryProducts(date);
            return Ok(result);
        }



        [HttpPost("AddProduct")]
        public ActionResult AddStaleBread(StaleBread staleBread)
        {
            _staleBreadService.Add(staleBread);
            return Ok();
        }

        [HttpDelete("DeleteStaleBread")]
        public ActionResult DeleteStaleBread(int id)
        {
            _staleBreadService.DeleteById(id);
            return Ok();
        }

        [HttpPut("UpdateStaleBread")]
        public ActionResult UpdateStaleBread(StaleBread staleBread)
        {
            _staleBreadService.Update(staleBread);
            return Ok();
        }
    }
}