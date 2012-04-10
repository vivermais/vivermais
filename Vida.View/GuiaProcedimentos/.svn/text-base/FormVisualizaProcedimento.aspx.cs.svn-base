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
using Vida.Model;
using Vida.DAO;
using Vida.ServiceFacade.ServiceFacades.Procedimento;
using Vida.ServiceFacade.ServiceFacades;

namespace Vida.View.GuiaProcedimentos
{
    public partial class FormVisualizaProcedimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string co_infoProcedimento = Request.QueryString["co_infoProcedimento"];
            InfoProcedimento infoProcedimento = Factory.GetInstance<IVidaServiceFacade>().BuscarPorCodigo<InfoProcedimento>(int.Parse(co_infoProcedimento));
            lblAplicacao.Text = infoProcedimento.Aplicacao;
            lblCodigoProcedimento.Text = infoProcedimento.Procedimento.Codigo;
            lblConceito.Text = infoProcedimento.Conceito;
            lblDicas.Text = infoProcedimento.Dicas;
            lblNomeProcedimento.Text = infoProcedimento.Procedimento.Nome;
            lblObservacao.Text = infoProcedimento.Observacao;
            lblPreparo.Text = infoProcedimento.Preparo;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaProcedimentos.aspx");
        }
    }
}
