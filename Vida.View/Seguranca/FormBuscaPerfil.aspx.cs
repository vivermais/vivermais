using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;

namespace ViverMais.View.Seguranca
{
    public partial class FormBuscaPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
                if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "MANTER_PERFIL",Modulo.SEGURANCA))
                //{
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='Default.aspx';</script>");
                //}
                else
                {
                    IList<ViverMais.Model.Modulo> lm = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.Modulo>().OrderBy(m => m.Nome).ToList();
                    foreach (ViverMais.Model.Modulo m in lm)
                        DropDownList_Sistema.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));
                }
            }
        }

        /// <summary>
        /// Pesquisa os perfis cadastrados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_Pesquisar(object sender, EventArgs e) 
        {
            if (DropDownList_Sistema.SelectedValue != "0")
            {
                ViewState["indexOfModulo"] = DropDownList_Sistema.SelectedValue;
                CarregaPesquisa(int.Parse(DropDownList_Sistema.SelectedValue));
                Panel_ResultadoPesquisa.Visible = true;
            }
        }

        /// <summary>
        /// Carrega a pesquisa dos perfis
        /// </summary>
        /// <param name="modulo">código da unidade</param>
        private void CarregaPesquisa(int modulo)
        {
            IList<ViverMais.Model.Perfil> lp = Factory.GetInstance<IPerfil>().BuscarPorModulo<ViverMais.Model.Perfil>(modulo).OrderBy(p => p.Nome).ToList();

            GridView_ResultadoPesquisa.DataSource = lp;
            GridView_ResultadoPesquisa.DataBind();
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e) 
        {
            CarregaPesquisa(int.Parse(ViewState["indexOfModulo"].ToString()));
            GridView_ResultadoPesquisa.PageIndex = e.NewPageIndex;
            GridView_ResultadoPesquisa.DataBind();
        }
    }
}
