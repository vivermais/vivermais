using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Vacina;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using iTextSharp.text;

namespace ViverMais.View.Vacina
{
    public partial class FormRelatorioDiarioVacinacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string[] cnes = {"0004383",
                    "0003883",
                    "0004545",
                    "2470853",
                    "0003956",
                    "5633508",
                    "5242657",
                    "2653354"};

                //System.Data.DataTable tab = new System.Data.DataTable();
                //tab.Columns.Add(new DataColumn("unidade", typeof(string)));
                //tab.Columns.Add(new DataColumn("quantidade", typeof(int)));

                IList<ViverMais.Model.EstabelecimentoSaude> unidades = Factory.GetInstance<IEstabelecimentoSaude>().BuscarUnidadeDistrito<ViverMais.Model.EstabelecimentoSaude>(1).Where(p => cnes.Contains(p.CNES)).OrderBy(p => p.NomeFantasia).ToList();
                Factory.GetInstance<IRelatorioVacina>().RelatorioProducaoDiaria<ViverMais.Model.EstabelecimentoSaude>(unidades).UserControl = true;

                //foreach (ViverMais.Model.EstabelecimentoSaude unidade in unidades)
                //{
                //    DataRow row = tab.NewRow();
                //    row["unidade"] = unidade.NomeFantasia;
                //    row["quantidade"] = Factory.GetInstance<IRelatorioVacina>().RelatorioDispensacaoUnidadePorData(unidade.CNES, DateTime.Today);
                //    tab.Rows.Add(row);
                //}

                //GridView_Relatorio.DataSource = tab;
                //GridView_Relatorio.DataBind();

                //GerarExcel(unidades);
            }
        }


        protected void OnClickExportarExcel(object sender, EventArgs e)
        {
            Response.Clear();

            Response.AddHeader("content-disposition", "attachment;filename=Quantitativo.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlWrite =
            new HtmlTextWriter(stringWrite);

            //GridView_Relatorio.RenderControl(htmlWrite);

            Response.Write(stringWrite.ToString());

            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }


        private void GerarExcel(IList<ViverMais.Model.EstabelecimentoSaude> unidades)
        {
            try
            {
                Excel.Application _app = new Excel.Application();
                _app.Visible = true;

                Excel.Workbook _base = _app.Workbooks.Add(Excel.XlSheetType.xlWorksheet);
                Excel.Worksheet _planilha = _planilha = (Excel.Worksheet)_base.ActiveSheet;
                Excel.Range _range;
                //Excel.Range _range = _planilha.get_Range("A1", "G1");
                //FormataCabecalho(_planilha.get_Range("A1", "G1"));

                _range = _planilha.get_Range("H1", "J1");
                _range.Font.Bold = true;
                _range.MergeCells = true;
                _range.Interior.ColorIndex = 15;
                _range.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, 0);

                _range = ((Excel.Range)_planilha.Cells[1, "A"]);
                _range.Value2 = "UNIDADE";
                _range.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;

                _range = ((Excel.Range)_planilha.Cells[1, "H"]);
                _range.Value2 = "QUANTIDADE APLICADA";
                _range.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;

                int pos = 2;

                foreach (ViverMais.Model.EstabelecimentoSaude unidade in unidades)
                {
                    _range = _planilha.get_Range("A" + pos, "G" + pos);
                    _range.MergeCells = true;
                    _range.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    _range.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, 0);
                    ((Excel.Range)_planilha.Cells[pos, "A"]).Value2 = unidade.NomeFantasia;

                    _range = _planilha.get_Range("H" + pos, "J" + pos);
                    _range.MergeCells = true;
                    _range.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    _range.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium, XlColorIndex.xlColorIndexAutomatic, 0);
                    ((Excel.Range)_planilha.Cells[pos, "H"]).Value2 = Factory.GetInstance<IRelatorioVacina>().RelatorioDispensacaoUnidadePorData(unidade.CNES, DateTime.Today).ToString();

                    pos++;
                }

                _range = _planilha.get_Range("A" + pos, "J" + pos);
                _range.Font.Bold = true;
                _range.MergeCells = true;
                _range.HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                _range.Value2 = "Relatório referente ao dia " + DateTime.Today.ToString("dd/MM/yyyy");
                
                _app.UserControl = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
