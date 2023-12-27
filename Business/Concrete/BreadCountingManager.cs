using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BreadCountingManager : IBreadCountingService
    {


        IBreadCountingDal _breadCountingDal;
        
        public BreadCountingManager(IBreadCountingDal breadCountingDal)
        {
            _breadCountingDal = breadCountingDal;  
        }

        public void Add(BreadCounting breadCounting)
        {
            _breadCountingDal.Add(breadCounting);
        }

        public void DeleteById(int id)
        {
            _breadCountingDal.DeleteById(id);
        }

        public void Delete(BreadCounting breadCounting)
        {
            _breadCountingDal.Delete(breadCounting);
        }
        public List<BreadCounting> GetAll()
        {
           return _breadCountingDal.GetAll();
        }

        public BreadCounting GetById(int id)
        {
            return _breadCountingDal.Get(d => d.Id == id);
        }

        public void Update(BreadCounting breadCounting)
        {
            _breadCountingDal.Update(breadCounting);
        }

        public List<BreadCounting> GetBreadCountingByDate(DateTime date)
        {
           return _breadCountingDal.GetAll(b=>b.Date.Date == date.Date);
        }

        public void AddList(List<BreadCounting> breadCounting)
        {
            for (int i = 0; i < breadCounting.Count; i++)
            {
                _breadCountingDal.Add(breadCounting[i]);
            }
        }
    }
}
