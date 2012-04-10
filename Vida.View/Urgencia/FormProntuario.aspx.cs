using ViverMais.DAO;
using ViverMais.Model;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using System;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.View.Urgencia;
using ViverMais.View;
using System.IO;
using System.Threading;

namespace Urgence.View
{
    public partial class FormProntuario : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DropDownList_ClassificacaoRisco.Attributes.CssStyle.Add("float", "left");
            ImageButton suspeitacodigo = this.WUC_SuspeitaDiagnostica.WUC_ImageButtonBuscarCID;
            ImageButton suspeitanome = this.WUC_SuspeitaDiagnostica.WUC_ImageButtonBuscarCIDPorNome;
            DropDownList suspeitalista = this.WUC_SuspeitaDiagnostica.WUC_DropDownListGrupoCID;

            suspeitacodigo.Click += new ImageClickEventHandler(this.OnClick_BuscarCID);
            suspeitanome.Click += new ImageClickEventHandler(this.OnClickBuscarCIDPorNome);
            suspeitalista.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChanged_BuscarCids);

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
            this.InserirTrigger(this.Button_AdicionarProcedimento1.UniqueID, "Click", this.WUC_ProcedimentoCid.WUC_UpdatePanelPesquisarCID);

            //Master.AdicionarTriggerFimPagina(this.btnGrafAtendimento, "Click");

            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REALIZAR_CONSULTA_MEDICA", Modulo.URGENCIA))
                {
                    IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
                    ViverMais.Model.UsuarioProfissionalUrgence usuarioprofissional = iUsuarioProfissional.BuscarPorCodigoViverMais<ViverMais.Model.UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);

                    if (usuarioprofissional != null)
                    {
                        if (usuarioprofissional.UnidadeVinculo == ((Usuario)Session["Usuario"]).Unidade.CNES)
                        {
                            ICBO iCbo = Factory.GetInstance<ICBO>();
                            CBO cbo = iCbo.BuscarPorCodigo<CBO>(usuarioprofissional.CodigoCBO);

                            //if (VerificaFuncionalidadesMedico())
                            if (iCbo.CBOPertenceMedico<CBO>(cbo))
                            {
                                long co_prontuario;

                                if (Request["codigo"] != null && long.TryParse(Request["codigo"].ToString(), out co_prontuario))
                                {
                                    try
                                    {
                                        ((MasterUrgencia)Master).MenuVisivel = false;
                                        //Button_GerarHistoricoProntuario.OnClientClick = "javascript:window.open('FormImprimirProntuario.aspx?co_prontuario=" + Request["codigo"].ToString() + "');";
                                        IProntuario iProntuario = Factory.GetInstance<IProntuario>();

                                        CarregaDadosIniciais();
                                        ViverMais.Model.Prontuario prontuario = iProntuario.BuscarPorCodigo<ViverMais.Model.Prontuario>(co_prontuario);

                                        if (prontuario.Paciente.PacienteViverMais != null)
                                            ViewState["co_pacienteViverMais"] = prontuario.Paciente.PacienteViverMais.Codigo;

                                        if (prontuario.Situacao.Codigo == SituacaoAtendimento.AGUARDANDO_ATENDIMENTO)
                                        {
                                            lblNumero.Text = prontuario.NumeroToString;
                                            lblData.Text = prontuario.Data.ToString("dd/MM/yyyy");
                                            //ViverMais.Model.Paciente pacienteViverMais = null;

                                            if (prontuario.Paciente.PacienteViverMais != null)
                                            {
                                                //pacienteViverMais = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);
                                                lblPaciente.Visible = true;
                                                lblPaciente.Text = prontuario.Paciente.PacienteViverMais.Nome;

                                                if (prontuario.Paciente.PacienteViverMais.Sexo == 'F')
                                                {
                                                    RadioButton_SexoF.Checked = true;
                                                    RadioButton_SexoM.Checked = false;
                                                }

                                                RadioButton_SexoM.Enabled = false;
                                                RadioButton_SexoF.Enabled = false;
                                                tbxIdade.Text = prontuario.Paciente.PacienteViverMais.Idade.ToString();
                                                //iProntuario.CalculaIdade(DateTime.Today, pacienteViverMais.DataNascimento).ToString();
                                                tbxIdade.Enabled = false;
                                            }
                                            else
                                            {
                                                tbxNomePaciente.Visible = true;
                                                tbxNomePaciente.Text = string.IsNullOrEmpty(prontuario.Paciente.Nome) ? "" : prontuario.Paciente.Nome;
                                                tbxIdade.Text = prontuario.Idade.ToString();

                                                if (prontuario.Paciente.Sexo != '\0')
                                                {
                                                    if (prontuario.Paciente.Sexo == 'F')
                                                    {
                                                        RadioButton_SexoF.Checked = true; RadioButton_SexoM.Checked = false;
                                                    }
                                                }
                                            }

                                            AcolhimentoUrgence acolhimento = iProntuario.BuscarAcolhimento<AcolhimentoUrgence>(prontuario.Codigo);

                                            if (acolhimento == null)
                                            {
                                                tbxTensaoArterialInicio.Visible = true;
                                                tbxTensaoArterialFim.Visible = true;
                                                lbQuantificadorTensaoArterial.Visible = true;
                                                tbxTemperatura.Visible = true;
                                                tbxHgt.Visible = true;
                                                tbxFreqCardiaca.Visible = true;
                                                tbxFreqRespitatoria.Visible = true;

                                                ckbAcidente.Checked = false;
                                                ckbAlergia.Checked = false;
                                                ckbAsma.Checked = false;
                                                ckbConvulsao.Checked = false;
                                                ckbDiarreia.Checked = false;
                                                ckbDorIntensa.Checked = false;
                                                CheckBoxDorToraxica.Checked = false;
                                                CheckBoxSaturacaoOxigenio.Checked = false;
                                                ckbFratura.Checked = false;

                                                TextBox_Queixa.Text = "-";
                                            }
                                            else
                                            {
                                                lblTensaoArterial.Text = string.IsNullOrEmpty(acolhimento.TensaoArterialInicio) ? "-" : (acolhimento.TensaoArterialInicio + "X" + acolhimento.TensaoArterialFim);
                                                lblTemperatura.Text = string.IsNullOrEmpty(acolhimento.Temperatura) ? "-" : acolhimento.Temperatura;
                                                lblHgt.Text = string.IsNullOrEmpty(acolhimento.Hgt) ? "-" : acolhimento.Hgt;
                                                lblFreqCardiaca.Text = string.IsNullOrEmpty(acolhimento.FrequenciaCardiaca) ? "-" : acolhimento.FrequenciaCardiaca;
                                                lblFreqRespiratoria.Text = string.IsNullOrEmpty(acolhimento.FrequenciaRespiratoria) ? "-" : acolhimento.FrequenciaRespiratoria;

                                                ckbAcidente.Checked = acolhimento.Acidente;
                                                ckbAlergia.Checked = acolhimento.Alergia;
                                                ckbAsma.Checked = acolhimento.Asma;
                                                ckbConvulsao.Checked = acolhimento.Convulsao;
                                                ckbDiarreia.Checked = acolhimento.Diarreia;
                                                ckbDorIntensa.Checked = acolhimento.DorIntensa;
                                                CheckBoxDorToraxica.Checked = acolhimento.DorToraxica;
                                                CheckBoxSaturacaoOxigenio.Checked = acolhimento.SaturacaoOxigenio;
                                                ckbFratura.Checked = acolhimento.Fratura;

                                                TextBox_Queixa.Text = !string.IsNullOrEmpty(acolhimento.Queixa) ? acolhimento.Queixa : "-";
                                            }
                                            
                                            this.Label_HorarioClassificacaoRisco.Text = prontuario.Desacordado ? prontuario.Data.ToString("dd/MM/yyyy HH:mm") : prontuario.DataAcolhimento.Value.ToString("dd/MM/yyyy HH:mm");

                                            //Caso a situação seja atendimento inicial, seleciona por default 'Aguardando Atendimento'
                                            //ddlSituacao.SelectedValue = prontuario.Situacao.Codigo == SituacaoAtendimento.ATENDIMENTO_INICIAL ? SituacaoAtendimento.AGUARDANDO_ATENDIMENTO.ToString() : prontuario.Situacao.Codigo.ToString();
                                            lblSituacao.Text = prontuario.Situacao.Nome;
                                            //Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<SituacaoAtendimento>(prontuario.Situacao.Codigo).Nome;

                                            DropDownList_ClassificacaoRisco.SelectedValue = prontuario.ClassificacaoRisco.Codigo.ToString();
                                            this.OnSelectedIndexChanged_ClassificacaoRisco(new object(), new EventArgs());

                                            this.ddlSituacao.SelectedValue = SituacaoAtendimento.EM_OBSERVACAO_UNIDADE.ToString();
                                            this.OnSelectedIndexChanged_Situacao(new object(), new EventArgs());

                                            Session["tempo_atendimento"] = 0;
                                        }
                                    }
                                    catch (Exception f)
                                    {
                                        throw f;
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

        protected void OnSelectedIndexChanged_ClassificacaoRisco(object sender, EventArgs e)
        {
            ClassificacaoRisco classificacaorisco = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ClassificacaoRisco>(int.Parse(DropDownList_ClassificacaoRisco.SelectedValue));
            Image_ClassificacaoRisco.ImageUrl = "~/Urgencia/img/" + classificacaorisco.Imagem;
        }

        protected void OnClick_FecharQuadroAtendimento(object sender, EventArgs e)
        {
            Panel_QuadroAtendimento.Visible = false;
        }

        protected void OnClick_VerBulario(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('http://www.anvisa.gov.br/bularioeletronico/default.asp?txtPrincipioAtivo=" + ddlMedicamentos.SelectedItem.Text + "');", true);
        }

        #region DADOS DO FORMULÁRIO
        /// <summary>
        /// Carrega todos os dados necessários para os formulários de suspeita diagnóstica/prescrição médica e solitação de exames
        /// </summary>
        private void CarregaDadosIniciais()
        {
            Session.Remove("ListaCid"); Session["ListaCid"] = new List<Cid>();
            Session.Remove("ListaMedicamento");
            Session.Remove("ListaProcedimento");
            Session.Remove("ListaProcedimentoNaoFaturavel");
            Session.Remove("ListaExame");
            Session.Remove("ListaExameEletivo");

            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
            IList<ViverMais.Model.SituacaoAtendimento> situacoes = iUrgencia.ListarTodos<ViverMais.Model.SituacaoAtendimento>().Where(p => p.Codigo != SituacaoAtendimento.ATENDIMENTO_INICIAL && p.Codigo != SituacaoAtendimento.AGUARDANDO_ATENDIMENTO && p.Codigo != SituacaoAtendimento.FINALIZADO && p.Codigo != SituacaoAtendimento.TRANSFERENCIA).OrderBy(s => s.Nome).ToList();

            ddlSituacao.DataTextField = "Nome";
            ddlSituacao.DataValueField = "Codigo";
            ddlSituacao.DataSource = situacoes;
            ddlSituacao.DataBind();

            ddlSituacao.Items.Insert(0, new ListItem("Selecione...", "0"));

            IList<ClassificacaoRisco> classificacoes = iUrgencia.ListarTodos<ClassificacaoRisco>("Ordem", false);
            DropDownList_ClassificacaoRisco.DataSource = classificacoes;
            DropDownList_ClassificacaoRisco.DataBind();

            CarregaListaExames();
            CarregaGridCid((IList<Cid>)Session["ListaCid"]);
            CarregaGridPrescricaoMedicamento(RetornaListaMedicamento());
            CarregaGridProcedimento(RetornaListaProcedimento());
            CarregaGridProcedimentoNaoFaturavel(RetornaListaProcedimentoNaoFaturavel());
            CarregaGridExames(RetornaListaExames());
            CarregaGridExamesEletivos(RetornaListaExamesEletivos());

            CarregaViasAdministracao();

            this.AplicarLegendaDropDownList(DropDownList_ProcedimentosNaoFaturaveis);
            this.AplicarLegendaDropDownList(ddlMedicamentos);
            this.AplicarLegendaDropDownList(DropDownList_ExameEletivo);
            this.AplicarLegendaDropDownList(ddlExames);
            this.AplicarLegendaDropDownList(DropDownList_ViaAdministracaoMedicamento);
            this.AplicarLegendaDropDownList(DropDownList_Procedimento);
            this.AplicarLegendaDropDownList(DropDownList_ProcedimentoCID);
            this.AplicarLegendaDropDownList(this.WUC_ProcedimentoCid.WUC_DropDownListGrupoCID);
            this.AplicarLegendaDropDownList(this.WUC_SuspeitaDiagnostica.WUC_DropDownListGrupoCID);
            this.AplicarLegendaDropDownList(ddlCid);

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

        #region CONSULTA MÉDICA
        /// <summary>
        /// Salva o objeto prontuário médico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            char tipo_vaga = ' ';
            bool ocupa_vaga = false;
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
            //Usuario usuario = (Usuario)Session["Usuario"];
            UsuarioProfissionalUrgence usuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
            ViverMais.Model.Prontuario prontuario = iProntuario.BuscarPorCodigo<ViverMais.Model.Prontuario>(long.Parse(Request["codigo"].ToString()));

            try
            {
                if ((!string.IsNullOrEmpty(tbxTensaoArterialInicio.Text) && string.IsNullOrEmpty(tbxTensaoArterialFim.Text))
                || (string.IsNullOrEmpty(tbxTensaoArterialInicio.Text) && !string.IsNullOrEmpty(tbxTensaoArterialFim.Text)))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe a máxima e mínima da tensão arterial.');", true);
                    return;
                }

                if (!CustomValidator_ConsultaMedica.IsValid)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_ConsultaMedica.ErrorMessage + "');", true);
                    return;
                }

                if (int.Parse(ddlSituacao.SelectedValue) == SituacaoAtendimento.EM_OBSERVACAO_UNIDADE) //Ocupa Vaga
                {
                    ocupa_vaga = true;

                    if (int.Parse(tbxIdade.Text) <= 12)
                        tipo_vaga = VagaUrgencia.INFANTIL;
                    else
                        tipo_vaga = RadioButton_SexoM.Checked ? VagaUrgencia.MASCULINA : VagaUrgencia.FEMININA;

                    if (Factory.GetInstance<IVagaUrgencia>().VerificaDisponibilidadeVaga<ViverMais.Model.VagaUrgencia>(tipo_vaga, ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES) == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Número de vagas insuficientes para ocupação deste paciente!');", true);
                        return;
                    }
                }

                prontuario.Paciente.Nome = prontuario.Paciente != null && !string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais) ? lblPaciente.Text.ToUpper() : tbxNomePaciente.Text.ToUpper();
                prontuario.Paciente.Sexo = RadioButton_SexoM.Checked ? 'M' : 'F';
                prontuario.FaixaEtaria = Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc.IFaixaEtaria>().buscaPorIdade<ViverMais.Model.FaixaEtaria>(int.Parse(tbxIdade.Text));
                prontuario.Idade = int.Parse(tbxIdade.Text);

                //if (!string.IsNullOrEmpty(TextBox_Peso.Text))
                //    prontuario.Peso = float.Parse(TextBox_Peso.Text);

                prontuario.SumarioAlta = Panel_SumarioAlta.Visible ? TextBox_SumarioAlta.Text : "";
                prontuario.Situacao = iUrgencia.BuscarPorCodigo<ViverMais.Model.SituacaoAtendimento>(int.Parse(ddlSituacao.SelectedValue));
                prontuario.DataConsultaMedica = DateTime.Now;
                prontuario.ClassificacaoRisco = iUrgencia.BuscarPorCodigo<ClassificacaoRisco>(int.Parse(DropDownList_ClassificacaoRisco.SelectedValue));

                AcolhimentoUrgence acolhimento = iProntuario.BuscarAcolhimento<AcolhimentoUrgence>(prontuario.Codigo);

                if (acolhimento == null)
                {
                    acolhimento = new AcolhimentoUrgence();
                    acolhimento.FrequenciaCardiaca = tbxFreqCardiaca.Text;
                    acolhimento.FrequenciaRespiratoria = tbxFreqRespitatoria.Text;
                    acolhimento.Temperatura = tbxTemperatura.Text;
                    acolhimento.TensaoArterialInicio = tbxTensaoArterialInicio.Text;
                    acolhimento.TensaoArterialFim = tbxTensaoArterialFim.Text;
                    acolhimento.Hgt = tbxHgt.Text;
                    acolhimento.Data = prontuario.DataConsultaMedica.Value;
                    acolhimento.Prontuario = prontuario;
                    acolhimento.ClassificacaoRisco = prontuario.ClassificacaoRisco;
                    acolhimento.CodigoProfissional = usuarioProfissional.Id_Profissional;
                    acolhimento.CBOProfissional = usuarioProfissional.CodigoCBO;
                }

                EvolucaoMedica consulta = new EvolucaoMedica();
                consulta.PrimeiraConsulta = true;
                consulta.CodigoProfissional = usuarioProfissional.Id_Profissional;
                consulta.CBOProfissional = usuarioProfissional.CodigoCBO;
                consulta.Observacao = tbxAvaliacaoMedica.Text;
                consulta.Data = DateTime.Now;
                consulta.Prontuario = prontuario;
                consulta.ClassificacaoRisco = prontuario.ClassificacaoRisco;

                if (Session["ListaCid"] != null && ((IList<Cid>)Session["ListaCid"]).Count() > 0)
                {
                    IList<ViverMais.Model.Cid> cids = (IList<Cid>)Session["ListaCid"];
                    consulta.CodigosCids = cids.Select(p => p.Codigo).ToList();
                }

                if (ocupa_vaga)
                {
                    iProntuario.SalvarProntuario<Prontuario, AcolhimentoUrgence, EvolucaoMedica, PrescricaoProcedimento, PrescricaoProcedimentoNaoFaturavel, PrescricaoMedicamento, ProntuarioExame, ProntuarioExameEletivo>(usuarioProfissional.Id_Usuario, prontuario, acolhimento, consulta, RetornaListaProcedimento(), RetornaListaProcedimentoNaoFaturavel(), RetornaListaMedicamento(), RetornaListaExames(), RetornaListaExamesEletivos(), false, ocupa_vaga, tipo_vaga);
                    iProntuario.Inserir(new LogUrgencia(DateTime.Now, usuarioProfissional.Id_Usuario, 8, "id prontuario:" + prontuario.Codigo));
                }
                else
                {
                    iProntuario.SalvarProntuario<Prontuario, AcolhimentoUrgence, EvolucaoMedica, PrescricaoProcedimento, PrescricaoProcedimentoNaoFaturavel, PrescricaoMedicamento, ProntuarioExame, ProntuarioExameEletivo>(usuarioProfissional.Id_Usuario, prontuario, acolhimento, consulta, RetornaListaProcedimento(), RetornaListaProcedimentoNaoFaturavel(), RetornaListaMedicamento(), RetornaListaExames(), RetornaListaExamesEletivos(), false);
                    iProntuario.Inserir(new LogUrgencia(DateTime.Now, usuarioProfissional.Id_Usuario, 7, "id prontuario:" + prontuario.Codigo));
                }
            }
            catch (Exception f)
            {
                throw f;
            }

            HelperView.ExecutarPlanoContingencia(usuarioProfissional.Id_Usuario, prontuario.Codigo);
            //try
            //{
            //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuarioProfissional.Id_Usuario, prontuario.Codigo); });
            //}
            //catch { }

            if (prontuario.Situacao.Codigo != SituacaoAtendimento.EVASAO && prontuario.Situacao.Codigo != SituacaoAtendimento.OBITO)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados salvos com sucesso!');", true);
                HabilitarBotoesReceitaAtestado();
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados salvos com sucesso!');location='ExibeFilaAtendimento.aspx';", true);
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
        /// Habilita os botões para gerar atestados e receitas
        /// </summary>
        private void HabilitarBotoesReceitaAtestado()
        {
            this.Panel_Acoes.Visible = true;
            //ButtonSair.Visible = true;
            //Button_GerarReceita1.Visible = true;
            //Button_GerarAtestado1.Visible = true;
            //Button_GerarComparecimento.Visible = true;

            btnSalvar1.Visible = false;
            Button_Cancelar.Visible = false;
        }
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

            //if ((!string.IsNullOrEmpty(tbxTensaoArterialInicio.Text) && string.IsNullOrEmpty(tbxTensaoArterialFim.Text))
            //    || (string.IsNullOrEmpty(tbxTensaoArterialInicio.Text) && !string.IsNullOrEmpty(tbxTensaoArterialFim.Text)))
            //{
            //    CustomValidator_ConsultaMedica.ErrorMessage = "Informe a máxima e mínima da tensão arterial.";
            //    e.IsValid = false;
            //    return;
            //}

            if (ddlSituacao.SelectedValue != SituacaoAtendimento.ATENDIMENTO_INICIAL.ToString() && ddlSituacao.SelectedValue != SituacaoAtendimento.OBITO.ToString() && ddlSituacao.SelectedValue != SituacaoAtendimento.EVASAO.ToString() && ddlSituacao.SelectedValue != SituacaoAtendimento.FINALIZADO.ToString() && ddlSituacao.SelectedValue != SituacaoAtendimento.TRANSFERENCIA.ToString())
            //if (ddlSituacao.SelectedValue == "2" || ddlSituacao.SelectedValue == "4" || ddlSituacao.SelectedValue == "8" || ddlSituacao.SelectedValue == "9")
            {
                if (Session["ListaCid"] == null || ((IList<Cid>)Session["ListaCid"]).Count() <= 0)
                {
                    CustomValidator_ConsultaMedica.ErrorMessage = "Informe pelo menos uma suspeita diagnóstica.";
                    e.IsValid = false;
                    return;
                }
            }

            //ViverMais.Model.HorarioUnidade horariounidade = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.HorarioUnidade>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);
            //if (horariounidade == null &&
            //    (
            //        RetornaListaMedicamento().Count() > 0 ||
            //        RetornaListaProcedimento().Count() > 0 ||
            //        RetornaListaProcedimentoNaoFaturavel().Count() > 0)
            //   )
            //{
            //    CustomValidator_ConsultaMedica.ErrorMessage = "Atenção usuário! O seu PA não possui um horário de vingência para a prescrição solicitada! É necessário que este cadastro seja realizado. Entrar em contato com a administração.";
            //    e.IsValid = false;
            //    return;
            //}
        }

        /// <summary>
        /// Gera a receita/atestado para o prontuário corrente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_GerarReceitaAtestado(object sender, EventArgs e)
        {
            ImageButton bt = (ImageButton)sender;
            UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
            EvolucaoMedica consulta = Factory.GetInstance<IEvolucaoMedica>().BuscarConsultaMedica<EvolucaoMedica>(long.Parse(Request["codigo"].ToString()));

            if (bt.CommandArgument == "receita")
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Receita','../FormGerarAtestadoReceita.aspx?model=prescription&co_evolucao=" + consulta.Codigo.ToString() + "&co_profissional=" + usuarioprofissional.Id_Profissional + "&cbo_profissional=" + usuarioprofissional.CodigoCBO + "');", true);
            else
                if (bt.CommandArgument == "atestado")
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Atestado','../FormGerarAtestadoReceita.aspx?model=attested&co_evolucao=" + consulta.Codigo.ToString() + "&co_profissional=" + usuarioprofissional.Id_Profissional + "&cbo_profissional=" + usuarioprofissional.CodigoCBO + "');", true);
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Comparecimento','../FormGerarAtestadoReceita.aspx?model=attendance&co_evolucao=" + consulta.Codigo.ToString() + "&co_profissional=" + usuarioprofissional.Id_Profissional + "&cbo_profissional=" + usuarioprofissional.CodigoCBO + "');", true);
        }

        /// <summary>
        /// Chamada para habilitar ou desabilitar os validadores a depender da situação escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_Situacao(object sender, EventArgs e)
        {
            if (ddlSituacao.SelectedValue == SituacaoAtendimento.EVASAO.ToString() || ddlSituacao.SelectedValue == SituacaoAtendimento.OBITO.ToString() || ddlSituacao.SelectedValue == SituacaoAtendimento.TRANSFERENCIA.ToString())
            {
                Panel_SumarioAlta.Visible = false;

                if (ViewState["co_pacienteViverMais"] == null)
                {
                    ViewState["nomePaciente"] = this.tbxNomePaciente.Text;
                    ViewState["idadePaciente"] = this.tbxIdade.Text;
                    this.tbxNomePaciente.Text = "Ignorado";
                    this.tbxNomePaciente.Enabled = false;
                    this.tbxIdade.Text = "0";
                    this.tbxIdade.Enabled = false;
                }

                HabilitaDesabilitaValidadores(false, false, false, false);
            }
            else
            {
                if (ViewState["co_pacienteViverMais"] == null &&
                    ViewState["nomePaciente"] != null && ViewState["idadePaciente"] != null)
                {
                    this.tbxNomePaciente.Text = ViewState["nomePaciente"].ToString();
                    this.tbxNomePaciente.Enabled = true;
                    this.tbxIdade.Text = ViewState["idadePaciente"].ToString();
                    this.tbxIdade.Enabled = true;
                }

                if (ddlSituacao.SelectedValue == SituacaoAtendimento.ALTA_MEDICA.ToString() || ddlSituacao.SelectedValue == SituacaoAtendimento.ALTA_PEDIDO.ToString())
                {
                    Panel_SumarioAlta.Visible = true;
                    HabilitaDesabilitaValidadores(false, true, ddlSituacao.SelectedValue == SituacaoAtendimento.ALTA_MEDICA.ToString() ? true : false, false);
                }
                else
                {
                    Panel_SumarioAlta.Visible = false;
                    HabilitaDesabilitaValidadores(true, false, true, true);
                }
            }
        }

        /// <summary>
        /// Habilita/Desabilita os validadores da primeira consulta medica
        /// </summary>
        /// <param name="anamnese">RequiredField de Anamnese</param>
        /// <param name="sumarioalta">RequiredField de Sumário Alta</param>
        /// <param name="avaliacao_medica">CustomValidator Consulta Médica</param>
        /// <param name="peso">RequiredField de Peso</param>
        /// <param name="idade">RequiredField de Idade</param>
        private void HabilitaDesabilitaValidadores(bool anamnese, bool sumarioalta, bool avaliacao_medica, bool idade)
        {
            RequiredFieldValidator_Anamnese.Enabled = anamnese;
            RequiredFieldValidator_SumarioAlta.Enabled = sumarioalta;
            CustomValidator_ConsultaMedica.Enabled = avaliacao_medica;
            //RequiredFieldValidator_Peso.Enabled = peso;
            CompareValidator_Idade.Enabled = idade;
        }

        private void tbxAvaliacaoMedica_TextChanged(object sender, EventArgs e)
        {
            tbxAvaliacaoMedica.Text = tbxAvaliacaoMedica.Text.ToUpper();
        }

        #endregion

        #region SUSPEITA DIAGNÓSTICA/PRESCRIÇÃO MÉDICA
        /// <summary>
        /// Carrega os tipos de via de administração para medicamento
        /// </summary>
        private void CarregaViasAdministracao()
        {
            IList<ViaAdministracao> viasadministracao = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViaAdministracao>().OrderBy(p => p.Nome).ToList();
            DropDownList_ViaAdministracaoMedicamento.DataTextField = "Nome";
            DropDownList_ViaAdministracaoMedicamento.DataValueField = "Codigo";

            DropDownList_ViaAdministracaoMedicamento.DataSource = viasadministracao;
            DropDownList_ViaAdministracaoMedicamento.DataBind();

            DropDownList_ViaAdministracaoMedicamento.Items.Insert(0, new ListItem("Selecione...", "-1"));
        }

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
        /// Carrega o grid de procedimento
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridProcedimento(IList<PrescricaoProcedimento> procedimentos)
        {
            GridView_Procedimento.DataSource = procedimentos;
            GridView_Procedimento.DataBind();
        }

        /// <summary>
        /// Carrega o grid de medicamento para a prescrição corrente
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridPrescricaoMedicamento(IList<PrescricaoMedicamento> medicamentos)
        {
            gridMedicamentos.DataSource = medicamentos;
            gridMedicamentos.DataBind();
        }

        /// <summary>
        /// Carrega o grid de suspeita diagnóstica
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridCid(IList<Cid> cids)
        {
            gridCid.DataSource = cids;
            gridCid.DataBind();
        }

        /// <summary>
        /// Carrega a lista de procedimentos não-faturáveis para a prescrição válida
        /// </summary>
        /// <param name="p"></param>
        private void CarregaGridProcedimentoNaoFaturavel(IList<PrescricaoProcedimentoNaoFaturavel> procedimentos)
        {
            GridView_ProcedimentosNaoFaturavel.DataSource = procedimentos;
            GridView_ProcedimentosNaoFaturavel.DataBind();
        }

        /// <summary>
        /// Adiciona um CID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdicionarCid_Click(object sender, EventArgs e)
        {
            IList<Cid> lista = Session["ListaCid"] != null ? (IList<Cid>)Session["ListaCid"] : new List<Cid>();

            if (lista.Where(p => p.Codigo == ddlCid.SelectedValue).FirstOrDefault() == null)
            {
                lista.Add(Factory.GetInstance<ICid>().BuscarPorCodigo<Cid>(ddlCid.SelectedValue));

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
        protected void gridCid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IList<Cid> lista = Session["ListaCid"] != null ? (IList<Cid>)Session["ListaCid"] : new List<Cid>();

            lista.RemoveAt(e.RowIndex);

            Session["ListaCid"] = lista;
            CarregaGridCid(lista);
        }

        /// <summary>
        /// Busca os CID's de acordo com seu código
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_BuscarCids(object sender, EventArgs e)
        {
            IList<ViverMais.Model.Cid> cids = Factory.GetInstance<ICid>().BuscarPorGrupo<ViverMais.Model.Cid>(this.WUC_SuspeitaDiagnostica.WUC_DropDownListGrupoCID.SelectedValue);
            ddlCid.DataSource = cids;
            ddlCid.DataBind();

            this.InserirElementoDefault(this.ddlCid, 0);
            ddlCid.Focus();
            this.UpdatePanel_SuspeitaDiagnostica.Update();
        }

        /// <summary>
        /// Busca os CID's de acordo com o código digitado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_BuscarCID(object sender, ImageClickEventArgs e)
        {
            IList<Cid> cids = new List<Cid>();
            Cid cid = Factory.GetInstance<ICid>().BuscarPorCodigo<Cid>(this.WUC_SuspeitaDiagnostica.WUC_TextBoxCodigoCID.Text.ToUpper());

            if (cid != null)
                cids.Add(cid);

            ddlCid.DataSource = cids;
            ddlCid.DataBind();
            this.InserirElementoDefault(this.ddlCid, 0);
            ddlCid.Focus();
            this.UpdatePanel_SuspeitaDiagnostica.Update();
        }

        void InserirElementoDefault(DropDownList dropdown, int valor)
        {
            dropdown.Items.Insert(0, new ListItem("Selecione...", valor.ToString()));
        }

        protected void OnClickBuscarCIDPorNome(object sender, ImageClickEventArgs e)
        {
            ddlCid.DataSource = Factory.GetInstance<ICid>().BuscarPorInicioNome<Cid>(this.WUC_SuspeitaDiagnostica.WUC_TextBoxNomeCID.Text);
            ddlCid.DataBind();
            this.InserirElementoDefault(this.ddlCid, 0);
            ddlCid.Focus();
            this.UpdatePanel_SuspeitaDiagnostica.Update();
        }

        /// <summary>
        /// Carrega os procedimentos para o 'Cid' escolhido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaProcedimentos(object sender, EventArgs e)
        {
            DropDownList_Procedimento.Items.Clear();
            DropDownList_Procedimento.Items.Add(new ListItem("Selecione...", "-1"));

            if (ddlCid.SelectedValue != "-1")
            {
                IList<ProcedimentoCid> lpc = Factory.GetInstance<IProcedimento>().BuscarPorCid<ProcedimentoCid>(ddlCid.SelectedValue).OrderBy(p => p.Procedimento.Nome).ToList();
                foreach (ProcedimentoCid pc in lpc)
                {
                    ListItem item = new ListItem(pc.Procedimento.Nome, pc.Procedimento.Codigo);
                    DropDownList_Procedimento.Items.Add(item);
                }
                DropDownList_Procedimento.Focus();
            }
        }

        /// <summary>
        /// Adiciona o medicamento na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdicionarMedicamento_Click(object sender, EventArgs e)
        {
            IList<PrescricaoMedicamento> lista = Session["ListaMedicamento"] != null ? (IList<PrescricaoMedicamento>)Session["ListaMedicamento"] : new List<PrescricaoMedicamento>();

            if (lista.Where(p => p.Medicamento.ToString() == ddlMedicamentos.SelectedValue).FirstOrDefault() == null)
            {
                PrescricaoMedicamento medicamento = new PrescricaoMedicamento();
                medicamento.ObjetoMedicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(int.Parse(ddlMedicamentos.SelectedValue));
                medicamento.Medicamento = medicamento.ObjetoMedicamento.Codigo;
                medicamento.SetIntervalo(tbxIntervaloMedicamento.Text, int.Parse(DropDownList_UnidadeTempoFrequenciaMedicamento.SelectedValue));
                medicamento.ExecutarPrimeiroMomento = CheckBox_ExecutarMedicamentoAgora.Checked;
                medicamento.Observacao = TextBox_ObservacaoPrescricaoMedicamento.Text;

                if (DropDownList_ViaAdministracaoMedicamento.SelectedValue != "-1")
                    medicamento.ViaAdministracao = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViaAdministracao>(int.Parse(DropDownList_ViaAdministracaoMedicamento.SelectedValue));

                if (!medicamento.IntervaloValido())
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O intervalo máximo para aplicação do(a) medicamento/prescrição é de 24 horas.');", true);
                    return;
                }

                lista.Add(medicamento);
                Session["ListaMedicamento"] = lista;
                CarregaGridPrescricaoMedicamento(lista);
                OnClick_CancelarMedicamento(sender, e);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este(a) Medicamento/Prescrição já se encontra na lista de solicitações.');", true);
        }

        /// <summary>
        /// Adiciona o procedimento na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarProcedimento(object sender, EventArgs e)
        {
            IList<PrescricaoProcedimento> procedimentos = RetornaListaProcedimento();
            UsuarioProfissionalUrgence usuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();

            if (this.CheckBox_ExecutarProcimentoAgora.Checked &&
                !iProcedimento.CBOExecutaProcedimento(DropDownList_Procedimento.SelectedValue, usuarioProfissional.CodigoCBO))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, de acordo com o seu CBO, você não tem permissão para executar este procedimento.');", true);
                return;
            }

            if (procedimentos.Where(p => p.Procedimento.Codigo == DropDownList_Procedimento.SelectedValue).FirstOrDefault() == null)
            {
                IRegistro iRegistro = Factory.GetInstance<IRegistro>();
                bool exigenciaCid = iRegistro.ProcedimentoExigeCid(DropDownList_Procedimento.SelectedValue);

                if (exigenciaCid && DropDownList_ProcedimentoCID.SelectedValue == "-1")
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este procedimento exige a escolha de um CID.')", true);
                    return;
                }

                ICid iCid = Factory.GetInstance<ICid>();
                PrescricaoProcedimento procedimento = new PrescricaoProcedimento();

                procedimento.CodigoProcedimento = DropDownList_Procedimento.SelectedValue;
                procedimento.Procedimento = iProcedimento.BuscarPorCodigo<Procedimento>(DropDownList_Procedimento.SelectedValue);
                procedimento.CodigoCid = this.DropDownList_ProcedimentoCID.SelectedValue;
                procedimento.Cid = iCid.BuscarPorCodigo<Cid>(this.DropDownList_ProcedimentoCID.SelectedValue);
                procedimento.ExecutarPrimeiroMomento = CheckBox_ExecutarProcimentoAgora.Checked;
                procedimento.SetIntervalo(TextBox_IntervaloProcedimento.Text, int.Parse(DropDownList_UnidadeTempoFrequenciaProcedimento.SelectedValue));

                if (!procedimento.IntervaloValido())
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O intervalo máximo para aplicação do procedimento é de 24 horas.');", true);
                    return;
                }

                procedimentos.Add(procedimento);
                Session["ListaProcedimento"] = procedimentos;
                CarregaGridProcedimento(procedimentos);
                OnClick_CancelarProcedimento(sender, e);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este Procedimento já se encontra na lista de solicitações.');", true);
        }

        /// <summary>
        /// Adiciona um procedimento não-faturável na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            IList<PrescricaoProcedimentoNaoFaturavel> lista = RetornaListaProcedimentoNaoFaturavel();

            if (lista.Where(p => p.Procedimento.Codigo == int.Parse(DropDownList_ProcedimentosNaoFaturaveis.SelectedValue)).FirstOrDefault() == null)
            {
                PrescricaoProcedimentoNaoFaturavel procedimento = new PrescricaoProcedimentoNaoFaturavel();
                procedimento.Procedimento = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ProcedimentoNaoFaturavel>(int.Parse(DropDownList_ProcedimentosNaoFaturaveis.SelectedValue));
                procedimento.SetIntervalo(TextBox_IntervaloProcedimentoNaoFaturavel.Text, int.Parse(DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavel.SelectedValue));
                procedimento.ExecutarPrimeiroMomento = CheckBox_ExecutarProcedimentoNaoFaturavelAgora.Checked;

                if (!procedimento.IntervaloValido())
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O intervalo máximo para aplicação do procedimento é de 24 horas.');", true);
                    return;
                }

                lista.Add(procedimento);
                Session["ListaProcedimentoNaoFaturavel"] = lista;
                CarregaGridProcedimentoNaoFaturavel(lista);
                this.OnClick_CancelarProcedimentoNaoFaturavel(new object(), new EventArgs());
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este Procedimento já se encontra na lista de solicitações.');", true);
        }

        protected void OnClick_BuscarProcimentoCID(object sender, ImageClickEventArgs e)
        {
            if (this.DropDownList_Procedimento.SelectedValue != "-1")
            {
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IList<Cid> cids = iPrescricao.BuscarCidsProcedimentoPorCodigo<Cid>(DropDownList_Procedimento.SelectedValue, this.WUC_ProcedimentoCid.WUC_TextBoxCodigoCID.Text.ToUpper());

                DropDownList_ProcedimentoCID.DataSource = cids;
                DropDownList_ProcedimentoCID.DataBind();

                this.InserirElementoDefault(this.DropDownList_ProcedimentoCID, -1);
                this.UpdatePanel_ProcedimentoCID.Update();
            }
        }

        protected void OnSelectedIndexChanged_ProcecimentoCid(object sender, EventArgs e)
        {
            if (this.DropDownList_Procedimento.SelectedValue != "-1")
            {
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IList<Cid> cids = iPrescricao.BuscarCidsProcedimentoPorGrupo<Cid>(DropDownList_Procedimento.SelectedValue, this.WUC_ProcedimentoCid.WUC_DropDownListGrupoCID.SelectedValue);

                DropDownList_ProcedimentoCID.DataSource = cids;
                DropDownList_ProcedimentoCID.DataBind();

                this.InserirElementoDefault(this.DropDownList_ProcedimentoCID, -1);
                this.UpdatePanel_ProcedimentoCID.Update();
            }
        }

        protected void OnClickBuscarProcedimentoCIDPorNome(object sender, ImageClickEventArgs e)
        {
            if (this.DropDownList_Procedimento.SelectedValue != "-1")
            {
                this.DropDownList_ProcedimentoCID.DataSource = Factory.GetInstance<ICid>().BuscarPorInicioNome<Cid>(DropDownList_Procedimento.SelectedValue, this.WUC_ProcedimentoCid.WUC_TextBoxNomeCID.Text);
                this.DropDownList_ProcedimentoCID.DataBind();
                this.InserirElementoDefault(this.DropDownList_ProcedimentoCID, -1);
                this.UpdatePanel_ProcedimentoCID.Update();
            }
        }

        /// <summary>
        /// Deleta o medicamento na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridMedicamentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IList<PrescricaoMedicamento> lista = Session["ListaMedicamento"] != null ? (IList<PrescricaoMedicamento>)Session["ListaMedicamento"] : new List<PrescricaoMedicamento>();
            lista.RemoveAt(e.RowIndex);
            Session["ListaMedicamento"] = lista;

            CarregaGridPrescricaoMedicamento(lista);
        }

        /// <summary>
        /// Deleta o procedimento adicionado na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_DeletarProcedimento(object sender, GridViewDeleteEventArgs e)
        {
            IList<PrescricaoProcedimento> lista = RetornaListaProcedimento();
            lista.RemoveAt(e.RowIndex);
            Session["ListaProcedimento"] = lista;

            CarregaGridProcedimento(lista);
        }

        /// <summary>
        /// Deleta o procedimento não-faturável adicionado na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_ExcluirProcedimentoNaoFaturavel(object sender, GridViewDeleteEventArgs e)
        {
            IList<PrescricaoProcedimentoNaoFaturavel> lista = RetornaListaProcedimentoNaoFaturavel();
            lista.RemoveAt(e.RowIndex);
            Session["ListaProcedimentoNaoFaturavel"] = lista;

            CarregaGridProcedimentoNaoFaturavel(lista);
        }

        protected void OnSelectedIndexChanged_RetiraCids(object sender, EventArgs e)
        {
            this.DropDownList_ProcedimentoCID.Items.Clear();
            this.DropDownList_ProcedimentoCID.Items.Add(new ListItem("Selecione...", "-1"));
        }

        /// <summary>
        /// Cancela a inserção do procedimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarProcedimento(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimento, this.RequiredFieldValidator_FrequenciaProcedimento,
                    this.RegularExpressionValidator_FrequenciaProcedimento, this.CompareValidator_FrequenciaProcedimento,
                    true);
            DropDownList_Procedimento.SelectedValue = "-1";
            TextBox_IntervaloProcedimento.Text = "";
            this.OnSelectedIndexChanged_RetiraCids(new object(), new EventArgs());

            this.WUC_ProcedimentoCid.WUC_DropDownListGrupoCID.SelectedValue = "-1";
            this.WUC_ProcedimentoCid.WUC_UpdatePanelPesquisarCID.Update();

            CheckBox_ExecutarProcimentoAgora.Checked = false;
            DropDownList_UnidadeTempoFrequenciaProcedimento.SelectedValue = ((int)PrescricaoProcedimento.UNIDADETEMPO.HORAS).ToString();
        }

        /// <summary>
        /// Cancela a inserção do medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarMedicamento(object sender, EventArgs e)
        {
            ddlMedicamentos.SelectedValue = "0";
            TextBox_ObservacaoPrescricaoMedicamento.Text = "";
            tbxIntervaloMedicamento.Text = "";
            DropDownList_ViaAdministracaoMedicamento.SelectedValue = "-1";
            CheckBox_ExecutarMedicamentoAgora.Checked = false;
            DropDownList_UnidadeTempoFrequenciaMedicamento.SelectedValue = ((int)PrescricaoMedicamento.UNIDADETEMPO.HORAS).ToString();
            this.HabilitaDesabilitaFrequencia(this.tbxIntervaloMedicamento, this.RequiredFieldValidator_FrequenciaMedicamento,
                    this.RegularExpressionValidator_FrequenciaMedicamento, this.CompareValidator_FrequenciaMedicamento,
                    true);
        }

        protected void OnClick_CancelarProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            TextBox_BuscarProcedimento.Text = "";
            DropDownList_ProcedimentosNaoFaturaveis.SelectedValue = "-1";
            TextBox_IntervaloProcedimentoNaoFaturavel.Text = "";
            CheckBox_ExecutarProcedimentoNaoFaturavelAgora.Checked = false;
            DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavel.SelectedValue = ((int)PrescricaoProcedimentoNaoFaturavel.UNIDADETEMPO.HORAS).ToString();

            this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoNaoFaturavel, this.RequiredFieldValidator_FrequenciaProcedimentoNaoFaturavel,
            this.RegularExpressionValidator_FrequenciaProcedimentoNaoFaturavel, this.CompareValidator_FrequenciaProcedimentoNaoFaturavel,
            true);
        }
        #endregion

        #region SOLICITAÇÃO DE EXAMES
        /// <summary>
        /// Carrega a lista de exames para possíveis solicitações
        /// </summary>
        private void CarregaListaExames()
        {
            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();

            ddlExames.DataTextField = "Descricao";
            ddlExames.DataValueField = "Codigo";
            DropDownList_ExameEletivo.DataTextField = "Descricao";
            DropDownList_ExameEletivo.DataValueField = "Codigo";

            ddlExames.DataSource = iUrgencia.ListarTodos<ViverMais.Model.Exame>().Where(p => p.Status == Convert.ToChar(Exame.EnumDescricaoStatus.Ativo)).OrderBy(s => s.Descricao).ToList();
            ddlExames.DataBind();

            DropDownList_ExameEletivo.DataSource = iUrgencia.ListarTodos<ExameEletivo>().Where(p => p.Status == Convert.ToChar(ExameEletivo.DescricaoStatus.Ativo)).OrderBy(p => p.Descricao).ToList();
            DropDownList_ExameEletivo.DataBind();

            ddlExames.Items.Insert(0, new ListItem("Selecione...", "0"));
            DropDownList_ExameEletivo.Items.Insert(0, new ListItem("Selecione...", "0"));
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
        private void CarregaGridExames(IList<ProntuarioExame> exames)
        {
            gridExames.DataSource = exames;
            gridExames.DataBind();
        }

        /// <summary>
        /// Carrega o grid de exames eletivos
        /// </summary>
        /// <param name="lista"></param>
        private void CarregaGridExamesEletivos(IList<ProntuarioExameEletivo> exames)
        {
            GridView_ExamesEletivos.DataSource = exames;
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

        /// <summary>
        /// Adiciona o exame na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdicionarExames_Click(object sender, EventArgs e)
        {
            IList<ProntuarioExame> exames = RetornaListaExames();

            if (exames.Where(p => p.Exame.Codigo.ToString() == ddlExames.SelectedValue).FirstOrDefault() == null)
            {
                ProntuarioExame exame = new ProntuarioExame();
                exame.Exame = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<Exame>(int.Parse(ddlExames.SelectedValue));
                exame.Data = DateTime.Now;
                UsuarioProfissionalUrgence ususarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                exame.Profissional = ususarioprofissional.Id_Profissional;
                exame.CBOProfissional = ususarioprofissional.CodigoCBO;

                exames.Add(exame);

                Session["ListaExame"] = exames;
                CarregaGridExames(exames);

                ddlExames.SelectedValue = "0";
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este Exame Interno já se encontra na lista de solicitações.');", true);
        }

        /// <summary>
        /// Deleta o exame adicionado na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridExames_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IList<ProntuarioExame> exames = RetornaListaExames();
            exames.RemoveAt(e.RowIndex);
            Session["ListaExame"] = exames;
            CarregaGridExames(exames);
        }

        /// <summary>
        /// Adiciona um exame eletivo solicitado pelo profissional
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarExameEletivo(object sender, EventArgs e)
        {
            IList<ProntuarioExameEletivo> exames = RetornaListaExamesEletivos();

            if (exames.Where(p => p.Exame.Codigo.ToString() == DropDownList_ExameEletivo.SelectedValue).FirstOrDefault() == null)
            {
                ProntuarioExameEletivo exame = new ProntuarioExameEletivo();
                exame.Exame = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ExameEletivo>(int.Parse(DropDownList_ExameEletivo.SelectedValue));
                exame.Data = DateTime.Now;
                UsuarioProfissionalUrgence ususarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                exame.Profissional = ususarioprofissional.Id_Profissional;
                exame.CBOProfissional = ususarioprofissional.CodigoCBO;
                exames.Add(exame);

                Session["ListaExameEletivo"] = exames;
                CarregaGridExamesEletivos(exames);

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
            IList<ProntuarioExameEletivo> exames = RetornaListaExamesEletivos();
            exames.RemoveAt(e.RowIndex);
            Session["ListaExameEletivo"] = exames;
            CarregaGridExamesEletivos(exames);
        }

        #endregion

        protected void OnClick_BuscarProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            DropDownList_ProcedimentosNaoFaturaveis.DataTextField = "Nome";
            DropDownList_ProcedimentosNaoFaturaveis.DataValueField = "Codigo";

            DropDownList_ProcedimentosNaoFaturaveis.DataSource = Factory.GetInstance<IProcedimentoNaoFaturavel>().BuscarPorNome<ProcedimentoNaoFaturavel>(TextBox_BuscarProcedimento.Text);
            DropDownList_ProcedimentosNaoFaturaveis.DataBind();

            DropDownList_ProcedimentosNaoFaturaveis.Items.Insert(0, new ListItem("Selecione...", "-1"));
            DropDownList_ProcedimentosNaoFaturaveis.Focus();
        }

        protected void OnClick_BuscarMedicamento(object sender, EventArgs e)
        {
            ddlMedicamentos.DataTextField = "Nome";
            ddlMedicamentos.DataValueField = "Codigo";

            ddlMedicamentos.DataSource = Factory.GetInstance<IMedicamento>().BuscarPorDescricao<Medicamento>(-1, TextBox_BuscarMedicamento.Text, false);
            ddlMedicamentos.DataBind();

            ddlMedicamentos.Items.Insert(0, new ListItem("Selecione...", "0"));
            ddlMedicamentos.Focus();
        }

        protected void OnClick_BuscarProcedimentoSIGTAP(object sender, EventArgs e)
        {
            DropDownList_Procedimento.DataTextField = "Nome";
            DropDownList_Procedimento.DataValueField = "Codigo";

            DropDownList_Procedimento.DataSource = Factory.GetInstance<IProcedimento>().BuscarPorNome<Procedimento>(TextBox_BuscarProcedimentoSIGTAP.Text.ToUpper());
            DropDownList_Procedimento.DataBind();

            DropDownList_Procedimento.Items.Insert(0, new ListItem("Selecione...", "-1"));
            DropDownList_Procedimento.Focus();
        }

        protected void OnSelectedIndexChanged_FrequenciaProcedimento(object sender, EventArgs e)
        {
            if (int.Parse(this.DropDownList_UnidadeTempoFrequenciaProcedimento.SelectedValue) == (int)PrescricaoProcedimento.UNIDADETEMPO.UNICA)
            {
                this.TextBox_IntervaloProcedimento.Text = "";
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimento, this.RequiredFieldValidator_FrequenciaProcedimento,
                    this.RegularExpressionValidator_FrequenciaProcedimento, this.CompareValidator_FrequenciaProcedimento,
                    false);
            }
            else
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimento, this.RequiredFieldValidator_FrequenciaProcedimento,
                    this.RegularExpressionValidator_FrequenciaProcedimento, this.CompareValidator_FrequenciaProcedimento,
                    true);
        }

        protected void OnSelectedIndexChanged_FrequenciaProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            if (int.Parse(this.DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavel.SelectedValue) == (int)PrescricaoProcedimentoNaoFaturavel.UNIDADETEMPO.UNICA)
            {
                this.TextBox_IntervaloProcedimentoNaoFaturavel.Text = "";
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoNaoFaturavel, this.RequiredFieldValidator_FrequenciaProcedimentoNaoFaturavel,
                    this.RegularExpressionValidator_FrequenciaProcedimentoNaoFaturavel, this.CompareValidator_FrequenciaProcedimentoNaoFaturavel,
                    false);
            }
            else
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoNaoFaturavel, this.RequiredFieldValidator_FrequenciaProcedimentoNaoFaturavel,
                    this.RegularExpressionValidator_FrequenciaProcedimentoNaoFaturavel, this.CompareValidator_FrequenciaProcedimentoNaoFaturavel,
                    true);
        }

        protected void OnSelectedIndexChanged_MedicamentoNaoFaturavel(object sender, EventArgs e)
        {
            if (int.Parse(this.DropDownList_UnidadeTempoFrequenciaMedicamento.SelectedValue) == (int)PrescricaoMedicamento.UNIDADETEMPO.UNICA)
            {
                this.tbxIntervaloMedicamento.Text = "";
                this.HabilitaDesabilitaFrequencia(this.tbxIntervaloMedicamento, this.RequiredFieldValidator_FrequenciaMedicamento,
                    this.RegularExpressionValidator_FrequenciaMedicamento, this.CompareValidator_FrequenciaMedicamento,
                    false);
            }
            else
                this.HabilitaDesabilitaFrequencia(this.tbxIntervaloMedicamento, this.RequiredFieldValidator_FrequenciaMedicamento,
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
    }
}
