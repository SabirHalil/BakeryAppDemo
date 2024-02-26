namespace Entities.DTOs
{
    public class EndOfDayResult
    {
        public EndOfDayAccountForBread EndOfDayAccount { get; set; }
        public Account Account { get; set; }
        public decimal PastaneRevenue { get; set; }
    }
}
