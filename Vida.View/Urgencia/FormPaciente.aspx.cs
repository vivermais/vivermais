using System;
using ViverMais.DAO;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.IO;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace ViverMais.View.Urgencia
{
    public partial class FormPaciente : System.Web.UI.Page
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
                        if (Request["tipo_paciente"] != null)
                        {
                            this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.ImageUrl = "~/Urgencia/img/pesquisarcartao1.png";
                            this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Attributes.Add("onmouseover", "this.src='img/pesquisarcartao2.png';");
                            this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Attributes.Add("onmouseout", "this.src='img/pesquisarcartao1.png';");

                            LinkButton botaobiometria = this.WUC_PesquisarPaciente.WUC_BotaoBiometria;
                            botaobiometria.PostBackUrl = "~/Urgencia/BiometriaPacienteSUS.aspx";

                            this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Src = "~/Urgencia/img/bts/id_biometrica1.png";
                            this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Attributes.Add("onmouseover", "this.src='img/bts/id_biometrica2.png';");
                            this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Attributes.Add("onmouseout", "this.src='img/bts/id_biometrica1.png';");

                            CarregaDadosPaciente(Request["tipo_paciente"].ToString());

                            if (Request["codigo"] != null) //Caso tenha encontrado o paciente simplificado o campo descrição será de preenchimento alternativo
                            {
                                string codigo = Request["codigo"].ToString();
                                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(codigo);

                                if (Request["tipo_paciente"].ToString().Equals("pacientesus")) //Paciente com atendimento pelo SUS
                                {
                                    IList<ViverMais.Model.Paciente> pacientes = new List<ViverMais.Model.Paciente>();
                                    pacientes.Add(paciente);
                                    GridView_ResultadoPesquisa.DataSource = pacientes;
                                    GridView_ResultadoPesquisa.DataBind();
                                    PanelResultado.Visible = true;
                                }
                                else if (Request["tipo_paciente"].ToString().Equals("pacientesimplificado"))
                                {
                                    Panel_IdentificacaoPaciente.Visible = true;
                                    OnCheckedChanged_VerificaProntuarioAberto(sender, e);
                                    Label_NomePaciente.Text = paciente.Nome;
                                    Label_NomeMaePaciente.Text = paciente.NomeMae;
                                    Label_DataNascimentoPaciente.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
                                    RequiredFieldValidator_Descricao.Enabled = false;
                                }
                            }
                        }
                    }
                    else
                        Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
        }

        /// <summary>
        /// Carrega os dados necessários a depender do tipo do paciente
        /// </summary>
        /// <param name="tipo_paciente">tipo do paciente</param>
        private void CarregaDadosPaciente(string tipo_paciente)
        {
            if (tipo_paciente.Equals("pacientesus"))
                Panel_PacienteNormal.Visible = true;
            else if (tipo_paciente.Equals("pacientesimplificado"))
            {
                Panel_PacienteSimplificado.Visible = true;
                RadioButton_Desacordado.Checked = true;
            }
        }

        /// <summary>
        /// Verifica se existe um prontuário aberto no caso do paciente chegar desorientado e for identificado como paciente do SUS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCheckedChanged_VerificaProntuarioAberto(object sender, EventArgs e)
        {
            if (Request["codigo"] != null)
            {
                ViverMais.Model.Prontuario prontuarioAberto = Factory.GetInstance<IProntuario>().BuscarProntuarioAberto<ViverMais.Model.EstabelecimentoSaude, ViverMais.Model.Prontuario>(Request["codigo"].ToString(), ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade);
                if (prontuarioAberto != null)
                {
                    btSalvarPaciente.OnClientClick = "javascript:return confirm('Atenção usuário, o paciente escolhido está em atendimento na unidade!" +
                                       " Para prosseguir, você precisa finalizar o registro eletrônico em aberto do paciente. Deseja fazê-lo agora?');";
                }
                else
                    if (btSalvarPaciente.OnClientClick.Length > 0)
                        btSalvarPaciente.OnClientClick.Remove(0);
            }
            else
                if (btSalvarPaciente.OnClientClick.Length > 0)
                    btSalvarPaciente.OnClientClick.Remove(0);
        }

        /// <summary>
        /// Inicia o atendimento para o paciente
        /// </summary>
        /// <param name="sender">Objeto de envio</param>
        /// <param name="e">Argumento de inicialização</param>
        protected void OnClick_IniciarAtendimento(object sender, EventArgs e)
        {
            //try
            //{
            Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
            string codigoPacienteViverMais = ((LinkButton)sender).CommandArgument.ToString();

            string numeroatendimento = Factory.GetInstance<IProntuario>().IniciarAtendimento<Usuario>(usuario, codigoPacienteViverMais);
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            Prontuario prontuario = iProntuario.BuscarPorNumeroProntuario<Prontuario>(long.Parse(numeroatendimento));

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro eletrônico de atendimento número: " + prontuario.Numero.ToString() + ".');window.open('FormImprimirSenhaAcolhimentoAtendimento.aspx?co_prontuario=" + prontuario.Codigo.ToString() + "&tipo_impressao=acolhimento','Impressão','height = 270, width = 250');", true);

            PanelResultado.Visible = false;
            this.Panel_AcoesPacienteSUS.Visible = true;
            this.LinkButton_ImprimirFichaAtendimento.CommandArgument = numeroatendimento;
            this.LinkButton_ReimprimirSenhaAcolhimento.CommandArgument = prontuario.Codigo.ToString();
            //}
            //catch (Exception f)
            //{
            //    throw f;
            //}

            //try
            //{    
            //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, prontuario.Codigo); });
            //}
            //catch { }
            HelperView.ExecutarPlanoContingencia(usuario.Codigo, prontuario.Codigo);
        }

        ///// <summary>
        ///// Função que executa um procedimento em background
        ///// </summary>
        ///// <param name="threadStart"></param>
        //public void StartBackgroundThread(ThreadStart threadStart)
        //{
        //    if (threadStart != null)
        //    {
        //        Thread thread = new Thread(threadStart);
        //        thread.IsBackground = true;
        //        thread.Start();
        //    }
        //}

        ///// <summary>
        ///// Executa o plano de contingência
        ///// </summary>
        ///// <param name="co_usuario">código do usuário</param>
        ///// <param name="co_prontuario">código do prontuário</param>
        //public void ExecutarPlanoContingencia(int co_usuario, long co_prontuario)
        //{
        //    try
        //    {
        //        IProntuario iProntuario = Factory.GetInstance<IProntuario>();
        //        iProntuario.ExecutarPlanoContingencia(co_usuario, co_prontuario);
        //    }
        //    catch
        //    {
        //    }
        //}

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
        protected void OnClick_SalvarPaciente(object sender, EventArgs e)
        {
            Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];

            string numeroatendimento = Factory.GetInstance<IProntuario>().IniciarAtendimento<Usuario>(usuario, Request.QueryString["codigo"] != null ? Request.QueryString["codigo"] : "", RadioButton_Desacordado.Checked ? SituacaoAtendimento.AGUARDANDO_ATENDIMENTO : SituacaoAtendimento.ATENDIMENTO_INICIAL, tbxDescricao.Text);
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
            //try
            //{
            //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, prontuario.Codigo); });
            //}
            //catch { }
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ViverMais.Model.Prontuario prontuarioAberto = Factory.GetInstance<IProntuario>().BuscarProntuarioAberto<ViverMais.Model.EstabelecimentoSaude, ViverMais.Model.Prontuario>(GridView_ResultadoPesquisa.DataKeys[e.Row.RowIndex]["Codigo"].ToString(), ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade);

                if (prontuarioAberto != null)
                {
                    LinkButton bt = (LinkButton)e.Row.FindControl("btnIniciarAtendimento");
                    bt.OnClientClick = "javascript:return confirm('Atenção usuário, o paciente escolhido está em atendimento na unidade!" +
                                       " Para prosseguir, você precisa finalizar o registro eletrônico em aberto do paciente. Deseja fazê-lo agora?');";
                }
            }
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

                if (!string.IsNullOrEmpty(cartaosus))
                {
                    Regex validaCartaoSUS = new Regex(@"^\d{15}$");

                    if (validaCartaoSUS.IsMatch(cartaosus))
                    {
                        ViewState.Remove("nomepaciente");
                        ViewState.Remove("nomemae");
                        ViewState.Remove("datanascimento");
                        ViewState["cartaosus"] = cartaosus;
                        CarregaGridView(cartaosus, "", "", "");
                        this.UpdatePanel_PacienteSUS.Update();
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O cartão SUS deve conter 15 dígitos!');", true);
                }
                else
                {
                    //char[] del = { ' ' };
                    //if (!string.IsNullOrEmpty(tbxNomePaciente.Text) && tbxNomePaciente.Text.Split(del).Length >= 2 && ((!string.IsNullOrEmpty(tbxNomeMae.Text) && tbxNomeMae.Text.Split(del).Length >= 2) || !string.IsNullOrEmpty(tbxDataNascimento.Text)))
                    //{
                    ViewState.Remove("cartaosus");
                    ViewState["nomepaciente"] = nomepaciente;
                    ViewState["nomemae"] = nomemae;
                    ViewState["datanascimento"] = datanascimento;
                    CarregaGridView("", nomepaciente, nomemae, datanascimento);
                    this.UpdatePanel_PacienteSUS.Update();
                    //}
                    //else
                    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe um dos seguintes campos: \\n (1) Número do cartão SUS. \\n (2) Nome e Sobrenome do Paciente e Nome e Sobrenome da Mãe. \\n (3) Nome e Sobrenome do Paciente e Data de Nascimento.');", true);
                }
            }
        }

        /// <summary>
        /// Carrega o gridview de pacientes para o usuário
        /// </summary>
        /// <param name="cartao_sus">número do cartão sus</param>
        /// <param name="nomepaciente">nome do paciente</param>
        /// <param name="nomemae">nome da mãe</param>
        /// <param name="datanascimento">data de nascimento do paciente</param>
        private void CarregaGridView(string cartao_sus, string nomepaciente, string nomemae, string datanascimento)
        {
            IList<ViverMais.Model.Paciente> pacientes = new List<ViverMais.Model.Paciente>();

            if (cartao_sus != "")
            {
                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(cartao_sus);
                if (paciente != null)
                    pacientes.Add(paciente);
            }
            else
            {
                pacientes = Factory.GetInstance<IPaciente>().PesquisarPaciente<ViverMais.Model.Paciente>(nomepaciente, !string.IsNullOrEmpty(nomemae) ? nomemae : "", !string.IsNullOrEmpty(datanascimento) ? DateTime.Parse(datanascimento) : DateTime.MinValue);
            }

            GridView_ResultadoPesquisa.DataSource = pacientes;
            GridView_ResultadoPesquisa.DataBind();

            PanelResultado.Visible = true;
            this.Panel_AcoesPacienteSUS.Visible = false;
        }

        /// <summary>
        /// Paginação do GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["cartaosus"] != null)
                CarregaGridView(ViewState["cartaosus"].ToString(), "", "", "");
            else
                CarregaGridView("", ViewState["nomepaciente"] != null ? ViewState["nomepaciente"].ToString() : "", ViewState["nomemae"] != null ? ViewState["nomemae"].ToString() : "", ViewState["datanascimento"] != null ? ViewState["datanascimento"].ToString() : "");

            GridView_ResultadoPesquisa.PageIndex = e.NewPageIndex;
            GridView_ResultadoPesquisa.DataBind();
        }

        protected void lnkBiometria_Click(object sender, EventArgs e)
        {
            //if (Request["tipo_paciente"] != null)
            //{
            //if (Request["tipo_paciente"] == "1")
            //    Response.Redirect("BiometriaPacienteSUS.aspx");
            //else 
            Response.Redirect("FormIdentificacaoBiometrica.aspx");
            //}
        }
    }
}
