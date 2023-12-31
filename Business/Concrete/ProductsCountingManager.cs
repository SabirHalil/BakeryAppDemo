﻿using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductsCountingManager : IProductsCountingService
    {


        IProductsCountingDal _productsCountingDal;
        
        public ProductsCountingManager(IProductsCountingDal productsCountingDal)
        {
            _productsCountingDal = productsCountingDal;  
        }

        public void Add(ProductsCounting productsCounting)
        {
            _productsCountingDal.Add(productsCounting);
        }

        public void DeleteById(int id)
        {
            _productsCountingDal.DeleteById(id);
        }

        public void Delete(ProductsCounting productsCounting)
        {
            _productsCountingDal.Delete(productsCounting);
        }
        public List<ProductsCounting> GetAll()
        {
           return _productsCountingDal.GetAll();
        }

        public ProductsCounting GetById(int id)
        {
            return _productsCountingDal.Get(d => d.Id == id);
        }

        public void Update(ProductsCounting productsCounting)
        {
            _productsCountingDal.Update(productsCounting);
        }

        public List<ProductsCounting> GetProductsCountingByDate(DateTime date)
        {
            return _productsCountingDal.GetAll(p=>p.Date.Date == date.Date);
        }

        public bool IsExist(int productId)
        {
            return _productsCountingDal.Get(d => d.ProductId == productId) == null ? false : true;
        }

        public int GetQuantityProductsCountingByDateAndProductId(DateTime date, int productId)
        {
            ProductsCounting productsCounting = _productsCountingDal.Get(p => p.Date.Date == date.Date && p.ProductId == productId);
            return productsCounting == null ? 0 : productsCounting.Quantity;
        }
    }
}
