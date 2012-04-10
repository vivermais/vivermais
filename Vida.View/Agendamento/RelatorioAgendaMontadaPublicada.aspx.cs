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
using ViverMais.View.Agendamento.RelatoriosCrystal;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.View.Agendamento
{
    public partial class RelatorioAgendaMontadaPublicada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CriaPDF();
        }

        private void CriaPDF()
        {
            Hashtable hashtable = (Hashtable)Session["HashAgendaMontadaPublicada"];

            DSCabecalhoAgendaMontadaPublicada cabecalho = new DSCabecalhoAgendaMontadaPublicada();
            cabecalho.Tables.Add((DataTable)hashtable["cabecalho"]);

            DSRelatorioAgendaMontadaPublicada dados = new DSRelatorioAgendaMontadaPublicada();
            dados.Tables.Add((DataTable)hashtable["dados"]);

            ReportDocument repDoc = new ReportDocument();

            repDoc.Load(Server.MapPath("RelatoriosCrystal/CrystalReportViewer_AgendaMontadaPublicada.rpt"));
            repDoc.SetDataSource(dados.Tables[1]);
            repDoc.Subreports["CrystalReportViewer_CabecalhoAgendaMontadaPublicada.rpt"].SetDataSource(cabecalho.Tables[1]);

            System.IO.Stream s = repDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "applicattion/octect-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=RelatorioDeAgendasMontadasPublicadas.pdf");
            Response.AddHeader("Content-Length", s.Length.ToString());
            Response.BinaryWrite(((System.IO.MemoryStream)s).ToArray());
            Response.End();

            Session.Remove("HashAgendaMontadaPublicada");
        }
    }
}
