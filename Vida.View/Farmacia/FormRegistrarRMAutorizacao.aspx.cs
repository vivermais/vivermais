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
using Vida.DAO;
using Vida.ServiceFacade.ServiceFacades;
using System.Collections.Generic;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Misc;

namespace Vida.View.Farmacia
{
    public partial class FormRegistrarRMAutorizacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaCampos();
                Vida.Model.RequisicaoMedicamento rm = null;
                if (Request.QueryString["codigo"] != null)
                {
                    rm = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<Vida.Model.RequisicaoMedicamento>(int.Parse(Request.QueryString["codigo"].ToString()));
                    lblDataCriacao.Text = rm.DataCriacao.ToShortDateString();
                    lblNumeroRequisicao.Text = rm.Codigo.ToString();
                    ddlFarmacia.SelectedValue = rm.Farmacia.Codigo.ToString();
                    IList<Vida.Model.ItemRequisicao> itens = Factory.GetInstance<IRequisicaoMedicamento>().BuscarItensRequisicao<Vida.Model.ItemRequisicao>(rm.Codigo);
                    if (itens != null && itens.Count > 0)
                    {
                        gridItens.DataSource = itens;
                        gridItens.DataBind();
                    }
                    else
                        itens = new List<Vida.Model.ItemRequisicao>();
                    Session["itens"] = itens;
                    Session["rm"] = rm;
                }
                else
                    lblDataCriacao.Text = DateTime.Today.ToShortDateString();
                
                
                //IList<Vida.Model.ElencoMedicamento> elencos = Factory.GetInstance<IElencoMedicamento>().;
                Vida.Model.Farmacia farmacia = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<Vida.Model.Farmacia>(((Vida.Model.Usuario)Session["Usuario"]).Codigo);
                //Preenche a árvore com os elencos da farmacia
                TreeViewMedicamento.Nodes.Clear();
                foreach (Vida.Model.ElencoMedicamento elenco in farmacia.Elencos)
                {
                    TreeNode root = new TreeNode(elenco.Nome, elenco.Codigo.ToString());
                    root.NavigateUrl = "#";
                    foreach (Vida.Model.Medicamento medicamento in elenco.Medicamentos)
                    {
                        TreeNode NodeMedicamento = new TreeNode(medicamento.Nome, medicamento.Codigo.ToString() + ";" + elenco.Codigo.ToString());
                        root.ChildNodes.Add(NodeMedicamento);
                    }
                    TreeViewMedicamento.Nodes.Add(root);
                    TreeViewMedicamento.CollapseAll();
                }
            }
        }

        void CarregaCampos()
        {
            //Mudar para buscar somente as farmacias do distrito
                IList<Vida.Model.Farmacia> farmacias = Factory.GetInstance<IFarmacia>().ListarTodos<Vida.Model.Farmacia>();
                ddlFarmacia.Items.Add(new ListItem("Selecione...","0"));
                foreach (Vida.Model.Farmacia f in farmacias)
                { 
                    ddlFarmacia.Items.Add(new ListItem(f.Nome,f.Codigo.ToString()));
                }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Vida.Model.RequisicaoMedicamento rm;
            if (Session["rm"] != null)
            {
                rm = (Vida.Model.RequisicaoMedicamento)Session["rm"];
                if (rm.Cod_Status != "3")
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Falha! Esta requisição já foi encaminhada para o Distrito Sanitário!');", true);
                    return;
                }
            }
            else
                rm = new Vida.Model.RequisicaoMedicamento();

            rm.DataCriacao = DateTime.Parse(lblDataCriacao.Text);
            Vida.Model.Farmacia farmacia = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<Vida.Model.Farmacia>(((Vida.Model.Usuario)Session["Usuario"]).Codigo);
            rm.Farmacia = farmacia;
            rm.Data_Status = DateTime.Now;
            rm.Cod_Status = "3"; //rm aberta
            Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(rm);
            IList<Vida.Model.ItemRequisicao> itens = (IList<Vida.Model.ItemRequisicao>)Session["itens"];
            if (itens != null && itens.Count > 0)
                foreach (Vida.Model.ItemRequisicao i in itens)
                {
                    i.Requisicao = rm;
                    i.Solicitante = "U";
                    Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(i);
                }

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Requisição salva com sucesso.');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O Nº desta Requisição é " + rm.Codigo + "');", true);
        }

        protected void gridItens_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TreeViewMedicamento_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode no = TreeViewMedicamento.SelectedNode;
            Vida.Model.Medicamento medicamento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<Vida.Model.Medicamento>(int.Parse(no.Value.Split(';')[0]));
            Vida.Model.ElencoMedicamento elenco = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<Vida.Model.ElencoMedicamento>(int.Parse(no.Value.Split(';')[1]));

            Vida.Model.ItemRequisicao item = new Vida.Model.ItemRequisicao();
            item.Medicamento = medicamento;
            item.Elenco = elenco;

            IList<Vida.Model.ItemRequisicao> itens;
            if (Session["itens"] != null)
                itens = (IList<Vida.Model.ItemRequisicao>)Session["itens"];
            else
                itens = new List<Vida.Model.ItemRequisicao>();
            foreach (Vida.Model.ItemRequisicao i in itens)
            {
                if (i.Medicamento.Codigo == item.Medicamento.Codigo && i.Elenco.Codigo == item.Elenco.Codigo)
                    return;
            }
            itens.Add(item);
            gridItens.DataSource = itens;
            gridItens.DataBind();
            Session["itens"] = itens;
        }

        /// <summary>
        /// atualiza a quantidade de um item de requisicao
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnAdicionar_Click1(object sender, EventArgs e)
        //{
        //    IList<Vida.Model.ItemRequisicao> itens = (IList<Vida.Model.ItemRequisicao>)Session["itens"];
        //    itens[gridItens.SelectedIndex].QtdPedida = int.Parse(tbxQuantidade.Text);
        //    gridItens.DataSource = itens;
        //    gridItens.DataBind();
        //    Session["itens"] = itens;
        //}

        void RecarregaGrid()
        {
            IList<Vida.Model.ItemRequisicao> itens = (IList<Vida.Model.ItemRequisicao>)Session["itens"];
            gridItens.DataSource = itens;
            gridItens.DataBind();
        }

        protected void gridItens_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            IList<Vida.Model.ItemRequisicao> itens = (IList<Vida.Model.ItemRequisicao>)Session["itens"];
            GridViewRow r = gridItens.Rows[e.RowIndex];
            itens.ElementAt<Vida.Model.ItemRequisicao>(e.RowIndex).QtdPedida = int.Parse(((TextBox)r.FindControl("TextBox_Quantidade")).Text);
            gridItens.EditIndex = -1;
            RecarregaGrid();
        }

        protected void gridItens_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gridItens.EditIndex = e.NewEditIndex;
            RecarregaGrid();
        }

        protected void gridItens_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IList<Vida.Model.ItemRequisicao> itens = (IList<Vida.Model.ItemRequisicao>)Session["itens"];
            itens.RemoveAt(e.RowIndex);
            //gridItens.DeleteRow(e.RowIndex);
            RecarregaGrid();
        }

        protected void gridItens_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridItens.EditIndex = -1;
            RecarregaGrid();
        }

        protected void gridItens_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Vida.Model.RequisicaoMedicamento rm;
                if (Session["rm"] != null)
                {
                    rm = (Vida.Model.RequisicaoMedicamento)Session["rm"];
                    if (rm.Status != "ABERTO")
                        e.Row.Controls[3].Visible = false;//não permite alterar os campos
                }

            }

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (Session["rm"] == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Salve ou Pesquise uma requisição para realizar esta operação!');", true);
                return;
            }
            Vida.Model.RequisicaoMedicamento rm = (Vida.Model.RequisicaoMedicamento)Session["rm"];
            if (rm.Cod_Status == "2")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Falha! Esta requisição já foi enviada!');", true);
                return;
            }
            rm.Cod_Status = "2";
            rm.DataEnvio = DateTime.Now;
            Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(rm);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Requisição enviada com sucesso!');", true);
        }
    }
}
