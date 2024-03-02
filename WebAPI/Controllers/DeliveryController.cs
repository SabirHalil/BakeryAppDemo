using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {

        private IDeliveryService _deliveryService;
        

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService; ;            
        }

        

        [HttpGet("GetPaymentAmountDue")]
        public ActionResult GetPaymentAmountDue(DateTime date)
        {
            var result = _deliveryService.PaymentAmountDue(date);
            return Ok(result);
        }

        [HttpGet("GetBetweenDates")]
        public ActionResult GetBetweenDates(DateTime startDate, DateTime endDate)
        {
            var result = _deliveryService.GetBetweenDates(startDate,endDate);
            return Ok(result);
        }
        
        [HttpGet("GetAllDelivery")]
        public ActionResult GetAllDelivery()
        {
            var result = _deliveryService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetByIdDelivery")]
        public ActionResult GetByIdDelivery(int id)
        {
            var result = _deliveryService.GetById(id);
            return Ok(result);
        }

        [HttpPost("AddDelivery")]
        public ActionResult AddDelivery(Delivery delivery)
        {
            _deliveryService.Add(delivery);
            return Ok();
        }

        [HttpDelete("DeleteDelivery")]
        public ActionResult DeleteDelivery(Delivery delivery)
        {
            _deliveryService.Delete(delivery);
            return Ok();
        }

        [HttpPut("UpdateDelivery")]
        public ActionResult UpdateDelivery(Delivery delivery)
        {
            _deliveryService.Update(delivery);
            return Ok();
        }
    }
}