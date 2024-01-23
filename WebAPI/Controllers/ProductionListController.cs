using Business.Abstract;
using Business.Constants;
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

        [HttpGet("GetAddedProductsByDateAndCategoryId")]
        public ActionResult GetAddedProductsByDateAndCategoryId(DateTime date, int categoryId)
        {
            if (categoryId == 0 || date.Date > DateTime.Now.Date)
            {
                return BadRequest(Messages.WrongInput);
            }
            var listId = _productionListService.GetByDateAndCategoryId(date, categoryId);
            var productsList = _productionListDetailService.GetProductsByListId(listId);
            return Ok(productsList);
        }

        [HttpGet("GetNotAddedProductsByListAndCategoryId")]
        public ActionResult GetNotAddedProductsByListAndCategoryId(DateTime date, int categoryId)
        {
            if(categoryId == 0 || date.Date >  DateTime.Now.Date) {
                return BadRequest(Messages.WrongInput);
            }

            var listId = _productionListService.GetByDateAndCategoryId(date,categoryId);
         

            if(listId == 0)
            {
                var productList = _productService.GetAllByCategoryId(categoryId);
                return Ok(productList);
            }
            var productListNotAdded = _productService.GetNotAddedProductsByListAndCategoryId(listId,categoryId);
            return Ok(productListNotAdded);

        }

        [HttpPost("AddProductionListAndDetail")]
        public ActionResult AddProductionDetailList(List<ProductionListDetail> productionListDetail, int userId, int categoryId)
        {
            if (productionListDetail == null || productionListDetail.Count == 0)
            {
                return BadRequest("Product list is null or empty.");
            }
            var listId = _productionListService.GetByDateAndCategoryId(DateTime.Now, categoryId);

            if (listId == 0)
            {
                listId = _productionListService.Add(new ProductionList { Id = listId, UserId = userId, Date = DateTime.Now , CategoryId= categoryId});
                
            }

            for (int i = 0; i < productionListDetail.Count; i++)
            {  
                    productionListDetail[i].ProductionListId = listId;
              
                    if (_productionListDetailService.IsExist(productionListDetail[i].ProductId, productionListDetail[i].ProductionListId))
                    {
                        return Conflict("A product already exist in the list.");
                    }
               
               productionListDetail[i].Price = _productService.GetPriceById(productionListDetail[i].ProductId);
            }

            _productionListDetailService.AddList(productionListDetail);
            return Ok();
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

