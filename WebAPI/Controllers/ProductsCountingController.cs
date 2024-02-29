using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;

using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsCountingController : ControllerBase
    {

        private IProductsCountingService _productsCountingService;
        private IProductService _productService;


        public ProductsCountingController(IProductService productService, IProductsCountingService productsCountingService)
        {
            _productsCountingService = productsCountingService;
            _productService = productService;
        }


        [HttpGet("GetDictionaryProductsCountingByDateAndCategory")]
        public ActionResult GetDictionaryProductsCountingByDateAndCategory(DateTime date, int categoryId)
        {
            try
            {               
                var result = _productsCountingService.GetDictionaryProductsCountingByDateAndCategory(date,categoryId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("GetProductsCountingByDateAndCategory")]
        public ActionResult GetProductsCountingByDateAndCategory(DateTime date, int categoryId)
        {
            try
            {               
                var result = _productsCountingService.GetProductsCountingByDateAndCategory(date,categoryId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("GetAddedProductsCountingByDate")]
        public ActionResult GetAddedProductsCountingByDate(DateTime date)
        {
            try
            {
                List<ProductsCounting> productsCountings = _productsCountingService.GetProductsCountingByDate(date);
                List<ProductsCountingDto> getAddedProducts = new();

                for (int i = 0; i < productsCountings.Count; i++)
                {
                    ProductsCountingDto getAddedProduct = new();
                    getAddedProduct.ProductName = _productService.GetById(productsCountings[i].ProductId).Name;
                    getAddedProduct.ProductId = productsCountings[i].ProductId;
                    getAddedProduct.Quantity = productsCountings[i].Quantity;
                    getAddedProduct.Id = productsCountings[i].Id;
                    getAddedProduct.Date = productsCountings[i].Date;

                    getAddedProducts.Add(getAddedProduct);

                }
                return Ok(getAddedProducts);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetNotAddedProductsCountingByDate")]
        public ActionResult GetNotAddedProductsCountingByDate(DateTime date, int categoryId)
        {
            try
            {
                List<ProductsCountingDto> productsCountings = _productsCountingService.GetProductsCountingByDateAndCategory(date, categoryId);
                List<int> AddedProductsIds = new();

                for (int i = 0; i < productsCountings.Count; i++)
                {
                    if (!AddedProductsIds.Contains(productsCountings[i].ProductId))
                    {
                        AddedProductsIds.Add(productsCountings[i].ProductId);
                    }
                }

                List<Product> products = _productService.GetAllByCategoryId(categoryId);

                List<ProductNotAddedDto> getNotAddedProducts = new();

                for (int i = 0; i < products.Count; i++)
                {
                    if (!AddedProductsIds.Contains(products[i].Id))
                    {
                        ProductNotAddedDto getNotAddedProduct = new();
                        getNotAddedProduct.Id = products[i].Id;
                        getNotAddedProduct.Name = products[i].Name;
                        getNotAddedProducts.Add(getNotAddedProduct);

                    }
                }

                return Ok(getNotAddedProducts);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddProductsCounting")]
        public ActionResult AddProductsCounting(ProductsCounting productsCounting)
        {
            if (productsCounting == null || productsCounting.Quantity < 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                if (_productsCountingService.IsExist(productsCounting.ProductId ,productsCounting.Date))
                {
                    return BadRequest(Messages.OncePerDay);
                }

                _productsCountingService.Add(productsCounting);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteProductsCountingById")]
        public ActionResult DeleteProductsCountingById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                _productsCountingService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateProductsCounting")]
        public ActionResult UpdateProductsCounting(ProductsCounting productsCounting)
        {
            Console.WriteLine(productsCounting);
            if (productsCounting == null || productsCounting.Quantity < 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                if (productsCounting.Quantity == 0)
                {
                    _productsCountingService.DeleteById(productsCounting.Id);
                }
                else
                {
                    _productsCountingService.Update(productsCounting);
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