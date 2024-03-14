using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceEndOfDayDal
    {

        public void GivenTotalAmountByServiceProductIdAndAndDate(int serviceProductId, DateTime date)
        {
            using (BakeryAppContext context = new())
            {
                var result = (from serviceDetail in context.ServiceListDetails
                              join serviceList in context.ServiceLists
                                  on serviceDetail.ServiceListId equals serviceList.Id
                              join marketContract in context.MarketContracts
                                  on serviceDetail.MarketContractId equals marketContract.Id                              
                              where marketContract.ServiceProductId == serviceProductId && serviceList.Date.Date == date.Date
                              select serviceDetail.Quantity).Sum();

            }
        }
        public void StaleTotalAmountByServiceProductIdAndAndDate(int serviceProductId, DateTime date)
        {
            using (BakeryAppContext context = new())
            {
                var result = (from staleProduct in context.StaleProductsReceivedFromMarkets                             
                              where staleProduct.ServiceProductId == serviceProductId && staleProduct.Date.Date == date.Date
                              select staleProduct.Quantity).Sum();

            }
        }

    }
}
