using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;

namespace ViverMais.View.Vacina
{
    public partial class FormLoteImunobiologico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_LOTE_VACINA", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    long temp;

                    //DropDownList_Vacina.DataSource = Factory.GetInstance<IItemVacina>().ListarVacinas<ViverMais.Model.Vacina>();
                    DropDownList_Vacina.DataSource = Factory.GetInstance<IVacina>().ListarTodos<ViverMais.Model.Vacina>();
                    DropDownList_Vacina.DataBind();
                    DropDownList_Vacina.Items.Insert(0, new ListItem("Selecione...", "-1"));
                    this.CarregaDropDownDefault(DropDownList_Fabricante);
                    this.CarregaDropDownDefault(DropDownList_Aplicacao);

                    if (Request["co_lote"] != null && long.TryParse(Request["co_lote"].ToString(), out temp))
                    {
                        ViewState["co_lote"] = temp;
                        LoteVacina lote = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<LoteVacina>(temp);
                        TextBox_Validade.Text = lote.DataValidade.ToString("dd/MM/yyyy");
                        TextBox_Lote.Text = lote.Identificacao;
                        DropDownList_Vacina.SelectedValue = lote.ItemVacina.Vacina.Codigo.ToString();
                        this.OnSelectedIndexChanged_CarregaFabricantes(new object(), new EventArgs());
                        DropDownList_Fabricante.SelectedValue = lote.ItemVacina.FabricanteVacina.Codigo.ToString();
                        this.OnSelectedIndexChanged_CarregaAplicacao(new object(), new EventArgs());
                        DropDownList_Aplicacao.SelectedValue = lote.ItemVacina.Codigo.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Salva o lote da vacina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_SalvarLote(object sender, EventArgs e)
        {
            try
            {
                LoteVacina lote = ViewState["co_lote"] != null ? Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<LoteVacina>(long.Parse(ViewState["co_lote"].ToString())) : new LoteVacina();
                lote.DataValidade = DateTime.Parse(TextBox_Validade.Text);
                lote.ItemVacina = Factory.GetInstance<IItemVacina>().BuscarPorCodigo<ItemVacina>(int.Parse(DropDownList_Aplicacao.SelectedValue.ToString()));
                lote.Identificacao = TextBox_Lote.Text.Trim();

                if (Factory.GetInstance<ILoteVacina>().ValidaCadastroLote<LoteVacina>(lote.Identificacao.Trim(), lote.DataValidade, lote.ItemVacina.Codigo, lote.Codigo))
                {
                    Factory.GetInstance<IVacinaServiceFacade>().Salvar(lote);
                    Factory.GetInstance<IVacinaServiceFacade>().Inserir(new LogVacina(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 4, "id lote: " + lote.Codigo));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Lote salvo com sucesso.');location='FormExibirPesquisarLote.aspx';", true);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um lote para este imunobiológico cadastrado com as mesmas informações: Fabricante, Número de Aplicações, Lote e Data de Validade!');", true);
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Carrega os fabricantes vinculados ao imunobiológico escolhido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaFabricantes(object sender, EventArgs e)
        {
            this.CarregaDropDownDefault(DropDownList_Fabricante);
            this.CarregaDropDownDefault(DropDownList_Aplicacao);

            if (DropDownList_Vacina.SelectedValue != "-1")
            {
                DropDownList_Fabricante.DataSource = Factory.GetInstance<IItemVacina>().ListarFabricantes<FabricanteVacina>(int.Parse(DropDownList_Vacina.SelectedValue));
                DropDownList_Fabricante.DataBind();
                DropDownList_Fabricante.Items.Insert(0, new ListItem("Selecione...", "-1"));
            }

            DropDownList_Fabricante.Focus();
        }

        protected void OnSelectedIndexChanged_CarregaAplicacao(object sender, EventArgs e)
        {
            this.CarregaDropDownDefault(DropDownList_Aplicacao);

            if (DropDownList_Fabricante.SelectedValue != "-1")
            {
                DropDownList_Aplicacao.DataSource = Factory.GetInstance<IItemVacina>().ListarPorVacina<ItemVacina>(int.Parse(DropDownList_Vacina.SelectedValue), int.Parse(DropDownList_Fabricante.SelectedValue));
                DropDownList_Aplicacao.DataBind();
                DropDownList_Aplicacao.Items.Insert(0, new ListItem("Selecione...", "-1"));
            }

            DropDownList_Aplicacao.Focus();
        }

        private void CarregaDropDownDefault(DropDownList dropdownlist)
        {
            dropdownlist.Items.Clear();
            dropdownlist.Items.Add(new ListItem("Selecione...", "-1"));
        }
    }
}
