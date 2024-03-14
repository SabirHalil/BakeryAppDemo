using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketEndOfDayAccountController : ControllerBase
    {
        private IMarketContractService _marketContractService;
        private IMarketEndOfDayService _marketEndOfDayService;
        public MarketEndOfDayAccountController(IMarketEndOfDayService marketEndOfDayService,
            IMarketContractService marketContractService)
        {
            _marketEndOfDayService = marketEndOfDayService;
            _marketContractService = marketContractService;
        }



        [HttpGet("GetMarketEndOfDayAccount")]
        public ActionResult GetMarketEndOfDayAccount(int marketId, DateTime date)
        {

            try
            {             
                return Ok(_marketEndOfDayService.MarketEndOfDayAccount(date, marketId));
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetMarketEndOfDayAccountDetail")]
        public ActionResult GetMarketEndOfDayAccountDetail(int marketId, DateTime date)
        {

            try
            {            
                return Ok(_marketEndOfDayService.MarketEndOfDayAccountDetail(date, marketId));
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        //[HttpGet("GetMarketEndOfDayAccount")]
        //public ActionResult GetMarketEndOfDayAccount(int marketId, DateTime date)
        //{

        //    try
        //    {
        //        decimal TotalAmount = 0;

        //        var result = _marketEndOfDayService.MarketEndOfDayDetail(date, marketId);

        //        var ServiceProducts = _marketContractService.ServiceProductsIdsByMarketId(marketId);

        //        for (int i = 0; i < ServiceProducts.Count; i++)
        //        {
        //            List<ServiceProductInfo> product = new();

        //            foreach (var serviceDetail in result.serviceDetail)
        //            {
        //                ServiceProductInfo serviceProductInfo = serviceDetail.Value.FirstOrDefault(s => s.ServiceProductId == ServiceProducts[i]);
        //                if (serviceProductInfo != null)
        //                {
        //                    product.Add(serviceProductInfo);
        //                }
        //            }

        //            int total = 0;
        //            decimal price = product[0].Price;

        //            for (int s = 0; s < product.Count; s++)
        //            {
        //                total += product[s].Quantity;
        //            }

        //            var stale = result.staleProductsReceivedFromMarkets.FirstOrDefault(s => s.ServiceProductId == ServiceProducts[i]);
        //            if (stale != null)
        //            {
        //                total -= stale.Quantity;
        //            }

        //            TotalAmount += (total * price);
        //        }

        //        return Ok(TotalAmount);
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, e.Message);
        //    }

        //}

        [HttpGet("GetMarketEndOfDayDetail")]
        public ActionResult GetMarketEndOfDayDetail(int marketId, DateTime date)
        {

            try
            {
                var result = _marketEndOfDayService.MarketEndOfDayDetail(date, marketId);

                return Ok(new { StaleProducts = result.staleProductsReceivedFromMarkets, ServiceDetail = result.serviceDetail });
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        private class MarketEndOfDayAccountDetail
        {
            public List<MarketEndOfDay> marketEndOfDay;
            public decimal TotalDebt;
        }
        private class MarketEndOfDay
        {
            public int ProductId;
            public int TotalGivenAmount;
            public int StaleAmount;
        }

    }
}
