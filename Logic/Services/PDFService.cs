using iTextSharp.text;
using iTextSharp.text.pdf;
using Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.AcroFields;

namespace Logic.Services
{
    public interface IPDFService
    {
        MemoryStream DownloadFileBase();
        MemoryStream DownloadMovingsPDF(Search search);
    }
    public class PDFService : IPDFService
    {
        private static BaseFont basefont = BaseFont.CreateFont(Path.GetFullPath("Fonts/GISHA.ttf"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

        private Image GIF = Image.GetInstance("Imgs/logo.JPG");
        private IMovingService movingService;
        public PDFService(IMovingService movingService)
        {
            this.movingService = movingService;
        }

        public MemoryStream DownloadMovingsPDF(Search search)
        {
            PdfPTable title = CreateTitle("תנועות");
            title.WidthPercentage = 100;
            PdfPTable table = CreateTableMovings(search);
            table.WidthPercentage = 100;
            var stream = CreatePdfDocument(title, table);

            return stream;
        }

        private PdfPTable CreateTableMovings(Search search)
        {
            var data = movingService.GetMovings(search, 0);//להביא את userId!!!!
            PdfPTable table = new PdfPTable(6);
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            //כותרות של הטבלה
            AddCell(table, "תאריך");
            AddCell(table, "סה''כ");
            AddCell(table, "סוג");
            AddCell(table, " ");
            AddCell(table, " ");
            AddCell(table, " ");

            foreach (var item in data)
            {

                AddCell(table, item.Date.ToString());
                AddCell(table, item.Sum.ToString());
                AddCell(table, " ",18,1);
                AddCell(table, " ");
                AddCell(table, " ");
                AddCell(table, " ");

            }

            return table;
        }


        public MemoryStream DownloadFileBase()
        {
            PdfPTable title = CreateTitle("כותרת ראשית");
            title.WidthPercentage = 100;
            PdfPTable table = CreateTableBase();
            table.WidthPercentage = 100;
            var stream = CreatePdfDocument(title, table);

            return stream;
        }

        private PdfPTable CreateTitle(string header, bool isSpace = false)
        {
            Font gisha = new Font(basefont, 20, Font.BOLD, BaseColor.BLACK);

            PdfPTable title = new PdfPTable(1);
            title.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            PdfPCell cell = new PdfPCell(new Phrase(header, gisha));
            cell.Border = 0;
            PdfPCell space = new PdfPCell(new Phrase(" ", gisha));
            space.Border = 0;
            title.AddCell(cell);
            title.AddCell(space);

            return title;
        }

        private PdfPTable CreateTableBase()
        {
            PdfPTable table = new PdfPTable(1);
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;


            AddCell(table, " ");
            AddCell(table, "שלח את תהטקסט לכאן");
            AddCell(table, " הטקסט שלך כאן");
            AddCell(table, " ");
            AddCell(table, " בברכה");
            return table;

        }

        private MemoryStream CreatePdfDocument(PdfPTable title, PdfPTable table = null, List<PdfPTable> list = null)
        {
            var document = new iTextSharp.text.Document(new Rectangle(288f, 144f), 20, 50, 20, 50);
            document.SetPageSize(PageSize.A4);

            var stream = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, stream);

            title.WidthPercentage = 100;
            if (table != null)
            {
                table.WidthPercentage = 100;
            }

            document.Open();

            document.Add(GIF);

            document.Add(title);
            document.Add(new Paragraph("   "));
            if (table != null)
            {
                document.Add(table);
            }
            if (list != null)
            {
                foreach (var item in list)
                {
                    item.WidthPercentage = 100;
                    document.Add(new Paragraph("   "));
                    document.Add(item);
                    document.Add(new Paragraph("   "));
                }
            }

            document.Add(new Paragraph("   "));
            document.Close();


            return stream;
        }

        private void AddCell(PdfPTable table, string value, int size = 14, int isBold = 0)
        {
            Font gisha = new Font(basefont, size, isBold, BaseColor.BLACK);
            PdfPCell cell = new PdfPCell(new Phrase(value, gisha));
            cell.Border = 0;
            table.AddCell(cell);
        }


    }
}
