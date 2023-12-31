﻿using Business.Abstract;
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

        public MoneyReceivedFromMarketController(IDebtMarketService debtMarketService, IStaleBreadReceivedFromMarketService staleBreadReceivedFromMarketService, IMarketService marketService, IMarketContractService marketContractService, IMoneyReceivedFromMarketService moneyReceivedFromMarketService, IServiceListService serviceListService, IServiceListDetailService serviceListDetailService)
        {
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
                var result = _moneyReceivedFromMarketService.GetByMarketIdAndDate(marketId,date);
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
                List<MoneyReceivedFromMarket> moneyReceivedFromMarkets = _moneyReceivedFromMarketService.GetByDate(date);

                List<PaymentMarket> paymentMarkets = new();

                for (int i = 0; i < moneyReceivedFromMarkets.Count; i++)
                {
                    PaymentMarket paymentMarket = new();
                    paymentMarket.MarketId = moneyReceivedFromMarkets[i].MarketId;
                    paymentMarket.id = moneyReceivedFromMarkets[i].Id;
                    paymentMarket.Amount = moneyReceivedFromMarkets[i].Amount;
                    paymentMarket.MarketName = _marketService.GetNameById(moneyReceivedFromMarkets[i].MarketId);
                    paymentMarket.TotalAmount = TotalAmout(date, moneyReceivedFromMarkets[i].MarketId);
                    paymentMarket.StaleBread = _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketId(paymentMarket.MarketId, date);
                    paymentMarkets.Add(paymentMarket);

                }

                return Ok(paymentMarkets);
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
                    notPaymentMarket.TotalAmount = TotalAmout(date, filteredMarkets[i]);
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
                decimal totalAmount = TotalAmout(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);
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

                _debtMarketService.Delete(new DebtMarket
                {
                    Date = moneyReceivedFromMarket.Date,
                    MarketId = moneyReceivedFromMarket.MarketId,
                    Id = _debtMarketService.GetDebtIdByDateAndMarketId(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)
                });

                _moneyReceivedFromMarketService.Delete(moneyReceivedFromMarket);
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
                decimal totalAmount = TotalAmout(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);
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

        private decimal TotalAmout(DateTime date, int marketId)
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

            return TotalAmount;
        }
        private class PaymentMarket
        {
            public int id { get; set; }
            public decimal Amount { get; set; }
            public int MarketId { get; set; }
            public string MarketName { get; set; }
            public decimal TotalAmount { get; set; }
            public int StaleBread { get; set; }
        }

        private class NotPaymentMarket
        {
            public int MarketId { get; set; }
            public string MarketName { get; set; }
            public decimal TotalAmount { get; set; }
            public int StaleBread { get; set; }
        }
    }





}