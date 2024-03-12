using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoughFactoryProductController : ControllerBase
    {


        private IDoughFactoryProductService _doughFactoryProductService;

        public DoughFactoryProductController(IDoughFactoryProductService doughFactoryProductService)
        {
            _doughFactoryProductService = doughFactoryProductService;
        }

        [HttpGet("GetDoughFactoryProducts")]
        public ActionResult GetAllDoughfactoryProducts()
        {
           
            try
            {
                var result = _doughFactoryProductService.GetAllProducts();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost("AddDoughFactoryProduct")]
        public ActionResult AddProduct(DoughFactoryProduct product)
        {
            if (product == null)
            {
                return BadRequest("There is no data!");
            }
            _doughFactoryProductService.Add(product);
            return Ok();
        }

        [HttpPut("UpdateDoughFactoryProduct")]
        public ActionResult UpdateProduct(DoughFactoryProduct product)
        {
            if (product == null)
            {
                return BadRequest("There is no data!");
            }
            _doughFactoryProductService.Update(product);
            return Ok();
        }

    }
}
