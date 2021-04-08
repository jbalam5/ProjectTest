using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCore.BussinesLayer
{
    public class CLS_XML
    {
        public DataSet getTable(string fileNamePath)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(fileNamePath);

                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveToTxt(DataSet ds, String fileName)
        {
            try
            {
                //System.Text.StringBuilder sb = new StringBuilder();
                DataTable dt = new DataTable();

                string[] columns = { "VersionDocumentoSAT", "RFCcontribuyente", "MesPeriodo", "AnioPeriodo", "TipoEnvio", "FechaModBal", "NumCuenta", "SaldoInicial", "Cargos", "Creditos", "SaldoFinal" };

                foreach (string col in columns)
                    dt.Columns.Add(col, typeof(string));

                string _Name = "";


                foreach (DataRow rowenc in ds.Tables[0].Rows)
                {
                    foreach (DataRow rowdet in ds.Tables[1].Rows)
                    {

                        DataRow row = dt.NewRow();


                        row["VersionDocumentoSAT"] = rowenc["Version"];
                        row["RFCcontribuyente"] = rowenc["RFC"];
                        row["MesPeriodo"] = Convert.ToInt32(rowenc["Mes"]);
                        row["AnioPeriodo"] = rowenc["Anio"];
                        row["TipoEnvio"] = rowenc["TipoEnvio"];

                        row["NumCuenta"] = rowdet["NumCta"];
                        row["SaldoInicial"] = rowdet["SaldoIni"];
                        row["Cargos"] = rowdet["Debe"];
                        row["Creditos"] = rowdet["Haber"];
                        row["SaldoFinal"] = rowdet["SaldoFin"];

                        dt.Rows.Add(row);
                        //sb.AppendLine(String.Join("|", row.ItemArray));
                    }
                    _Name = $"_{ rowenc["Mes"]}_{rowenc["Anio"]}.xlsx";
                }


                ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();
                wb.Worksheets.Add(dt, "Balanza");

                wb.SaveAs($"{fileName.Replace(".XML", "").Replace(".xml", "")}{_Name}");


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public void SaveToTxtCatalogos(DataSet ds, String fileName)
        {
            try
            {
                //Version	RFC	Mes	Anio	CodAgrup	NumCta	Desc	Nivel	Natur
                DataTable dt = new DataTable();

                string[] columns = { "VersionDocumentoSAT", "RFCcontribuyente", "MesPeriodo", "AnioPeriodo", "CodigoAgrupSAT", "NumCuenta", "DescripciónCuenta", "NivelCuenta", "NaturalezaCuenta" };

                foreach (string col in columns)
                    dt.Columns.Add(col, typeof(string));

                string _Name = "";


                foreach (DataRow rowenc in ds.Tables[0].Rows)
                {
                    foreach (DataRow rowdet in ds.Tables[1].Rows)
                    {

                        DataRow row = dt.NewRow();


                        row["VersionDocumentoSAT"] = rowenc["Version"];
                        row["RFCcontribuyente"] = rowenc["RFC"];
                        row["MesPeriodo"] = Convert.ToInt32(rowenc["Mes"]);
                        row["AnioPeriodo"] = rowenc["Anio"];
                        
                        row["CodigoAgrupSAT"] = rowdet["CodAgrup"];
                        row["NumCuenta"] = rowdet["NumCta"];
                        row["DescripciónCuenta"] = rowdet["Desc"];
                        row["NivelCuenta"] = rowdet["Nivel"];
                        row["NaturalezaCuenta"] = rowdet["Natur"];

                        dt.Rows.Add(row);
                        //sb.AppendLine(String.Join("|", row.ItemArray));
                    }
                    _Name = $"_{ rowenc["Mes"]}_{rowenc["Anio"]}.xlsx";
                }


                ClosedXML.Excel.XLWorkbook wb = new ClosedXML.Excel.XLWorkbook();
                wb.Worksheets.Add(dt, "Balanza");

                wb.SaveAs($"{fileName.Replace(".XML", "").Replace(".xml", "")}{_Name}");


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
