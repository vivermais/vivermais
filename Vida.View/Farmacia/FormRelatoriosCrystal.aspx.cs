using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ViverMais.View.Farmacia
{
    public partial class FormRelatoriosCrystal : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["relatoriofarmacia"] != null)
            {
                ReportDocument doc = (ReportDocument)Session["relatoriofarmacia"];
                CrystalReportViewer_Relatorio.ReportSource = doc;
                CrystalReportViewer_Relatorio.DataBind();
            }
        }
    }
}
