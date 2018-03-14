using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using System.Web;

namespace Coopers.BusinessLayer.Services.Services
{
    public class PDFExportApplicationService : IPDFExportApplicationService
    {

        #region PRIVATE MEMBERS
   

        #endregion


        #region CONSTRUCTOR

        public PDFExportApplicationService()
        {
          
        }

        #endregion


        #region PUBLIC MEMBERS     

        public byte[] GeneratePDF(string Template)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                StringReader sr = new StringReader(Template);

                Document document = new Document(PageSize.A4, 25, 25, 30, 30);

                HTMLWorker htmlparser = new HTMLWorker(document);

                // Create an instance to the PDF file by creating an instance of the PDF 
                // Writer class using the document and the filestrem in the constructor.

                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                document.AddAuthor("Micke Blomquist");

                document.AddCreator("Sample application using iTextSharp");

                document.AddKeywords("PDF tutorial education");

                document.AddSubject("Document subject - Describing the steps creating a PDF document");

                document.AddTitle("The document title - PDF creation using iTextSharp");

                // Open the document to enable you to write to the document
                document.Open();

                htmlparser.Parse(sr);

                // Add a simple and wellknown phrase to the document in a flow layout manner
                //document.Add(new Paragraph("Hello World!"));

                // Close the document
                document.Close();

                // Close the writer instance
                writer.Close();

                return ms.ToArray();
            }
        }

        #endregion


        #region PRIVATE MEMBERS     


        #endregion

    }
}
