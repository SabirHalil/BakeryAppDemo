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

        [HttpPost("AddServiceListAndListDetail")]
        public ActionResult AddService(List<ServiceListDetailDto> serviceListDetailDto, int userId)
        {

            if (serviceListDetailDto.IsNullOrEmpty())
            {
                return BadRequest(Messages.ListEmpty);
            }

            try
            {
                int id = 0;
                bool IsNewList = false;
                if (serviceListDetailDto[0].ServiceListId == 0)
                {
                    id = _serviceListService.Add(new ServiceList { Id = 0, UserId = userId, Date = DateTime.Now });
                    IsNewList = true;
                }

                for (int i = 0; i < serviceListDetailDto.Count; i++)
                {
                    ServiceListDetail serviceListDetail = new ServiceListDetail();

                    if (IsNewList)
                    {
                        serviceListDetail.ServiceListId = id;
                    }
                    else
                    {

                        serviceListDetail.ServiceListId = serviceListDetailDto[i].ServiceListId;

                        //    //if (_serviceListDetailService.IsExist(serviceListDetail[i].ServiceProductId))
                        //    //{
                        //    //    return Conflict(Messages.Conflict);
                        //    //}
                        //    //else
                        //    //{

                        //    // }   
                    }

                    serviceListDetail.MarketContractId = _marketContractService.GetIdByMarketId(serviceListDetailDto[i].MarketId);
                    serviceListDetail.Price = _marketContractService.GetPriceById(serviceListDetail.MarketContractId);
                    serviceListDetail.Quantity = serviceListDetailDto[i].Quantity;
                    _serviceListDetailService.Add(serviceListDetail);
                }
                return new ObjectResult(new { message = Messages.Created })
                {
                    StatusCode = 201
                };

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);

            }
        }

        [HttpGet("GetAddedMarketByServiceListId")]
        public ActionResult GetAddedMarketByServiceListId(int listId)
        {
            try
            {
                List<ServiceListDetail> serviceListDetail = _serviceListDetailService.GetByListId(listId);

                List<GetAddedServiceListDetailDto> List = new List<GetAddedServiceListDetailDto>();

                for (int i = 0; i < serviceListDetail.Count; i++)
                {
                    GetAddedServiceListDetailDto addedServiceListDetailDto = new();
                    addedServiceListDetailDto.ServiceListId = serviceListDetail[i].ServiceListId;
                    addedServiceListDetailDto.Quantity = serviceListDetail[i].Quantity;

                    addedServiceListDetailDto.MarketId = _marketContractService.GetMarketIdById(serviceListDetail[i].MarketContractId);

                    addedServiceListDetailDto.MarketName = _marketService.GetNameById(addedServiceListDetailDto.MarketId);

                    List.Add(addedServiceListDetailDto);
                }

                return Ok(List);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetMarketByServiceListId")]
        public ActionResult GetMarketByServiceListId(int listId)
        {


            try
            {
                if (listId == 0) {

                    List<Market> allMarkets0 = _marketService.GetAll();


                    List<GetServiceListDetailDto> serviceListDetailDto0 = new List<GetServiceListDetailDto>();
                    for (int i = 0; i < allMarkets0.Count; i++)
                    {
                        GetServiceListDetailDto service = new GetServiceListDetailDto();
                        service.MarketId = allMarkets0[i].Id;
                        service.MarketName = allMarkets0[i].Name;
                        service.ServiceListId = listId;
                    }
                }

                List<ServiceListDetail> serviceListDetail = _serviceListDetailService.GetByListId(listId);
                List<int> MarketIds = new List<int>();

                for (int i = 0; i < serviceListDetail.Count; i++)
                {
                    MarketIds.Add(_marketContractService.GetMarketIdById(serviceListDetail[i].MarketContractId));
                }

                List<Market> allMarkets = _marketService.GetAll();
                // LINQ kullanarak filtreleme
                List<Market> filteredMarkets = allMarkets.Where(m => !MarketIds.Contains(m.Id)).ToList();


                List<GetServiceListDetailDto> serviceListDetailDto = new List<GetServiceListDetailDto>();
                for (int i = 0; i < filteredMarkets.Count; i++)
                {
                    GetServiceListDetailDto service = new GetServiceListDetailDto();
                    service.MarketId = filteredMarkets[i].Id;
                    service.MarketName = filteredMarkets[i].Name;
                    service.ServiceListId = listId;
                    serviceListDetailDto.Add(service);
                }


                return Ok(serviceListDetailDto);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteServiceListDetail")]
        public ActionResult DeleteServiceListDetail(DataForDelete dataForDelete)
        {
            if (dataForDelete.ServiceListId <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                _serviceListDetailService.DeleteByServiceListIdAndMarketContracId(dataForDelete.ServiceListId, _marketContractService.GetIdByMarketId(dataForDelete.MarketId));               
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("UpdateServiceListDetail")]
        public ActionResult UpdateServiceListDetail(ServiceListDetailDto serviceListDetailDto)
        {
            if (serviceListDetailDto == null)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                ServiceListDetail serviceListDetail = new();
                serviceListDetail.ServiceListId = serviceListDetailDto.ServiceListId;
                serviceListDetail.Quantity = serviceListDetailDto.Quantity;
                serviceListDetail.MarketContractId = _marketContractService.GetIdByMarketId(serviceListDetailDto.MarketId);
                serviceListDetail.Price = _marketContractService.GetPriceById(serviceListDetail.MarketContractId);
                serviceListDetail.Id = _serviceListDetailService.GetIdByServiceListIdAndMarketContracId(serviceListDetail.ServiceListId, serviceListDetail.MarketContractId);
                _serviceListDetailService.Update(serviceListDetail);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        public class DataForDelete {

            public int ServiceListId { get; set; }
            public int MarketId { get; set; }
        }




    }
}
