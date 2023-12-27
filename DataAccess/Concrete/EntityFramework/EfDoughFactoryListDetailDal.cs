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

        public bool IsExist(int id)
        {
            using (var context = new BakeryAppContext())
            {
                var entity = context.DoughFactoryListDetails.Where(p=>p.DoughFactoryProductId == id); 
                return entity != null;
            }

            
        }
    }
}
