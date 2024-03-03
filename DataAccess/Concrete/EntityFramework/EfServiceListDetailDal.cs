using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceListDetailDal : EfEntityRepositoryBase<ServiceListDetail, BakeryAppContext>, IServiceListDetailDal
    {

        public List<MarketAddedToServiceDto> GetMarketAddedToServiceList(int serviceListId)
        {
            using (BakeryAppContext context = new())
            {
                var result = (from serviceDetail in context.ServiceListDetails
                              join marketContract in context.MarketContracts
                                  on serviceDetail.MarketContractId equals marketContract.Id
                              join market in context.Markets
                                  on marketContract.MarketId equals market.Id
                              where serviceDetail.ServiceListId == serviceListId
                              select new MarketAddedToServiceDto
                              {
                                  MarketId = market.Id,
                                  MarketName = market.Name
                              }).Distinct().ToList();

                //LINQ sorgusunada Distinct() ekledim. Böylece aynı MarketId'ye sahip olanları bir kez listeye ekler.
                return result;
            }
        }

        public List<ProductsAddedToServiceListDetailDto> GetProductsAddedToServiceListDetail(int serviceListId, int marketId)
        {
            using (BakeryAppContext context = new())
            {
                var result = (from serviceDetail in context.ServiceListDetails
                              join marketContract in context.MarketContracts
                                  on serviceDetail.MarketContractId equals marketContract.Id
                              join serviceProduct in context.ServiceProducts
                                  on marketContract.ServiceProductId equals serviceProduct.Id
                              where serviceDetail.ServiceListId == serviceListId && marketContract.MarketId == marketId
                              select new ProductsAddedToServiceListDetailDto
                              {
                                  ServiceListDetailId = serviceDetail.Id,
                                  ServiceProductName = serviceProduct.Name,
                                  Quantity = serviceDetail.Quantity
                              }).ToList();

                return result;
            }
        }

        public List<ProductsNotAddedToServiceListDetailDto> GetProductsNotAddedToServiceListDetail(int serviceListId, int marketId)
        {
            using (BakeryAppContext context = new())
            {
                var result = (from marketContract in context.MarketContracts
                              where marketContract.MarketId == marketId
                              where !context.ServiceListDetails
                                  .Any(serviceDetail => serviceDetail.ServiceListId == serviceListId && serviceDetail.MarketContractId == marketContract.Id)
                              join serviceProduct in context.ServiceProducts
                                  on marketContract.ServiceProductId equals serviceProduct.Id
                              select new ProductsNotAddedToServiceListDetailDto
                              {
                                  MarketContractId = marketContract.Id,
                                  ServiceProductName = serviceProduct.Name
                              }).ToList();

                return result;
            }
        }

        public void UpdateQuantity(int entityId, int newQuantity)
        {
            using (BakeryAppContext context = new())
            {
                var entity = context.Set<ServiceListDetail>().Find(entityId);

                if (entity != null)
                {
                    entity.Quantity = newQuantity;
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Kimlik numarası {entityId} olan öğe bulunamadı. Güncelleme işlemi gerçekleştirilemedi.");
                }
            }
        }

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
                var deletedEntity = context.Entry(context.Set<ServiceListDetail>().FirstOrDefault(s => s.ServiceListId == serviceListId && s.MarketContractId == marketContracId));
                if (deletedEntity != null)
                {
                    deletedEntity.State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public bool IsExist(int serviceListId, int marketContractId)
        {
            using (BakeryAppContext context = new())
            {
                return context.Set<ServiceListDetail>().Any(s => s.MarketContractId == marketContractId && s.ServiceListId == serviceListId);
            }
        }

        public bool IsExistByServiceListId(int serviceListId)
        {
            using (BakeryAppContext context = new())
            {
                return context.Set<ServiceListDetail>().Any(s => s.ServiceListId == serviceListId);
            }
        }
    }
}
