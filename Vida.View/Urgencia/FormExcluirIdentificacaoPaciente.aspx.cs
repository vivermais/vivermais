﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Data;
using System.Drawing;

namespace ViverMais.View.Urgencia
{
    public partial class FormExcluirIdentificacaoPaciente : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.TabContainer_ExcluirIdentificacao.ActiveTabIndex = this.TabContainer_ExcluirIdentificacao.ActiveTabIndex;

            this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.SelectedIndexChanging += new GridViewSelectEventHandler(OnSelectedIndexChanging_PacienteUrgence);
            this.WUC_PesquisarAtendimento.WUC_LinkButton_ListarTodos.Click += new EventHandler(OnClick_ListarTodosPacientes);
            this.WUC_PesquisarAtendimento.WUC_LinkButton_Pesquisar.Click += new EventHandler(OnClick_PesquisarPacientes);
            this.WUC_PesquisarPaciente.GridView.SelectedIndexChanging +=new GridViewSelectEventHandler(OnSelectedIndexChanging_PacienteSUS);

            ImageButton botaopesquisarpacientesus = this.WUC_PesquisarPaciente.WUC_BotaoPesquisar;
            botaopesquisarpacientesus.Click += new ImageClickEventHandler(OnClick_PesquisarPacienteSUS);

            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.UniqueID, "SelectedIndexChanging", this.WUC_ExibirPacienteAtendimento.WUC_UpdatePanelExibirPaciente);
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.UniqueID, "SelectedIndexChanging", this.UpdatePanelAtendimentos);
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.UniqueID, "SelectedIndexChanging", this.UpdatePanel_BotoesAcao);
            this.InserirTrigger(this.LinkButton_ExcluirIdentificacao.UniqueID, "Click", this.WUC_ExibirPacienteAtendimento.WUC_UpdatePanelExibirPaciente);
            this.InserirTrigger(this.LinkButton_Cancelar.UniqueID, "Click", this.WUC_ExibirPacienteAtendimento.WUC_UpdatePanelExibirPaciente);

            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_LinkButton_ListarTodos.UniqueID, "Click", this.UpdatePanelAtendimentos);
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_LinkButton_Pesquisar.UniqueID, "Click", this.UpdatePanelAtendimentos);
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_LinkButton_ListarTodos.UniqueID, "Click", this.UpdatePanel_BotoesAcao);
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_LinkButton_Pesquisar.UniqueID, "Click", this.UpdatePanel_BotoesAcao);
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_LinkButton_ListarTodos.UniqueID, "Click", this.WUC_ExibirPacienteAtendimento.WUC_UpdatePanelExibirPaciente);
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_LinkButton_Pesquisar.UniqueID, "Click", this.WUC_ExibirPacienteAtendimento.WUC_UpdatePanelExibirPaciente);

            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_LinkButton_ListarTodos.UniqueID, "Click", this.WUC_PesquisarPaciente.WUC_UpdatePanel_ResultadoPesquisa);
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_LinkButton_Pesquisar.UniqueID, "Click", this.WUC_PesquisarPaciente.WUC_UpdatePanel_ResultadoPesquisa);
            this.InserirTrigger(this.LinkButton_ExcluirIdentificacao.UniqueID, "Click", this.WUC_PesquisarPaciente.WUC_UpdatePanel_ResultadoPesquisa);

            this.InserirTrigger(botaopesquisarpacientesus.UniqueID, "Click", this.UpdatePanelAtendimentos);
            this.InserirTrigger(botaopesquisarpacientesus.UniqueID, "Click", this.WUC_ExibirPacienteAtendimento.WUC_UpdatePanelExibirPaciente);
            this.InserirTrigger(botaopesquisarpacientesus.UniqueID, "Click", this.UpdatePanel_BotoesAcao);

            this.InserirTrigger(this.WUC_PesquisarPaciente.GridView.UniqueID, "SelectedIndexChanging", this.WUC_PesquisarAtendimento.WUC_UpdatePanel_ResultadoPesquisa);
            this.InserirTrigger(this.WUC_PesquisarPaciente.GridView.UniqueID, "SelectedIndexChanging", this.WUC_ExibirPacienteAtendimento.WUC_UpdatePanelExibirPaciente);
            this.InserirTrigger(this.WUC_PesquisarPaciente.GridView.UniqueID, "SelectedIndexChanging", this.UpdatePanel_BotoesAcao);

            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "EXCLUIR_IDENTIFICACAO_PACIENTE", Modulo.URGENCIA))
                {
                    this.WUC_PesquisarPaciente.GridView.Columns.RemoveAt(1);

                    BoundField nomepaciente = new BoundField();
                    nomepaciente.DataField = "Nome";
                    nomepaciente.HeaderText = "Nome";
                    this.WUC_PesquisarPaciente.GridView.Columns.Insert(1, nomepaciente);

                    this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.ImageUrl = "~/Urgencia/img/pesquisarcartao1.png";
                    this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Attributes.Add("onmouseover", "this.src='img/pesquisarcartao2.png';");
                    this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Attributes.Add("onmouseout", "this.src='img/pesquisarcartao1.png';");

                    LinkButton botaobiometria = this.WUC_PesquisarPaciente.WUC_BotaoBiometria;
                    botaobiometria.PostBackUrl = "~/Urgencia/FormBiometriaExcluirIdentificacaoPaciente.aspx";

                    this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Src = "~/Urgencia/img/bts/id_biometrica1.png";
                    this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Attributes.Add("onmouseover", "this.src='img/bts/id_biometrica2.png';");
                    this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Attributes.Add("onmouseout", "this.src='img/bts/id_biometrica1.png';");

                    this.VerificaIdentificacaoBiometrica();
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Adiciona uma trigger do tipo asyncpostback em um updatepanel
        /// </summary>
        /// <param name="idcontrole">id do controle que disparará a trigger</param>
        /// <param name="nomeevento">nome do evento associado ao controle</param>
        /// <param name="updatepanel">updatepanel onde será registrado a trigger</param>
        private void InserirTrigger(string idcontrole, string nomeevento, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trig = new AsyncPostBackTrigger();
            trig.ControlID = idcontrole;
            trig.EventName = nomeevento;
            updatepanel.Triggers.Add(trig);
        }

        private void VerificaIdentificacaoBiometrica()
        {
            if (Request["co_paciente"] != null)
            {
                IList<ViverMais.Model.Paciente> pacientes = new List<ViverMais.Model.Paciente>();
                ViverMais.Model.Paciente paciente = BLL.PacienteBLL.Pesquisar(Request["co_paciente"].ToString());

                if (paciente != null)
                    pacientes.Add(paciente);

                Session["WUCPacientes"] = pacientes;

                this.WUC_PesquisarPaciente.GridView.DataSource = pacientes;
                this.WUC_PesquisarPaciente.GridView.DataBind();
                this.TabContainer_ExcluirIdentificacao.ActiveTabIndex = 1;
                //Selecionar o segundo tabpanel
            }
        }

        protected void OnSelectedIndexChanging_PacienteSUS(object sender, GridViewSelectEventArgs e)
        {
            this.EsconderComandos();
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            string co_paciente = this.WUC_PesquisarPaciente.Paciente.Codigo;
                //((ViverMais.Model.Paciente)Session["WUCPacienteSelecionado"]).Codigo;

            this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.SelectedIndex = -1;
            this.CarregaAtendimentos(iProntuario.BuscarProntuarioPacientesIdentificados<Prontuario>(((Usuario)Session["Usuario"]).Unidade.CNES, co_paciente));

            this.WUC_PesquisarAtendimento.WUC_Panel_ResultadoPesquisa.Visible = true;
            this.WUC_PesquisarAtendimento.WUC_UpdatePanel_ResultadoPesquisa.Update();
            
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "select", "SetActiveTab('" + this.TabContainer_ExcluirIdentificacao.ClientID + "',0);", true);
            //this.TabContainer_ExcluirIdentificacao.ActiveTab = this.TabContainer_ExcluirIdentificacao.Tabs[0];
        }

        protected void OnClick_PesquisarPacienteSUS(object sender, ImageClickEventArgs e)
        {
            if (this.WUC_PesquisarPaciente.Page.IsValid)
                this.EsconderComandos();
        }

        protected void OnClick_ListarTodosPacientes(object sender, EventArgs e)
        {
            this.EsconderPesquisaPaciente();
            this.OnClick_CancelarExclusao(new object(), new EventArgs());
            this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.SelectedIndex = -1;
            this.CarregaPacientesIdentificados(((Usuario)Session["Usuario"]).Unidade.CNES);
            this.WUC_PesquisarAtendimento.WUC_Panel_ResultadoPesquisa.Visible = true;
        }

        protected void OnClick_PesquisarPacientes(object sender, EventArgs e)
        {
            CustomValidator custom = this.WUC_PesquisarAtendimento.WUC_CustomValidator_PesquisarAtendimento;

            if (custom.IsValid)
            {
                TextBox textboxnumero = this.WUC_PesquisarAtendimento.WUC_TextBox_NumeroAtendimento;
                TextBox textboxdatainicio = this.WUC_PesquisarAtendimento.WUC_TextBox_DataInicialAtendimento;
                TextBox textboxdatafim = this.WUC_PesquisarAtendimento.WUC_TextBox_DataFinalAtendimento;

                IProntuario iProntuario = Factory.GetInstance<IProntuario>();

                this.EsconderPesquisaPaciente();
                this.OnClick_CancelarExclusao(new object(), new EventArgs());
                this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.SelectedIndex = -1;

                if (!string.IsNullOrEmpty(textboxnumero.Text))
                    this.CarregaAtendimentos(iProntuario.BuscarProntuarioPacientesIdentificados<Prontuario>(((Usuario)Session["Usuario"]).Unidade.CNES, int.Parse(textboxnumero.Text)));
                else
                    this.CarregaAtendimentos(iProntuario.BuscarProntuarioPacientesIdentificados<Prontuario>(((Usuario)Session["Usuario"]).Unidade.CNES, DateTime.Parse(textboxdatainicio.Text), DateTime.Parse(textboxdatafim.Text)));

                this.WUC_PesquisarAtendimento.WUC_Panel_ResultadoPesquisa.Visible = true;
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + custom.ErrorMessage + "');", true);
        }

        private void CarregaPacientesIdentificados(string co_unidade)
        {
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            this.CarregaAtendimentos(iProntuario.BuscarProntuarioPacientesIdentificados<Prontuario>(co_unidade));
        }

        private void CarregaAtendimentos(IList<Prontuario> atendimentos)
        {
            Session["atendimentosUrgencePesquisados"] = atendimentos;

            this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.DataSource = atendimentos;
            this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.DataBind();
        }

        protected void OnSelectedIndexChanging_PacienteUrgence(object sender, GridViewSelectEventArgs e)
        {
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            ViverMais.Model.Prontuario prontuario = iProntuario.BuscarPorCodigo<ViverMais.Model.Prontuario>(long.Parse(this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.DataKeys[e.NewSelectedIndex]["Codigo"].ToString()));

            this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.SelectedRowStyle.BackColor = Color.LightGray;

            this.WUC_ExibirAtendimento.Prontuario = prontuario;
            this.WUC_ExibirAtendimento.Visible = true;
            this.UpdatePanelAtendimentos.Update();

            this.WUC_ExibirPacienteAtendimento.Paciente = BLL.PacienteBLL.Pesquisar(prontuario.Paciente.CodigoViverMais);
            this.WUC_ExibirPacienteAtendimento.WUC_PanelDadosPaciente.Visible = true;
            this.WUC_ExibirPacienteAtendimento.WUC_UpdatePanelExibirPaciente.Update();

            this.Panel_Acoes.Visible = true;
            this.UpdatePanel_BotoesAcao.Update();
        }

        protected void OnClickExcluirIdentificacao(object sender, EventArgs e)
        {
            IPacienteUrgence iPaciente = Factory.GetInstance<IPacienteUrgence>();
            ViverMais.Model.PacienteUrgence pacienteUrgencia = this.WUC_ExibirAtendimento.Prontuario.Paciente;
            pacienteUrgencia.CodigoViverMais = string.Empty;

            try
            {
                iPaciente.Atualizar(pacienteUrgencia);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Identificação excluída com sucesso!');", true);
                iPaciente.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 10, "ID: " + pacienteUrgencia.Codigo.ToString()));

                this.OnClick_CancelarExclusao(new object(), new EventArgs());
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        private void EsconderPesquisaPaciente()
        {
            this.WUC_PesquisarPaciente.GridView.Visible = false;
            this.WUC_PesquisarPaciente.WUC_UpdatePanel_ResultadoPesquisa.Update();
        }

        protected void OnClick_CancelarExclusao(object sender, EventArgs e)
        {
            this.EsconderComandos();
            this.EsconderPesquisaPaciente();
            //this.WUC_PesquisarAtendimento.WUC_Panel_ResultadoPesquisa.Visible = false;
            //this.WUC_PesquisarAtendimento.WUC_UpdatePanel_ResultadoPesquisa.Update();

            this.WUC_ExibirAtendimento.Prontuario = null;
            //this.WUC_ExibirAtendimento.Visible = false;

            this.WUC_ExibirPacienteAtendimento.Paciente = null;
            //this.WUC_ExibirPacienteAtendimento.WUC_PanelDadosPaciente.Visible = false;
            //this.WUC_ExibirPacienteAtendimento.WUC_UpdatePanelExibirPaciente.Update();

            //this.Panel_Acoes.Visible = false;
            //this.UpdatePanel_BotoesAcao.Update();
        }

        private void EsconderComandos()
        {
            this.WUC_PesquisarAtendimento.WUC_Panel_ResultadoPesquisa.Visible = false;
            this.WUC_PesquisarAtendimento.WUC_UpdatePanel_ResultadoPesquisa.Update();
            this.WUC_ExibirAtendimento.Visible = false;
            this.UpdatePanelAtendimentos.Update();

            this.WUC_ExibirPacienteAtendimento.WUC_PanelDadosPaciente.Visible = false;
            this.WUC_ExibirPacienteAtendimento.WUC_UpdatePanelExibirPaciente.Update();
            this.Panel_Acoes.Visible = false;
            this.UpdatePanel_BotoesAcao.Update();
        }
    }
}
