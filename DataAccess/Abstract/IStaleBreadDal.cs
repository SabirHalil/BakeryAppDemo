using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IStaleBreadDal : IEntityRepository<StaleBread>
    {
        void DeleteById(int id);
        List<DoughFactoryProduct> GetDoughFactoryProductsByDate(DateTime date);
        double GetReport(DateTime date);
        List<StaleBreadDto> GetAllByDate(DateTime date);
        bool IsExist(int doughFactoryProductId, DateTime date);
    }
}
