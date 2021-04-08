using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevExpress.Pdf;

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
                string StructureString = System.IO.File.ReadAllText(@"E:\NASA\NASANET2017\ContaNET_Git\structure\BanorteStructure.json");

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
                                Fields = Details["Fields"]?.Values<string>()?.ToArray() ?? new string[] { };
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

                if (Text.Contains("[DETERMINACIÓN DEL IMPUESTO EMPRESARIAL A TASA ÚNICA]"))
                {
                    Text = FormatEmpresarialTax(Text);
                }

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

                var PatternRegex = new Regex(FormatTextPattern(Group.Key) + @"[^\]]", RegexOptions.Multiline);
                var structureGroup = StructureJSON[Group.Key][0];

                //Format the header
                if (structureGroup["Type"].ToString() == "Table")
                    Text = PatternRegex.Replace(Text, $"\n[{Group.Key}]", 1, StartIndex);
                else
                    Text = PatternRegex.Replace(Text, $"\n[{Group.Key}]", 1, StartIndex);                

                //Get the started index of the group
                int NewIndex = Text.IndexOf(Group.Key, StartIndex);
                StartIndex = NewIndex >= 0 ? (NewIndex + Group.Key.Length) : StartIndex;

                if (NewIndex < 0) { return Text; }

                //Si es de tipo tabla identificamos el inicio de las filas para poder guardarlas para procesar después
                //If it is of type table we identify the beginning of the rows to be able to save them to process later
                if (Group.Key == "DETALLE DE MOVIMIENTOS (PESOS)")
                {
                    PatternRegex = new Regex(@"^[0-9]{2}\b-\b[a-zA-Z]{3}\b-\b[0-9]{2}", RegexOptions.Multiline);
                    MatchCollection coincidencias = PatternRegex.Matches(Text, StartIndex);

                    var startAt = 0;
                    foreach (Match item in coincidencias)
                    {
                        Text = PatternRegex.Replace(Text, $"\nREGISTRO={item.Value}", 1, item.Index + startAt);
                        startAt = item.Length;
                    }

                    if (coincidencias != null && coincidencias.Count > 0)
                    {
                        //([0-9]{2}\b-\b[a-zA-Z]{3}\b-\b[0-9]{2})((.+)(\s+))(([0-9]+?)(,[0-9])*[0-9]+\.[0-9]{2})[^\r\n]
                        ///([0-9]{2}\b-\b[a-zA-Z]{3}\b-\b[0-9]{2})((.+)(\s+))(([0-9]+?)(,[0-9])*[0-9]+\.[0-9]{2}[\\])/gU
                        ///REGISTRO=(.+)(\n+)*(\r+)*(([0-9]+?)(,[0-9])*[0-9]+\.[0-9]{2})+[\\]
                        // @"REGISTRO=(.+)(([0-9]+?)(,[0-9])*[0-9]+\.[0-9]{2})[\\r\\n?](.+)"
                        //PatternRegex = new Regex(@"REGISTRO=(.+)([^$]\s\d+?,\d*\d+\.{1}\d{2})", RegexOptions.Multiline | RegexOptions.Compiled);
                        //MatchCollection rows = PatternRegex.Matches(Text);

                        Regex  PatternRegex_Split = new Regex(@"REGISTRO=", RegexOptions.Multiline| RegexOptions.Compiled);
                        string[] rows_split = PatternRegex_Split.Split(Text);
                        //var rows_split = Regex.Split(Text, @"REGISTRO=");

                        for (int i = 0; i < rows_split.Length - 1; i++)
                        {
                            PatternRegex = new Regex(@"^[0-9]{2}\b-\b[a-zA-Z]{3}\b-\b[0-9]{2}", RegexOptions.Multiline);

                            if (PatternRegex.IsMatch(rows_split[i]))
                                rows_split[i] = $"REGISTRO={rows_split[i].Replace("\r\n", " ").Replace("\n", "")}";

                        }

                        Text = string.Join("\n", rows_split);
                        Console.WriteLine();
                        /*
                        startAt = 0;
                        foreach (Match row in rows)
                        {
                            var text_ = row.Value;
                            Console.WriteLine(row.Value);
                            //Text = PatternRegex.Replace(Text, row.Value.Replace("\r\n", "_"), 1, row.Index + startAt);
                            startAt = row.Length;
                        }

                         Console.WriteLine();*/
                    }

                }
                else
                {
                    foreach (string Valor in Group.Value)
                    {
                        try
                        {
                            //Format the values
                            PatternRegex = new Regex(FormatTextPattern(Valor), RegexOptions.Multiline);
                            Text = PatternRegex.Replace(Text, $"\n{Valor}=", 1, StartIndex);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

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

        private string FormatEmpresarialTax(string Text)
        {
            try
            {
                string HeaderTax = "DETERMINACIÓN DEL IMPUESTO EMPRESARIAL A TASA ÚNICA";
                int StartIndex = Text.IndexOf($"[{HeaderTax}]");
                int EndIndex = Text.IndexOf("[", (StartIndex + $"[{HeaderTax}]".Length + 10));
                int SectionLength = EndIndex - StartIndex;
                string SectionText = Text.Substring(StartIndex, SectionLength);

                var Fields = declarationDictionary[HeaderTax];

                foreach (string Field in Fields)
                {
                    //Format the values
                    var PatternRegex = new Regex(FormatTextPattern(Field), RegexOptions.Multiline);
                    SectionText = PatternRegex.Replace(SectionText, $"\n{Field}=", 1, 0);
                }

                SectionText = DeleteExtraSpaces(SectionText);

                SectionText = FormatTableTax(SectionText, HeaderTax, Fields);

                Text = Text.Remove(StartIndex, SectionLength);
                Text = Text.Insert(StartIndex, $"{SectionText}\n");

                return Text;

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FormatEmpresarialTax: {0}", ex.Message));
            }
        }

        private string FormatTableTax(string RawSection, string HeaderTax, string[] Fields)
        {
            try
            {
                List<string> Lines = RawSection.Split('\n')?.ToList();
                string NewSection = $"\n[{HeaderTax}]";

                for (int i = 0; i < Fields.Length; i++)
                {
                    string Field = Fields[i];
                    string Line = Lines.Find(x => x.Contains(Field));

                    if (i == 0) { NewSection += $"\n{Line}"; continue; }

                    string[] values = Line?.Split('=')?[1]?.Trim()?.Split(' ');

                    if (values?.Length == 3)
                    {
                        NewSection += $"\n{Field}=ACUMULADOS(AS) DE PERIODOS ANTERIORES&{values[0]}|" +
                            $"DEL PERIODO&{values[1]}|TOTAL ACUMULADO&{values[2]}";
                    }
                    else if (values?.Length == 2)
                    {
                        NewSection += $"\n{Field}=ACUMULADOS(AS) DE PERIODOS ANTERIORES&{values[0]}|" +
                            $"DEL PERIODO&|TOTAL ACUMULADO&{values[1]}";
                    }
                }

                return NewSection;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("FormatTableTax: {0}", ex.Message));
            }
        }

        public string TextToJSON(string Text)
        {
            try
            {
                Dictionary<string, object> Datos = new Dictionary<string, object>();
                Dictionary<string, object> CurrentDictionary = null;
                foreach (string Line in Text.Split('\n'))
                {
                    if (Line.Trim().StartsWith("["))
                    {
                        string Header = Line.Replace("[", "").Replace("]", "").Trim();

                        CurrentDictionary = new Dictionary<string, object>();

                        if (Datos.ContainsKey(Header))
                        {
                            //int count = Datos.Keys.Count<string>(x => x.Contains(Header));
                            //Datos.Add(Header + count, CurrentDictionary);
                        }
                        else
                        {
                            Datos.Add(Header, CurrentDictionary);
                        }
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
    }
}
