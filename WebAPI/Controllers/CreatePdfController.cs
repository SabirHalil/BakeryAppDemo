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

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatePdfController : ControllerBase
    {

        [HttpGet("create")]
        public IActionResult CreatePdf()
        {          
            using (var stream = new MemoryStream())
            {
                string formattedDate;
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {

                        PdfFont font = PdfFontFactory.CreateFont("Helvetica", "CP1254", PdfFontFactory.EmbeddingStrategy.FORCE_NOT_EMBEDDED);

                        var document = new Document(pdf);
                        document.SetFont(font);


                       var today = DateTime.Now;
                        formattedDate = today.ToString("dd.MM.yyyy");
                        var dayOfWeek = today.ToString("dddd");

                        var date = new Paragraph($"Tarih: {formattedDate} - {dayOfWeek}")
                            .SetTextAlignment(TextAlignment.RIGHT)
                           ;

                        document.Add(date);

                    

                        var companyName = "ASLANOĞLU Fırın";
                        var company = new Paragraph(companyName)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(16);
                            
                        document.Add(company);


                        var title = new Paragraph("Gün Sonu Hesap")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(16);
                            
                        document.Add(title);



                        var table1 = CreateTable1("Tablo 1 Başlığı", new List<int> { 1, 2, 3 });
                        var table2 = CreateTable2("Tablo 2 Başlığı", new List<int> { 1, 2, 3 });
                        var table3 = CreateTable3("Tablo 3 Başlığı", new List<int> { 1, 2, 3 });

                        // Tabloları yan yana eklemek için bir layout
                        var layout = new Table(3)
                                 .UseAllAvailableWidth()
                                 .SetBorder(Border.NO_BORDER);

                        // Tabloları layout içine ekleyin
                        layout.AddCell(new Cell().Add(table1).SetBorder(Border.NO_BORDER));
                        layout.AddCell(new Cell().Add(table2).SetBorder(Border.NO_BORDER));
                        layout.AddCell(new Cell().Add(table3).SetBorder(Border.NO_BORDER));

                        document.Add(layout);

                        //***************   Giderler ***************

                        var giderler = new Paragraph("Giderler")
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(16);
                            
                        document.Add(giderler);


                        var giderlerTable = CreateTable("Giderler", new List<int> { 1, 2, 3 });         
                        giderlerTable.SetWidth(UnitValue.CreatePercentValue(33));



                        document.Add(giderlerTable);

                        var KrediKartıAmount = 500;
                        var NetEldenAmount = 1000;

                        var KrediKartı = new Paragraph($"Kredi Kartı: {KrediKartıAmount}")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetFontSize(16);                           
                        document.Add(KrediKartı);

                        var NetElden = new Paragraph($"Net Elden: {NetEldenAmount}")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetFontSize(16);                          
                        document.Add(NetElden);

                         var Total = new Paragraph($"Toplam: {NetEldenAmount+KrediKartıAmount}")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetFontSize(16);                          
                        document.Add(Total);

                        document.Close();
                    }
                }

            
                //stream.Seek(0, SeekOrigin.Begin);

                // Dosyayı döndür
                return File(stream.ToArray(), "application/pdf", $"GünSonuHesap_{formattedDate}.pdf");
            }
        }


        private Table CreateTable(string title, List<int> data)
        {
            var table = new Table(2)
                .UseAllAvailableWidth()
                .SetHorizontalAlignment(HorizontalAlignment.LEFT);

            //table.AddHeaderCell(new Cell(1, 2)
            //    .Add(new Paragraph(title)
            //        .SetFontSize(14)
            //        .SetBold())
            //    .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
            //);

            //foreach (var item in data)
            //{
            //    table.AddCell(new Cell().Add(new Paragraph(item.ToString())));
            //    table.AddCell(new Cell().Add(new Paragraph(item.Property2.ToString())));
            //}
            
            foreach (var item in data)
            {
                table.AddCell(new Cell().Add(new Paragraph("Kapı")));
                table.AddCell(new Cell().Add(new Paragraph(item.ToString())));
            }

            return table;
        }

        private Table CreateTable1(string title, List<int> data)
        {
            var table1 = new Table(2)
                          .UseAllAvailableWidth()
                          .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            // Başlık satırı
            table1.AddCell(new Cell().Add(new Paragraph("Yarına Kalan Ekmek")));
            table1.AddCell(new Cell().Add(new Paragraph("20")));

            // Servis Ekmek
            table1.AddCell(new Cell().Add(new Paragraph("Servis Ekmek")));
            table1.AddCell(new Cell().Add(new Paragraph(20.ToString())));

            // Servis Bayat
            table1.AddCell(new Cell().Add(new Paragraph("Servis Bayat")));
            table1.AddCell(new Cell().Add(new Paragraph(20.ToString())));

            // Yenen Ekmek
            table1.AddCell(new Cell().Add(new Paragraph("Yenen Ekmek")));
            table1.AddCell(new Cell().Add(new Paragraph(20.ToString())));

            // Bayat
            table1.AddCell(new Cell().Add(new Paragraph("Bayat")));
            table1.AddCell(new Cell().Add(new Paragraph(20.ToString())));

            // Getir
            table1.AddCell(new Cell().Add(new Paragraph("Getir")));
            table1.AddCell(new Cell().Add(new Paragraph(20.ToString())));

            // Boş satır
            table1.AddCell(new Cell(1, 2).SetHeight(18));

            // Toplam
            table1.AddCell(new Cell().Add(new Paragraph("Toplam")));
            table1.AddCell(new Cell().Add(new Paragraph(20.ToString())));

            return table1;
        }

        private Table CreateTable2(string title, List<int> data)
        {
            var table2 = new Table(2)
                             .UseAllAvailableWidth()
                             .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            // İmalat
            table2.AddCell(new Cell().Add(new Paragraph("İmalat")));
            table2.AddCell(new Cell().Add(new Paragraph("10"))); 

            // Dünden Kalan
            table2.AddCell(new Cell().Add(new Paragraph("Dünden Kalan")));
            table2.AddCell(new Cell().Add(new Paragraph("5"))); 

            // Dışarıdan Alınan
            table2.AddCell(new Cell().Add(new Paragraph("Dışarıdan Alınan")));
            table2.AddCell(new Cell().Add(new Paragraph("8"))); 

            // Toplam
            table2.AddCell(new Cell().Add(new Paragraph("Toplam")));
            table2.AddCell(new Cell().Add(new Paragraph("23"))); 

            // Boş satır
            table2.AddCell(new Cell(1, 2).SetHeight(18));
            table2.AddCell(new Cell(1, 2).SetHeight(18));
            table2.AddCell(new Cell(1, 2).SetHeight(18));

            // Tezgahta Satılan Ekmek
            table2.AddCell(new Cell().Add(new Paragraph("Tezgahta Satılan Ekmek")));
            table2.AddCell(new Cell().Add(new Paragraph("15"))); 



            return table2;
        }

        private Table CreateTable3(string title, List<int> data)
        {
            // Üçüncü tablo
            var table3 = new Table(2)
                .UseAllAvailableWidth()
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            // Kasa Devir
            table3.AddCell(new Cell().Add(new Paragraph("Kasa Devir")));
            table3.AddCell(new Cell().Add(new Paragraph("100 TL")));  

            // Servis
            table3.AddCell(new Cell().Add(new Paragraph("Servis")));
            table3.AddCell(new Cell().Add(new Paragraph("200 TL")));  

            // Tezgah
            table3.AddCell(new Cell().Add(new Paragraph("Tezgah")));
            table3.AddCell(new Cell().Add(new Paragraph("300 TL")));  

            // Pastane
            table3.AddCell(new Cell().Add(new Paragraph("Pastane")));
            table3.AddCell(new Cell().Add(new Paragraph("400 TL")));  

            // Gelir
            table3.AddCell(new Cell().Add(new Paragraph("Gelir")));
            table3.AddCell(new Cell().Add(new Paragraph("1000 TL"))); 

            // Toplam
            table3.AddCell(new Cell().Add(new Paragraph("Toplam")));
            table3.AddCell(new Cell().Add(new Paragraph("1400 TL"))); 


            // Gider
            table3.AddCell(new Cell().Add(new Paragraph("Gider")));
            table3.AddCell(new Cell().Add(new Paragraph("500 TL")));  

            // Devir
            table3.AddCell(new Cell().Add(new Paragraph("Devir")));
            table3.AddCell(new Cell().Add(new Paragraph("200 TL")));  


            return table3;
        }
    }
}
