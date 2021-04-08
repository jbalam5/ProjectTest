using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;

using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace TestCore.BussinesLayer
{
    public class CLS_PDF
    {
        public string GetText_1(string path)
        {
            PDDocument doc = null;
            try
            {
                doc = PDDocument.load(path);
                PDFTextStripper stripper = new PDFTextStripper();
                return stripper.getText(doc);
            }
            finally
            {
                if (doc != null)
                {
                    doc.close();
                }
            }
        }

        public string ReadTextPDF(string path)
        {
            try
            {
                PdfReader reader = new PdfReader(path);
                int numberPage = reader.NumberOfPages;

                StringBuilder textPages = new StringBuilder();

                for (int i = 0; i < numberPage; i++)
                {
                    ITextExtractionStrategy textExtractionStrategy = new iTextSharp.text.pdf.parser.SimpleTextExtractionStrategy();

                    string textPage = PdfTextExtractor.GetTextFromPage(reader, i + 1, textExtractionStrategy);

                    textPage = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(textPage)));

                    textPages.Append(textPage);
                }
                reader.Close();

                string txt = textPages.ToString();

                return txt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetText_2(string path)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (UglyToad.PdfPig.PdfDocument document = UglyToad.PdfPig.PdfDocument.Open(path))
                {
                    foreach (Page page in document.GetPages())
                    {
                        string pageText = page.Text;

                        foreach (Word word in page.GetWords())
                        {
                            sb.AppendLine(word.Text);
                            Console.WriteLine(word.Text);
                        }
                    }
                }

           

                using (UglyToad.PdfPig.PdfDocument document = UglyToad.PdfPig.PdfDocument.Open(path))
                {
                    int pageCount = document.NumberOfPages;

                    // Page number starts from 1, not 0.
                    Page page = document.GetPage(1);
                    
                    double widthInPoints = page.Width;
                    double heightInPoints = page.Height;

                    string text = page.Text;
                }

                return sb.ToString();
            }
            catch(Exception ex) { throw new Exception(ex.Message);  }
        }

        public void getTextToTable(string FileName)
        {
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(FileName);
                iTextSharp.text.Document doc = new iTextSharp.text.Document();
                PdfWriter.GetInstance(doc, new System.IO.FileStream(FileName, System.IO.FileMode.Create));
                doc.Open();
                doc.Add(new iTextSharp.text.Paragraph(sr.ReadToEnd()));
                doc.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public string getText(string FileName)
        {
            try
            {
                PdfReader reader = new PdfReader(FileName);
                int numberPage = reader.NumberOfPages;

                StringBuilder textPages = new StringBuilder();

                for (int i = 0; i < numberPage; i++)
                {
                    ITextExtractionStrategy textExtractionStrategy = new SimpleTextExtractionStrategy();

                    string textPage = PdfTextExtractor.GetTextFromPage(reader, i+1, textExtractionStrategy);

                    textPage = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(textPage)));

                    textPages.Append(textPage);
                }
                reader.Close();

                //string strTextPDF = textPages.ToString().Replace("\n", " ").Replace("\r", " ");
                
                string strTextPDF = textPages.ToString().Replace("\r\n", Environment.NewLine);

                int index = 0;

                foreach (string Encabezado in Core.EntityObject.CLS_Declaracion_Info.labelEncabezados)
                {
      
                    if (strTextPDF.IndexOf(Encabezado) >= 0)
                    {
                        string tmpstr = strTextPDF.Substring(index, strTextPDF.Length - index);

                        do
                        {
                            string[] Fields = GetFields(Encabezado);

                            if (Fields == null) break;

                            foreach (string item in Fields)
                                FormatLine(ref strTextPDF, item, ref index);

                            tmpstr = strTextPDF.Substring(index, strTextPDF.Length - index);

                        } while (tmpstr.IndexOf(Encabezado) >= 0);
                    }
                }

                //ELIMINAMOS LOS ENCABEZADOS
                foreach (var item in Core.EntityObject.CLS_Declaracion_Info.labelEncabezados)
                    strTextPDF = strTextPDF.Replace(item.Replace("\n", " "), "");

                //SE ELIMINA LAS PAGINAS
                strTextPDF = System.Text.RegularExpressions.Regex.Replace(strTextPDF, @"Declaraciones::Página \d de \d", "");
                strTextPDF = System.Text.RegularExpressions.Regex.Replace(strTextPDF, @"Declaraciones::Página \d{2} de \d{2}", "");

                //SE ELIMINA TEXTOS QUE NO SON NECESARIOS
                foreach (string item in Core.EntityObject.CLS_Declaracion_Info.labelExclusiones)
                {
                    if (strTextPDF.Contains(item))
                        strTextPDF = strTextPDF.Replace(item, "");
                }

                return strTextPDF.Replace(":\n", ": ").Replace("\n\r\n", Environment.NewLine);

                /*
                if (strTextPDF.IndexOf("R1 ISR PERSONAS MORALES") > 0)
                {
                    string tmpstr = strTextPDF.Substring(index, strTextPDF.Length - index);
                    do
                    {
                        foreach (string item in Core.EntityObject.CLS_Declaracion_Info.labelDetallesPago)
                        {
                            index = FormatLine(ref strTextPDF, item, index);
                        }

                        tmpstr = strTextPDF.Substring(index, strTextPDF.Length - index);

                    } while (tmpstr.IndexOf("R1 ISR PERSONAS MORALES") > 0);
                }
                */
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); return null; }
        }

        /// <summary>
        /// Convierta el Texto a un DataTable
        /// </summary>
        /// <param name="textPDF"></param>
        /// <returns></returns>
        public DataTable GetData(string textPDF)
        {
            try
            {
                List<string> ListLinetext = textPDF.ToString().Split(new char[] { '\n', '\r', (char)13, (char)10 }, StringSplitOptions.RemoveEmptyEntries).ToList();

                DataTable dtLineText = new DataTable();
                dtLineText.Columns.Add("Id", typeof(int));
                dtLineText.Columns.Add("Text", typeof(string));
                dtLineText.Columns.Add("Value", typeof(string));

               

                for (int i = 0; i < ListLinetext.Count; i++)
                {
                    String textLine = ListLinetext[i].ToString();

                    /*
                    foreach (string item in Core.EntityObject.CLS_Declaracion_Info.labelExclusiones)
                    {
                        if(textLine == item )
                            textLine = textLine.Replace(item, "");

                        if (textLine.Contains(item))
                        {
                            textLine = "";
                            
                            //textLine = System.Text.RegularExpressions.Regex.Replace(textLine, @"\d", "");
                            //textLine = System.Text.RegularExpressions.Regex.Replace(textLine, @"\d{2}", "");
                            //textLine = System.Text.RegularExpressions.Regex.Replace(textLine, @"\d{3}", "");
                            
                        }
                    }*/
                    
                    //si existe alguna linea de texto que se deba excluir, esta reá omitida para guardar en el datatable
                    if (Core.EntityObject.CLS_Declaracion_Info.labelExclusiones.ToList().FindAll((x) => textLine.Contains(x)).Count > 0) continue;

                    if (string.IsNullOrWhiteSpace(textLine)) continue;

                    DataRow row = dtLineText.NewRow();
                    row["Id"] = i;

                    //se recupera el texto y valor por linea
                    string[] columnsLine = Convert.ToString(textLine).Split(':');

                    //se valida para evitar object reference
                    if (columnsLine.Length > 0)
                        row["Text"] = columnsLine[0];

                    if (columnsLine.Length > 1)
                        row["value"] = columnsLine[1].Trim();

                    dtLineText.Rows.Add(row);
                }

                return dtLineText;
            }
            catch(Exception ex) { throw new Exception($"getData: {ex.Message}"); }
        }

        public string[] GetFields(string name)
        {
            try
            {
                switch (name)
                {
                    case "DATOS DE IDENTIFICACIÓN":
                        return Core.EntityObject.CLS_Declaracion_Info.labelDatosIdentificacion;

                    case "DATOS GENERALES":
                        return Core.EntityObject.CLS_Declaracion_Info.labelDatosGenerales;

                    case "PERSONAS MORALES RÉGIMEN GENERAL":
                        return Core.EntityObject.CLS_Declaracion_Info.labelDeterminacionISR;

                    case "R1 ISR PERSONAS MORALES":
                        return Core.EntityObject.CLS_Declaracion_Info.labelR1ISRPERSONASMORALES;
                        
                    case "R13 ISR RETENCIONES POR ASIMILADOS A SALARIOS":
                    case "R14 ISR RETENCIONES POR SERVICIOS PROFESIONALES":
                    case "R18 ISR RETENCIONES POR PAGOS AL EXTRANJERO":
                        return Core.EntityObject.CLS_Declaracion_Info.labelDetallesPagoR31418;

                    case "MONTOS DE LOS ACTOS O ACTIVIDADES PAGADOS":
                        return Core.EntityObject.CLS_Declaracion_Info.labelMontosDeActosoActividadesPagadas;

                    case "DETALLE DEL TOTAL DE LOS ACTOS O ACTIVIDADES PAGADOS A LA TASA DEL 16% DE IVA":
                        return Core.EntityObject.CLS_Declaracion_Info.labelTotalActosoActividadesPagadasTasa16;

                    case "DETERMINACIÓN DEL IMPUESTO AL VALOR AGREGADO ACREDITABLE":
                        return Core.EntityObject.CLS_Declaracion_Info.labelDeterminacionIVAAcreditable;

                    case "DETERMINACIÓN DE IMPUESTO AL VALOR AGREGADO":
                        return Core.EntityObject.CLS_Declaracion_Info.labelDetermionacionIVA;

                    case "DETALLE DEL VALOR DE LOS ACTOS O ACTIVIDADES GRAVADOS A LA TASA DEL 16%":
                        return Core.EntityObject.CLS_Declaracion_Info.labelDetalleActosoActGrabadasTasa16;
                        
                    case "R21 IMPUESTO AL VALOR AGREGADO":
                        return Core.EntityObject.CLS_Declaracion_Info.labelR21_IVA;

                    case "R24 IVA RETENCIONES":
                        return Core.EntityObject.CLS_Declaracion_Info.labelR24IVARETENCIONES;

                    default:
                        return null;
                }
            }
            catch (Exception ex) { Console.WriteLine($"getFields: { ex.Message}"); return null; }
        }
        /// <summary>
        /// Se devuelve una nueva linea con la coincidencia del valor buscado
        /// </summary>
        /// <param name="text"></param>
        /// <param name="oldValue"></param>
        /// <returns>Nueva linea con saltos de linea</returns>
        public string FormatLine(string text, string oldValue)
        {
            try
            {
                int index = text.ToString().IndexOf(oldValue);
                //SE OBTIENE LA CADENA EL TODO EL STRING DEL PDF Y SE REEMPLAZA LOS SALTOS DE LINEA, DE LA LINEA EN LA QUE SE ENCUENTRE LA CADENA BUSCADA
                //SE AGREGA UN SALTO DE LINEA ANTES DEL NUEVO VALOR DE LA CADENA, EN CASO DE QUE EXISTA EN UNA LINEA DOS COLUMNAS
                string newValue = $"{Environment.NewLine}{text.ToString().Substring(index, oldValue.Length).Replace("\n", " ").Replace("\r", "")}:";
                
                return text.Remove(index, oldValue.Length).Insert(index, newValue);
            }
            catch(Exception ex)
            {
                throw new Exception($"formatLine: {ex.Message}");
            }
        }

        public void FormatLine(ref string allTextPDF, string oldValue, ref int startAt)
        {
            try
            {
                string subText = allTextPDF.Substring(startAt, allTextPDF.Length - startAt);

                int index = subText.ToString().IndexOf(oldValue);

                if (index < 0) return;

                //SE OBTIENE LA CADENA EL TODO EL STRING DEL PDF Y SE REEMPLAZA LOS SALTOS DE LINEA, DE LA LINEA EN LA QUE SE ENCUENTRE LA CADENA BUSCADA
                string newValue = subText.ToString().Substring(index, oldValue.Length).Replace("\n", " ").Replace("\r", "");
                
                //SE AGREGA UN SALTO DE LINEA ANTES DEL NUEVO VALOR DE LA CADENA, EN CASO DE QUE EXISTA EN UNA LINEA DOS COLUMNAS
                newValue = $"{Environment.NewLine}{newValue}:";

                //SE ELIMINA EL TEXTO QUE COINCIDIÓ CON LA BUSQUEDA Y SE REEMPLAZA CON EL NUEVO VALOR
                allTextPDF = allTextPDF.Remove(startAt + index, oldValue.Length).Insert(startAt + index, newValue);

                startAt = startAt+index+newValue.Length;
            }
            catch (Exception ex)
            {
                throw new Exception($"formatLine: {ex.Message}");
            }
        }

        public void FormatLineV2(ref string allTextPDF, string oldValue, ref int startAt)
        {
            try
            {
                string subText = allTextPDF.Substring(startAt, allTextPDF.Length - startAt);

                //SE OBTIENE LA CADENA EN TODO EL STRING DEL PDF Y SE REEMPLAZA LOS SALTOS DE LINEA, DE LA LINEA EN LA QUE SE ENCUENTRE LA CADENA BUSCADA
                int index = subText.ToString().IndexOf(oldValue.Replace("\n", " "));
                
                //SE AGREGA UN SALTO DE LINEA ANTES DEL NUEVO VALOR DE LA CADENA, EN CASO DE QUE EXISTA EN UNA LINEA DOS COLUMNAS
                string newValue = $"{Environment.NewLine}{subText.ToString().Substring(index, oldValue.Replace("\n", " ").Length)}:";

                if (index < 0) return;
                
                //SE ELIMINA EL TEXTO QUE COINCIDIÓ CON LA BUSQUEDA Y SE REEMPLAZA CON EL NUEVO VALOR
                allTextPDF = allTextPDF.Remove(startAt + index, oldValue.Length).Insert(startAt + index, newValue);

                startAt = startAt + index + newValue.Length;
            }
            catch (Exception ex)
            {
                throw new Exception($"formatLine: {ex.Message}");
            }
        }

        /// <summary>
        /// Se devuelve una nueva linea con la coincidencia del valor buscado
        /// </summary>
        /// <param name="text"></param>
        /// <param name="oldValue"></param>
        /// <returns>Nueva linea con saltos de linea</returns>
        public string FormatLinev2(string text, string oldValue)
        {
            try
            { 
                int index = text.ToString().IndexOf(oldValue.Replace("\n", " "));
            
                //SE OBTIENE LA CADENA EL TODO EL STRING DEL PDF Y SE REEMPLAZA LOS SALTOS DE LINEA, DE LA LINEA EN LA QUE SE ENCUENTRE LA CADENA BUSCADA
                //SE AGREGA UN SALTO DE LINEA ANTES DEL NUEVO VALOR DE LA CADENA, EN CASO DE QUE EXISTA EN UNA LINEA DOS COLUMNAS
                string newValue = $"{Environment.NewLine}{text.ToString().Substring(index, oldValue.Replace("\n", " ").Length)}:";

                return text.Remove(index, oldValue.Length).Insert(index, newValue);
            }
            catch (Exception ex)
            {
                throw new Exception($"formatLine: {ex.Message}");
            }
        }


    }
}
