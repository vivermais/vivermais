using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Vacina
{
    public partial class FormDispensacoesPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PESQUISAR_DISPENSACAO", Modulo.VACINA))
                {
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                    //return;
                }

                ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];
                WUC_ExibirPaciente.Paciente = paciente;
                //WUC_ExibirPaciente.Visible = true;

                Session.Remove("dispensacoesVacinaPaciente");
                Session.Remove("dispensacoesVacinaPesquisadas");
                Session.Remove("itensdispensacaovacinapesquisada");
                this.CarregaDispensacoes(paciente);
                this.OnClick_ListarTodos(new object(), new EventArgs());
            }
        }

        private void CarregaDispensacoes(ViverMais.Model.Paciente paciente)
        {
            IList<SalaVacina> salasvinculadas = Factory.GetInstance<ISalaVacina>().BuscarPorUsuario<SalaVacina, Usuario>((Usuario)Session["Usuario"], false, false);
            IList<DispensacaoVacina> dispensacoespaciente = Factory.GetInstance<IDispensacao>().BuscarPorPaciente<DispensacaoVacina, SalaVacina>(paciente.Codigo, salasvinculadas);
            Session["dispensacoesVacinaPaciente"] = dispensacoespaciente;
        }

        private void CarregaDispensacoes(IList<DispensacaoVacina> dispensacoes)
        {
            Session["dispensacoesVacinaPesquisadas"] = dispensacoes;
            GridView_Dispensacoes.DataSource = dispensacoes;
            GridView_Dispensacoes.DataBind();
        }

        protected void OnRowCommand_Dispensacao(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_VerInformacoes")
            {
                int co_dispensacao = int.Parse(GridView_Dispensacoes.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString());
                ViewState["co_dispensacao"] = co_dispensacao;

                Panel_Dispensacao.Visible = true;
                this.CarregaInformacoesDispensacao(co_dispensacao);
            }
        }

        private void CarregaInformacoesDispensacao(int co_dispensacao)
        {
            IList<DispensacaoVacina> dispensacoes = (IList<DispensacaoVacina>)Session["dispensacoesVacinaPaciente"];
            DispensacaoVacina dispensacao = dispensacoes.Where(p=>p.Codigo == co_dispensacao).First();
            Label_SalaInfo.Text = dispensacao.Sala.Nome;
            Label_DataInfo.Text = dispensacao.Data.ToString("dd/MM/yyyy");
            Label_GrupoInfo.Text = dispensacao.GrupoAtendimento.Descricao;
            IList<ItemDispensacaoVacina> itens = Factory.GetInstance<IDispensacao>().BuscarItensDispensacao<ItemDispensacaoVacina>(co_dispensacao);
            Session["itensdispensacaovacinapesquisada"] = itens;
            ViewState["inventarioaberto"] = Factory.GetInstance<IInventarioVacina>().BuscarPorSituacao<InventarioVacina>(Convert.ToChar(InventarioVacina.DescricaoSituacao.Aberto), dispensacao.Sala.Codigo).Count() > 0 ? true : false;

            GridView_ItensDispensacao.DataSource = itens;
            GridView_ItensDispensacao.DataBind();
        }

        protected void OnPageIndexChanging_Dispensacoes(object sender, GridViewPageEventArgs e)
        {
            this.CarregaDispensacoes((IList<DispensacaoVacina>)Session["dispensacoesVacinaPesquisadas"]);
            GridView_Dispensacoes.PageIndex = e.NewPageIndex;
            GridView_Dispensacoes.DataBind();
        }

        protected void OnClick_FiltrarDispensacoes(object sender, EventArgs e)
        {
            IList<DispensacaoVacina> dispensacoes = DispensacaoVacina.Filtrar((IList<DispensacaoVacina>)Session["dispensacoesVacinaPaciente"], DateTime.Parse(TextBox_DataAtendimento.Text));
            this.CarregaDispensacoes(dispensacoes);
        }

        protected void OnClick_ListarTodos(object sender, EventArgs e)
        {
            IList<DispensacaoVacina> dispensacoes = (IList<DispensacaoVacina>)Session["dispensacoesVacinaPaciente"];
            this.CarregaDispensacoes(dispensacoes);
        }

        protected void OnClick_Cancelar(object sender, EventArgs e)
        {
            ViewState.Remove("co_dispensacao");
            Panel_Dispensacao.Visible = false;
        }

        protected void OnClick_ImprimirRecibo(object sender, EventArgs e)
        {
            Response.Redirect("FormImprimirReciboDispensacao.aspx?co_dispensacao=" + ViewState["co_dispensacao"].ToString());
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "alert", "window.open('FormImprimirReciboDispensacao.aspx?co_dispensacao=" + ViewState["co_dispensacao"].ToString() + "');", true);
        }

        protected void OnRowDataBound_ItensDispensacao(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                IList<ItemDispensacaoVacina> itens = (IList<ItemDispensacaoVacina>)Session["itensdispensacaovacinapesquisada"];
                CheckBox checkexcluir = (CheckBox)e.Row.FindControl("CheckBox_Excluir");

                long co_item = long.Parse(GridView_ItensDispensacao.DataKeys[e.Row.RowIndex]["Codigo"].ToString());
                ItemDispensacaoVacina item = (from _item in itens where _item.Codigo == co_item select _item).First();

                if (item.AberturaAmpola == ItemDispensacaoVacina.AMPOLA_ABERTA &&
                    bool.Parse(ViewState["inventarioaberto"].ToString()) == true)
                    checkexcluir.Enabled = false;
            }
        }

        protected void OnClick_ExcluirItens(object sender, EventArgs e)
        {
            IList<long> itensexclusao = new List<long>();
            IList<ItemDispensacaoVacina> itens = (IList<ItemDispensacaoVacina>)Session["itensdispensacaovacinapesquisada"];
            foreach (GridViewRow row in GridView_ItensDispensacao.Rows)
            {
                CheckBox checkexcluir = (CheckBox)row.FindControl("CheckBox_Excluir");
                if (checkexcluir.Checked)
                    itensexclusao.Add(long.Parse(GridView_ItensDispensacao.DataKeys[row.RowIndex]["Codigo"].ToString()));
            }

            if (itensexclusao.Count() > 0)
            {
                int co_dispensacao = Factory.GetInstance<IDispensacao>().ExcluirItensDispensacao<ItemDispensacaoVacina>((from item in itens where itensexclusao.Contains(item.Codigo) select item).ToList(), ((Usuario)Session["Usuario"]).Codigo);
                this.OnClick_Cancelar(new object(), new EventArgs());
                this.CarregaDispensacoes((ViverMais.Model.Paciente)Session["pacienteSelecionado"]);
                this.OnClick_ListarTodos(new object(), new EventArgs());
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Itens excluídos com sucesso.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione pelo menos um item para exclusão.');", true);
        }
    }
}
