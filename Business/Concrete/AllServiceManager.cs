using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AllServiceManager : IAllServiceService
    {


        IAllServiceDal _allServiceDal;
        
        public AllServiceManager(IAllServiceDal allServiceDal)
        {
            _allServiceDal = allServiceDal;  
        }

        public void Add(AllService allService)
        {
            _allServiceDal.Add(allService);
        }

        public void DeleteById(int id)
        {
            _allServiceDal.DeleteById(id);
        }

        public void Delete(AllService allService)
        {
            _allServiceDal.Delete(allService);
        }
        public List<AllService> GetAll()
        {
           return _allServiceDal.GetAll();
        }

        public AllService GetById(int id)
        {
            return _allServiceDal.Get(d => d.Id == id);
        }

        public void Update(AllService allService)
        {
            _allServiceDal.Update(allService);
        }
    }
}
