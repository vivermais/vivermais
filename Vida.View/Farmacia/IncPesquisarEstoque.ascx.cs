using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class IncPesquisarEstoque : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Usuario usuario = (Usuario)Session["Usuario"];
                    bool permissao   = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo,"CONSULTAR_ESTOQUE", Modulo.FARMACIA);

                    if (permissao)
                    {
                        DropDownList_Farmacia.DataSource = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia, Usuario>(usuario, true, true);
                        DropDownList_Farmacia.DataBind();

                        IList<FabricanteMedicamento> lfm = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<FabricanteMedicamento>().OrderBy(p => p.Nome).ToList();
                        foreach (FabricanteMedicamento f in lfm)
                            DropDownList_Fabricante.Items.Add(new ListItem(f.Nome, f.Codigo.ToString()));

                        IList<Medicamento> lm = Factory.GetInstance<IMedicamento>().ListarTodosMedicamentos<Medicamento>().OrderBy(p => p.Nome).ToList();
                        foreach (Medicamento m in lm)
                            DropDownList_Medicamento.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));

                        DropDownList_Medicamento.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                    }

                    //ViverMais.Model.Farmacia farm = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((Usuario)Session["Usuario"]).Codigo);

                    //if (farm != null)
                    //{
                    //    Label_Farmacia.Text = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(farm.Codigo).Nome;

                    //    IList<FabricanteMedicamento> lfm = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<FabricanteMedicamento>().OrderBy(p => p.Nome).ToList();
                    //    foreach (FabricanteMedicamento f in lfm)
                    //        DropDownList_Fabricante.Items.Add(new ListItem(f.Nome, f.Codigo.ToString()));

                    //    IList<Medicamento> lm = Factory.GetInstance<IMedicamento>().ListarTodos<Medicamento>().OrderBy(p => p.Nome).ToList();
                    //    foreach (Medicamento m in lm)
                    //        DropDownList_Medicamento.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));

                    //    DropDownList_Medicamento.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                    //}
                    //else
                    //{
                    //    Panel_UsuarioNaoVinculado.Visible = true;
                    //    Panel_Farmacia.Visible = false;
                    //}
                }
                catch (Exception f)
                {
                    throw f;
                }
            }

            //ConfiguraGridView();
        }

        /// <summary>
        /// Configura o gridview com o Agrinei
        /// </summary>
        private void ConfiguraGridView()
        {
            GridViewHelper helper = new GridViewHelper(this.GridView_Estoque);
            helper.RegisterSummary("QuantidadeEstoque", SummaryOperation.Sum);
        }

        /// <summary>
        /// Pesquisa o estoque do medicamento indicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Pesquisar(object sender, EventArgs e)
        {
            try
            {
                //Label_Medicamento.Text = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(int.Parse(DropDownList_Medicamento.SelectedValue)).Nome;
                //GridView_Estoque.DataSource = Factory.GetInstance<IEstoque>().BuscarPorDescricao<Estoque>(Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((Usuario)Session["Usuario"]).Codigo).Codigo, int.Parse(DropDownList_Fabricante.SelectedValue), int.Parse(DropDownList_Medicamento.SelectedValue));

                ViewState["co_farmacia"] = DropDownList_Farmacia.SelectedValue;
                ViewState["co_medicamento"] = DropDownList_Medicamento.SelectedValue;
                ViewState["co_fabricante"] = DropDownList_Fabricante.SelectedValue;
                ViewState["lote"] = TextBox_Lote.Text;
                this.CarregaEstoque(int.Parse(DropDownList_Farmacia.SelectedValue),int.Parse(DropDownList_Medicamento.SelectedValue), int.Parse(DropDownList_Fabricante.SelectedValue), TextBox_Lote.Text);

                Panel_Resultado.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="co_farmacia">código da farmácia</param>
        /// <param name="co_medicamento">código do medicamento</param>
        /// <param name="co_fabricante">código do fabricante</param>
        /// <param name="lote">lote</param>
        private void CarregaEstoque(int co_farmacia, int co_medicamento, int co_fabricante, string lote)
        {
            IList<Estoque> estoques = Factory.GetInstance<IEstoque>().BuscarPorDescricao<Estoque>(co_farmacia, co_fabricante, co_medicamento, lote);
            GridView_Estoque.DataSource = estoques;
            GridView_Estoque.DataBind();
        }

        /// <summary>
        /// Paginação para o  estoque da farmácia selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            this.CarregaEstoque(int.Parse(ViewState["co_farmacia"].ToString()), int.Parse(ViewState["co_medicamento"].ToString()), int.Parse(ViewState["co_fabricante"].ToString()), ViewState["lote"].ToString());
            GridView_Estoque.PageIndex = e.NewPageIndex;
            GridView_Estoque.DataBind();
        }
    }
}