using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionListController : ControllerBase
    {

        private IProductionListService _productionListService;
        private IProductionListDetailService _productionListDetailService;
        private IProductService _productService;

        public ProductionListController(IProductService productService ,IProductionListService productionListService, IProductionListDetailService productionListDetailService)
        {
            _productService = productService;
            _productionListService = productionListService;
            _productionListDetailService = productionListDetailService;

        }

        [HttpGet("GetByDateProductionList")]
        public ActionResult GetByDateDoughFactoryList(DateTime date)
        {
            var result = _productionListService.GetByDate(date);
            return Ok(result);
        }

        [HttpPost("AddProductionListAndDetail")]
        public ActionResult AddProductionDetailList(List<ProductionListDetail> productionListDetail, int userId)
        {
            if (productionListDetail == null || productionListDetail.Count == 0)
            {
                return BadRequest("Product list is null or empty.");
            }
            int id = 0;
            bool IsNewList = false;
            if (productionListDetail[0].ProductionListId == 0)
            {
                id = _productionListService.Add(new ProductionList { Id = 0, UserId = userId, Date = DateTime.Now });
                IsNewList = true;
            }

            for (int i = 0; i < productionListDetail.Count; i++)
            {

                if (IsNewList)
                {
                    productionListDetail[i].ProductionListId = id;
                    //_doughFactoryListDetailService.Add(productionListDetail[i]);
                }
                else
                {
                    if (_productionListDetailService.IsExist(productionListDetail[i].ProductId))
                    {
                        return Conflict("A product already exist in the list.");
                    }
                }
               productionListDetail[i].Price = _productService.GetPriceById(productionListDetail[i].ProductId);
            }

            _productionListDetailService.AddList(productionListDetail);
            return Ok();
        }

        [HttpGet("GetProductionListDetailByDate")]
        public ActionResult GetDoughFactoryListDetail(DateTime date)
        {
            var result = _productionListService.GetByDate(date);
            if(result == null || result == 0)
            {
                return NotFound();
            }

            
            return Ok(_productionListDetailService.GetProductsByListId(result));
        }

        [HttpDelete("DeleteProductionListDetail")]
        public ActionResult DeleteDoughFactoryListDetail(int id)
        {
            _productionListDetailService.DeleteById(id);
            return Ok();
        }

        [HttpPut("UpdateProductionListDetail")]
        public ActionResult UpdateDoughFactoryListDetail(ProductionListDetail productionListDetail)
        {
            _productionListDetailService.Update(productionListDetail);
            return Ok();
        }

    }

}

