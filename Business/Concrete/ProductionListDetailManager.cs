using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductionListDetailManager : IProductionListDetailService
    {


        IProductionListDetailDal _productionListDetailDal;

        public ProductionListDetailManager(IProductionListDetailDal productionListDetailDal)
        {
            _productionListDetailDal = productionListDetailDal;
        }

        public void Add(ProductionListDetail productionListDetail)
        {
            _productionListDetailDal.Add(productionListDetail);
        }

        public void DeleteById(int id)
        {
            _productionListDetailDal.DeleteById(id);
        }

        public void Delete(ProductionListDetail productionListDetail)
        {
            _productionListDetailDal.Delete(productionListDetail);
        }
        public List<ProductionListDetail> GetAll()
        {
            return _productionListDetailDal.GetAll();
        }

        public ProductionListDetail GetById(int id)
        {
            return _productionListDetailDal.Get(d => d.Id == id);
        }

        public void Update(ProductionListDetail productionListDetail)
        {
            _productionListDetailDal.Update(productionListDetail);
        }

        public bool IsExist(int id)
        {
            return _productionListDetailDal.IsExist(id);
        }

        public void AddList(List<ProductionListDetail> productionListDetail)
        {
            _productionListDetailDal.AddList(productionListDetail);
        }

        public List<ProductionListDetail> GetProductsByListId(int id)
        {
            return _productionListDetailDal.GetAll(p => p.ProductionListId == id);

        }


    }
}
