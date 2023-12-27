using Business.Abstract;
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
            if(date.Date > DateTime.Now.Date)
            {
                return BadRequest("Invalid date!");
            }
            var result = _expenseService.GetExpensesByDate(date);
            return Ok(result);
        }

        [HttpPost("AddExpense")]
        public ActionResult AddExpense(Expense expense)
        {
            if (expense == null)
            {
                return BadRequest("There is no data!");
            }
            _expenseService.Add(expense);
            return Ok();
        }

        [HttpDelete("DeleteExpense")]
        public ActionResult DeleteExpense(Expense expense)
        {
            if (expense == null)
            {
                return BadRequest("There is no data!");
            }
            _expenseService.Delete(expense);
            return Ok();
        }

        [HttpPut("UpdateExpense")]
        public ActionResult UpdateExpense(Expense expense)
        {
            if (expense == null)
            {
                return BadRequest("There is no data!");
            }
            _expenseService.Update(expense);
            return Ok();
        }
    }
}