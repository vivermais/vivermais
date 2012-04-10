using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Data;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using System.Drawing;
using System.Text;
using ViverMais.View;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using System.Threading;
using ViverMais.View.Urgencia;

namespace Urgence.View
{
    public partial class FormEvolucaoEnfermagem : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InserirTrigger(this.LinkButton_HistoricoEnfermagem.UniqueID, "Click", this.UpdatePanel_HistoricoEvolucao);

            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_EVOLUCAO_ENFERMAGEM",Modulo.URGENCIA))
                {
                    ViverMais.Model.UsuarioProfissionalUrgence up = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<ViverMais.Model.UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);

                    if (up != null)
                    {
                        if (up.UnidadeVinculo == ((Usuario)Session["Usuario"]).Unidade.CNES)
                        {
                            ICBO iCbo = Factory.GetInstance<ICBO>();
                            CBO cbo = iCbo.BuscarPorCodigo<CBO>(Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo).CodigoCBO);
                            
                            if (iCbo.CBOPertenceEnfermeiro<CBO>(cbo))
                            {
                                long temp;
                                if (Request["codigo"] != null && long.TryParse(Request["codigo"].ToString(), out temp))
                                {
                                    Session["URL_UrgenciaVoltarAprazamento"] = HttpContext.Current.Request.Url.AbsoluteUri;

                                    Factory.GetInstance<IPrescricao>().AtualizarStatusPrescricoesProntuario(long.Parse(Request["codigo"].ToString()));

                                    ViverMais.Model.Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<ViverMais.Model.Prontuario>(long.Parse(Request["codigo"].ToString()));
                                    ViewState["co_prontuario"] = prontuario.Codigo;

                                    if (prontuario != null)
                                    {
                                        lblNumero.Text = prontuario.NumeroToString;
                                        lblData.Text = prontuario.Data.ToString("dd/MM/yyyy");

                                        lblPaciente.Text = prontuario.NomePacienteToString;
                                            //string.IsNullOrEmpty(prontuario.Paciente.Nome) ? "Não Identificado" : prontuario.Paciente.Nome;

                                        //if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
                                        //{
                                        //    ViverMais.Model.Paciente pacienteViverMais = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

                                        //    if (pacienteViverMais != null)
                                        //        lblPaciente.Text = pacienteViverMais.Nome;
                                        //}
                                    }
                                }
                            }
                            else
                                Response.Redirect("FormAcessoNegado.aspx?opcao=3");
                                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, somente profissionais de enfermagem tem acesso a esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                        }
                        else
                            Response.Redirect("FormAcessoNegado.aspx?opcao=4");
                            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, o seu profissional identificado não possui vínculo com a sua atual unidade. Por favor, entrar em contato com a administração.');location='Default.aspx';", true);
                    }else
                        Response.Redirect("FormAcessoNegado.aspx?opcao=5");
                        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Profissional não identificado! Por favor, identifique o usuário.');location='Default.aspx';", true);
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        //protected void OnItemDataBound_ConfiguraDataList(object sender, DataListItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item)
        //    {
        //        Literal conteudo = (Literal)e.Item.FindControl("Literal_Conteudo");
        //        StringBuilder builder = new StringBuilder();
        //        builder.Append("<p style=\"word-wrap:break-word;\">");
        //        builder.Append(conteudo.Text);
        //        builder.Append("</p>");
        //        conteudo.Text = builder.ToString();
        //        //conteudo.DataBind();
        //    }
        //}

        private void InserirTrigger(string idcontrole, string eventname, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = idcontrole;
            trigger.EventName = eventname;
            updatepanel.Triggers.Add(trigger);
            MasterMain mm = (MasterMain)Master.Master;
            ((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(this.FindControl(idcontrole));
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //private bool VerificaFuncionalidadesEnfermeiro()
        //{
        //    UsuarioProfissionalUrgence u = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
            
        //    if (u != null)
        //    {
        //        CategoriaOcupacao cat = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<CBO>(u.CodigoCBO).CategoriaOcupacao;

        //        if (cat != null && cat.Codigo == "25")
        //            return true;
        //    }

        //    return false;
        //}

        protected void OnClick_SalvarEvolucaoEnfermagem(object sender, EventArgs e)
        {
            ViverMais.Model.Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<ViverMais.Model.Prontuario>(long.Parse(ViewState["co_prontuario"].ToString()));
            ViverMais.Model.EvolucaoEnfermagem evolucao = new ViverMais.Model.EvolucaoEnfermagem();

            evolucao.Prontuario = prontuario;
            evolucao.Observacao = TextBox_ObservacaoEvolucaoEnfermagem.Text.ToUpper();
            evolucao.Data = DateTime.Now;
            UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
            evolucao.CodigoProfissional = usuarioprofissional.Id_Profissional;
            evolucao.CBOProfissional = usuarioprofissional.CodigoCBO;
            evolucao.Profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(evolucao.CodigoProfissional);

            try
            {
                Factory.GetInstance<IEvolucaoEnfermagem>().Salvar(evolucao);
                TextBox_ObservacaoEvolucaoEnfermagem.Text = "";
                //Registro de Log
                Factory.GetInstance<IUrgenciaServiceFacade>().Inserir(new LogUrgencia(DateTime.Now, 1, 16, "id evolucao:" + evolucao.Codigo));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados salvos com sucesso!');", true);
            }
            catch (Exception f)
            {
                throw f;
            }
            HelperView.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prontuario.Codigo);

            //try
            //{
            //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuarioprofissional.Id_Usuario, prontuario.Codigo); });
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

        protected void TextBox_ObservacaoEvolucaoEnfermagem_TextChanged(object sender, EventArgs e) 
        {
            TextBox_ObservacaoEvolucaoEnfermagem.Text = TextBox_ObservacaoEvolucaoEnfermagem.Text.ToUpper();
        }

        /// <summary>
        /// Mostra o histórico das evoluções de enfermagem do paciente ao decorrer de sua estadia
        /// na unidade de atendimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_HistoricoEnfermagem(object sender, EventArgs e)
        {
            this.DataList_HistoricoEnfermagem.DataSource = Factory.GetInstance<IProntuario>().RetornaHistoricoEnfermagem(long.Parse(ViewState["co_prontuario"].ToString()));
            this.DataList_HistoricoEnfermagem.DataBind();
            this.Panel_Evolucoes.Visible = true;
            this.UpdatePanel_HistoricoEvolucao.Update();
        }

        protected void OnClick_FecharHistoricoEvolucoes(object sender, EventArgs e)
        {
            this.Panel_Evolucoes.Visible = false;
            //this.LinkButton_HistoricoEnfermagem.Focus();
        }
    }
}
