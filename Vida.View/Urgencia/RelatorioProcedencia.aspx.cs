using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using System.Data;

namespace ViverMais.View.Urgencia
{
    public partial class RelatorioProcedencia : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            DateTime temp;

            if (Request["co_unidade"] != null && Request["co_cid"] != null
                && Request["datainicio"] != null && DateTime.TryParse(Request["datainicio"].ToString(), out temp)
                && Request["datafim"] != null && DateTime.TryParse(Request["datafim"].ToString(), out temp))
            {
                string co_unidade = Request["co_unidade"].ToString();
                string co_cid = Request["co_cid"].ToString();
                DateTime datainicio = DateTime.Parse(Request["datainicio"].ToString());
                DateTime datafim = DateTime.Parse(Request["datafim"].ToString());

                Hashtable hash = Factory.GetInstance<IRelatorioUrgencia>().RetornaHashTableProcedencia(co_unidade, co_cid, datainicio, datafim);

                ReportDocument doc = new ReportDocument();
                doc.Load(Server.MapPath("RelatoriosCrystal/RelProcedenciaCID.rpt"));

                DSCabecalhoProcedencia dscabecalho = new DSCabecalhoProcedencia();
                dscabecalho.Tables.Add((DataTable)hash["cabecalho"]);

                DSCorpoProcedencia dscorpo = new DSCorpoProcedencia();
                dscorpo.Tables.Add((DataTable)hash["corpo"]);

                doc.SetDataSource(dscabecalho.Tables[1]);
                doc.Subreports[0].SetDataSource(dscorpo.Tables[1]);

                Session["documentoImpressaoUrgencia"] = doc;
                Response.Redirect("FormImprimirCrystalReports.aspx");

                //CrystalReportViewer_Procedencia.ReportSource = doc;
                //CrystalReportViewer_Procedencia.DataBind();
            }
        }
    }
}
