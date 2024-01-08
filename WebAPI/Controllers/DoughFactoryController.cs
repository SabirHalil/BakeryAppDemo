﻿using Business.Abstract;
using Business.Constants;
using Castle.Core.Internal;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoughFactoryController : ControllerBase
    {

        private IDoughFactoryListService _doughFactoryListService;
        private IDoughFactoryListDetailService _doughFactoryListDetailService;
        private IDoughFactoryProductService _doughFactoryProductService;

        public DoughFactoryController(IDoughFactoryProductService doughFactoryProductService, IDoughFactoryListService doughFactoryListService, IDoughFactoryListDetailService doughFactoryListDetailService)
        {
            _doughFactoryListService = doughFactoryListService;
            _doughFactoryListDetailService = doughFactoryListDetailService;
            _doughFactoryProductService = doughFactoryProductService;
        }

        [HttpGet("GetByDateDoughFactoryList")]
        public ActionResult GetByDateDoughFactoryList(DateTime date)
        {
            if (date.Date > DateTime.Now.Date)
            {
                return BadRequest(Messages.WrongDate);
            }
            try
            {
                var result = _doughFactoryListService.GetByDate(date.Date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


        }

        [HttpPost("AddDoughFactoryListAndListDetail")]
        public ActionResult AddDoughFactory(List<DoughFactoryListDetail> doughFactoryListDetail, int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            if (doughFactoryListDetail.IsNullOrEmpty())
            {
                return BadRequest(Messages.ListEmpty);
            }

            try
            {
                int id = 0;
                bool IsNewList = false;
                if (doughFactoryListDetail[0].DoughFactoryListId == 0)
                {
                    id = _doughFactoryListService.Add(new DoughFactoryList { Id = 0, UserId = userId, Date = DateTime.Now });
                    IsNewList = true;
                }

                for (int i = 0; i < doughFactoryListDetail.Count; i++)
                {

                    if (IsNewList)
                    {
                        doughFactoryListDetail[i].DoughFactoryListId = id;
                        _doughFactoryListDetailService.Add(doughFactoryListDetail[i]);
                    }
                    else
                    {
                        if (_doughFactoryListDetailService.IsExist(doughFactoryListDetail[i].DoughFactoryProductId))
                        {
                            return Conflict(Messages.Conflict);
                        }
                        else
                        {
                            _doughFactoryListDetailService.Add(doughFactoryListDetail[i]);
                        }
                    }
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

        [HttpGet("GetAddedDoughFactoryListDetailByListId")]
        public ActionResult GetDoughFactoryListDetail(int listId)
        {

            try
            {
                List<DoughFactoryListDetail> doughFactoryListDetails = _doughFactoryListDetailService.GetByDoughFactoryList(listId);

                List<GetAddedDoughFactoryListDetailDto> List = new();

                for (int i = 0; i < doughFactoryListDetails.Count; i++)
                {
                    GetAddedDoughFactoryListDetailDto addedDoughFactoryListDetailDto = new();
                    addedDoughFactoryListDetailDto.Id = doughFactoryListDetails[i].Id;

                    addedDoughFactoryListDetailDto.DoughFactoryProductId = doughFactoryListDetails[i].DoughFactoryProductId;
                    addedDoughFactoryListDetailDto.DoughFactoryProductName = _doughFactoryProductService.GetById(doughFactoryListDetails[i].DoughFactoryProductId).Name;

                    addedDoughFactoryListDetailDto.Quantity = doughFactoryListDetails[i].Quantity;
                    addedDoughFactoryListDetailDto.DoughFactoryListId = doughFactoryListDetails[i].DoughFactoryListId;

                    List.Add(addedDoughFactoryListDetailDto);
                }

                return Ok(List);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("GetNotAddedDoughFactoryListDetailByListId")]
        public ActionResult GetMarketByServiceListId(int listId)
        {
            try
            {
                List<DoughFactoryProduct> allDoughFactoryProduct = _doughFactoryProductService.GetAll();

                List<GetNotAddedDoughFactoryListDetailDto> getNotAddedDoughFactoryListDetailDto = new();

                if (listId == 0)
                {
                    for (int i = 0; i < allDoughFactoryProduct.Count; i++)
                    {
                        GetNotAddedDoughFactoryListDetailDto Dto = new();
                    
                        Dto.Id = allDoughFactoryProduct[i].Id;
                        Dto.Name = allDoughFactoryProduct[i].Name;

                        getNotAddedDoughFactoryListDetailDto.Add(Dto);
                    }
                }
                else
                {


                    List<DoughFactoryListDetail> doughFactoryListDetails = _doughFactoryListDetailService.GetByDoughFactoryList(listId);

                    List<int> addedDoughFactoryProductIds = new List<int>();

                    for (int i = 0; i < doughFactoryListDetails.Count; i++)
                    {
                        addedDoughFactoryProductIds.Add(doughFactoryListDetails[i].DoughFactoryProductId);
                    }

                    // LINQ kullanarak filtreleme
                    List<DoughFactoryProduct> filteredDoughFactoryProducts = allDoughFactoryProduct.Where(m => !addedDoughFactoryProductIds.Contains(m.Id)).ToList();

                    for (int i = 0; i < filteredDoughFactoryProducts.Count; i++)
                    {
                        GetNotAddedDoughFactoryListDetailDto Dto = new();

                        Dto.Id = filteredDoughFactoryProducts[i].Id;
                        Dto.Name = filteredDoughFactoryProducts[i].Name;

                        getNotAddedDoughFactoryListDetailDto.Add(Dto);
                    }
                }

                return Ok(getNotAddedDoughFactoryListDetailDto);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteDoughFactoryListDetail")]
        public ActionResult DeleteDoughFactoryListDetail(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                _doughFactoryListDetailService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("UpdateDoughFactoryListDetail")]
        public ActionResult UpdateDoughFactoryListDetail(DoughFactoryListDetail doughFactoryListDetail)
        {
            if (doughFactoryListDetail == null)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                _doughFactoryListDetailService.Update(doughFactoryListDetail);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


    }
}
