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
using CrystalDecisions.CrystalReports.Engine;
using Vida.View.Agendamento.RelatoriosCrystal;
using Vida.DAO;
using Vida.ServiceFacade.ServiceFacades.Seguranca;
using Vida.Model;

namespace Vida.View.Agendamento
{
    public partial class RelatorioListaProcura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_LISTA_PROCURA",Modulo.AGENDAMENTO))
            {
                criaPDF();
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
        }

        protected void criaPDF()
        {
            CrystalReportViewer_ListaProcura.DisplayToolbar = true;
            CrystalReportViewer_ListaProcura.EnableDatabaseLogonPrompt = false;

            ReportDocument repDoc = new ReportDocument();


            try
            {
                DataTable table = (DataTable)Session["ListaProcura"];
                DataTable table2 = table.Copy();
                DSRelatorioListaProcura listaProcura = new DSRelatorioListaProcura();
                listaProcura.Tables.Add(table2);

                //Recupera a localizacao do arquivo rpt responsável pelo relatório
                repDoc.Load(Server.MapPath("RelatoriosCrystal/CrystalReportViewer_ListaProcura.rpt"));
                repDoc.SetDataSource(listaProcura.Tables[1]);
                CrystalReportViewer_ListaProcura.ReportSource = repDoc;

                //Salva na sessão para ser utilizado pelo pdf Export e pelo print   
                Session["report"] = repDoc;
            }
            catch (Exception ex)
            {
                string strMessage = ex.Message;
                throw ex;
            }
        }
    }
}
