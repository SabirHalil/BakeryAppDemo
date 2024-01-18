using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IStaleBreadService
    {
        List<StaleBread> GetAll();
        List<StaleBreadDto> GetAllByDate(DateTime date);
        void Add(StaleBread staleBread);
        void DeleteById(int id);
        void Delete(StaleBread staleBread);
        void Update(StaleBread staleBread);
        StaleBread GetById(int id);
        double GetStaleBreadDailyReport(DateTime date);
        List<DoughFactoryProduct> GetDoughFactoryProducts(DateTime date);
        bool IsExist(int doughFactoryProductId, DateTime date);
    }
}
