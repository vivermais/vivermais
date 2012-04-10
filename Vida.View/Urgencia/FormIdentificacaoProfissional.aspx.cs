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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;

namespace ViverMais.View.Urgencia
{
    public partial class FormIdentificacaoProfissional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CarregaProfissional();
            }
        }

        /// <summary>
        /// Carrega o profissional 
        /// </summary>
        private void CarregaProfissional()
        {
            //ViverMais.Model.UsuarioProfissionalUrgence pu = Factory.GetInstance<IUrgenciaServiceFacade>().BuscaPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>();
            //GridView_TipoConsultorio.DataSource = ltc;
            //GridView_TipoConsultorio.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ViverMais.Model.UsuarioProfissionalUrgence up = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>(int.Parse(ViewState["Id_UsuarioProfissionalUrgence"].ToString()));
                up.Identificacao = TextBox_Identificacao.Text;
                Factory.GetInstance<IUrgenciaServiceFacade>().Salvar(up);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Código de identificação salvo com sucesso!');", true);
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        protected void Button_ExibirCodigo_Click(object sender, EventArgs e)
        {
            if (Label_ExibeIdentificacao.Visible == false)
                Label_ExibeIdentificacao.Visible = true;
        }

        protected void Button_Identificar_Click(object sender, EventArgs e)
        {
            //é necessária esta confirmação de login e senha porque em alguns casos
            //usuários deixarão as maquinas logadas; isso impede que um usuário altere ou veja
            //o código de identificação de outro
            ViverMais.Model.Usuario u = Factory.GetInstance<ISeguranca>().Login<ViverMais.Model.Usuario>(TextBox_Usuario.Text, TextBox_Senha.Text);
            if (u != null)
            {
                ViverMais.Model.UsuarioProfissionalUrgence upu = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>(u.Codigo);
                //caso não exista código de identificação cadastrado
                if (upu.Identificacao != null)
                    Label_ExibeIdentificacao.Text = upu.Identificacao.ToString();
                ViewState["Id_UsuarioProfissionalUrgence"] = upu.Id_Usuario;
                ViverMais.Model.Profissional profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(upu.Id_Profissional);
                Label_Profissional.Text = profissional.Nome;
                Label_Resultado.Text = "";
            }
            else 
            {
                Label_Resultado.Text = "Profissional não encontrado";
            }
        }
    }
}
