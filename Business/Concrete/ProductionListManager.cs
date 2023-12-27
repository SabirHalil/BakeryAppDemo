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

        public int GetByDate(DateTime date)
        {
            ProductionList productList = _productionListDal.Get(p => p.Date.Date == date.Date);
            return productList == null ? 0 : productList.Id;

        }
    }
}
