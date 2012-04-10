using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.View.Urgencia
{
    public partial class FormRelatoriosCrystal : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["relatorio"] != null)
            {
                ReportDocument doc = (ReportDocument)Session["relatorio"];
                CrystalReportViewer_Relatorio.ReportSource= doc;
                CrystalReportViewer_Relatorio.DataBind();
            }
        }
    }
}
