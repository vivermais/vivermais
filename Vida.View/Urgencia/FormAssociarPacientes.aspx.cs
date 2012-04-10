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
using System.Data;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Drawing;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;

namespace ViverMais.View.Urgencia
{
    public partial class FormAssociarPacientes : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.WUC_PesquisarPaciente.GridView.SelectedIndexChanging += new GridViewSelectEventHandler(GridViewPacienteSusSelectedIndexChanged);
            this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.SelectedIndexChanging += new GridViewSelectEventHandler(OnSelectedIndexChanging_PacienteUrgence);
            this.WUC_PesquisarAtendimento.WUC_LinkButton_ListarTodos.Click +=new EventHandler(OnClick_ListarTodosPacientes);
            this.WUC_PesquisarAtendimento.WUC_LinkButton_Pesquisar.Click += new EventHandler(OnClick_PesquisarPacientes);
            this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Click +=new ImageClickEventHandler(OnClick_PesquisarPacienteSUS);

            this.InserirTrigger(this.WUC_PesquisarPaciente.GridView.UniqueID, "SelectedIndexChanging", this.WUC_ExibirPaciente.WUC_UpdatePanelExibirPaciente);
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.UniqueID, "SelectedIndexChanging", this.UpdatePanel_PacienteUrgence);
            
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_LinkButton_ListarTodos.UniqueID, "Click", this.UpdatePanel_PacienteUrgence);
            this.InserirTrigger(this.WUC_PesquisarAtendimento.WUC_LinkButton_Pesquisar.UniqueID, "Click", this.UpdatePanel_PacienteUrgence);

            this.InserirTrigger(this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.UniqueID, "Click", this.WUC_ExibirPaciente.WUC_UpdatePanelExibirPaciente);

            this.InserirTrigger(this.ButtonAssociar.UniqueID, "Click", this.WUC_PesquisarAtendimento.WUC_UpdatePanel_ResultadoPesquisa);
            this.InserirTrigger(this.ButtonAssociar.UniqueID, "Click", this.WUC_ExibirPaciente.WUC_UpdatePanelExibirPaciente);
            this.InserirTrigger(this.ButtonAssociar.UniqueID, "Click", this.WUC_PesquisarPaciente.WUC_UpdatePanel_ResultadoPesquisa);

            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "IDENTIFICAR_PACIENTE", Modulo.URGENCIA))
                {
                    this.CarregaPacientesSemIdentificacao(((Usuario)Session["Usuario"]).Unidade.CNES);
                    this.WUC_PesquisarAtendimento.WUC_Panel_ResultadoPesquisa.Visible = true;
                    this.WUC_PesquisarAtendimento.WUC_UpdatePanel_ResultadoPesquisa.Update();
                    this.WUC_PesquisarPaciente.GridView.Columns.RemoveAt(1);

                    BoundField nomepaciente = new BoundField();
                    nomepaciente.DataField = "Nome";
                    nomepaciente.HeaderText = "Nome";
                    this.WUC_PesquisarPaciente.GridView.Columns.Insert(1, nomepaciente);

                    this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.ImageUrl = "~/Urgencia/img/pesquisarcartao1.png";
                    this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Attributes.Add("onmouseover", "this.src='img/pesquisarcartao2.png';");
                    this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Attributes.Add("onmouseout", "this.src='img/pesquisarcartao1.png';");

                    LinkButton botaobiometria = this.WUC_PesquisarPaciente.WUC_BotaoBiometria;
                    botaobiometria.PostBackUrl = "~/Urgencia/FormBiometriaIdentificarPaciente.aspx";

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

        protected void OnClick_PesquisarPacienteSUS(object sender, ImageClickEventArgs e)
        {
            if (this.WUC_PesquisarPaciente.Page.IsValid)
            {
                this.WUC_ExibirPaciente.Paciente = null;
                this.WUC_ExibirPaciente.WUC_PanelDadosPaciente.Visible = false;
                this.WUC_ExibirPaciente.WUC_UpdatePanelExibirPaciente.Update();
            }
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
            }
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
                this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.SelectedIndex = -1;
                this.WUC_ExibirAtendimento.Prontuario = null;
                this.WUC_ExibirAtendimento.Visible = false;

                if (!string.IsNullOrEmpty(textboxnumero.Text))
                    this.CarregaAtendimentos(iProntuario.BuscarProntuarioPacientesSemIdentificacao<Prontuario>(((Usuario)Session["Usuario"]).Unidade.CNES,int.Parse(textboxnumero.Text)));
                else
                    this.CarregaAtendimentos(iProntuario.BuscarProntuarioPacientesSemIdentificacao<Prontuario>(((Usuario)Session["Usuario"]).Unidade.CNES, DateTime.Parse(textboxdatainicio.Text), DateTime.Parse(textboxdatafim.Text)));

                this.WUC_PesquisarAtendimento.WUC_Panel_ResultadoPesquisa.Visible = true;
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + custom.ErrorMessage + "');", true);
        }

        protected void OnClick_ListarTodosPacientes(object sender, EventArgs e)
        {
            this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.SelectedIndex = -1;
            this.WUC_ExibirAtendimento.Prontuario = null;
            this.WUC_ExibirAtendimento.Visible = false;

            this.CarregaPacientesSemIdentificacao(((Usuario)Session["Usuario"]).Unidade.CNES);
            this.WUC_PesquisarAtendimento.WUC_Panel_ResultadoPesquisa.Visible = true;
        }

        private void CarregaPacientesSemIdentificacao(string co_unidade)
        {
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            this.CarregaAtendimentos(iProntuario.BuscarProntuarioPacientesSemIdentificacao<Prontuario>(co_unidade));
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

            this.WUC_ExibirAtendimento.Prontuario = prontuario;
            this.WUC_ExibirAtendimento.Visible = true;
            this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.SelectedRowStyle.BackColor = Color.LightGray;
        }

        protected void GridViewPacienteSusSelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            this.WUC_ExibirPaciente.Paciente = this.WUC_PesquisarPaciente.Paciente;
                //(ViverMais.Model.Paciente)Session["WUCPacienteSelecionado"];
        }

        protected void OnClick_AssociarPacientes(object sender, EventArgs e)
        {
            IPacienteUrgence iPacienteUrgencia = Factory.GetInstance<IPacienteUrgence>();
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            IEstabelecimentoSaude iUnidade = Factory.GetInstance<IEstabelecimentoSaude>();

            ViverMais.Model.Prontuario prontuario = this.WUC_ExibirAtendimento.Prontuario;
            ViverMais.Model.Paciente pacienteViverMais = this.WUC_ExibirPaciente.Paciente;

            if (prontuario == null && pacienteViverMais == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert(' - Selecione um paciente atendido pelo Urgência e Emergência. \\n - Selecione um paciente com cartão SUS.');", true);
                return;
            }

            if (prontuario == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert(' - Selecione um paciente atendido pelo Urgência e Emergência.');", true);
                return;
            }

            if (pacienteViverMais == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert(' - Selecione um paciente com cartão SUS.');", true);
                return;
            }

            Prontuario registroAberto = iProntuario.BuscarProntuarioAberto<ViverMais.Model.EstabelecimentoSaude, Prontuario>(pacienteViverMais.Codigo, iUnidade.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(prontuario.CodigoUnidade));

            if (registroAberto != null)
            {
                string msg = string.Empty;

                if (registroAberto.Situacao.Codigo == SituacaoAtendimento.ATENDIMENTO_INICIAL)
                    msg = "na fila de acolhimento";
                else if (registroAberto.Situacao.Codigo == SituacaoAtendimento.AGUARDANDO_ATENDIMENTO)
                    msg = "na fila de atendimento";
                else if (registroAberto.Situacao.Codigo == SituacaoAtendimento.EM_OBSERVACAO_UNIDADE)
                    msg = "em observação";
                else if (registroAberto.Situacao.Codigo == SituacaoAtendimento.AGUARDANDO_REGULACAO_ENFERMARIA)
                    msg = "aguardando regulação para enfermaria";
                else
                    msg = "aguardando regulação para uti";

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", @"alert('Usuário, não é possível identificar o paciente para o registro de número: " + prontuario.NumeroToString + 
                    ", pois o paciente de cartão sus escolhido já se encontra nesta unidade " + msg + ".');", true);
                return;
            }

            PacienteUrgence pacienteUrgence = prontuario.Paciente;
            pacienteUrgence.CodigoViverMais = pacienteViverMais.Codigo;
            iPacienteUrgencia.Atualizar(pacienteUrgence);
            iPacienteUrgencia.Inserir(new LogUrgencia(DateTime.Now, ((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, 3, "ID:" + pacienteUrgence.Codigo));

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Paciente identificado com sucesso!');", true);

            this.WUC_ExibirAtendimento.Prontuario = null;
            this.WUC_ExibirAtendimento.Visible = false;
            this.WUC_PesquisarAtendimento.WUC_GridViewAtendimentos.SelectedIndex = -1;
            this.CarregaPacientesSemIdentificacao(((Usuario)Session["Usuario"]).Unidade.CNES);
            this.WUC_PesquisarAtendimento.WUC_UpdatePanel_ResultadoPesquisa.Update();

            this.WUC_ExibirPaciente.Paciente = null;
            this.WUC_ExibirPaciente.WUC_PanelDadosPaciente.Visible = false;
            this.WUC_ExibirPaciente.WUC_UpdatePanelExibirPaciente.Update();
            this.WUC_PesquisarPaciente.GridView.SelectedIndex = -1;
            this.WUC_PesquisarPaciente.GridView.DataSource = (IList<ViverMais.Model.Paciente>)Session["WUCPacientes"];
            this.WUC_PesquisarPaciente.GridView.DataBind();
            this.WUC_PesquisarPaciente.WUC_UpdatePanel_ResultadoPesquisa.Update();
        }
    }
}
