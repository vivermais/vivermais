using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.View.Urgencia
{
    public partial class RelatorioAbsenteismo : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            Usuario usuario = ((Usuario)Session["Usuario"]);

            DateTime data_Inicio = DateTime.Parse(Request.QueryString["data_inicio"]);
            DateTime data_Fim = DateTime.Parse(Request.QueryString["data_fim"]);
            //lblUnidade.Text = usuario.Unidade.NomeFantasia;
            //lblPeriodo.Text = data_Inicio.ToString("dd/MM/yyyy") + " a " + data_Fim.ToString("dd/MM/yyyy");
            //lblDatahora.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            DataTable cab = new DataTable();
            cab.Columns.Add("Unidade", typeof(string));
            cab.Columns.Add("Periodo", typeof(string));
            DataRow l = cab.NewRow();
            l["Unidade"] = usuario.Unidade.NomeFantasia;
            l["Periodo"] = data_Inicio.ToString("dd/MM/yyyy") + " a " + data_Fim.ToString("dd/MM/yyyy");
            cab.Rows.Add(l);
            DSCabecalhoEvasao dscab = new DSCabecalhoEvasao();
            dscab.Tables.Add(cab);


            //long id_unidade = long.Parse(Request["co_unidade"].ToString());

            IRelatorioUrgencia iRelatorio = Factory.GetInstance<IRelatorioUrgencia>();
            IList resultados = iRelatorio.ObterRelatorioAbsenteismo(Request["co_unidade"].ToString(), data_Inicio, data_Fim);

            DataTable table = new DataTable();
            DataColumn col1 = new DataColumn("NumeroAtendimento");
            DataColumn col2 = new DataColumn("Data");
           // DataColumn col3 = new DataColumn();
                        
            table.Columns.Add(col1);
            table.Columns.Add(col2);
            //table.Columns.Add(col3);

            //DataRow hrow = table.NewRow();
            //hrow[0] = "Número de Atendimento";
            //hrow[1] = "Data";
            ////hrow[2] = "Paciente";
            //table.Rows.Add(hrow);

            //variavel para contar o total de registros
            int total = 0;
            foreach (object o in resultados)
            {
                DataRow row = table.NewRow();
                //FaixaEtaria fE = new FaixaEtaria();

                //fE = Factory.GetInstance<IFaixaEtaria>().BuscarPorCodigo<FaixaEtaria>(int.Parse(((object[])o)[1].ToString()));
                row[0] = ((object[])o)[0].ToString();
                row[1] = ((object[])o)[1].ToString();
               // row[21] = ((object[])o)[2].ToString();
                table.Rows.Add(row);
                total++;
            }

            DSRelEvasa conteudo = new DSRelEvasa();
            conteudo.Tables.Add(table);

            ReportDocument doc = new ReportDocument();
            doc.Load(Server.MapPath("RelatoriosCrystal/RelEvasao.rpt"));
            doc.SetDataSource(conteudo.Tables[1]);
            doc.Subreports[0].SetDataSource(dscab.Tables[1]);

            Session["documentoImpressaoUrgencia"] = doc;
            Response.Redirect("FormImprimirCrystalReports.aspx");

            //CrystalReportViewer_Evasao.ReportSource = doc;
            //CrystalReportViewer_Evasao.DataBind();

            //imprime o total
            //DataRow trow = table.NewRow();
            //trow[0] = "Total";
            //trow[1] = total.ToString();
            //table.Rows.Add(trow);
            //GridViewAbsenteismo.DataSource = table;
            //GridViewAbsenteismo.DataBind();
        }
    }
}
