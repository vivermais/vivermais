using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.View.Agendamento.RelatoriosCrystal;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;

namespace ViverMais.View.Agendamento
{
    public partial class RelatorioExportacaoBPA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_GERAR_BPA_APAC",Modulo.AGENDAMENTO))
            {
                criarPDF();
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
        }

        protected void criarPDF()
        {
            CrystalReportViewer_ExportaBPA.DisplayToolbar = true;
            CrystalReportViewer_ExportaBPA.EnableDatabaseLogonPrompt = false;

            ReportDocument repDoc = new ReportDocument();

            try
            {
                DataTable table = new DataTable();
                DataColumn col1 = new DataColumn("Unidade");
                DataColumn col2 = new DataColumn("Sigla");
                DataColumn col3 = new DataColumn("CNPJ");
                DataColumn col4 = new DataColumn("Competencia");
                DataColumn col5 = new DataColumn("NomeArquivo");
                DataColumn col6 = new DataColumn("QtdRegistros");
                DataColumn col7 = new DataColumn("BPA(s)");
                DataColumn col8 = new DataColumn("CampoControle");
                table.Columns.Add(col1);                
                table.Columns.Add(col2);
                table.Columns.Add(col3);
                table.Columns.Add(col4);
                table.Columns.Add(col5);
                table.Columns.Add(col6);
                table.Columns.Add(col7);
                table.Columns.Add(col8);
                DataRow row = table.NewRow();
                row[0] = Session["Unidade"].ToString();
                row[1] = Session["Sigla"].ToString();
                row[2] = Session["CNPJ"].ToString();
                row[3] = Session["Competencia"].ToString();
                row[4] = Session["NomeArquivo"].ToString();
                row[5] = Session["QtdRegistros"].ToString();
                row[6] = Session["BPA(s)"].ToString();
                row[7] = Session["CampoControle"].ToString();
                table.Rows.Add(row);
                
                DSRelatorioExportaBPA relatorioBPA = new DSRelatorioExportaBPA();
                relatorioBPA.Tables.Add(table);

                //Recupera a localizacao do arquivo rpt responsável pelo relatório
                repDoc.Load(Server.MapPath("RelatoriosCrystal/CrystalReportViewer_ExportaBPA.rpt"));
                repDoc.SetDataSource(relatorioBPA.Tables[1]);
                CrystalReportViewer_ExportaBPA.ReportSource = repDoc;

                //Salva na sessão para ser utilizado pelo pdf Export e pelo print   
                Session["report"] = repDoc;

            }
            catch (Exception ex)
            {
                string strMessage = ex.Message;
                throw ex;
            }
        }

        //protected void btnImprimir_Click(object sender, EventArgs e)
        //{
        //    ReportDocument repDoc = (ReportDocument)Session["report"];

        //    //Adiciona os itens no ReportDocument para que se possa imprimi-los
        //    foreach (ParameterField pfield in repDoc.ParameterFields)
        //    {
        //        repDoc.SetParameterValue(pfield.ParameterFieldName, pfield.CurrentValues[0]);
        //    }
        //    repDoc.PrintToPrinter(1, false, 1, 1);
        //}
    }
}
