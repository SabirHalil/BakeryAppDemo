using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAllServiceService
    {
        List<AllService> GetAll();
        void Add(AllService allService);
        void DeleteById(int id);
        void Delete(AllService allService);
        void Update(AllService allService);
        AllService GetById(int id);
    }
}
