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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using System.Drawing;

namespace ViverMais.View.Farmacia
{
    public partial class FormAutorizarRM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViverMais.Model.RequisicaoMedicamento rm = null;
                if (Request.QueryString["codigo"] != null)
                {
                    rm = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.RequisicaoMedicamento>(int.Parse(Request.QueryString["codigo"].ToString()));
                    if (rm.Status != "PENDENTE")
                    {
                        gridItens.Columns[5].Visible = false;
                        gridItens.Columns[6].Visible = false;
                    }
                    lblDataCriacao.Text = rm.DataCriacao.ToString("dd/MM/yyyy");
                    lblDataEnvio.Text = rm.DataEnvio.Value.ToString("dd/MM/yyyy");
                    lblNumeroRequisicao.Text = rm.Codigo.ToString();
                    IList<ViverMais.Model.ItemRequisicao> itens = Factory.GetInstance<IRequisicaoMedicamento>().BuscarItensRequisicao<ViverMais.Model.ItemRequisicao>(rm.Codigo);
                    if (itens != null && itens.Count > 0)
                    {
                        gridItens.DataSource = itens;
                        gridItens.DataBind();
                    }
                    else
                        itens = new List<ViverMais.Model.ItemRequisicao>();
                    
                    Session["itens"] = itens;
                    Session["rm"] = rm;
                }
                else
                    lblDataCriacao.Text = DateTime.Today.ToShortDateString();

                //ViverMais.Model.Farmacia farmacia = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo);
                ViverMais.Model.Farmacia farmacia = new ViverMais.Model.Farmacia();

                lblFarmacia.Text = farmacia.Nome;
                //IList<ViverMais.Model.ElencoMedicamento> elencos = Factory.GetInstance<IElencoMedicamento>().;

