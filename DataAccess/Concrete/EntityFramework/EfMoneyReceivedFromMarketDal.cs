using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMoneyReceivedFromMarketDal : EfEntityRepositoryBase<MoneyReceivedFromMarket, BakeryAppContext>, IMoneyReceivedFromMarketDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<MoneyReceivedFromMarket>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public bool IsExist(int marketId, DateTime date)
        {
            using (BakeryAppContext context = new())
            {
                return context.Set<MoneyReceivedFromMarket>()
                                    .Any(m => m.MarketId == marketId && m.Date.Date == date.Date);
            }
        }

        public List<MoneyReceivedMarket> MoneyReceivedMarkets(DateTime date)
        {
            using (BakeryAppContext context = new())
            {
                var result = (from moneyReceivedFromMarket in context.MoneyReceivedFromMarkets
                              join market in context.Markets
                                  on moneyReceivedFromMarket.MarketId equals market.Id
                              where moneyReceivedFromMarket.Date.Date == date.Date
                              select new MoneyReceivedMarket
                              {
                                  Id = moneyReceivedFromMarket.Id,
                                  MarketId = market.Id,
                                  MarketName = market.Name,
                                  ReceivedAmount = moneyReceivedFromMarket.Amount,
                                 
                              }).ToList();

                return result;
            }
        }
        public List<Market> ServiceProductsDeliveredMarkets(DateTime date)
        {
            using (BakeryAppContext context = new())
            {
                var moneyReceivedMarkets = (from moneyReceivedFromMarket in context.MoneyReceivedFromMarkets
                                            join market in context.Markets
                                            on moneyReceivedFromMarket.MarketId equals market.Id
                                            where moneyReceivedFromMarket.Date.Date == date.Date
                                            select new Market
                                            {
                                                Id = market.Id,
                                                Name = market.Name
                                            }).ToList();

                var serviceProductsDeliveredMarkets = (from serviceList in context.ServiceLists
                                                       join serviceDetail in context.ServiceListDetails
                                                       on serviceList.Id equals serviceDetail.ServiceListId
                                                       join marketContract in context.MarketContracts
                                                       on serviceDetail.MarketContractId equals marketContract.Id
                                                       join market in context.Markets
                                                       on marketContract.MarketId equals market.Id
                                                       where serviceList.Date.Date == date.Date
                                                       select new Market
                                                       {
                                                           Id = market.Id,
                                                           Name = market.Name
                                                       }).Distinct().ToList();


                var result = serviceProductsDeliveredMarkets.Where(m => !moneyReceivedMarkets.Select(s => s.Id).Contains(m.Id)).ToList();

                return result;
            }
        }




    }
}
