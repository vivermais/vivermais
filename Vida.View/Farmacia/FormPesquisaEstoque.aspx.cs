using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormPesquisaEstoque : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CONSULTAR_ESTOQUE", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                //else
                //{
                //    if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CONSULTAR_ESTOQUE_TODAS_FARMACIAS"))
                //    {
                //    }

                //    ViverMais.Model.Farmacia farm = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((Usuario)Session["Usuario"]).Codigo);

                //    if (farm == null)
                //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário você não está associado com farmácia alguma! Por favor, vincule o seu usuário a uma farmácia previamente cadastrada.');location='Default.aspx';", true);
                //}
            }
        }
    }
}
