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
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.View.Agendamento.RelatoriosCrystal;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.DAO;
using ViverMais.View.Agendamento.Helpers;

namespace ViverMais.View.Agendamento
{
    public partial class RelatorioVagas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Parametros parametro = Factory.GetInstance<IAgendamentoServiceFacade>().ListarTodos<ViverMais.Model.Parametros>().FirstOrDefault();
                DateTime data_inicial = DateTime.Now.AddDays(parametro.Min_Dias);
                DateTime data_final = DateTime.Now.AddDays(parametro.Max_Dias);

                Hashtable hash = Factory.GetInstance<IRelatorioAgendamento>().RelatorioVagas(data_inicial, data_final);
                if (hash.Count != 0)
                {
                    Session["HashVagasDisponivel"] = hash;
                   // Redirector.Redirect("RelatorioVagas.aspx", "_blank", "");
                }
                CriaPDF();
            }
        }

        protected void CriaPDF()
        {
            Hashtable hash = (Hashtable)Session["HashVagasDisponivel"];

            //CrystalReportViewer_AgendaPrestador.DisplayToolbar = true;
            //CrystalReportViewer_AgendaPrestador.EnableDatabaseLogonPrompt = false;


            DSRelatorioVagasDisponivel conteudo = new DSRelatorioVagasDisponivel();
            conteudo.Tables.Add((DataTable)hash["corpo"]);

            ReportDocument repDoc = new ReportDocument();
            repDoc.Load(Server.MapPath("RelatoriosCrystal/CrystalReportViewer_VagasDisponivel.rpt"));
            repDoc.SetDataSource(conteudo.Tables[1]);


            //repDoc.Database.Tables["CabecalhoAgendaPrestador"].SetDataSource((DataTable)hash["cabecalho"]);
            //repDoc.Database.Tables["RelatorioAgendaPrestador"].SetDataSource((DataTable)hash["corpo"]);

            System.IO.Stream s = repDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "applicattion/octect-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=RelatorioVagasDisponivel.pdf");
            Response.AddHeader("Content-Length", s.Length.ToString());
            Response.BinaryWrite(((System.IO.MemoryStream)s).ToArray());
            Response.End();
        }

    }
}
