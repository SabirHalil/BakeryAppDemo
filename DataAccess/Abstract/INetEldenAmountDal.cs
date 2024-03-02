using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface INetEldenAmountDal : IEntityRepository<NetEldenAmount>
    {
        void DeleteById(int id);

        decimal TotalAmountBetweenDates(DateTime startDateTime, DateTime endDateTime);
    }
}
