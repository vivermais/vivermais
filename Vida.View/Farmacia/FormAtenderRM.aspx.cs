﻿using System;
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
using System.Drawing;
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.View.Farmacia
{
    public partial class FormAtenderRM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViverMais.Model.RequisicaoMedicamento rm = null;
                if (Request.QueryString["codigo"] != null)
                {
                    rm = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorCodigo<ViverMais.Model.RequisicaoMedicamento>(int.Parse(Request.QueryString["codigo"].ToString()));
                    lblDataCriacao.Text = rm.DataCriacao.ToString("dd/MM/yyyy");
                    lblDataAutorizacao.Text = rm.Data_Autorizacao.Value.ToString("dd/MM/yyyy");
                    lblNumeroRequisicao.Text = rm.Codigo.ToString();
                    lblFarmacia.Text = rm.Farmacia.Nome;
                    IList<ViverMais.Model.ItemRequisicao> itens = Factory.GetInstance<IRequisicaoMedicamento>().BuscarItensRequisicao<ViverMais.Model.ItemRequisicao>(rm.Codigo);
                    gridItens.DataSource = itens;
                    gridItens.DataBind();
                    Session["itens"] = itens;
                    Session["rm"] = rm;
                }
            }
        }

        void RecarregaGrid()
        {
            IList<ViverMais.Model.ItemRequisicao> itens = (IList<ViverMais.Model.ItemRequisicao>)Session["itens"];
            gridItens.DataSource = itens;
            gridItens.DataBind();
            MarcaLinhaItemFornecido(itens);
        }

        protected void gridItens_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridItens.EditIndex = e.NewEditIndex;
            GridViewRow r = gridItens.Rows[e.NewEditIndex];
            //RecarregaGrid();

            int index = gridItens.EditIndex;
            if (index == 0)
                index = gridItens.PageIndex * gridItens.PageSize;
            else
                index = (gridItens.PageIndex * gridItens.PageSize) + index;

            IList<ViverMais.Model.ItemRequisicao> itens = new List<ViverMais.Model.ItemRequisicao>();
            if (Session["itens"] != null)
                itens = (List<ViverMais.Model.ItemRequisicao>)(Session["itens"]);

            gridItens.DataSource = itens;
            gridItens.DataBind();
            Session["itens"] = itens;

            IList<ViverMais.Model.LoteMedicamento> lotes = Factory.GetInstance<ILoteMedicamento>().BuscarPorEstoqueAlmoxarifado<ViverMais.Model.LoteMedicamento>(itens[index].Medicamento.Codigo,ViverMais.Model.Farmacia.ALMOXARIFADO);
            DropDownList ddl = (DropDownList)gridItens.Rows[e.NewEditIndex].Cells[5].FindControl("ddlLote");
            ddl.Items.Add(new ListItem("......", "0"));
            foreach (ViverMais.Model.LoteMedicamento lote in lotes)
                ddl.Items.Add(new ListItem(lote.Lote, lote.Codigo.ToString()));

            if (itens[index].LoteMedicamento != null)
                ddl.SelectedValue = itens[index].LoteMedicamento.Codigo.ToString();

            DropDownList ddl2 = (DropDownList)gridItens.Rows[e.NewEditIndex].Cells[7].FindControl("ddlCodAtendimento");
            ddl2.Items.Add(new ListItem("0", "0"));
            ddl2.Items.Add(new ListItem("1", "1"));
            ddl2.Items.Add(new ListItem("2", "2"));
            ddl2.Items.Add(new ListItem("3", "3"));
            ddl2.Items.Add(new ListItem("4", "4"));
            ddl2.Items.Add(new ListItem("5", "5"));
            ddl2.Items.Add(new ListItem("6", "6"));
            ddl2.Items.Add(new ListItem("7", "7"));
            ddl2.SelectedValue = itens[index].Cod_Atendimento.ToString();

            ddl.Focus();
            MarcaLinhaItemFornecido(itens);
        }

        protected void gridItens_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow r = gridItens.Rows[e.RowIndex];
            int index = r.RowIndex;
            if (index == 0)
                index = gridItens.PageIndex * gridItens.PageSize;
            else
                index = (gridItens.PageIndex * gridItens.PageSize) + index;

            IList<ViverMais.Model.ItemRequisicao> itens = new List<ViverMais.Model.ItemRequisicao>();
            if (Session["itens"] != null)
                itens = (List<ViverMais.Model.ItemRequisicao>)(Session["itens"]);

            int co_loteMedicamento = int.Parse(((DropDownList)r.FindControl("ddlLote")).SelectedValue);

            int i = 0;
            foreach (ViverMais.Model.ItemRequisicao it in itens)
            {
                if (it.LoteMedicamento != null)
                {
                    if ((it.Medicamento.Codigo == itens[index].Medicamento.Codigo) && (it.Elenco.Codigo == itens[index].Elenco.Codigo)
                        && (it.LoteMedicamento.Codigo == co_loteMedicamento) && (index != i))
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                                 "alert('Este Lote já se encontra na Lista!');", true);
                        return;
                    }
                }
                i++;
            }

            ViverMais.Model.Estoque estoque = Factory.GetInstance<IEstoque>().BuscaQtdEstoque<ViverMais.Model.Estoque>(co_loteMedicamento, 22);
            int qtdEstoque = estoque.QuantidadeEstoque;
            int qtdFornecida = int.Parse(((TextBox)r.FindControl("TextBox_Quantidade")).Text);
            
            if (qtdEstoque < qtdFornecida)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                                   "alert('Quantidade Insuficiente em estoque. Qtd Total = " + qtdEstoque + "');", true);
                return;
            }

            ViverMais.Model.LoteMedicamento lote = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.LoteMedicamento>(co_loteMedicamento);

            //Salva os dados no banco
            ViverMais.Model.ItemRequisicao item = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorCodigo<ViverMais.Model.ItemRequisicao>(itens[index].Codigo);
            item.LoteMedicamento = lote;
            item.QtdFornecida = qtdFornecida;
            item.Cod_Atendimento = int.Parse(((DropDownList)r.FindControl("ddlCodAtendimento")).SelectedValue);
            Factory.GetInstance<IRequisicaoMedicamento>().AlterarItemRequisicao<ViverMais.Model.ItemRequisicao>(item, itens[index].QtdFornecida, itens[index].LoteMedicamento == null ? 0 : itens[index].LoteMedicamento.Codigo);
            //Fim

            itens[index].LoteMedicamento = lote;
            itens[index].QtdFornecida = qtdFornecida; 
            itens[index].Cod_Atendimento = int.Parse(((DropDownList)r.FindControl("ddlCodAtendimento")).SelectedValue);

            gridItens.EditIndex = -1;
            Session["itens"] = itens;
            RecarregaGrid();
        }

        protected void gridItens_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow r = gridItens.Rows[e.RowIndex];
            r.BackColor = Color.WhiteSmoke;
            gridItens.EditIndex = -1;
            RecarregaGrid();
        }

        protected void btnAdicionarMedicamento_Click(object sender, EventArgs e)
        {
            IList<ViverMais.Model.ItemRequisicao> itens = (List<ViverMais.Model.ItemRequisicao>)(Session["itens"]);
            var resultado = (from i in itens group i by i.Elenco into g select new {Elenco = g.Key, Nome = g}).ToList();
            foreach (var item in resultado)
            {
                ViverMais.Model.ItemRequisicao its = item.Nome.ElementAt<ViverMais.Model.ItemRequisicao>(0);
                TreeNode root = new TreeNode("<span style='background-color: #C5D8CC; color: #000000; font: Verdana; font-size: 11px; Width: 650px; font-weight: bold; height: 17px; float: left; line-height: 20px;'>&nbsp;: : :&nbsp;&nbsp;" +
                              its.Elenco.Nome + "</span>", its.Elenco.Codigo.ToString());

                foreach (ViverMais.Model.ItemRequisicao itemRequisicao in itens)
                {
                    if (its.Elenco.Codigo == itemRequisicao.Elenco.Codigo)
                    {
                        TreeNode NodeMedicamento = new TreeNode("<span style='font-size: X-Small; color:DarkSlateGray; font-family:Verdana,Arial;'><li>"
                                                                 + itemRequisicao.Medicamento.Nome + "</li></span>", itemRequisicao.Medicamento.Codigo.ToString() + ";" + itemRequisicao.Elenco.Codigo.ToString() + "@" + itemRequisicao.QtdPedida);
                        root.ChildNodes.Add(NodeMedicamento);
                    }
                }
                TreeViewMedicamento.Nodes.Add(root);
                TreeViewMedicamento.CollapseAll();
            }
            PanelListaMedicamento.Visible = true;
            TreeViewMedicamento.Focus();
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            PanelListaMedicamento.Visible = false;
            TreeViewMedicamento.Nodes.Clear();
        }

        protected void btnFechar_Click_2(object sender, EventArgs e)
        {
            PanelAddMedicamento.Visible = false;
        }

        protected void TreeViewMedicamento_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode no = TreeViewMedicamento.SelectedNode;
            if (no.Parent != null)
            {
                PanelAddMedicamento.Visible = true;
                string co_medicamento = no.Value.Split(';')[0];
                ViewState["co_medicamento"] = co_medicamento;
                string parte2 = no.Value.Split(';')[1];
                string co_elenco = parte2.Split('@')[0];
                ViewState["co_elenco"] = co_elenco;
                string qtdSolicitada = parte2.Split('@')[1];

                ViverMais.Model.Medicamento medicamento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Medicamento>(int.Parse(co_medicamento));
                ViverMais.Model.ElencoMedicamento elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(int.Parse(co_elenco));
                ViverMais.Model.RequisicaoMedicamento rm = (ViverMais.Model.RequisicaoMedicamento)Session["rm"];
                ViverMais.Model.ItemRequisicao item = new ViverMais.Model.ItemRequisicao();

                tbxCodigo.Text = medicamento.CodMedicamento;
                tbxMedicamento.Text = medicamento.Nome;
                tbxElenco.Text = elenco.Nome;
                tbxQtdSolicitada.Text = qtdSolicitada;

                ddlLotes.Items.Add(new ListItem("............", "0"));
                IList<ViverMais.Model.LoteMedicamento> lotes = Factory.GetInstance<ILoteMedicamento>().BuscarPorEstoqueAlmoxarifado<ViverMais.Model.LoteMedicamento>(medicamento.Codigo, 1);
                foreach (ViverMais.Model.LoteMedicamento lote in lotes)
                    ddlLotes.Items.Add(new ListItem(lote.Lote, lote.Codigo.ToString()));

                ddlCodAtendimento.Items.Add(new ListItem("0", "0"));
                ddlCodAtendimento.Items.Add(new ListItem("1", "1"));
                ddlCodAtendimento.Items.Add(new ListItem("2", "2"));
                ddlCodAtendimento.Items.Add(new ListItem("3", "3"));
                ddlCodAtendimento.Items.Add(new ListItem("4", "4"));
                ddlCodAtendimento.Items.Add(new ListItem("5", "5"));
                ddlCodAtendimento.Items.Add(new ListItem("6", "6"));
                ddlCodAtendimento.Items.Add(new ListItem("7", "7"));
            }
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            IList<ViverMais.Model.ItemRequisicao> itens = (List<ViverMais.Model.ItemRequisicao>)(Session["itens"]);
            foreach (ViverMais.Model.ItemRequisicao it in itens)
            {
                if (it.LoteMedicamento != null)
                {
                    if ((it.Medicamento.Codigo == int.Parse(ViewState["co_medicamento"].ToString())) && (it.Elenco.Codigo == int.Parse(ViewState["co_elenco"].ToString()))
                        && (it.LoteMedicamento.Codigo == int.Parse(ddlLotes.SelectedValue)))
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                                 "alert('Já foi adicionado este Lote!');", true);
                        return;
                    }
                }
            }

            int co_loteMedicamento = int.Parse(ddlLotes.SelectedValue);
            ViverMais.Model.Estoque estoque = Factory.GetInstance<IEstoque>().BuscaQtdEstoque<ViverMais.Model.Estoque>(co_loteMedicamento, 1);
            int qtdEstoque = estoque.QuantidadeEstoque;
            int qtdFornecida = int.Parse(tbxQtdFornecida.Text);

            if (qtdEstoque < qtdFornecida)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                                   "alert('Quantidade Insuficiente em estoque. Qtd Total = " + qtdEstoque + "');", true);
                return;
            }

            ViverMais.Model.ItemRequisicao item = new ViverMais.Model.ItemRequisicao();
            ViverMais.Model.RequisicaoMedicamento rm = (ViverMais.Model.RequisicaoMedicamento)Session["rm"];
            item.Requisicao = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorCodigo<ViverMais.Model.RequisicaoMedicamento>(rm.Codigo);
            item.Medicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<ViverMais.Model.Medicamento>(int.Parse(ViewState["co_medicamento"] != null ? ViewState["co_medicamento"].ToString() : "0"));
            item.Elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(int.Parse(ViewState["co_elenco"] != null ? ViewState["co_elenco"].ToString() : "0"));
            item.LoteMedicamento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.LoteMedicamento>(int.Parse(ddlLotes.Text));
            item.QtdPedidaDistrito = int.Parse(tbxQtdSolicitada.Text);
            item.QtdFornecida = int.Parse(tbxQtdFornecida.Text);
            item.Cod_Atendimento = int.Parse(ddlLotes.SelectedValue);
            item.Solicitante = "A";
            itens.Add(item);
            Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(item);
            estoque.QuantidadeEstoque -= qtdFornecida;
            Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(estoque);
            Session["itens"] = itens;
            RecarregaGrid();
            PanelAddMedicamento.Visible = false;
        }

        protected void gridItens_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridItens.PageIndex = e.NewPageIndex;
            if (Session["itens"] != null)
                RecarregaGrid();
        }

        void MarcaLinhaItemFornecido(IList<ViverMais.Model.ItemRequisicao> itens)
        {
            itens = (List<ViverMais.Model.ItemRequisicao>)Session["itens"];
            int index = 0;
            for (int i = gridItens.PageIndex * gridItens.PageSize; i < (5 * (gridItens.PageIndex + 1)); i++)
            {
                if (i < itens.Count)
                {
                    if (itens[i].LoteMedicamento != null)
                    {
                        gridItens.Rows[index].BackColor = Color.DimGray;
                        gridItens.Rows[index].Cells[1].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        gridItens.Rows[index].Cells[2].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        gridItens.Rows[index].Cells[3].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        gridItens.Rows[index].Cells[4].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        gridItens.Rows[index].Cells[5].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        gridItens.Rows[index].Cells[6].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        gridItens.Rows[index].Cells[7].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                    }
                }
                else
                    break;

                index++;
            }
        }

        protected void btnCodigosAtendimento_Click(object sender, EventArgs e)
        {
            PanelCodigoAtendimento.Visible = true;
        }

        protected void btnFechar3_Click(object sender, EventArgs e)
        {
            PanelCodigoAtendimento.Visible = false;
        }
    }
}
