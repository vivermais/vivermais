﻿using System;
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
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using InfoSoftGlobal;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Threading;

namespace ViverMais.View.Urgencia
{
    public partial class FormEvolucaoMedica : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DropDownList_ClassificacaoRisco.Attributes.CssStyle.Add("float", "left");
            this.InserirTrigger(this.LinkButton_HistoricoMedico.UniqueID, "Click", this.UpdatePanel_HistoricoMedico);
            this.InserirTrigger(this.LinkButton_HistoricoSuspeitaDiagnostica.UniqueID, "Click", this.UpdatePanel_HistoricoMedico);

            ImageButton suspeitacodigo = this.WUC_SuspeitaDiagnostica.WUC_ImageButtonBuscarCID;
            ImageButton suspeitanome = this.WUC_SuspeitaDiagnostica.WUC_ImageButtonBuscarCIDPorNome;
            DropDownList suspeitalista = this.WUC_SuspeitaDiagnostica.WUC_DropDownListGrupoCID;

            suspeitacodigo.Click += new ImageClickEventHandler(this.OnClick_BuscarCIDEvolucaoMedica);
            suspeitanome.Click += new ImageClickEventHandler(this.OnClickBuscarCIDPorNome);
            suspeitalista.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChanged_BuscarCidsEvolucaoMedica);

            this.InserirTrigger(suspeitacodigo.UniqueID, "Click", UpdatePanel_SuspeitaDiagnostica);
            this.InserirTrigger(suspeitanome.UniqueID, "Click", UpdatePanel_SuspeitaDiagnostica);
            this.InserirTrigger(suspeitalista.UniqueID, "SelectedIndexChanged", UpdatePanel_SuspeitaDiagnostica);

            ImageButton procedimentocidcodigo = this.WUC_ProcedimentoCid.WUC_ImageButtonBuscarCID;
            ImageButton procedimentocidnome = this.WUC_ProcedimentoCid.WUC_ImageButtonBuscarCIDPorNome;
            DropDownList procedimentocidlista = this.WUC_ProcedimentoCid.WUC_DropDownListGrupoCID;

            procedimentocidcodigo.Click += new ImageClickEventHandler(this.OnClick_BuscarProcimentoCID);
            procedimentocidnome.Click += new ImageClickEventHandler(this.OnClickBuscarProcedimentoCIDPorNome);
            procedimentocidlista.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChanged_ProcecimentoCid);

            this.InserirTrigger(procedimentocidcodigo.UniqueID, "Click", UpdatePanel_ProcedimentoCID);
            this.InserirTrigger(procedimentocidnome.UniqueID, "Click", UpdatePanel_ProcedimentoCID);
            this.InserirTrigger(procedimentocidlista.UniqueID, "SelectedIndexChanged", UpdatePanel_ProcedimentoCID);

            this.InserirTrigger(this.Button_AdicionarProcedimentoEvolucaoMedica.UniqueID, "Click", this.WUC_ProcedimentoCid.WUC_UpdatePanelPesquisarCID);

            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_EVOLUCAO_MEDICA", Modulo.URGENCIA))
                {
                    ViverMais.Model.UsuarioProfissionalUrgence up = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<ViverMais.Model.UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);

                    if (up != null)
                    {
                        if (up.UnidadeVinculo == ((Usuario)Session["Usuario"]).Unidade.CNES)
                        {
                            ICBO iCbo = Factory.GetInstance<ICBO>();
                            CBO cbo = iCbo.BuscarPorCodigo<CBO>(Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo).CodigoCBO);

                            //if (VerificaFuncionalidadesMedico())
                            if (iCbo.CBOPertenceMedico<CBO>(cbo))
                            {
                                long temp;
                                if (Request["codigo"] != null && long.TryParse(Request["codigo"].ToString(), out temp))
                                {
                                    Session["URL_UrgenciaVoltarAprazamento"] = HttpContext.Current.Request.Url.AbsoluteUri;
                                    ((MasterUrgencia)Master).MenuVisivel = false;

                                    CarregaDadosIniciais();
                                    Factory.GetInstance<IPrescricao>().AtualizarStatusPrescricoesProntuario(long.Parse(Request["codigo"].ToString()));

                                    ViverMais.Model.Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<ViverMais.Model.Prontuario>(long.Parse(Request["codigo"].ToString()));

                                    if (prontuario != null)
                                    {
                                        ViewState["co_prontuario"] = prontuario.Codigo;

                                        lblNumero.Text = prontuario.NumeroToString;
                                        lblData.Text = prontuario.Data.ToString("dd/MM/yyyy");
                                        lblPaciente.Text = prontuario.NomePacienteToString;
                                        DropDownList_SituacaoEvolucaoMedica.SelectedValue = prontuario.Situacao.Codigo.ToString();
                                        DropDownList_ClassificacaoRisco.SelectedValue = prontuario.ClassificacaoRisco.Codigo.ToString();

                                        this.OnSelectedIndexChanged_ClassificacaoRisco(new object(), new EventArgs());
                                    }
                                }
                            }
                            else
                                Response.Redirect("FormAcessoNegado.aspx?opcao=2");
                                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, somente profissionais médicos tem acesso a esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                        }
                        else
                            Response.Redirect("FormAcessoNegado.aspx?opcao=4");
                            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, o seu profissional identificado não possui vínculo com a sua atual unidade. Por favor, entrar em contato com a administração.');location='Default.aspx';", true);
                    }
                    else
                        Response.Redirect("FormAcessoNegado.aspx?opcao=5");
                        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Profissional não identificado! Por favor, identifique o usuário.');location='Default.aspx';", true);
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Insere uma trigger do tipo async em um updatepanel específico
        /// </summary>
        /// <param name="idcontrole">controle que disparará a trigger</param>
        /// <param name="nomeevento">nome do evento associado ao controle</param>
        /// <param name="updatepanel">updatepanel onde será adicionado a trigger</param>
        private void InserirTrigger(string idcontrole, string nomeevento, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = idcontrole;
            trigger.EventName = nomeevento;
            updatepanel.Triggers.Add(trigger);
            MasterMain mm = (MasterMain)Master.Master;
            ((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(this.FindControl(idcontrole));
        }

        protected void OnClick_MostrarQuadroAtendimento(object sender, EventArgs e)
        {
            Literal_GraficoAtendimento.Text = Factory.GetInstance<IRelatorioUrgencia>().GraficoFilaAtendimentoUnidade(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);
            Panel_QuadroAtendimento.Visible = true;
        }

        protected void OnClick_FecharQuadroAtendimento(object sender, EventArgs e)
        {
            Panel_QuadroAtendimento.Visible = false;
        }

        protected void OnClick_VerBulario(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('http://www.anvisa.gov.br/bularioeletronico/default.asp?txtPrincipioAtivo=" + DropDownList_MedicamentoEvolucaoMedica.SelectedItem.Text + "');", true);
        }

        protected void OnSelectedIndexChanged_ClassificacaoRisco(object sender, EventArgs e)
        {
            ClassificacaoRisco classificacaorisco = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ClassificacaoRisco>(int.Parse(DropDownList_ClassificacaoRisco.SelectedValue));
            Image_ClassificacaoRisco.ImageUrl = "~/Urgencia/img/" + classificacaorisco.Imagem;
        }

        #region EVOLUÇÃO MÉDICA
        /// <summary>
        /// Gera a receita/atestado para o prontuário corrente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_GerarReceitaAtestado(object sender, EventArgs e)
        {
            //ImageButton bt = (ImageButton)sender;

            //if (bt.CommandArgument == "receita")
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Receita','../FormGerarAtestadoReceita.aspx?model=prescription&co_prontuario=" + Request["codigo"].ToString() + "&co_profissional=" + Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo).Id_Profissional + "');", true);
            //else
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Atestado','../FormGerarAtestadoReceita.aspx?model=attested&co_prontuario=" + Request["codigo"].ToString() + "&co_profissional=" + Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo).Id_Profissional + "');", true);
        }

        /// <summary>
        /// Salva a evolução médica para este prontuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_SalvarEvolucaoMedica(object sender, EventArgs e)
        {
            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
            IVagaUrgencia iVaga = Factory.GetInstance<IVagaUrgencia>();
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();

            ViverMais.Model.Prontuario prontuario = iProntuario.BuscarPorCodigo<ViverMais.Model.Prontuario>(long.Parse(Request["codigo"]));
            ViverMais.Model.VagaUrgencia vagaProntuario = iVaga.BuscarPorProntuario<ViverMais.Model.VagaUrgencia>(prontuario.Codigo);
            ViverMais.Model.UsuarioProfissionalUrgence usuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<ViverMais.Model.UsuarioProfissionalUrgence>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
            Prescricao prescricaovigente = Factory.GetInstance<IPrescricao>().RetornaPrescricaoVigente<Prescricao>(prontuario.Codigo);

            //se zero, mantém a vaga como está; se 1, ocupa vaga; se 2, desocupa vaga.
            int ocupa_vaga = 0;
            char tipo_vaga = ' ';
            bool agendarprescricao = !this.CheckBox_Vigencia.Checked;

            if (prontuario.Situacao.Codigo != SituacaoAtendimento.EM_OBSERVACAO_UNIDADE && int.Parse(DropDownList_SituacaoEvolucaoMedica.SelectedValue) == SituacaoAtendimento.EM_OBSERVACAO_UNIDADE) //Ocupa Vaga
            {
                ocupa_vaga = 1;

                if (prontuario.Idade <= 12)
                    tipo_vaga = VagaUrgencia.INFANTIL;
                else
                    tipo_vaga = prontuario.Paciente.Sexo;

                vagaProntuario = iVaga.VerificaDisponibilidadeVaga<ViverMais.Model.VagaUrgencia>(tipo_vaga, ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);
                if (vagaProntuario == null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Número de vagas insuficientes para ocupação deste paciente!');", true);
                    return;
                }
            }

            if (prontuario.Situacao.Codigo == SituacaoAtendimento.EM_OBSERVACAO_UNIDADE && int.Parse(DropDownList_SituacaoEvolucaoMedica.SelectedValue) != SituacaoAtendimento.EM_OBSERVACAO_UNIDADE)//desocupa a vaga
                ocupa_vaga = 2;

            if (!CustomValidator_ConsultaMedica.IsValid)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_ConsultaMedica.ErrorMessage + "');", true);
                return;
            }

            if (RetornaListaMedicamento().Count() > 0 || RetornaListaProcedimento().Count() > 0 || RetornaListaProcedimentoNaoFaturavel().Count() > 0) //Existe nova prescrição
            {
                if (Factory.GetInstance<IPrescricao>().BuscarPorProntuario<Prescricao>(prontuario.Codigo, Convert.ToChar(Prescricao.StatusPrescricao.Agendada)).FirstOrDefault() != null &&
                    agendarprescricao)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Atenção usuário, não é possível registrar uma prescrição agendada quando já existe uma prescrição com o mesmo status.');", true);
                    return;
                }

                if (prescricaovigente != null) //Validar a possibilidade da nova prescrição
                {
                    if (prescricaovigente.Status == Convert.ToChar(Prescricao.StatusPrescricao.Invalida) && agendarprescricao)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Atenção usuário, não é possível registrar uma prescrição agendada quando a prescrição VIGENTE possui status INVÁLIDA.');", true);
                        return;
                    }
                    else
                    {
                        if (prescricaovigente.Status == Convert.ToChar(Prescricao.StatusPrescricao.Valida) && !agendarprescricao)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Atenção usuário, não é possível registrar uma prescrição VÁLIDA quando já existe uma prescrição com o mesmo status.');", true);
                            return;
                        }
                    }
                }
                else
                {
                    if (agendarprescricao)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Atenção usuário, para registrar uma prescrição agendada é necessário a existência de uma prescrição válida.');", true);
                        return;
                    }
                }
            }

            prontuario.Situacao = iUrgencia.BuscarPorCodigo<ViverMais.Model.SituacaoAtendimento>(int.Parse(DropDownList_SituacaoEvolucaoMedica.SelectedValue));
            prontuario.ClassificacaoRisco = iUrgencia.BuscarPorCodigo<ClassificacaoRisco>(int.Parse(DropDownList_ClassificacaoRisco.SelectedValue));

            //se alta médica ou alta a pedido, insere no prontuario as informações do sumario de alta
            if (prontuario.Situacao.Codigo == SituacaoAtendimento.ALTA_PEDIDO || prontuario.Situacao.Codigo == SituacaoAtendimento.ALTA_MEDICA)
                prontuario.SumarioAlta = TextBox_SumarioAltaEvolucaoMedica.Text;
            else if (prontuario.Situacao.Codigo == SituacaoAtendimento.TRANSFERENCIA)
                prontuario.CodigoUnidadeTransferencia = int.Parse(DropDownList_UnidadeTransferencia.SelectedValue);

            ViverMais.Model.EvolucaoMedica evolucaoMedica = new ViverMais.Model.EvolucaoMedica();
            evolucaoMedica.PrimeiraConsulta = false;
            evolucaoMedica.CodigoProfissional = usuarioProfissional.Id_Profissional;
            evolucaoMedica.CBOProfissional = usuarioProfissional.CodigoCBO;
            evolucaoMedica.Observacao = TextBox_ObservacaoEvolucaoMedica.Text;
            evolucaoMedica.Data = DateTime.Now;
            evolucaoMedica.Prontuario = prontuario;
            evolucaoMedica.ClassificacaoRisco = prontuario.ClassificacaoRisco;

            if (RetornaListaCid().Count() > 0)
            {
                var consulta = from c in RetornaListaCid() select c.Codigo;
                evolucaoMedica.CodigosCids = consulta.ToList<string>();
            }

            if (ocupa_vaga != 0)
            {
                bool flag_vaga = ocupa_vaga == 1 ? true : false;
                iProntuario.SalvarProntuario<Prontuario, AcolhimentoUrgence, EvolucaoMedica, PrescricaoProcedimento, PrescricaoProcedimentoNaoFaturavel, PrescricaoMedicamento, ProntuarioExame, ProntuarioExameEletivo>(usuarioProfissional.Id_Usuario, prontuario, null, evolucaoMedica, RetornaListaProcedimento(), RetornaListaProcedimentoNaoFaturavel(), RetornaListaMedicamento(), RetornaListaExames(), RetornaListaExamesEletivos(), agendarprescricao, flag_vaga, tipo_vaga);
                iProntuario.Inserir(new LogUrgencia(DateTime.Now, usuarioProfissional.Id_Usuario, 37, "id evolucao:" + evolucaoMedica.Codigo));
            }
            else
            {
                iProntuario.SalvarProntuario<Prontuario, AcolhimentoUrgence, EvolucaoMedica, PrescricaoProcedimento, PrescricaoProcedimentoNaoFaturavel, PrescricaoMedicamento, ProntuarioExame, ProntuarioExameEletivo>(usuarioProfissional.Id_Usuario, prontuario, null, evolucaoMedica, RetornaListaProcedimento(), RetornaListaProcedimentoNaoFaturavel(), RetornaListaMedicamento(), RetornaListaExames(), RetornaListaExamesEletivos(), agendarprescricao);
                iProntuario.Inserir(new LogUrgencia(DateTime.Now, usuarioProfissional.Id_Usuario, 1, "id evolucao:" + evolucaoMedica.Codigo));
            }

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados salvos com sucesso!');location='Default.aspx';", true);
            HelperView.ExecutarPlanoContingencia(usuarioProfissional.Id_Usuario, prontuario.Codigo);

            //try
            //{
            //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuarioProfissional.Id_Usuario, prontuario.Codigo); });
            //}
            //catch { }
        }

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

        ///// <summary>
        ///// Função que executa um procedimento em background
        ///// </summary>
        ///// <param name="threadStart"></param>
        //public static void StartBackgroundThread(ThreadStart threadStart)
        //{
        //    if (threadStart != null)
        //    {
        //        Thread thread = new Thread(threadStart);
        //        thread.IsBackground = true;
        //        thread.Start();
        //    }
        //}

        /// <summary>
        /// Valida a concretização da consulta médica nos casos de situação em:
        /// 1 - Observação na Unidade
        /// 2 - Aguardando Leito em Unidade
        /// 3 - Aguardando Leito em UTI
        /// 4 - Aguardando Regulação
        /// E caso seja passada uma prescrição para o paciente, tem que ter obrigatoriamente o horário de funcionamento da unidade
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_ConsultaMedica(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;
            if (DropDownList_SituacaoEvolucaoMedica.SelectedValue != SituacaoAtendimento.OBITO.ToString() && DropDownList_SituacaoEvolucaoMedica.SelectedValue != SituacaoAtendimento.EVASAO.ToString())
            //if (DropDownList_SituacaoEvolucaoMedica.SelectedValue == SituacaoAtendimento.EM_OBSERVACAO_UNIDADE.ToString() || DropDownList_SituacaoEvolucaoMedica.SelectedValue == SituacaoAtendimento.AGUARDANDO_REGULACAO_UTI.ToString() || DropDownList_SituacaoEvolucaoMedica.SelectedValue == SituacaoAtendimento.ALTA_MEDICA.ToString())
            {
                if (RetornaListaCid().Count() <= 0)
                {
                    CustomValidator_ConsultaMedica.ErrorMessage = "Informe pelo menos uma suspeita diagnóstica.";
                    e.IsValid = false;
                    return;
                }
            }
        }

        /// <summary>
        /// Verifica a existência de uma prescrição válida para este prontuário
        /// </summary>
        /// <param name="co_prontuario"></param>
        /// <returns></returns>
        private bool VerificaExistenciaPrescricaoValida(long co_prontuario, IList<PrescricaoMedicamento> medicamentos, IList<PrescricaoProcedimento> procedimentos, IList<PrescricaoProcedimentoNaoFaturavel> procedimentosnaofaturaveis)
        {
            if ((medicamentos != null && medicamentos.Count() > 0) || (procedimentos != null && procedimentos.Count() > 0) || (procedimentosnaofaturaveis != null && procedimentosnaofaturaveis.Count() > 0))
                return Factory.GetInstance<IPrescricao>().BuscarPorProntuario<Prescricao>(co_prontuario, Convert.ToChar(Prescricao.StatusPrescricao.Valida)).FirstOrDefault() != null ? true : false;

            return false;
        }

        /// <summary>
        /// De acordo com a situação do paciente é solicitado ou não o preenchimento do 'Sumário de Alta'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_SituacaoEvolucaoMedica(object sender, EventArgs e)
        {
            if (DropDownList_SituacaoEvolucaoMedica.SelectedValue == SituacaoAtendimento.ALTA_MEDICA.ToString() || DropDownList_SituacaoEvolucaoMedica.SelectedValue == SituacaoAtendimento.ALTA_PEDIDO.ToString())
            {
                Panel_SumarioAlta.Visible = true;
                RequiredFieldValidator_SumarioAlta.Enabled = true;
                this.Panel_UnidadeTransferencia.Visible = false;
                this.CompareValidator_UnidadeTransferencia.Enabled = false;
            }
            else
            {
                Panel_SumarioAlta.Visible = false;
                RequiredFieldValidator_SumarioAlta.Enabled = false;

                if (DropDownList_SituacaoEvolucaoMedica.SelectedValue == SituacaoAtendimento.TRANSFERENCIA.ToString())
                {
                    this.Panel_UnidadeTransferencia.Visible = true;
                    this.CompareValidator_UnidadeTransferencia.Enabled = true;
                }
                else
                {
                    this.Panel_UnidadeTransferencia.Visible = false;
                    this.CompareValidator_UnidadeTransferencia.Enabled = false;
                }
            }
        }
        #endregion

        #region DADOS DO FORMULÁRIO
        /// <summary>
        /// Carrega todos os dados necessários para os formulários de suspeita diagnóstica/prescrição médica, solitação de exames,
        /// aprazamento e alteração de itens de uma prescrição
        /// </summary>
        private void CarregaDadosIniciais()
        {
            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();

            IList<ClassificacaoRisco> classificacoes = iUrgencia.ListarTodos<ClassificacaoRisco>("Ordem", false);
            DropDownList_ClassificacaoRisco.DataSource = classificacoes;
            DropDownList_ClassificacaoRisco.DataBind();

            IList<ViverMais.Model.Exame> exames = iUrgencia.ListarTodos<ViverMais.Model.Exame>().Where(p => p.Status == Convert.ToChar(Exame.EnumDescricaoStatus.Ativo)).OrderBy(s => s.Descricao).ToList();
            DropDownList_ExamesEvolucaoMedica.DataValueField = "Codigo";
            DropDownList_ExamesEvolucaoMedica.DataTextField = "Descricao";

            DropDownList_ExamesEvolucaoMedica.DataSource = exames;
            DropDownList_ExamesEvolucaoMedica.DataBind();
            DropDownList_ExamesEvolucaoMedica.Items.Insert(0, new ListItem("Selecione...", "0"));

            IList<ViverMais.Model.ExameEletivo> exameseletivos = iUrgencia.ListarTodos<ViverMais.Model.ExameEletivo>().Where(p => p.Status == Convert.ToChar(ExameEletivo.DescricaoStatus.Ativo)).OrderBy(s => s.Descricao).ToList();
            DropDownList_ExameEletivo.DataValueField = "Codigo";
            DropDownList_ExameEletivo.DataTextField = "Descricao";

            DropDownList_ExameEletivo.DataSource = exameseletivos;
            DropDownList_ExameEletivo.DataBind();
            DropDownList_ExameEletivo.Items.Insert(0, new ListItem("Selecione...", "0"));

            Session.Remove("ListaCid");
            Session.Remove("ListaMedicamento");
            Session.Remove("ListaProcedimento");
            Session.Remove("ListaProcedimentoNaoFaturavel");
            Session.Remove("ListaExame");
            Session.Remove("ListaExameEletivo");

            CarregaGridCid(RetornaListaCid());
            CarregaGridPrescricaoMedicamento(RetornaListaMedicamento());
            CarregaGridProcedimento(RetornaListaProcedimento());
            CarregaGridProcedimentoNaoFaturavel(RetornaListaProcedimentoNaoFaturavel());
            CarregaGridExames(RetornaListaExames());
            CarregaGridExamesEletivos(RetornaListaExamesEletivos());

            IList<ViverMais.Model.SituacaoAtendimento> situacoes = iUrgencia.ListarTodos<ViverMais.Model.SituacaoAtendimento>().Where(p => 
                p.Codigo != SituacaoAtendimento.ATENDIMENTO_INICIAL &&
                p.Codigo != SituacaoAtendimento.AGUARDANDO_ATENDIMENTO &&
                p.Codigo != SituacaoAtendimento.FINALIZADO
                //p.Codigo == SituacaoAtendimento.EM_OBSERVACAO_UNIDADE ||
                //p.Codigo == SituacaoAtendimento.AGUARDANDO_REGULACAO_UTI ||
                //p.Codigo == SituacaoAtendimento.AGUARDANDO_REGULACAO_ENFERMARIA ||
                //p.Codigo == SituacaoAtendimento.EVASAO ||
                //p.Codigo == SituacaoAtendimento.ALTA_MEDICA ||
                //p.Codigo == SituacaoAtendimento.ALTA_PEDIDO ||
                //p.Codigo == SituacaoAtendimento.OBITO
                ).OrderBy(s => s.Nome).ToList();

            DropDownList_SituacaoEvolucaoMedica.DataTextField = "Nome";
            DropDownList_SituacaoEvolucaoMedica.DataValueField = "Codigo";

            DropDownList_SituacaoEvolucaoMedica.DataSource = situacoes;
            DropDownList_SituacaoEvolucaoMedica.DataBind();
            DropDownList_SituacaoEvolucaoMedica.Items.Insert(0, new ListItem("Selecione...", "0"));

            IList<ViaAdministracao> viasadministracao = iUrgencia.ListarTodos<ViaAdministracao>().OrderBy(p => p.Nome).ToList();

            DropDownList_ViaAdministracaoMedicamentoEvolucaoMedica.DataTextField = "Nome";
            DropDownList_ViaAdministracaoMedicamentoEvolucaoMedica.DataValueField = "Codigo";

            DropDownList_ViaAdministracaoMedicamentoEvolucaoMedica.DataSource = viasadministracao;
            DropDownList_ViaAdministracaoMedicamentoEvolucaoMedica.DataBind();
            DropDownList_ViaAdministracaoMedicamentoEvolucaoMedica.Items.Insert(0, new ListItem("Selecione...", "-1"));

            IList<ViverMais.Model.EstabelecimentoSaudeEmergencial> unidadestransferencias = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.EstabelecimentoSaudeEmergencial>().Where(p => p.CNES != ((Usuario)Session["Usuario"]).Unidade.CNES).OrderBy(p => p.Nome).ToList();
            this.DropDownList_UnidadeTransferencia.DataSource = unidadestransferencias;
            this.DropDownList_UnidadeTransferencia.DataBind();
            this.DropDownList_UnidadeTransferencia.Items.Insert(0, new ListItem("Selecione...","-1"));

            this.AplicarLegendaDropDownList(DropDownList_UnidadeTransferencia);
            this.AplicarLegendaDropDownList(DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica);
            this.AplicarLegendaDropDownList(DropDownList_MedicamentoEvolucaoMedica);
            this.AplicarLegendaDropDownList(DropDownList_ExameEletivo);
            this.AplicarLegendaDropDownList(DropDownList_ExamesEvolucaoMedica);
            this.AplicarLegendaDropDownList(DropDownList_ViaAdministracaoMedicamentoEvolucaoMedica);
            this.AplicarLegendaDropDownList(DropDownList_ProcedimentoEvolucaoMedica);
            this.AplicarLegendaDropDownList(DropDownList_CidEvolucaoMedica);

            foreach (string unidadeintervalo in Enum.GetNames(typeof(PrescricaoMedicamento.UNIDADETEMPO)).ToList())
                DropDownList_UnidadeTempoFrequenciaMedicamento.Items.Add(new ListItem(unidadeintervalo, ((int)Enum.Parse(typeof(PrescricaoMedicamento.UNIDADETEMPO), unidadeintervalo)).ToString()));

            foreach (string unidadeintervalo in Enum.GetNames(typeof(PrescricaoProcedimento.UNIDADETEMPO)).ToList())
                DropDownList_UnidadeTempoFrequenciaProcedimento.Items.Add(new ListItem(unidadeintervalo, ((int)Enum.Parse(typeof(PrescricaoProcedimento.UNIDADETEMPO), unidadeintervalo)).ToString()));

            foreach (string unidadeintervalo in Enum.GetNames(typeof(PrescricaoProcedimentoNaoFaturavel.UNIDADETEMPO)).ToList())
                DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavel.Items.Add(new ListItem(unidadeintervalo, ((int)Enum.Parse(typeof(PrescricaoProcedimentoNaoFaturavel.UNIDADETEMPO), unidadeintervalo)).ToString()));
        }

        private void AplicarLegendaDropDownList(DropDownList dropdown)
        {
            dropdown.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        #endregion

        #region SUSPEITA DIAGNÓSTICA/PRESCRIÇÃO MÉDICA
        /// <summary>
        /// Retorna a lista temporária de procedimentos
        /// </summary>
        /// <returns></returns>
        public IList<PrescricaoProcedimento> RetornaListaProcedimento()
        {
            return Session["ListaProcedimento"] != null ? (IList<PrescricaoProcedimento>)Session["ListaProcedimento"] : new List<PrescricaoProcedimento>();
        }

        /// <summary>
        /// Retorna a lista temporária de medicamentos
        /// </summary>
        /// <returns></returns>
        public IList<PrescricaoMedicamento> RetornaListaMedicamento()
        {
            return Session["ListaMedicamento"] != null ? (IList<PrescricaoMedicamento>)Session["ListaMedicamento"] : new List<PrescricaoMedicamento>();
        }

        /// <summary>
        /// Retorna a lista temporária de procedimentos não-faturáveis
        /// </summary>
        /// <returns></returns>
        public IList<PrescricaoProcedimentoNaoFaturavel> RetornaListaProcedimentoNaoFaturavel()
        {
            return Session["ListaProcedimentoNaoFaturavel"] != null ? (IList<PrescricaoProcedimentoNaoFaturavel>)Session["ListaProcedimentoNaoFaturavel"] : new List<PrescricaoProcedimentoNaoFaturavel>();
        }

        /// <summary>
        /// Retorna a lista temporária de Cids
        /// </summary>
        /// <returns></returns>
        public IList<Cid> RetornaListaCid()
        {
            return Session["ListaCid"] != null ? (IList<Cid>)Session["ListaCid"] : new List<Cid>();
        }

        /// <summary>
        /// Carrega o grid de procedimento
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridProcedimento(IList<PrescricaoProcedimento> iList)
        {
            GridView_ProcedimentoEvolucaoMedica.DataSource = iList;
            GridView_ProcedimentoEvolucaoMedica.DataBind();
        }

        /// <summary>
        /// Carrega o grid de medicamento para a prescrição corrente
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridPrescricaoMedicamento(IList<PrescricaoMedicamento> iList)
        {
            GridView_MedicamentoEvolucaoMedica.DataSource = iList;
            GridView_MedicamentoEvolucaoMedica.DataBind();
        }

        /// <summary>
        /// Carrega o grid de suspeita diagnóstica
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridCid(IList<Cid> iList)
        {
            GridView_CidEvolucaoMedica.DataSource = iList;
            GridView_CidEvolucaoMedica.DataBind();
        }

        /// <summary>
        /// Carrega a lista de procedimentos não-faturáveis para a prescrição válida
        /// </summary>
        /// <param name="p"></param>
        private void CarregaGridProcedimentoNaoFaturavel(IList<PrescricaoProcedimentoNaoFaturavel> lista)
        {
            GridView_ProcedimentosNaoFaturavelEvolucaoMedica.DataSource = lista;
            GridView_ProcedimentosNaoFaturavelEvolucaoMedica.DataBind();
        }

        /// <summary>
        /// Adiciona um CID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarCidEvolucaoMedica(object sender, EventArgs e)
        {
            IList<Cid> lista = RetornaListaCid();

            if (!lista.Select(p => p.Codigo).Contains(DropDownList_CidEvolucaoMedica.SelectedValue))
            {
                lista.Add(Factory.GetInstance<ICid>().BuscarPorCodigo<Cid>(DropDownList_CidEvolucaoMedica.SelectedValue));
                Session["ListaCid"] = lista;
                CarregaGridCid(lista);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este CID já se encontra na lista de solicitações.');", true);
        }

        /// <summary>
        /// Deleta uma linha específica do gridview de CID's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_CidEvolucaoMedica(object sender, GridViewDeleteEventArgs e)
        {
            IList<Cid> lista = RetornaListaCid();
            lista.RemoveAt(e.RowIndex);
            Session["ListaCid"] = lista;
            CarregaGridCid(lista);
        }

        /// <summary>
        /// Busca os CID's de acordo com seu código
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_BuscarCidsEvolucaoMedica(object sender, EventArgs e)
        {
            IList<ViverMais.Model.Cid> cids = Factory.GetInstance<ICid>().BuscarPorGrupo<ViverMais.Model.Cid>(this.WUC_SuspeitaDiagnostica.WUC_DropDownListGrupoCID.SelectedValue);
            DropDownList_CidEvolucaoMedica.DataSource = cids;
            DropDownList_CidEvolucaoMedica.DataBind();

            DropDownList_CidEvolucaoMedica.Items.Insert(0, new ListItem("Selecione...", "0"));
            DropDownList_CidEvolucaoMedica.Focus();
            this.UpdatePanel_SuspeitaDiagnostica.Update();
        }

        /// <summary>
        /// Busca os CID's de acordo com o código digitado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_BuscarCIDEvolucaoMedica(object sender, EventArgs e)
        {
            IList<Cid> cids = new List<Cid>();
            Cid cid = Factory.GetInstance<ICid>().BuscarPorCodigo<Cid>(this.WUC_SuspeitaDiagnostica.WUC_TextBoxCodigoCID.Text.ToUpper());

            if (cid != null)
                cids.Add(cid);

            DropDownList_CidEvolucaoMedica.DataSource = cids;
            DropDownList_CidEvolucaoMedica.DataBind();

            DropDownList_CidEvolucaoMedica.Items.Insert(0, new ListItem("Selecione...", "0"));
            DropDownList_CidEvolucaoMedica.Focus();

            this.UpdatePanel_SuspeitaDiagnostica.Update();
        }

        /// <summary>
        /// Carrega os procedimentos para o 'Cid' escolhido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaProcedimentosEvolucaoMedica(object sender, EventArgs e)
        {
            DropDownList_ProcedimentoEvolucaoMedica.Items.Clear();
            DropDownList_ProcedimentoEvolucaoMedica.Items.Add(new ListItem("Selecione...", "-1"));

            if (DropDownList_CidEvolucaoMedica.SelectedValue != "-1")
            {
                IList<ProcedimentoCid> procedimentoscid = Factory.GetInstance<IProcedimento>().BuscarPorCid<ProcedimentoCid>(DropDownList_CidEvolucaoMedica.SelectedValue).OrderBy(p => p.Procedimento.Nome).ToList();
                foreach (ProcedimentoCid procedimentocid in procedimentoscid)
                {
                    ListItem item = new ListItem(procedimentocid.Procedimento.Nome, procedimentocid.Procedimento.Codigo.ToString());
                    DropDownList_ProcedimentoEvolucaoMedica.Items.Add(item);
                }
                DropDownList_ProcedimentoEvolucaoMedica.Focus();
            }
        }

        /// <summary>
        /// Adiciona o medicamento na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarMedicamentoEvolucaoMedica(object sender, EventArgs e)
        {
            IList<PrescricaoMedicamento> lista = RetornaListaMedicamento();

            if (lista.Where(p => p.Medicamento.ToString() == DropDownList_MedicamentoEvolucaoMedica.SelectedValue).FirstOrDefault() == null)
            {
                PrescricaoMedicamento prescricaomedicamento = new PrescricaoMedicamento();
                prescricaomedicamento.ObjetoMedicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(int.Parse(DropDownList_MedicamentoEvolucaoMedica.SelectedValue));
                prescricaomedicamento.Medicamento = prescricaomedicamento.ObjetoMedicamento.Codigo;
                prescricaomedicamento.SetIntervalo(this.TextBox_IntervaloMedicamentoEvolucaoMedica.Text, int.Parse(this.DropDownList_UnidadeTempoFrequenciaMedicamento.SelectedValue));
                prescricaomedicamento.Observacao = TextBox_ObservacaoMedicamentoEvolucaoMedica.Text;

                if (DropDownList_ViaAdministracaoMedicamentoEvolucaoMedica.SelectedValue != "-1")
                    prescricaomedicamento.ViaAdministracao = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViaAdministracao>(int.Parse(DropDownList_ViaAdministracaoMedicamentoEvolucaoMedica.SelectedValue));

                if (prescricaomedicamento.IntervaloValido())
                {
                    lista.Add(prescricaomedicamento);
                    Session["ListaMedicamento"] = lista;
                    CarregaGridPrescricaoMedicamento(lista);
                    OnClick_CancelarMedicamento(sender, e);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O intervalo máximo para aplicação do(a) medicamento/prescrição é de 24 horas.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este(a) Medicamento/Prescrição já se encontra na lista de solicitações.');", true);
        }

        /// <summary>
        /// Adiciona o procedimento na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarProcedimentoEvolucaoMedica(object sender, EventArgs e)
        {
            IList<PrescricaoProcedimento> procedimentos = RetornaListaProcedimento();

            if (procedimentos.Where(p => p.Procedimento.Codigo == DropDownList_ProcedimentoEvolucaoMedica.SelectedValue).FirstOrDefault() == null)
            {
                IRegistro iRegistro = Factory.GetInstance<IRegistro>();
                ICid iCid = Factory.GetInstance<ICid>();
                bool exigenciaCid = iRegistro.ProcedimentoExigeCid(DropDownList_ProcedimentoEvolucaoMedica.SelectedValue);

                if (exigenciaCid && DropDownList_ProcedimentoCID.SelectedValue == "-1")
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este procedimento exige a escolha de um CID.')", true);
                    return;
                }

                PrescricaoProcedimento prescricaoprocedimento = new PrescricaoProcedimento();
                prescricaoprocedimento.CodigoProcedimento = DropDownList_ProcedimentoEvolucaoMedica.SelectedValue;
                prescricaoprocedimento.Procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(DropDownList_ProcedimentoEvolucaoMedica.SelectedValue);
                prescricaoprocedimento.SetIntervalo(this.TextBox_IntervaloProcedimentoEvolucaoMedica.Text, int.Parse(this.DropDownList_UnidadeTempoFrequenciaProcedimento.SelectedValue));
                prescricaoprocedimento.CodigoCid = this.DropDownList_ProcedimentoCID.SelectedValue;
                prescricaoprocedimento.Cid = iCid.BuscarPorCodigo<Cid>(this.DropDownList_ProcedimentoCID.SelectedValue);

                if (prescricaoprocedimento.IntervaloValido())
                {
                    procedimentos.Add(prescricaoprocedimento);
                    Session["ListaProcedimento"] = procedimentos;
                    CarregaGridProcedimento(procedimentos);
                    OnClick_CancelarProcedimento(sender, e);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O intervalo máximo para aplicação do procedimento é de 24 horas.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este Procedimento já se encontra na lista de solicitações.');", true);
        }

        /// <summary>
        /// Adiciona um procedimento não-faturável na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarProcedimentoNaoFaturavelEvolucaoMedica(object sender, EventArgs e)
        {
            IList<PrescricaoProcedimentoNaoFaturavel> lista = RetornaListaProcedimentoNaoFaturavel();

            if (lista.Where(p => p.Procedimento.Codigo == int.Parse(DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica.SelectedValue)).FirstOrDefault() == null)
            {
                PrescricaoProcedimentoNaoFaturavel prescricaoprocedimento = new PrescricaoProcedimentoNaoFaturavel();
                prescricaoprocedimento.Procedimento = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ProcedimentoNaoFaturavel>(int.Parse(DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica.SelectedValue));
                prescricaoprocedimento.SetIntervalo(this.TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica.Text, int.Parse(this.DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavel.SelectedValue));

                if (prescricaoprocedimento.IntervaloValido())
                {
                    lista.Add(prescricaoprocedimento);
                    Session["ListaProcedimentoNaoFaturavel"] = lista;
                    CarregaGridProcedimentoNaoFaturavel(lista);

                    this.DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica.SelectedValue = "-1";
                    this.TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica.Text = "";
                    this.TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica.Enabled = true;

                    this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica, this.RequiredFieldValidator_FrequenciaProcedimentoNaoFaturavel,
                    this.RegularExpressionValidator_FrequenciaProcedimentoNaoFaturavel, this.CompareValidator_FrequenciaProcedimentoNaoFaturavel,
                    true);

                    this.DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavel.SelectedValue = ((int)PrescricaoProcedimentoNaoFaturavel.UNIDADETEMPO.HORAS).ToString();
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O intervalo máximo para aplicação do procedimento é de 24 horas.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este Procedimento já se encontra na lista de solicitações.');", true);
        }

        /// <summary>
        /// Deleta o medicamento na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_MedicamentoEvolucaoMedica(object sender, GridViewDeleteEventArgs e)
        {
            IList<PrescricaoMedicamento> lista = RetornaListaMedicamento();
            lista.RemoveAt(e.RowIndex);
            Session["ListaMedicamento"] = lista;
            CarregaGridPrescricaoMedicamento(lista);
        }

        /// <summary>
        /// Deleta o procedimento adicionado na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_ProcedimentoEvolucaoMedica(object sender, GridViewDeleteEventArgs e)
        {
            IList<PrescricaoProcedimento> lpp = RetornaListaProcedimento();
            lpp.RemoveAt(e.RowIndex);
            Session["ListaProcedimento"] = lpp;

            CarregaGridProcedimento(lpp);
        }

        /// <summary>
        /// Deleta o procedimento não-faturável adicionado na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_ProcedimentoNaoFaturavelEvolucaoMedica(object sender, GridViewDeleteEventArgs e)
        {
            IList<PrescricaoProcedimentoNaoFaturavel> lista = RetornaListaProcedimentoNaoFaturavel();
            lista.RemoveAt(e.RowIndex);
            Session["ListaProcedimentoNaoFaturavel"] = lista;

            CarregaGridProcedimentoNaoFaturavel(lista);
        }

        /// <summary>
        /// Cancela a inserção do procedimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarProcedimento(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoEvolucaoMedica, this.RequiredFieldValidator_FrequenciaProcedimento,
                    this.RegularExpressionValidator_FrequenciaProcedimento, this.CompareValidator_FrequenciaProcedimento,
                    true);

            DropDownList_ProcedimentoEvolucaoMedica.SelectedValue = "-1";
            TextBox_IntervaloProcedimentoEvolucaoMedica.Text = "";
            TextBox_IntervaloProcedimentoEvolucaoMedica.Enabled = true;
            DropDownList_UnidadeTempoFrequenciaProcedimento.SelectedValue = ((int)PrescricaoProcedimento.UNIDADETEMPO.HORAS).ToString();

            this.WUC_ProcedimentoCid.WUC_DropDownListGrupoCID.SelectedValue = "-1";
            this.WUC_ProcedimentoCid.WUC_UpdatePanelPesquisarCID.Update();

            this.OnSelectedIndexChanged_RetiraCids(new object(), new EventArgs());
        }

        protected void OnSelectedIndexChanged_RetiraCids(object sender, EventArgs e)
        {
            this.DropDownList_ProcedimentoCID.Items.Clear();
            this.DropDownList_ProcedimentoCID.Items.Add(new ListItem("Selecione...", "-1"));
        }

        /// <summary>
        /// Cancela a inserção do medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarMedicamento(object sender, EventArgs e)
        {
            DropDownList_MedicamentoEvolucaoMedica.Items.Clear();
            DropDownList_MedicamentoEvolucaoMedica.Items.Add(new ListItem("Selecione...", "0"));

            this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloMedicamentoEvolucaoMedica, this.RequiredFieldValidator_FrequenciaMedicamento,
                    this.RegularExpressionValidator_FrequenciaMedicamento, this.CompareValidator_FrequenciaMedicamento,
                    true);

            TextBox_ObservacaoMedicamentoEvolucaoMedica.Text = "";
            TextBox_IntervaloMedicamentoEvolucaoMedica.Text = "";
            TextBox_IntervaloMedicamentoEvolucaoMedica.Enabled = true;
            DropDownList_ViaAdministracaoMedicamentoEvolucaoMedica.SelectedValue = "-1";
            DropDownList_UnidadeTempoFrequenciaMedicamento.SelectedValue = ((int)PrescricaoMedicamento.UNIDADETEMPO.HORAS).ToString();
        }

        /// <summary>
        /// Copia os itens da última prescrição para a nova que será registrada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CopiarItensUltimaPrescricao(object sender, EventArgs e)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            Prescricao p = iPrescricao.BuscarPorProntuario<Prescricao>(long.Parse(ViewState["co_prontuario"].ToString())).OrderByDescending(pt => pt.Data).FirstOrDefault();

            if (p != null)
            {
                IList<PrescricaoMedicamento> medicamentos = iPrescricao.BuscarMedicamentos<PrescricaoMedicamento>(p.Codigo);
                IList<PrescricaoMedicamento> novosmedicamentos = new List<PrescricaoMedicamento>();

                foreach (PrescricaoMedicamento medicamento in medicamentos)
                {
                    PrescricaoMedicamento prescricao = new PrescricaoMedicamento();
                    prescricao.Intervalo = medicamento.Intervalo;
                    prescricao.AplicacaoUnica = medicamento.AplicacaoUnica;
                    prescricao.Medicamento = medicamento.Medicamento;
                    prescricao.ObjetoMedicamento = medicamento.ObjetoMedicamento;
                    prescricao.ViaAdministracao = medicamento.ViaAdministracao;
                    prescricao.Observacao = medicamento.Observacao;
                    novosmedicamentos.Add(prescricao);
                }

                Session["ListaMedicamento"] = novosmedicamentos;
                CarregaGridPrescricaoMedicamento(novosmedicamentos);

                IList<PrescricaoProcedimento> procedimentos = iPrescricao.BuscarProcedimentos<PrescricaoProcedimento>(p.Codigo);
                IList<PrescricaoProcedimento> novosprocedimentos = new List<PrescricaoProcedimento>();

                foreach (PrescricaoProcedimento procedimento in procedimentos)
                {
                    PrescricaoProcedimento prescricao = new PrescricaoProcedimento();
                    prescricao.CodigoProcedimento = procedimento.CodigoProcedimento;
                    prescricao.Intervalo = procedimento.Intervalo;
                    prescricao.AplicacaoUnica = procedimento.AplicacaoUnica;
                    prescricao.Procedimento = procedimento.Procedimento;
                    prescricao.Cid = procedimento.Cid;
                    prescricao.CodigoCid = procedimento.CodigoCid;
                    novosprocedimentos.Add(prescricao);
                }

                Session["ListaProcedimento"] = novosprocedimentos;
                CarregaGridProcedimento(novosprocedimentos);
                OnClick_CancelarProcedimento(sender, e);

                IList<PrescricaoProcedimentoNaoFaturavel> procedimentosnaofaturaveis = iPrescricao.BuscarProcedimentosNaoFaturaveis<PrescricaoProcedimentoNaoFaturavel>(p.Codigo);
                IList<PrescricaoProcedimentoNaoFaturavel> novosnaofaturaveis = new List<PrescricaoProcedimentoNaoFaturavel>();

                foreach (PrescricaoProcedimentoNaoFaturavel procedimentonaofaturavel in procedimentosnaofaturaveis)
                {
                    PrescricaoProcedimentoNaoFaturavel prescricao = new PrescricaoProcedimentoNaoFaturavel();
                    prescricao.Procedimento = procedimentonaofaturavel.Procedimento;
                    prescricao.Intervalo = procedimentonaofaturavel.Intervalo;
                    prescricao.AplicacaoUnica = procedimentonaofaturavel.AplicacaoUnica;
                    novosnaofaturaveis.Add(prescricao);
                }

                Session["ListaProcedimentoNaoFaturavel"] = novosnaofaturaveis;
                CarregaGridProcedimentoNaoFaturavel(novosnaofaturaveis);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não existe prescrição alguma para copiar os seus itens.');", true);
        }
        #endregion

        #region SOLICITAÇÃO DE EXAMES
        /// <summary>
        /// Adiciona o exame na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarExameEvolucaoMedica(object sender, EventArgs e)
        {
            IList<ProntuarioExame> lista = RetornaListaExames();

            if (lista.Where(p => p.Exame.Codigo.ToString() == DropDownList_ExamesEvolucaoMedica.SelectedValue).FirstOrDefault() == null)
            {
                IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
                ProntuarioExame exame = new ProntuarioExame();
                exame.Exame = iUrgencia.BuscarPorCodigo<Exame>(int.Parse(DropDownList_ExamesEvolucaoMedica.SelectedValue));
                exame.Data = DateTime.Now;
                UsuarioProfissionalUrgence usuarioprofissional = iUrgencia.BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                exame.Profissional = usuarioprofissional.Id_Profissional;
                exame.CBOProfissional = usuarioprofissional.CodigoCBO;

                lista.Add(exame);

                Session["ListaExame"] = lista;
                CarregaGridExames(lista);

                DropDownList_ExamesEvolucaoMedica.SelectedValue = "0";
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este Exame Interno já se encontra na lista de solicitações.');", true);
        }

        /// <summary>
        /// Retorna a Lista de Exames Solicitados
        /// </summary>
        /// <returns></returns>
        public IList<ProntuarioExame> RetornaListaExames()
        {
            return Session["ListaExame"] != null ? (IList<ProntuarioExame>)Session["ListaExame"] : new List<ProntuarioExame>();
        }

        /// <summary>
        /// Carrega o grid de exames solicitados
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridExames(IList<ProntuarioExame> iList)
        {
            GridView_ExameEvolucaoMedica.DataSource = iList;
            GridView_ExameEvolucaoMedica.DataBind();
        }

        /// <summary>
        /// Deleta o exame adicionado na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_ExameEvolucaoMedica(object sender, GridViewDeleteEventArgs e)
        {
            IList<ProntuarioExame> lista = RetornaListaExames();
            lista.RemoveAt(e.RowIndex);
            Session["ListaExame"] = lista;
            CarregaGridExames(lista);
        }

        /// <summary>
        /// Adiciona um exame eletivo solicitado pelo profissional
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarExameEletivo(object sender, EventArgs e)
        {
            IList<ProntuarioExameEletivo> lista = RetornaListaExamesEletivos();

            if (lista.Where(p => p.Exame.Codigo.ToString() == DropDownList_ExameEletivo.SelectedValue).FirstOrDefault() == null)
            {
                IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
                ProntuarioExameEletivo exame = new ProntuarioExameEletivo();
                exame.Exame = iUrgencia.BuscarPorCodigo<ExameEletivo>(int.Parse(DropDownList_ExameEletivo.SelectedValue));
                exame.Data = DateTime.Now;
                UsuarioProfissionalUrgence ususarioprofissional = iUrgencia.BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                exame.Profissional = ususarioprofissional.Id_Profissional;
                exame.CBOProfissional = ususarioprofissional.CodigoCBO;
                lista.Add(exame);

                Session["ListaExameEletivo"] = lista;
                CarregaGridExamesEletivos(lista);

                DropDownList_ExameEletivo.SelectedValue = "0";
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este Exame Eletivo já se encontra na lista de solicitações.');", true);
        }

        /// <summary>
        /// Exclui o exame eletivo solicitado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_ExameEletivo(object sender, GridViewDeleteEventArgs e)
        {
            IList<ProntuarioExameEletivo> lista = RetornaListaExamesEletivos();
            lista.RemoveAt(e.RowIndex);
            Session["ListaExameEletivo"] = lista;
            CarregaGridExamesEletivos(lista);
        }

        /// <summary>
        /// Carrega o grid de exames eletivos
        /// </summary>
        /// <param name="lista"></param>
        private void CarregaGridExamesEletivos(IList<ProntuarioExameEletivo> lista)
        {
            GridView_ExamesEletivos.DataSource = lista;
            GridView_ExamesEletivos.DataBind();
        }

        /// <summary>
        /// Retorna a lista de exames eletivos solicitados
        /// </summary>
        /// <returns></returns>
        private IList<ProntuarioExameEletivo> RetornaListaExamesEletivos()
        {
            return Session["ListaExameEletivo"] != null ? (IList<ProntuarioExameEletivo>)Session["ListaExameEletivo"] : new List<ProntuarioExameEletivo>();
        }
        #endregion

        protected void OnClick_BuscarProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            IList<ProcedimentoNaoFaturavel> procedimentos = Factory.GetInstance<IProcedimentoNaoFaturavel>().BuscarPorNome<ProcedimentoNaoFaturavel>(TextBox_BuscarProcedimento.Text);
            DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica.DataValueField = "Codigo";
            DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica.DataTextField = "Nome";

            DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica.DataSource = procedimentos;
            DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica.DataBind();

            DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica.Items.Insert(0, new ListItem("Selecione...", "-1"));
            DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica.Focus();
        }

        protected void OnClick_BuscarMedicamento(object sender, EventArgs e)
        {
            DropDownList_MedicamentoEvolucaoMedica.DataTextField = "Nome";
            DropDownList_MedicamentoEvolucaoMedica.DataValueField = "Codigo";
            IList<Medicamento> medicamentos = Factory.GetInstance<IMedicamento>().BuscarPorDescricao<Medicamento>(-1, TextBox_BuscarMedicamento.Text, false);

            DropDownList_MedicamentoEvolucaoMedica.DataSource = medicamentos;
            DropDownList_MedicamentoEvolucaoMedica.DataBind();

            DropDownList_MedicamentoEvolucaoMedica.Items.Insert(0, new ListItem("Selecione...", "0"));
            DropDownList_MedicamentoEvolucaoMedica.Focus();
        }

        protected void OnClick_BuscarProcedimentoSIGTAP(object sender, EventArgs e)
        {
            IList<Procedimento> procedimentos = Factory.GetInstance<IProcedimento>().BuscarPorNome<Procedimento>(TextBox_BuscarProcedimentoSIGTAP.Text.ToUpper());

            DropDownList_ProcedimentoEvolucaoMedica.DataTextField = "Nome";
            DropDownList_ProcedimentoEvolucaoMedica.DataValueField = "Codigo";

            DropDownList_ProcedimentoEvolucaoMedica.DataSource = procedimentos;
            DropDownList_ProcedimentoEvolucaoMedica.DataBind();

            DropDownList_ProcedimentoEvolucaoMedica.Items.Insert(0, new ListItem("Selecione...", "-1"));
            DropDownList_ProcedimentoEvolucaoMedica.Focus();
        }

        protected void OnClick_BuscarProcimentoCID(object sender, ImageClickEventArgs e)
        {
            if (this.DropDownList_ProcedimentoEvolucaoMedica.SelectedValue != "-1")
            {
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IList<Cid> cids = iPrescricao.BuscarCidsProcedimentoPorCodigo<Cid>(DropDownList_ProcedimentoEvolucaoMedica.SelectedValue, this.WUC_ProcedimentoCid.WUC_TextBoxCodigoCID.Text.ToUpper());

                DropDownList_ProcedimentoCID.DataSource = cids;
                DropDownList_ProcedimentoCID.DataBind();

                DropDownList_ProcedimentoCID.Items.Insert(0, new ListItem("Selecione...", "-1"));
                //this.UpdatePanel_ProcedimentoCID.Update();
            }
        }

        protected void OnSelectedIndexChanged_ProcecimentoCid(object sender, EventArgs e)
        {
            if (this.DropDownList_ProcedimentoEvolucaoMedica.SelectedValue != "-1")
            {
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IList<Cid> cids = iPrescricao.BuscarCidsProcedimentoPorGrupo<Cid>(DropDownList_ProcedimentoEvolucaoMedica.SelectedValue, this.WUC_ProcedimentoCid.WUC_DropDownListGrupoCID.SelectedValue);

                DropDownList_ProcedimentoCID.DataSource = cids;
                DropDownList_ProcedimentoCID.DataBind();
                this.InserirElementoDefault(this.DropDownList_ProcedimentoCID, -1);
                //this.UpdatePanel_ProcedimentoCID.Update();
            }
        }

        void InserirElementoDefault(DropDownList dropdown, int valor)
        {
            dropdown.Items.Insert(0, new ListItem("Selecione...", valor.ToString()));
        }

        protected void OnClickBuscarCIDPorNome(object sender, ImageClickEventArgs e)
        {
            DropDownList_CidEvolucaoMedica.DataSource = Factory.GetInstance<ICid>().BuscarPorInicioNome<Cid>(this.WUC_SuspeitaDiagnostica.WUC_TextBoxNomeCID.Text);
            DropDownList_CidEvolucaoMedica.DataBind();
            this.InserirElementoDefault(this.DropDownList_CidEvolucaoMedica, 0);
            DropDownList_CidEvolucaoMedica.Focus();
            this.UpdatePanel_SuspeitaDiagnostica.Update();
        }

        protected void OnClickBuscarProcedimentoCIDPorNome(object sender, ImageClickEventArgs e)
        {
            if (this.DropDownList_ProcedimentoEvolucaoMedica.SelectedValue != "-1")
            {
                this.DropDownList_ProcedimentoCID.DataSource = Factory.GetInstance<ICid>().BuscarPorInicioNome<Cid>(DropDownList_ProcedimentoEvolucaoMedica.SelectedValue, this.WUC_ProcedimentoCid.WUC_TextBoxNomeCID.Text);
                this.DropDownList_ProcedimentoCID.DataBind();
                this.InserirElementoDefault(this.DropDownList_ProcedimentoCID, -1);
                //this.UpdatePanel_ProcedimentoCID.Update();
            }
        }

        protected void OnSelectedIndexChanged_FrequenciaProcedimento(object sender, EventArgs e)
        {
            if (int.Parse(this.DropDownList_UnidadeTempoFrequenciaProcedimento.SelectedValue) == (int)PrescricaoProcedimento.UNIDADETEMPO.UNICA)
            {
                this.TextBox_IntervaloProcedimentoEvolucaoMedica.Text = "";
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoEvolucaoMedica, this.RequiredFieldValidator_FrequenciaProcedimento,
                    this.RegularExpressionValidator_FrequenciaProcedimento, this.CompareValidator_FrequenciaProcedimento,
                    false);
            }
            else
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoEvolucaoMedica, this.RequiredFieldValidator_FrequenciaProcedimento,
                    this.RegularExpressionValidator_FrequenciaProcedimento, this.CompareValidator_FrequenciaProcedimento,
                    true);
        }

        protected void OnSelectedIndexChanged_FrequenciaProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            if (int.Parse(this.DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavel.SelectedValue) == (int)PrescricaoProcedimentoNaoFaturavel.UNIDADETEMPO.UNICA)
            {
                this.TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica.Text = "";
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica, this.RequiredFieldValidator_FrequenciaProcedimentoNaoFaturavel,
                    this.RegularExpressionValidator_FrequenciaProcedimentoNaoFaturavel, this.CompareValidator_FrequenciaProcedimentoNaoFaturavel,
                    false);
            }
            else
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica, this.RequiredFieldValidator_FrequenciaProcedimentoNaoFaturavel,
                    this.RegularExpressionValidator_FrequenciaProcedimentoNaoFaturavel, this.CompareValidator_FrequenciaProcedimentoNaoFaturavel,
                    true);
        }

        protected void OnSelectedIndexChanged_FrequenciaMedicamento(object sender, EventArgs e)
        {
            if (int.Parse(this.DropDownList_UnidadeTempoFrequenciaMedicamento.SelectedValue) == (int)PrescricaoMedicamento.UNIDADETEMPO.UNICA)
            {
                this.TextBox_IntervaloMedicamentoEvolucaoMedica.Text = "";
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloMedicamentoEvolucaoMedica, this.RequiredFieldValidator_FrequenciaMedicamento,
                    this.RegularExpressionValidator_FrequenciaMedicamento, this.CompareValidator_FrequenciaMedicamento,
                    false);
            }
            else
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloMedicamentoEvolucaoMedica, this.RequiredFieldValidator_FrequenciaMedicamento,
                    this.RegularExpressionValidator_FrequenciaMedicamento, this.CompareValidator_FrequenciaMedicamento,
                    true);
        }

        private void HabilitaDesabilitaFrequencia(TextBox intervalo, RequiredFieldValidator validador1, RegularExpressionValidator validador2,
    CompareValidator validador3, bool habilitarCampos)
        {
            intervalo.Enabled = habilitarCampos;
            validador1.Enabled = habilitarCampos;
            validador2.Enabled = habilitarCampos;
            validador3.Enabled = habilitarCampos;
        }

        /// <summary>
        ///Mostra o histórico de todas suspeitas diagnósticas do paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_HistoricoSuspeitaDiagnostica(object sender, EventArgs e)
        {
            this.Label_TituloHistorico.Text = "Histórico de Suspeita Diagnóstica";
            this.DataList_HistoricoMedico.DataSource = Factory.GetInstance<IProntuario>().RetornaHistoricoSuspeitaDiagnostica(long.Parse(ViewState["co_prontuario"].ToString()));
            this.DataList_HistoricoMedico.DataBind();
            this.Panel_HistoricoMedico.Visible = true;
            this.UpdatePanel_HistoricoMedico.Update();
        }

        /// <summary>
        /// Mostra o histórico do paciente relacionado a sua anamnese da primeira consulta médica
        /// e suas posteriores evoluções médicas na unidade de atendimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_HistoricoMedico(object sender, EventArgs e)
        {
            this.Label_TituloHistorico.Text = "Histórico de Evoluções";
            this.DataList_HistoricoMedico.DataSource = Factory.GetInstance<IProntuario>().RetornaHistoricoMedico(long.Parse(ViewState["co_prontuario"].ToString()));
            this.DataList_HistoricoMedico.DataBind();
            this.Panel_HistoricoMedico.Visible = true;
            this.UpdatePanel_HistoricoMedico.Update();
        }

        protected void OnClick_FecharHistoricoMedico(object sender, EventArgs e)
        {
            this.Panel_HistoricoMedico.Visible = false;
            //this.LinkButton_HistoricoMedico.Focus();
            //this.UpdatePanel_HistoricoMedico.Update();
        }
    }
}
