using System;
using ViverMais.DAO;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using System.Threading;

namespace ViverMais.View.Urgencia
{
    public partial class FormAssociarUsuarioProfissional : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "IDENTIFICAR_PROFISSIONAIS", Modulo.URGENCIA))
                {
                    Label_UnidadeSaude.Text = ((Usuario)Session["Usuario"]).Unidade.NomeFantasia;
                    //CarregaUsuariosCadastrados();
                    CarregaUsuarios();
                    CarregaCBOs();

                    if (Request["co_usuario"] != null)
                    {
                        int co_usuario = int.Parse(Request["co_usuario"].ToString());
                        IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();

                        ViverMais.Model.UsuarioProfissionalUrgence usuarioprofissional = iUrgencia.BuscarPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>(co_usuario);

                        if (ddlFiltroUsuarios.Items.FindByValue(usuarioprofissional.Id_Usuario.ToString()) != null)
                        {
                            ddlFiltroUsuarios.SelectedValue = usuarioprofissional.Id_Usuario.ToString();
                            OnSelectedIndexChanged_MostrarDadosUsuario(sender, new EventArgs());
                        }

                        ddlFiltroCBO.SelectedValue = usuarioprofissional.CodigoCBO;
                        ddlFiltroUsuarios.Enabled = false;

                        OnSelectedIndexChanged_CarregaProfissionais(sender, new EventArgs());
                        if (ddlProfissionais.Items.FindByValue(usuarioprofissional.Id_Profissional.Trim()) != null)
                        {
                            ddlProfissionais.SelectedValue = usuarioprofissional.Id_Profissional.Trim();
                            OnSelectedIndexChanged_MostrarDadosProfissional(sender, new EventArgs());
                        }
                    }
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        private void CarregaUsuarios()
        {
            ddlFiltroUsuarios.Items.Clear();

            IList<Usuario> usuarios = Factory.GetInstance<IUsuario>().BuscarPorModulo<Usuario>(Modulo.URGENCIA, ((Usuario)Session["Usuario"]).Unidade.CNES).Where(p => p.Ativo == 1).ToList();

            ddlFiltroUsuarios.DataValueField = "Codigo";
            ddlFiltroUsuarios.DataTextField = "Nome";

            ddlFiltroUsuarios.DataSource = usuarios;
            ddlFiltroUsuarios.DataBind();

            ddlFiltroUsuarios.Items.Insert(0, new ListItem("Selecione...", "0"));
        }

        /// <summary>
        /// Carrega as categorias de profissionais existentes
        /// </summary>
        private void CarregaCBOs()
        {
            ICBO iCbo = Factory.GetInstance<ICBO>();
            string[] tecnicosenfermagem =
                iCbo.ListarCBOsTecnicosAuxiliarEnfermagem<CBO>().Select(p => p.Codigo).ToArray();
            ddlFiltroCBO.Items.Clear();

            IList<ViverMais.Model.CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsSaude<ViverMais.Model.CBO>().Where(cbo =>
                ((cbo.CategoriaOcupacao != null && cbo.CategoriaOcupacao.Codigo == CategoriaOcupacao.MEDICO
                && cbo.Codigo != "223305") || (cbo.CategoriaOcupacao != null && cbo.CategoriaOcupacao.Codigo == CategoriaOcupacao.ENFERMEIRO)
                || (tecnicosenfermagem.Contains(cbo.Codigo)))).ToList();

            ddlFiltroCBO.DataTextField = "Nome";
            ddlFiltroCBO.DataValueField = "Codigo";

            ddlFiltroCBO.DataSource = cbos;
            ddlFiltroCBO.DataBind();

            ddlFiltroCBO.Items.Insert(0, new ListItem("Selecione...", "0"));
        }

        /// <summary>
        /// Carrega os profissinais que tenham aquele CBO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaProfissionais(object sender, EventArgs e)
        {
            if (ddlFiltroCBO.SelectedValue != "0")
            {
                ddlProfissionais.Enabled = true;
                //IList<ViverMais.Model.VinculoProfissional> vinculos = Factory.GetInstance<IVinculo>().BuscarPorCNESCBO<VinculoProfissional>(((Usuario)Session["Usuario"]).Unidade.CNES, ddlFiltroCBO.SelectedValue).Where(p => p.Status != null && Convert.ToChar(p.Status) == Convert.ToChar(VinculoProfissional.DescricaStatus.Ativo)).ToList();
                IList<ViverMais.Model.VinculoProfissional> vinculos = Factory.GetInstance<IVinculo>().BuscarPorCNESCBO<VinculoProfissional>(((Usuario)Session["Usuario"]).Unidade.CNES, ddlFiltroCBO.SelectedValue, Convert.ToChar(VinculoProfissional.DescricaStatus.Ativo));

                ddlProfissionais.DataSource = vinculos;
                ddlProfissionais.DataBind();
                //foreach (VinculoProfissional vinculo in vinculos)
                //    ddlProfissionais.Items.Add(new ListItem(vinculo.Profissional.Nome, vinculo.Profissional.CPF));

                ddlProfissionais.Items.Insert(0, new ListItem("Selecione...", "0"));
                ddlProfissionais.Focus();
            }
            else
            {
                ddlProfissionais.Items.Clear();
                ddlProfissionais.Items.Insert(0, new ListItem("Selecione...", "0"));
                //ddlProfissionais.SelectedValue = "0";
                ddlProfissionais.Enabled = false;
            }
        }

        /// <summary>
        /// Mostra os dados do profissional selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_MostrarDadosProfissional(object sender, EventArgs e)
        {
            if (ddlProfissionais.SelectedValue != "0")
            {
                ViverMais.Model.Profissional profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(ddlProfissionais.SelectedValue.ToString());

                if (profissional != null)
                {
                    panel_dados_profissional.Visible = true;

                    lbNomeProfissional.Text = profissional.Nome;
                    lbDataNascimentoProfissional.Text = profissional.DataNascimento.ToString("dd/MM/yyyy");
                    lbCPFProfissional.Text = profissional.CPF;
                }
                else
                    panel_dados_profissional.Visible = false;
            }
            else
                panel_dados_profissional.Visible = false;
        }

        /// <summary>
        /// Mostra os dados do usuário selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_MostrarDadosUsuario(object sender, EventArgs e)
        {
            if (ddlFiltroUsuarios.SelectedValue != "0")
            {
                ViverMais.Model.Usuario usuario = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Usuario>(int.Parse(ddlFiltroUsuarios.SelectedValue.ToString()));

                if (usuario != null)
                {
                    panel_dados_usuario.Visible = true;
                    lbNomeUsuario.Text = usuario.Nome;
                    lbCartaoSUSUsuario.Text = usuario.CartaoSUS;
                    lbDataNascimentoUsuario.Text = usuario.DataNascimento.ToString("dd/MM/yyyy");
                    ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(usuario.CartaoSUS);

                    if (paciente != null)
                        lbNomeMaeUsuario.Text = paciente.NomeMae;
                    //{
                    //}
                }
                else
                    panel_dados_usuario.Visible = false;
            }
            else
                panel_dados_usuario.Visible = false;
        }

        /// <summary>
        /// Associa o usuário seleciondo com o profissional escolhido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AssociarUsuarioProfissional(object sender, EventArgs e)
        {
            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
            ViverMais.Model.UsuarioProfissionalUrgence usuarioprofissional = iUrgencia.BuscarPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>(int.Parse(ddlFiltroUsuarios.SelectedValue));

            try
            {
                if (usuarioprofissional == null)
                {
                    usuarioprofissional = new ViverMais.Model.UsuarioProfissionalUrgence();
                    usuarioprofissional.Id_Usuario = int.Parse(ddlFiltroUsuarios.SelectedValue);
                    usuarioprofissional.Id_Profissional = ddlProfissionais.SelectedValue;
                    usuarioprofissional.CodigoCBO = ddlFiltroCBO.SelectedValue;
                    usuarioprofissional.UnidadeVinculo = ((Usuario)Session["Usuario"]).Unidade.CNES;
                    iUrgencia.Insert(usuarioprofissional);
                }
                else
                {
                    usuarioprofissional.Id_Profissional = ddlProfissionais.SelectedValue;
                    usuarioprofissional.CodigoCBO = ddlFiltroCBO.SelectedValue;
                    usuarioprofissional.UnidadeVinculo = ((Usuario)Session["Usuario"]).Unidade.CNES;
                    iUrgencia.Salvar(usuarioprofissional);
                }

                //Colocar Log Aqui
                iUrgencia.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 5, "id usuario=" + usuarioprofissional.Id_Usuario + " id profissional=" + usuarioprofissional.Id_Profissional));
                //OnClick_CancelarAssociacao(sender, e);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Associação realizada com sucesso!');location='FormExibeIdentificacaoProfissional.aspx';", true);
            }
            catch (Exception f)
            {
                throw f;
            }

            try
            {
                StartBackgroundThread(delegate { this.CadastrarUsuarioSenhador(usuarioprofissional); });
            }
            catch { }
        }

        public void CadastrarUsuarioSenhador(UsuarioProfissionalUrgence usuarioProfissional)
        {
            try
            {
                IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
                iUsuarioProfissional.CadastrarUsuarioSenhador<UsuarioProfissionalUrgence>(usuarioProfissional);
            }
            catch
            {
            }
        }

        public static void StartBackgroundThread(ThreadStart threadStart)
        {
            if (threadStart != null)
            {
                Thread thread = new Thread(threadStart);
                thread.IsBackground = true;
                thread.Start();
            }
        }
    }
}
