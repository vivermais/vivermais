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
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.BPA;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.EnvioBPA
{
    public partial class RelatorioEnvioBPA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
            if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "VISUALIZAR_RELATORIO_ENVIO_BPA",Modulo.ENVIO_BPA))
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
            }

            if (!IsPostBack) 
            {
                Usuario usuario = (Usuario)Session["Usuario"];
              //  lblLogin.Text = usuario.Login;                lblCNES.Text = usuario.Unidade.NomeFantasia;
            }
        }

        protected void imgBtnEnviar_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            IList<ProtocoloEnvioBPA> protocolos = Factory.GetInstance<IEnviarBPA>().ListarProtocolos<ProtocoloEnvioBPA>(usuario.Unidade, int.Parse(ddlAno.SelectedValue));
            GridView1.DataSource = protocolos;
            GridView1.DataBind();
        }
    }
}
