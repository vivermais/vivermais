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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.Model;

namespace ViverMais.View.Relatorio
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "RELATORIOS",Modulo.CARTAO_SUS))
                {
                    //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
                }
            }
        }
    }
}
