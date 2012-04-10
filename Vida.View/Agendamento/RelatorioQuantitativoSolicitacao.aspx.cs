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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Agendamento
{
    public partial class RelatorioQuantitativoSolicitacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable cabecalho = (DataTable)((Hashtable)Session["HashQuantitativo"])["cabecalho"];
            DataTable solicitacoes = (DataTable)((Hashtable)Session["HashQuantitativo"])["corpo"];

            lblPeriodo1.Text = cabecalho.Rows[0]["Periodo"].ToString();
            lblData.Text = cabecalho.Rows[0]["DataGeracao"].ToString();


            GridViewQuantitativo.DataSource = solicitacoes;
            GridViewQuantitativo.DataBind();
            
        }
    }
}
