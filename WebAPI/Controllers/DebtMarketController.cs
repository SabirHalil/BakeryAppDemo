using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtMarketController : ControllerBase
    {

        private IDebtMarketService _debtMarketService;


        public DebtMarketController(IDebtMarketService debtMarketService)
        {
            _debtMarketService = debtMarketService;
        }



        [HttpGet("GetAllDebtMarket")]
        public ActionResult GetDebtMarket()
        {

            try
            {
                var result = _debtMarketService.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetDebtByMarketId")]
        public ActionResult GetDebtByMarketId(int marketId)
        {


            try
            {

                var result = _debtMarketService.GetDebtByMarketId(marketId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetById")]
        public ActionResult GetById(int id)
        {

            try
            {
                if (id <= 0)
                {

                    return BadRequest(Messages.WrongInput);
                }
                var result = _debtMarketService.GetById(id);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }



        }

        [HttpPost("AddNewDebt")]
        public ActionResult AddNewDebt(DebtMarket debtMarket)
        {
            try
            {
                if (debtMarket == null || debtMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
                _debtMarketService.Add(debtMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


        }

        [HttpPost("PayDebt")]
        public ActionResult PayDebt(DebtMarket debtMarket)
        {
            try
            {
                if (debtMarket == null || debtMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
                debtMarket.Amount *= -1;
                _debtMarketService.Add(debtMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteDebtMarket")]
        public ActionResult DeleteDebtMarket(DebtMarket debtMarket)
        {
            try
            {
                if (debtMarket == null || debtMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
                _debtMarketService.Delete(debtMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateDebtMarket")]
        public ActionResult UpdateDebtMarket(DebtMarket debtMarket)
        {
            try
            {
                if (debtMarket == null || debtMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
                if (_debtMarketService.GetById(debtMarket.Id).Amount < 0)
                {
                    debtMarket.Amount *= -1;
                }
                _debtMarketService.Update(debtMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}