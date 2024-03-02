namespace BakeryAppUI.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal DeliveredAmount { get; set; }
        public decimal TotalAccumulatedAmount { get; set; }
    }
}