                //Preenche a árvore com os elencos da farmacia
                TreeViewMedicamento.Nodes.Clear();
                foreach (ViverMais.Model.ElencoMedicamento elenco in farmacia.Elencos)
                {
                    TreeNode root = new TreeNode("<span style='background-color: #C5D8CC; color: #000000; font: Verdana; font-size: 11px; Width: 650px; font-weight: bold; height: 17px; float: left; line-height: 20px;'>&nbsp;: : :&nbsp;&nbsp;" +
                                                  elenco.Nome + "</span>", elenco.Codigo.ToString());
                    root.NavigateUrl = "#";
                    foreach (ViverMais.Model.Medicamento medicamento in elenco.Medicamentos)
                    {
                        TreeNode NodeMedicamento = new TreeNode("<span style='font-size: X-Small; color:DarkSlateGray; font-family:Verdana,Arial;'><li>"
                                                                 + medicamento.Nome + "</li></span>", medicamento.Codigo.ToString() + ";" + elenco.Codigo.ToString());
                        root.ChildNodes.Add(NodeMedicamento);
                    }
                    TreeViewMedicamento.Nodes.Add(root);
                    TreeViewMedicamento.CollapseAll();
                }
            }
        }

        void CarregaCampos()
        {
            //depois mudar para puxar da sessao
            ViverMais.Model.Farmacia farmacia = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Farmacia>(2);
        }

        protected void TreeViewMedicamento_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode no = TreeViewMedicamento.SelectedNode;
            ViverMais.Model.Medicamento medicamento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Medicamento>(int.Parse(no.Value.Split(';')[0]));
            ViverMais.Model.ElencoMedicamento elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(int.Parse(no.Value.Split(';')[1]));
            ViverMais.Model.RequisicaoMedicamento rm = (ViverMais.Model.RequisicaoMedicamento)Session["rm"];
            ViverMais.Model.ItemRequisicao item = new ViverMais.Model.ItemRequisicao();
            
            item.Medicamento = medicamento;
            item.Elenco = elenco;
            item.Requisicao = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.RequisicaoMedicamento>(rm.Codigo);

            IList<ViverMais.Model.ItemRequisicao> itens;
            if (Session["itens"] != null)
                itens = (IList<ViverMais.Model.ItemRequisicao>)Session["itens"];
            else
                itens = new List<ViverMais.Model.ItemRequisicao>();
            foreach (ViverMais.Model.ItemRequisicao i in itens)
            {
                if (i.Medicamento.Codigo == item.Medicamento.Codigo && i.Elenco.Codigo == item.Elenco.Codigo)
                    return;
            }
            itens.Add(item);
            gridItens.DataSource = itens;
            gridItens.DataBind();
            Session["itens"] = itens;
            gridItens.Focus();
        }

        /// <summary>
        /// atualiza a quantidade de um item de requisicao
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnAdicionar_Click1(object sender, EventArgs e)
        //{
        //    IList<ViverMais.Model.ItemRequisicao> itens = (IList<ViverMais.Model.ItemRequisicao>)Session["itens"];
        //    itens[gridItens.SelectedIndex].QtdPedida = int.Parse(tbxQuantidade.Text);
        //    gridItens.DataSource = itens;
        //    gridItens.DataBind();
        //    Session["itens"] = itens;
        //}

        void RecarregaGrid()
        {
            IList<ViverMais.Model.ItemRequisicao> itens = (IList<ViverMais.Model.ItemRequisicao>)Session["itens"];
            gridItens.DataSource = itens;
            gridItens.DataBind();
        }

        protected void gridItens_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            IList<ViverMais.Model.ItemRequisicao> itens = (IList<ViverMais.Model.ItemRequisicao>)Session["itens"];
            GridViewRow r = gridItens.Rows[e.RowIndex];
            itens.ElementAt<ViverMais.Model.ItemRequisicao>(e.RowIndex).QtdPedidaDistrito = int.Parse(((TextBox)r.FindControl("TextBox_Quantidade")).Text);
            int co_itemRequisicao = itens.ElementAt<ViverMais.Model.ItemRequisicao>(e.RowIndex).Codigo;
            ViverMais.Model.ItemRequisicao item;
            if (co_itemRequisicao == 0)
            {
                item = new ViverMais.Model.ItemRequisicao();
                item.Medicamento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Medicamento>(itens.ElementAt<ViverMais.Model.ItemRequisicao>(e.RowIndex).Medicamento.Codigo);
                item.Requisicao = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.RequisicaoMedicamento>(itens.ElementAt<ViverMais.Model.ItemRequisicao>(e.RowIndex).Requisicao.Codigo);
                item.Elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ElencoMedicamento>(itens.ElementAt<ViverMais.Model.ItemRequisicao>(e.RowIndex).Elenco.Codigo);
                item.Cod_Atendimento = 0;
                DateTime dataUltimaRequisicao = Factory.GetInstance<IRequisicaoMedicamento>().BuscarDataUltimaRequisicao();
                int consumoDispensacao = Factory.GetInstance<IRequisicaoMedicamento>().CalculaConsumoDispensacao(dataUltimaRequisicao, DateTime.Today,
                                                                                                                 itens.ElementAt<ViverMais.Model.ItemRequisicao>(e.RowIndex).Medicamento.Codigo,
                                                                                                                 itens.ElementAt<ViverMais.Model.ItemRequisicao>(e.RowIndex).Requisicao.Farmacia.Codigo);
                item.Consumo = consumoDispensacao;
                item.SaldoAtual = Factory.GetInstance<IRequisicaoMedicamento>().BuscarSaldoAtual(itens.ElementAt<ViverMais.Model.ItemRequisicao>(e.RowIndex).Medicamento.Codigo,
                                                                                                 itens.ElementAt<ViverMais.Model.ItemRequisicao>(e.RowIndex).Requisicao.Farmacia.Codigo);
                item.Solicitante = "D";
                item.QtdPedida = 0;
            }
            else
                item = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorCodigo<ViverMais.Model.ItemRequisicao>(co_itemRequisicao);
            
            item.QtdPedidaDistrito = int.Parse(((TextBox)r.FindControl("TextBox_Quantidade")).Text);
            Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(item);
            itens.ElementAt<ViverMais.Model.ItemRequisicao>(e.RowIndex).Codigo = item.Codigo;
            r.BackColor = Color.WhiteSmoke;
            gridItens.EditIndex = -1;
            Session["itens"] = itens;
            RecarregaGrid();
        }

        protected void gridItens_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridItens.EditIndex = e.NewEditIndex;
            RecarregaGrid();
            GridViewRow r = gridItens.Rows[e.NewEditIndex];
            r.BackColor = Color.LightGray;
        }

        protected void gridItens_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow r = gridItens.Rows[e.RowIndex];
            r.BackColor = Color.WhiteSmoke;
            gridItens.EditIndex = -1;
            RecarregaGrid();
        }

        protected void gridItens_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ViverMais.Model.RequisicaoMedicamento rm;
            //    if (Session["rm"] != null)
            //    {
            //        rm = (ViverMais.Model.RequisicaoMedicamento)Session["rm"];
            //        if (rm.Status != "PENDENTE")
            //        {
            //            e.Row.Controls[5].Visible = false;//não permite alterar os campos
            //            e.Row.Controls[6].Visible = false;//não permite alterar os campos
            //        }
            //    }
            //}
        }

        protected void gridItens_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeletarItem")
            {
                IList<ViverMais.Model.ItemRequisicao> itens = (IList<ViverMais.Model.ItemRequisicao>)Session["itens"];
                int index = int.Parse(e.CommandArgument.ToString()) == 0 ? gridItens.PageIndex * gridItens.PageSize : (gridItens.PageIndex * gridItens.PageSize) + int.Parse(e.CommandArgument.ToString());
                GridViewRow row = gridItens.Rows[index];
                int co_itemRequisicao = int.Parse(Server.HtmlDecode(row.Cells[0].Text));
                if (co_itemRequisicao != 0)
                {
                    ViverMais.Model.ItemRequisicao item = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorCodigo<ViverMais.Model.ItemRequisicao>(co_itemRequisicao);
                    Factory.GetInstance<IFarmaciaServiceFacade>().Deletar(item);
                }
                itens.RemoveAt(index);
                Session["itens"] = itens;
                RecarregaGrid();
            }
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            PanelListaMedicamento.Visible = false;
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (Session["rm"] == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Salve ou Pesquise uma requisição para realizar esta operação!');", true);
                return;
            }

            IList<ViverMais.Model.ItemRequisicao> itens = (IList<ViverMais.Model.ItemRequisicao>)Session["itens"];
            foreach (ViverMais.Model.ItemRequisicao item in itens)
            {
                if (item.QtdPedidaDistrito == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Há quantidade(s) zerada(s)!');", true);
                    return;
                }
            }

            ViverMais.Model.RequisicaoMedicamento rm = (ViverMais.Model.RequisicaoMedicamento)Session["rm"];
            if (rm.Cod_Status == 4)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Falha! Esta requisição já foi autorizada!');", true);
                return;
            }

            rm.Cod_Status = 4;
            rm.Data_Autorizacao = DateTime.Now;
            Factory.GetInstance<IFarmaciaServiceFacade>().Atualizar(rm);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Requisição ENVIADA com sucesso!');document.location.href='FormAutorizarRM.aspx?codigo=" + rm.Codigo + "';", true);
        }

        protected void btnAdicionarMedicamento_Click(object sender, EventArgs e)
        {
            if (((ViverMais.Model.RequisicaoMedicamento)Session["rm"]).Cod_Status == 4)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Falha! Esta requisição já foi autorizada!');", true);
                return;
            }
            PanelListaMedicamento.Visible = true;
            TreeViewMedicamento.Focus();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ViverMais.Model.RequisicaoMedicamento rm = (ViverMais.Model.RequisicaoMedicamento)Session["rm"];
            rm.Cod_Status = 6;
            Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(rm);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Requisição CANCELADA!');", true);
        }
    }
}
