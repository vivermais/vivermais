using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;

namespace ViverMais.View
{
    public partial class FormLembrarCartaoSUS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        /// <summary>
        /// Pesquisa o usuário de acordo com os dados informados: Nome e Sobrenome e Data de Nascimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_PesquisarUsuario(object sender, EventArgs e)
        {
            if (TextBox_Nome.Text.Trim().Split(' ').Length >= 2)
            {
                ViewState["nomeusuario"] = TextBox_Nome.Text;
                ViewState["nascimento"] = TextBox_DataNascimento.Text;
                this.CarregaUsuarios(TextBox_Nome.Text, DateTime.Parse(TextBox_DataNascimento.Text));
                Panel_ResultadoBusca.Visible = true;
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Por favor, informe o nome e sobrenome do usuário.');", true);
        }

        private void CarregaUsuarios(string nomeusuario, DateTime nascimento)
        {
            GridView_Usuarios.DataSource = Factory.GetInstance<IUsuario>().BuscarUsuarioPorNomeDataNascimento<Usuario>(nomeusuario, nascimento).Where(p=>p.Ativo == 1).ToList();
            GridView_Usuarios.DataBind();
        }

        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            this.CarregaUsuarios(ViewState["nomeusuario"].ToString(), DateTime.Parse(ViewState["nascimento"].ToString()));
            GridView_Usuarios.PageIndex = e.NewPageIndex;
            GridView_Usuarios.DataBind();
        }

        protected void OnClick_RetornaPesquisa(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "parent.parent.document.location.href='Index.aspx?co_usuario=" + ((LinkButton)sender).CommandArgument.ToString() + "';parent.parent.GB_hide();", true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "window.opener.location.href='Index.aspx?co_usuario=" + ((LinkButton)sender).CommandArgument.ToString() + "';self.close();", true);
        }
    }
}
