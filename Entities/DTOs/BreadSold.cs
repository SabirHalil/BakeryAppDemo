namespace Entities.DTOs
{
    public class BreadSold
    {
        //public int ProductId { get; set; }
        public string ProductName { get; set; }
        //public decimal Price { get; set; }
        public double Price { get; set; }
        public double Revenue { get; set; }
        //  public decimal Revenue { get; set; }
        public int RemainingYesterday { get; set; }
        public double ProductedToday { get; set; }
        public int RemainingToday { get; set; }
        public double StaleProductToday { get; set; }
        //  public int GivenBreadsToServiceTotal { get; set; }


    }
}
