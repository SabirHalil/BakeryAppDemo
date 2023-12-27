using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceListDetailDal : EfEntityRepositoryBase<ServiceListDetail, BakeryAppContext>, IServiceListDetailDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<ServiceListDetail>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public void DeleteByServiceListIdAndMarketContracId(int serviceListId, int marketContracId)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<ServiceListDetail>().FirstOrDefault(s=>s.ServiceListId ==serviceListId && s.MarketContractId == marketContracId));
                if (deletedEntity != null)
                {
                    deletedEntity.State = EntityState.Deleted;
                    context.SaveChanges();
                }                
            }
        }

        public bool IsExist(int id)
        {
            throw new NotImplementedException();
        }
    }
}
