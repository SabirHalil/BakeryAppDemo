using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfGivenProductsToServiceDal : EfEntityRepositoryBase<GivenProductsToService, BakeryAppContext>, IGivenProductsToServiceDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<GivenProductsToService>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public List<GivenProductsToService> GetAllByDateAndServisTypeId(DateTime date, int servisTypeId)
        {
            using (BakeryAppContext context = new())
            {
                var result = context.GivenProductsToServices
                    .Where(x => x.Date.Date == date.Date && x.ServiceTypeId == servisTypeId)
                    .ToList();

                return result;
            }
        }

        public List<GivenProductsToServiceTotalResultDto> GetTotalQuantityResultByDate(DateTime date)
        {
            using (BakeryAppContext context = new())
            {
                var result = context.GivenProductsToServices
                    .Where(x => x.Date.Date == date.Date)
                    .GroupBy(x => x.ServiceTypeId)
                    .Select(group => new GivenProductsToServiceTotalResultDto
                    {
                        ServiceTypeName = context.ServiceTypes.FirstOrDefault(st => st.Id == group.Key).Name,
                        TotalQuantity = group.Sum(x => x.Quantity).ToString()
                    })
                    .ToList();

                return result;
            }
        }
    }
}
