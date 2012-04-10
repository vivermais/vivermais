using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections;
using ViverMais.View.Urgencia.RelatoriosCrystal;
using System.Data;
using System.Threading;

namespace ViverMais.View.Urgencia
{
    public partial class Inc_PrescricoesRegistradas : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButton procedimentocidcodigo = this.WUC_PrescricaoProcedimentoCID.WUC_ImageButtonBuscarCID;
            ImageButton procedimentocidnome = this.WUC_PrescricaoProcedimentoCID.WUC_ImageButtonBuscarCIDPorNome;
            DropDownList procedimentocidlista = this.WUC_PrescricaoProcedimentoCID.WUC_DropDownListGrupoCID;

            procedimentocidcodigo.Click += new ImageClickEventHandler(this.OnClick_BuscarProcimentoCID);
            procedimentocidnome.Click += new ImageClickEventHandler(this.OnClickBuscarProcedimentoCIDPorNome);
            procedimentocidlista.SelectedIndexChanged += new EventHandler(this.OnSelectedIndexChanged_ProcecimentoCid);

            this.InserirTrigger(procedimentocidcodigo.UniqueID, "Click", UpdatePanel_ProcedimentoCIDPrescricao);
            this.InserirTrigger(procedimentocidnome.UniqueID, "Click", UpdatePanel_ProcedimentoCIDPrescricao);
            this.InserirTrigger(procedimentocidlista.UniqueID, "SelectedIndexChanged", UpdatePanel_ProcedimentoCIDPrescricao);

            this.InserirTrigger(this.Button_AdicionarProcedimentoAlterar.UniqueID, "Click", this.WUC_PrescricaoProcedimentoCID.WUC_UpdatePanelPesquisarCID);

