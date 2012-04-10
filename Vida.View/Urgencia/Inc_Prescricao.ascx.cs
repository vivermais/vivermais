using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using System.Drawing;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.View.Urgencia
{
    public partial class Inc_Prescricao : System.Web.UI.UserControl
    {
        public event EventHandler OnAlterarMedicamentoPrescricao;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetExpires(DateTime.Now.AddSeconds(1));
            //Response.Cache.SetCacheability(HttpCacheability.Private);

            //if (!IsPostBack)
            //{
            //    long co_prontuario = long.Parse(Request["codigo"].ToString());
            //    ViewState["co_prontuario"] = co_prontuario;
            //    CarregaPrescricoes(co_prontuario);
            //    CarregaCombos();
            //}

            //Response.AddCacheItemDependency("Pages");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache); 
        }

        public void CarregaPagina()
        {
            long co_prontuario = long.Parse(Request["codigo"].ToString());
            ViewState["co_prontuario"] = co_prontuario;
            CarregaPrescricoes(co_prontuario);
            CarregaCombos();
        }

        protected void CarregaCombos()
        {
            //Carrega os medicamentos do elenco do PA
            ViverMais.Model.ElencoMedicamento elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(15);
            ddlMedicamentos.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.Medicamento m in elenco.Medicamentos)
                ddlMedicamentos.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));

            CarregaViasAdministracao();

            //remover  ---------------------*****************
            gridProcedimentoNaoFaturavel.DataSource = new List<ViverMais.Model.PrescricaoProcedimentoNaoFaturavel>();
            gridProcedimentoNaoFaturavel.DataBind();
            GridView_Procedimento.DataSource = new List<ViverMais.Model.PrescricaoProcedimento>();
            GridView_Procedimento.DataBind();
        }

        /// <summary>
        /// Carrega as prescrições do prontuário
        /// </summary>
        /// <param name="co_prontuario"></param>
        private void CarregaPrescricoes(long co_prontuario)
        {
            IList<Prescricao> lp = Factory.GetInstance<IPrescricao>().BuscarPorProntuario<Prescricao>(co_prontuario);
            //Prescricao p = Factory.GetInstance<IPrescricao>().BuscarPorProntuario<Prescricao>(co_prontuario);

            //if (p != null)
            //    lp.Add(p);

            //IList<EvolucaoMedica> lem = Factory.GetInstance<IEvolucaoMedica>().buscaPorProntuario<EvolucaoMedica>(co_prontuario);
            //foreach (EvolucaoMedica em in lem)
            //{
            //    p = Factory.GetInstance<IPrescricao>().BuscarPorEvolucaoMedica<Prescricao>(em.Codigo);
            //    if (p != null)
            //        lp.Add(p);
            //}

            GridView_PrescricoesRegistradas.DataSource = lp;
            GridView_PrescricoesRegistradas.DataBind();
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoPrescricao(object sender, GridViewPageEventArgs e)
        {
            CarregaPrescricoes(long.Parse(ViewState["co_prontuario"].ToString()));
            GridView_PrescricoesRegistradas.PageIndex = e.NewPageIndex;
            GridView_PrescricoesRegistradas.DataBind();
        }

        /// <summary>
        /// Formata o gridview de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void OnRowDataBound_FormataGridView(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Prescricao p = Factory.GetInstance<IPrescricao>().BuscarPorCodigo<Prescricao>(long.Parse(GridView_PrescricoesRegistradas.DataKeys[e.Row.RowIndex]["Codigo"].ToString()));
        //    }
        //}

        /// <summary>
        /// Verifica se a prescrição foi selecionada para aprazar os medicamentos/kit/procedimentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_SelecionarPrescricao(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_Selecionar")
            {
                ViewState["co_prescricao"] = GridView_PrescricoesRegistradas.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString();
                CarregaItens(long.Parse(ViewState["co_prescricao"].ToString()));
                if (GridView_PrescricoesRegistradas.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text == "Suspensa")
                    Panel_IncluiMedicamento.Visible = false;
                else
                    Panel_IncluiMedicamento.Visible = true;

            }
        }

        /// <summary>
        /// Carrega os itens da prescrição selecionada
        /// </summary>
        /// <param name="co_prescricao"></param>
        private void CarregaItens(long co_prescricao)
        {
            Panel_Um.Visible = true;
            Prescricao prescricao = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<Prescricao>(co_prescricao);
            //se a prescrição está suspensa, impede o usuário de alterá-la
            if (prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Suspensa))
                ViewState["prescricaobloqueada"] = "sim";
            else
                ViewState["prescricaobloqueada"] = "nao";

            IList<PrescricaoProcedimentoNaoFaturavel> lpnf = Factory.GetInstance<IPrescricao>().BuscarProcedimentosNaoFaturaveis<PrescricaoProcedimentoNaoFaturavel>(co_prescricao);
            gridProcedimentoNaoFaturavel.DataSource = lpnf;
            gridProcedimentoNaoFaturavel.DataBind();

            IList<PrescricaoMedicamento> lpm = Factory.GetInstance<IPrescricao>().BuscarMedicamentos<PrescricaoMedicamento>(co_prescricao);
            foreach (PrescricaoMedicamento pm in lpm)
                pm.ObjetoMedicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(pm.Medicamento);
            gridMedicamentos.DataSource = lpm;
            gridMedicamentos.DataBind();
            //Session["listamedicamentos"] = lpm;

            IList<PrescricaoProcedimento> lpp = Factory.GetInstance<IPrescricao>().BuscarProcedimentos<PrescricaoProcedimento>(co_prescricao);
            foreach (PrescricaoProcedimento pp in lpp)
                pp.Procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(pp.CodigoProcedimento);
            GridView_Procedimento.DataSource = lpp;
            GridView_Procedimento.DataBind();
        }

        /// <summary>
        /// Carrega os tipos de via de administração para medicamento
        /// </summary>
        private void CarregaViasAdministracao()
        {
            IList<ViaAdministracao> lva = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViaAdministracao>().OrderBy(p => p.Nome).ToList();

            foreach (ViaAdministracao va in lva)
            {
                DropDownList_ViaAdministracaoMedicamento.Items.Add(new ListItem(va.Nome, va.Codigo.ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_SuspenderMedicamento(object sender, GridViewCommandEventArgs e)
        {

            int co_medicamento = int.Parse(gridMedicamentos.DataKeys[int.Parse(e.CommandArgument.ToString())]["Medicamento"].ToString());
            //IList<PrescricaoMedicamento> lista = (IList<PrescricaoMedicamento>)Session["listamedicamentos"];
            IList<PrescricaoMedicamento> lista = Factory.GetInstance<IPrescricao>().BuscarMedicamentos<PrescricaoMedicamento>(long.Parse(ViewState["co_prescricao"].ToString()));

            foreach (PrescricaoMedicamento pm in lista)
            {
                if (pm.Medicamento == co_medicamento)
                {
                    pm.Suspenso = !pm.Suspenso;
                    if (pm.Suspenso)
                    {
                        //gridMedicamentos.Rows[int.Parse(e.CommandArgument.ToString())].BackColor = Color.LightCoral;
                        //((LinkButton)gridMedicamentos.Rows[int.Parse(e.CommandArgument.ToString())].Cells[6].Controls[0]).Text = "Ativar";
                        Factory.GetInstance<IUrgenciaServiceFacade>().Atualizar(pm);
                        //exclui os aprazamentos não executados deste item
                        Factory.GetInstance<IPrescricao>().ExcluirAprazamentosNaoExecutadosMedicamento<AprazamentoMedicamento>(pm.Prescricao.Codigo, pm.Medicamento);
                    }
                    else
                    {
                        //gridMedicamentos.Rows[int.Parse(e.CommandArgument.ToString())].BackColor = Color.White;
                        //((LinkButton)gridMedicamentos.Rows[int.Parse(e.CommandArgument.ToString())].Cells[6].Controls[0]).Text = "Suspender";
                        Factory.GetInstance<IUrgenciaServiceFacade>().Atualizar(pm);
                    }
                }
            }

            Session["co_prescricao"] = ViewState["co_prescricao"].ToString();
            this.OnAlterarMedicamentoPrescricao.Invoke(this, new EventArgs());
            CarregaItens(long.Parse(Session["co_prescricao"].ToString()));
            //Session["listamedicamentos"] = lista;
            //gridMedicamentos.DataSource = lista;
            //gridMedicamentos.DataBind();

            //ViverMais.Model.PrescricaoMedicamento pm = Factory.GetInstance<IUrgenciaServiceFacade>( gridMedicamentos.SelectedDataKey.ToString());
        }


        protected void btnAdicionarMedicamentoPrescricao_Click(object sender, EventArgs e)
        {
            if (ViewState["co_prescricao"] == null) 
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione uma prescrição para executar esta operação!');", true);
                return;
            }
            IList<PrescricaoMedicamento> lista = Session["listamedicamentos"] != null ? (IList<PrescricaoMedicamento>)Session["listamedicamentos"] : new List<PrescricaoMedicamento>();
            ViverMais.Model.Prescricao prescricao = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Prescricao>(long.Parse(ViewState["co_prescricao"].ToString()));
            if (lista.Where(p => p.Medicamento.ToString() == ddlMedicamentos.SelectedValue).FirstOrDefault() == null)
            {
                PrescricaoMedicamento pm = new PrescricaoMedicamento();
                pm.Prescricao = prescricao;
                pm.ObjetoMedicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(int.Parse(ddlMedicamentos.SelectedValue));
                pm.Medicamento = pm.ObjetoMedicamento.Codigo;
                pm.Intervalo = tbxIntervaloMedicamento.Text;
                pm.Observacao = TextBox_ObservacaoPrescricaoMedicamento.Text;
                if (DropDownList_ViaAdministracaoMedicamento.SelectedValue != "-1")
                    pm.ViaAdministracao = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViaAdministracao>(int.Parse(DropDownList_ViaAdministracaoMedicamento.SelectedValue));

                if (DropDownList_FormaAdministracaoMedicamento.SelectedValue != "-1")
                    pm.FormaAdministracao = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<FormaAdministracao>(int.Parse(DropDownList_FormaAdministracaoMedicamento.SelectedValue));
                lista.Add(pm);
                Factory.GetInstance<IUrgenciaServiceFacade>().Inserir(pm);
            }
            Session["co_prescricao"] = prescricao.Codigo;
            this.OnAlterarMedicamentoPrescricao.Invoke(this, new EventArgs());
            
            Session["listamedicamentos"] = lista;
            gridMedicamentos.DataSource = lista;
            gridMedicamentos.DataBind();

        }

        protected void gridMedicamentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text == "Suspenso")
                {
                    e.Row.BackColor = Color.LightCoral;
                    ((LinkButton)e.Row.Cells[6].Controls[0]).Text = "Ativar";
                }
                else
                {
                    e.Row.BackColor = Color.White;
                    ((LinkButton)e.Row.Cells[6].Controls[0]).Text = "Suspender";
                }
                if (ViewState["prescricaobloqueada"].ToString() == "sim")
                    gridMedicamentos.Columns[gridMedicamentos.Columns.Count - 1].Visible = false;
                else
                {
                    gridMedicamentos.Columns[gridMedicamentos.Columns.Count - 1].Visible = true;
                    ((LinkButton)e.Row.Cells[6].Controls[0]).OnClientClick = "Javascript:return confirm('Se você estiver suspendendo um medicamento, os próximos aprazamentos deste serão excluídos. Deseja Continuar?');";
                }

            }
        }

        /// <summary>
        /// Carrega as formas de administração a partir da via escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaFormaAdministracaoMedicamento(object sender, EventArgs e)
        {
            ViaAdministracao va = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViaAdministracao>(int.Parse(DropDownList_ViaAdministracaoMedicamento.SelectedValue));
            DropDownList_FormaAdministracaoMedicamento.Items.Clear();
            DropDownList_FormaAdministracaoMedicamento.Items.Add(new ListItem("Selecione...", "-1"));

            if (va != null)
            {
                foreach (FormaAdministracao fa in va.FormasAdministracao)
                    DropDownList_FormaAdministracaoMedicamento.Items.Add(new ListItem(fa.Nome, fa.Codigo.ToString()));
            }
        }
    }
}