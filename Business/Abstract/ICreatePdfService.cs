using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICreatePdfService
    {
        byte[] EndOfDayAccountCreatePdf(DateTime date, EndOfDayResult endOfDayResult , decimal ProductsSoldInTheBakery);
        byte[] CreatePdf(DateTime date);
        byte[] CreatePdfForHamurhane(DateTime date);

    }
}
