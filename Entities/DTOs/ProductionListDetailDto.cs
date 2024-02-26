namespace Entities.DTOs
{
    public class ProductionListDetailDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int ProductedToday { get; set; }
        public int RemainingYesterday { get; set; }
        public int RemainingToday { get; set; }
        public int StaleProductToday { get; set; }

    }
}
