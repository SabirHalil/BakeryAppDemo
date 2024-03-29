﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMarketContractDal : EfEntityRepositoryBase<MarketContract, BakeryAppContext>, IMarketContractDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<MarketContract>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public List<int> ServiceProductsIdsByMarketId(int marketId)
        {
            using (BakeryAppContext context = new())
            {

                List<int> serviceProductIds = context.MarketContracts
                    .Where(mc => mc.MarketId == marketId)
                    .Select(mc => mc.ServiceProductId)
                    .ToList();

                return serviceProductIds;
            }
        }
    }
}
