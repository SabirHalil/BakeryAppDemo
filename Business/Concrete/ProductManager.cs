using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {


        IProductDal _productDal;
        
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;  
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void DeleteById(int id)
        {
            _productDal.DeleteById(id);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }
        public List<Product> GetAll()
        {
           return _productDal.GetAll();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(d => d.Id == id);
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }

      

        public List<Product> GetNotAddedProductsByListAndCategoryId(int listId, int categoryId)
        {
            return _productDal.GetNotAddedProductsByListAndCategoryId(listId,categoryId);
        }

        public decimal GetPriceById(int id)
        {
            return _productDal.Get(p => p.Id == id).Price;
        }

        public List<Product> GetAllByCategoryId(int categoryId)
        {
            return _productDal.GetAll(p => p.CategoryId == categoryId);
        }
    }
}
