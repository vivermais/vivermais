using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;

namespace ViverMais.View.Urgencia
{
    public partial class Inc_AtestadoReceita : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long co_prontuario;
                if (Request["codigo"] != null && long.TryParse(Request["codigo"].ToString(), out co_prontuario))
                {
                    //Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<ViverMais.Model.Prontuario>(long.Parse(Request["codigo"].ToString()));
                    //IList<Prontuario> prontuarios = new List<Prontuario>();
                    //prontuarios.Add(prontuario);
                    //GridView_AtestadoReceitaConsultaMedica.DataSource = prontuarios;
                    //GridView_AtestadoReceitaConsultaMedica.DataBind();

                    //ViewState["co_prontuarioatestadoreceita"] = prontuario.Codigo;
                    Session.Remove("medicamentosPrescristos");
                    Session.Remove("procedimentosPrescristos");
                    Session.Remove("procedimentosNaoFaturaveisPrescristos");
                    CarregaAtestatdosReceitasEvolucoes(co_prontuario);
                }
            }
        }

        private void CarregaAtestatdosReceitasEvolucoes(long co_prontuarrio)
        {
            IEvolucaoMedica iEvolucao = Factory.GetInstance<IEvolucaoMedica>();
            IList<EvolucaoMedica> evolucoes = iEvolucao.BuscarPorProntuario<EvolucaoMedica>(co_prontuarrio).OrderBy(p => p.Data).ToList();
            EvolucaoMedica[] consulta = new EvolucaoMedica[1];
            consulta[0] = iEvolucao.BuscarConsultaMedica<EvolucaoMedica>(co_prontuarrio);

            this.GridView_AtestadoReceitaConsultaMedica.DataSource = consulta;
            this.GridView_AtestadoReceitaConsultaMedica.DataBind();

            Session["evolucoesProntuario"] = evolucoes;
            this.GridView_AtestadoReceitaEvolucaoMedica.DataSource = evolucoes;
            this.GridView_AtestadoReceitaEvolucaoMedica.DataBind();
        }

        /// <summary>
        /// Paginação para o gridview dos atestados e receitas das evoluções médicas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoAtestadoReceitaEvolucaoesMedica(object sender, GridViewPageEventArgs e)
        {
            //CarregaAtestatdosReceitasEvolucoes(long.Parse(ViewState["co_prontuarioatestadoreceita"].ToString()));
            this.GridView_AtestadoReceitaEvolucaoMedica.DataSource = (EvolucaoMedica[])Session["evolucoesProntuario"];
            this.GridView_AtestadoReceitaEvolucaoMedica.DataBind();

            this.GridView_AtestadoReceitaEvolucaoMedica.PageIndex = e.NewPageIndex;
            this.GridView_AtestadoReceitaEvolucaoMedica.DataBind();
        }

        //protected void OnClick_GerarAtestadoConsulta(object sender, EventArgs e)
        //{
        //    UsuarioProfissionalUrgence ususarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Atestado','../FormGerarAtestadoReceita.aspx?model=attested&co_prontuario=" + ((ImageButton)sender).CommandArgument.ToString() + "&co_profissional=" + ususarioprofissional.Id_Profissional + "&cbo_profissional=" + ususarioprofissional.CodigoCBO + "');", true);
        //}

        //protected void OnClick_GerarReceitaConsulta(object sender, EventArgs e)
        //{
        //    UsuarioProfissionalUrgence ususarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Receita','../FormGerarAtestadoReceita.aspx?model=prescription&co_prontuario=" + ((ImageButton)sender).CommandArgument.ToString() + "&co_profissional=" + ususarioprofissional.Id_Profissional + "&cbo_profissional=" + ususarioprofissional.CodigoCBO + "');", true);
        //}

        //protected void OnClick_GerarComparecimentoConsulta(object sender, EventArgs e)
        //{
        //    UsuarioProfissionalUrgence ususarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Comparecimento','../FormGerarAtestadoReceita.aspx?model=attendance&co_prontuario=" + ((ImageButton)sender).CommandArgument.ToString() + "&co_profissional=" + ususarioprofissional.Id_Profissional + "&cbo_profissional=" + ususarioprofissional.CodigoCBO + "');", true);
        //}

        protected void OnClick_GerarAtestadoEvolucao(object sender, EventArgs e)
        {
            UsuarioProfissionalUrgence ususarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Atestado','../FormGerarAtestadoReceita.aspx?model=attested&co_evolucao=" + ((ImageButton)sender).CommandArgument.ToString() + "&co_profissional=" + ususarioprofissional.Id_Profissional + "&cbo_profissional=" + ususarioprofissional.CodigoCBO + "');", true);
        }

        protected void OnClick_GerarReceitaEvolucao(object sender, EventArgs e)
        {
            UsuarioProfissionalUrgence ususarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Receita','../FormGerarAtestadoReceita.aspx?model=prescription&co_evolucao=" + ((ImageButton)sender).CommandArgument.ToString() + "&co_profissional=" + ususarioprofissional.Id_Profissional + "&cbo_profissional=" + ususarioprofissional.CodigoCBO + "');", true);
        }

        protected void OnClick_GerarComparecimentoEvolucao(object sender, EventArgs e)
        {
            UsuarioProfissionalUrgence ususarioprofissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigoViverMais<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Comparecimento','../FormGerarAtestadoReceita.aspx?model=attendance&co_evolucao=" + ((ImageButton)sender).CommandArgument.ToString() + "&co_profissional=" + ususarioprofissional.Id_Profissional + "&cbo_profissional=" + ususarioprofissional.CodigoCBO + "');", true);
        }
    }
}