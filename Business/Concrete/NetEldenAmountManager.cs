using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class NetEldenAmountManager : INetEldenAmountService
    {


        INetEldenAmountDal _netEldenAmountDal;
        
        public NetEldenAmountManager(INetEldenAmountDal netEldenAmountDal)
        {
            _netEldenAmountDal = netEldenAmountDal;  
        }

        public void Add(NetEldenAmount netEldenAmount)
        {
            _netEldenAmountDal.Add(netEldenAmount);
        }

        public void DeleteById(int id)
        {
            _netEldenAmountDal.DeleteById(id);
        }

        public void Delete(NetEldenAmount netEldenAmount)
        {
            _netEldenAmountDal.Delete(netEldenAmount);
        }
        public List<NetEldenAmount> GetAll()
        {
           return _netEldenAmountDal.GetAll();
        }

        public NetEldenAmount GetById(int id)
        {
            return _netEldenAmountDal.Get(d => d.Id == id);
        }

        public void Update(NetEldenAmount netEldenAmount)
        {
            _netEldenAmountDal.Update(netEldenAmount);
        }
    }
}
