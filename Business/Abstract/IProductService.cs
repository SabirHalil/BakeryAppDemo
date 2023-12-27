﻿using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
 
        List<Product> GetAllByCategoryId(int categoryId);
        void Add(Product product);
        void DeleteById(int id);
        void Delete(Product product);
        void Update(Product product);
        Product GetById(int id);

        List<Product> GetProductsByListId(int listId, int categoryId);

        decimal GetPriceById(int id);
    }
}
