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
using Vida.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc;
using Vida.ServiceFacade.ServiceFacades;
using Vida.DAO;
using System.Collections.Generic;
using Vida.ServiceFacade.ServiceFacades.Farmacia;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Misc;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;

namespace Vida.View.Farmacia
{
    public partial class FormRegistrarRM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Vida.Model.RequisicaoMedicamento rm = null;
                if (Request.QueryString["codigo"] != null)
                {
                    rm = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<Vida.Model.RequisicaoMedicamento>(int.Parse(Request.QueryString["codigo"].ToString()));
                    Session["rm"] = rm;
                    lblDataCriacao.Text = rm.DataCriacao.ToString("dd/MM/yyyy");
                    lblNumeroRequisicao.Text = rm.Codigo.ToString();
                    IList<Vida.Model.ItemRequisicao> itens = Factory.GetInstance<IRequisicaoMedicamento>().BuscarItensRequisicao<Vida.Model.ItemRequisicao>(rm.Codigo);
                    if (itens != null && itens.Count > 0)
                    {
                        gridItens.DataSource = itens;
                        gridItens.DataBind();
                    }
                    else
                        itens = new List<Vida.Model.ItemRequisicao>();
                    if (rm.Cod_Status == 3)
                        Panel_ListaMedicamento.Visible = true;
                    Session["itens"] = itens;
                }
                else
                {
                    lblDataCriacao.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    Panel_ListaMedicamento.Visible = true;
                    Session.Remove("rm");
                }
                Vida.Model.Farmacia farmacia = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<Vida.Model.Farmacia>(((Vida.Model.Usuario)Session["Usuario"]).Codigo);
                Session["farmacia"] = farmacia;
                lblFarmacia.Text = farmacia.Nome;
                //IList<Vida.Model.ElencoMedicamento> elencos = Factory.GetInstance<IElencoMedicamento>().;

                //Preenche a árvore com os elencos da farmacia
                TreeViewMedicamento.Nodes.Clear();
                foreach (Vida.Model.ElencoMedicamento elenco in farmacia.Elencos)
                {
                    TreeNode root = new TreeNode("<span style='font-size: X-Small; color:DarkSlateGray; font-family:Verdana,Arial;'><b>"
                                                 +elenco.Nome+"</b></span>",elenco.Codigo.ToString());
                    root.NavigateUrl = "#";
                    foreach (Vida.Model.Medicamento medicamento in elenco.Medicamentos)
                    {
                        TreeNode NodeMedicamento = new TreeNode("<span style='font-size: X-Small; color:DarkSlateGray; font-family:Verdana,Arial;'>"
                                                                 +medicamento.Nome+"</span>"
                                                                 ,medicamento.Codigo.ToString() + ";" + elenco.Codigo.ToString());
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
            Vida.Model.Farmacia farmacia = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<Vida.Model.Farmacia>(2);
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Vida.Model.RequisicaoMedicamento rm;
            Vida.Model.Farmacia farmacia = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<Vida.Model.Farmacia>(((Vida.Model.Usuario)Session["Usuario"]).Codigo);

            if (Request.QueryString["codigo"] != null)//editando uma requisicao já existente
            {
                rm = (Vida.Model.RequisicaoMedicamento)Session["rm"];
                if (rm.Cod_Status != 3)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Falha! Esta requisição já foi encaminhada para o Distrito Sanitário!');", true);
                    return;
                }
                Factory.GetInstance<IFarmaciaServiceFacade>().Atualizar(rm);
            }
            else //cadastrando uma nova rm
            {
                //se existe rm aberta
                if (Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorFarmacia<Vida.Model.RequisicaoMedicamento>(farmacia.Codigo,3).Count() > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Falha! Já existe uma requisição aberta para esta farmácia!');", true);
                    return;
                }
                rm = new Vida.Model.RequisicaoMedicamento();
                rm.DataCriacao = DateTime.Now;
                rm.Farmacia = farmacia;
                rm.Data_Status = DateTime.Now;
                rm.Cod_Status = 3; //rm aberta
                Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(rm);
                Session["rm"] = rm;
            }

           

            //Insere ou atualiza os itens da requisição
            IList<Vida.Model.ItemRequisicao> itens = (IList<Vida.Model.ItemRequisicao>)Session["itens"];
            if (itens != null && itens.Count > 0)
                foreach (Vida.Model.ItemRequisicao i in itens)
                {
                    i.Requisicao = rm;
                    i.Solicitante = "U";
                    if(i.Codigo == 0)
                        Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(i);
                    else
                        Factory.GetInstance<IFarmaciaServiceFacade>().Atualizar(i);

                }

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Requisição salva com sucesso.');", true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O Nº desta Requisição é "+rm.Codigo+"');", true);
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
            btnCancelar.Focus();
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
            DateTime dataUltimaRequisicao = Factory.GetInstance<IRequisicaoMedicamento>().BuscarDataUltimaRequisicao();
            Vida.Model.Farmacia farmacia = (Vida.Model.Farmacia)Session["farmacia"];
            int consumoDispensacao = Factory.GetInstance<IRequisicaoMedicamento>().CalculaConsumoDispensacao(dataUltimaRequisicao, DateTime.Today, itens.ElementAt<Vida.Model.ItemRequisicao>(e.RowIndex).Medicamento.Codigo, farmacia.Codigo);
            itens.ElementAt<Vida.Model.ItemRequisicao>(e.RowIndex).Consumo = consumoDispensacao;
            int saldoAtual = Factory.GetInstance<IRequisicaoMedicamento>().BuscarSaldoAtual(itens.ElementAt<Vida.Model.ItemRequisicao>(e.RowIndex).Medicamento.Codigo, farmacia.Codigo);
            itens.ElementAt<Vida.Model.ItemRequisicao>(e.RowIndex).SaldoAtual = saldoAtual;
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
                        gridItens.Columns[3].Visible = false;//não permite alterar os campos                }
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
            if (rm.Cod_Status == 2)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Falha! Esta requisição já foi enviada!');", true);
                return;
            }
            rm.Cod_Status = 2;
            rm.DataEnvio = DateTime.Now;
            Factory.GetInstance<IFarmaciaServiceFacade>().Atualizar(rm);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Requisição enviada com sucesso!');document.location.href='FormRegistrarRM.aspx?codigo=" + rm.Codigo + "';", true);
            
        }
    }
}
