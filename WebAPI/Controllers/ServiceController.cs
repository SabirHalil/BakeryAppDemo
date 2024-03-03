using Business.Abstract;
using Business.Constants;
using Castle.Core.Internal;
using Entities.Concrete;
using Entities.DTOs;

using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private IMarketService _marketService;
        private IMarketContractService _marketContractService;
        private IServiceListService _serviceListService;
        private IServiceListDetailService _serviceListDetailService;

        public ServiceController(IMarketService marketService, IMarketContractService marketContractService, IServiceListService serviceListService, IServiceListDetailService serviceListDetailService)
        {
            _marketService = marketService;
            _serviceListService = serviceListService;
            _serviceListDetailService = serviceListDetailService;
            _marketContractService = marketContractService;
        }

        [HttpGet("GetByDateServiceList")]
        public ActionResult GetByDateServiceList(DateTime date)
        {
            if (date.Date > DateTime.Now.Date)
            {
                return BadRequest(Messages.WrongDate);
            }
            try
            {
                var result = _serviceListService.GetByDate(date.Date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        //[HttpPost("AddServiceListAndListDetail")]
        //public ActionResult AddService(List<ServiceListDetailDto> serviceListDetailDto, int userId)
        //{

        //    if (serviceListDetailDto.IsNullOrEmpty())
        //    {
        //        return BadRequest(Messages.ListEmpty);
        //    }

        //    try
        //    {
        //        int id = serviceListDetailDto[0].ServiceListId;
        //        bool IsNewList = false;
        //        if (id == 0)
        //        {
        //            id = _serviceListService.Add(new ServiceList { Id = 0, UserId = userId, Date = DateTime.Now });
        //            IsNewList = true;
        //        }

        //        for (int i = 0; i < serviceListDetailDto.Count; i++)
        //        {
        //            ServiceListDetail serviceListDetail = new ServiceListDetail();

        //            if (IsNewList)
        //            {
        //                serviceListDetail.ServiceListId = id;
        //            }
        //            else
        //            {
        //                serviceListDetail.ServiceListId = serviceListDetailDto[i].ServiceListId;
        //            }

        //            serviceListDetail.MarketContractId = _marketContractService.GetIdByMarketIdAndServiceProductId(serviceListDetailDto[i].MarketId, serviceListDetailDto[i].ServiceProductId);

        //            if (_serviceListDetailService.IsExist(serviceListDetail.ServiceListId, serviceListDetail.MarketContractId))
        //            {
        //                return Conflict(Messages.Conflict);
        //            }

        //            serviceListDetail.Price = _marketContractService.GetPriceById(serviceListDetail.MarketContractId);
        //            serviceListDetail.Quantity = serviceListDetailDto[i].Quantity;
        //            _serviceListDetailService.Add(serviceListDetail);
        //        }
        //        return Ok(id);

        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, e.Message);

        //    }
        //}

        [HttpPost("AddServiceListAndListDetail")]
        public ActionResult AddService(ServiceListDetailDto serviceListDetailDto, int userId)
        {

            if (serviceListDetailDto == null && userId <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                int id = serviceListDetailDto.ServiceListId;
                bool IsNewList = false;
                if (id == 0)
                {
                    id = _serviceListService.Add(new ServiceList { Id = 0, UserId = userId, Date = DateTime.Now });
                    IsNewList = true;
                }

                ServiceListDetail serviceListDetail = new ServiceListDetail();

                if (IsNewList)
                {
                    serviceListDetail.ServiceListId = id;
                }
                else
                {
                    serviceListDetail.ServiceListId = serviceListDetailDto.ServiceListId;

                    if (_serviceListDetailService.IsExist(serviceListDetail.ServiceListId, serviceListDetail.MarketContractId))
                    {
                        return Conflict(Messages.Conflict);
                    }
                }

                serviceListDetail.MarketContractId = serviceListDetailDto.MarketContractId;
                serviceListDetail.Price = _marketContractService.GetPriceById(serviceListDetail.MarketContractId);
                serviceListDetail.Quantity = serviceListDetailDto.Quantity;
                _serviceListDetailService.Add(serviceListDetail);

                return Ok(id);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);

            }
        }

        [HttpGet("GetMarketAddedToServiceList")]
        public ActionResult GetMarketAddedToServiceList(int listId)
        {
            try
            {
                return Ok(_serviceListDetailService.GetMarketAddedToServiceList(listId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetMarketNotAddedToServiceList")]
        public ActionResult GetMarketNotAddedToServiceList(int listId)
        {
            try
            {
                List<Market> allMarkets = _marketService.GetAll();
                if (listId == 0)
                {
                    return Ok(allMarkets);
                }

                List<MarketAddedToServiceDto> marketAddedToServiceDto = _serviceListDetailService.GetMarketAddedToServiceList(listId);

                List<Market> filteredMarkets = allMarkets
                                            .Where(market => !marketAddedToServiceDto.Select(dto => dto.MarketId).Contains(market.Id))
                                            .ToList();


                // Aynı tür olması için List<MarketAddedToServiceDto> e çevirdim. 
                List<MarketAddedToServiceDto> filteredMarketsDto = filteredMarkets
                    .Select(m => new MarketAddedToServiceDto
                    {
                        MarketId = m.Id,
                        MarketName = m.Name
                    })
                    .ToList();

                return Ok(filteredMarketsDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetProductsAddedToServiceListDetail")]
        public ActionResult GetProductsAddedToServiceListDetail(int listId, int marketId)
        {
            try
            {
                return Ok(_serviceListDetailService.GetProductsAddedToServiceListDetail(listId, marketId));
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetProductsNotAddedToServiceListDetail")]
        public ActionResult GetProductsNotAddedToServiceListDetail(int listId, int marketId)
        {
            try
            {
                return Ok(_serviceListDetailService.GetProductsNotAddedToServiceListDetail(listId, marketId));
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteServiceListDetail")]
        public ActionResult DeleteServiceListDetail(int serviceListId, int serviceListDetailId)
        {
            if (serviceListDetailId <= 0 && serviceListId <=0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                _serviceListDetailService.DeleteById(serviceListDetailId);
                if (!_serviceListDetailService.IsExistByServiceListId(serviceListId))
                {
                    _serviceListService.DeleteById(serviceListId);
                }
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("UpdateServiceListDetail")]
        public ActionResult UpdateServiceListDetail(int serviceListId,int serviceListDetailId, int quantity)
        {

            if (quantity < 0 && serviceListDetailId <= 0 && serviceListId <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                if (quantity == 0)
                {
                    _serviceListDetailService.DeleteById(serviceListDetailId);

                    if (! _serviceListDetailService.IsExistByServiceListId(serviceListId))
                    {
                        _serviceListService.DeleteById(serviceListId);
                    }
                    return Ok();
                }


                _serviceListDetailService.UpdateQuantity(serviceListDetailId, quantity);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


        //[HttpPut("UpdateServiceListDetail")]
        //public ActionResult UpdateServiceListDetail(ServiceListDetailDto serviceListDetailDto)
        //{
        //    if (serviceListDetailDto == null)
        //    {
        //        return BadRequest(Messages.WrongInput);
        //    }
        //    try
        //    {
        //        ServiceListDetail serviceListDetail = new();
        //        serviceListDetail.ServiceListId = serviceListDetailDto.ServiceListId;
        //        serviceListDetail.MarketContractId = _marketContractService.GetIdByMarketId(serviceListDetailDto.MarketId);

        //        if (!_serviceListDetailService.IsExist(serviceListDetail.ServiceListId, serviceListDetail.MarketContractId))
        //        {
        //            return Conflict(Messages.WrongInput);
        //        }

        //        serviceListDetail.Quantity = serviceListDetailDto.Quantity;
        //        serviceListDetail.Price = _marketContractService.GetPriceById(serviceListDetail.MarketContractId);
        //        serviceListDetail.Id = _serviceListDetailService.GetIdByServiceListIdAndMarketContracId(serviceListDetail.ServiceListId, serviceListDetail.MarketContractId);
        //        _serviceListDetailService.Update(serviceListDetail);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, e.Message);
        //    }
        //}

        public class DataForDelete
        {

            public int ServiceListId { get; set; }
            public int MarketId { get; set; }
        }




    }
}
