using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardAmountController : ControllerBase
    {

        private ICreditCardAmountService _creditCardAmountService;
        

        public CreditCardAmountController(ICreditCardAmountService creditCardAmountService)
        {
            _creditCardAmountService = creditCardAmountService; ;            
        }

        

        [HttpGet("GetAllCreditCardAmount")]
        public ActionResult GetCreditCardAmount()
        {
            var result = _creditCardAmountService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByIdCreditCardAmount")]
        public ActionResult GetByIdCreditCardAmount(int id)
        {
            var result = _creditCardAmountService.GetById(id);
            return Ok(result);
        }

        [HttpPost("AddCreditCardAmount")]
        public ActionResult AddCreditCardAmount(CreditCardAmount creditCardAmount)
        {
            _creditCardAmountService.Add(creditCardAmount);
            return Ok();
        }

        [HttpDelete("DeleteCreditCardAmount")]
        public ActionResult DeleteCreditCardAmount(CreditCardAmount creditCardAmount)
        {
            _creditCardAmountService.Delete(creditCardAmount);
            return Ok();
        }

        [HttpPut("UpdateCreditCardAmount")]
        public ActionResult UpdateCreditCardAmount(CreditCardAmount creditCardAmount)
        {
            _creditCardAmountService.Update(creditCardAmount);
            return Ok();
        }
    }
}