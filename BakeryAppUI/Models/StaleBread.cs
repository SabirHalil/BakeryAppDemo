namespace BakeryAppUI.Models
{
    public class StaleBread
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int DoughFactoryProductId { get; set; }
        public string DoughFactoryProductName { get; set; }
    }

}
