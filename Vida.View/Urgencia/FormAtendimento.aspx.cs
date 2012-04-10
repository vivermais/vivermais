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
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Text;

namespace ViverMais.View.Urgencia
{
    public partial class FormAtendimento : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButton botaopesquisar = this.WUC_PesquisarPaciente.WUC_BotaoPesquisar;
            botaopesquisar.Click += new ImageClickEventHandler(OnClick_BuscarPaciente);

            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = botaopesquisar.UniqueID;
            trigger.EventName = "Click";
            this.UpdatePanel_PacienteSUS.Triggers.Add(trigger);

            if (!IsPostBack)
            {
                try
                {
                    if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "INICIAR_ATENDIMENTO", Modulo.URGENCIA))
                    {
                        if (Request["tipo_atendimento"] != null)
                        {
                            this.RadioButtonList_TipoAcolhimentoNormal.Items.Add(new ListItem("Adulto", AcolhimentoUrgence.ADULTO.ToString()));
                            this.RadioButtonList_TipoAcolhimentoNormal.Items.Add(new ListItem("Infantil", AcolhimentoUrgence.INFANTIL.ToString()));
                            
                            this.RadioButtonList_TipoAcolhimentoSimplificado.Items.Add(new ListItem("Adulto", AcolhimentoUrgence.ADULTO.ToString()));
                            this.RadioButtonList_TipoAcolhimentoSimplificado.Items.Add(new ListItem("Infantil", AcolhimentoUrgence.INFANTIL.ToString()));

                            this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.ImageUrl = "~/Urgencia/img/pesquisarcartao1.png";
                            this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Attributes.Add("onmouseover", "this.src='img/pesquisarcartao2.png';");
                            this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Attributes.Add("onmouseout", "this.src='img/pesquisarcartao1.png';");

                            LinkButton botaobiometria = this.WUC_PesquisarPaciente.WUC_BotaoBiometria;
                            botaobiometria.PostBackUrl = "~/Urgencia/FormAtendimentoBiometriaPaciente.aspx?tipo_atendimento=normal";

                            this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Src = "~/Urgencia/img/bts/id_biometrica1.png";
                            this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Attributes.Add("onmouseover", "this.src='img/bts/id_biometrica2.png';");
                            this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Attributes.Add("onmouseout", "this.src='img/bts/id_biometrica1.png';");

                            if (Request["tipo_atendimento"].ToString().Equals("normal"))
                                Panel_PacienteNormal.Visible = true;
                            else
                            {
                                if (Request["tipo_atendimento"].ToString().Equals("simplificado"))
                                {
                                    Panel_PacienteSimplificado.Visible = true;
                                    this.RadioButtonList_SituacaoSimplificado.SelectedValue = "Desacordado";
                                }
                            }

                            if (Request["co_paciente"] != null)
                            {
                                string codigo = Request["co_paciente"].ToString();
                                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(codigo);

                                if (Request["tipo_atendimento"].ToString().Equals("normal")) //Paciente com atendimento pelo SUS
                                {
                                    IList<ViverMais.Model.Paciente> pacientes = new List<ViverMais.Model.Paciente>();

                                    pacientes.Add(paciente);
                                    this.PacientesPesquisados = pacientes;
                                    this.CarregaPacientesSUSPesquisados();
                                }
                                else if (Request["tipo_atendimento"].ToString().Equals("simplificado")) //Caso tenha encontrado o paciente simplificado, o campo descrição será de preenchimento alternativo
                                {
                                    this.Panel_IdentificacaoPacienteSimplificado.Visible = true;
                                    OnSelectedIndexChanged_VerificaProntuarioAberto(sender, e);
                                    this.Label_NomePacienteSimplificado.Text = paciente.Nome;
                                    this.Label_MaePacienteSimplificado.Text = paciente.NomeMae;
                                    this.Label_DataNascimentoPacienteSimplificado.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
                                    RequiredFieldValidator_Descricao.Enabled = false;
                                }
                            }
                        }
                    }
                    else
                        Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
        }

        private ViverMais.Model.Paciente PacienteSelecionadoAtendimentoNormal
        {
            get { return (ViverMais.Model.Paciente)ViewState["pacienteAtendimentoNormal"]; }
            set { ViewState["pacienteAtendimentoNormal"] = value; }
        }

        private IList<ViverMais.Model.Paciente> PacientesPesquisados
        {
            get { return (IList<ViverMais.Model.Paciente>)ViewState["pacientesPesquisados"]; }
            set { ViewState["pacientesPesquisados"] = value; }
        }

        /// <summary>
        /// Verifica se existe um prontuário aberto no caso do paciente chegar desorientado e for identificado como paciente do SUS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_VerificaProntuarioAberto(object sender, EventArgs e)
        {
            ViverMais.Model.Paciente paciente = null;

            if (Request["co_paciente"] != null)
            {
                paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(Request["co_paciente"].ToString());
                ViverMais.Model.Prontuario prontuarioAberto = Factory.GetInstance<IProntuario>().BuscarProntuarioAberto<ViverMais.Model.EstabelecimentoSaude, ViverMais.Model.Prontuario>(paciente.Codigo, ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade);

                if (prontuarioAberto != null)
                {
                    this.LinkButton_IniciarAtendimentoSimplificado.OnClientClick = "javascript:return confirm('Atenção usuário, o paciente escolhido está em atendimento na unidade!" +
                                       " Para prosseguir, você precisa finalizar o registro eletrônico em aberto do paciente. Deseja fazê-lo agora?');";
                }
                else
                    this.LinkButton_IniciarAtendimentoSimplificado.OnClientClick = string.Empty;
            }
            else
                this.LinkButton_IniciarAtendimentoSimplificado.OnClientClick = string.Empty;

            if (this.RadioButtonList_SituacaoSimplificado.SelectedValue.Equals("Desacordado"))
                this.Panel_TipoAcolhimentoSimplificado.Visible = false;
            else
            {
                this.Panel_TipoAcolhimentoSimplificado.Visible = true;

                if (paciente == null)
                    this.RadioButtonList_TipoAcolhimentoSimplificado.SelectedValue = AcolhimentoUrgence.ADULTO.ToString();
                else 
                {
                    if (paciente.Idade > 17)
                        this.RadioButtonList_TipoAcolhimentoSimplificado.SelectedValue = AcolhimentoUrgence.ADULTO.ToString();
                    else
                        this.RadioButtonList_TipoAcolhimentoSimplificado.SelectedValue = AcolhimentoUrgence.INFANTIL.ToString();
                }
            }
        }

        /// <summary>
        /// Inicia o atendimento para o paciente
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Argumento de inicialização</param>
        protected void OnClick_IniciarAtendimento(object sender, EventArgs e)
        {
            Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();

            string numeroatendimento = iProntuario.IniciarAtendimento<Usuario>(usuario, this.PacienteSelecionadoAtendimentoNormal.Codigo, 
                Convert.ToChar(this.RadioButtonList_TipoAcolhimentoNormal.SelectedValue));
            Prontuario prontuario = iProntuario.BuscarPorNumeroProntuario<Prontuario>(long.Parse(numeroatendimento));

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro eletrônico de atendimento número: " + prontuario.Numero.ToString() + ".');window.open('FormImprimirSenhaAcolhimentoAtendimento.aspx?co_prontuario=" + prontuario.Codigo.ToString() + "&tipo_impressao=acolhimento','Impressão','height = 270, width = 250');", true);

            this.Panel_AcoesPosAtendimentoNormal.Visible = true;
            this.Panel_LinkButtonIniciarAtendimentoNormal.Visible = false;
            //this.LinkButton_IniciarAtendimentoNormal.Visible = false;

            this.LinkButton_ImprimirFichaAtendimento.CommandArgument = numeroatendimento;
            this.LinkButton_ReimprimirSenhaAcolhimento.CommandArgument = prontuario.Codigo.ToString();

            HelperView.ExecutarPlanoContingencia(usuario.Codigo, prontuario.Codigo);
        }

        protected void OnClick_ReimprimirSenhaAcolhimento(object sender, EventArgs e)
        {
            long co_prontuario = long.Parse(((LinkButton)sender).CommandArgument.ToString());
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "window.open('FormImprimirSenhaAcolhimentoAtendimento.aspx?co_prontuario=" + co_prontuario.ToString() + "&tipo_impressao=acolhimento','Impressão','height = 270, width = 250');", true);
        }

        /// <summary>
        /// Imprime a ficha de atendimento do paciente atendido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_ImprimirFichaAtendimentoPaciente(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('FormImprimirFichaAtendimento.aspx?numeroatendimento=" + ((LinkButton)sender).CommandArgument.ToString() + "');", true);
        }

        /// <summary>
        /// Salva o paciente simplificado
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Argumento de inicialização</param>
        protected void OnClick_IniciarAtendimentoSimplificado(object sender, EventArgs e)
        {
            Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
            char ? tipoacolhimento = null;

            if (this.Panel_TipoAcolhimentoSimplificado.Visible)
                tipoacolhimento = Convert.ToChar(this.RadioButtonList_TipoAcolhimentoSimplificado.SelectedValue);

            string numeroatendimento = Factory.GetInstance<IProntuario>().IniciarAtendimento<Usuario>(usuario, Request.QueryString["co_paciente"],
                this.RadioButtonList_SituacaoSimplificado.SelectedValue.Equals("Desacordado"), tbxDescricao.Text, tipoacolhimento);

            this.Panel_AcoesPacienteSimplificado.Visible = true;
            this.Panel_SalvarPacienteSimplificado.Visible = false;

            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            Prontuario prontuario = iProntuario.BuscarPorNumeroProntuario<Prontuario>(long.Parse(numeroatendimento));

            if (!prontuario.Desacordado)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro eletrônico de atendimento número: " + prontuario.Numero.ToString() + ".');window.open('FormImprimirSenhaAcolhimentoAtendimento.aspx?co_prontuario=" + prontuario.Codigo.ToString() + "&tipo_impressao=acolhimento','Impressão','height = 270, width = 250');", true);
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro eletrônico de atendimento número: " + prontuario.Numero.ToString() + ".');window.open('FormImprimirSenhaAcolhimentoAtendimento.aspx?co_prontuario=" + prontuario.Codigo.ToString() + "&tipo_impressao=atendimento','Impressão','height = 270, width = 250');", true);

            this.LinkButton_ReimprimirSenhaAtendimento.CommandArgument = prontuario.Codigo.ToString();

            HelperView.ExecutarPlanoContingencia(usuario.Codigo, prontuario.Codigo);
        }

        protected void OnClick_ReimprimirSenhaAtendimento(object sender, EventArgs e)
        {
            long co_prontuario = long.Parse(((LinkButton)sender).CommandArgument.ToString());

            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            Prontuario prontuario = iProntuario.BuscarPorCodigo<Prontuario>(co_prontuario);

            if (!prontuario.Desacordado)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "window.open('FormImprimirSenhaAcolhimentoAtendimento.aspx?co_prontuario=" + prontuario.Codigo.ToString() + "&tipo_impressao=acolhimento','Impressão','height = 270, width = 250');", true);
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "window.open('FormImprimirSenhaAcolhimentoAtendimento.aspx?co_prontuario=" + prontuario.Codigo.ToString() + "&tipo_impressao=atendimento','Impressão','height = 270, width = 250');", true);
        }

        /// <summary>
        /// Cria os botões para validar os prontuários que estejam abertos para cada um dos pacientes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_CriandoGridView(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ViverMais.Model.Prontuario prontuarioAberto = Factory.GetInstance<IProntuario>().BuscarProntuarioAberto<ViverMais.Model.EstabelecimentoSaude, ViverMais.Model.Prontuario>(GridView_ResultadoPesquisa.DataKeys[e.Row.RowIndex]["Codigo"].ToString(), ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade);

            //    if (prontuarioAberto != null)
            //    {
            //        LinkButton bt = (LinkButton)e.Row.FindControl("btnIniciarAtendimento");
            //        bt.OnClientClick = "javascript:return confirm('Atenção usuário, o paciente escolhido está em atendimento na unidade!" +
            //                           " Para prosseguir, você precisa finalizar o registro eletrônico em aberto do paciente. Deseja fazê-lo agora?');";
            //    }
            //}
            //else
            //{
            if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                
                HyperLink linkNovoPaciente = (HyperLink)e.Row.FindControl("HyperLink_NovoPaciente");
                linkNovoPaciente.NavigateUrl = HelperRedirector.URLRelativaAplicacao() +
                    "Paciente/FormPaciente.aspx?url_retorno=" +
                    HelperRedirector.EncodeURL(this.Request.Url.ToString() + "&co_paciente=");

                linkNovoPaciente.DataBind();
            }
            //}
        }

        protected void OnSelectedIndexChanging_AtendimentoNormal(object sender, GridViewSelectEventArgs e)
        {
            IPaciente iPaciente = Factory.GetInstance<IPaciente>();

            this.PacienteSelecionadoAtendimentoNormal = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(
                this.GridView_ResultadoPesquisa.DataKeys[e.NewSelectedIndex]["Codigo"].ToString());

            if (this.PacienteSelecionadoAtendimentoNormal.Idade <= 17)
                this.RadioButtonList_TipoAcolhimentoNormal.SelectedValue = AcolhimentoUrgence.INFANTIL.ToString();
            else
                this.RadioButtonList_TipoAcolhimentoNormal.SelectedValue = AcolhimentoUrgence.ADULTO.ToString();

            ViverMais.Model.Prontuario prontuarioAberto = Factory.GetInstance<IProntuario>().BuscarProntuarioAberto<ViverMais.Model.EstabelecimentoSaude, ViverMais.Model.Prontuario>(this.PacienteSelecionadoAtendimentoNormal.Codigo,
                ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade);

            if (prontuarioAberto != null)
            {
                this.LinkButton_IniciarAtendimentoNormal.OnClientClick = "javascript:return confirm('Atenção usuário, o paciente escolhido " +
                    " está em atendimento na unidade! Para prosseguir, você precisa finalizar o registro eletrônico em aberto do paciente. " +
                    "Deseja fazê-lo agora?');";
            }
            else
                this.LinkButton_IniciarAtendimentoNormal.OnClientClick = string.Empty;

            this.Panel_AtendimentoNormal.Visible = true;
            this.Panel_LinkButtonIniciarAtendimentoNormal.Visible = true;
            //this.LinkButton_IniciarAtendimentoNormal.Visible = true;
            this.Panel_AcoesPosAtendimentoNormal.Visible = false;

            this.Label_NomePacienteNormal.Text = this.PacienteSelecionadoAtendimentoNormal.Nome;
            this.Label_DataNascimentoPacienteNormal.Text = this.PacienteSelecionadoAtendimentoNormal.DataNascimento.ToString("dd/MM/yyyy");
            this.Label_MaePacienteNormal.Text = this.PacienteSelecionadoAtendimentoNormal.NomeMae;
        }

        /// <summary>
        /// Pesquisa o paciente que possua cartão SUS
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Argumento de Inicialização</param>
        protected void OnClick_BuscarPaciente(object sender, ImageClickEventArgs e)
        {
            if (this.WUC_PesquisarPaciente.Page.IsValid)
            {
                this.WUC_PesquisarPaciente.GridView.Visible = false;
                this.WUC_PesquisarPaciente.WUC_UpdatePanel_ResultadoPesquisa.Update();

                string cartaosus = this.WUC_PesquisarPaciente.WUC_CartaoSUSPesquisado;
                string datanascimento = this.WUC_PesquisarPaciente.WUC_DataNascimentoPesquisado;
                string nomepaciente = this.WUC_PesquisarPaciente.WUC_PacientePesquisado;
                string nomemae = this.WUC_PesquisarPaciente.WUC_MaePesquisado;

                IList<ViverMais.Model.Paciente> pacientes = new List<ViverMais.Model.Paciente>();

                if (!string.IsNullOrEmpty(cartaosus))
                {
                    //Regex validaCartaoSUS = new Regex(@"^\d{15}$");

                    //if (validaCartaoSUS.IsMatch(cartaosus))
                    //{                        
                    ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(cartaosus);

                    if (paciente != null)
                        pacientes.Add(paciente);

                    this.PacientesPesquisados = pacientes;
                    this.CarregaPacientesSUSPesquisados();
                    //}
                    //else
                    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O cartão SUS deve conter 15 dígitos!');", true);
                }
                else
                {
                    pacientes = Factory.GetInstance<IPaciente>().PesquisarPaciente<ViverMais.Model.Paciente>(nomepaciente, !string.IsNullOrEmpty(nomemae) ? nomemae : "", !string.IsNullOrEmpty(datanascimento) ? DateTime.Parse(datanascimento) : DateTime.MinValue);

                    this.PacientesPesquisados = pacientes;
                    this.CarregaPacientesSUSPesquisados();
                }
            }
        }

        public void CarregaPacientesSUSPesquisados()
        {
            this.GridView_ResultadoPesquisa.DataSource = this.PacientesPesquisados;
            this.GridView_ResultadoPesquisa.DataBind();

            this.Panel_PacientesPesquisados.Visible = true;
            this.Panel_AcoesPosAtendimentoNormal.Visible = false;
            this.Panel_AtendimentoNormal.Visible = false;

            this.UpdatePanel_PacienteSUS.Update();
        }

        /// <summary>
        /// Paginação do GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            this.CarregaPacientesSUSPesquisados();

            this.GridView_ResultadoPesquisa.PageIndex = e.NewPageIndex;
            this.GridView_ResultadoPesquisa.DataBind();
        }
    }
}
