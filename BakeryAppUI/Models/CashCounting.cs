namespace BakeryAppUI.Models
{
    public class CashCounting
    {
        public int Id { get; set; }
        public decimal TotalMoney { get; set; }
        public decimal RemainedMoney { get; set; }
        public decimal CreditCard { get; set; }
        public DateTime Date { get; set; }
    }
}
