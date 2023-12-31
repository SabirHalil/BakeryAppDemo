﻿using Business.Abstract;
using Business.Constants;
using Castle.Core.Internal;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreadCountingController : ControllerBase
    {

        private IBreadCountingService _breadCountingService;


        public BreadCountingController(IBreadCountingService breadCountingService)
        {
            _breadCountingService = breadCountingService; ;
        }

        [HttpGet("GetBreadCountingByDate")]
        public ActionResult GetBreadCountingByDate(DateTime date)
        {
            try
            {
                var result = _breadCountingService.GetBreadCountingByDate(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        // needs some changes

        [HttpPost("AddBreadCounting")]
        public ActionResult AddBreadCounting(BreadCounting breadCounting)
        {

            try
            {
                if (_breadCountingService.GetAll().Any())
                {
                    return BadRequest(Messages.OncePerDay);
                }

                if (breadCounting == null || breadCounting.Quantity < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                _breadCountingService.Add(breadCounting);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteBreadCountingById")]
        public ActionResult DeleteBreadCountingById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                _breadCountingService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateBreadCounting")]
        public ActionResult UpdateBreadCounting(BreadCounting breadCounting)
        {
            if (breadCounting == null || breadCounting.Quantity < 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                if (breadCounting.Quantity == 0)
                {
                    _breadCountingService.DeleteById(breadCounting.Id);
                }
                else
                {
                    _breadCountingService.Update(breadCounting);

                }


                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}