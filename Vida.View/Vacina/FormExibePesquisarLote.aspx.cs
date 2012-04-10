using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Collections;

namespace ViverMais.View.Vacina
{
    public partial class FormExibePesquisarLote : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList_Vacina.DataSource = Factory.GetInstance<IVacina>().ListarTodos<ViverMais.Model.Vacina>().OrderBy(p => p.Nome).ToList();
                DropDownList_Vacina.DataBind();
                DropDownList_Vacina.Items.Insert(0, new ListItem("Selecione...", "-1"));

                DropDownList_Fabricante.DataSource = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<FabricanteVacina>().OrderBy(p => p.Nome).ToList();
                DropDownList_Fabricante.DataBind(); 
                DropDownList_Fabricante.Items.Insert(0, new ListItem("Selecione...", "-1"));

                this.OnClick_ListarTodosLotes(new object(), new EventArgs());
            }
        }

        /// <summary>
        /// Lista todos os lotes cadastrados
        /// </summary>
        private void OnClick_ListarTodosLotes(object sender, EventArgs e)
        {
            IList<LoteVacina> lotes = Factory.GetInstance<ILoteVacina>().ListarTodos<LoteVacina>().OrderBy(p => p.ItemVacina.Vacina.Nome).ToList();
            Session["loteVacinaPesquisado"] = lotes;

            GridView_Lote.DataSource = lotes;
            GridView_Lote.DataBind();
        }

        /// <summary>
        /// Evento que dispara a chamada para a função de pesquisar os lotes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_PesquisarLote(object sender, EventArgs e)
        {
            if (DropDownList_Fabricante.SelectedValue != "-1" || DropDownList_Vacina.SelectedValue != "-1"
                || !string.IsNullOrEmpty(TextBox_Lote.Text) || !string.IsNullOrEmpty(TextBox_Validade.Text) || !string.IsNullOrEmpty(TextBox_Aplicacoes.Text))
                this.CarregarPesquisaLote(int.Parse(DropDownList_Vacina.SelectedValue), int.Parse(DropDownList_Fabricante.SelectedValue), TextBox_Lote.Text, !string.IsNullOrEmpty(TextBox_Validade.Text) ? DateTime.Parse(TextBox_Validade.Text) : DateTime.MinValue, string.IsNullOrEmpty(TextBox_Aplicacoes.Text) ? -1 : int.Parse(TextBox_Aplicacoes.Text));
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe pelo menos um dos seguintes campos para pesquisa: \\n\\n(1) Imunobiológico, \\n(2) Fabricante, \\n(3) Nº Aplicações, \\n(4) Lote, \\n(5) Data de Validade.');", true);
        }

        /// <summary>
        /// Carrega os lotes de vacinas com os paramêtros fornecidos
        /// </summary>
        /// <param name="co_vacina">código da vacina</param>
        /// <param name="co_fabricante">código do fabricante</param>
        /// <param name="lote">identificação lote</param>
        /// <param name="datavalidade">data de validade</param>
        private void CarregarPesquisaLote(int co_vacina, int co_fabricante, string lote, DateTime datavalidade, int numeroaplicacoes)
        {
            IList<LoteVacina> lotespesquisados = Factory.GetInstance<ILoteVacina>().BuscarLote<LoteVacina>(lote, datavalidade, co_vacina, co_fabricante, numeroaplicacoes);
            Session["loteVacinaPesquisado"] = lotespesquisados;

            GridView_Lote.DataSource = lotespesquisados;
            GridView_Lote.DataBind();
        }

        /// <summary>
        /// Paginação do gridview de lotes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            GridView_Lote.DataSource = (IList<LoteVacina>)Session["loteVacinaPesquisado"];
            GridView_Lote.DataBind();

            GridView_Lote.PageIndex = e.NewPageIndex;
            GridView_Lote.DataBind();
        }
    }
}
