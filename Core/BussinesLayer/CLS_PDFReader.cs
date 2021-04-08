using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BussinesLayer
{
    class CLS_PDFReader
    {
        public string GetText(string FileName)
        {
            try
            {
                PdfReader reader = new PdfReader(FileName);
                int numberPage = reader.NumberOfPages;

                StringBuilder textPages = new StringBuilder();

                for (int i = 0; i < numberPage; i++)
                {
                    ITextExtractionStrategy textExtractionStrategy = new SimpleTextExtractionStrategy();

                    string textPage = PdfTextExtractor.GetTextFromPage(reader, i + 1, textExtractionStrategy);

                    textPage = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(textPage)));

                    textPages.Append(textPage);
                }
                reader.Close();

                return textPages.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
