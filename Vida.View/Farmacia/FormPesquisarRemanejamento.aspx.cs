using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;

namespace ViverMais.View.Farmacia
{
    public partial class FormPesquisarRemanejamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];

                bool permissao = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "PESQUISAR_REMANEJAMENTO", Modulo.FARMACIA);

                if (!permissao)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    DropDownList_Farmacia.DataSource = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia, Usuario>(usuario, true, true);
                    DropDownList_Farmacia.DataBind();
                }
            }
        }

        /// <summary>
        /// Paginação do gridview de remanejamentos concluídos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaPesquisa(int.Parse(ViewState["co_farmacia"].ToString()), ViewState["data"].ToString(), int.Parse(ViewState["co_farmacia_origem"].ToString()));
            GridView_ItensRemanejamento.PageIndex = e.NewPageIndex;
            GridView_ItensRemanejamento.DataBind();
        }

        /// <summary>
        /// Valida se os campos necessários para a pesquisa foram preenchidos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_ValidaPesquisa(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;

            /*if (string.IsNullOrEmpty(TextBox_Data.Text) && int.Parse(DropDownList_FarmaciaOrigem.SelectedValue) == -1)
            {
                e.IsValid = false;
                CustomValidator_Pesquisa.ErrorMessage = "Informe a Data do Remanejamento e/ou a Farmácia de Origem.";
            }*/
        }

        /// <summary>
        /// Pesquisa os remanejamentos concluídos para a farmácia selecionada e de acordo com os campos de pesquisa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Pesquisar(object sender, EventArgs e)
        {
            if (CustomValidator_Pesquisa.IsValid)
            {
                ViewState["co_farmacia"] = DropDownList_Farmacia.SelectedValue;
                ViewState["co_farmacia_origem"] = DropDownList_FarmaciaOrigem.SelectedValue;
                ViewState["data"] = TextBox_Data.Text;

                CarregaPesquisa(int.Parse(DropDownList_Farmacia.SelectedValue), TextBox_Data.Text, int.Parse(DropDownList_FarmaciaOrigem.SelectedValue));
                Panel_Pesquisa.Visible = true;
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_Pesquisa.ErrorMessage + "');", true);
        }

        private void CarregaPesquisa(int co_farmacia, string data, int co_farmaciaorigem)
        {
            IList<RemanejamentoMedicamento> lr = Factory.GetInstance<IMovimentacao>().BuscarRemanejamentosPorFarmacia<RemanejamentoMedicamento>(co_farmacia).Where(p => p.Status == RemanejamentoMedicamento.FECHADO).ToList();

            if (!string.IsNullOrEmpty(data))
                lr = lr.Where(p => p.DataAbertura.ToString("dd/MM/yyyy") == data).ToList();

            if (co_farmaciaorigem != -1)
                lr = lr.Where(p => p.Movimento.Farmacia.Codigo == co_farmaciaorigem).ToList();

            GridView_ItensRemanejamento.DataSource = lr;
            GridView_ItensRemanejamento.DataBind();
        }

        /// <summary>
        /// Carrega as possíveis farmácias de origem do remanejamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaFarmaciasOrigem(object sender, EventArgs e)
        {
            DropDownList_FarmaciaOrigem.Items.Clear();
            DropDownList_FarmaciaOrigem.Items.Add(new ListItem("Selecione...", "-1"));

            if (int.Parse(DropDownList_Farmacia.SelectedValue) != -1)
            {
                IList<ViverMais.Model.Farmacia> lf = Factory.GetInstance<IFarmacia>().ListarTodos<ViverMais.Model.Farmacia>().Where(p => p.Codigo != int.Parse(DropDownList_Farmacia.SelectedValue)).OrderBy(p => p.Nome).ToList();
                DropDownList_FarmaciaOrigem.DataSource = lf;
                DropDownList_FarmaciaOrigem.DataBind();

                DropDownList_FarmaciaOrigem.Items.Insert(0, new ListItem("Selecione...", "-1"));
            }
        }

        protected void OnRowCommand_VerificarAcao(object sender, GridViewCommandEventArgs e) 
        {
            if (e.CommandName == "CommandName_Imprimir")
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('FormImprimirMovimentoRemanejamento.aspx?tipo=2&codigo=" + int.Parse(GridView_ItensRemanejamento.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString()) + "');", true);
        }
    }
}
