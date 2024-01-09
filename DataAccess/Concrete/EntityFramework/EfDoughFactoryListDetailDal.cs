using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfDoughFactoryListDetailDal : EfEntityRepositoryBase<DoughFactoryListDetail, BakeryAppContext>, IDoughFactoryListDetailDal
    {
        public void DeleteById(int id)
        {
            using (var context = new BakeryAppContext())
            {
                var entity = context.DoughFactoryListDetails.Find(id);

                if (entity != null)
                {
                    context.DoughFactoryListDetails.Remove(entity);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Belirtilen kimlik değerine sahip nesne bulunamadı.");
                }
            }
        }

        public bool IsExist(int id, int listId)
        {
            using (var context = new BakeryAppContext())
            {
                return (context.DoughFactoryListDetails?.Any(p => p.DoughFactoryProductId == id && p.DoughFactoryListId == listId)).GetValueOrDefault();
            }
        }
    }
}
