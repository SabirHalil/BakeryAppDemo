using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceivedMoneyFromServiceController : ControllerBase
    {

        private IReceivedMoneyFromServiceService _receivedMoneyFromServiceService;
        public ReceivedMoneyFromServiceController(IReceivedMoneyFromServiceService receivedMoneyFromServiceService)
        {
            _receivedMoneyFromServiceService = receivedMoneyFromServiceService;
        }

        [HttpGet("GetAllReceivedMoneyFromService")]
        public ActionResult GetAllReceivedMoneyFromService()
        {
            var result = _receivedMoneyFromServiceService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetReceivedMoneyFromServiceByDateAndServiceType")]
        public ActionResult GetAllReceivedMoneyFromServiceByDate(DateTime date,int serviceType)
        {
            var result = _receivedMoneyFromServiceService.GetByDate(date,serviceType);
            Console.WriteLine("Result of receive money: "+result);
            if (result != null)
            {
                return Ok(result);

            }
            return NoContent();
        }


        [HttpPost("AddReceivedMoneyFromService")]
        public ActionResult AddReceivedMoneyFromService(ReceivedMoneyFromService receivedMoneyFromService)
        {
            if (receivedMoneyFromService == null)
            {
                return BadRequest("There is no data!");
            }
            var isAdded = _receivedMoneyFromServiceService.GetByDate(receivedMoneyFromService.Date, receivedMoneyFromService.ServiceTypeId);
            if (isAdded != null)
            {
                return BadRequest("Already Added!");
            }

            _receivedMoneyFromServiceService.Add(receivedMoneyFromService);
            return Ok();
        }

        [HttpDelete("DeleteReceivedMoneyFromServiceById")]
        public ActionResult DeleteReceivedMoneyFromService(int id)
        {
            if (id == 0)
            {
                return BadRequest("There is no data!");
            }
            _receivedMoneyFromServiceService.DeleteById(id);
            return Ok();
        }

        [HttpPut("UpdateReceivedMoneyFromService")]
        public ActionResult UpdateReceivedMoneyFromService(ReceivedMoneyFromService receivedMoneyFromService)
        {
            if (receivedMoneyFromService == null)
            {
                return BadRequest("There is no data!");
            }
            _receivedMoneyFromServiceService.Update(receivedMoneyFromService);
            return Ok();
        }
    }
}
