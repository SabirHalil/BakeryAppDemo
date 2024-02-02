using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaleBreadReceivedFromMarketController : ControllerBase
    {

        private IStaleBreadReceivedFromMarketService _staleBreadReceivedFromMarketService;

        private IMarketContractService _marketContractService;
        private IServiceListDetailService _serviceListDetailService;
        private IServiceListService _serviceListService;
        private IMarketService _marketService;

        public StaleBreadReceivedFromMarketController(IMarketService marketService, IMarketContractService marketContractService, IServiceListService serviceListService, IServiceListDetailService serviceListDetailService, IStaleBreadReceivedFromMarketService staleBreadReceivedFromMarketService)
        {
            _staleBreadReceivedFromMarketService = staleBreadReceivedFromMarketService;

            _serviceListService = serviceListService;
            _serviceListDetailService = serviceListDetailService;
            _marketContractService = marketContractService;
            _marketService = marketService;
        }

        [HttpGet("GetStaleBreadReceivedFromMarketByDate")]
        public ActionResult GetStaleBreadReceivedFromMarketByDate(DateTime date)
        {
            try
            {
                List<StaleBreadReceivedFromMarket> staleBreadReceivedFromMarket = _staleBreadReceivedFromMarketService.GetByDate(date);
                List<StaleBreadReceivedFromMarketDto> staleBreadReceivedFromMarketDto = staleBreadReceivedFromMarket
                            .Select(item => new StaleBreadReceivedFromMarketDto
                            {
                                id = item.Id,
                                MarketId = item.MarketId,
                                MarketName = _marketService.GetNameById(item.MarketId),
                                Quantity = item.Quantity
                            })
                                .ToList();


                return Ok(staleBreadReceivedFromMarketDto);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("GetNoBreadReceivedMarketListByDate")]
        public ActionResult GetNoMoneyReceivedMarketListByDate(DateTime date)
        {
            try
            {
                List<int> MarketIds = new List<int>();
                List<ServiceList> serviceList = _serviceListService.GetByDate(date);

                for (int i = 0; i < serviceList.Count; i++)
                {
                    List<ServiceListDetail> serviceListDetail = _serviceListDetailService.GetByListId(serviceList[i].Id);

                    for (int j = 0; j < serviceListDetail.Count; j++)
                    {

                        var newMarketId = _marketContractService.GetMarketIdById(serviceListDetail[j].MarketContractId);
                        if (!MarketIds.Contains(newMarketId))
                        {
                            MarketIds.Add(newMarketId);
                        }
                    }
                }

                List<StaleBreadReceivedFromMarket> staleBreadReceivedFromMarket = _staleBreadReceivedFromMarketService.GetByDate(date);

                List<int> filteredMarkets = MarketIds.Except(staleBreadReceivedFromMarket.Select(s => s.MarketId)).ToList();

                List<NoStaleBreadReceivedFromMarketDto> noStaleBreadReceivedFromMarketDto = new();

                for (int i = 0; i < filteredMarkets.Count; i++)
                {
                    NoStaleBreadReceivedFromMarketDto s = new();
                    s.MarketId = filteredMarkets[i];
                    s.MarketName = _marketService.GetNameById(filteredMarkets[i]);
                    noStaleBreadReceivedFromMarketDto.Add(s);
                }

               return Ok(noStaleBreadReceivedFromMarketDto);
               
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetStaleBreadReceivedFromMarketByMarketId")]
        public ActionResult GetStaleBreadReceivedFromMarket(int marketId, DateTime date)
        {
            try
            {
                var result = _staleBreadReceivedFromMarketService.GetByMarketId(marketId, date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetByIdStaleBreadReceivedFromMarket")]
        public ActionResult GetByIdStaleBreadReceivedFromMarket(int id)
        {

            try
            {
                var result = _staleBreadReceivedFromMarketService.GetById(id);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("AddStaleBreadReceivedFromMarket")]
        public ActionResult AddStaleBreadReceivedFromMarket(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {
            if (staleBreadReceivedFromMarket == null || staleBreadReceivedFromMarket.Quantity < 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            
            if (_staleBreadReceivedFromMarketService.IsExist(staleBreadReceivedFromMarket.MarketId,staleBreadReceivedFromMarket.Date))
            {
                return BadRequest(Messages.Conflict);
            }

            try
            {
                _staleBreadReceivedFromMarketService.Add(staleBreadReceivedFromMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteStaleBreadReceivedFromMarketById")]
        public ActionResult DeleteStaleBreadReceivedFromMarket(int id)
        {

            try
            {
                _staleBreadReceivedFromMarketService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteByDateAndMarketId")]
        public ActionResult DeleteByDateAndMarketId(DataForDeleteForStaleBreadReceivedFromMarket dataForDeleteForStaleBreadReceivedFromMarket)
        {

            try
            {
                _staleBreadReceivedFromMarketService.DeleteByDateAndMarketId(dataForDeleteForStaleBreadReceivedFromMarket.Date, dataForDeleteForStaleBreadReceivedFromMarket.MarketId);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("UpdateStaleBreadReceivedFromMarket")]
        public ActionResult UpdateStaleBreadReceivedFromMarket(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {


            try
            {
                _staleBreadReceivedFromMarketService.Update(staleBreadReceivedFromMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        public class DataForDeleteForStaleBreadReceivedFromMarket
        {
            public DateTime Date { get; set; }
            public int MarketId { get; set; }
        }

        public class StaleBreadReceivedFromMarketDto
        {
            public int id { get; set; }
            public int MarketId { get; set; }
            public string MarketName { get; set; }
            public int Quantity { get; set; }
        }
        class NoStaleBreadReceivedFromMarketDto

        {
            public int MarketId { get; set; }
            public string MarketName { get; set; }
           
        }
    }
}