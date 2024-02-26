namespace Entities.DTOs
{
    public class EndOfDayAccountForBread
    {
        public decimal Price { get; set; }
        public double ProductedToday { get; set; }
        public double RemainingYesterday { get; set; }
        public double RemainingToday { get; set; }
        public double StaleBreadToday { get; set; }
        public int TotalBreadGivenToService { get; set; }
        public int TotalBreadGivenToGetir { get; set; }
        public int PurchasedBread { get; set; }
        public int TotalStaleBreadFromService { get; set; }
        public int EatenBread { get; set; }

    }
}
