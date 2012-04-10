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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.View.Urgencia
{
    public partial class RelatorioAtendimentoCID : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IUrgenciaServiceFacade iurgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
            IRelatorioUrgencia irelatorio = Factory.GetInstance<IRelatorioUrgencia>();
            Usuario usuario = (Usuario)Session["Usuario"];
            //IList resultados = irelatorio.ObterQuantitativoAtendimentoCID(usuario.Unidade.Codigo, Request["cid"].ToString(), DateTime.Parse(Request["data_inicio"].ToString()), DateTime.Parse(Request["data_fim"].ToString()));
            DataTable tab = irelatorio.ObterQuantitativoAtendimentoCID(Request["co_unidade"].ToString(), Request["cid"].ToString(), DateTime.Parse(Request["data_inicio"].ToString()), DateTime.Parse(Request["data_fim"].ToString()));
            DSAtendimentoCID dsatendimento = new DSAtendimentoCID();
            dsatendimento.Tables.Add(tab);

            DataTable cab = new DataTable();
            cab.Columns.Add("Unidade", typeof(string));
            cab.Columns.Add("CID", typeof(string));

            DataRow linha = cab.NewRow();
            linha["Unidade"] = usuario.Unidade.NomeFantasia;
            linha["CID"] = string.IsNullOrEmpty(Request["cid"].ToString()) ? "TODOS" : Factory.GetInstance<ICid>().BuscarPorCodigo<Cid>(Request["cid"].ToString()).Nome;
            cab.Rows.Add(linha);
            DSCabecalhoAtendimentoCID dscabecalho = new DSCabecalhoAtendimentoCID();
            dscabecalho.Tables.Add(cab);

            ReportDocument doc = new ReportDocument();
            doc.Load(Server.MapPath("RelatoriosCrystal/RelAtendimentoCID.rpt"));
            doc.SetDataSource(dsatendimento.Tables[1]);
            doc.Subreports[0].SetDataSource(dscabecalho.Tables[1]);

            Session["documentoImpressaoUrgencia"] = doc;
            Response.Redirect("FormImprimirCrystalReports.aspx");

            //CrystalReportViewer_AtendimentoCID.ReportSource = doc;
            //CrystalReportViewer_AtendimentoCID.DataBind();

            //Cid cid = iurgencia.BuscarPorCodigo<Cid>(Request.QueryString["cid"].ToString());
            //lblCID.Text = cid.Nome;

            //DataTable table = new DataTable();
            //DataColumn col1 = new DataColumn("Descricao");
            //DataColumn col2 = new DataColumn("Quantidade");
            //table.Columns.Add(col1);
            //table.Columns.Add(col2);

            //foreach (object o in resultados)
            //{
            //    DataRow row = table.NewRow();
            //    object[] valor = (object[])o;
            //    row[0] = valor[0].ToString();
            //    row[1] = valor[1].ToString();
            //    table.Rows.Add(row);
            //}

            //GridView1.DataSource = tab;
            //GridView1.DataBind();
        }
    }
}
