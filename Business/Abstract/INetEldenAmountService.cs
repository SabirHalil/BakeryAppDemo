using Entities.Concrete;

namespace Business.Abstract
{
    public interface INetEldenAmountService
    {
        List<NetEldenAmount> GetAll();
        void Add(NetEldenAmount netEldenAmount);
        void DeleteById(int id);
        void Delete(NetEldenAmount netEldenAmount);
        void Update(NetEldenAmount netEldenAmount);
        NetEldenAmount GetById(int id);
    }
}
