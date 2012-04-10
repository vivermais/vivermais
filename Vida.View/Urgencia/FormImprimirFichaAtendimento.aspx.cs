using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class FormImprimirFichaAtendimento : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            long numeroatendimento;

            if (Request["numeroatendimento"] != null && long.TryParse(Request["numeroatendimento"].ToString(), out numeroatendimento)) 
            {
                //ReportDocument doc = new ReportDocument();
                //DSFichaAtendimentoProntuario ds = new DSFichaAtendimentoProntuario();
                //ds.Tables.Add(Factory.GetInstance<IProntuario>().RetornaDataTableFichaATendimento(numeroatendimento));
                //doc.Load(Server.MapPath("RelatoriosCrystal/RelFichaAtendimentoProntuario.rpt"));
                //doc.SetDataSource(ds.Tables[1]);

                //Session["documentoImpressaoUrgencia"] = doc;
                Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioFichaAtendimento(numeroatendimento);
                Response.Redirect("FormImprimirCrystalReports.aspx?nomearquivo=fichaatendimento.pdf");
                //CrystalReportViewer_FichaAtendimento.ReportSource = doc;
                //CrystalReportViewer_FichaAtendimento.DataBind();
            }
        }
    }
}
