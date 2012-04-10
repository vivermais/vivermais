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
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Vacina
{
    public partial class FormVacinacaoCampanha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_VACINACAO_CAMPANHA", Modulo.VACINA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                    OnClick_ListarTodasCampanhas(new object(), new EventArgs());
            }
        }

        /// <summary>
        /// Pesquisa as campanhas compreendidas no período informado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_PesquisarCampanha(object sender, EventArgs e)
        {
            ViewState["pesquisa"] = "alguns";
            ViewState["DataInicioPesquisa"] = TextBox_DataInicioCampanha.Text;
            ViewState["DataFimPesquisa"] = TextBox_DataTerminoCampanha.Text;
            PesquisarCampanhaPeriodo(DateTime.Parse(TextBox_DataInicioCampanha.Text), DateTime.Parse(TextBox_DataTerminoCampanha.Text));
            Panel_InformacoesCampanha.Visible = false;
            Panel_VacinasCampanha.Visible = false;
        }

        /// <summary>
        /// Pesquisa as campanhas no período
        /// </summary>
        /// <param name="datainicio">Data Início</param>
        /// <param name="datafim">Data Final</param>
        private void PesquisarCampanhaPeriodo(DateTime datainicio, DateTime datafim)
        {
            GridView_Campanhas.DataSource = Factory.GetInstance<ICampanhaVacinacao>().BuscarCampanhasPorPeriodo<Campanha>(datainicio, datafim).OrderBy(p=>p.Nome).OrderBy(p=>p.DataInicio).ToList();
            GridView_Campanhas.DataBind();
        }

        /// <summary>
        /// Chamada para listar todas as campanhas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_ListarTodasCampanhas(object sender, EventArgs e)
        {
            ViewState["pesquisa"] = "todos";
            ListarTodasCampanhas();
            Panel_InformacoesCampanha.Visible = false;
            Panel_VacinasCampanha.Visible = false;
        }

        /// <summary>
        /// Lista todas as campanhas cadastradas
        /// </summary>
        private void ListarTodasCampanhas()
        {
            GridView_Campanhas.DataSource = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<Campanha>().OrderBy(p => p.Nome).OrderBy(p=>p.DataInicio).ToList();
            GridView_Campanhas.DataBind();
        }

        /// <summary>
        /// Paginação do gridview das campanhas pesquisadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoCampanhas(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["pesquisa"] != null)
            {
                if (ViewState["pesquisa"].ToString().Equals("alguns"))
                    PesquisarCampanhaPeriodo(DateTime.Parse(ViewState["DataInicioPesquisa"].ToString()), DateTime.Parse(ViewState["DataFimPesquisa"].ToString()));
                else
                    ListarTodasCampanhas();

                GridView_Campanhas.PageIndex = e.NewPageIndex;
                GridView_Campanhas.DataBind();
            }
        }

        /// <summary>
        /// Verifica qual ação o deseja disparar no gridview de campanhas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_VerificarAcao(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_Selecionar")
            {
                foreach (GridViewRow row in GridView_Campanhas.Rows)
                    row.BackColor = System.Drawing.Color.White;

                GridView_Campanhas.Rows[int.Parse(e.CommandArgument.ToString())].BackColor = System.Drawing.Color.Yellow;
                CarregaInformacoesCampanha(int.Parse(GridView_Campanhas.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString()));
            }
        }

        /// <summary>
        /// Carrega as informações da campanha
        /// </summary>
        /// <param name="co_campanha">código da campanha</param>
        private void CarregaInformacoesCampanha(int co_campanha)
        {
            Campanha campanha = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<Campanha>(co_campanha);
            Label_NomeCampanha.Text = campanha.Nome;
            Label_DataInicioCampanha.Text = campanha.DataInicio.ToString("dd/MM/yyyy");
            Label_DataTerminoCampanha.Text = campanha.DataFim.ToString("dd/MM/yyyy");

            string faixa = campanha.FaixaEtariaInicial.ToString();
            faixa += Convert.ToChar(campanha.UnidadeFaixaInicial) == Convert.ToChar(Campanha.DescricaoUnidadeFaixa.Anos) ? " Anos " : " Meses";
            faixa += " a " + campanha.FaixaEtariaFinal.ToString();
            faixa += Convert.ToChar(campanha.UnidadeFaixaFinal) == Convert.ToChar(Campanha.DescricaoUnidadeFaixa.Anos) ? " Anos " : " Meses";
            Label_FaixaEtariaCampanha.Text = faixa;

            switch (campanha.Sexo)
            {
                case 1: Label_SexoCampanha.Text = "Masculino";
                    break;
                case 2: Label_SexoCampanha.Text = "Feminino";
                    break;
                default: Label_SexoCampanha.Text = "Ambos";
                    break;
            }

            Label_MetaCampanha.Text = campanha.Meta.ToString();

            if (Convert.ToChar(Campanha.DescricaoStatus.Ativa) == Convert.ToChar(campanha.Status))
            {
                Label_StatusCampanha.Text = "Em andamento";
                AccordionPane1.Enabled = true;
            }
            else
            {
                Label_StatusCampanha.Text = "Finalizada";
                AccordionPane1.Enabled = false;
            }

            Panel_InformacoesCampanha.Visible = true;
            Panel_VacinasCampanha.Visible = true;
            ViewState["co_campanha"] = co_campanha;

            DropDownList_VacinaCampanha.DataSource = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ViverMais.Model.Vacina>().OrderBy(p => p.Nome).ToList();
            DropDownList_VacinaCampanha.DataBind();

            DropDownList_VacinaCampanha.Items.Insert(0, new ListItem("Selecione...", "-1"));
            CarregaItensCampanha(co_campanha);
        }

        /// <summary>
        /// Paginaão do gridview de vacinas da campanha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoVacinas(object sender, GridViewPageEventArgs e)
        {
            CarregaItensCampanha(int.Parse(ViewState["co_campanha"].ToString()));
            GridView_VacinasCampanha.PageIndex = e.NewPageIndex;
            GridView_VacinasCampanha.DataBind();
        }

        /// <summary>
        /// Carrega todos os itens da campanha
        /// </summary>
        /// <param name="co_campanha">código da campanha</param>
        private void CarregaItensCampanha(int co_campanha)
        {
            GridView_VacinasCampanha.DataSource = Factory.GetInstance<ICampanhaVacinacao>().BuscarItemCampanhaPorCampanha<ItemCampanha>(co_campanha);
            GridView_VacinasCampanha.DataBind();
        }

        /// <summary>
        /// Adiciona a vacina na campanha atual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_AdicionarVacina(object sender, EventArgs e)
        {
            try
            {
                ItemCampanha item = new ItemCampanha();
                item.Campanha = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<Campanha>(int.Parse(ViewState["co_campanha"].ToString()));
                item.ItemVacina = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ItemVacina>(int.Parse(DropDownList_FabricanteVacinaCampanha.Text));
                item.Quantidade = int.Parse(TextBox_QuantidadeAplicacoes.Text);

                if (Factory.GetInstance<ICampanhaVacinacao>().BuscarItemCampanha<ItemCampanha>(item.ItemVacina.Codigo, item.Campanha.Codigo) == null)
                {
                    Factory.GetInstance<IVacinaServiceFacade>().Inserir(item);

                    OnClick_CancelarAdicaoVacina(new object(), new EventArgs());
                    CarregaItensCampanha(int.Parse(ViewState["co_campanha"].ToString()));

                    Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 7, "id item: " + item.Codigo));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Item da campanha adicionado com sucesso.');", true);
                }else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Um item da campanha com mesmo fabricante e vacina já está cadastrado! Por favor informe outro item ou altere o valor de aplicações para este registro.');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cancela a adição da vacina na campanha atual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarAdicaoVacina(object sender, EventArgs e)
        {
            DropDownList_VacinaCampanha.SelectedValue = "-1";
            OnSelectedIndexChanged_CarregaFabricantesVacina(new object(), new EventArgs());
            TextBox_QuantidadeAplicacoes.Text = "";
        }

        /// <summary>
        /// Carrega os fabricantes de acordo com a vacina selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaFabricantesVacina(object sender, EventArgs e)
        {
            DropDownList_FabricanteVacinaCampanha.Items.Clear();
            DropDownList_FabricanteVacinaCampanha.Items.Add(new ListItem("Selecione...", "-1"));

            if (DropDownList_VacinaCampanha.SelectedValue != "-1")
            {
                IList<ItemVacina> itensvacina = Factory.GetInstance<IItemVacina>().ListarPorVacina<ItemVacina>(int.Parse(DropDownList_VacinaCampanha.SelectedValue)).OrderBy(p => p.FabricanteVacina.Nome).ToList();
                foreach (ItemVacina item in itensvacina)
                    DropDownList_FabricanteVacinaCampanha.Items.Add(new ListItem(item.FabricanteVacina.Nome, item.Codigo.ToString()));
            }
        }

        /// <summary>
        /// Verifica qual ação está sendo executada pelo usuário no item vacinação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_VerificarAcaoItemCampanha(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_Excluir")
            {
                try
                {
                    int co_item = int.Parse(GridView_VacinasCampanha.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString());
                    Factory.GetInstance<IVacinaServiceFacade>().Deletar(Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ItemCampanha>(co_item));
                    CarregaItensCampanha(int.Parse(ViewState["co_campanha"].ToString()));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Item da campanha excluído com sucesso.');", true);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Formata o gridview de vacinas de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewVacinas(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<Campanha>(int.Parse(ViewState["co_campanha"].ToString())).Status == Convert.ToChar(Campanha.DescricaoStatus.Finalizada))
                {
                    GridView_VacinasCampanha.Columns[GridView_VacinasCampanha.Columns.Count - 1].Visible = false;
                    GridView_VacinasCampanha.Columns[GridView_VacinasCampanha.Columns.Count - 2].Visible = false;
                }
                else
                {
                    ImageButton lbexcluir = (ImageButton)e.Row.Cells[4].Controls[0];
                    lbexcluir.OnClientClick = "javascript:return confirm('Tem certeza que deseja excluir este item da campanha ?');";

                    GridView_VacinasCampanha.Columns[GridView_VacinasCampanha.Columns.Count - 1].Visible = true;
                    GridView_VacinasCampanha.Columns[GridView_VacinasCampanha.Columns.Count - 2].Visible = true;
                }
            }
        }

        /// <summary>
        /// Habilita a edição do item da campanha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_EditarItemCampanha(object sender, GridViewEditEventArgs e)
        {
            GridView_VacinasCampanha.EditIndex = e.NewEditIndex;
            CarregaItensCampanha(int.Parse(ViewState["co_campanha"].ToString()));
        }

        /// <summary>
        /// Cancela a edição do item da campanha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarEdicaoItemCampanha(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_VacinasCampanha.EditIndex = -1;
            CarregaItensCampanha(int.Parse(ViewState["co_campanha"].ToString()));
        }

        /// <summary>
        /// Atualiza o item da campanha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_AlterarItemCampanha(object sender, GridViewUpdateEventArgs e)
        {
            int co_item = int.Parse(GridView_VacinasCampanha.DataKeys[e.RowIndex]["Codigo"].ToString());
            int qtd_solicitada = int.Parse(((TextBox)GridView_VacinasCampanha.Rows[e.RowIndex].FindControl("TextBox_Quantidade")).Text);

            try
            {
                ItemCampanha item = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ItemCampanha>(co_item);
                item.Quantidade = qtd_solicitada;
                Factory.GetInstance<IViverMaisServiceFacade>().Atualizar(item);
                GridView_VacinasCampanha.EditIndex = -1;
                CarregaItensCampanha(int.Parse(ViewState["co_campanha"].ToString()));

                Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 8, "id item: " + item.Codigo));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Item da campanha alterado com suceso.');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
