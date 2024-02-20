using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Concrete.EntityFramework
{
    // Context : Db tabloları ile proje classlarını bağlamak.
    public class BakeryAppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ///////////////
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BakeryApp;Trusted_Connection=true");
            ///////
            optionsBuilder.UseSqlServer(@"Server=.;Database=bakery2;Trusted_Connection=true");

        }

        public DbSet<GivenProductsToService> GivenProductsToServices { get; set; }
        public DbSet<DebtMarket> DebtMarkets { get; set; }
        public DbSet<StaleBreadReceivedFromMarket> StaleBreadReceivedFromMarkets { get; set; }
        public DbSet<MoneyReceivedFromMarket> MoneyReceivedFromMarkets { get; set; }
        public DbSet<ServiceListDetail> ServiceListDetails { get; set; }
        public DbSet<ServiceList> ServiceLists { get; set; }
        public DbSet<ServiceProduct> ServiceProducts { get; set; }
        public DbSet<ServiceRemindMoney> ServiceRemindMoney { get; set; }
        public DbSet<ServiceStaleProduct> ServiceStaleProducts { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<StaleBread> StaleBread { get; set; }
        public DbSet<StaleProduct> StaleProducts { get; set; }
        public DbSet<ProductsCounting> ProductsCountings { get; set; }
        public DbSet<PurchasedProductListDetail> PurchasedProductListDetails { get; set; }       
        public DbSet<ReceivedMoney> ReceivedMoneys { get; set; }
        public DbSet<BreadPrice> BreadPrices { get; set; }
        public DbSet<BreadCounting> BreadCountings { get; set; }
        public DbSet<CashCounting> CashCountings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<MarketContract> MarketContracts { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<ProductionListDetail> ProductionListDetails { get; set; }
        public DbSet<ProductionList> ProductionLists { get; set; }
        public DbSet<AllService> AllServices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<DoughFactoryProduct> DoughFactoryProducts { get; set; }
        public DbSet<DoughFactoryList> DoughFactoryLists { get; set; }
        public DbSet<DoughFactoryListDetail> DoughFactoryListDetails { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<SystemAvailabilityTime> SystemAvailabilityTime { get; set; }
        public DbSet<ReceivedMoneyFromService> ReceivedMoney { get; set; }

    }
}
