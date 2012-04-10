using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Data;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Urgencia
{
    public partial class FormExibeIdentificacaoProfissional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "IDENTIFICAR_PROFISSIONAIS", Modulo.URGENCIA))
                {
                    this.Label_Unidade.Text = ((Usuario)Session["Usuario"]).Unidade.NomeFantasia;
                    this.CarregaUsuariosCadastrados();
                }else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Carrega os usuários cadastrados e que possuam um perfil de profissional associado no Urgence
        /// </summary>
        private void CarregaUsuariosCadastrados()
        {
            IList<ViverMais.Model.UsuarioProfissionalUrgence> usuarios = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorVinculoUnidade<ViverMais.Model.UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Unidade.CNES);
            DataTable table = this.getDataTableUsuarios(usuarios);
            GridView_UsuariosProfissionais.DataSource = table;
            GridView_UsuariosProfissionais.DataBind();
            Session["usuarios"] = table;
        }

        /// <summary>
        /// Retorna uma tabela de usuários a partir de uma lista de usuários/profissionais
        /// </summary>
        /// <param name="lup">IList de usuarioprofissionalurgence</param>
        /// <returns>DataTable</returns>
        private DataTable getDataTableUsuarios(IList<ViverMais.Model.UsuarioProfissionalUrgence> usuarios)
        {
            DataTable tabela = new DataTable();
            tabela.Columns.Add(new DataColumn("CodigoUsuario", typeof(string)));
            tabela.Columns.Add(new DataColumn("NomeUsuario", typeof(string)));
            tabela.Columns.Add(new DataColumn("NomeProfissional", typeof(string)));
            tabela.Columns.Add(new DataColumn("UnidadeVinculo", typeof(string)));
            tabela.Columns.Add(new DataColumn("Ocupacao", typeof(string)));
            
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();

            foreach (ViverMais.Model.UsuarioProfissionalUrgence usuario in usuarios)
            {
                DataRow linha = tabela.NewRow();
                ViverMais.Model.Profissional prof = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(usuario.Id_Profissional.Trim());
                ViverMais.Model.Usuario usu = iViverMais.BuscarPorCodigo<ViverMais.Model.Usuario>(usuario.Id_Usuario);
                ViverMais.Model.EstabelecimentoSaude es = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(usuario.UnidadeVinculo);
                ViverMais.Model.CBO c = iViverMais.BuscarPorCodigo<CBO>(usuario.CodigoCBO);

                linha["CodigoUsuario"] = usu != null ? usu.Codigo.ToString() : "";
                linha["NomeUsuario"] = usu != null ? usu.Nome : "";
                linha["NomeProfissional"] = prof != null ? prof.Nome : "";
                linha["Ocupacao"] = c.Nome;

                tabela.Rows.Add(linha);
            }

            if (tabela.Rows.Count == 0)
                return tabela;

            return tabela.Select("", "NomeUsuario ASC").CopyToDataTable();
        }

        /// <summary>
        /// Paginação para o gridview de usuários/profissionais
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            GridView_UsuariosProfissionais.DataSource = (DataTable)Session["usuarios"];
            GridView_UsuariosProfissionais.DataBind();
            GridView_UsuariosProfissionais.PageIndex = e.NewPageIndex;
            GridView_UsuariosProfissionais.DataBind();
        }

        protected void OnClick_ExcluirUsuarioProfissional(object sender, EventArgs e)
        {
            int co_usuario = int.Parse(((LinkButton)sender).CommandArgument.ToString());
            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
            DataTable table = (DataTable)Session["usuarios"];
            DataRow rowdelete = table.Select("CodigoUsuario=" + co_usuario).First();

            ViverMais.Model.UsuarioProfissionalUrgence usuarioprofissional = iUrgencia.BuscarPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>(co_usuario);

            try
            {
                table.Rows.Remove(rowdelete);
                Session["usuarios"] = table;
                this.GridView_UsuariosProfissionais.DataSource = table;
                this.GridView_UsuariosProfissionais.DataBind();

                iUrgencia.Deletar(usuarioprofissional);
                iUrgencia.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 11, "ID: " + usuarioprofissional.Id_Usuario.ToString()));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Associação excluída com sucesso!');", true);
                //OnClick_CancelarAssociacao(new object(), new EventArgs());
                //this.AtualizarUpdatesPanel();
            }
            catch (Exception f)
            {
                throw f;
            }
        }
    }
}
