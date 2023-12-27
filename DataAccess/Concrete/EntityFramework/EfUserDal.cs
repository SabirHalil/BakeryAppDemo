using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, BakeryAppContext>, IUserDal
    {
        public void DeleteById(int id)
        {
            using (var context = new BakeryAppContext())
            {
                var entity = context.Users.Find(id); 

                if (entity != null)
                {
                    context.Users.Remove(entity);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Belirtilen kimlik değerine sahip nesne bulunamadı.");
                }
            }
        }

        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new BakeryAppContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }
    }
}
