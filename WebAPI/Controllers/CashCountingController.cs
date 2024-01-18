using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashCountingController : ControllerBase
    {

        private ICashCountingService _cashCountingService;


        public CashCountingController(ICashCountingService cashCountingService)
        {
            _cashCountingService = cashCountingService; ;
        }


        [HttpGet("GetCashCountingByDate")]
        public ActionResult GetCashCountingByDate(DateTime date)
        {
            try
            {
                var result = _cashCountingService.GetOneCashCountingByDate(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddCashCounting")]
        public ActionResult AddCashCounting(CashCounting cashCounting)
        {
            if (cashCounting == null || cashCounting.TotalMoney < 0 || cashCounting.RemainedMoney < 0)
            {
                return BadRequest(Messages.WrongInput);
            }


            try
            {
                if (_cashCountingService.GetOneCashCountingByDate(cashCounting.Date) != null)
                {
                    return BadRequest(Messages.OncePerDay);
                }
                _cashCountingService.Add(cashCounting);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteCashCountingById")]
        public ActionResult DeleteCashCountingById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                _cashCountingService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateCashCounting")]
        public ActionResult UpdateCashCounting(CashCounting cashCounting)
        {
            if (cashCounting == null || cashCounting.TotalMoney < 0 || cashCounting.RemainedMoney < 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                if (cashCounting.TotalMoney == 0 && cashCounting.RemainedMoney == 0)
                {
                    _cashCountingService.DeleteById(cashCounting.Id);
                }
                else
                {
                    _cashCountingService.Update(cashCounting);
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