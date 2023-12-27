using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class DoughFactoryListDetailManager : IDoughFactoryListDetailService
    {
        IDoughFactoryListDetailDal _doughFactoryListDetailDal;

        public DoughFactoryListDetailManager(IDoughFactoryListDetailDal doughFactoryListDetailDal)
        {
            _doughFactoryListDetailDal = doughFactoryListDetailDal;

        }

        public void Add(DoughFactoryListDetail doughFactoryListDetail)
        {
            _doughFactoryListDetailDal.Add(doughFactoryListDetail);
        }

        public void Delete(DoughFactoryListDetail doughFactoryListDetail)
        {
            _doughFactoryListDetailDal.Delete(doughFactoryListDetail);
        }
        public void DeleteById(int id)
        {
            _doughFactoryListDetailDal.DeleteById(id);
        }

        public List<DoughFactoryListDetail> GetAll()
        {
            return _doughFactoryListDetailDal.GetAll();
        }

        public List<DoughFactoryListDetail> GetByDoughFactoryList(int id)
        {
            return _doughFactoryListDetailDal.GetAll(d => d.DoughFactoryListId == id);
        }

        public DoughFactoryListDetail GetById(int id)
        {
            return _doughFactoryListDetailDal.Get(d => d.Id == id);
        }
        
        public bool IsExist(int id)
        {
            return _doughFactoryListDetailDal.IsExist(id);
        }

        public void Update(DoughFactoryListDetail doughFactoryListDetail)
        {
            _doughFactoryListDetailDal.Update(doughFactoryListDetail);
        }
    }

}