            if (!IsPostBack)
            {
                ViewState["co_prontuario"] = Request["codigo"].ToString();
                //verifica se o usuário tem permissão para excluir as permissões agendadas
                //a consulta é feita aqui e guardada porque, desta maneira, só é preciso executá-la uma vez
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "EXCLUIR_PRESCRICAO_AGENDADA", Modulo.URGENCIA))
                    ViewState["ExcluirPrescricao"] = true;
                else
                    ViewState["ExcluirPrescricao"] = false;

                CarregaDadosIniciais();
                CarregaPrescricoesAlterar(long.Parse(ViewState["co_prontuario"].ToString()));
                this.WUC_PrescricaoProcedimentoCID.WUC_PanelPesquisarCID.Visible = false;
                this.WUC_PrescricaoProcedimentoCID.WUC_UpdatePanelPesquisarCID.Update();
            }
        }

        private void NaoMostrarCadastroProcedimento()
        {
            Panel_CadastrarProcedimento.Visible = false;
            Panel_CidProcedimentoPrescricao.Visible = false;
            Panel_BotaoAdicionarProcecimentoPrescricao.Visible = false;
            this.WUC_PrescricaoProcedimentoCID.WUC_PanelPesquisarCID.Visible = false;
            this.WUC_PrescricaoProcedimentoCID.WUC_UpdatePanelPesquisarCID.Update();
        }

        private void MostrarCadastroProcedimento()
        {
            Panel_CadastrarProcedimento.Visible = true;
            Panel_CidProcedimentoPrescricao.Visible = true;
            Panel_BotaoAdicionarProcecimentoPrescricao.Visible = true;
            this.WUC_PrescricaoProcedimentoCID.WUC_PanelPesquisarCID.Visible = true;
            this.WUC_PrescricaoProcedimentoCID.WUC_UpdatePanelPesquisarCID.Update();
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
        }

        protected void OnClick_VerBulario(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('http://www.anvisa.gov.br/bularioeletronico/default.asp?txtPrincipioAtivo=" + DropDownList_MedicamentoAlterar.SelectedItem.Text + "');", true);
        }

        /// <summary>
        /// Carrega os dados inicias para as prescrições registradas
        /// </summary>
        private void CarregaDadosIniciais()
        {
            IList<ViaAdministracao> viasadministracao = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViaAdministracao>().OrderBy(p => p.Nome).ToList();

            DropDownList_ViaAdministracaoMedicamentoAlterar.DataTextField = "Nome";
            DropDownList_ViaAdministracaoMedicamentoAlterar.DataValueField = "Codigo";

            DropDownList_ViaAdministracaoMedicamentoAlterar.DataSource = viasadministracao;
            DropDownList_ViaAdministracaoMedicamentoAlterar.DataBind();
            DropDownList_ViaAdministracaoMedicamentoAlterar.Items.Insert(0, new ListItem("Selecione...", "-1"));

            DropDownList_MedicamentoAlterar.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
            DropDownList_ViaAdministracaoMedicamentoAlterar.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
            DropDownList_ProcedimentoNaoFaturavelCadastrar.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
            DropDownList_ProcedimentoAlterar.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
            DropDownList_CidAlterarProcedimento.Attributes.Add("onmouseover", "javascript:showTooltip(this);");

            foreach (string unidadeintervalo in Enum.GetNames(typeof(PrescricaoMedicamento.UNIDADETEMPO)).ToList())
                DropDownList_UnidadeTempoFrequenciaMedicamentoAlterar.Items.Add(new ListItem(unidadeintervalo, ((int)Enum.Parse(typeof(PrescricaoMedicamento.UNIDADETEMPO), unidadeintervalo)).ToString()));

            foreach (string unidadeintervalo in Enum.GetNames(typeof(PrescricaoProcedimento.UNIDADETEMPO)).ToList())
                DropDownList_UnidadeTempoFrequenciaProcedimentoAlterar.Items.Add(new ListItem(unidadeintervalo, ((int)Enum.Parse(typeof(PrescricaoProcedimento.UNIDADETEMPO), unidadeintervalo)).ToString()));

            foreach (string unidadeintervalo in Enum.GetNames(typeof(PrescricaoProcedimentoNaoFaturavel.UNIDADETEMPO)).ToList())
                DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavelAlterar.Items.Add(new ListItem(unidadeintervalo, ((int)Enum.Parse(typeof(PrescricaoProcedimentoNaoFaturavel.UNIDADETEMPO), unidadeintervalo)).ToString()));
        }

        /// <summary>
        /// A partir do vínculo do profissional verifica se o mesmo pode alterar um procedimento, procedimento não faturável ou medicamento de determinada prescrição
        /// </summary>
        private bool VerificaFuncionalidadesMedico()
        {
            UsuarioProfissionalUrgence u = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
            if (u == null)
                return false;
            ICBO iCbo = Factory.GetInstance<ICBO>();

            if (iCbo.CBOPertenceMedico<CBO>(iCbo.BuscarPorCodigo<CBO>(u.CodigoCBO)))
                return true;

            return false;
        }

        #region PRESCRIÇÕES
        /// <summary>
        /// Carrega as prescrições do prontuário para alterar seus itens
        /// </summary>
        /// <param name="co_prontuario"></param>
        private void CarregaPrescricoesAlterar(long co_prontuario)
        {
            IList<Prescricao> prescricoes = Factory.GetInstance<IPrescricao>().BuscarPorProntuario<Prescricao>(co_prontuario);
            Session["prescricoesProntuario"] = prescricoes;
            GridView_PrescricaoAlterar.DataSource = prescricoes;
            GridView_PrescricaoAlterar.DataBind();
        }

        /// <summary>
        /// Paginação do gridview das prescrições registradas para este prontuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoPrescricaoAlterar(object sender, GridViewPageEventArgs e)
        {
            //IList<Prescricao> prescricoes = Factory.GetInstance<IPrescricao>().BuscarPorProntuario<Prescricao>(co_prontuario);
            //Session["prescricoesProntuario"] = prescricoes;
            GridView_PrescricaoAlterar.DataSource = (IList<Prescricao>)Session["prescricoesProntuario"];
            GridView_PrescricaoAlterar.DataBind();

            GridView_PrescricaoAlterar.PageIndex = e.NewPageIndex;
            GridView_PrescricaoAlterar.DataBind();
        }

        protected void OnRowDataBound_GridView_PrescricaoAlterar(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                long co_prescricao = long.Parse(GridView_PrescricaoAlterar.DataKeys[e.Row.RowIndex]["Codigo"].ToString());
                Prescricao prescricao = ((IList<Prescricao>)Session["prescricoesProntuario"]).Where(p => p.Codigo == co_prescricao).First();
                //Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<Prescricao>(co_prescricao);

                if (prescricao.Status != Convert.ToChar(Prescricao.StatusPrescricao.Invalida) && prescricao.Status != Convert.ToChar(Prescricao.StatusPrescricao.Valida))
                    ((LinkButton)e.Row.FindControl("LinkButton_Aprazar")).Enabled = false;

                if (prescricao.Status != Convert.ToChar(Prescricao.StatusPrescricao.Agendada) || (prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Agendada) && !bool.Parse(ViewState["ExcluirPrescricao"].ToString())))
                {
                    LinkButton lbexcluir = (LinkButton)e.Row.FindControl("LinkButton_Exluir");
                    lbexcluir.Enabled = false;
                    lbexcluir.OnClientClick = lbexcluir.OnClientClick.Remove(0, lbexcluir.OnClientClick.Length);
                }

                if (prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Agendada))
                {
                    LinkButton lbaprazados = e.Row.FindControl("LinkButton_Aprazados") as LinkButton;
                    if (lbaprazados != null)
                        lbaprazados.Enabled = false;
                }

                //Ver aprazamentos
                this.InserirTrigger(e.Row, this.UpdatePanel_VerAprazados, "LinkButton_Aprazados", "Click");

                //Excluir prescrição
                this.InserirTrigger(e.Row, this.UpdatePanel_DescricaoPrescricao, "LinkButton_Exluir", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_CadastrarProcedimento, "LinkButton_Exluir", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_ProcedimentoCIDPrescricao, "LinkButton_Exluir", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_BotaoAdicionarProcimentoPrescricao, "LinkButton_Exluir", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_CadastrarProcedimentoNaoFaturavel, "LinkButton_Exluir", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_CadastrarMedicamento, "LinkButton_Exluir", "Click");
                this.InserirTrigger(e.Row, this.WUC_PrescricaoProcedimentoCID.WUC_UpdatePanelPesquisarCID, "LinkButton_Exluir", "Click");

                this.InserirTrigger(e.Row, this.UpdatePanel_ProcedimentoPrescricao, "LinkButton_Exluir", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_ProcedimentoNaoFaturavelPrescricao, "LinkButton_Exluir", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_MedicamentosPrescricao, "LinkButton_Exluir", "Click");

                //Visualização dos itens
                this.InserirTrigger(e.Row, this.UpdatePanel_ProcedimentoPrescricao, "LinkButton_VerItens", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_ProcedimentoNaoFaturavelPrescricao, "LinkButton_VerItens", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_MedicamentosPrescricao, "LinkButton_VerItens", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_CadastrarProcedimento, "LinkButton_VerItens", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_ProcedimentoCIDPrescricao, "LinkButton_VerItens", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_BotaoAdicionarProcimentoPrescricao, "LinkButton_VerItens", "Click");
                this.InserirTrigger(e.Row, this.WUC_PrescricaoProcedimentoCID.WUC_UpdatePanelPesquisarCID, "LinkButton_VerItens", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_DescricaoPrescricao, "LinkButton_VerItens", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_CadastrarProcedimentoNaoFaturavel, "LinkButton_VerItens", "Click");
                this.InserirTrigger(e.Row, this.UpdatePanel_CadastrarMedicamento, "LinkButton_VerItens", "Click");
            }
        }

        /// <summary>
        /// Insere uma trigger do tipo asyncPostBack
        /// </summary>
        /// <param name="linha">linha do gridview onde será procurado o controle</param>
        /// <param name="updatepanel">updatepanel onde será inserido a trigger</param>
        /// <param name="idcontrole">id único do controle procurado</param>
        /// <param name="nomeevento">nome do evento que disparará a trigger</param>
        private void InserirTrigger(GridViewRow linha, UpdatePanel updatepanel, string idcontrole, string nomeevento)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            Control control = linha.FindControl(idcontrole);
            trigger.ControlID = control.UniqueID;
            trigger.EventName = nomeevento;
            updatepanel.Triggers.Add(trigger);
        }

        protected void OnClick_VerItensPrescricao(object sender, EventArgs e)
        {
            long co_prescricao = long.Parse(((LinkButton)sender).CommandArgument.ToString());
            //IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();

            ViewState["co_prescricao_alterar"] = co_prescricao;
            CarregaItensPrescritos(co_prescricao);

            this.Panel_Prescricao.Visible = true;
            this.Panel_ProcedimentosPrescricao.Visible = true;
            this.Panel_ProcedimentosNaoFaturaveisPrescricao.Visible = true;
            this.Panel_MedicamentosPrescricao.Visible = true;

            this.UpdatePanel_DescricaoPrescricao.Update();
            this.UpdatePanel_ProcedimentoPrescricao.Update();
            this.UpdatePanel_ProcedimentoNaoFaturavelPrescricao.Update();
            this.UpdatePanel_MedicamentosPrescricao.Update();

            Prescricao prescricao = ((IList<Prescricao>)Session["prescricoesProntuario"]).Where(p => p.Codigo == co_prescricao).First();
            //iPrescricao.BuscarPorCodigo<Prescricao>(co_prescricao);

            this.Label_DataPrescricao.Text = prescricao.DataCompleta;
            this.Label_StatusPrescricao.Text = prescricao.DescricaoStatus;
            this.Label_AprazarPrescricao.Text = prescricao.UltimaDataValida.ToString("dd/MM/yyyy HH:mm");

            VinculoProfissional vinculo = Factory.GetInstance<IVinculo>().BuscarPorVinculoProfissional<VinculoProfissional>(prescricao.Prontuario.CodigoUnidade, prescricao.Profissional, prescricao.CBOProfissional).FirstOrDefault();
            this.Label_ProfissionalCRM.Text = vinculo.Profissional.Nome + " - " + (string.IsNullOrEmpty(vinculo.RegistroConselho) ? "CRM Não Identificado" : vinculo.RegistroConselho);

            if (prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Suspensa)
                || prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Invalida))
            {
                Panel_IncluiMedicamento.Visible = false;
                Panel_ProcedimentoNaoFaturavel.Visible = false;
                this.NaoMostrarCadastroProcedimento();
            }
            else
            {
                if (!VerificaFuncionalidadesMedico())
                {
                    Panel_IncluiMedicamento.Visible = false;
                    Panel_ProcedimentoNaoFaturavel.Visible = true;
                    this.NaoMostrarCadastroProcedimento();
                }
                else
                {
                    Panel_IncluiMedicamento.Visible = true;
                    this.MostrarCadastroProcedimento();
                    Panel_ProcedimentoNaoFaturavel.Visible = true;
                }
            }

            this.UpdatePanel_CadastrarProcedimento.Update();
            this.UpdatePanel_ProcedimentoCIDPrescricao.Update();
            this.UpdatePanel_BotaoAdicionarProcimentoPrescricao.Update();
            this.UpdatePanel_CadastrarProcedimentoNaoFaturavel.Update();
            this.UpdatePanel_CadastrarMedicamento.Update();
        }

        protected void OnClick_AprazarPrescricao(object sender, EventArgs e)
        {
            Session["tempo_atendimento"] = 0;
            Response.Redirect("FormAprazamento.aspx?co_prescricao=" + ((LinkButton)sender).CommandArgument.ToString());
        }

        protected void OnClick_ExcluirPrescricao(object sender, EventArgs e)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            iPrescricao.ExcluirPrescricao<Prescricao>(iPrescricao.BuscarPorCodigo<Prescricao>(long.Parse(((LinkButton)sender).CommandArgument.ToString())));
            CarregaPrescricoesAlterar(long.Parse(ViewState["co_prontuario"].ToString()));

            #region DESCRIÇÃO DA PRESCRIÇÃO
            this.Panel_Prescricao.Visible = false;
            this.UpdatePanel_DescricaoPrescricao.Update();
            #endregion

            #region PROCEDIMENTOS
            this.NaoMostrarCadastroProcedimento();
            this.UpdatePanel_CadastrarProcedimento.Update();
            this.UpdatePanel_ProcedimentoCIDPrescricao.Update();
            this.UpdatePanel_BotaoAdicionarProcimentoPrescricao.Update();
            this.Panel_ProcedimentosPrescricao.Visible = false;
            this.UpdatePanel_ProcedimentoPrescricao.Update();
            #endregion

            #region PROCEDIMENTOS NÃO-FATURÁVEIS
            this.Panel_ProcedimentoNaoFaturavel.Visible = false;
            this.UpdatePanel_CadastrarProcedimentoNaoFaturavel.Update();
            this.Panel_ProcedimentosNaoFaturaveisPrescricao.Visible = false;
            this.UpdatePanel_ProcedimentoNaoFaturavelPrescricao.Update();
            #endregion

            #region MEDICAMENTOS
            this.Panel_IncluiMedicamento.Visible = false;
            this.UpdatePanel_CadastrarMedicamento.Update();
            this.Panel_MedicamentosPrescricao.Visible = false;
            this.UpdatePanel_MedicamentosPrescricao.Update();
            #endregion

            ViewState.Remove("co_prescricao_alterar");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Prescrição excluída com sucesso.');", true);
        }

        protected void OnClick_VerMedicamentosAprazados(object sender, EventArgs e)
        {
            this.Panel_VerTabelaAprazamento.Visible = true;
            this.LinkButton_PesquisarAprazamento.CommandArgument = ((LinkButton)sender).CommandArgument.ToString();
            this.TextBox_DataPesquisaAprazamento.Text = "";
            this.UpdatePanel_VerAprazados.Update();
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Pesquisar Aprazamento','../FormPesquisarAprazamento.aspx?co_prescricao=" + ((ImageButton)sender).CommandArgument.ToString() + "');", true);
        }

        protected void OnClick_FecharPesquisaAprazamento(object sender, EventArgs e)
        {
            this.Panel_VerTabelaAprazamento.Visible = false;
            this.UpdatePanel_VerAprazados.Update();
        }

        protected void OnClick_PesquisarAprazamento(object sender, EventArgs e)
        {
            DateTime datapesquisa = DateTime.Parse(this.TextBox_DataPesquisaAprazamento.Text);
            long co_prescricao = long.Parse(((LinkButton)sender).CommandArgument.ToString());

            Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioAprazados(co_prescricao, datapesquisa);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('FormImprimirCrystalReports.aspx?nomearquivo=aprazados.pdf');", true);
        }

        /// <summary>
        /// Carrega os itens da prescrição selecionada para uma possível alteração
        /// </summary>
        /// <param name="co_prescricao"></param>
        private void CarregaItensPrescritos(long co_prescricao)
        {
            CarregaProcedimentosNaoFaturaveisAlterar(co_prescricao);
            CarregaMedicamentosAlterar(co_prescricao);
            CarregaProcedimentosAlterar(co_prescricao);
        }

        /// <summary>
        /// Carrega os procedimentos não-faturáveis passíveis de alteração
        /// </summary>
        /// <param name="co_prescricao">código da prescrição</param>
        private void CarregaProcedimentosNaoFaturaveisAlterar(long co_prescricao)
        {
            IList<PrescricaoProcedimentoNaoFaturavel> procedimentos = Factory.GetInstance<IPrescricao>().BuscarProcedimentosNaoFaturaveis<PrescricaoProcedimentoNaoFaturavel>(co_prescricao);
            Session["procedimentosNaoFaturaveisPrescritos"] = procedimentos;
            GridView_ProcedimentoNaoFaturavelAlterar.DataSource = procedimentos;
            GridView_ProcedimentoNaoFaturavelAlterar.DataBind();
        }

        /// <summary>
        /// Carrega os procedimentos prescritos passíveis de alteração
        /// </summary>
        /// <param name="co_prescricao">código da prescrição</param>
        private void CarregaProcedimentosAlterar(long co_prescricao)
        {
            IList<PrescricaoProcedimento> procedimentos = Factory.GetInstance<IPrescricao>().BuscarProcedimentos<PrescricaoProcedimento>(co_prescricao);
            Session["procedimentosPrescritos"] = procedimentos;
            GridView_ProcedimentoAlterar.DataSource = procedimentos;
            GridView_ProcedimentoAlterar.DataBind();
        }

        /// <summary>
        /// Carrega os medicamentos prescritos passíveis de alteração
        /// </summary>
        /// <param name="co_prescricao">código da prescrição</param>
        private void CarregaMedicamentosAlterar(long co_prescricao)
        {
            IList<PrescricaoMedicamento> medicamentos = Factory.GetInstance<IPrescricao>().BuscarMedicamentos<PrescricaoMedicamento>(co_prescricao);
            Session["medicamentosPrescritos"] = medicamentos;
            GridView_MedicamentoAlterar.DataSource = medicamentos;
            GridView_MedicamentoAlterar.DataBind();
        }

        ///// <summary>
        ///// Paginação do gridview de medicamentos para alteração
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnPageIndexChanging_MedicamentoAlterar(object sender, GridViewPageEventArgs e)
        //{
        //    CarregaMedicamentosAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
        //    GridView_MedicamentoAlterar.PageIndex = e.NewPageIndex;
        //    GridView_MedicamentoAlterar.DataBind();
        //}

        ///// <summary>
        ///// Paginação do gridview de procedimentos não-faturáveis para alteração
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnPageIndexChanging_ProcedimentoNaoFaturavelAlterar(object sender, GridViewPageEventArgs e)
        //{
        //    CarregaProcedimentosNaoFaturaveisAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
        //    GridView_ProcedimentoNaoFaturavelAlterar.PageIndex = e.NewPageIndex;
        //    GridView_ProcedimentoNaoFaturavelAlterar.DataBind();
        //}

        ///// <summary>
        ///// Paginação do gridview de procedimentos para alteração
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnPageIndexChanging_ProcedimentoAlterar(object sender, GridViewPageEventArgs e)
        //{
        //    CarregaProcedimentosAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
        //    GridView_ProcedimentoAlterar.PageIndex = e.NewPageIndex;
        //    GridView_ProcedimentoAlterar.DataBind();
        //}

        protected void OnClick_ExcluirMedicamento(object sender, EventArgs e)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            int co_medicamento = int.Parse(((LinkButton)sender).CommandArgument.ToString());
            long co_prescricao = long.Parse(ViewState["co_prescricao_alterar"].ToString());
            PrescricaoMedicamento medicamento = iPrescricao.BuscarMedicamento<PrescricaoMedicamento>(co_prescricao, co_medicamento);
            Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];

            if (iPrescricao.VerificaPossibilidadeExcluirItemPrescricao(co_prescricao))
            {
                if (iPrescricao.VerificaPossibilidadeExcluirMedicamentoPrescricao(co_prescricao, co_medicamento))
                {
                    iPrescricao.ExcluirMedicamentoPrescricao<PrescricaoMedicamento>(medicamento);
                    iPrescricao.Inserir(new LogUrgencia(DateTime.Now, usuario.Codigo, 49, "id_prescricao=" + medicamento.Prescricao.Codigo + ", medicamento: " + medicamento.ObjetoMedicamento.Nome));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento/Prescrição excluído(a) com sucesso.');", true);
                    this.CarregaMedicamentosAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));

                    HelperView.ExecutarPlanoContingencia(usuario.Codigo, medicamento.Prescricao.Prontuario.Codigo);
                    //try
                    //{
                    //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, medicamento.Prescricao.Prontuario.Codigo); });
                    //}
                    //catch { }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, não é possível excluir o(a) medicamento/prescrição, pois o(a) mesmo(a) possui aprazamento(s) com o status de confirmado ou recusado pelo paciente.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, não é possível excluir este(a) medicamento/prescrição, pois a prescrição ficará sem item algum.');", true);
        }

        protected void OnClick_SuspenderAtivarMedicamento(object sender, EventArgs e)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            int co_medicamento = int.Parse(((LinkButton)sender).CommandArgument.ToString());
            PrescricaoMedicamento medicamento = iPrescricao.BuscarMedicamento<PrescricaoMedicamento>(long.Parse(ViewState["co_prescricao_alterar"].ToString()), co_medicamento);

            medicamento.Suspenso = !medicamento.Suspenso;
            if (medicamento.Suspenso)
            {
                Factory.GetInstance<IUrgenciaServiceFacade>().Atualizar(medicamento);
                //exclui os aprazamentos não executados deste item
                Factory.GetInstance<IAprazamento>().ExcluirAprazamentosNaoExecutadosMedicamento<AprazamentoMedicamento>(medicamento.Prescricao.Codigo, medicamento.Medicamento);
                Factory.GetInstance<IUrgenciaServiceFacade>().Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 24, "id_prescricao=" + medicamento.Prescricao.Codigo + " id_medicamento = " + medicamento.Medicamento));
            }
            else
            {
                Factory.GetInstance<IUrgenciaServiceFacade>().Atualizar(medicamento);
                Factory.GetInstance<IUrgenciaServiceFacade>().Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 25, "id_prescricao=" + medicamento.Prescricao.Codigo + " id_medicamento = " + medicamento.Medicamento));
            }

            CarregaMedicamentosAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
        }

        protected void OnClick_ExcluirProcedimento(object sender, EventArgs e)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            string co_procedimento = ((LinkButton)sender).CommandArgument.ToString();
            long co_prescricao = long.Parse(ViewState["co_prescricao_alterar"].ToString());
            PrescricaoProcedimento procedimento = iPrescricao.BuscarProcedimento<PrescricaoProcedimento>(co_prescricao, co_procedimento);
            Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];

            if (iPrescricao.VerificaPossibilidadeExcluirItemPrescricao(co_prescricao))
            {
                if (iPrescricao.VerificaPossibilidadeExcluirProcedimentoPrescricao(co_prescricao, co_procedimento))
                {
                    iPrescricao.ExcluirProcedimentoPrescricao<PrescricaoProcedimento>(procedimento);
                    iPrescricao.Inserir(new LogUrgencia(DateTime.Now, usuario.Codigo, 47, "id_prescricao=" + procedimento.Prescricao.Codigo + ", procedimento: " + procedimento.Procedimento.Nome));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento excluído com sucesso.');", true);
                    this.CarregaProcedimentosAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
                    HelperView.ExecutarPlanoContingencia(usuario.Codigo, procedimento.Prescricao.Prontuario.Codigo);
                    //try
                    //{
                    //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, procedimento.Prescricao.Prontuario.Codigo); });
                    //}
                    //catch { }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, não é possível excluir o procedimento, pois o mesmo possui aprazamento(s) com o status de confirmado.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, não é possível excluir este procedimento, pois a prescrição ficará sem item algum.');", true);
        }

        protected void OnClick_SuspenderAtivarProcedimento(object sender, EventArgs e)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            string co_procedimento = ((LinkButton)sender).CommandArgument.ToString();
            PrescricaoProcedimento procedimento = iPrescricao.BuscarProcedimento<PrescricaoProcedimento>(long.Parse(ViewState["co_prescricao_alterar"].ToString()), co_procedimento);

            procedimento.Suspenso = !procedimento.Suspenso;
            if (procedimento.Suspenso)
            {
                iPrescricao.Atualizar(procedimento);
                //exclui os aprazamentos não executados deste item
                Factory.GetInstance<IAprazamento>().ExcluirAprazamentosNaoExecutadosProcedimento<AprazamentoProcedimento>(procedimento.Prescricao.Codigo, procedimento.CodigoProcedimento);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 26, "id_prescricao=" + procedimento.Prescricao.Codigo + " co_procedimento = " + procedimento.CodigoProcedimento));
            }
            else
            {
                iPrescricao.Atualizar(procedimento);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 27, "id_prescricao=" + procedimento.Prescricao.Codigo + " co_procedimento = " + procedimento.CodigoProcedimento));
            }

            CarregaProcedimentosAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
        }

        protected void OnClick_SuspenderAtivarProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            int co_procedimento = int.Parse(((LinkButton)sender).CommandArgument.ToString());
            PrescricaoProcedimentoNaoFaturavel procedimentonaofaturavel = iPrescricao.BuscarProcedimentoNaoFaturavel<PrescricaoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao_alterar"].ToString()), co_procedimento);

            procedimentonaofaturavel.Suspenso = !procedimentonaofaturavel.Suspenso;
            if (procedimentonaofaturavel.Suspenso)
            {
                iPrescricao.Atualizar(procedimentonaofaturavel);
                //exclui os aprazamentos não executados deste item
                Factory.GetInstance<IAprazamento>().ExcluirAprazamentosNaoExecutadosProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(procedimentonaofaturavel.Prescricao.Codigo, procedimentonaofaturavel.CodigoProcedimento);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 19, "id_prescricao=" + procedimentonaofaturavel.Prescricao.Codigo + " id_procedimento = " + procedimentonaofaturavel.CodigoProcedimento));
            }
            else
            {
                iPrescricao.Atualizar(procedimentonaofaturavel);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 20, "id_prescricao=" + procedimentonaofaturavel.Prescricao.Codigo + " id_procedimento = " + procedimentonaofaturavel.CodigoProcedimento));
            }

            CarregaProcedimentosNaoFaturaveisAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
        }

        protected void OnClick_ExcluirProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            int co_procedimento = int.Parse(((LinkButton)sender).CommandArgument.ToString());
            long co_prescricao = long.Parse(ViewState["co_prescricao_alterar"].ToString());
            PrescricaoProcedimentoNaoFaturavel procedimentonaofaturavel = iPrescricao.BuscarProcedimentoNaoFaturavel<PrescricaoProcedimentoNaoFaturavel>(co_prescricao, co_procedimento);
            Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];

            if (iPrescricao.VerificaPossibilidadeExcluirItemPrescricao(co_prescricao))
            {
                if (iPrescricao.VerificaPossibilidadeExcluirProcedimentoNaoFaturavelPrescricao(co_prescricao, co_procedimento))
                {
                    iPrescricao.ExcluirProcedimentoNaoFaturavelPrescricao<PrescricaoProcedimentoNaoFaturavel>(procedimentonaofaturavel);
                    iPrescricao.Inserir(new LogUrgencia(DateTime.Now, usuario.Codigo, 48, "id_prescricao=" + procedimentonaofaturavel.Prescricao.Codigo + ", procedimento: " + procedimentonaofaturavel.Procedimento.Nome));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento excluído com sucesso.');", true);
                    this.CarregaProcedimentosNaoFaturaveisAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
                    HelperView.ExecutarPlanoContingencia(usuario.Codigo, procedimentonaofaturavel.Prescricao.Prontuario.Codigo);
                    //try
                    //{
                    //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, procedimentonaofaturavel.Prescricao.Prontuario.Codigo); });
                    //}
                    //catch { }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, não é possível excluir o procedimento, pois o mesmo possui aprazamento(s) com o status de confirmado.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, não é possível excluir este procedimento, pois a prescrição ficará sem item algum.');", true);
        }

        protected void OnSelectedIndexChanged_RetiraCids(object sender, EventArgs e)
        {
            this.DropDownList_CidAlterarProcedimento.Items.Clear();
            this.DropDownList_CidAlterarProcedimento.Items.Add(new ListItem("Selecione...", "-1"));
        }

        /// <summary>
        /// Adiciona um novo medicamento para a prescrição escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarMedicamentoAlterar(object sender, EventArgs e)
        {
            if (ViewState["co_prescricao_alterar"] == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione uma prescrição para executar esta operação!');", true);
                return;
            }

            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();

            ViverMais.Model.Prescricao prescricao = iPrescricao.BuscarPorCodigo<ViverMais.Model.Prescricao>(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
            Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];

            int co_medicamento = int.Parse(DropDownList_MedicamentoAlterar.SelectedValue);

            if (((IList<PrescricaoMedicamento>)Session["medicamentosPrescritos"]).Where(p => p.Medicamento == co_medicamento).FirstOrDefault() == null)
            {
                //try
                //{
                PrescricaoMedicamento medicamento = new PrescricaoMedicamento();
                medicamento.Prescricao = prescricao;
                medicamento.ObjetoMedicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(int.Parse(DropDownList_MedicamentoAlterar.SelectedValue));
                medicamento.Medicamento = medicamento.ObjetoMedicamento.Codigo;
                medicamento.SetIntervalo(TextBox_IntervaloMedicamentoAlterar.Text, int.Parse(DropDownList_UnidadeTempoFrequenciaMedicamentoAlterar.SelectedValue));
                medicamento.Observacao = TextBox_ObservacaoMedicamentoAlterar.Text;

                UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(usuario.Codigo);
                medicamento.CodigoProfissional = usuarioprofissional.Id_Profissional;
                medicamento.CBOProfissional = usuarioprofissional.Id_Profissional;
                medicamento.Data = DateTime.Now;
                medicamento.Suspenso = false;

                if (DropDownList_ViaAdministracaoMedicamentoAlterar.SelectedValue != "-1")
                    medicamento.ViaAdministracao = iUrgencia.BuscarPorCodigo<ViaAdministracao>(int.Parse(DropDownList_ViaAdministracaoMedicamentoAlterar.SelectedValue));

                if (medicamento.IntervaloValido())
                {
                    iPrescricao.Inserir(medicamento);
                    iPrescricao.Inserir(new LogUrgencia(DateTime.Now, usuario.Codigo, 38, "id_prescricao=" + medicamento.Prescricao.Codigo + " id_medicamento = " + medicamento.Medicamento));

                    DropDownList_MedicamentoAlterar.SelectedValue = "0";
                    //DropDownList_MedicamentoAlterar.Items.Clear();
                    //DropDownList_MedicamentoAlterar.Items.Add(new ListItem("Selecione...", "0"));
                    this.TextBox_IntervaloMedicamentoAlterar.Text = "";
                    this.TextBox_IntervaloMedicamentoAlterar.Enabled = true;
                    this.TextBox_ObservacaoMedicamentoAlterar.Text = "";
                    this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloMedicamentoAlterar, this.RequiredFieldValidator_PrescricaoFrequenciaMedicamento,
                this.RegularExpressionValidator_PrescricaoFrequenciaMedicamento, this.CompareValidator_PrescricaoFrequenciaMedicamento,
                true);

                    this.DropDownList_ViaAdministracaoMedicamentoAlterar.SelectedValue = "-1";
                    this.DropDownList_UnidadeTempoFrequenciaMedicamentoAlterar.SelectedValue = ((int)PrescricaoMedicamento.UNIDADETEMPO.HORAS).ToString();
                    CarregaMedicamentosAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
                    HelperView.ExecutarPlanoContingencia(usuario.Codigo, prescricao.Prontuario.Codigo);
                    //try
                    //{
                    //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, prescricao.Prontuario.Codigo); });
                    //}
                    //catch { }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O intervalo máximo para aplicação do(a) medicamento/prescrição é de 24 horas.');", true);
                //}
                //catch (Exception f)
                //{
                //    throw f;
                //}
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O(A) medicamento/prescrição já se encontra registrado na prescrição escolhida! Por favor, escolha outro(a) medicamento/prescrição.');", true);
        }

        /// <summary>
        /// Adiciona um novo procedimento para a prescrição válida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarProcedimentoAlterar(object sender, EventArgs e)
        {
            if (ViewState["co_prescricao_alterar"] == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione uma prescrição para executar esta operação!');", true);
                return;
            }

            //try
            //{
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            ViverMais.Model.Prescricao prescricao = iPrescricao.BuscarPorCodigo<ViverMais.Model.Prescricao>(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
            Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
            string co_procedimento = DropDownList_ProcedimentoAlterar.SelectedValue;

            if (((IList<PrescricaoProcedimento>)Session["procedimentosPrescritos"]).Where(p => p.CodigoProcedimento == co_procedimento).FirstOrDefault() == null)
            {
                IRegistro iRegistro = Factory.GetInstance<IRegistro>();
                ICid iCid = Factory.GetInstance<ICid>();
                bool exigenciaCid = iRegistro.ProcedimentoExigeCid(DropDownList_ProcedimentoAlterar.SelectedValue);

                if (exigenciaCid && DropDownList_CidAlterarProcedimento.SelectedValue == "-1")
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este procedimento exige a escolha de um CID.')", true);
                    return;
                }

                PrescricaoProcedimento proc = new PrescricaoProcedimento();
                proc.Prescricao = prescricao;
                proc.CodigoProcedimento = DropDownList_ProcedimentoAlterar.SelectedValue;
                proc.SetIntervalo(TextBox_IntervaloProcedimentoAlterar.Text, int.Parse(DropDownList_UnidadeTempoFrequenciaProcedimentoAlterar.SelectedValue));
                proc.Data = DateTime.Now;

                UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                proc.CodigoProfissional = usuarioprofissional.Id_Profissional;
                proc.CBOProfissional = usuarioprofissional.CodigoCBO;
                proc.Suspenso = false;
                proc.CodigoCid = DropDownList_CidAlterarProcedimento.SelectedValue;
                proc.Cid = iCid.BuscarPorCodigo<Cid>(DropDownList_CidAlterarProcedimento.SelectedValue);

                if (proc.IntervaloValido())
                {
                    iPrescricao.Inserir(proc);
                    iPrescricao.Inserir(new LogUrgencia(DateTime.Now, usuario.Codigo, 39, "id_prescricao=" + proc.Prescricao.Codigo + " co_procedimento = " + proc.CodigoProcedimento));

                    this.WUC_PrescricaoProcedimentoCID.WUC_DropDownListGrupoCID.SelectedValue = "-1";
                    this.WUC_PrescricaoProcedimentoCID.WUC_UpdatePanelPesquisarCID.Update();

                    this.OnSelectedIndexChanged_RetiraCids(new object(), new EventArgs());
                    this.DropDownList_ProcedimentoAlterar.SelectedValue = "-1";
                    this.TextBox_IntervaloProcedimentoAlterar.Text = "";
                    this.TextBox_IntervaloProcedimentoAlterar.Enabled = true;
                    this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoAlterar, this.RequiredFieldValidator_PrescricaoFrequenciaProcedimento,
                this.RegularExpressionValidator_PrescricaoFrequenciaProcedimento, this.CompareValidator_PrescricaoFrequenciaProcedimento,
                true);

                    this.DropDownList_UnidadeTempoFrequenciaProcedimentoAlterar.SelectedValue = ((int)PrescricaoProcedimento.UNIDADETEMPO.HORAS).ToString();
                    CarregaProcedimentosAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
                    HelperView.ExecutarPlanoContingencia(usuario.Codigo, prescricao.Prontuario.Codigo);
                    //try
                    //{
                    //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, prescricao.Prontuario.Codigo); });
                    //}
                    //catch { }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O intervalo máximo para aplicação do procedimento é de 24 horas.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O procedimento já se encontra registrado na prescrição escolhida! Por favor, escolha outro procedimento.');", true);
            //}
            //catch (Exception f)
            //{
            //    throw f;
            //}
        }

        /// <summary>
        /// Adiciona um novo procedimento não-faturável para a prescrição válida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarProcedimentoNaoFaturavelAlterar(object sender, EventArgs e)
        {
            //try
            //{
            if (ViewState["co_prescricao_alterar"] == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione uma prescrição para executar esta operação!');", true);
                return;
            }

            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            IList<PrescricaoProcedimentoNaoFaturavel> lista = iPrescricao.BuscarProcedimentosNaoFaturaveis<PrescricaoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
            ViverMais.Model.Prescricao prescricao = iPrescricao.BuscarPorCodigo<ViverMais.Model.Prescricao>(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
            int co_procedimento = int.Parse(DropDownList_ProcedimentoNaoFaturavelCadastrar.SelectedValue);
            Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];

            if (((IList<PrescricaoProcedimentoNaoFaturavel>)Session["procedimentosNaoFaturaveisPrescritos"]).Where(p => p.CodigoProcedimento == co_procedimento).FirstOrDefault() == null)
            {
                PrescricaoProcedimentoNaoFaturavel proc = new PrescricaoProcedimentoNaoFaturavel();
                proc.Prescricao = prescricao;
                proc.Data = DateTime.Now;
                proc.Procedimento = iPrescricao.BuscarPorCodigo<ProcedimentoNaoFaturavel>(int.Parse(DropDownList_ProcedimentoNaoFaturavelCadastrar.SelectedValue));
                proc.SetIntervalo(TextBox_IntervaloProcedimentoNaoFaturavelAlterar.Text, int.Parse(DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavelAlterar.SelectedValue));

                UsuarioProfissionalUrgence upu = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                proc.CodigoProfissional = upu.Id_Profissional; proc.CBOProfissional = upu.CodigoCBO;
                proc.Suspenso = false;

                if (proc.IntervaloValido())
                {
                    iPrescricao.Inserir(proc);
                    iPrescricao.Inserir(new LogUrgencia(DateTime.Now, usuario.Codigo, 40, "id_prescricao=" + proc.Prescricao.Codigo + " id_procedimento = " + proc.Procedimento.Codigo));

                    this.DropDownList_ProcedimentoNaoFaturavelCadastrar.SelectedValue = "-1";
                    this.TextBox_IntervaloProcedimentoNaoFaturavelAlterar.Text = "";
                    this.TextBox_IntervaloProcedimentoNaoFaturavelAlterar.Enabled = true;
                    this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoNaoFaturavelAlterar, this.RequiredFieldValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel,
               this.RegularExpressionValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel, this.CompareValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel,
               true);
                    this.DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavelAlterar.SelectedValue = ((int)PrescricaoProcedimentoNaoFaturavel.UNIDADETEMPO.HORAS).ToString();
                    CarregaProcedimentosNaoFaturaveisAlterar(long.Parse(ViewState["co_prescricao_alterar"].ToString()));
                    HelperView.ExecutarPlanoContingencia(usuario.Codigo, prescricao.Prontuario.Codigo);
                    //try
                    //{
                    //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, prescricao.Prontuario.Codigo); });
                    //}
                    //catch { }

                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O intervalo máximo para aplicação do procedimento é de 24 horas.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O procedimento já se encontra registrado na prescrição escolhida! Por favor, escolha outro procedimento.');", true);


            //}
            //catch (Exception f)
            //{
            //    throw f;
            //}
        }

        /// <summary>
        /// Formata o gridview de medicamentos da prescrição que foi alterada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewMedicamentoAlterar(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                int co_medicamento = int.Parse(GridView_MedicamentoAlterar.DataKeys[e.Row.RowIndex]["Medicamento"].ToString());

                PrescricaoMedicamento prescricaomedicamento = ((IList<PrescricaoMedicamento>)Session["medicamentosPrescritos"]).Where(p => p.Medicamento == co_medicamento).First();
                //iPrescricao.BuscarMedicamentos<PrescricaoMedicamento>(long.Parse(ViewState["co_prescricao_alterar"].ToString())).Where(p => p.Medicamento == int.Parse(GridView_MedicamentoAlterar.DataKeys[e.Row.RowIndex]["Medicamento"].ToString())).First();

                if (prescricaomedicamento.Suspenso)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).Text = "Ativar";
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).OnClientClick = "javascript:return confirm('Usuário, tem certeza que deseja ativar a utilização deste(a) medicamento/prescrição ?');";
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.White;
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).Text = "Suspender";
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).OnClientClick = "javascript:return confirm('Usuário, ao suspender este(a) medicamento/prescrição, os aprazamentos que ainda não foram confirmados serão excluídos. Deseja realmente continuar?');";
                }

                if (prescricaomedicamento.Prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                {
                    GridView_MedicamentoAlterar.Columns[GridView_MedicamentoAlterar.Columns.Count - 1].Visible = false;
                    GridView_MedicamentoAlterar.Columns[GridView_MedicamentoAlterar.Columns.Count - 2].Visible = false;
                }
                else
                {
                    if (!VerificaFuncionalidadesMedico())
                    {
                        GridView_MedicamentoAlterar.Columns[GridView_MedicamentoAlterar.Columns.Count - 1].Visible = false;
                        GridView_MedicamentoAlterar.Columns[GridView_MedicamentoAlterar.Columns.Count - 2].Visible = false;
                    }
                    else
                    {
                        GridView_MedicamentoAlterar.Columns[GridView_MedicamentoAlterar.Columns.Count - 1].Visible = true;
                        GridView_MedicamentoAlterar.Columns[GridView_MedicamentoAlterar.Columns.Count - 2].Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Formata o gridview de procedimentos da prescrição que foi alterada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewProcedimentoAlterar(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                string co_procedimento = GridView_ProcedimentoAlterar.DataKeys[e.Row.RowIndex]["CodigoProcedimento"].ToString();

                PrescricaoProcedimento prescricaoprocedimento = ((IList<PrescricaoProcedimento>)Session["procedimentosPrescritos"]).Where(p => p.CodigoProcedimento == co_procedimento).First();
                //iPrescricao.BuscarProcedimento<PrescricaoProcedimento>(long.Parse(ViewState["co_prescricao_alterar"].ToString()), GridView_ProcedimentoAlterar.DataKeys[e.Row.RowIndex]["CodigoProcedimento"].ToString());

                if (prescricaoprocedimento.Suspenso)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).Text = "Ativar";
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).OnClientClick = "javascript:return confirm('Usuário, tem certeza que deseja ativar a utilização deste procedimento ?');";
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.White;
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).Text = "Suspender";
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).OnClientClick = "javascript:return confirm('Usuário, ao suspender este procedimento, os aprazamentos que ainda não foram confirmados serão excluídos. Deseja realmente continuar?');";
                }

                if (prescricaoprocedimento.Prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                {
                    GridView_ProcedimentoAlterar.Columns[GridView_ProcedimentoAlterar.Columns.Count - 1].Visible = false;
                    GridView_ProcedimentoAlterar.Columns[GridView_ProcedimentoAlterar.Columns.Count - 2].Visible = false;
                }
                else
                {
                    if (!VerificaFuncionalidadesMedico())
                    {
                        GridView_ProcedimentoAlterar.Columns[GridView_ProcedimentoAlterar.Columns.Count - 1].Visible = false;
                        GridView_ProcedimentoAlterar.Columns[GridView_ProcedimentoAlterar.Columns.Count - 2].Visible = false;
                    }
                    else
                    {
                        GridView_ProcedimentoAlterar.Columns[GridView_ProcedimentoAlterar.Columns.Count - 1].Visible = true;
                        GridView_ProcedimentoAlterar.Columns[GridView_ProcedimentoAlterar.Columns.Count - 2].Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Formata o gridview de procedimentos não faturáveis da prescrição que foi alterada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewProcedimentoNaoFaturavelAlterar(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                int co_procedimento = int.Parse(GridView_ProcedimentoNaoFaturavelAlterar.DataKeys[e.Row.RowIndex]["CodigoProcedimento"].ToString());
                PrescricaoProcedimentoNaoFaturavel prescricaoprocedimento = ((IList<PrescricaoProcedimentoNaoFaturavel>)Session["procedimentosNaoFaturaveisPrescritos"]).Where(p => p.CodigoProcedimento == co_procedimento).First();
                //iPrescricao.BuscarProcedimentosNaoFaturaveis<PrescricaoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao_alterar"].ToString())).Where(p => p.CodigoProcedimento == int.Parse(GridView_ProcedimentoNaoFaturavelAlterar.DataKeys[e.Row.RowIndex]["CodigoProcedimento"].ToString())).First();

                if (prescricaoprocedimento.Suspenso)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).Text = "Ativar";
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).OnClientClick = "javascript:return confirm('Usuário, tem certeza que deseja ativar a utilização deste procedimento ?');";
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.White;
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).Text = "Suspender";
                    ((LinkButton)e.Row.FindControl("LinkButton_Suspender")).OnClientClick = "javascript:return confirm('Usuário, ao suspender este procedimento, os aprazamentos que ainda não foram confirmados serão excluídos. Deseja realmente continuar?');";
                }

                if (prescricaoprocedimento.Prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                {
                    GridView_ProcedimentoNaoFaturavelAlterar.Columns[GridView_ProcedimentoNaoFaturavelAlterar.Columns.Count - 1].Visible = false;
                    GridView_ProcedimentoNaoFaturavelAlterar.Columns[GridView_ProcedimentoNaoFaturavelAlterar.Columns.Count - 2].Visible = false;
                }
                else
                {
                    GridView_ProcedimentoNaoFaturavelAlterar.Columns[GridView_ProcedimentoNaoFaturavelAlterar.Columns.Count - 1].Visible = true;
                    GridView_ProcedimentoNaoFaturavelAlterar.Columns[GridView_ProcedimentoNaoFaturavelAlterar.Columns.Count - 2].Visible = true;
                }
            }
        }
        #endregion

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

        protected void OnClick_BuscarProcedimentoNaoFaturavelAlterar(object sender, EventArgs e)
        {
            DropDownList_ProcedimentoNaoFaturavelCadastrar.DataValueField = "Codigo";
            DropDownList_ProcedimentoNaoFaturavelCadastrar.DataTextField = "Nome";

            IList<ProcedimentoNaoFaturavel> procedimentos = Factory.GetInstance<IProcedimentoNaoFaturavel>().BuscarPorNome<ProcedimentoNaoFaturavel>(TextBox_BuscarProcedimentoAlterar.Text);
            DropDownList_ProcedimentoNaoFaturavelCadastrar.DataSource = procedimentos;
            DropDownList_ProcedimentoNaoFaturavelCadastrar.DataBind();

            DropDownList_ProcedimentoNaoFaturavelCadastrar.Items.Insert(0, new ListItem("Selecione...", "-1"));
            DropDownList_ProcedimentoNaoFaturavelCadastrar.Focus();
        }

        protected void OnClick_BuscarMedicamentoAlterar(object sender, EventArgs e)
        {
            DropDownList_MedicamentoAlterar.DataValueField = "Codigo";
            DropDownList_MedicamentoAlterar.DataTextField = "Nome";

            DropDownList_MedicamentoAlterar.DataSource = Factory.GetInstance<IMedicamento>().BuscarPorDescricao<Medicamento>(-1, TextBox_BuscarMedicamentoAlterar.Text, false);
            DropDownList_MedicamentoAlterar.DataBind();

            DropDownList_MedicamentoAlterar.Items.Insert(0, new ListItem("Selecione...", "0"));
            DropDownList_MedicamentoAlterar.Focus();
        }

        protected void OnClick_BuscarProcedimentoSIGTAP(object sender, EventArgs e)
        {
            DropDownList_ProcedimentoAlterar.DataTextField = "Nome";
            DropDownList_ProcedimentoAlterar.DataValueField = "Codigo";

            IList<Procedimento> procedimentos = Factory.GetInstance<IProcedimento>().BuscarPorNome<Procedimento>(TextBox_BuscarProcedimentoSIGTAPAlterar.Text.ToUpper());
            DropDownList_ProcedimentoAlterar.DataSource = procedimentos;
            DropDownList_ProcedimentoAlterar.DataBind();

            this.InserirElementoDefault(this.DropDownList_ProcedimentoAlterar, -1);
            DropDownList_ProcedimentoAlterar.Focus();
        }

        protected void OnClick_BuscarProcimentoCID(object sender, ImageClickEventArgs e)
        {
            if (this.DropDownList_ProcedimentoAlterar.SelectedValue != "-1")
            {
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IList<Cid> cids = iPrescricao.BuscarCidsProcedimentoPorCodigo<Cid>(DropDownList_ProcedimentoAlterar.SelectedValue, this.WUC_PrescricaoProcedimentoCID.WUC_TextBoxCodigoCID.Text);

                DropDownList_CidAlterarProcedimento.DataSource = cids;
                DropDownList_CidAlterarProcedimento.DataBind();
                this.InserirElementoDefault(this.DropDownList_CidAlterarProcedimento, -1);
            }
        }

        protected void OnSelectedIndexChanged_ProcecimentoCid(object sender, EventArgs e)
        {
            if (this.DropDownList_ProcedimentoAlterar.SelectedValue != "-1")
            {
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IList<Cid> cids = iPrescricao.BuscarCidsProcedimentoPorGrupo<Cid>(DropDownList_ProcedimentoAlterar.SelectedValue, this.WUC_PrescricaoProcedimentoCID.WUC_DropDownListGrupoCID.SelectedValue);

                DropDownList_CidAlterarProcedimento.DataSource = cids;
                DropDownList_CidAlterarProcedimento.DataBind();
                this.InserirElementoDefault(this.DropDownList_CidAlterarProcedimento, -1);
            }
        }

        protected void OnClickBuscarProcedimentoCIDPorNome(object sender, ImageClickEventArgs e)
        {
            if (this.DropDownList_ProcedimentoAlterar.SelectedValue != "-1")
            {
                this.DropDownList_CidAlterarProcedimento.DataSource = Factory.GetInstance<ICid>().BuscarPorInicioNome<Cid>(DropDownList_ProcedimentoAlterar.SelectedValue, this.WUC_PrescricaoProcedimentoCID.WUC_TextBoxNomeCID.Text);
                this.DropDownList_CidAlterarProcedimento.DataBind();
                this.InserirElementoDefault(this.DropDownList_CidAlterarProcedimento, -1);
            }
        }

        void InserirElementoDefault(DropDownList dropdown, int valor)
        {
            dropdown.Items.Insert(0, new ListItem("Selecione...", valor.ToString()));
        }

        protected void OnSelectedIndexChanged_FrequenciaProcedimento(object sender, EventArgs e)
        {
            if (int.Parse(this.DropDownList_UnidadeTempoFrequenciaProcedimentoAlterar.SelectedValue) == (int)PrescricaoProcedimento.UNIDADETEMPO.UNICA)
            {
                this.TextBox_IntervaloProcedimentoAlterar.Text = "";
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoAlterar, this.RequiredFieldValidator_PrescricaoFrequenciaProcedimento,
                    this.RegularExpressionValidator_PrescricaoFrequenciaProcedimento, this.CompareValidator_PrescricaoFrequenciaProcedimento,
                    false);
            }
            else
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoAlterar, this.RequiredFieldValidator_PrescricaoFrequenciaProcedimento,
                    this.RegularExpressionValidator_PrescricaoFrequenciaProcedimento, this.CompareValidator_PrescricaoFrequenciaProcedimento,
                    true);
        }

        protected void OnSelectedIndexChanged_FrequenciaProcedimentoNaoFaturavel(object sender, EventArgs e)
        {
            if (int.Parse(this.DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavelAlterar.SelectedValue) == (int)PrescricaoProcedimentoNaoFaturavel.UNIDADETEMPO.UNICA)
            {
                this.TextBox_IntervaloProcedimentoNaoFaturavelAlterar.Text = "";
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoNaoFaturavelAlterar, this.RequiredFieldValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel,
                    this.RegularExpressionValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel, this.CompareValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel,
                    false);
            }
            else
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloProcedimentoNaoFaturavelAlterar, this.RequiredFieldValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel,
                    this.RegularExpressionValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel, this.CompareValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel,
                    true);
        }

        protected void OnSelectedIndexChanged_FrequenciaMedicamento(object sender, EventArgs e)
        {
            if (int.Parse(this.DropDownList_UnidadeTempoFrequenciaMedicamentoAlterar.SelectedValue) == (int)PrescricaoMedicamento.UNIDADETEMPO.UNICA)
            {
                this.TextBox_IntervaloMedicamentoAlterar.Text = "";
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloMedicamentoAlterar, this.RequiredFieldValidator_PrescricaoFrequenciaMedicamento,
                    this.RegularExpressionValidator_PrescricaoFrequenciaMedicamento, this.CompareValidator_PrescricaoFrequenciaMedicamento,
                    false);
            }
            else
                this.HabilitaDesabilitaFrequencia(this.TextBox_IntervaloMedicamentoAlterar, this.RequiredFieldValidator_PrescricaoFrequenciaMedicamento,
                    this.RegularExpressionValidator_PrescricaoFrequenciaMedicamento, this.CompareValidator_PrescricaoFrequenciaMedicamento,
                    true);
        }

        private void HabilitaDesabilitaFrequencia(TextBox intervalo, RequiredFieldValidator frequencia, RegularExpressionValidator aceitarsomentenumeros,
    CompareValidator valormaiorquezero, bool habilitarCampos)
        {
            intervalo.Enabled = habilitarCampos;
            frequencia.Enabled = habilitarCampos;
            aceitarsomentenumeros.Enabled = habilitarCampos;
            valormaiorquezero.Enabled = habilitarCampos;
        }
    }
}