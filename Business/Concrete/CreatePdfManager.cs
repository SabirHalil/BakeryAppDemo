using Business.Abstract;
using Entities.DTOs;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.Kernel.Font;
using Entities.Concrete;
using Business.AbstractAPI;
using System.Globalization;


namespace Business.Concrete
{
    public class CreatePdfManager : ICreatePdfService
    {
        private IProductionListService _productionListService;
        private IProductionListDetailService _productionListDetailService;
        private IProductsCountingService _productsCountingService;
        private IStaleProductService _staleProductService;

        private IDoughFactoryAPIService _doughFactoryAPIService;
        private IDoughFactoryProductService _doughFactoryProductService;

        private IBreadPriceService _breadPriceService;

        private IMarketEndOfDayService _marketEndOfDayService;
        public CreatePdfManager(IMarketEndOfDayService marketEndOfDayService,
            IBreadPriceService breadPriceService,
            IDoughFactoryProductService doughFactoryProductService, IDoughFactoryAPIService doughFactoryAPIService,
            IStaleProductService staleProductService,
            IProductsCountingService productsCountingService, IProductionListService productionListService, IProductionListDetailService productionListDetailService)
        {
            _marketEndOfDayService = marketEndOfDayService;

            _breadPriceService = breadPriceService;

            _doughFactoryProductService = doughFactoryProductService;
            _doughFactoryAPIService = doughFactoryAPIService;

            _staleProductService = staleProductService;
            _productsCountingService = productsCountingService;
            _productionListService = productionListService;
            _productionListDetailService = productionListDetailService;
        }


        public byte[] EndOfDayAccountCreatePdf(DateTime date, EndOfDayResult endOfDayResult, decimal ProductsSoldInTheBakery)
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



