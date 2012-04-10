using System;
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
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.Model;
using System.Text.RegularExpressions;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Urgencia
{
    public partial class BuscaProntuario : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ImageButton botaopesquisar = this.WUC_PesquisarPaciente.WUC_BotaoPesquisar;
            botaopesquisar.Click += new ImageClickEventHandler(OnClick_BuscarPaciente);

            CarregaTriggersUpdatePanel();

            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "BUSCAR_REGISTROS_ATENDIMENTO", Modulo.URGENCIA))
                {
                    Session.Remove("RegistrosPesquisados");
                    this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.ImageUrl = "~/Urgencia/img/pesquisarcartao1.png";
                    this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Attributes.Add("onmouseover", "this.src='img/pesquisarcartao2.png';");
                    this.WUC_PesquisarPaciente.WUC_BotaoPesquisar.Attributes.Add("onmouseout", "this.src='img/pesquisarcartao1.png';");

                    LinkButton botaobiometria = this.WUC_PesquisarPaciente.WUC_BotaoBiometria;
                    botaobiometria.PostBackUrl = "~/Urgencia/FormBiometriaBuscarRegistroEletronico.aspx";

                    this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Src = "~/Urgencia/img/bts/id_biometrica1.png";
                    this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Attributes.Add("onmouseover", "this.src='img/bts/id_biometrica2.png';");
                    this.WUC_PesquisarPaciente.WUC_ImagemBiometria.Attributes.Add("onmouseout", "this.src='img/bts/id_biometrica1.png';");

                    CarregaSituacoes();
                    CarregaClassificacaoRisco();

                    //IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
                    //bool acesso_medico = iUsuarioProfissional.VerificarAcessoMedico(usuario.Codigo, "BUSCAR_REGISTROS_ATENDIMENTO", usuario.Unidade.CNES);
                    //bool acess_enfemeiro = iUsuarioProfissional.VerificarAcessoEnfermeiro(usuario.Codigo, "BUSCAR_REGISTROS_ATENDIMENTO", usuario.Unidade.CNES);
                    //bool acesso_auxiliartecnico_enfermagem = iUsuarioProfissional.VerificarAcessoAuxiliarTecnicoEnfermagem(usuario.Codigo, "BUSCAR_REGISTROS_ATENDIMENTO", usuario.Unidade.CNES);

                    //ViewState["informacao_paciente_restrita"] = true;

                    //if (acesso_medico || acess_enfemeiro || acesso_auxiliartecnico_enfermagem)
                    //    ViewState["informacao_paciente_restrita"] = false;

                    if (Request["co_paciente"] != null)
                    {
                        //ViewState["tipo_busca"] = "paciente";
                        //ViewState["co_paciente"] = Request["codigo"].ToString();
                        CarregaBuscaProntuarioPorPaciente(Request["co_paciente"].ToString());
                    }
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        private void CarregaTriggersUpdatePanel()
        {
            ImageButton botaopesquisarpaciente = this.WUC_PesquisarPaciente.WUC_BotaoPesquisar;
            //MasterMain mm = (MasterMain)Master.Master;
            //((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(btnPesquisar);
            //((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(btnPesquisar2);
            //((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(btnPesquisar3);
            //((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(botaopesquisarpaciente);
            //((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(GridView_ResultadoPesquisaPaciente);

            AsyncPostBackTrigger trig = new AsyncPostBackTrigger();
            trig.ControlID = btnPesquisar.UniqueID;
            trig.EventName = "Click";
            UpdatePanel_Um.Triggers.Add(trig);

            trig = new AsyncPostBackTrigger();
            trig.ControlID = btnPesquisar2.UniqueID;
            trig.EventName = "Click";
            UpdatePanel_Um.Triggers.Add(trig);

            trig = new AsyncPostBackTrigger();
            trig.ControlID = btnPesquisar3.UniqueID;
            trig.EventName = "Click";
            UpdatePanel_Um.Triggers.Add(trig);

            trig = new AsyncPostBackTrigger();
            trig.ControlID = botaopesquisarpaciente.UniqueID;
            trig.EventName = "Click";
            UpdatePanel_Um.Triggers.Add(trig);

            trig = new AsyncPostBackTrigger();
            trig.ControlID = btnPesquisar.UniqueID;
            trig.EventName = "Click";
            UpdatePanel_Prontuarios.Triggers.Add(trig);

            trig = new AsyncPostBackTrigger();
            trig.ControlID = btnPesquisar2.UniqueID;
            trig.EventName = "Click";
            UpdatePanel_Prontuarios.Triggers.Add(trig);

            trig = new AsyncPostBackTrigger();
            trig.ControlID = btnPesquisar3.UniqueID;
            trig.EventName = "Click";
            UpdatePanel_Prontuarios.Triggers.Add(trig);

            trig = new AsyncPostBackTrigger();
            trig.ControlID = botaopesquisarpaciente.UniqueID;
            trig.EventName = "Click";
            UpdatePanel_Prontuarios.Triggers.Add(trig);

            trig = new AsyncPostBackTrigger();
            trig.ControlID = GridView_ResultadoPesquisaPaciente.UniqueID;
            trig.EventName = "RowCommand";
            UpdatePanel_Prontuarios.Triggers.Add(trig);
        }

        /// <summary>
        /// Carrega as situações existentes
        /// </summary>
        private void CarregaSituacoes()
        {
            ddlSituacao.DataValueField = "Codigo";
            ddlSituacao.DataTextField = "Nome";

            IList<ViverMais.Model.SituacaoAtendimento> situacoes = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.SituacaoAtendimento>().OrderBy(s => s.Nome).ToList();

            ddlSituacao.DataSource = situacoes;
            ddlSituacao.DataBind();

            ddlSituacao.Items.Insert(0, new ListItem("Selecione...", "0"));
        }

        /// <summary>
        /// Carrega as classificações de risco
        /// </summary>
        private void CarregaClassificacaoRisco()
        {
            IList<ViverMais.Model.ClassificacaoRisco> classificacoes = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.ClassificacaoRisco>().OrderBy(p => p.Cor).ToList();

            ddlSituacaoRisco.DataValueField = "Codigo";
            ddlSituacaoRisco.DataTextField = "Cor";

            ddlSituacaoRisco.DataSource = classificacoes;
            ddlSituacaoRisco.DataBind();

            ddlSituacaoRisco.Items.Insert(0, new ListItem("Selecione...", "0"));
        }

        /// <summary>
        /// Pesquisa de prontuário por situação de atendimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPesquisar2_Click(object sender, EventArgs e)
        {
            //ViewState["tipo_busca"] = "situacao";
            //ViewState["situacao_busca"] = ddlSituacao.SelectedValue;
            //ViewState["data_busca"] = tbxData.Text;

            this.CarregaBuscaProntuarioPorDataSituacao(int.Parse(ddlSituacao.SelectedValue), DateTime.Parse(tbxData.Text));

            UpdatePanel_Prontuarios.Update();
            UpdatePanel_Um.Update();

            CarregaTriggersUpdatePanel();
        }

        private void CarregaBuscaProntuarioPorDataSituacao(int co_situacao, DateTime data)
        {
            IList<ViverMais.Model.Prontuario> prontuarios = new List<ViverMais.Model.Prontuario>();
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            prontuarios = iProntuario.BuscarPorSituacao<ViverMais.Model.Prontuario>(co_situacao, data, ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);

            //.Where(p => data.ToString("dd/MM/yyyy") == p.Data.ToString("dd/MM/yyyy") && p.CodigoUnidade == ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES).ToList();
            IPaciente iPaciente = Factory.GetInstance<IPaciente>();

            foreach (ViverMais.Model.Prontuario prontuario in prontuarios)
            {
                if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
                    prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);
            }

            Session["RegistrosPesquisados"] = prontuarios;

            gridProntuario.DataSource = prontuarios;
            gridProntuario.DataBind();

            Panel_ResultadoBusca.Visible = true;
            Panel_ResultadoPaciente.Visible = false;
        }

        /// <summary>
        /// Pesquisa de prontuário por número
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnPesquisar_Click(object sender, EventArgs e)
        {
            ViverMais.Model.Prontuario prontuario = new ViverMais.Model.Prontuario();
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            prontuario = iProntuario.BuscarPorNumeroProntuario<ViverMais.Model.Prontuario>(long.Parse(tbxNumero.Text), ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);
            IPaciente iPaciente = Factory.GetInstance<IPaciente>();

            IList<ViverMais.Model.Prontuario> prontuarios = new List<ViverMais.Model.Prontuario>();

            if (prontuario != null)
            {
                if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
                    prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

                prontuarios.Add(prontuario);
            }

            Session["RegistrosPesquisados"] = prontuarios;

            gridProntuario.DataSource = prontuarios;
            gridProntuario.DataBind();

            Panel_ResultadoBusca.Visible = true;
            Panel_ResultadoPaciente.Visible = false;

            UpdatePanel_Prontuarios.Update();
            UpdatePanel_Um.Update();

            CarregaTriggersUpdatePanel();
        }

        /// <summary>
        /// Pesquisa de prontuário por classificação de risco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnPesquisar3_Click(object sender, EventArgs e)
        {
            //ViewState["tipo_busca"] = "classificacao";
            //ViewState["classificacao_busca"] = ddlSituacaoRisco.SelectedValue;
            //ViewState["data_busca"] = tbxData2.Text;
            CarregaBuscaProntuarioPorClassificacaoData(int.Parse(ddlSituacaoRisco.SelectedValue), DateTime.Parse(tbxData2.Text));

            UpdatePanel_Prontuarios.Update();
            UpdatePanel_Um.Update();

            CarregaTriggersUpdatePanel();
        }

        private void CarregaBuscaProntuarioPorClassificacaoData(int co_classificacao, DateTime data)
        {
            IList<ViverMais.Model.Prontuario> prontuarios = new List<ViverMais.Model.Prontuario>();
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            prontuarios = iProntuario.BuscarPorClassificacaoRisco<ViverMais.Model.Prontuario>(co_classificacao, data, ((Usuario)Session["Usuario"]).Unidade.CNES);
            //.Where(c => c.Data.ToString("dd/MM/yyyy") == data.ToString("dd/MM/yyyy") && c.CodigoUnidade == ((Usuario)Session["Usuario"]).Unidade.CNES).ToList();
            IPaciente iPaciente = Factory.GetInstance<IPaciente>();

            foreach (ViverMais.Model.Prontuario pron in prontuarios)
            {
                if (!string.IsNullOrEmpty(pron.Paciente.CodigoViverMais))
                    pron.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(pron.Paciente.CodigoViverMais);
            }

            Session["RegistrosPesquisados"] = prontuarios;

            gridProntuario.DataSource = prontuarios;
            gridProntuario.DataBind();

            Panel_ResultadoBusca.Visible = true;
            Panel_ResultadoPaciente.Visible = false;
        }

        protected void lnkBiometria_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormBiometriaBuscarRegistroEletronico.aspx");
        }

        protected void OnClick_BuscarPaciente(object sender, ImageClickEventArgs e)
        {
            if (WUC_PesquisarPaciente.Page.IsValid)
            {
                this.WUC_PesquisarPaciente.GridView.Visible = false;
                this.WUC_PesquisarPaciente.WUC_UpdatePanel_ResultadoPesquisa.Update();

                string cartaosus = this.WUC_PesquisarPaciente.WUC_CartaoSUSPesquisado;
                string datanascimento = this.WUC_PesquisarPaciente.WUC_DataNascimentoPesquisado;
                string nomepaciente = this.WUC_PesquisarPaciente.WUC_PacientePesquisado;
                string nomemae = this.WUC_PesquisarPaciente.WUC_MaePesquisado;

                //ViewState["tipo_busca"] = "paciente";
                IPaciente iPaciente = Factory.GetInstance<IPaciente>();

                if (!string.IsNullOrEmpty(cartaosus))
                {
                    ViverMais.Model.Paciente paciente = iPaciente.PesquisarPacientePorCNS<ViverMais.Model.Paciente>(cartaosus);

                    //if (paciente != null)
                    //    ViewState["co_paciente"] = paciente.Codigo;

                    CarregaBuscaProntuarioPorPaciente(paciente != null ? paciente.Codigo : "");
                    Panel_ResultadoPaciente.Visible = false;
                }
                else
                {
                    IList<ViverMais.Model.Paciente> pacientes = iPaciente.PesquisarPaciente<ViverMais.Model.Paciente>(nomepaciente, !string.IsNullOrEmpty(nomemae) ? nomemae : "", !string.IsNullOrEmpty(datanascimento) ? DateTime.Parse(datanascimento) : DateTime.MinValue);
                    GridView_ResultadoPesquisaPaciente.DataSource = pacientes;
                    GridView_ResultadoPesquisaPaciente.DataBind();
                    Panel_ResultadoPaciente.Visible = true;
                    Panel_ResultadoBusca.Visible = false;
                }

                UpdatePanel_Prontuarios.Update();
                UpdatePanel_Um.Update();

                CarregaTriggersUpdatePanel();
            }
            //else
            //{
                //ValidatorCollection validators = WUC_PesquisarPaciente.Page.GetValidators("WUCPesquisarPaciente");
                //foreach (IValidator iValidator in validators)
                //{
                //ValidatorCollection validators = WUC_PesquisarPaciente.Page.Request.
                //CustomValidator 
                //validators[0].GetType()
                //    iValidator.Validate();
                //}
            //}
        }

        private void CarregaBuscaProntuarioPorPaciente(string co_paciente)
        {
            IList<Prontuario> prontuarios = null;

            if (!string.IsNullOrEmpty(co_paciente))
                prontuarios = Factory.GetInstance<IProntuario>().BuscarPorPacienteViverMais<Prontuario>(co_paciente, ((Usuario)Session["Usuario"]).Unidade.CNES);
            //.Where(p => p.CodigoUnidade == ((Usuario)Session["Usuario"]).Unidade.CNES).ToList();
            else
                prontuarios = new List<Prontuario>();

            Session["RegistrosPesquisados"] = prontuarios;

            gridProntuario.DataSource = prontuarios;
            gridProntuario.DataBind();

            Panel_ResultadoBusca.Visible = true;
        }

        protected void OnRowCommand_VerProntuarios(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_VerProntuarios")
            {
                ViewState["co_paciente"] = GridView_ResultadoPesquisaPaciente.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString();
                CarregaBuscaProntuarioPorPaciente(GridView_ResultadoPesquisaPaciente.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString());

                UpdatePanel_Prontuarios.Update();
                UpdatePanel_Um.Update();

                CarregaTriggersUpdatePanel();
            }
        }

        protected void OnPageIndexChanging_PaginacaoGridViewProntuarios(object sende, GridViewPageEventArgs e)
        {
            //if (ViewState["tipo_busca"] != null)
            //{
                //switch (ViewState["tipo_busca"].ToString())
                //{
                //    case "situacao":
                        //CarregaBuscaProntuarioPorDataSituacao(int.Parse(ViewState["situacao_busca"].ToString()), DateTime.Parse(ViewState["data_busca"].ToString()));
                    //    break;
                    //case "classificacao":
                        //CarregaBuscaProntuarioPorClassificacaoData(int.Parse(ViewState["classificacao_busca"].ToString()), DateTime.Parse(ViewState["data_busca"].ToString()));
                    //    break;
                    //case "paciente":
                        //CarregaBuscaProntuarioPorPaciente(ViewState["co_paciente"].ToString());
                //        break;
                //}

                gridProntuario.DataSource = (IList<ViverMais.Model.Prontuario>)Session["RegistrosPesquisados"];
                gridProntuario.DataBind();

                gridProntuario.PageIndex = e.NewPageIndex;
                gridProntuario.DataBind();
            //}
        }

        //protected void OnRowDataBound_RegistrosPesquisados(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if ((bool)ViewState["informacao_paciente_restrita"])
        //        {
        //            long co_prontuario = long.Parse(gridProntuario.DataKeys[e.Row.RowIndex]["Codigo"].ToString());
        //            IList<ViverMais.Model.Prontuario> registros = (IList<ViverMais.Model.Prontuario>)Session["RegistrosPesquisados"];
        //            ViverMais.Model.Prontuario registro = registros.Where(p => p.Codigo == co_prontuario).First();

        //            if (registro.Situacao.Codigo == SituacaoAtendimento.ALTA_MEDICA ||
        //                registro.Situacao.Codigo == SituacaoAtendimento.ALTA_PEDIDO ||
        //                registro.Situacao.Codigo == SituacaoAtendimento.EVASAO ||
        //                registro.Situacao.Codigo == SituacaoAtendimento.FINALIZADO ||
        //                registro.Situacao.Codigo == SituacaoAtendimento.TRANSFERENCIA)
        //                ((DataControlFieldCell)e.Row.Controls[3]).Text = "Ausente no P.A";
        //            else
        //                ((DataControlFieldCell)e.Row.Controls[3]).Text = "Presente no P.A";
        //        }
        //    }
        //}
    }
}
