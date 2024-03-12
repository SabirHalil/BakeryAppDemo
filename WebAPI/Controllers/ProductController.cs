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
        public ProductController(IProductService productService)
        {
            _productService = productService; 
        }

        [HttpGet("GetAllProductsBycategoryId")]
        public ActionResult GetAllProductsBycategoryId(int categoryId)
        {
            var result = _productService.GetAllProductsByCategoryId(categoryId);
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