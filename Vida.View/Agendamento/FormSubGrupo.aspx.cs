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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class FormSubGrupo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER SUB-GRUPO", Modulo.AGENDAMENTO))
                {
                    CarregaGridSubGrupo();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        void CarregaGridSubGrupo()
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IList<SubGrupo> subGrupos = iViverMais.ListarTodos<SubGrupo>("NomeSubGrupo", true);
            GridViewLista.DataSource = subGrupos;
            GridViewLista.DataBind();
            Session["ListaSubGrupo"] = subGrupos;
        }

        protected void GridViewLista_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Selecionar")
            {
                int co_subGrupo = Convert.ToInt32(e.CommandArgument.ToString());
                SubGrupo subGrupo = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<SubGrupo>(co_subGrupo);
                if (subGrupo != null)
                {
                    tbxNomeSubGrupo.Text = subGrupo.NomeSubGrupo;
                    ViewState["SubGrupo"] = subGrupo.Codigo;
                }
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

            SubGrupo subGrupo = new SubGrupo();
            //Caso seja uma atualização
            if (ViewState["SubGrupo"] != null)
            {
                int co_subgrupo = int.Parse(ViewState["SubGrupo"].ToString());
                subGrupo = iViverMais.BuscarPorCodigo<SubGrupo>(co_subgrupo);
            }
            subGrupo.NomeSubGrupo = tbxNomeSubGrupo.Text.ToUpper();
            iViverMais.Salvar(subGrupo);
            CarregaGridSubGrupo();
            tbxNomeSubGrupo.Text = String.Empty;
            ViewState.Remove("SubGrupo");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Dados Salvos com Sucesso!');", true);
        }

        protected void GridViewLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewLista.DataSource = Session["ListaSubGrupo"];
            GridViewLista.PageIndex = e.NewPageIndex;
            GridViewLista.DataBind();
        }
    }
}
