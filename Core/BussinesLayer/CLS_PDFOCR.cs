using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.util;
using DevExpress.Pdf;
//using iText.Kernel.Geom;
//using iText.Kernel.Pdf.Canvas.Parser;
//using iText.Kernel.Pdf.Canvas.Parser.Listener;


namespace TestCore.BussinesLayer
{
    public class CLS_PDFOCR
    {
        private Dictionary<string, string[]> declarationDictionary;
        private string[] headers;
        private Newtonsoft.Json.Linq.JObject StructureJSON;

        public string Load(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path)) throw new Exception("No se ha seleccionado un archivo válido");
                if (!System.IO.File.Exists(path)) throw new Exception("El archivo seleccionado no existe.");

                LoadDocumentStructure();
                //Coordenadas(path);
                //GetStructureTablePDF(path, "template.xml");
                //GetTextTable(path);
                string TextFile = PDFToRAWText(path);
                string TextFile2 = FormatLines(TextFile);

                return TextFile2;
            }
            catch(Exception ex) { return ex.Message; }
        }

        private void LoadDocumentStructure()
        {
            try
            {
                string StructureString = System.IO.File.ReadAllText(@"E:\NASA\NASANET2017\ContaNET_Git\structure\BanorteStructure2021.json");

                StructureJSON = Newtonsoft.Json.Linq.JObject.Parse(StructureString);
                headers = StructureJSON.Properties().Select(p => p.Name).ToArray();

                if (headers != null)
                {
                    declarationDictionary = new Dictionary<string, string[]>();
                    foreach (var Header in headers)
                    {
                        try
                        {
                            var Details = StructureJSON[Header][0];

                            var Fields = new string[] { };

                            if (Details["Type"].ToString() == "Table")
                            {
                                Newtonsoft.Json.Linq.JObject FieldsDetails = Newtonsoft.Json.Linq.JObject.Parse(Details["Fields"].ToString());
                                Fields = FieldsDetails.Properties().Select(p => p.Name).ToArray();
                            }
                            else
                            {
                                if (Details["Fields"] != null)
                                    Fields = Details["Fields"]?.Values<string>()?.ToArray() ?? new string[] { };
                                else
                                    Fields = new string[] { };
                            }
                            
                            declarationDictionary.Add(Header, Fields);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"LoadDocumentStructure: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("LoadDocumentsStructure: {0}", ex.Message));
            }
        }

        public string PDFToRAWText(string filePath)
        {
            string documentText = "";
            try
            {
                using (PdfDocumentProcessor documentProcessor = new PdfDocumentProcessor())
                {
                    documentProcessor.LoadDocument(filePath);
                    documentText = documentProcessor.Text;
                }
                return documentText;//.Replace("\r", "_").Replace("\n", "\n");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("PDFToRAWText: {0}", ex.Message));
            }
        }

        private string FormatLines(string Text)
        {
            try
            {
                foreach (var Group in declarationDictionary)
                {
                    Text = FormatGroup(Text, Group, 0);
                }

                //if (Text.Contains("[DETERMINACIÓN DEL IMPUESTO EMPRESARIAL A TASA ÚNICA]"))
                //{
                //    Text = FormatEmpresarialTax(Text);
                //}

                Text = DeleteExtraSpaces(Text);
                return Text.Trim();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FormatLines: {0}", ex.Message));
            }
        }

        private string FormatGroup(string Text, KeyValuePair<string, string[]> Group, int StartIndex)
        {
            try
            {
                //Format the headers
                var PatternRegex = new Regex(FormatTextPattern(Group.Key) + @"[^\]]", RegexOptions.Multiline);

                var Key = (Group.Value.Length > 0) ? $"[{Group.Key}]" : $"\n[{Group.Key.Replace(":","")}]\n{Group.Key.Replace(":", "")}=";

                Text = PatternRegex.Replace(Text, Key, 1, StartIndex);               
                 
                //Get the started index of the group
                int NewIndex = Text.IndexOf(Key, StartIndex);
                StartIndex = NewIndex >= 0 ? (NewIndex + Key.Length) : StartIndex;

                if (NewIndex < 0) { return Text; }

                Newtonsoft.Json.Linq.JToken groupStructure = StructureJSON[Group.Key][0];

                //Si es de tipo tabla identificamos el inicio de las filas para poder guardarlas para procesar después
                switch (groupStructure["Type"].ToString())
                {
                    case "Table":
                        FormatTextTable(ref Text, StartIndex, groupStructure);
                        break;
                    case "Value":
                        foreach (string Valor in Group.Value)
                        {
                            //Se formatea el texto que representa el valor a recuperar
                            PatternRegex = new Regex(FormatTextPattern(Valor), RegexOptions.Multiline);
                            Text = PatternRegex.Replace(Text, $"\n{Valor}=", 1, StartIndex);
                        }
                        break;
                    default:
                        break;
                }
                
                return FormatGroup(Text, Group, StartIndex);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FormatGroup: {0}", ex.Message));
            }
        }

        private string FormatTextPattern(string Valor)
        {
            return Valor
                .Replace(")", @"\)")
                .Replace("(", @"\(")
                .Replace("+", @"\+")
                .Replace("-", @"\-")
                .Replace(" ", @"(\r\n|\s)")
                .Replace("?", @"\?");
        }

        private string DeleteExtraSpaces(string Text)
        {
            //Text = Regex.Replace(Text, "\r\n", " ");
            Text = Regex.Replace(Text, "\n{2,}", "\n");
            Text = Regex.Replace(Text, "={2,}", "=");
            Text = Regex.Replace(Text, @"=\n^(((?!=|\[).)*)$", "=$1", RegexOptions.Multiline);
            return Text;
        }
                
        public string TextToJSON(string Text)
        {
            try
            {
                Dictionary<string, object> Datos = new Dictionary<string, object>();
                Dictionary<string, object> CurrentDictionary = null;
                foreach (string Line in Text.Split('\n'))
                {
                    Console.WriteLine(Line);
                    if (Line.Trim().StartsWith("["))
                    {
                        string Header = Line.Replace("[", "").Replace("]", "").Trim();

                        CurrentDictionary = new Dictionary<string, object>();

                        if (!Datos.ContainsKey(Header))
                            Datos.Add(Header, CurrentDictionary);
                    }
                    else if (Line.Contains("|"))
                    {
                        string[] data = Line.Split('=');
                        string[] values = data[1].Split('|');

                        var TempDictionary = new Dictionary<string, object>();
                        foreach (string value in values)
                        {
                            var subvalues = value.Split('&');
                            TempDictionary.Add(subvalues[0].Trim(), subvalues[1].Trim());
                        }

                        CurrentDictionary.Add(data[0].Trim(), TempDictionary);
                    }
                    else if (Line.Contains("="))
                    {
                        string[] data = Line.Split('=');

                        if (CurrentDictionary.ContainsKey(data[0]))
                        {
                            int count = CurrentDictionary.Keys.Count<string>(x => x.Contains(data[0]));
                            CurrentDictionary.Add(data[0].Trim()+ count, data[1]?.Trim());
                        }
                        else
                        {
                            CurrentDictionary.Add(data[0].Trim(), data[1]?.Trim());
                        }
                    }
                }
                return NASA.PROCNASA.BusinessLayer.CLS_Json.SerializeObject(Datos);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("TextToJSON: {0}", ex.Message));
            }
        }

        public void Coordenadas(string path)
        {
            try
            {
                List<Tuple<string, PdfOrientedRectangle>> WordCoordinates = new List<Tuple<string, PdfOrientedRectangle>>();
                using (PdfDocumentProcessor processor = new PdfDocumentProcessor())
                {
                    processor.LoadDocument(path);
                    
                    PdfWord currentWord = processor.NextWord();
                    while (currentWord != null)
                    {
                        for (int i = 0; i < currentWord.Rectangles.Count; i++)
                        {
                            //Retrieve the rectangle encompassing the word
                            var wordRectangle = currentWord.Rectangles[i];

                            //Add the segment's content and its coordinates to the list
                            WordCoordinates.Add(new Tuple<string, PdfOrientedRectangle>(currentWord.Segments[i].Text, wordRectangle));
                        }
                        //Switch to the next word
                        currentWord = processor.NextWord();
                    }
                }

                Console.WriteLine();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region "TablePDF"
        private void FormatTextTable(ref string Text, int StartIndex, Newtonsoft.Json.Linq.JToken groupStructure)
        {
            try
            {
                Newtonsoft.Json.Linq.JObject StructureJSONTable = Newtonsoft.Json.Linq.JObject.Parse(groupStructure["Fields"].ToString());
                string[] FieldsTable = StructureJSONTable.Properties().Select(p => p.Name).ToArray();

                foreach (string Field in FieldsTable)
                {
                    Newtonsoft.Json.Linq.JObject FieldProperties = Newtonsoft.Json.Linq.JObject.Parse(StructureJSONTable[Field].ToString());

                    //buscamos los registros de las tablas que inicien con la expresión indicada
                    Regex PatternRegex = new Regex(FieldProperties["PatternStart"].ToString(), RegexOptions.Multiline);
                    MatchCollection coincidencias = PatternRegex.Matches(Text, StartIndex);

                    if (coincidencias != null && coincidencias.Count > 0)
                    {
                        //1.- IDENTIFICAR LOS REGISTROS
                        //agregamos en texto REGISTRO para indicar que es una fila de la tabla
                        var startAt = 0;
                        foreach (Match item in coincidencias)
                        {
                            Text = PatternRegex.Replace(Text, $"{Field}={item.Value}", 1, item.Index + startAt);
                            startAt = item.Length;
                        }

                        //2.- BUSCAMOS LAS FILAS MARCADAS Y LAS VOLVEMOS A DIVIDIR
                        Regex PatternRegex_Split = new Regex($"{Field}=", RegexOptions.Multiline | RegexOptions.Compiled);
                        string[] rows_split = PatternRegex_Split.Split(Text);

                        //3.- MARCAMOS NUEVAMENTE LOS REGISTRO Y ELIMINAMOS LOS SALTOS DE LINEA PARA AGRUPAR CADA REGISTRO
                        for (int i = 0; i < rows_split.Length; i++)
                        {
                            //verficicamos si cada fila inicia con la palabra llave indicando que es una fila
                            PatternRegex = new Regex(FieldProperties["PatternStart"].ToString(), RegexOptions.Multiline);

                            if (PatternRegex.IsMatch(rows_split[i]))
                            {
                                //reemplazamos los saltos de linea con un pipe para poder tener una referencia donde termina la fila
                                var line = rows_split[i].ToString().Replace("\r", "|").Replace("\n", "");

                                //buscamos donde termina la fila
                                var pattern2 = new Regex(FieldProperties["PatternEnd"].ToString());
                                Match rtes = pattern2.Match(line);

                                if (rtes.Success && rtes.Index > 0)
                                {
                                    //Reemplazamos el pipe con un espacio en blanco y recortamos la fila hasta donde termina la coincidencia con el regex
                                    rows_split[i] = $"{Field}={line.Substring(0, rtes.Index + rtes.Length).Replace("|", " ")}";

                                    //si es la ultima posición reemplazamos el pipe  y concatenamos el texto que no pertenece a la fila para que no se pierda el texto
                                    if (i + 1 == rows_split.Length)
                                    {
                                        if (line.Length > rtes.Index + rtes.Length)
                                            rows_split[i] += "\n" + line.Substring(rtes.Index + rtes.Length, line.Length - (rtes.Index + rtes.Length)).Replace("|", "");
                                    }
                                }
                            }
                        }

                        //UNIMOS EL CONTENIDO
                        Text = string.Join("\n", rows_split);
                    }
                }
            }
            catch { }
        }

        public void GetStructureTablePDF(string path, string outpath)
        {
            try
            {
                using (System.IO.FileStream SourceStream = System.IO.File.Open(outpath, System.IO.FileMode.Open))
                {
                    iTextSharp.text.pdf.parser.TaggedPdfReaderTool convertor = new iTextSharp.text.pdf.parser.TaggedPdfReaderTool();
                    convertor.ConvertToXml(new iTextSharp.text.pdf.PdfReader(path), SourceStream);
                }


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Lê uma tabela de um pdf
        /// </summary>
        /// <param name="pdf">Caminho do PDF</param>
        /// <param name="origemXPag1">Inicio da leitura no eixo X para a primeira página</param>
        /// <param name="origemYPag1">Inicio da leitura no eixo Y para a primeira página</param>
        /// <param name="linhasPag1">Quantidade de linhas da primeira página</param>
        /// <param name="origemXOutrasPag">Inicio da leitura no eixo X para as demais páginas</param>
        /// <param name="origemYOutrasPag">Inicio da leitura no eixo Y para as demais páginas</param>
        /// <param name="linhasOutrasPag">Quantidade de linhas das demais páginas</param>
        /// <param name="alturaLinha">Altrura da linha</param>
        /// <param name="colunas">Nome e largura das colunas</param>
        /// <returns></returns>
        private List<Dictionary<string, string>> ReadTabelaPDF(string pdf, float origemXPag1, float origemYPag1, int linhasPag1, float origemXOutrasPag, float origemYOutrasPag, int linhasOutrasPag, float alturaLinha, Dictionary<string, float> colunas)
        {
            // Primeira página
            float origemX = origemXPag1;
            float origemY = origemYPag1;
            int quantidadeLinhas = linhasPag1;

            var resultado = new List<Dictionary<string, string>>();
            using (iTextSharp.text.pdf.PdfReader leitor = new iTextSharp.text.pdf.PdfReader(pdf))
            {
                var texto = string.Empty;
                for (int i = 1; i <= leitor.NumberOfPages; i++)
                {
                    if (i > 1)
                    {
                        origemX = origemXOutrasPag;
                        origemY = origemYOutrasPag;
                        quantidadeLinhas = linhasOutrasPag;
                    }
                    for (int l = 0; l < quantidadeLinhas; l++)
                    {
                        var dados = new Dictionary<string, string>();
                        int c = 0;
                        float deslocamentoX = 0;
                        foreach (var coluna in colunas)
                        {
                            RectangleJ rect = new RectangleJ(origemX + deslocamentoX, origemY + (l * alturaLinha), coluna.Value, alturaLinha);
                            iTextSharp.text.pdf.parser.RenderFilter filter = new iTextSharp.text.pdf.parser.RegionTextRenderFilter(rect);
                            iTextSharp.text.pdf.parser.ITextExtractionStrategy strategy = new iTextSharp.text.pdf.parser.FilteredTextRenderListener(new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy(), filter);
                            texto = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(leitor, i, strategy);

                            dados.Add(coluna.Key, texto);
                            c++;
                            deslocamentoX += coluna.Value;
                        }
                        if (dados != null)
                            resultado.Add(dados);
                    }
                }
            }
            return resultado;
        }

        public void GetTextTable(string pdf)
        {
            try
            {
                var columns = new Dictionary<string, float>();
                columns.Add("FECHA", 9);
                columns.Add("DESCRIPCIÓN / ESTABLECIMIENTO", 2000);
                columns.Add("MONTO DEL DEPOSITO", 18);
                columns.Add("MONTO DEL RETIRO", 18);
                columns.Add("SALDO", 18);

                var registros = ReadTabelaPDF(pdf, 19, 75, 9, 19, 40, 13, 40, columns);
                var cod = registros[0]["cod"];
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /*
        public string[] ExtractText(this iText.Kernel.Pdf.PdfPage page, params Rectangle[] rects)
        {
            
            //iTextSharp.text.pdf.parser.LocationTextExtractionStrategy;
            var textEventListener = new iText.Kernel.Pdf.Canvas.Parser.Listener.LocationTextExtractionStrategy();
            PdfTextExtractor.GetTextFromPage(page, textEventListener);
            string[] result = new string[rects.Length];
            for (int i = 0; i < result.Length; i++)
            {
                //result[i] = textEventListener.GetResultantText(rects[i]);
                result[i] = textEventListener.GetResultantText();
            }
            return result;
        }

        public String GetResultantText(this LocationTextExtractionStrategy strategy, Rectangle rect)
        {
            IList<TextChunk> locationalResult = (IList<TextChunk>)locationalResultField.GetValue(strategy);
            List<TextChunk> nonMatching = new List<TextChunk>();
            foreach (TextChunk chunk in locationalResult)
            {
                ITextChunkLocation location = chunk.GetLocation();
                Vector start = location.GetStartLocation();
                Vector end = location.GetEndLocation();
                if (!rect.IntersectsLine(start.Get(Vector.I1), start.Get(Vector.I2), end.Get(Vector.I1), end.Get(Vector.I2)))
                {
                    nonMatching.Add(chunk);
                }
            }
            nonMatching.ForEach(c => locationalResult.Remove(c));
            try
            {
                return strategy.GetResultantText();
            }
            finally
            {
                nonMatching.ForEach(c => locationalResult.Add(c));
            }
        }

        public FieldInfo locationalResultField = typeof(LocationTextExtractionStrategy).GetField("locationalResult", BindingFlags.NonPublic | BindingFlags.Instance);*/
        #endregion

        #region "JSON Viewer..."
        public Dictionary<string, object> Deserialize(string JSONText)
        {
          
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();

            try
            {
                return js.Deserialize<Dictionary<string, object>>(JSONText);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region "Libraries..."
        public void GetStringFormObject()
        {
            try
            {
                
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //public void getString() { 
        //        // Get API
        //        AsposeOcr api = new AsposeOcr();

        //        // Create license
        //        License lic = new License();

        //        // Set license 
        //        lic.SetLicense("Aspose.Total.lic");

        //        // Get image for recognize
        //        string image = "D://img.png";

        //        // Recognize image           
        //        var set = new RecognitionSettings { Language = Language.Swe, DetectAreas = false };
        //        var result = api.RecognizeImage(img, set);

        //        // Get result
        //        Console.WriteLine(result.RecognitionText);

        //        // Save result
        //        result.Save("D:\\test.txt", true, SpellCheckLanguage.Swe);
            
        //}

        #endregion
    }
}
