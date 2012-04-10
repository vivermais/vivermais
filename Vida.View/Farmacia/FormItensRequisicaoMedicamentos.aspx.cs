using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormItensRequisicaoMedicamentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REGISTRAR_REQUISICAO_MEDICAMENTO",Modulo.FARMACIA))
                {
                    int temp;
                    Session.Remove("requisicao");
                    Session.Remove("itensrequisicao");

                    if (Request["co_requisicao"] != null && int.TryParse(Request["co_requisicao"].ToString(), out temp))
                    {
                        RequisicaoMedicamento requisicao = Factory.GetInstance<IRequisicaoMedicamento>().BuscarPorCodigo<RequisicaoMedicamento>(temp);
                        Label_Farmacia.Text = requisicao.Farmacia.Nome;
                        Label_NumeroRequisicao.Text = requisicao.Codigo.ToString();
                        Label_DataAbertura.Text = requisicao.DataCriacao.ToString("dd/MM/yyyy");
                        Label_Status.Text = requisicao.Status;
                        Label_DataEnvioDistrito.Text = requisicao.DataEnvio.HasValue ? requisicao.DataEnvio.Value.ToString("dd/MM/yyyy") : "-";

                        IList<ItemRequisicao> itensrequisicao = Factory.GetInstance<IRequisicaoMedicamento>().BuscarItensRequisicao<ItemRequisicao>(requisicao.Codigo);

                        if (requisicao.Cod_Status == (int)RequisicaoMedicamento.StatusRequisicao.ABERTA)
                        {
                            TreeView_Medicamentos.Visible = true;
                            this.CarregaTreeViewMedicamentos(requisicao.Farmacia, itensrequisicao);
                            LinkButton_EnviarRequisicao.Visible = true;
                            LinkButton_EnviarRequisicao.OnClientClick = "javascript:return confirm('Tem certeza que deseja enviar esta requisição ao distrito ?');";
                        }
                        else
                            LabelElencosFarmacias.Visible = true;

                        Session["requisicao"] = requisicao;
                        Session["itensrequisicao"] = itensrequisicao;

                        this.CarregaGridViewMedicamentosSolicitados(itensrequisicao);
                    }
                }else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Carrega os medicamentos disponíveis para solicitar de acordo com os elencos da farmácia
        /// </summary>
        /// <param name="farmacia">Farmácia de onde será retirado os elencos</param>
        /// <param name="itensrequisicao">Itens da Requisição atual</param>
        private void CarregaTreeViewMedicamentos(ViverMais.Model.Farmacia farmacia, IList<ItemRequisicao> itensrequisicao)
        {
            TreeView_Medicamentos.Nodes.Clear();

            foreach (ElencoMedicamento elenco in farmacia.Elencos.OrderBy(p=>p.Nome).ToList())
            {
                TreeNode nodoelenco = new TreeNode(elenco.Nome, elenco.Codigo.ToString());
                nodoelenco.NavigateUrl = "#";

                foreach (Medicamento medicamento in elenco.Medicamentos.OrderBy(p => p.Nome).ToList())
                {
                    if (itensrequisicao.Where(p => p.Medicamento.Codigo == medicamento.Codigo).FirstOrDefault() == null)
                    {
                        TreeNode nodomedicamento = new TreeNode(medicamento.Nome, medicamento.Codigo.ToString());
                        nodoelenco.ChildNodes.Add(nodomedicamento);
                    }
                }

                if (nodoelenco.ChildNodes != null && nodoelenco.ChildNodes.Count > 0)
                    TreeView_Medicamentos.Nodes.Add(nodoelenco);
            }

            TreeView_Medicamentos.CollapseAll();

            LabelElencosFarmacias.Visible = (TreeView_Medicamentos.Nodes != null && TreeView_Medicamentos.Nodes.Count > 0) ? false : true;
        }

        /// <summary>
        /// Seleciona um medicamento para incluí-lo na requisição
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedNodeChanged_IncluirMedicamento(object sender, EventArgs e)
        {
            TreeNode nodoselecionado = TreeView_Medicamentos.SelectedNode;
            //Panel_IncluirMedicamento.Visible = true;

            if (nodoselecionado.Parent != null) //É filho
            {
                Medicamento medicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<ViverMais.Model.Medicamento>(int.Parse(nodoselecionado.Value));
                Label_MedicamentoSelecionado.Text = medicamento.Nome;
                Label_PosicaoAtualEstoqueMedicamento.Text = Factory.GetInstance<IEstoque>().BuscarQuantidadeEstoqueMedicamento(((RequisicaoMedicamento)Session["requisicao"]).Farmacia.Codigo, medicamento.Codigo).ToString();
                ViewState["co_medicamentoselecionado"] = medicamento.Codigo;
                ModalPopupExtender_IncluirMedicamento.Show();
            }
            else
                ModalPopupExtender_IncluirMedicamento.Hide();

            
            TreeView_Medicamentos.SelectedNode.Selected = false;
        }

        /// <summary>
        /// Paginação do gridview de medicamentos da requisição
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Medicamentos(object sender, GridViewPageEventArgs e)
        {
            this.CarregaGridViewMedicamentosSolicitados((IList<ItemRequisicao>)Session["itensrequisicao"]);
            GridView_MedicamentosSolicitados.PageIndex = e.NewPageIndex;
            GridView_MedicamentosSolicitados.DataBind();
        }

        /// <summary>
        /// Configurar o gridview dos medicamentos solicitados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_ConfigurarGridMedicamentos(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbexcluir = (LinkButton)e.Row.Controls[4].Controls[0];

                if (lbexcluir != null)
                    lbexcluir.OnClientClick = "javascript:return confirm('Deseja realmente excluir este item da requisição ?');";

                if (((RequisicaoMedicamento)Session["requisicao"]).Cod_Status != (int)RequisicaoMedicamento.StatusRequisicao.ABERTA)
                {
                    GridView_MedicamentosSolicitados.Columns[3].Visible = false;
                    GridView_MedicamentosSolicitados.Columns[4].Visible = false;
                }
            }
        }

        /// <summary>
        /// Habilita o campo Quantidade Solicitada para edição do item da requisição
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_ItemRequisicao(object sender, GridViewEditEventArgs e)
        {
            IList<ItemRequisicao> itensrequisicao = (IList<ItemRequisicao>)Session["itensrequisicao"];
            GridView_MedicamentosSolicitados.EditIndex = e.NewEditIndex;
            this.CarregaGridViewMedicamentosSolicitados(itensrequisicao);

            GridViewRow row = GridView_MedicamentosSolicitados.Rows[e.NewEditIndex];
            ItemRequisicao itemrequisicao = itensrequisicao.Where(p=>p.Codigo == int.Parse(GridView_MedicamentosSolicitados.DataKeys[row.RowIndex]["Codigo"].ToString())).First();

            ((Label)row.FindControl("Label_QtdEstoqueAlterar")).Text = Factory.GetInstance<IEstoque>().BuscarQuantidadeEstoqueMedicamento(itemrequisicao.Requisicao.Farmacia.Codigo, itemrequisicao.Medicamento.Codigo).ToString();
        }

        /// <summary>
        /// Cancela a edição do campo Quantidade Solicitada do item da requisição
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_ItemRequisicao(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_MedicamentosSolicitados.EditIndex = -1;
            this.CarregaGridViewMedicamentosSolicitados((IList<ItemRequisicao>)Session["itensrequisicao"]);
        }

        /// <summary>
        /// Altera a Quantidade Solicitada do item da requisição
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_ItemRequisicao(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                IRequisicaoMedicamento iRequisicao = Factory.GetInstance<IRequisicaoMedicamento>();

                GridViewRow rowGrid = GridView_MedicamentosSolicitados.Rows[e.RowIndex];
                TextBox tbxQuantidadeSolicitada = (TextBox)rowGrid.FindControl("TextBox_QtdSolicitada");
                ItemRequisicao itemrequisicao = iRequisicao.BuscarPorCodigo<ItemRequisicao>(int.Parse(GridView_MedicamentosSolicitados.DataKeys[e.RowIndex]["Codigo"].ToString()));
                itemrequisicao.QtdPedida = int.Parse(tbxQuantidadeSolicitada.Text);
                itemrequisicao.SaldoAtual = int.Parse(((Label)rowGrid.FindControl("Label_QtdEstoqueAlterar")).Text);

                iRequisicao.Atualizar(itemrequisicao);
                iRequisicao.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.ALTERAR_ITEM_REQUISICAO_MEDICAMENTO,
                    "id requisicao: " + itemrequisicao.Requisicao.Codigo + " item requisicao: " + itemrequisicao.Medicamento.Nome + " quantidade: " + itemrequisicao.QtdPedida));

                IList<ItemRequisicao> itensrequisicao = (IList<ItemRequisicao>)Session["itensrequisicao"];
                var itemsessao = itensrequisicao.Select((Item, index) => new { index, Item }).Where(p => p.Item.Codigo == itemrequisicao.Codigo).First();
                itensrequisicao[itemsessao.index] = itemrequisicao;

                Session["itensrequisicao"] = itensrequisicao;
                GridView_MedicamentosSolicitados.EditIndex = -1;
                this.CarregaGridViewMedicamentosSolicitados(itensrequisicao);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Item alterado com sucesso.');", true);
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Exclui o item da requisição
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_ExcluirItemRequisicao(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_Excluir")
            {
                IRequisicaoMedicamento iRequisicao = Factory.GetInstance<IRequisicaoMedicamento>();
                int co_itemrequisicao = int.Parse(GridView_MedicamentosSolicitados.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString());
                IList<ItemRequisicao> itensrequisicao = (IList<ItemRequisicao>)Session["itensrequisicao"];
                
                var itemsessao = itensrequisicao.Select((Item, index) => new { index, Item }).Where(p => p.Item.Codigo == co_itemrequisicao).First();
                itensrequisicao.RemoveAt(itemsessao.index);
                Session["itensrequisicao"] = itensrequisicao;

                ItemRequisicao itemrequisicao = iRequisicao.BuscarPorCodigo<ItemRequisicao>(co_itemrequisicao);

                iRequisicao.Deletar(itemrequisicao);
                iRequisicao.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.EXCLUIR_ITEM_REQUISICAO_MEDICAMENTO,
                    "id requisicao: " + itemrequisicao.Requisicao.Codigo + " item requisicao: " + itemrequisicao.Medicamento.Nome));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Item excluído com sucesso.');", true);
                this.CarregaGridViewMedicamentosSolicitados(itensrequisicao);
                this.CarregaTreeViewMedicamentos(((RequisicaoMedicamento)Session["requisicao"]).Farmacia, itensrequisicao);
                //Panel_IncluirMedicamento.Visible = false;
            }
        }

        /// <summary>
        /// Carrega o gridview de medicamentos na requisição com os itens passados por parâmetro
        /// </summary>
        /// <param name="itensrequisicao"></param>
        private void CarregaGridViewMedicamentosSolicitados(IList<ItemRequisicao> itensrequisicao)
        {
            GridView_MedicamentosSolicitados.DataSource = itensrequisicao;
            GridView_MedicamentosSolicitados.DataBind();
        }

        /// <summary>
        /// Cancela a inclusão de um novo medicamento na requisição atual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarInclusaoMedicamento(object sender, EventArgs e)
        {
            ViewState.Remove("co_medicamentoselecionado");
            Label_MedicamentoSelecionado.Text = "";
            Label_PosicaoAtualEstoqueMedicamento.Text = "";
            TextBox_QuantidadeSolicitadaMedicamento.Text = "";
            ModalPopupExtender_IncluirMedicamento.Hide();
            TreeView_Medicamentos.CollapseAll();
        }

        /// <summary>
        /// Inclui um novo medicamento na requisição atual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_IncluirNovoMedicamento(object sender, EventArgs e)
        {
            RequisicaoMedicamento requisicao = ((RequisicaoMedicamento)Session["requisicao"]);
            IList<ItemRequisicao> itensrequisicao = (IList<ItemRequisicao>)Session["itensrequisicao"];

            ItemRequisicao itemrequisicao = new ItemRequisicao();
            itemrequisicao.SaldoAtual = int.Parse(Label_PosicaoAtualEstoqueMedicamento.Text);
            itemrequisicao.Requisicao = requisicao;
            itemrequisicao.Medicamento = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<ViverMais.Model.Medicamento>(int.Parse(ViewState["co_medicamentoselecionado"].ToString()));
            itemrequisicao.QtdPedida = int.Parse(TextBox_QuantidadeSolicitadaMedicamento.Text);

            Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(itemrequisicao);
            Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.INCLUIR_ITEM_REQUISICAO_MEDICAMENTO, "id requisicao: " + requisicao.Codigo + " id item requisicao: " + itemrequisicao.Codigo));

            itensrequisicao.Add(itemrequisicao);
            Session["itensrequisicao"] = itensrequisicao;

            this.OnClick_CancelarInclusaoMedicamento(new object(), new EventArgs());
            this.CarregaTreeViewMedicamentos(requisicao.Farmacia, itensrequisicao);
            this.CarregaGridViewMedicamentosSolicitados(itensrequisicao);

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Item incluído com sucesso.');", true);
        }

        /// <summary>
        /// Atualiza o status da requisição e envia ao distrito da farmácia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_EnviarDistrito(object sender, EventArgs e)
        {
            IList<ItemRequisicao> itensrequisicao = (IList<ItemRequisicao>)Session["itensrequisicao"];
            RequisicaoMedicamento requisicao = ((RequisicaoMedicamento)Session["requisicao"]);

            if (itensrequisicao.Count() > 0)
            {
                requisicao.Cod_Status = (int)RequisicaoMedicamento.StatusRequisicao.PENDENTE;
                requisicao.DataEnvio = DateTime.Now;
                Factory.GetInstance<IRequisicaoMedicamento>().Atualizar(requisicao);
                Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.ENVIAR_REQUISICAO_DISTRITO,
                    "id requisicao: " + requisicao.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Sua solicitação foi enviada com sucesso para o distrito de sua farmácia.');location='FormItensRequisicaoMedicamentos.aspx?co_requisicao=" + requisicao.Codigo + "';", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, para enviar sua solicitação ao distrito de sua farmácia, é necessário a inclusão de pelo menos um medicamento.');", true);
        }
    }
}
