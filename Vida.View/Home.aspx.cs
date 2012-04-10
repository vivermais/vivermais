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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.DAO;

namespace ViverMais.View
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Remove os itens da Home aos quais o usuário não tem permissão de acesso
            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
            Usuario usuario = (Usuario)Session["Usuario"];
            if (usuario == null)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("Index.aspx");
            }

            lnkAtendimento.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ATENDIMENTO", Modulo.ATENDIMENTO);
            LinkCatalogo.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "GUIAPROCEDIMENTOS", Modulo.GUIAPROCEDIMENTOS);
            lnkSus.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "PACIENTES", Modulo.CARTAO_SUS);
            lnkFarmacia.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "FARMACIA", Modulo.FARMACIA);
            lnkOuvidoria.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "OUVIDORIA", Modulo.OUVIDORIA);
            lnkAgendamento.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "REGULACAO", Modulo.AGENDAMENTO);
            lnkUrgencia.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "URGENCIA", Modulo.URGENCIA);
            lnkSeguranca.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "SEGURANCA", Modulo.SEGURANCA);
            lnkBPA.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ENVIAR_BPA", Modulo.ENVIO_BPA);
            lnkExportCNS.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "EXPORTAR_CNS", Modulo.CARTAO_SUS);
            lnkVacina.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "VACINA", Modulo.VACINA);
            lnkRelatorio.Visible = iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "RELATORIOS", Modulo.CARTAO_SUS);
            
            ((Panel)((MasterMain)Master).FindControl("Panel_NomeModulo")).Visible = false;
            ((Panel)((MasterMain)Master).FindControl("Panel_Modulo")).Visible = false;
        }
    }
}
