using Business.Abstract;
using Business.Constants;
using Castle.Core.Internal;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoughFactoryController : ControllerBase
    {

        private IDoughFactoryListService _doughFactoryListService;
        private IDoughFactoryListDetailService _doughFactoryListDetailService;

        public DoughFactoryController(IDoughFactoryListService doughFactoryListService, IDoughFactoryListDetailService doughFactoryListDetailService)
        {
            _doughFactoryListService = doughFactoryListService;
            _doughFactoryListDetailService = doughFactoryListDetailService;
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

        [HttpGet("GetDoughFactoryListDetail")]
        public ActionResult GetDoughFactoryListDetail(int doughFactoryList)
        {
            try
            {
                var result = _doughFactoryListDetailService.GetByDoughFactoryList(doughFactoryList);
                return Ok(result);
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