                        formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));
                        var dayOfWeek = date.ToString("dddd", new CultureInfo("tr-TR"));

                        var date2 = new Paragraph($"Tarih: {formattedDate} - {dayOfWeek}")
                            .SetTextAlignment(TextAlignment.RIGHT)
                           ;

                        document.Add(date2);


                        var companyName = "ASLANOĞLU Fırın";
                        var company = new Paragraph(companyName)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(16);

                        document.Add(company);


                        var title = new Paragraph("Gün Sonu Hesap")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(16);

                        document.Add(title);



                        (Table table1, Table table2, double TotalBread) = CreateTable1(endOfDayResult.EndOfDayAccount);
                        var table3 = CreateTable3((decimal)TotalBread * endOfDayResult.EndOfDayAccount.Price, endOfDayResult.Account, endOfDayResult.PastaneRevenue);

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


                        var giderlerTable = GiderlerCreateTable("Giderler", new List<int> { 1, 2, 3 });
                        giderlerTable.SetWidth(UnitValue.CreatePercentValue(33));



                        document.Add(giderlerTable);

                        var KrediKartıAmount = 500;
                        var NetEldenAmount = 1000;

                        var KrediKartı = new Paragraph($"Kredi Kartı: {KrediKartıAmount}TL")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetFontSize(16);
                        document.Add(KrediKartı);

                        var NetElden = new Paragraph($"Net Elden: {NetEldenAmount}TL")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetFontSize(16);
                        document.Add(NetElden);

                        var Total = new Paragraph($"Toplam: {NetEldenAmount + KrediKartıAmount}TL")
                           .SetTextAlignment(TextAlignment.RIGHT)
                           .SetFontSize(16);
                        document.Add(Total);

                        document.Close();
                    }
                }


                //stream.Seek(0, SeekOrigin.Begin);

                // Dosyayı döndür
                // return File(stream.ToArray(), "application/pdf", $"GünSonuHesap_{formattedDate}.pdf");

                return stream.ToArray();
            }
        }

        public byte[] CreatePdfForHamurhane(DateTime date)
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


                        formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));
                        var dayOfWeek = date.ToString("dddd", new CultureInfo("tr-TR"));

                        var date2 = new Paragraph($"Tarih: {formattedDate} - {dayOfWeek}")
                            .SetTextAlignment(TextAlignment.RIGHT);
                        document.Add(date2);

                        var companyName = "ASLANOĞLU Fırın";
                        var company = new Paragraph(companyName)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(16);
                        document.Add(company);


                        var title = new Paragraph("Hamurhane")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(16);

                        document.Add(title);

                        var data = RegularDataForHamurhane(date);

                        var Table = CreateTableForHamurhane(data.Item1);

                        //Table.SetWidth(UnitValue.CreatePercentValue(33));

                        document.Add(Table);



                        var TotalRevenueAmount = data.Item2;

                        var TotalRevenue = new Paragraph($"Toplam Gelir:  {TotalRevenueAmount}TL")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetFontSize(16);
                        document.Add(TotalRevenue);

                        document.Close();
                    }
                }

                return stream.ToArray();
            }
        }
        public byte[] CreatePdfForMarketService(DateTime date)
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


                        formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));
                        var dayOfWeek = date.ToString("dddd", new CultureInfo("tr-TR"));

                        var date2 = new Paragraph($"Tarih: {formattedDate} - {dayOfWeek}")
                            .SetTextAlignment(TextAlignment.RIGHT);
                        document.Add(date2);

                        var companyName = "ASLANOĞLU Fırın";
                        var company = new Paragraph(companyName)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(16);
                        document.Add(company);


                        var title = new Paragraph("Market Servis")
                            .SetTextAlignment(TextAlignment.CENTER)
                            .SetFontSize(16);

                        document.Add(title);

                        var data = RegularDataForHamurhane(date);

                        var Table = CreateTableForHamurhane(data.Item1);




                        //Table.SetWidth(UnitValue.CreatePercentValue(33));

                        document.Add(Table);



                        var TotalRevenueAmount = data.Item2;

                        var TotalRevenue = new Paragraph($"Toplam Gelir:  {TotalRevenueAmount}TL")
                            .SetTextAlignment(TextAlignment.RIGHT)
                            .SetFontSize(16);
                        document.Add(TotalRevenue);

                        document.Close();
                    }
                }

                return stream.ToArray();
            }
        }

        private static Dictionary<int, string> CategoryNameByCategoryId = new Dictionary<int, string>()
        {
            { 1, "Pastane" },
            { 2, "Börek" },
            { 3, "Dışarıda Alınan Ürünler" },
        };

        public byte[] CreatePdf(DateTime date)
        {
            using (var stream = new MemoryStream())
            {
                string formattedDate;
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        int CategoryId = 0;

                        PdfFont font = PdfFontFactory.CreateFont("Helvetica", "CP1254", PdfFontFactory.EmbeddingStrategy.FORCE_NOT_EMBEDDED);

                        var document = new Document(pdf);
                        document.SetFont(font);


                        formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));
                        var dayOfWeek = date.ToString("dddd", new CultureInfo("tr-TR"));

                        var date2 = new Paragraph($"Tarih: {formattedDate} - {dayOfWeek}")
                            .SetTextAlignment(TextAlignment.RIGHT);
                        document.Add(date2);

                        var companyName = "ASLANOĞLU Fırın";
                        var company = new Paragraph(companyName)
                            .SetTextAlignment(TextAlignment.LEFT)
                            .SetFontSize(16);
                        document.Add(company);

                        decimal AllProductTotalRevenue = 0;

                        for (int i = 0; i < 3; i++)
                        {
                            CategoryId = i + 1;

                            var title = new Paragraph($"{CategoryNameByCategoryId[CategoryId]}")
                                .SetTextAlignment(TextAlignment.LEFT)
                                .SetFontSize(16);

                            document.Add(title);

                            List<ProductionListDetailDto> detail = RegularData(date, CategoryId);

                            if (detail.Count > 0)
                            {
                                var Table = CreateTable(detail);

                                //Table.SetWidth(UnitValue.CreatePercentValue(33));

                                document.Add(Table);

                                decimal TotalRevenueAmount = 0;

                                foreach (var item in detail)
                                {
                                    TotalRevenueAmount += (item.ProductedToday + item.RemainingYesterday - item.StaleProductToday - item.RemainingToday) * item.Price;
                                }


                                AllProductTotalRevenue += TotalRevenueAmount;

                                var TotalRevenue = new Paragraph($"Toplam Gelir:  {TotalRevenueAmount}TL")
                                    .SetTextAlignment(TextAlignment.RIGHT)
                                    .SetFontSize(16);

                                document.Add(TotalRevenue);
                            }
                            else
                            {
                                document.Add(new Paragraph("Bugün eklenen ürün yok maalesef."));
                            }



                        }

                        var AllProductTotalRevenueText = new Paragraph($"Tüm Ürünlerin Toplam Geliri:  {AllProductTotalRevenue}TL")
                                   .SetTextAlignment(TextAlignment.RIGHT)
                                   .SetFontSize(16);

                        document.Add(AllProductTotalRevenueText);

                        document.Close();
                    }
                }

                return stream.ToArray();
            }

        }

        // -------------------------   Data Func  ------------------------------
        public List<ProductionListDetailDto> RegularData(DateTime date, int categoryId)
        {

            var listId = _productionListService.GetByDateAndCategoryId(date, categoryId);
            List<GetAddedProductsDto> productionListDetail = _productionListDetailService.GetProductsByListId(listId);


            Dictionary<int, int> productsCountingToday = _productsCountingService.GetDictionaryProductsCountingByDateAndCategory(date, categoryId);


            Dictionary<int, int> productsCountingYesterday = _productsCountingService.GetDictionaryProductsCountingByDateAndCategory(date.AddDays(-1), categoryId);


            Dictionary<int, int> staleProducts = _staleProductService.GetStaleProductsByDateAndCategory(date, categoryId);



            List<int> productIds = productionListDetail.Select(pd => pd.ProductId).ToList();
            productIds.AddRange(productsCountingYesterday.Keys.Except(productIds));


            var productionListDetailDto = productIds.Select(productId =>
            {
                var specificProduct = productionListDetail.FirstOrDefault(pd => pd.ProductId == productId);

                return new ProductionListDetailDto
                {
                    ProductId = productId,
                    ProductName = specificProduct?.ProductName,
                    ProductedToday = specificProduct?.Quantity ?? 0,
                    Price = specificProduct?.Price ?? 0,
                    RemainingToday = productsCountingToday.TryGetValue(productId, out var todayValue) ? todayValue : 0,
                    RemainingYesterday = productsCountingYesterday.TryGetValue(productId, out var yesterdayValue) ? yesterdayValue : 0,
                    StaleProductToday = staleProducts.TryGetValue(productId, out var staleValue) ? staleValue : 0
                };
            }).ToList();


            return productionListDetailDto;
        }

        public (List<DoughFactoryListAndDetailDto>, decimal) RegularDataForHamurhane(DateTime date)
        {

            decimal breadPrice = _breadPriceService.BreadPriceByDate(date);


            List<DoughFactoryListDto> doughFactoryListDto = _doughFactoryAPIService.GetByDateDoughFactoryList(date);


            List<GetAddedDoughFactoryListDetailDto> AllDoughFactoryProducts = new();
            for (int i = 0; i < doughFactoryListDto.Count; i++)
            {
                List<GetAddedDoughFactoryListDetailDto> getAddedDoughFactoryListDetailDto
                    = _doughFactoryAPIService.GetDoughFactoryListDetail(doughFactoryListDto[i].Id);

                AllDoughFactoryProducts.AddRange(getAddedDoughFactoryListDetailDto);
            }


            List<int> uniqueProductIds = AllDoughFactoryProducts.Select(dto => dto.DoughFactoryProductId).Distinct().ToList();



            List<DoughFactoryListAndDetailDto> doughFactoryListAndDetailDto = new();

            decimal TotalRevenue = 0;
            foreach (var u in uniqueProductIds)
            {
                DoughFactoryListAndDetailDto d = new();
                d.DoughFactoryProductQuantity = new Dictionary<string, int>();

                int TotalQuantity = 0;

                for (int j = 0; j < doughFactoryListDto.Count; j++)
                {
                    string dynamicName = "Hamur" + (j + 1).ToString();

                    var result = AllDoughFactoryProducts.FirstOrDefault(dto => dto.DoughFactoryProductId == u && dto.DoughFactoryListId == doughFactoryListDto[j].Id);

                    d.DoughFactoryProductQuantity[dynamicName] = result == null ? 0 : result.Quantity;
                    TotalQuantity += d.DoughFactoryProductQuantity[dynamicName];
                }


                DoughFactoryProduct doughFactoryProduct = _doughFactoryProductService.GetById(u);


                d.Name = doughFactoryProduct.Name;
                d.UnitPrice = (breadPrice * (decimal)doughFactoryProduct.BreadEquivalent);
                d.TotalQuantity = TotalQuantity;

                TotalRevenue += d.UnitPrice * d.TotalQuantity;
                doughFactoryListAndDetailDto.Add(d);
            }


            return (doughFactoryListAndDetailDto, TotalRevenue);
        }
        public void RegularDataForMarketService(DateTime date)
        {
            //_marketEndOfDayService.MarketEndOfDayDetail(date);
        }

        public class DoughFactoryListAndDetailDto
        {
            public Dictionary<string, int> DoughFactoryProductQuantity { get; set; }
            public string Name { get; set; }
            public decimal UnitPrice { get; set; }
            public int TotalQuantity { get; set; }
        }



        // -------------------------   CreateTable Func  ------------------------------

        public Table CreateTable(List<ProductionListDetailDto> data)
        {
            var table = new Table(9)
                .UseAllAvailableWidth()
                .SetHorizontalAlignment(HorizontalAlignment.LEFT);


            table.AddHeaderCell(new Cell().Add(new Paragraph("Ürün Adı")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Dünden Kalan")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("İmal Edilen")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Toplam Net")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Bayata Atılan")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Net")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Satılan")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Fiyatı")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Gelir")));




            foreach (var item in data)
            {
                var ToplamNet = item.RemainingYesterday + item.ProductedToday;
                var Net = ToplamNet - item.StaleProductToday;
                var Satilan = ToplamNet - item.RemainingToday;


                table.AddCell(new Cell().Add(new Paragraph(item.ProductName)));
                table.AddCell(new Cell().Add(new Paragraph(item.RemainingYesterday.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(item.ProductedToday.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(ToplamNet.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(item.StaleProductToday.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(Net.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(Satilan.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(item.Price.ToString() + "TL")));
                table.AddCell(new Cell().Add(new Paragraph((item.Price * Satilan).ToString() + "TL")));
            }

            return table;
        }

        public Table CreateTableForHamurhane(List<DoughFactoryListAndDetailDto> data)
        {
            var table = new Table(data[0].DoughFactoryProductQuantity.Count + 4)
                .UseAllAvailableWidth()
                .SetHorizontalAlignment(HorizontalAlignment.LEFT);


            table.AddHeaderCell(new Cell().Add(new Paragraph("İmal Edilen Ürünler")));

            foreach (var item in data[0].DoughFactoryProductQuantity.Keys)
            {
                table.AddHeaderCell(new Cell().Add(new Paragraph($"{item}")));
            }

            table.AddHeaderCell(new Cell().Add(new Paragraph("Toplam Adet")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Birim Fiyatı")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Gelir")));



            foreach (var item in data)
            {
                var Gelir = item.UnitPrice * item.TotalQuantity;

                table.AddCell(new Cell().Add(new Paragraph(item.Name)));

                foreach (var value in item.DoughFactoryProductQuantity.Values)
                {
                    table.AddCell(new Cell().Add(new Paragraph(value == 0 ? "-" : value.ToString())));
                }

                table.AddCell(new Cell().Add(new Paragraph(item.TotalQuantity.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(item.UnitPrice.ToString() + "TL")));
                table.AddCell(new Cell().Add(new Paragraph(Gelir.ToString() + "TL")));
            }

            return table;
        }
        public Table CreateTableForMarketService(List<DoughFactoryListAndDetailDto> data)
        {
            var table = new Table(data[0].DoughFactoryProductQuantity.Count + 4)
                .UseAllAvailableWidth()
                .SetHorizontalAlignment(HorizontalAlignment.LEFT);


            table.AddHeaderCell(new Cell().Add(new Paragraph("Market Adı")));

            foreach (var item in data[0].DoughFactoryProductQuantity.Keys)
            {
                table.AddHeaderCell(new Cell().Add(new Paragraph($"{item}")));
            }

            table.AddHeaderCell(new Cell().Add(new Paragraph("Toplam Verilen")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Bayat")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Toplam Satış")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Toplam Para")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Alınan Para")));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Kalan Para")));



            foreach (var item in data)
            {
                var Gelir = item.UnitPrice * item.TotalQuantity;

                table.AddCell(new Cell().Add(new Paragraph(item.Name)));

                foreach (var value in item.DoughFactoryProductQuantity.Values)
                {
                    table.AddCell(new Cell().Add(new Paragraph(value == 0 ? "-" : value.ToString())));
                }

                table.AddCell(new Cell().Add(new Paragraph(item.TotalQuantity.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(item.UnitPrice.ToString() + "TL")));
                table.AddCell(new Cell().Add(new Paragraph(Gelir.ToString() + "TL")));

                table.AddCell(new Cell().Add(new Paragraph(item.TotalQuantity.ToString())));
                table.AddCell(new Cell().Add(new Paragraph(item.UnitPrice.ToString() + "TL")));
                table.AddCell(new Cell().Add(new Paragraph(Gelir.ToString() + "TL")));
            }

            return table;
        }


        public Table GiderlerCreateTable(string title, List<int> data)
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

        private (Table Table1, Table Table2, double TotalBread) CreateTable1(EndOfDayAccountForBread endOfDayAccountForBread)
        {
            var table1 = new Table(2)
                          .UseAllAvailableWidth()
                          .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            var table2 = new Table(2)
                          .UseAllAvailableWidth()
                          .SetHorizontalAlignment(HorizontalAlignment.CENTER);


            var totalTable1 = endOfDayAccountForBread.RemainingToday +
                endOfDayAccountForBread.TotalBreadGivenToService +
                endOfDayAccountForBread.TotalStaleBreadFromService +
                endOfDayAccountForBread.EatenBread +
                endOfDayAccountForBread.StaleBreadToday +
                endOfDayAccountForBread.TotalBreadGivenToGetir;


            // Başlık satırı
            table1.AddCell(new Cell().Add(new Paragraph("Yarına Kalan Ekmek")));
            table1.AddCell(new Cell().Add(new Paragraph($"{endOfDayAccountForBread.RemainingToday}")));

            // Servis Ekmek
            table1.AddCell(new Cell().Add(new Paragraph("Servis Ekmek")));
            table1.AddCell(new Cell().Add(new Paragraph($"{endOfDayAccountForBread.TotalBreadGivenToService}")));

            // Servis Bayat
            table1.AddCell(new Cell().Add(new Paragraph("Servis Bayat")));
            table1.AddCell(new Cell().Add(new Paragraph($"{endOfDayAccountForBread.TotalStaleBreadFromService}")));

            // Yenen Ekmek
            table1.AddCell(new Cell().Add(new Paragraph("Yenen Ekmek")));
            table1.AddCell(new Cell().Add(new Paragraph($"{endOfDayAccountForBread.EatenBread}")));

            // Bayat
            table1.AddCell(new Cell().Add(new Paragraph("Bayat")));
            table1.AddCell(new Cell().Add(new Paragraph($"{endOfDayAccountForBread.StaleBreadToday}")));

            // Getir
            table1.AddCell(new Cell().Add(new Paragraph("Getir")));
            table1.AddCell(new Cell().Add(new Paragraph($"{endOfDayAccountForBread.TotalBreadGivenToGetir}")));

            // Boş satır
            table1.AddCell(new Cell(1, 2).SetHeight(18));

            // Toplam
            table1.AddCell(new Cell().Add(new Paragraph("Toplam")));
            table1.AddCell(new Cell().Add(new Paragraph($"{totalTable1}")));


            // Tablo 2 -------------

            var totalTable2 = endOfDayAccountForBread.ProductedToday +
                endOfDayAccountForBread.RemainingYesterday +
                endOfDayAccountForBread.PurchasedBread;

            // İmalat
            table2.AddCell(new Cell().Add(new Paragraph("İmalat")));
            table2.AddCell(new Cell().Add(new Paragraph($"{endOfDayAccountForBread.ProductedToday}")));

            // Dünden Kalan
            table2.AddCell(new Cell().Add(new Paragraph("Dünden Kalan")));
            table2.AddCell(new Cell().Add(new Paragraph($"{endOfDayAccountForBread.RemainingYesterday}")));

            // Dışarıdan Alınan
            table2.AddCell(new Cell().Add(new Paragraph("Dışarıdan Alınan")));
            table2.AddCell(new Cell().Add(new Paragraph($"{endOfDayAccountForBread.PurchasedBread}")));

            // Toplam
            table2.AddCell(new Cell().Add(new Paragraph("Toplam")));
            table2.AddCell(new Cell().Add(new Paragraph($"{totalTable2}")));

            // Boş satır
            table2.AddCell(new Cell(1, 2).SetHeight(18));
            table2.AddCell(new Cell(1, 2).SetHeight(18));
            table2.AddCell(new Cell(1, 2).SetHeight(18));

            // Tezgahta Satılan Ekmek
            table2.AddCell(new Cell().Add(new Paragraph("Tezgahta Satılan Ekmek")));
            table2.AddCell(new Cell().Add(new Paragraph($"{totalTable2 - totalTable1}")));



            return (table1, table2, totalTable2 - totalTable1);
        }


        private Table CreateTable3(decimal TotalBread, Account account, decimal PastaneRevenue)
        {
            // Üçüncü tablo
            var table3 = new Table(2)
                .UseAllAvailableWidth()
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);

            // Kasa Devir
            table3.AddCell(new Cell().Add(new Paragraph("Kasa Devir")));
            table3.AddCell(new Cell().Add(new Paragraph($"{account.KasaDevir} TL")));

            // Servis
            table3.AddCell(new Cell().Add(new Paragraph("Servis")));
            table3.AddCell(new Cell().Add(new Paragraph($"{account.ServisGelir} TL")));

            // Tezgah
            table3.AddCell(new Cell().Add(new Paragraph("Tezgah")));
            table3.AddCell(new Cell().Add(new Paragraph($"{TotalBread} TL")));

            // Pastane
            table3.AddCell(new Cell().Add(new Paragraph("Pastane")));
            table3.AddCell(new Cell().Add(new Paragraph($"{PastaneRevenue} TL")));

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
            table3.AddCell(new Cell().Add(new Paragraph($"{account.Devir} TL")));


            return table3;
        }


    }

}
