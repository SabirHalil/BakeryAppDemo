using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using iText.Kernel.Font;
using iText.IO.Font;
using Business.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePdfController : ControllerBase
    {

        private ICreatePdfService _createPdfService;
        public CreatePdfController(ICreatePdfService createPdfService)
        {
            _createPdfService = createPdfService;
        }

        private static Dictionary<int, string> CategoryNameByCategoryId = new Dictionary<int, string>()
        {
            { 1, "Pastane" },
            { 2, "Börek" },
            { 3, "Dışarıda Alınan Ürünler" },
        };


        [HttpGet("CreatePdf")]
        public IActionResult CreatePdf(DateTime date, int categoryId)
        {

            try
            {              

                byte[] pdfContent = _createPdfService.CreatePdf(date, categoryId);
                string formattedDate = date.ToString("dd.MM.yyyy");

                string fileName = $"{CategoryNameByCategoryId[categoryId]}_{formattedDate}.pdf";
                string contentType = "application/pdf";


                FileContentResult fileContentResult = new FileContentResult(pdfContent, contentType)
                {
                    FileDownloadName = fileName
                };

                return fileContentResult;

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }
        [HttpGet("CreatePdfForHamurhane")]
        public IActionResult CreatePdfForHamurhane(DateTime date)
        {

            try
            {              

                byte[] pdfContent = _createPdfService.CreatePdfForHamurhane(date);
                string formattedDate = date.ToString("dd.MM.yyyy");

                string fileName = $"Hamurhane_{formattedDate}.pdf";
                string contentType = "application/pdf";


                FileContentResult fileContentResult = new FileContentResult(pdfContent, contentType)
                {
                    FileDownloadName = fileName
                };

                return fileContentResult;

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }
    }
}
