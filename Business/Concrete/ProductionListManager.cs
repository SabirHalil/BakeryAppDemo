using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductionListManager : IProductionListService
    {


        IProductionListDal _productionListDal;

        public ProductionListManager(IProductionListDal productionListDal)
        {
            _productionListDal = productionListDal;
        }

        public int Add(ProductionList productionList)
        {
            _productionListDal.Add(productionList);
            return productionList.Id;
        }

        public void DeleteById(int id)
        {
            _productionListDal.DeleteById(id);
        }

        public void Delete(ProductionList productionList)
        {
            _productionListDal.Delete(productionList);
        }
        public List<ProductionList> GetAll()
        {
            return _productionListDal.GetAll();
        }

        public ProductionList GetById(int id)
        {
            return _productionListDal.Get(d => d.Id == id);
        }

        public void Update(ProductionList productionList)
        {
            _productionListDal.Update(productionList);
        }



        public int GetByDateAndCategoryId(DateTime date, int categoryId)
        {
            ProductionList productList = _productionListDal.Get(p => p.Date.Date == date.Date && p.CategoryId == categoryId);
            return productList == null ? 0 : productList.Id;
        }

        public List<int> GetByDate(DateTime date)
        {
            List<ProductionList> productList = _productionListDal.GetAll(p => p.Date.Date == date.Date);
            if (productList != null && productList.Any())
            {
                List<int> productListIds = productList.Select(p => p.Id).ToList();
                return productListIds;
            }
            else
            {
                return null;
            }
        }
    }
}
