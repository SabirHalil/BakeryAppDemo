using Business.Abstract;
using Business.Constants;
using Entities.Concrete;

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
            try
            {
                var result = _staleBreadService.GetAllByDate(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetStaleBreadDailyReport")]
        public ActionResult GetStaleBreadDailyReport(DateTime date)
        {
            try
            {
                var result = _staleBreadService.GetStaleBreadDailyReport(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetDoughFactoryProducts")]
        public ActionResult GetDoughFactoryProducts(DateTime date)
        {
            try
            {
                var result = _staleBreadService.GetDoughFactoryProducts(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }



        [HttpPost("AddStaleBread")]
        public ActionResult AddStaleBread(StaleBread staleBread)
        {

            try
            {
                if (_staleBreadService.IsExist(staleBread.DoughFactoryProductId, staleBread.Date))
                {
                    return BadRequest(Messages.OncePerDay);
                }
                if (staleBread == null || staleBread.Quantity < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }


                _staleBreadService.Add(staleBread);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteStaleBread")]
        public ActionResult DeleteStaleBread(int id)
        {
            try
            {
                _staleBreadService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateStaleBread")]
        public ActionResult UpdateStaleBread(StaleBread staleBread)
        {
            try
            {
                _staleBreadService.Update(staleBread);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}