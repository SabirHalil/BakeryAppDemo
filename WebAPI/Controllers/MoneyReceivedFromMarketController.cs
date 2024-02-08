using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyReceivedFromMarketController : ControllerBase
    {
        private IMarketService _marketService;
        private IMarketContractService _marketContractService;
        private IMoneyReceivedFromMarketService _moneyReceivedFromMarketService;
        private IServiceListDetailService _serviceListDetailService;
        private IServiceListService _serviceListService;
        private IStaleBreadReceivedFromMarketService _staleBreadReceivedFromMarketService;
        private IDebtMarketService _debtMarketService;

        private IMarketEndOfDayService _marketEndOfDayService;


        public MoneyReceivedFromMarketController(IMarketEndOfDayService marketEndOfDayService, IDebtMarketService debtMarketService, IStaleBreadReceivedFromMarketService staleBreadReceivedFromMarketService, IMarketService marketService, IMarketContractService marketContractService, IMoneyReceivedFromMarketService moneyReceivedFromMarketService, IServiceListService serviceListService, IServiceListDetailService serviceListDetailService)
        {
            _marketEndOfDayService = marketEndOfDayService;
            _debtMarketService = debtMarketService;
            _moneyReceivedFromMarketService = moneyReceivedFromMarketService;
            _serviceListDetailService = serviceListDetailService;
            _serviceListService = serviceListService;
            _marketService = marketService;
            _marketContractService = marketContractService;
            _staleBreadReceivedFromMarketService = staleBreadReceivedFromMarketService;
        }



        [HttpGet("GetMoneyReceivedFromMarketByMarketId")]
        public ActionResult GetMoneyReceivedFromMarketByMarketId(int marketId, DateTime date)
        {

            try
            {
                //var result = _moneyReceivedFromMarketService.GetByMarketId(marketId);
                var result = _moneyReceivedFromMarketService.GetByMarketIdAndDate(marketId, date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetMoneyReceivedMarketListByDate")]
        public ActionResult GetMoneyReceivedMarketListByDate(DateTime date)
        {
            try
            {
                return Ok(_marketEndOfDayService.CalculateMarketEndOfDay(date));
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
        [HttpGet("GetMarketsEndOfDayCalculationWithDetail")]
        public ActionResult GetMarketsEndOfDayCalculationWithDetail(DateTime date)
        {
            try
            {
                return Ok(_marketEndOfDayService.MarketsEndOfDayCalculationWithDetail(date));
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }


        [HttpGet("GetNotMoneyReceivedMarketListByDate")]
        public ActionResult GetNotMoneyReceivedMarketListByDate(DateTime date)
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

                List<MoneyReceivedFromMarket> moneyReceivedFromMarkets = _moneyReceivedFromMarketService.GetByDate(date);

                List<int> filteredMarkets = MarketIds.Except(moneyReceivedFromMarkets.Select(m => m.MarketId)).ToList();

                List<NotPaymentMarket> NotPaymentMarkets = new();

                for (int i = 0; i < filteredMarkets.Count; i++)
                {
                    NotPaymentMarket notPaymentMarket = new();
                    notPaymentMarket.MarketId = filteredMarkets[i];
                    notPaymentMarket.MarketName = _marketService.GetNameById(filteredMarkets[i]);

                    var result = CalculateTotalAmountAndBread(date, filteredMarkets[i]);
                    
                    notPaymentMarket.TotalAmount = result.TotalAmount;
                    notPaymentMarket.GivenBread = result.TotalBread;
                    notPaymentMarket.StaleBread = _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketId(notPaymentMarket.MarketId, date);
                    NotPaymentMarkets.Add(notPaymentMarket);
                }

                return Ok(NotPaymentMarkets);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("AddMoneyReceivedFromMarket")]
        public ActionResult AddMoneyReceivedFromMarket(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            try
            {
                if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                if (_moneyReceivedFromMarketService.IsExist(moneyReceivedFromMarket.MarketId, moneyReceivedFromMarket.Date))
                {

                    return BadRequest(Messages.Conflict);
                }

                
                var result = CalculateTotalAmountAndBread(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);
                decimal totalAmount = result.TotalAmount;
                if (totalAmount < moneyReceivedFromMarket.Amount)
                {
                    return BadRequest(Messages.InvalidAmount);
                }
                if (totalAmount > moneyReceivedFromMarket.Amount)
                {
                    _debtMarketService.Add(new DebtMarket
                    {
                        Amount = (totalAmount - moneyReceivedFromMarket.Amount),
                        Date = moneyReceivedFromMarket.Date,
                        MarketId = moneyReceivedFromMarket.MarketId,
                    });
                }

                _moneyReceivedFromMarketService.Add(moneyReceivedFromMarket);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

            return Ok();
        }

        [HttpDelete("DeleteMoneyReceivedFromMarket")]
        public ActionResult DeleteMoneyReceivedFromMarket(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            try
            {
                if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }


                int debtId = _debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);
          
                if(debtId != 0)
                {
                    _debtMarketService.DeleteById(debtId);
                }

                _moneyReceivedFromMarketService.DeleteById(moneyReceivedFromMarket.Id);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


            return Ok();
        }

        [HttpPut("UpdateMoneyReceivedFromMarket")]
        public ActionResult UpdateMoneyReceivedFromMarket(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            try
            {
                if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                if (!_moneyReceivedFromMarketService.IsExist(moneyReceivedFromMarket.MarketId, moneyReceivedFromMarket.Date))
                {

                    return BadRequest(Messages.WrongInput);
                }

                var result = CalculateTotalAmountAndBread(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);
                decimal totalAmount = result.TotalAmount;
                if (totalAmount < moneyReceivedFromMarket.Amount)
                {
                    return BadRequest(Messages.InvalidAmount);
                }
                if (totalAmount > moneyReceivedFromMarket.Amount)
                {
                    if (_debtMarketService.IsExist(_debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)))
                    {
                        _debtMarketService.Update(new DebtMarket
                        {
                            Amount = (totalAmount - moneyReceivedFromMarket.Amount),
                            Date = moneyReceivedFromMarket.Date,
                            MarketId = moneyReceivedFromMarket.MarketId,
                            Id = _debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)
                        });
                    }
                    else
                    {
                        _debtMarketService.Add(new DebtMarket
                        {
                            Amount = (totalAmount - moneyReceivedFromMarket.Amount),
                            Date = moneyReceivedFromMarket.Date,
                            MarketId = moneyReceivedFromMarket.MarketId,
                        });
                    }
                }
                if (totalAmount == moneyReceivedFromMarket.Amount)
                {
                    _debtMarketService.Delete(new DebtMarket
                    {
                        Date = moneyReceivedFromMarket.Date,
                        MarketId = moneyReceivedFromMarket.MarketId,
                        Id = _debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)
                    });
                }
                _moneyReceivedFromMarketService.Update(moneyReceivedFromMarket);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


            return Ok();
        }

        private (decimal TotalAmount, int TotalBread) CalculateTotalAmountAndBread(DateTime date, int marketId)
        {

            List<ServiceList> serviceLists = _serviceListService.GetByDate(date);

            int TotalBread = 0;
            decimal Price = 0;
            for (int i = 0; i < serviceLists.Count; i++)
            {

                ServiceListDetail serviceListDetail = _serviceListDetailService.GetByServiceListIdAndMarketContractId(serviceLists[i].Id, _marketContractService.GetIdByMarketId(marketId));
                if (serviceListDetail != null)
                {
                    TotalBread += serviceListDetail.Quantity;
                    Price = serviceListDetail.Price;
                }
            }

            int StaleBreadCount = _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketId(marketId, date);

            decimal TotalAmount = (TotalBread - StaleBreadCount) * Price;


            return (TotalAmount, TotalBread);
        }

    }





}