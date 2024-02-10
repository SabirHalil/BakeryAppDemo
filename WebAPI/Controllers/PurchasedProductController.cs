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
    public class PurchasedProductController : ControllerBase
    {



        private IPurchasedProductListDetailService _purchasedProductListDetailService;
        private IProductService _productService;
        public PurchasedProductController(IProductService productService, IPurchasedProductListDetailService purchasedProductListDetailService)
        {
            _productService = productService;
            _purchasedProductListDetailService = purchasedProductListDetailService;
        }


        [HttpGet("GetAddedPurchasedProductByDate")]
        public ActionResult GetAddedPurchasedProductByDate(DateTime date)
        {
            try
            {
                List<PurchasedProductListDetail> purchasedProductListDetail = _purchasedProductListDetailService.GetPurchasedProductListDetailByDate(date);
                List<GetAddedPurchasedProduct> getAddedPurchasedProducts = new();

                for (int i = 0; i < purchasedProductListDetail.Count; i++)
                {
                    GetAddedPurchasedProduct getAddedPurchasedProduct = new();
                    getAddedPurchasedProduct.ProductName = _productService.GetById(purchasedProductListDetail[i].ProductId).Name;
                    getAddedPurchasedProduct.ProductId = purchasedProductListDetail[i].ProductId;
                    getAddedPurchasedProduct.Quantity = purchasedProductListDetail[i].Quantity;
                    getAddedPurchasedProduct.Price = purchasedProductListDetail[i].Price;
                    getAddedPurchasedProduct.Id = purchasedProductListDetail[i].Id;

                    getAddedPurchasedProducts.Add(getAddedPurchasedProduct);

                }
                return Ok(getAddedPurchasedProducts);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetNotAddedPurchasedProductByDate")]
        public ActionResult GetNotAddedPurchasedProductByDate(DateTime date, int categoryId)
        {
            try
            {
                List<PurchasedProductListDetail> purchasedProductListDetail = _purchasedProductListDetailService.GetPurchasedProductListDetailByDate(date);
                List<int> AddedProductsIds = new();

                for (int i = 0; i < purchasedProductListDetail.Count; i++)
                {
                    if (!AddedProductsIds.Contains(purchasedProductListDetail[i].ProductId))
                    {
                        AddedProductsIds.Add(purchasedProductListDetail[i].ProductId);
                    }
                }

                List<Product> products = _productService.GetAllByCategoryId(categoryId);

                List<GetNotAddedPurchasedProduct> getNotAddedPurchasedProducts = new();

                for (int i = 0; i < products.Count; i++)
                {
                    if (!AddedProductsIds.Contains(products[i].Id))
                    {
                        GetNotAddedPurchasedProduct getNotAddedPurchasedProduct = new();
                        getNotAddedPurchasedProduct.ProductId = products[i].Id;
                        getNotAddedPurchasedProduct.ProductName = products[i].Name;

                        getNotAddedPurchasedProducts.Add(getNotAddedPurchasedProduct);

                    }
                }

                return Ok(getNotAddedPurchasedProducts);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddPurchasedProducts")]
        public ActionResult AddPurchasedProductListDetail(List<PurchasedProductListDetail> purchasedProductListDetail)
        {
            try
            {             
                if (purchasedProductListDetail.IsNullOrEmpty())
                {
                    return BadRequest(Messages.ListEmpty);
                }

                for (int i = 0; i < purchasedProductListDetail.Count; i++)
                {
                    if (purchasedProductListDetail[i] == null || purchasedProductListDetail[i].Quantity < 0)
                    {
                        return BadRequest(Messages.WrongInput);
                    }
                    if (_purchasedProductListDetailService.IsExist(purchasedProductListDetail[i].ProductId, purchasedProductListDetail[i].Date))
                    {
                        return BadRequest(Messages.OncePerDay);
                    }
                    purchasedProductListDetail[i].Price = _productService.GetById(purchasedProductListDetail[i].ProductId).Price;                    
                }

                _purchasedProductListDetailService.AddList(purchasedProductListDetail);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeletePurchasedProductById")]
        public ActionResult DeletePurchasedProductById(int id, int userId)
        {
            if (id <= 0 || userId <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                PurchasedProductListDetail purchasedProductListDetail = _purchasedProductListDetailService.GetById(id);
                if (purchasedProductListDetail == null)
                {
                    return BadRequest("Bu ürün zaten ekli değil!");
                }
                if (purchasedProductListDetail.UserId != userId)
                {
                    return BadRequest("Bu ürünü silmek için yetkiniz yok!");
                }

                _purchasedProductListDetailService.DeleteById(id, userId);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateProductsCounting")]
        public ActionResult UpdateProductsCounting(PurchasedProductListDetail purchasedProductListDetail, int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                if (purchasedProductListDetail == null || purchasedProductListDetail.Quantity < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                PurchasedProductListDetail purchasedProductListDetail2 = new();

                purchasedProductListDetail2 = _purchasedProductListDetailService.GetById(purchasedProductListDetail.Id);

                if (purchasedProductListDetail2 == null)
                {
                    return BadRequest("Bu ürün zaten ekli değil!");
                }
                if (purchasedProductListDetail2.UserId != userId)
                {
                    return BadRequest("Bu ürünü güncellemek için yetkiniz yok!");
                }

                _purchasedProductListDetailService.Update(purchasedProductListDetail);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


        }

        private class GetAddedPurchasedProduct
        {
            public int Id { get; set; }
            public decimal Price { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public string ProductName { get; set; }
        }
        private class GetNotAddedPurchasedProduct
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
        }
    }
}
