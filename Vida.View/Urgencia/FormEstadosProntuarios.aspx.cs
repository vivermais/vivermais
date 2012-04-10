using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using System.Globalization;
using System.Data;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Urgencia
{
    public partial class FormEstadosProntuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int temp;

                if (Request["regulacao"] != null && int.TryParse(Request["regulacao"].ToString(), out temp))
                {
                    Label_Chamada.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.SituacaoAtendimento>(int.Parse(Request["regulacao"].ToString())).Nome.ToLower());

                    IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
                    Usuario usuario = (Usuario)Session["Usuario"];

                    bool acesso_medico = iUsuarioProfissional.VerificarAcessoMedico(usuario.Codigo, "CADASTRAR_EVOLUCAO_MEDICA", usuario.Unidade.CNES);
                    bool acesso_enfermagem = iUsuarioProfissional.VerificarAcessoEnfermeiro(usuario.Codigo, "CADASTRAR_EVOLUCAO_ENFERMAGEM", usuario.Unidade.CNES);
                    bool acesso_auxiliartecnicoenfermagem = iUsuarioProfissional.VerificarAcessoAuxiliarTecnicoEnfermagem(usuario.Codigo, "CONFIRMAR_APRAZAMENTO", usuario.Unidade.CNES);

                    if (!acesso_enfermagem && !acesso_auxiliartecnicoenfermagem)
                        this.GridView_Prontuarios.Columns[4].Visible = false;

                    if (!acesso_medico)
                        this.GridView_Prontuarios.Columns[5].Visible = false;

                    CarregaProntuarios(int.Parse(Request["regulacao"].ToString()));
                }
            }
        }

        /// <summary>
        /// Carrega os prontuarios da unidade do usuário logado e que estejam em observação
        /// </summary>
        private void CarregaProntuarios(int co_situacao)
        {
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            IList<Prontuario> prontuarios = iProntuario.BuscarPorUnidade<Prontuario>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES, co_situacao);

            GridView_Prontuarios.DataSource = prontuarios;
            GridView_Prontuarios.DataBind();
        }

        /// <summary>
        /// Redireciona o enfermeiro ou técnico
        /// de enfermagem para sua página específica para realizar a evolução de enfermagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_EvolucaoEnfermagem(object sender, EventArgs e)
        {
            UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);

            if (usuarioprofissional != null)
            {
                ICBO iCbo = Factory.GetInstance<ICBO>();
                CBO cbo = iCbo.BuscarPorCodigo<CBO>(usuarioprofissional.CodigoCBO);

                if (iCbo.CBOPertenceAuxiliarTecnicoEnfermagem<CBO>(cbo))
                {
                    ViewState["co_prontuario"] = ((LinkButton)sender).CommandArgument.ToString();
                    PanelIdentificarProfissional.Visible = true;
                    //base.SetFocus(this.Button_ValidarProfissional);
                }
                else
                    Response.Redirect("FormEvolucaoEnfermagem.aspx?codigo=" + ((LinkButton)sender).CommandArgument.ToString()); //Enfermeiro
            }
        }

        protected void OnClick_ValidarIdentificacao(object sender, EventArgs e)
        {
            try
            {
                UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoIdentificacao<UsuarioProfissionalUrgence>(TextBox_CodigoIdentificacao.Text, ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);

                if (usuarioprofissional == null)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi encontrado profissional algum com o código informado. Por favor, tente novamente.');", true);
                else
                {
                    ViverMais.Model.Profissional profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(usuarioprofissional.Id_Profissional);
                    Session["UsuarioTecnico"] = usuarioprofissional;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "function",
                    "javascript:document.getElementById('cinza').style.display='none';javascript:document.getElementById('mensagem').style.display='none';alert('Olá: " + profissional.Nome + ".');location='FormConfirmarAprazamento.aspx?codigo=" + ViewState["co_prontuario"].ToString() + "';void(0)", true);
                }
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        protected void OnClick_FecharJanelaIdentificacaoProfissional(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "function",
                    "javascript:document.getElementById('cinza').style.display='none';javascript:document.getElementById('mensagem').style.display='none';void(0)", true);
        }

        protected void OnClick_EvolucaoMedica(object sender, EventArgs e)
        {
            Session["tempo_atendimento"] = 0;
            Response.Redirect("FormEvolucaoMedica.aspx?codigo=" + ((LinkButton)sender).CommandArgument.ToString(), false);
        }

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        int temp;

        //        if (Request["regulacao"] != null && int.TryParse(Request["regulacao"].ToString(), out temp))
        //        {
        //            Label_Chamada.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.SituacaoAtendimento>(int.Parse(Request["regulacao"].ToString())).Nome.ToLower());
                    
        //            IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
        //            Usuario usuario = (Usuario)Session["Usuario"];

        //            bool acesso_medico = iUsuarioProfissional.VerificarAcessoMedico(usuario.Codigo, "CADASTRAR_EVOLUCAO_MEDICA", usuario.Unidade.CNES);
        //            bool acesso_enfermagem = iUsuarioProfissional.VerificarAcessoEnfermeiro(usuario.Codigo, "CADASTRAR_EVOLUCAO_ENFERMAGEM", usuario.Unidade.CNES);
        //            bool acesso_auxiliartecnicoenfermagem = iUsuarioProfissional.VerificarAcessoAuxiliarTecnicoEnfermagem(usuario.Codigo, "CONFIRMAR_APRAZAMENTO", usuario.Unidade.CNES);

        //            if (!acesso_enfermagem && !acesso_auxiliartecnicoenfermagem)
        //                this.GridView_Prontuarios.Columns[4].Visible = false;

        //            if (!acesso_medico)
        //                this.GridView_Prontuarios.Columns[5].Visible = false;

        //            CarregaProntuarios(int.Parse(Request["regulacao"].ToString()));
        //        }
        //    }
        //}

        ///// <summary>
        ///// Carrega os prontuarios da unidade do usuário logado e que estejam em observação
        ///// </summary>
        //private void CarregaProntuarios(int co_situacao)
        //{
        //    IProntuario iProntuario         = Factory.GetInstance<IProntuario>();
        //    IList<Prontuario> prontuarios   = iProntuario.BuscarPorUnidade<Prontuario>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES,co_situacao).OrderBy(p => p.ClassificacaoRisco.Codigo).ToList();
        //    DataTable datatable = iProntuario.getDataTablePronturario<IList<ViverMais.Model.Prontuario>>(prontuarios);
        //    Session["RegulacaoUrgencia"] = datatable;
        //    GridView_Prontuarios.DataSource = datatable;
        //    GridView_Prontuarios.DataBind();
        //}

        ///// <summary>
        ///// Paginação do GridView
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnSelectedIndexChanging_Paginacao(object sender, GridViewPageEventArgs e) 
        //{
        //    GridView_Prontuarios.DataSource = (DataTable)Session["RegulacaoUrgencia"];
        //    GridView_Prontuarios.DataBind();
            
        //    GridView_Prontuarios.PageIndex  = e.NewPageIndex;
        //    GridView_Prontuarios.DataBind();
        //}

        ///// <summary>
        ///// Redireciona o enfermeiro ou técnico
        ///// de enfermagem para sua página específica para realizar a evolução de enfermagem
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnClick_EvolucaoEnfermagem(object sender, EventArgs e)
        //{
        //    UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);

        //    if (usuarioprofissional != null)
        //    {
        //        ICBO iCbo = Factory.GetInstance<ICBO>();
        //        CBO cbo = iCbo.BuscarPorCodigo<CBO>(usuarioprofissional.CodigoCBO);

        //        //if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "APRAZAR")) //Perfil de técnico
        //        if (iCbo.CBOPertenceAuxiliarTecnicoEnfermagem<CBO>(cbo))
        //        {
        //            ViewState["co_prontuario"] = ((LinkButton)sender).CommandArgument.ToString();
        //            PanelIdentificarProfissional.Visible = true;
        //        }
        //        //Response.Redirect("FormConfirmarSenhaUnica.aspx?codigo=" + ((LinkButton)sender).CommandArgument.ToString() + "&regulacao=" + ViewState["co_situacao"].ToString());
        //        else
        //            Response.Redirect("FormEvolucaoEnfermagem.aspx?codigo=" + ((LinkButton)sender).CommandArgument.ToString()); //Enfermeiro
        //    }
        //}

        //protected void OnClick_ValidarIdentificacao(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        UsuarioProfissionalUrgence usuarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoIdentificacao<UsuarioProfissionalUrgence>(TextBox_CodigoIdentificacao.Text, ((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES);

        //        if (usuarioprofissional == null)
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi encontrado profissional algum com o código informado. Por favor, tente novamente.');", true);
        //        else
        //        {
        //            ViverMais.Model.Profissional profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(usuarioprofissional.Id_Profissional);
        //            Session["UsuarioTecnico"] = usuarioprofissional;
        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "function",
        //            "javascript:document.getElementById('cinza').style.display='none';javascript:document.getElementById('mensagem').style.display='none';alert('Olá: " + profissional.Nome + ".');location='FormConfirmarAprazamento.aspx?codigo=" + ViewState["co_prontuario"].ToString() + "';void(0)", true);

        //            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Olá: " + p.Nome + ".');location='FormEvolucaoEnfermagem.aspx?codigo=" + ViewState["codigo"].ToString() + "';", true);
        //        }
        //    }
        //    catch (Exception f)
        //    {
        //        throw f;
        //    }
        //}

        //protected void OnClick_FecharJanelaIdentificacaoProfissional(object sender, EventArgs e) 
        //{
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "function",
        //            "javascript:document.getElementById('cinza').style.display='none';javascript:document.getElementById('mensagem').style.display='none';void(0)", true);
        //}

        //protected void OnClick_EvolucaoMedica(object sender, EventArgs e) 
        //{
        //    Session["tempo_atendimento"] = 0;
        //    Response.Redirect("FormEvolucaoMedica.aspx?codigo=" + ((LinkButton)sender).CommandArgument.ToString());
        //}
    }
}
