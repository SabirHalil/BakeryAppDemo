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
using System.Globalization;

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
     

        [HttpGet("CreatePdf")]
        public IActionResult CreatePdf(DateTime date)
        {

            try
            {              

                byte[] pdfContent = _createPdfService.CreatePdf(date);
                string formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));

                string fileName = $"PASTANE_İMALAT_VE_SATIŞ_TAKİP_ENVANTERİ_{formattedDate}.pdf";
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
                string formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));

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
