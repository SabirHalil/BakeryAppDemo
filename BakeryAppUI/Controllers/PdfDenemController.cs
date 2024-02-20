using Microsoft.AspNetCore.Mvc;

namespace BakeryAppUI.Controllers
{
    public class PdfDenemController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7207/api/CreatePdf/create");

                if (response.IsSuccessStatusCode)
                {
                    var pdfData = await response.Content.ReadAsByteArrayAsync();
                    var base64Pdf = Convert.ToBase64String(pdfData);
                    ViewBag.PdfData = base64Pdf;
                }
                else
                {
                    // Handle error if needed
                }
            }

            return View();
        }


        //public async Task<IActionResult> IndexAsync()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var response = await httpClient.GetAsync("https://localhost:7207/api/CreatePdf/create");

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var pdfData = await response.Content.ReadAsByteArrayAsync();
        //            var fileName = "custom_filename.pdf"; // Belirlediğiniz dosya adını burada tanımlayabilirsiniz

        //            // Content-Disposition başlığını kullanarak dosya adını belirleme
        //            Response.Headers.Add("Content-Disposition", $"inline; filename={fileName}");

        //            return File(pdfData, "application/pdf");
        //        }
        //        else
        //        {
        //            // Handle error if needed
        //            return View("Error"); // Örneğin, bir hata görüntüleme sayfasına yönlendirme
        //        }
        //    }
        //}

    }
}
