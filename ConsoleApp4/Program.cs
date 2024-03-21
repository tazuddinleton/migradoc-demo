using ConsoleApp4;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

class Program
{
        private static string OutputPath = "C:\\Users\\User\\RiderProjects\\ConsoleApp4\\ConsoleApp4\\Output";
    static void Main(string[] args)
    {
        GlobalFontSettings.FontResolver = new CustomFontResolver();
        // Step 1: Create a new MigraDoc document
        Document document = new Document();
        document.Info.Title = "My PDF Document";
        document.Info.Author = "Your Name";

// Step 2: Create a section and define page setup
        Section section = document.AddSection();
        section.PageSetup.OddAndEvenPagesHeaderFooter = true;
        section.PageSetup.StartingNumber = 1;


// Step 3: Add a table to the section
        Table table = section.AddTable();
        table.Borders.Color = Colors.Black;
        table.Borders.Width = 0.25;

// Step 4: Add columns to the table
        Column column = table.AddColumn(Unit.FromInch(1));
        column = table.AddColumn(Unit.FromInch(2));
        column = table.AddColumn(Unit.FromInch(2));
        column = table.AddColumn(Unit.FromInch(2));

// Step 5: Add rows and cells to the table
        Row row = table.AddRow();
        row.Cells[0].AddParagraph("ID");
        row.Cells[1].AddParagraph("Name");
        row.Cells[2].AddParagraph("Email");
        row.Cells[3].AddParagraph("Phone");

// Add more rows and data as needed
        for (int i = 1; i <= 100; i++)
        {
            row = table.AddRow();
            row.Cells[0].AddParagraph(i.ToString());
            row.Cells[1].AddParagraph("Name " + i);
            row.Cells[2].AddParagraph("email" + i + "@example.com");
            row.Cells[3].AddParagraph("(123) 456-789"  + i);
        }

// Step 6: Add pagination to the table

// Step 7: Render the document to a PDF file
        PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
        renderer.Document = document;
        renderer.RenderDocument();

     
        
        // Add headers and footers to the PDF document
        PdfDocument pdfDocument = renderer.PdfDocument;
        XFont font = new XFont("Arial", 10, XFontStyleEx.Regular);

        for (int pageNumber = 0; pageNumber < pdfDocument.PageCount; pageNumber++)
        {
                PdfPage page = pdfDocument.Pages[pageNumber];
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Draw header
                XRect headerRect = new XRect(0, 0, page.Width, 60);
                gfx.DrawString("My Header - Page: " + (pageNumber + 1), font, XBrushes.Black, headerRect, XStringFormats.Center);

                // Draw footer
                XRect footerRect = new XRect(0, page.Height - 50, page.Width, 30);
                gfx.DrawString("My Footer - www.example.com", font, XBrushes.Black, footerRect, XStringFormats.Center);
        }
        
        renderer.PdfDocument.Save(Path.Combine(OutputPath, "output.pdf"));
    }
    
    
    
}