﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using System.Collections;
using System.Threading;

namespace ViverMais.View.Urgencia
{
    public partial class FormAprazamento : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "APRAZAR", Modulo.URGENCIA))
                {
                    IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                    ViverMais.Model.UsuarioProfissionalUrgence usuarioprofissional = iPrescricao.BuscarPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);

                    if (usuarioprofissional != null)
                    {
                        if (usuarioprofissional.UnidadeVinculo == ((Usuario)Session["Usuario"]).Unidade.CNES)
                        {
                            ICBO iCbo = Factory.GetInstance<ICBO>();
                            CBO cbo = iCbo.BuscarPorCodigo<CBO>(usuarioprofissional.CodigoCBO);

                            if (iCbo.CBOPertenceMedico<CBO>(cbo) || iCbo.CBOPertenceEnfermeiro<CBO>(cbo))
                            {
                                long temp;

                                if (Request["co_prescricao"] != null && long.TryParse(Request["co_prescricao"].ToString(), out temp))
                                {
                                    Session.Remove("medicamentosPrescristos");
                                    Session.Remove("procedimentosPrescristos");
                                    Session.Remove("procedimentosNaoFaturaveisPrescristos");

                                    ViewState["co_prescricao"] = Request["co_prescricao"].ToString();
                                    Prescricao prescricao = iPrescricao.BuscarPorCodigo<Prescricao>(long.Parse(Request["co_prescricao"].ToString()));
                                    Label_Data.Text = prescricao.Data.ToString("dd/MM/yyyy HH:mm:ss");
                                    Label_Status.Text = prescricao.DescricaoStatus;
                                    Label_UltimaDataValida.Text = prescricao.UltimaDataValida.ToString("dd/MM/yyyy HH:mm");

                                    if (prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                                    {
                                        Panel_RegistrarAprazamentoProcedimentoNaoFaturavel.Visible = false;
                                        Panel_RegistrarAprazamentoMedicamento.Visible = false;
                                        Panel_AprazarProcedimento.Visible = false;
                                    }

                                    iPrescricao.AtualizarStatusPrescricoesProntuario(prescricao.Prontuario.Codigo);
                                    iPrescricao.AtualizarStatusItensAprazadosPrescricao<Prescricao>(prescricao, true, true, true);

                                    CarregaItensAprazar(prescricao.Codigo);

                                    CarregaMedicamentosAprazados(prescricao.Codigo);
                                    CarregaProcedimentosNaoFaturavelAprazados(prescricao.Codigo);
                                    CarregaProcedimentosAprazados(prescricao.Codigo);

                                    OnClick_CancelarMedicamentoAprazamento(sender, e);
                                    OnClick_CancelarProcedimentoNaoFaturavelAprazamento(sender, e);
                                    OnClick_CancelarProcedimentoAprazamento(sender, e);
                                    this.AplicarLegendaDropDownList(this.DropDownList_ProcedimentoAprazar);
                                    this.AplicarLegendaDropDownList(this.DropDownList_ProcedimentoNaoFaturavelAprazar);
                                    this.AplicarLegendaDropDownList(this.DropDownList_MedicamentoAprazar);
                                }
                            }
                            else
                                Response.Redirect("FormAcessoNegado.aspx?opcao=6");
                            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, somente profissionais médicos e de enfermagem tem acesso a esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
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

        private void AplicarLegendaDropDownList(DropDownList dropdown)
        {
            dropdown.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        protected void OnClick_VoltarPagina(object sender, EventArgs e)
        {
            if (Session["URL_UrgenciaVoltarAprazamento"] != null)
                Response.Redirect(Session["URL_UrgenciaVoltarAprazamento"].ToString());
            else
                Response.Redirect("Default.aspx");
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

        #region APRAZAMENTO
        /// <summary>
        /// Carrega os medicamentos já aprazados para a prescrição selecionada
        /// </summary>
        /// <param name="co_prescriccao">código da prescrição</param>
        private void CarregaMedicamentosAprazados(long co_prescricao)
        {
            IList<AprazamentoMedicamento> medicamentos = Factory.GetInstance<IAprazamento>().BuscarAprazamentoMedicamento<AprazamentoMedicamento>(co_prescricao);
            Session["MedicamentosAprazados"] = medicamentos;

            GridView_MedicamentoAprazado.DataSource = medicamentos;
            GridView_MedicamentoAprazado.DataBind();
        }

        /// <summary>
        /// Carrega os procedimentos não faturáveis já aprazados para a prescrição selecionada
        /// </summary>
        /// <param name="co_prescricao"></param>
        private void CarregaProcedimentosNaoFaturavelAprazados(long co_prescricao)
        {
            IList<AprazamentoProcedimentoNaoFaturavel> procedimentos = Factory.GetInstance<IAprazamento>().BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(co_prescricao);
            Session["ProcedimentosNaoFaturaveisAprazados"] = procedimentos;

            GridView_ProcedimentoNaoFaturavelAprazado.DataSource = procedimentos;
            GridView_ProcedimentoNaoFaturavelAprazado.DataBind();
        }

        /// <summary>
        /// Carrega os procedimentos já aprazados para a prescrição selecionada
        /// </summary>
        /// <param name="co_prescricao"></param>
        private void CarregaProcedimentosAprazados(long co_prescricao)
        {
            IList<AprazamentoProcedimento> procedimentos = Factory.GetInstance<IAprazamento>().BuscarAprazamentoProcedimento<AprazamentoProcedimento>(co_prescricao);
            Session["ProcedimentosAprazados"] = procedimentos;

            GridView_ProcedimentosAprazados.DataSource = procedimentos;
            GridView_ProcedimentosAprazados.DataBind();
        }

        /// <summary>
        /// Carrega os itens da prescrição selecionada para aprazamento
        /// </summary>
        /// <param name="co_prescricao"></param>
        public void CarregaItensAprazar(long co_prescricao)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();

            IList<PrescricaoMedicamento> medicamentos = iPrescricao.BuscarMedicamentos<PrescricaoMedicamento>(co_prescricao);
            Session["medicamentosPrescritos"] = medicamentos;

            GridView_MedicamentoAprazar.DataSource = medicamentos;
            GridView_MedicamentoAprazar.DataBind();

            IList<PrescricaoProcedimento> procedimentos = iPrescricao.BuscarProcedimentos<PrescricaoProcedimento>(co_prescricao);
            Session["procedimentosPrescritos"] = procedimentos;

            GridView_ProcedimentoAprazar.DataSource = procedimentos;
            GridView_ProcedimentoAprazar.DataBind();

            IList<PrescricaoProcedimentoNaoFaturavel> procedimentosNaoFaturaveis = iPrescricao.BuscarProcedimentosNaoFaturaveis<PrescricaoProcedimentoNaoFaturavel>(co_prescricao);
            Session["procedimentosNaoFaturaveisPrescritos"] = procedimentosNaoFaturaveis;

            GridView_ProcedimentoNaoFaturavelAprazar.DataSource = procedimentosNaoFaturaveis;
            GridView_ProcedimentoNaoFaturavelAprazar.DataBind();
        }

        protected void OnSelectedIndexChanging_Medicamentos(object sender, GridViewSelectEventArgs e)
        {
            int co_medicamento = int.Parse(GridView_MedicamentoAprazar.DataKeys[e.NewSelectedIndex]["Medicamento"].ToString());

            //IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            //IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

            PrescricaoMedicamento prescricaomedicamento = ((IList<PrescricaoMedicamento>)Session["medicamentosPrescritos"]).Where(p => p.Medicamento == co_medicamento).First();
            //iPrescricao.BuscarMedicamento<PrescricaoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString()), co_medicamento);

            if (prescricaomedicamento.AplicacaoUnica)
            {
                AprazamentoMedicamento medicamento = ((IList<AprazamentoMedicamento>)Session["MedicamentosAprazados"]).Where(p => p.CodigoMedicamento == co_medicamento).FirstOrDefault();
                //iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString()), co_medicamento);

                if (medicamento != null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, segundo as informações da prescrição, este(a) medicamento/prescrição deve ser executado(a) uma única vez e o(a) mesmo(a) já se encontra no quadro de aprazamento. Por favor, escolha outro medicamento/prescrição.');", true);
                    return;
                }
                else
                {
                    this.HabilitaAprazarMedicamento(prescricaomedicamento.ObjetoMedicamento);
                    this.CheckBox_AprazarMedicamentoAutomaticamente.Enabled = false;
                }
            }
            else
                this.HabilitaAprazarMedicamento(prescricaomedicamento.ObjetoMedicamento);
        }

        protected void OnSelectedIndexChanging_Procedimentos(object sender, GridViewSelectEventArgs e)
        {
            string co_procedimento = GridView_ProcedimentoAprazar.DataKeys[e.NewSelectedIndex]["CodigoProcedimento"].ToString();
            //IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            //IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

            PrescricaoProcedimento prescricaoprocedimento = ((IList<PrescricaoProcedimento>)Session["procedimentosPrescritos"]).Where(p => p.CodigoProcedimento == co_procedimento).First();
            //iPrescricao.BuscarProcedimento<PrescricaoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento);

            if (prescricaoprocedimento.AplicacaoUnica)
            {
                AprazamentoProcedimento procedimento = ((IList<AprazamentoProcedimento>)Session["ProcedimentosAprazados"]).Where(p => p.CodigoProcedimento == co_procedimento).FirstOrDefault();
                //iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento);

                if (procedimento != null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, segundo as informações da prescrição, este procedimento deve ser executado uma única vez e o mesmo já se encontra no quadro de aprazamento. Por favor, escolha outro procedimento.');", true);
                    return;
                }
                else
                {
                    this.HabilitaAprazarProcedimento(prescricaoprocedimento.Procedimento);
                    this.CheckBox_AprazarProcedimentoAutomaticamente.Enabled = false;
                }
            }
            else
                this.HabilitaAprazarProcedimento(prescricaoprocedimento.Procedimento);
        }

        protected void OnSelectedIndexChanging_ProcedimentosNaoFaturaveis(object sender, GridViewSelectEventArgs e)
        {
            int co_procedimento = int.Parse(GridView_ProcedimentoNaoFaturavelAprazar.DataKeys[e.NewSelectedIndex]["CodigoProcedimento"].ToString());

            //IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            //IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

            PrescricaoProcedimentoNaoFaturavel prescricaoprocedimento = ((IList<PrescricaoProcedimentoNaoFaturavel>)Session["procedimentosNaoFaturaveisPrescritos"]).Where(p => p.CodigoProcedimento == co_procedimento).First();
            //iPrescricao.BuscarProcedimentoNaoFaturavel<PrescricaoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento);

            if (prescricaoprocedimento.AplicacaoUnica)
            {
                AprazamentoProcedimentoNaoFaturavel procedimento = ((IList<AprazamentoProcedimentoNaoFaturavel>)Session["ProcedimentosNaoFaturaveisAprazados"]).Where(p => p.CodigoProcedimento == co_procedimento).FirstOrDefault();
                //iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento);

                if (procedimento != null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, segundo as informações da prescrição, este procedimento deve ser executado uma única vez e o mesmo já se encontra no quadro de aprazamento. Por favor, escolha outro procedimento.');", true);
                    return;
                }
                else
                {
                    this.HabilitaAprazarProcedimentoNaoFaturavel(prescricaoprocedimento.Procedimento);
                    this.CheckBox_AprazarProcedimentoNaoFaturavelAutomaticamente.Enabled = false;
                }
            }
            else
                this.HabilitaAprazarProcedimentoNaoFaturavel(prescricaoprocedimento.Procedimento);
        }

        private void HabilitaAprazarProcedimento(Procedimento procedimento)
        {
            //Procedimento procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(co_procedimento);
            OnClick_CancelarProcedimentoAprazamento(new object(), new EventArgs());
            this.DropDownList_ProcedimentoAprazar.Items.Clear();
            DropDownList_ProcedimentoAprazar.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo.ToString()));
            DropDownList_ProcedimentoAprazar.SelectedValue = procedimento.Codigo.ToString();
        }

        private void HabilitaAprazarMedicamento(Medicamento medicamento)
        {
            //Medicamento medicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(co_medicamento);
            OnClick_CancelarMedicamentoAprazamento(new object(), new EventArgs());
            this.DropDownList_MedicamentoAprazar.Items.Clear();
            DropDownList_MedicamentoAprazar.Items.Add(new ListItem(medicamento.Nome, medicamento.Codigo.ToString()));
            DropDownList_MedicamentoAprazar.SelectedValue = medicamento.Codigo.ToString();
        }

        private void HabilitaAprazarProcedimentoNaoFaturavel(ProcedimentoNaoFaturavel procedimento)
        {
            //ProcedimentoNaoFaturavel procedimento = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ProcedimentoNaoFaturavel>(co_procedimento);
            OnClick_CancelarProcedimentoNaoFaturavelAprazamento(new object(), new EventArgs());
            this.DropDownList_ProcedimentoNaoFaturavelAprazar.Items.Clear();
            DropDownList_ProcedimentoNaoFaturavelAprazar.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo.ToString()));
            DropDownList_ProcedimentoNaoFaturavelAprazar.SelectedValue = procedimento.Codigo.ToString();
        }
        /// <summary>
        /// Formata o gridview de procedimentos a serem aprazados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewProcedimentoAprazar(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string co_procedimento = GridView_ProcedimentoAprazar.DataKeys[e.Row.RowIndex]["CodigoProcedimento"].ToString();

                //PrescricaoProcedimento procedimento = Factory.GetInstance<IPrescricao>().BuscarProcedimentos<PrescricaoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString())).Where(pt => pt.CodigoProcedimento == co_procedimento).First();
                PrescricaoProcedimento procedimento = ((IList<PrescricaoProcedimento>)Session["procedimentosPrescritos"]).Where(p => p.CodigoProcedimento == co_procedimento).First();
                //Factory.GetInstance<IPrescricao>().BuscarProcedimento<PrescricaoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento);

                if (procedimento.Prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                    GridView_ProcedimentoAprazar.Columns[GridView_ProcedimentoAprazar.Columns.Count - 1].Visible = false;

                if (procedimento.Suspenso)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;

                    if (procedimento.Prescricao.Status != Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                    {
                        LinkButton lbaprazar = (LinkButton)e.Row.Cells[3].Controls[0];
                        lbaprazar.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Formata o gridview de procedimentos não faturáveis a serem aprazados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewProcedimentoNaoFaturavelAprazar(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int co_procedimento = int.Parse(GridView_ProcedimentoNaoFaturavelAprazar.DataKeys[e.Row.RowIndex]["CodigoProcedimento"].ToString());

                //PrescricaoProcedimentoNaoFaturavel procedimentonaofaturavel = Factory.GetInstance<IPrescricao>().BuscarProcedimentosNaoFaturaveis<PrescricaoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString())).Where(pt => pt.CodigoProcedimento == co_procedimento).First();
                PrescricaoProcedimentoNaoFaturavel procedimentonaofaturavel =
                    ((IList<PrescricaoProcedimentoNaoFaturavel>)Session["procedimentosNaoFaturaveisPrescritos"]).Where(p => p.CodigoProcedimento == co_procedimento).First();
                //Factory.GetInstance<IPrescricao>().BuscarProcedimentoNaoFaturavel<PrescricaoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento);

                if (procedimentonaofaturavel.Prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                    GridView_ProcedimentoNaoFaturavelAprazar.Columns[GridView_ProcedimentoNaoFaturavelAprazar.Columns.Count - 1].Visible = false;

                if (procedimentonaofaturavel.Suspenso)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;

                    if (procedimentonaofaturavel.Prescricao.Status != Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                    {
                        LinkButton lbaprazar = (LinkButton)e.Row.Cells[3].Controls[0];
                        lbaprazar.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Formata o gridview de medicamentos a serem aprazados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewMedicamentoAprazar(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int co_medicamento = int.Parse(GridView_MedicamentoAprazar.DataKeys[e.Row.RowIndex]["Medicamento"].ToString());
                KitMedicamentoPA kit = Factory.GetInstance<IKitPA>().BuscarKitPorMedicamentoPrincipal<KitMedicamentoPA>(co_medicamento);

                LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton_Medicamento");

                if (kit != null)
                    lb.OnClientClick = "javascript:window.open('FormMostrarConteudoKit.aspx?co_kit=" + kit.KitPA.Codigo + "','Medicamento','width=600,height=300');";
                else
                    lb.OnClientClick = "javascript:alert('Este medicamento/prescrição não está associado(a) com nenhum kit.');";

                PrescricaoMedicamento prescricao = ((IList<PrescricaoMedicamento>)Session["medicamentosPrescritos"]).Where(p => p.Medicamento == co_medicamento).First();
                //Factory.GetInstance<IPrescricao>().BuscarMedicamento<PrescricaoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString()), co_medicamento);
                //Prescricao p = Factory.GetInstance<IPrescricao>().BuscarPorCodigo<Prescricao>(long.Parse(ViewState["co_prescricao"].ToString()));

                if (prescricao.Prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                    GridView_MedicamentoAprazar.Columns[GridView_MedicamentoAprazar.Columns.Count - 1].Visible = false;

                if (prescricao.Suspenso)
                {
                    e.Row.BackColor = System.Drawing.Color.LightCoral;

                    if (prescricao.Prescricao.Status != Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                    {
                        LinkButton lbaprazar = (LinkButton)e.Row.Cells[5].Controls[0];
                        lbaprazar.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Adiciona um procedimento para aprazamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarProcedimentoAprazamento(object sender, EventArgs e)
        {
            try
            {
                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                Prescricao prescricao = iPrescricao.BuscarPorCodigo<Prescricao>(long.Parse(ViewState["co_prescricao"].ToString()));

                char[] delimitador = { '/', ':' };
                string[] stringdata = TextBox_DataAprazarProcedimento.Text.Split(delimitador);
                string[] stringhora = TextBox_HoraAprazarProcedimento.Text.Split(delimitador);
                string co_procedimento = DropDownList_ProcedimentoAprazar.SelectedValue;

                PrescricaoProcedimento prescricaoprocedimento = ((IList<PrescricaoProcedimento>)Session["procedimentosPrescritos"]).Where(p => p.CodigoProcedimento == co_procedimento).First();
                //iPrescricao.BuscarProcedimento<PrescricaoProcedimento>(prescricao.Codigo, DropDownList_ProcedimentoAprazar.SelectedValue);
                IList<AprazamentoProcedimento> procedimentos = ((IList<AprazamentoProcedimento>)Session["ProcedimentosAprazados"]).Where(p => p.CodigoProcedimento == co_procedimento).ToList();
                //iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(prescricao.Codigo, DropDownList_ProcedimentoAprazar.SelectedValue);

                if (prescricaoprocedimento.AplicacaoUnica && procedimentos.Count() > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, segundo as informações da prescrição, este procedimento deve ser executado uma única vez e o mesmo já se encontra no quadro de aprazamento. Por favor, escolha outro procedimento.');", true);
                    return;
                }

                AprazamentoProcedimento aprazamento = new AprazamentoProcedimento();
                aprazamento.CodigoProcedimento = DropDownList_ProcedimentoAprazar.SelectedValue;
                aprazamento.Procedimento = iPrescricao.BuscarPorCodigo<ViverMais.Model.Procedimento>(DropDownList_ProcedimentoAprazar.SelectedValue);
                aprazamento.CodigoCid = prescricaoprocedimento.CodigoCid;
                aprazamento.Horario = new DateTime(int.Parse(stringdata[2]), int.Parse(stringdata[1]), int.Parse(stringdata[0]), int.Parse(stringhora[0]), int.Parse(stringhora[1]), 0);
                aprazamento.Status = Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo);

                UsuarioProfissionalUrgence usuarioprofissional = iPrescricao.BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                aprazamento.CodigoProfissional = usuarioprofissional.Id_Profissional;
                aprazamento.CBOProfissional = usuarioprofissional.CodigoCBO;
                aprazamento.HorarioValidoPrescricao = prescricao.UltimaDataValida;
                aprazamento.Prescricao = prescricao;

                if (procedimentos.Where(p => p.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Bloqueado)).FirstOrDefault() != null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não é possível registrar um novo horário de aprazamento para o procedimento escolhido, pois existe(m) aprazamento(s) bloqueado(s). Para resolver este impasse realize uma das opções abaixo: \\n\\n (1) Desbloqueie e execute todos as aprazamentos bloqueados. \\n (2) Exclua todos os aprazamentos bloqueados.');", true);
                }
                else
                {
                    if (procedimentos.Where(p => p.Horario.CompareTo(aprazamento.Horario) == 0).FirstOrDefault() != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um mesmo horário de aprazamento para este procedimento! Por favor, informe outro horário.');", true);
                    }
                    else
                    {
                        if (aprazamento.Horario.CompareTo(DateTime.Now) <= 0)
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A data e hora informada para a execução do procedimento não pode ser igual ou menor que a data e hora atual.');", true);
                        else
                        {
                            if (aprazamento.Horario.CompareTo(prescricao.UltimaDataValida) > 0)
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A data e hora informada para a execução do procedimento não pode ser maior que a data válida para aprazamento da prescrição.');", true);
                            else
                            {
                                if (prescricaoprocedimento.AplicacaoUnica)
                                    iAprazamento.AprazarProcedimento<AprazamentoProcedimento>(aprazamento, false, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
                                else
                                    iAprazamento.AprazarProcedimento<AprazamentoProcedimento>(aprazamento, this.CheckBox_AprazarProcedimentoAutomaticamente.Checked, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);

                                TextBox_DataAprazarProcedimento.Text = "";
                                TextBox_HoraAprazarProcedimento.Text = "";
                                this.CheckBox_AprazarProcedimentoAutomaticamente.Checked = false;

                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário(s) de aprazamento(s) incluído(s) com sucesso.');", true);
                                this.CarregaProcedimentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));

                                HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prescricao.Prontuario.Codigo);

                                //try
                                //{
                                //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prescricao.Prontuario.Codigo); });
                                //}
                                //catch { }
                            }
                        }
                    }
                }
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Cancela a edição de procedimento para aprazamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarProcedimentoAprazamento(object sender, EventArgs e)
        {
            DropDownList_ProcedimentoAprazar.Items.Clear();
            DropDownList_ProcedimentoAprazar.Items.Add(new ListItem("Selecione...", "-1"));
            DropDownList_ProcedimentoAprazar.SelectedValue = "-1";
            TextBox_DataAprazarProcedimento.Text = "";
            TextBox_HoraAprazarProcedimento.Text = "";
            this.CheckBox_AprazarProcedimentoAutomaticamente.Checked = false;
            this.CheckBox_AprazarProcedimentoAutomaticamente.Enabled = true;
        }

        /// <summary>
        /// Adiciona um procedimento não faturável para aprazamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarProcedimentoNaoFaturavelAprazamento(object sender, EventArgs e)
        {
            try
            {
                char[] delimitador = { '/', ':' };
                string[] stringdata = TextBox_DataAprazarProcedimentoNaoFaturavel.Text.Split(delimitador);
                string[] stringhora = TextBox_HoraAprazarProcedimentoNaoFaturavel.Text.Split(delimitador);
                int co_procedimento = int.Parse(DropDownList_ProcedimentoNaoFaturavelAprazar.SelectedValue);

                IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

                Prescricao prescricao = Factory.GetInstance<IPrescricao>().BuscarPorCodigo<Prescricao>(long.Parse(ViewState["co_prescricao"].ToString()));
                IList<AprazamentoProcedimentoNaoFaturavel> aprazamentos = ((IList<AprazamentoProcedimentoNaoFaturavel>)Session["ProcedimentosNaoFaturaveisAprazados"]).Where(p => p.CodigoProcedimento == co_procedimento).ToList();
                //iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(prescricao.Codigo, co_procedimento);
                PrescricaoProcedimentoNaoFaturavel prescricaoprocedimento = ((IList<PrescricaoProcedimentoNaoFaturavel>)Session["procedimentosNaoFaturaveisPrescritos"]).Where(p => p.CodigoProcedimento == co_procedimento).First();
                //iPrescricao.BuscarProcedimentoNaoFaturavel<PrescricaoProcedimentoNaoFaturavel>(prescricao.Codigo, co_procedimento);

                if (prescricaoprocedimento.AplicacaoUnica && aprazamentos.Count() > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, segundo as informações da prescrição, este procedimento deve ser executado uma única vez e o mesmo já se encontra no quadro de aprazamento. Por favor, escolha outro procedimento.');", true);
                    return;
                }

                AprazamentoProcedimentoNaoFaturavel aprazamento = new AprazamentoProcedimentoNaoFaturavel();
                aprazamento.ProcedimentoNaoFaturavel = iUrgencia.BuscarPorCodigo<ProcedimentoNaoFaturavel>(co_procedimento);
                aprazamento.Horario = new DateTime(int.Parse(stringdata[2]), int.Parse(stringdata[1]), int.Parse(stringdata[0]), int.Parse(stringhora[0]), int.Parse(stringhora[1]), 0);
                aprazamento.Status = Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo);
                UsuarioProfissionalUrgence usuarioprofissional = iUrgencia.BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                aprazamento.CodigoProfissional = usuarioprofissional.Id_Profissional;
                aprazamento.CBOProfissional = usuarioprofissional.CodigoCBO;
                aprazamento.HorarioValidoPrescricao = prescricao.UltimaDataValida;
                aprazamento.Prescricao = prescricao;

                if (aprazamentos.Where(p => p.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Bloqueado)).FirstOrDefault() != null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não é possível registrar um novo horário de aprazamento para o procedimento escolhido, pois existe(m) aprazamento(s) bloqueado(s). Para resolver este impasse realize uma das opções abaixo: \\n\\n (1) Desbloqueie e execute todos as aprazamentos bloqueados. \\n (2) Exclua todos os aprazamentos bloqueados.');", true);
                }
                else
                {
                    if (aprazamentos.Where(p => p.Horario.CompareTo(aprazamento.Horario) == 0).FirstOrDefault() != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um mesmo horário de aprazamento para este procedimento! Por favor, informe outro horário.');", true);
                    }
                    else
                    {
                        if (aprazamento.Horario.CompareTo(DateTime.Now) <= 0)
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A data e hora informada para a execução do procedimento não pode ser igual ou menor que a data e hora atual.');", true);
                        else
                        {
                            if (aprazamento.Horario.CompareTo(prescricao.UltimaDataValida) > 0)
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A data e hora informada para a execução do procedimento não pode ser maior que a data válida para aprazamento da prescrição.');", true);
                            else
                            {
                                if (prescricaoprocedimento.AplicacaoUnica)
                                    iAprazamento.AprazarProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(aprazamento, false, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
                                else
                                    iAprazamento.AprazarProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(aprazamento, this.CheckBox_AprazarProcedimentoNaoFaturavelAutomaticamente.Checked, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);

                                TextBox_DataAprazarProcedimentoNaoFaturavel.Text = "";
                                TextBox_HoraAprazarProcedimentoNaoFaturavel.Text = "";
                                this.CheckBox_AprazarProcedimentoNaoFaturavelAutomaticamente.Checked = false;

                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário(s) de aprazamento(s) incluído(s) com sucesso.');", true);
                                this.CarregaProcedimentosNaoFaturavelAprazados(long.Parse(ViewState["co_prescricao"].ToString()));

                                HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prescricao.Prontuario.Codigo);

                                //try
                                //{
                                //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prescricao.Prontuario.Codigo); });
                                //}
                                //catch { }
                            }
                        }
                    }
                }
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Cancela a edição de procedimento não faturável para aprazamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarProcedimentoNaoFaturavelAprazamento(object sender, EventArgs e)
        {
            DropDownList_ProcedimentoNaoFaturavelAprazar.Items.Clear();
            DropDownList_ProcedimentoNaoFaturavelAprazar.Items.Add(new ListItem("Selecione...", "-1"));
            DropDownList_ProcedimentoNaoFaturavelAprazar.SelectedValue = "-1";
            TextBox_DataAprazarProcedimentoNaoFaturavel.Text = "";
            TextBox_HoraAprazarProcedimentoNaoFaturavel.Text = "";
            this.CheckBox_AprazarProcedimentoNaoFaturavelAutomaticamente.Checked = false;
            this.CheckBox_AprazarProcedimentoNaoFaturavelAutomaticamente.Enabled = true;
        }

        /// <summary>
        /// Adicionar medicamento para aprazamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarMedicamentoAprazamento(object sender, EventArgs e)
        {
            try
            {
                char[] delimitador = { '/', ':' };
                string[] stringdata = TextBox_DataAprazarMedicamento.Text.Split(delimitador);
                string[] stringhora = TextBox_HoraAprazarMedicamento.Text.Split(delimitador);
                int co_medicamento = int.Parse(DropDownList_MedicamentoAprazar.SelectedValue);

                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
                IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

                Prescricao prescricao = iPrescricao.BuscarPorCodigo<Prescricao>(long.Parse(ViewState["co_prescricao"].ToString()));
                PrescricaoMedicamento prescricaomedicamento = ((IList<PrescricaoMedicamento>)Session["medicamentosPrescritos"]).Where(p => p.Medicamento == co_medicamento).First();
                //iPrescricao.BuscarMedicamento<PrescricaoMedicamento>(prescricao.Codigo, co_medicamento);
                IList<AprazamentoMedicamento> aprazamentos = ((IList<AprazamentoMedicamento>)Session["MedicamentosAprazados"]).Where(p => p.CodigoMedicamento == co_medicamento).ToList();
                //iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(prescricao.Codigo, co_medicamento);

                if (prescricaomedicamento.AplicacaoUnica && aprazamentos.Count() > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, segundo as informações da prescrição, este medicamento deve ser executado uma única vez e o mesmo já se encontra no quadro de aprazamento. Por favor, escolha outro medicamento.');", true);
                    return;
                }

                AprazamentoMedicamento aprazamento = new AprazamentoMedicamento();
                aprazamento.CodigoMedicamento = co_medicamento;
                aprazamento.Medicamento = iMedicamento.BuscarPorCodigo<Medicamento>(aprazamento.CodigoMedicamento);
                aprazamento.Horario = new DateTime(int.Parse(stringdata[2]), int.Parse(stringdata[1]), int.Parse(stringdata[0]), int.Parse(stringhora[0]), int.Parse(stringhora[1]), 0);
                aprazamento.Status = Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo);
                UsuarioProfissionalUrgence usuarioprofissional = iUrgencia.BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                aprazamento.CodigoProfissional = usuarioprofissional.Id_Profissional;
                aprazamento.CBOProfissional = usuarioprofissional.CodigoCBO;
                aprazamento.HorarioValidoPrescricao = prescricao.UltimaDataValida;
                aprazamento.Prescricao = prescricao;

                if (aprazamentos.Where(p => p.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado)).FirstOrDefault() != null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não é possível registrar um novo horário de aprazamento para o medicamento escolhido, pois existe(m) aprazamento(s) bloqueado(s). Para resolver este impasse realize uma das opções abaixo: \\n\\n (1) Desbloqueie e execute todos as aprazamentos bloqueados. \\n (2) Exclua todos os aprazamentos bloqueados.');", true);
                }
                else
                {
                    if (aprazamentos.Where(p => p.Horario.CompareTo(aprazamento.Horario) == 0).FirstOrDefault() != null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um mesmo horário de aprazamento para este medicamento! Por favor, informe outro horário.');", true);
                    }
                    else
                    {
                        if (aprazamento.Horario.CompareTo(DateTime.Now) <= 0)
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A data e hora informada para a execução do procedimento não pode ser igual ou menor que a data e hora atual.');", true);
                        else
                        {
                            if (aprazamento.Horario.CompareTo(prescricao.UltimaDataValida) > 0)
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A data e hora informada para a execução do procedimento não pode ser maior que a data válida para aprazamento da prescrição.');", true);
                            else
                            {
                                if (prescricaomedicamento.AplicacaoUnica)
                                    iAprazamento.AprazarMedicamento<AprazamentoMedicamento>(aprazamento, false, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
                                else
                                    iAprazamento.AprazarMedicamento<AprazamentoMedicamento>(aprazamento, this.CheckBox_AprazarMedicamentoAutomaticamente.Checked, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);

                                TextBox_DataAprazarMedicamento.Text = "";
                                TextBox_HoraAprazarMedicamento.Text = "";
                                this.CheckBox_AprazarMedicamentoAutomaticamente.Checked = false;

                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário(s) de aprazamento(s) incluído(s) com sucesso.');", true);
                                this.CarregaMedicamentosAprazados(prescricao.Codigo);
                                HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prescricao.Prontuario.Codigo);

                                //try
                                //{
                                //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prescricao.Prontuario.Codigo); });
                                //}
                                //catch { }
                            }
                        }
                    }
                }
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Cancelar adição do medicamento para aprazamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarMedicamentoAprazamento(object sender, EventArgs e)
        {
            DropDownList_MedicamentoAprazar.Items.Clear();
            DropDownList_MedicamentoAprazar.Items.Add(new ListItem("Selecione...", "-1"));
            DropDownList_MedicamentoAprazar.SelectedValue = "-1";
            TextBox_DataAprazarMedicamento.Text = "";
            TextBox_HoraAprazarMedicamento.Text = "";
            CheckBox_AprazarMedicamentoAutomaticamente.Checked = false;
            CheckBox_AprazarMedicamentoAutomaticamente.Enabled = true;
        }

        /// <summary>
        /// Exclui um aprazamento de medicamento já registrado e que ainda não foi confirmado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_MedicamentoAprazado(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
                int co_medicamento = int.Parse(GridView_MedicamentoAprazado.DataKeys[e.RowIndex]["CodigoMedicamento"].ToString());
                DateTime horario = DateTime.Parse(GridView_MedicamentoAprazado.DataKeys[e.RowIndex]["Horario"].ToString());
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
                AprazamentoMedicamento aprazamento = iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString()), co_medicamento, horario);
                Prescricao prescricao = aprazamento.Prescricao;

                //iPrescricao.BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == co_medicamento && p.Horario == horario).First();
                UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);

                iPrescricao.Deletar(aprazamento);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, usuario.Codigo, 30, "id_prescricao=" + aprazamento.Prescricao.Codigo + " id_medicamento = " + aprazamento.Medicamento.Codigo + " horario: " + aprazamento.Horario.ToString("dd/MM/yyyy HH:mm") + " id profissional: " + usuarioprofissional.Id_Profissional));
                iPrescricao.AtualizarStatusItensAprazadosPrescricao<Prescricao>(prescricao, true, false, false);

                GridView_MedicamentoAprazado.EditIndex = -1;
                CarregaMedicamentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário de aprazamento excluído com sucesso.');", true);
                HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prescricao.Prontuario.Codigo);
                //try
                //{
                //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, prescricao.Prontuario.Codigo); });
                //}
                //catch { }
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Exclui um aprazamento de procedimento já registrado e que ainda não foi confirmado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_ProcedimentoAprazado(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
                string co_procedimento = GridView_ProcedimentosAprazados.DataKeys[e.RowIndex]["CodigoProcedimento"].ToString();
                DateTime horario = DateTime.Parse(GridView_ProcedimentosAprazados.DataKeys[e.RowIndex]["Horario"].ToString());
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
                AprazamentoProcedimento aprazamento = iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento, horario);
                Prescricao prescricao = aprazamento.Prescricao;
                //iPrescricao.BuscarProcedimentosAprazadosPorPrescricaoVigente<AprazamentoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.Procedimento.Codigo == co_procedimento && p.Horario == horario).First();
                UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);

                iPrescricao.Deletar(aprazamento);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, usuario.Codigo, 31, "id_prescricao=" + aprazamento.Prescricao.Codigo + " co_procedimento = " + aprazamento.Procedimento.Codigo + " horario: " + aprazamento.Horario.ToString("dd/MM/yyyy HH:mm") + " id profissional: " + usuarioprofissional.Id_Profissional));
                iPrescricao.AtualizarStatusItensAprazadosPrescricao<Prescricao>(prescricao, false, true, false);

                GridView_ProcedimentosAprazados.EditIndex = -1;
                CarregaProcedimentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário de aprazamento excluído com sucesso.');", true);
                HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prescricao.Prontuario.Codigo);
                //try
                //{
                //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, prescricao.Prontuario.Codigo); });
                //}
                //catch { }
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Exclui um aprazamento de procedimento não faturável já registrado e que ainda não foi confirmado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_ProcedimentoNaoFaturavelAprazado(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
                int co_procedimento = int.Parse(GridView_ProcedimentoNaoFaturavelAprazado.DataKeys[e.RowIndex]["CodigoProcedimento"].ToString());
                DateTime horario = DateTime.Parse(GridView_ProcedimentoNaoFaturavelAprazado.DataKeys[e.RowIndex]["Horario"].ToString());
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

                AprazamentoProcedimentoNaoFaturavel aprazamento = iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento, horario);
                Prescricao prescricao = aprazamento.Prescricao;

                //iPrescricao.BuscarProcedimentosNaoFaturaveisAprazadosPorPrescricaoVigente<AprazamentoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.ProcedimentoNaoFaturavel.Codigo == co_procedimento && p.Horario == horario).First();
                UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);

                iPrescricao.Deletar(aprazamento);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, usuario.Codigo, 32, "id_prescricao=" + aprazamento.Prescricao.Codigo + " id_procedimento = " + aprazamento.ProcedimentoNaoFaturavel.Codigo + " horario: " + aprazamento.Horario.ToString("dd/MM/yyyy HH:mm") + " id profissional: " + usuarioprofissional.Id_Profissional));
                iPrescricao.AtualizarStatusItensAprazadosPrescricao<Prescricao>(prescricao, false, false, true);

                GridView_ProcedimentoNaoFaturavelAprazado.EditIndex = -1;
                CarregaProcedimentosNaoFaturavelAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário de aprazamento excluído com sucesso.');", true);
                HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prescricao.Prontuario.Codigo);
                //try
                //{
                //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, prescricao.Prontuario.Codigo); });
                //}
                //catch { }
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Formata o gridview de medicamentos aprazados de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewMedicamentoAprazado(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbrecusarmedicamento = (LinkButton)e.Row.Cells[8].Controls[0];
                //if (lbrecusarmedicamento != null)
                //    lbrecusarmedicamento.OnClientClick = "javascript:return confirm('Tem certeza que deseja recusar a aplicação deste medicamento ?');";

                int co_medicamento = int.Parse(GridView_MedicamentoAprazado.DataKeys[e.Row.RowIndex]["CodigoMedicamento"].ToString());
                DateTime horario = DateTime.Parse(GridView_MedicamentoAprazado.DataKeys[e.Row.RowIndex]["Horario"].ToString());

                //IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
                AprazamentoMedicamento aprazamento = ((IList<AprazamentoMedicamento>)Session["MedicamentosAprazados"]).Where(p => p.CodigoMedicamento == co_medicamento && p.Horario == horario).First();
                //iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString()), co_medicamento, horario);

                //AprazamentoMedicamento aprazamento = 
                //    Factory.GetInstance<IPrescricao>().BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == co_medicamento && p.Horario == horario).First();

                if (aprazamento.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo))
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8d271");
                else
                {
                    if (aprazamento.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado))
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#cbe9c6");
                    else
                        if (aprazamento.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado))
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#cda1a2");
                        else
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#bfbfbf");
                }

                if (aprazamento.Prescricao.Status != Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                {
                    if (Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado) == aprazamento.Status || Convert.ToChar(AprazamentoMedicamento.StatusItem.Recusado) == aprazamento.Status)
                    {
                        LinkButton lbexcluir = (LinkButton)e.Row.FindControl("LinkButton_Excluir");

                        if (lbexcluir != null)
                        {
                            lbexcluir.Enabled = false;
                            lbexcluir.OnClientClick = lbexcluir.OnClientClick.Length > 0 ? lbexcluir.OnClientClick.Remove(0) : lbexcluir.OnClientClick;
                        }

                        LinkButton lbconfirmarexecucao = (LinkButton)e.Row.FindControl("LinkButton_ConfirmarExcecucao");

                        if (lbconfirmarexecucao != null)
                        {
                            lbconfirmarexecucao.Enabled = false;
                            lbconfirmarexecucao.OnClientClick = lbconfirmarexecucao.OnClientClick.Length > 0 ? lbconfirmarexecucao.OnClientClick.Remove(0) : lbconfirmarexecucao.OnClientClick;
                        }

                        if (lbrecusarmedicamento != null)
                        {
                            if (Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado) == aprazamento.Status)
                                lbrecusarmedicamento.Enabled = false;
                            else
                                lbrecusarmedicamento.Text = "Justificativa de Recusa";
                        }
                    }
                    else
                    {
                        if (aprazamento.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado))
                        {
                            LinkButton lbconfirmarexecucao = (LinkButton)e.Row.FindControl("LinkButton_ConfirmarExcecucao");

                            if (lbconfirmarexecucao != null)
                                lbconfirmarexecucao.OnClientClick = "javascript:return confirm('A execução deste procedimento foi bloqueado por um dos seguintes motivos:\\n\\n (1) A sua execução ultrapassou o tempo máximo de 2 horas. \\n (2) Um procedimento anterior com status de bloqueado acabou por bloqueá-lo. \\n\\n Deseja desbloqueá-lo e executá-lo agora ?');";
                        }
                    }
                }
                else
                {
                    GridView_MedicamentoAprazado.Columns[GridView_MedicamentoAprazado.Columns.Count - 1].Visible = false;
                    GridView_MedicamentoAprazado.Columns[GridView_MedicamentoAprazado.Columns.Count - 2].Visible = false;
                    GridView_MedicamentoAprazado.Columns[GridView_MedicamentoAprazado.Columns.Count - 3].Visible = false;
                }
            }
        }

        /// <summary>
        /// Formata o gridview de procedimentos aprazados de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewProcedimentoAprazado(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string co_procedimento = GridView_ProcedimentosAprazados.DataKeys[e.Row.RowIndex]["CodigoProcedimento"].ToString();
                DateTime horario = DateTime.Parse(GridView_ProcedimentosAprazados.DataKeys[e.Row.RowIndex]["Horario"].ToString());

                //IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
                //AprazamentoProcedimento aprazamento = iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento, horario);

                AprazamentoProcedimento aprazamento = ((IList<AprazamentoProcedimento>)Session["ProcedimentosAprazados"]).Where(p => p.CodigoProcedimento == co_procedimento && p.Horario == horario).First();

                if (aprazamento.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo))
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8d271");
                else
                {
                    if (aprazamento.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Finalizado))
                        e.Row.BackColor = e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#cbe9c6");
                    else
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#cda1a2");
                }

                if (aprazamento.Prescricao.Status != Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                {
                    if (Convert.ToChar(AprazamentoProcedimento.StatusItem.Finalizado) == aprazamento.Status)
                    {
                        LinkButton lbexcluir = (LinkButton)e.Row.FindControl("LinkButton_Excluir");

                        if (lbexcluir != null)
                        {
                            lbexcluir.Enabled = false;
                            lbexcluir.OnClientClick = lbexcluir.OnClientClick.Length > 0 ? lbexcluir.OnClientClick.Remove(0) : lbexcluir.OnClientClick;
                        }

                        LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton_ConfirmarExcecucao");

                        if (lb != null)
                        {
                            lb.Enabled = false;
                            lb.OnClientClick = lb.OnClientClick.Length > 0 ? lb.OnClientClick.Remove(0) : lb.OnClientClick;
                        }
                    }
                    else
                    {
                        if (aprazamento.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Bloqueado))
                        {
                            LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton_ConfirmarExcecucao");

                            if (lb != null)
                                lb.OnClientClick = "javascript:return confirm('A execução deste procedimento foi bloqueado por um dos seguintes motivos:\\n\\n (1) A sua execução ultrapassou o tempo máximo de 2 horas. \\n (2) Um procedimento anterior com status de bloqueado acabou por bloqueá-lo. \\n\\n Deseja desbloqueá-lo e executá-lo agora ?');";
                        }
                    }
                }
                else
                {
                    GridView_ProcedimentosAprazados.Columns[GridView_ProcedimentosAprazados.Columns.Count - 1].Visible = false;
                    GridView_ProcedimentosAprazados.Columns[GridView_ProcedimentosAprazados.Columns.Count - 2].Visible = false;
                }
            }
        }

        /// <summary>
        /// Formata o gridview de procedimentos não faturáveis aprazados de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewProcedimentoNaoFaturavelAprazado(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int co_procedimento = int.Parse(GridView_ProcedimentoNaoFaturavelAprazado.DataKeys[e.Row.RowIndex]["CodigoProcedimento"].ToString());
                DateTime horario = DateTime.Parse(GridView_ProcedimentoNaoFaturavelAprazado.DataKeys[e.Row.RowIndex]["Horario"].ToString());

                //IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
                //AprazamentoProcedimentoNaoFaturavel aprazamento = iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento, horario);
                AprazamentoProcedimentoNaoFaturavel aprazamento = ((IList<AprazamentoProcedimentoNaoFaturavel>)Session["ProcedimentosNaoFaturaveisAprazados"]).Where(p => p.ProcedimentoNaoFaturavel.Codigo == co_procedimento && p.Horario == horario).First();

                if (aprazamento.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo))
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#e8d271");
                else
                {
                    if (aprazamento.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado))
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#cbe9c6");
                    else
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#cda1a2");
                }

                if (aprazamento.Prescricao.Status != Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                {
                    if (Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado) == aprazamento.Status)
                    {
                        LinkButton lbexcluir = (LinkButton)e.Row.FindControl("LinkButton_Excluir");

                        if (lbexcluir != null)
                        {
                            lbexcluir.Enabled = false;
                            lbexcluir.OnClientClick = lbexcluir.OnClientClick.Length > 0 ? lbexcluir.OnClientClick.Remove(0) : lbexcluir.OnClientClick;
                        }

                        LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton_ConfirmarExcecucao");

                        if (lb != null)
                        {
                            lb.Enabled = false;
                            lb.OnClientClick = lb.OnClientClick.Length > 0 ? lb.OnClientClick.Remove(0) : lb.OnClientClick;
                        }
                    }
                    else
                    {
                        if (aprazamento.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Bloqueado))
                        {
                            LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton_ConfirmarExcecucao");

                            if (lb != null)
                                lb.OnClientClick = "javascript:return confirm('A execução deste procedimento foi bloqueado por um dos seguintes motivos:\\n\\n (1) A sua execução ultrapassou o tempo máximo de 2 horas. \\n (2) Um procedimento anterior com status de bloqueado acabou por bloqueá-lo. \\n\\n Deseja desbloqueá-lo e executá-lo agora ?');";
                        }
                    }
                }
                else
                {
                    GridView_ProcedimentoNaoFaturavelAprazado.Columns[GridView_ProcedimentoNaoFaturavelAprazado.Columns.Count - 1].Visible = false;
                    GridView_ProcedimentoNaoFaturavelAprazado.Columns[GridView_ProcedimentoNaoFaturavelAprazado.Columns.Count - 2].Visible = false;
                }
            }
        }

        /// <summary>
        /// Paginação do gridview de medicamentos já aprazados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_MedicamentosAprazados(object sender, GridViewPageEventArgs e)
        {
            GridView_MedicamentoAprazado.DataSource = (IList<AprazamentoMedicamento>)Session["MedicamentosAprazados"];
            GridView_MedicamentoAprazado.DataBind();

            GridView_MedicamentoAprazado.PageIndex = e.NewPageIndex;
            GridView_MedicamentoAprazado.DataBind();
        }

        /// <summary>
        /// Paginação do gridview de procedimentos não faturáveis já aprazados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_ProcedimentosNaoFaturaveisAprazados(object sender, GridViewPageEventArgs e)
        {
            GridView_ProcedimentoNaoFaturavelAprazado.DataSource = (IList<AprazamentoProcedimentoNaoFaturavel>)Session["ProcedimentosNaoFaturaveisAprazados"];
            GridView_ProcedimentoNaoFaturavelAprazado.DataBind();

            //CarregaProcedimentosNaoFaturavelAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
            GridView_ProcedimentoNaoFaturavelAprazado.PageIndex = e.NewPageIndex;
            GridView_ProcedimentoNaoFaturavelAprazado.DataBind();
        }

        /// <summary>
        /// Paginação do gridview de procedimentos já aprazados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_ProcedimentosAprazados(object sender, GridViewPageEventArgs e)
        {
            GridView_ProcedimentosAprazados.DataSource = (IList<AprazamentoProcedimento>)Session["ProcedimentosAprazados"];
            GridView_ProcedimentosAprazados.DataBind();
            //CarregaProcedimentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
            GridView_ProcedimentosAprazados.PageIndex = e.NewPageIndex;
            GridView_ProcedimentosAprazados.DataBind();
        }
        #endregion

        #region CONFIRMAR APRAZAMENTO
        /// <summary>
        /// Cancela a confirmação de aprazamento para um medicamento específico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarAprazamentoMedicamento(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_MedicamentoAprazado.EditIndex = -1;
            //CarregaMedicamentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
            GridView_MedicamentoAprazado.DataSource = (IList<AprazamentoMedicamento>)Session["MedicamentosAprazados"];
            GridView_MedicamentoAprazado.DataBind();
        }

        /// <summary>
        /// Cancela a confirmação de aprazamento para um procedimento não faturável específico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarAprazamentoProcedimentoNaoFaturavel(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_ProcedimentoNaoFaturavelAprazado.EditIndex = -1;
            GridView_ProcedimentoNaoFaturavelAprazado.DataSource = (IList<AprazamentoProcedimentoNaoFaturavel>)Session["ProcedimentosNaoFaturaveisAprazados"];
            GridView_ProcedimentoNaoFaturavelAprazado.DataBind();
            //CarregaProcedimentosNaoFaturavelAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
        }

        /// <summary>
        /// Cancela a confirmação de aprazamento para um procedimento específico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarAprazamentoProcedimento(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_ProcedimentosAprazados.EditIndex = -1;
            GridView_ProcedimentosAprazados.DataSource = (IList<AprazamentoProcedimento>)Session["ProcedimentosAprazados"];
            GridView_ProcedimentosAprazados.DataBind();
            //CarregaProcedimentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
        }

        /// <summary>
        /// Edita o gridview com as informações necessárias para confirmar a excecução do aprazamento de um medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_EditarAprazamentoMedicamento(object sender, GridViewEditEventArgs e)
        {
            int co_medicamento = int.Parse(GridView_MedicamentoAprazado.DataKeys[e.NewEditIndex]["CodigoMedicamento"].ToString());
            DateTime horario = DateTime.Parse(GridView_MedicamentoAprazado.DataKeys[e.NewEditIndex]["Horario"].ToString());
            //IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
            IList<AprazamentoMedicamento> medicamentos = (IList<AprazamentoMedicamento>)Session["MedicamentosAprazados"];

            AprazamentoMedicamento aprazamento = medicamentos.Where(p => p.CodigoMedicamento == co_medicamento && p.Horario == horario).First();
            //iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString()), co_medicamento, horario);
            //iPrescricao.BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == co_medicamento && p.Horario == horario).First();
            bool aprazamentonaoexecutado = medicamentos.Where(p => p.CodigoMedicamento == co_medicamento && p.Horario < horario && (p.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado) || p.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo))).FirstOrDefault() == null ? false : true;
            //iAprazamento.AprazamentoMedicamentoAnteriorNaoExecutado(aprazamento.Prescricao.Codigo, aprazamento.CodigoMedicamento, aprazamento.Horario);
            //iPrescricao.BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == co_medicamento && p.Horario.CompareTo(aprazamento.Horario) < 0 && (p.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado) || p.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo))).FirstOrDefault();

            if (aprazamento.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado) && !Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "EXECUTAR_PROCEDIMENTO_BLOQUEADO", Modulo.URGENCIA))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para executar um procedimento bloqueado! Por favor, procure a supervisão.');", true);
            else
            {
                if (aprazamentonaoexecutado)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, existe um procedimento para este medicamento que deve ser confirmado antes!');", true);
                else
                {
                    GridView_MedicamentoAprazado.EditIndex = e.NewEditIndex;
                    //CarregaMedicamentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
                    GridView_MedicamentoAprazado.DataSource = medicamentos;
                    GridView_MedicamentoAprazado.DataBind();
                }
            }
        }

        /// <summary>
        /// Edita o gridview com as informações necessárias para confirmar a excecução do aprazamento de um procedimento não faturável
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_EditarAprazamentoProcedimentoNaoFaturavel(object sender, GridViewEditEventArgs e)
        {
            int co_procedimento = int.Parse(GridView_ProcedimentoNaoFaturavelAprazado.DataKeys[e.NewEditIndex]["CodigoProcedimento"].ToString());
            DateTime horario = DateTime.Parse(GridView_ProcedimentoNaoFaturavelAprazado.DataKeys[e.NewEditIndex]["Horario"].ToString());
            //IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
            IList<AprazamentoProcedimentoNaoFaturavel> procedimentos = (IList<AprazamentoProcedimentoNaoFaturavel>)Session["ProcedimentosNaoFaturaveisAprazados"];

            AprazamentoProcedimentoNaoFaturavel aprazamento = procedimentos.Where(p => p.CodigoProcedimento == co_procedimento && p.Horario == horario).First();
            //iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento, horario);
            bool aprazamentonaoexecutado = procedimentos.Where(p => p.CodigoProcedimento == co_procedimento && p.Horario < horario && (p.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Bloqueado) || p.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo))).FirstOrDefault() == null ? false : true;
            //iAprazamento.AprazamentoProcedimentoNaoFaturavelAnteriorNaoExecutado(aprazamento.Prescricao.Codigo, co_procedimento, horario);
            //AprazamentoProcedimentoNaoFaturavel aprazamento = iPrescricao.BuscarProcedimentosNaoFaturaveisAprazadosPorPrescricaoVigente<AprazamentoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.ProcedimentoNaoFaturavel.Codigo == co_procedimento && p.Horario == horario).First();

            //AprazamentoProcedimentoNaoFaturavel aprazamentotemporario = iPrescricao.BuscarProcedimentosNaoFaturaveisAprazadosPorPrescricaoVigente<AprazamentoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.ProcedimentoNaoFaturavel.Codigo == co_procedimento && p.Horario.CompareTo(aprazamento.Horario) < 0 && (p.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Bloqueado) || p.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo))).FirstOrDefault();

            if (aprazamento.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Bloqueado) && !Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "EXECUTAR_PROCEDIMENTO_BLOQUEADO", Modulo.URGENCIA))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para executar um procedimento bloqueado! Por favor, procure a supervisão.');", true);
            else
            {
                if (aprazamentonaoexecutado)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, existe um procedimento que deve ser confirmado antes!');", true);
                else
                {
                    GridView_ProcedimentoNaoFaturavelAprazado.EditIndex = e.NewEditIndex;
                    //CarregaProcedimentosNaoFaturavelAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
                    GridView_ProcedimentoNaoFaturavelAprazado.DataSource = procedimentos;
                    GridView_ProcedimentoNaoFaturavelAprazado.DataBind();
                }
            }
        }

        /// <summary>
        /// Edita o gridview com as informações necessárias para confirmar a excecução do aprazamento de um procedimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_EditarAprazamentoProcedimento(object sender, GridViewEditEventArgs e)
        {
            string co_procedimento = GridView_ProcedimentosAprazados.DataKeys[e.NewEditIndex]["CodigoProcedimento"].ToString();
            DateTime horario = DateTime.Parse(GridView_ProcedimentosAprazados.DataKeys[e.NewEditIndex]["Horario"].ToString());
            //IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();

            IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
            UsuarioProfissionalUrgence usuarioProfissional = iUsuarioProfissional.BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

            IList<AprazamentoProcedimento> procedimentos = (IList<AprazamentoProcedimento>)Session["ProcedimentosAprazados"];
            AprazamentoProcedimento aprazamento = procedimentos.Where(p => p.CodigoProcedimento == co_procedimento && p.Horario == horario).First();
            //iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento, horario);
            bool aprazamentonaoexecutado = procedimentos.Where(p => p.CodigoProcedimento == co_procedimento && p.Horario < horario && (p.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Bloqueado) || p.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo))).FirstOrDefault() == null ? false : true;
            //iAprazamento.AprazamentoProcedimentoAnteriorNaoExecutado(aprazamento.Prescricao.Codigo, co_procedimento, horario);

            //AprazamentoProcedimento aprazamento = iPrescricao.BuscarProcedimentosAprazadosPorPrescricaoVigente<AprazamentoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.Procedimento.Codigo == co_procedimento && p.Horario == horario).First();
            //AprazamentoProcedimento aprazamentotemporario = iPrescricao.BuscarProcedimentosAprazadosPorPrescricaoVigente<AprazamentoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.Procedimento.Codigo == co_procedimento && p.Horario.CompareTo(aprazamento.Horario) < 0 && (p.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Bloqueado) || p.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo))).FirstOrDefault();

            if (iProcedimento.CBOExecutaProcedimento(co_procedimento, usuarioProfissional.CodigoCBO))
            {
                if (aprazamento.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Bloqueado) && !Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "EXECUTAR_PROCEDIMENTO_BLOQUEADO", Modulo.URGENCIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para executar um procedimento bloqueado! Por favor, procure a supervisão.');", true);
                else
                {
                    if (aprazamentonaoexecutado)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, existe um procedimento que deve ser confirmado antes!');", true);
                    else
                    {
                        GridView_ProcedimentosAprazados.EditIndex = e.NewEditIndex;
                        GridView_ProcedimentosAprazados.DataSource = procedimentos;
                        GridView_ProcedimentosAprazados.DataBind();
                        //CarregaProcedimentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
                    }
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, de acordo com o seu CBO, você não tem permissão para executar este procedimento.');", true);
        }

        /// <summary>
        /// Finaliza a execução do aprazamento para um medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_ConfirmarAprazamentoMedicamento(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                char[] delimitador = { '/', ':' };
                GridViewRow linha = GridView_MedicamentoAprazado.Rows[e.RowIndex];
                string[] data = ((TextBox)linha.FindControl("TextBox_DataExecucao")).Text.Split(delimitador);
                string[] horario = ((TextBox)linha.FindControl("TextBox_HoraExecucao")).Text.Split(delimitador);
                DateTime horaexecucao = new DateTime(int.Parse(data[2]), int.Parse(data[1]), int.Parse(data[0]), int.Parse(horario[0]), int.Parse(horario[1]), 0);

                int co_medicamento = int.Parse(GridView_MedicamentoAprazado.DataKeys[e.RowIndex]["CodigoMedicamento"].ToString());
                DateTime horariodefinitivo = DateTime.Parse(GridView_MedicamentoAprazado.DataKeys[e.RowIndex]["Horario"].ToString());
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

                AprazamentoMedicamento aprazamento = iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString()), co_medicamento, horariodefinitivo);
                //iPrescricao.BuscarMedicamentosAprazadosPorPrescricaoVigente<AprazamentoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.CodigoMedicamento == co_medicamento && p.Horario == horariodefinitivo).First();
                aprazamento.Status = Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado);
                aprazamento.HorarioConfirmacao = horaexecucao;
                UsuarioProfissionalUrgence usuarioprofissional = iPrescricao.BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                aprazamento.CodigoProfissionalConfirmacao = usuarioprofissional.Id_Profissional;
                aprazamento.CBOProfissionalConfirmacao = usuarioprofissional.CodigoCBO;
                aprazamento.HorarioConfirmacaoSistema = DateTime.Now;

                iPrescricao.Atualizar(aprazamento);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 23, "id_prescricao=" + aprazamento.Prescricao.Codigo + " id_medicamento = " + aprazamento.Medicamento.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento confirmado com sucesso.');", true);

                iPrescricao.AtualizarStatusItensAprazadosPrescricao<Prescricao>(aprazamento.Prescricao, true, false, false);
                GridView_MedicamentoAprazado.EditIndex = -1;
                CarregaMedicamentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));

                HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, aprazamento.Prescricao.Prontuario.Codigo);

                //try
                //{
                //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, aprazamento.Prescricao.Prontuario.Codigo); });
                //}
                //catch { }
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Finaliza a execução do aprazamento para um procedimento não faturável
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_ConfirmarAprazamentoProcedimentoNaoFaturavel(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                char[] delimitador = { '/', ':' };
                GridViewRow linha = GridView_ProcedimentoNaoFaturavelAprazado.Rows[e.RowIndex];
                string[] data = ((TextBox)linha.FindControl("TextBox_DataExecucao")).Text.Split(delimitador);
                string[] horario = ((TextBox)linha.FindControl("TextBox_HoraExecucao")).Text.Split(delimitador);
                DateTime horaexecucao = new DateTime(int.Parse(data[2]), int.Parse(data[1]), int.Parse(data[0]), int.Parse(horario[0]), int.Parse(horario[1]), 0);

                int co_procedimento = int.Parse(GridView_ProcedimentoNaoFaturavelAprazado.DataKeys[e.RowIndex]["CodigoProcedimento"].ToString());
                DateTime horariodefinitivo = DateTime.Parse(GridView_ProcedimentoNaoFaturavelAprazado.DataKeys[e.RowIndex]["Horario"].ToString());
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();

                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
                AprazamentoProcedimentoNaoFaturavel aprazamento = iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento, horariodefinitivo);

                //AprazamentoProcedimentoNaoFaturavel aprazamento = iPrescricao.BuscarProcedimentosNaoFaturaveisAprazadosPorPrescricaoVigente<AprazamentoProcedimentoNaoFaturavel>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.ProcedimentoNaoFaturavel.Codigo == co_procedimento && p.Horario == horariodefinitivo).First();

                aprazamento.Status = Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado);
                aprazamento.HorarioConfirmacao = horaexecucao;
                UsuarioProfissionalUrgence usuarioprofissional = iPrescricao.BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                aprazamento.CodigoProfissionalConfirmacao = usuarioprofissional.Id_Profissional;
                aprazamento.CBOProfissionalConfirmacao = usuarioprofissional.CodigoCBO;
                aprazamento.HorarioConfirmacaoSistema = DateTime.Now;

                iPrescricao.Atualizar(aprazamento);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 36, "id_prescricao=" + aprazamento.Prescricao.Codigo + " id_procedimento = " + aprazamento.ProcedimentoNaoFaturavel.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento confirmado com sucesso.');", true);

                iPrescricao.AtualizarStatusItensAprazadosPrescricao<Prescricao>(aprazamento.Prescricao, false, false, true);
                GridView_ProcedimentoNaoFaturavelAprazado.EditIndex = -1;
                CarregaProcedimentosNaoFaturavelAprazados(long.Parse(ViewState["co_prescricao"].ToString()));

                HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, aprazamento.Prescricao.Prontuario.Codigo);

                //try
                //{
                //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, aprazamento.Prescricao.Prontuario.Codigo); });
                //}
                //catch { }
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Finaliza a execução do aprazamento para um procedimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_ConfirmarAprazamentoProcedimento(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                char[] delimitador = { '/', ':' };
                GridViewRow linha = GridView_ProcedimentosAprazados.Rows[e.RowIndex];
                string[] data = ((TextBox)linha.FindControl("TextBox_DataExecucao")).Text.Split(delimitador);
                string[] horario = ((TextBox)linha.FindControl("TextBox_HoraExecucao")).Text.Split(delimitador);
                DateTime horaexecucao = new DateTime(int.Parse(data[2]), int.Parse(data[1]), int.Parse(data[0]), int.Parse(horario[0]), int.Parse(horario[1]), 0);

                string co_procedimento = GridView_ProcedimentosAprazados.DataKeys[e.RowIndex]["CodigoProcedimento"].ToString();
                DateTime horariodefinitivo = DateTime.Parse(GridView_ProcedimentosAprazados.DataKeys[e.RowIndex]["Horario"].ToString());

                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
                AprazamentoProcedimento aprazamento = iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString()), co_procedimento, horariodefinitivo);

                //AprazamentoProcedimento aprazamento = iPrescricao.BuscarProcedimentosAprazadosPorPrescricaoVigente<AprazamentoProcedimento>(long.Parse(ViewState["co_prescricao"].ToString())).Where(p => p.Procedimento.Codigo == co_procedimento && p.Horario == horariodefinitivo).First();

                aprazamento.Status = Convert.ToChar(AprazamentoProcedimento.StatusItem.Finalizado);
                aprazamento.HorarioConfirmacao = horaexecucao;
                UsuarioProfissionalUrgence usuarioprofissional = iPrescricao.BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
                aprazamento.CodigoProfissionalConfirmacao = usuarioprofissional.Id_Profissional;
                aprazamento.CBOProfissionalConfirmacao = usuarioprofissional.CodigoCBO;
                aprazamento.HorarioConfirmacaoSistema = DateTime.Now;

                iPrescricao.Atualizar(aprazamento);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 35, "id_prescricao=" + aprazamento.Prescricao.Codigo + " co_procedimento = " + aprazamento.Procedimento.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento confirmado com sucesso.');", true);

                iPrescricao.AtualizarStatusItensAprazadosPrescricao<Prescricao>(aprazamento.Prescricao, false, true, false);
                GridView_ProcedimentosAprazados.EditIndex = -1;
                CarregaProcedimentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));

                HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, aprazamento.Prescricao.Prontuario.Codigo);

                //try
                //{
                //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, aprazamento.Prescricao.Prontuario.Codigo); });
                //}
                //catch { }
            }
            catch (Exception f)
            {
                throw f;
            }
        }
        #endregion

        #region RECUSAR APLICACAO DE MEDICAMENTO
        /// <summary>
        /// 1 - Habilita a opção de recusa do medicamento aprazado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_Medicamentos(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Recusar")
            {
                int co_medicamento = int.Parse(GridView_MedicamentoAprazado.DataKeys[int.Parse(e.CommandArgument.ToString())]["CodigoMedicamento"].ToString());
                DateTime horario = DateTime.Parse(GridView_MedicamentoAprazado.DataKeys[int.Parse(e.CommandArgument.ToString())]["Horario"].ToString());

                IList<AprazamentoMedicamento> medicamentos = (IList<AprazamentoMedicamento>)Session["MedicamentosAprazados"];

                AprazamentoMedicamento aprazamento = medicamentos.Where(p => p.CodigoMedicamento == co_medicamento && p.Horario == horario).First();
                bool aprazamentonaoexecutado = medicamentos.Where(p => p.CodigoMedicamento == co_medicamento && p.Horario < horario && (p.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado) || p.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo))).FirstOrDefault() == null ? false : true;

                if (aprazamento.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado) && !Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "EXECUTAR_PROCEDIMENTO_BLOQUEADO", Modulo.URGENCIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para executar um procedimento bloqueado! Por favor, procure a supervisão.');", true);
                else
                {
                    if (aprazamentonaoexecutado)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, existe um procedimento para este medicamento que deve ser confirmado/recusado antes!');", true);
                    else
                    {
                        ViewState["co_aprazamentoRecusa"] = aprazamento.Codigo;
                        this.Label_MedicamentoRecusa.Text = aprazamento.Medicamento.Nome;
                        this.Label_HorarioMedicamentoRecusa.Text = aprazamento.Horario.ToString("dd/MM/yyyy HH:mm");
                        this.TextBox_MotivoRecusaMedicamento.Text = string.Empty;
                        this.Panel_RecusarMedicamento.Visible = true;

                        if (Convert.ToChar(AprazamentoMedicamento.StatusItem.Recusado) == aprazamento.Status)
                        {
                            this.Panel_ConfirmarCancelarRecusaMedicamento.Visible = false;
                            this.Panel_FecharMotivoRecusaMedicamento.Visible = true;
                            this.TextBox_MotivoRecusaMedicamento.Enabled = false;
                            this.TextBox_MotivoRecusaMedicamento.Text = aprazamento.MotivoRecusa;
                        }
                        else
                        {
                            this.Panel_ConfirmarCancelarRecusaMedicamento.Visible = true;
                            this.Panel_FecharMotivoRecusaMedicamento.Visible = false;
                            this.TextBox_MotivoRecusaMedicamento.Enabled = true;
                            this.TextBox_MotivoRecusaMedicamento.Text = string.Empty;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Confirma a recusa do medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_ConfirmarRecusaMedicamento(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
                IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                AprazamentoMedicamento aprazamento = iAprazamento.BuscarPorCodigo<AprazamentoMedicamento>(long.Parse(ViewState["co_aprazamentoRecusa"].ToString()));

                aprazamento.Status = Convert.ToChar(AprazamentoMedicamento.StatusItem.Recusado);
                UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
                aprazamento.CodigoProfissionalConfirmacao = usuarioprofissional.Id_Profissional;
                aprazamento.CBOProfissionalConfirmacao = usuarioprofissional.CodigoCBO;
                aprazamento.MotivoRecusa = this.TextBox_MotivoRecusaMedicamento.Text;
                aprazamento.HorarioConfirmacao = DateTime.Now;
                aprazamento.HorarioConfirmacaoSistema = aprazamento.HorarioConfirmacao;

                iAprazamento.Atualizar(aprazamento);
                iPrescricao.Inserir(new LogUrgencia(DateTime.Now, usuario.Codigo, 42, "id_prescricao=" + aprazamento.Prescricao.Codigo + " id_medicamento = " + aprazamento.CodigoMedicamento.ToString()));
                iPrescricao.AtualizarStatusItensAprazadosPrescricao<Prescricao>(aprazamento.Prescricao, true, false, false);

                GridView_MedicamentoAprazado.EditIndex = -1;
                CarregaMedicamentosAprazados(long.Parse(ViewState["co_prescricao"].ToString()));
                this.OnClick_CancelarRecusaMedicamento(new object(), new EventArgs());
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento/Prescrição recusado(a) com sucesso.');", true);

                HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, aprazamento.Prescricao.Prontuario.Codigo);

                //try
                //{
                //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, aprazamento.Prescricao.Prontuario.Codigo); });
                //}
                //catch { }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cancela a recusa do medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarRecusaMedicamento(object sender, EventArgs e)
        {
            ViewState.Remove("co_aprazamentoRecusa");
            this.Panel_RecusarMedicamento.Visible = false;
        }
        #endregion
    }
}
