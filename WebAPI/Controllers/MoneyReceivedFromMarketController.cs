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


        [HttpGet("GetMoneyReceivedMarketListByDate")]
        public ActionResult GetMoneyReceivedMarketListByDate(DateTime date)
        {
            try
            {
                var moneyReceivedMarket = _moneyReceivedFromMarketService.GetMoneyReceivedMarketListByDate(date);

                foreach (var market in moneyReceivedMarket)
                {
                    market.TotalAmount = _marketEndOfDayService.MarketEndOfDayAccount(date, market.MarketId);
                }

                return Ok(moneyReceivedMarket);
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
                return Ok(_moneyReceivedFromMarketService.GetNotMoneyReceivedMarketListByDate(date));
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }




        [HttpPost("AddMoneyReceivedFromMarket")]
        public ActionResult AddMoneyReceivedFromMarket(MoneyReceivedFromMarketDto moneyReceivedFromMarket)
        {
            try
            {
                if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.ReceivedAmount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                if (_moneyReceivedFromMarketService.IsExist(moneyReceivedFromMarket.MarketId, moneyReceivedFromMarket.Date))
                {

                    return BadRequest(Messages.Conflict);
                }


                if (moneyReceivedFromMarket.TotalAmount < moneyReceivedFromMarket.ReceivedAmount)
                {
                    return BadRequest(Messages.InvalidAmount);
                }
                if (moneyReceivedFromMarket.TotalAmount > moneyReceivedFromMarket.ReceivedAmount)
                {
                    _debtMarketService.Add(new DebtMarket
                    {
                        Amount = (moneyReceivedFromMarket.TotalAmount - moneyReceivedFromMarket.ReceivedAmount),
                        Date = moneyReceivedFromMarket.Date,
                        MarketId = moneyReceivedFromMarket.MarketId,
                    });
                }


                _moneyReceivedFromMarketService.Add(new MoneyReceivedFromMarket
                {
                    MarketId = moneyReceivedFromMarket.MarketId,
                    Date = moneyReceivedFromMarket.Date,
                    Amount = moneyReceivedFromMarket.ReceivedAmount
                });
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

            return Ok();
        }



        //[HttpPost("AddMoneyReceivedFromMarket")]
        //public ActionResult AddMoneyReceivedFromMarket(MoneyReceivedFromMarket moneyReceivedFromMarket)
        //{
        //    try
        //    {
        //        if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.Amount < 0)
        //        {
        //            return BadRequest(Messages.WrongInput);
        //        }

        //        if (_moneyReceivedFromMarketService.IsExist(moneyReceivedFromMarket.MarketId, moneyReceivedFromMarket.Date))
        //        {

        //            return BadRequest(Messages.Conflict);
        //        }


        //        var result = CalculateTotalAmountAndBread(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);
        //        decimal totalAmount = result.TotalAmount;
        //        if (totalAmount < moneyReceivedFromMarket.Amount)
        //        {
        //            return BadRequest(Messages.InvalidAmount);
        //        }
        //        if (totalAmount > moneyReceivedFromMarket.Amount)
        //        {
        //            _debtMarketService.Add(new DebtMarket
        //            {
        //                Amount = (totalAmount - moneyReceivedFromMarket.Amount),
        //                Date = moneyReceivedFromMarket.Date,
        //                MarketId = moneyReceivedFromMarket.MarketId,
        //            });
        //        }

        //        _moneyReceivedFromMarketService.Add(moneyReceivedFromMarket);
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, e.Message);
        //    }

        //    return Ok();
        //}



        //[HttpDelete("DeleteMoneyReceivedFromMarket")]
        //public ActionResult DeleteMoneyReceivedFromMarket(MoneyReceivedFromMarket moneyReceivedFromMarket)
        //{
        //    try
        //    {
        //        if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.Amount < 0)
        //        {
        //            return BadRequest(Messages.WrongInput);
        //        }


        //        int debtId = _debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);

        //        if (debtId != 0)
        //        {
        //            _debtMarketService.DeleteById(debtId);
        //        }

        //        _moneyReceivedFromMarketService.DeleteById(moneyReceivedFromMarket.Id);
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, e.Message);
        //    }


        //    return Ok();
        //}

        //[HttpPut("UpdateMoneyReceivedFromMarket")]
        //public ActionResult UpdateMoneyReceivedFromMarket(MoneyReceivedFromMarket moneyReceivedFromMarket)
        //{
        //    try
        //    {
        //        if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.Amount < 0)
        //        {
        //            return BadRequest(Messages.WrongInput);
        //        }

        //        if (!_moneyReceivedFromMarketService.IsExist(moneyReceivedFromMarket.MarketId, moneyReceivedFromMarket.Date))
        //        {

        //            return BadRequest(Messages.WrongInput);
        //        }

        //        var result = CalculateTotalAmountAndBread(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);
        //        decimal totalAmount = result.TotalAmount;
        //        if (totalAmount < moneyReceivedFromMarket.Amount)
        //        {
        //            return BadRequest(Messages.InvalidAmount);
        //        }
        //        if (totalAmount > moneyReceivedFromMarket.Amount)
        //        {
        //            if (_debtMarketService.IsExist(_debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)))
        //            {
        //                _debtMarketService.Update(new DebtMarket
        //                {
        //                    Amount = (totalAmount - moneyReceivedFromMarket.Amount),
        //                    Date = moneyReceivedFromMarket.Date,
        //                    MarketId = moneyReceivedFromMarket.MarketId,
        //                    Id = _debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)
        //                });
        //            }
        //            else
        //            {
        //                _debtMarketService.Add(new DebtMarket
        //                {
        //                    Amount = (totalAmount - moneyReceivedFromMarket.Amount),
        //                    Date = moneyReceivedFromMarket.Date,
        //                    MarketId = moneyReceivedFromMarket.MarketId,
        //                });
        //            }
        //        }
        //        if (totalAmount == moneyReceivedFromMarket.Amount)
        //        {
        //            _debtMarketService.Delete(new DebtMarket
        //            {
        //                Date = moneyReceivedFromMarket.Date,
        //                MarketId = moneyReceivedFromMarket.MarketId,
        //                Id = _debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)
        //            });
        //        }
        //        _moneyReceivedFromMarketService.Update(moneyReceivedFromMarket);
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, e.Message);
        //    }


        //    return Ok();
        //}




        [HttpDelete("DeleteMoneyReceivedFromMarket")]
        public ActionResult DeleteMoneyReceivedFromMarket(MoneyReceivedFromMarketDto moneyReceivedFromMarket)
        {
            try
            {
                if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.ReceivedAmount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }


                int debtId = _debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);

                if (debtId != 0)
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
        public ActionResult UpdateMoneyReceivedFromMarket(MoneyReceivedFromMarketDto moneyReceivedFromMarket)
        {
            try
            {
                if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.ReceivedAmount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                if (!_moneyReceivedFromMarketService.IsExist(moneyReceivedFromMarket.MarketId, moneyReceivedFromMarket.Date))
                {

                    return BadRequest(Messages.WrongInput);
                }

              
                if (moneyReceivedFromMarket.TotalAmount < moneyReceivedFromMarket.ReceivedAmount)
                {
                    return BadRequest(Messages.InvalidAmount);
                }
                if (moneyReceivedFromMarket.TotalAmount > moneyReceivedFromMarket.ReceivedAmount)
                {
                    if (_debtMarketService.IsExist(_debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)))
                    {
                        _debtMarketService.Update(new DebtMarket
                        {
                            Amount = (moneyReceivedFromMarket.TotalAmount - moneyReceivedFromMarket.ReceivedAmount),
                            Date = moneyReceivedFromMarket.Date,
                            MarketId = moneyReceivedFromMarket.MarketId,
                            Id = _debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)
                        });
                    }
                    else
                    {
                        _debtMarketService.Add(new DebtMarket
                        {
                            Amount = (moneyReceivedFromMarket.TotalAmount - moneyReceivedFromMarket.ReceivedAmount),
                            Date = moneyReceivedFromMarket.Date,
                            MarketId = moneyReceivedFromMarket.MarketId,
                        });
                    }
                }
                if (moneyReceivedFromMarket.TotalAmount == moneyReceivedFromMarket.ReceivedAmount)
                {
                    _debtMarketService.Delete(new DebtMarket
                    {
                        Date = moneyReceivedFromMarket.Date,
                        MarketId = moneyReceivedFromMarket.MarketId,
                        Id = _debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)
                    });
                }

                _moneyReceivedFromMarketService.Update(new MoneyReceivedFromMarket
                {
                    MarketId = moneyReceivedFromMarket.MarketId,
                    Date = moneyReceivedFromMarket.Date,
                    Amount = moneyReceivedFromMarket.ReceivedAmount
                });
               
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


            return Ok();
        }



       //private (decimal TotalAmount, int TotalBread) CalculateTotalAmountAndBread(DateTime date, int marketId)
       // {

       //     List<ServiceList> serviceLists = _serviceListService.GetByDate(date);

       //     int TotalBread = 0;
       //     decimal Price = 0;
       //     for (int i = 0; i < serviceLists.Count; i++)
       //     {

       //         ServiceListDetail serviceListDetail = _serviceListDetailService.GetByServiceListIdAndMarketContractId(serviceLists[i].Id, _marketContractService.GetIdByMarketId(marketId));
       //         if (serviceListDetail != null)
       //         {
       //             TotalBread += serviceListDetail.Quantity;
       //             Price = serviceListDetail.Price;
       //         }
       //     }

       //     int StaleBreadCount = _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketId(marketId, date);

       //     decimal TotalAmount = (TotalBread - StaleBreadCount) * Price;


       //     return (TotalAmount, TotalBread);
       // }

    }





}