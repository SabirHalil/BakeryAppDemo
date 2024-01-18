namespace BakeryAppUI.Models
{
    public class Expense 
    {
        public int Id { get; set; }
        public string Detail { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
