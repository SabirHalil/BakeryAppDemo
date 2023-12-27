using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private IProductService _productService;
        private IProductionListDetailService _productionListDetailService;


        public ProductController(IProductService productService)
        {
            _productService = productService; 
        }

   [HttpGet("GetAllProductBycategoryId")]
        public ActionResult GetAllProductBycategoryId(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            return Ok(result);
        }

        [HttpGet("GetByListIdProduct")]
        public ActionResult GetByIdProduct(int listId, int categoryId)
        {
            if(listId ==  0 || categoryId == 0)
            {
                return BadRequest("There is no data!");
            }
            var result = _productService.GetProductsByListId(listId, categoryId);
            return Ok(result);
        }

        [HttpPost("AddProduct")]
        public ActionResult AddProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("There is no data!");
            }
            _productService.Add(product);
            return Ok();
        }

        [HttpDelete("DeleteProduct")]
        public ActionResult DeleteProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("There is no data!");
            }
            _productService.Delete(product);
            return Ok();
        }

        [HttpPut("UpdateProduct")]
        public ActionResult UpdateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("There is no data!");
            }
            _productService.Update(product);
            return Ok();
        }
    }
}