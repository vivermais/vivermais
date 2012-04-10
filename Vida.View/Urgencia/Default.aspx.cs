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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using System.IO;
using System.Net;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ViverMais.View.Urgencia
{
    public partial class Default : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
    }
}
