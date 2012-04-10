using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using System.Data;

namespace ViverMais.View.Urgencia
{
    public partial class RelatorioProcedimentosFPO : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            int temp;
            if (Request["co_unidade"] != null && (Request["competencia"] != null && int.TryParse(Request["competencia"].ToString(), out temp))&&
                Request["co_procedimento"] != null && (Request["co_procedimentonaofaturavel"] != null && int.TryParse(Request["co_procedimentonaofaturavel"].ToString(), out temp)))
            {
                string co_procedimento = Request["co_procedimento"].ToString();
                int competencia = int.Parse(Request["competencia"].ToString());
                string co_unidade = Request["co_unidade"].ToString();
                int co_procedimentonaofaturavel = int.Parse(Request["co_procedimentonaofaturavel"].ToString());

                Hashtable hash = Factory.GetInstance<IProntuario>().RetornaHashtableProcedimentosFPO(co_procedimento, co_procedimentonaofaturavel, competencia, co_unidade);

                DSCabecalhoFPO cab = new DSCabecalhoFPO();
                cab.Tables.Add((DataTable)hash["cabecalho"]);

                DSProcedimentosFPO corpo = new DSProcedimentosFPO();
                corpo.Tables.Add((DataTable)hash["proc"]);

                DSProcedimentosNaoFaturaveisFPO corpo2 = new DSProcedimentosNaoFaturaveisFPO();
                corpo2.Tables.Add((DataTable)hash["procnf"]);

                ReportDocument doc = new ReportDocument();
                doc.Load(Server.MapPath("RelatoriosCrystal/RelProcedimentosFPO.rpt"));
                doc.SetDataSource(cab.Tables[1]);
                doc.Subreports[0].SetDataSource(corpo.Tables[1]);
                doc.Subreports[1].SetDataSource(corpo2.Tables[1]);

                //CrystalReportViewer_Procedimento.ReportSource = doc;
                //CrystalReportViewer_Procedimento.DataBind();
                Session["documentoImpressaoUrgencia"] = doc;
                Response.Redirect("FormImprimirCrystalReports.aspx");
            }
        }
    }
}
