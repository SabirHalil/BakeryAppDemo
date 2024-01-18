using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {

        private IExpenseService _expenseService;


        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService; ;
        }

        [HttpGet("GetExpensesByDate")]
        public ActionResult GetExpense(DateTime date)
        {
            try
            {
                if (date.Date > DateTime.Now.Date)
                {
                    return BadRequest("Invalid date!");
                }
                var result = _expenseService.GetExpensesByDate(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpPost("AddExpense")]
        public ActionResult AddExpense(Expense expense)
        {
            try
            {
                if (expense == null)
                {
                    return BadRequest("There is no data!");
                }
                _expenseService.Add(expense);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpDelete("DeleteExpense")]
        public ActionResult DeleteExpense(int  id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
                _expenseService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpPut("UpdateExpense")]
        public ActionResult UpdateExpense(Expense expense)
        {
            try
            {
                if (expense == null)
                {
                    return BadRequest("There is no data!");
                }
                _expenseService.Update(expense);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }
    }
}