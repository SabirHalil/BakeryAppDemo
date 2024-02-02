using Business.Abstract;
using Business.Constants;
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

        [HttpGet("GetByDoughFactoryProductId")]
        public ActionResult GetByDoughFactoryProductId(int doughFactoryProductId)
        {
            if (doughFactoryProductId <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                var result = _doughFactoryProductService.GetById(doughFactoryProductId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }


        }

    }
}
