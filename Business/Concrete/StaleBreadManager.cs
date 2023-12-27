using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class StaleBreadManager : IStaleBreadService
    {


        IStaleBreadDal _staleBreadDal;

        public StaleBreadManager(IStaleBreadDal staleBreadDal)
        {
            _staleBreadDal = staleBreadDal;
        }

        public void Add(StaleBread staleBread)
        {
            _staleBreadDal.Add(staleBread);
        }

        public void DeleteById(int id)
        {
            _staleBreadDal.DeleteById(id);
        }

        public void Delete(StaleBread staleBread)
        {
            _staleBreadDal.Delete(staleBread);
        }
        public List<StaleBread> GetAll()
        {
            return _staleBreadDal.GetAll();
        }

        public StaleBread GetById(int id)
        {
            return _staleBreadDal.Get(d => d.Id == id);
        }

        public void Update(StaleBread staleBread)
        {
            _staleBreadDal.Update(staleBread);
        }

        public double GetStaleBreadDailyReport(DateTime date)
        {
            return _staleBreadDal.GetReport(date);
        }

        public List<DoughFactoryProduct> GetDoughFactoryProducts(DateTime date)
        {
            return _staleBreadDal.GetDoughFactoryProductsByDate(date);
        }

        public List<StaleBreadDto> GetAllByDate(DateTime date)
        {
            return _staleBreadDal.GetAllByDate(date);
        }
    }
}
