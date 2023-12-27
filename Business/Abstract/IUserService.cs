

using Core.Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        void DeleteById(int id);
        User GetByMail(string email);
        List<User> GetUsers();
    }
}
