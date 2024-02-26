using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICreatePdfService
    {
        byte[] EndOfDayAccountCreatePdf(DateTime date, EndOfDayResult endOfDayResult , decimal ProductsSoldInTheBakery);
        byte[] CreatePdf(DateTime date, int CategoryId);
        byte[] CreatePdfForHamurhane(DateTime date);

    }
}
