using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Data;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.View.Urgencia
{
    public partial class FormImprimirMedicamentosAprazados : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            long co_prescricao;
            if (Request["co_prescricao"] != null && long.TryParse(Request["co_prescricao"].ToString(), out co_prescricao))
            {
                //Hashtable hash = Factory.GetInstance<IProntuario>().RetornaHashTableMedicamentosAprazados(co_prescricao);
                //ReportDocument doc = new ReportDocument();
                //DSCabecalho cabecalho = new DSCabecalho();
                //DSMedicamentosAprazados medicamentos = new DSMedicamentosAprazados();
                //DSProcedimentosAprazados procedimentos = new DSProcedimentosAprazados();
                //DSProcedimentosNaoFaturaveisAprazados procedimentosnaofaturaveis = new DSProcedimentosNaoFaturaveisAprazados();

                //cabecalho.Tables.Add((DataTable)hash["cabecalho"]);
                //medicamentos.Tables.Add((DataTable)hash["medicamento"]);
                //procedimentos.Tables.Add((DataTable)hash["procedimento"]);
                //procedimentosnaofaturaveis.Tables.Add((DataTable)hash["procedimentonaofaturavel"]);

                //doc.Load(Server.MapPath("RelatoriosCrystal/RelItensAprazadosPrescricao.rpt"));
                //doc.Subreports["RelCabecalho.rpt"].SetDataSource(cabecalho.Tables[1]);
                //doc.Subreports["Inc_MedicamentosAprazados.rpt"].SetDataSource(medicamentos.Tables[1]);
                //doc.Subreports["Inc_ProcedimentosAprazados.rpt"].SetDataSource(procedimentos.Tables[1]);
                //doc.Subreports["Inc_ProcedimentosNaoFaturaveisAprazados.rpt"].SetDataSource(procedimentosnaofaturaveis.Tables[1]);

                //Session["documentoImpressaoUrgencia"] = doc;
                //Response.Redirect("FormImprimirCrystalReports.aspx");
            }
        }
    }
}
