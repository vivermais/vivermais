using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Urgencia
{
    public partial class FormKitPA2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_KIT_PA", Modulo.URGENCIA))
                {
                    int co_kit = -1;
                    if (Request["co_kit"] != null)
                    {
                        ViewState["co_kit"] = Request["co_kit"].ToString();
                        KitPA kit = Factory.GetInstance<IKitPA>().BuscarPorCodigo<KitPA>(int.Parse(ViewState["co_kit"].ToString()));

                        TextBox_Nome.Text = kit.Nome;
                        co_kit = kit.Codigo;
                    }

                    CarregaItensKitDisponiveis(co_kit);
                    CarregaItensKitIncluidos(co_kit);

                    CarregaMedicamentosKitDisponiveis(co_kit);
                    CarregaMedicamentosKitIncluidos(co_kit);

                    CarregaItensKitProximos(RetornaItensASeremIncluidos());
                    CarregaMedicamentosKitProximos(RetornaMedicamentosASeremIncluidos());
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        /// <summary>
        /// Carrega os próximos itens a serem incluídos no kit
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaItensKitProximos(IList<KitItemPA> proximositens)
        {
            GridView_ProximosItens.DataSource = proximositens;
            GridView_ProximosItens.DataBind();
        }

        /// <summary>
        /// Carrega os itens incluídos no kit
        /// </summary>
        /// <param name="co_kit">código do kit</param>
        private void CarregaItensKitIncluidos(int co_kit)
        {
            GridView_ItensIncluidos.DataSource = Factory.GetInstance<IKitPA>().BuscarItemPA<KitItemPA>(co_kit);
            GridView_ItensIncluidos.DataBind();
        }

        /// <summary>
        /// Carrega os itens diponíveis para inserção no kit
        /// </summary>
        /// <param name="co_kit">código do kit</param>
        private void CarregaItensKitDisponiveis(int co_kit)
        {
            IKitPA iKit = Factory.GetInstance<IKitPA>();
            IList<ItemPA> itens = iKit.ListarTodos<ItemPA>().Where(p => p.Status != ItemPA.INATIVO).ToList<ItemPA>();

            if (co_kit != -1)
            {
                IList<KitItemPA> itenstemp = iKit.BuscarItemPA<KitItemPA>(co_kit);
                var cons1 = from i in itens where !itenstemp.Select(p => p.ItemPA.Codigo).Contains(i.Codigo) select i;
                itens = cons1.ToList();
            }

            IList<KitItemPA> itensfuturos = RetornaItensASeremIncluidos();
            var cons2 = from i in itens where !itensfuturos.Select(p => p.ItemPA.Codigo).Contains(i.Codigo) select i;
            itens = cons2.ToList();

            GridView_ItensDisponiveis.DataSource = itens.OrderBy(p => p.Nome).ToList();
            GridView_ItensDisponiveis.DataBind();
        }

        ///// <summary>
        ///// Carrega os kits cadastrados
        ///// </summary>
        //private void CarregaKits()
        //{
        //    GridView_Kits.DataSource = Factory.GetInstance<IKitPA>().ListarTodos<KitPA>().OrderBy(p => p.Nome).ToList();
        //    GridView_Kits.DataBind();
        //}

        /// <summary>
        /// Salva o kit com as propriedades indicadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            if (CustomValidator_ItensKit.IsValid)
            {
                try
                {
                    IKitPA iKit = Factory.GetInstance<IKitPA>();
                    KitPA kit = ViewState["co_kit"] != null ? Factory.GetInstance<IKitPA>().BuscarPorCodigo<KitPA>(int.Parse(ViewState["co_kit"].ToString())) : new KitPA();

                    if (RetornaMedicamentosASeremIncluidos().Where(p => p.MedicamentoPrincipal == true).FirstOrDefault() != null && !Factory.GetInstance<IKitPA>().ValidaCadastroKit(kit.Codigo, RetornaMedicamentosASeremIncluidos().Where(p => p.MedicamentoPrincipal == true).First().CodigoMedicamento))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um kit em que o Medicamento Principal é o: " + RetornaMedicamentosASeremIncluidos().Where(p => p.MedicamentoPrincipal == true).First().Medicamento.Nome + "');", true);
                    else
                    {
                        kit.Nome = TextBox_Nome.Text;
                        //kit.Status = RadioButton_Ativo.Checked ? 'A' : 'I';

                        if (ViewState["co_kit"] == null)
                            iKit.SalvarKit<KitPA, KitItemPA, KitMedicamentoPA>(kit, RetornaItensASeremIncluidos(), RetornaMedicamentosASeremIncluidos());
                        //else
                        //    iKit.SalvarKit<KitPA, KitItemPA, KitMedicamentoPA>(kit, RetornaItensASeremExcluidos(), RetornaItensASeremIncluidos(), RetornaMedicamentosASeremExcluidos(), RetornaMedicamentosASeremIncluidos());

                        iKit.Inserir(new LogUrgencia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 15, "id kit:" + kit.Codigo));
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Kit salvo com sucesso.');location='FormExibeKit.aspx';", true);

                        //OnClick_Cancelar(sender, e);
                    }
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_ItensKit.ErrorMessage + "');", true);
        }

        ///// <summary>
        ///// Cancela a ação de salvar o kit
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnClick_Cancelar(object sender, EventArgs e)
        //{
        //    ViewState.Remove("co_kit");
        //    Session.Remove("itensaseremincluidos");
        //    Session.Remove("medicamentosaseremincluidos");

        //    TextBox_Nome.Text = "";
        //    //RadioButton_Ativo.Checked = true;
        //    //RadioButton_Inativo.Checked = false;
        //    TabContainer_Kit.ActiveTabIndex = 0;

        //    CarregaKits();

        //    CarregaItensKitDisponiveis(-1);
        //    CarregaItensKitIncluidos(-1);
        //    CarregaItensKitProximos(RetornaItensASeremIncluidos());

        //    CarregaMedicamentosKitDisponiveis(-1);
        //    CarregaMedicamentosKitIncluidos(-1);
        //    CarregaMedicamentosKitProximos(RetornaMedicamentosASeremIncluidos());
        //}

        /// <summary>
        /// Retorna a lista de medicamentos a serem incluídos
        /// </summary>
        /// <returns></returns>
        private IList<KitMedicamentoPA> RetornaMedicamentosASeremIncluidos()
        {
            if (Session["medicamentosaseremincluidos"] != null)
            {
                IList<KitMedicamentoPA> medicamentos = (List<KitMedicamentoPA>)Session["medicamentosaseremincluidos"];
                IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();

                foreach (KitMedicamentoPA medicamento in medicamentos)
                    medicamento.Medicamento = iMedicamento.BuscarPorCodigo<Medicamento>(medicamento.CodigoMedicamento);

                return medicamentos;
            }

            return new List<KitMedicamentoPA>();
        }

        /// <summary>
        /// Carrega os próximos medicamentos a serem incluídos
        /// </summary>
        /// <param name="medicamentos"></param>
        private void CarregaMedicamentosKitProximos(IList<KitMedicamentoPA> medicamentos)
        {
            GridView_ProximosMedicamentos.DataSource = medicamentos;
            GridView_ProximosMedicamentos.DataBind();
        }

        /// <summary>
        /// Carrega os medicamento já incluídos no kit
        /// </summary>
        /// <param name="código do kit"></param>
        private void CarregaMedicamentosKitIncluidos(int co_kit)
        {
            IList<KitMedicamentoPA> medicamentos = Factory.GetInstance<IKitPA>().BuscarMedicamentoPA<KitMedicamentoPA>(co_kit);
            IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();

            foreach (KitMedicamentoPA medicamento in medicamentos)
                medicamento.Medicamento = iMedicamento.BuscarPorCodigo<Medicamento>(medicamento.CodigoMedicamento);

            GridView_MedicamentosIncluidos.DataSource = medicamentos;
            GridView_MedicamentosIncluidos.DataBind();
        }

        /// <summary>
        /// Carrega os medicamentos diponíveis para inclusão no kit
        /// </summary>
        /// <param name="código do kit"></param>
        private void CarregaMedicamentosKitDisponiveis(int co_kit)
        {
            IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
            //ElencoMedicamento em = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ElencoMedicamento>(15);
            IList<Medicamento> medicamentos = iMedicamento.ListarTodos<Medicamento>("Nome", true);

            if (co_kit != -1)
            {
                IList<KitMedicamentoPA> kitmedicamentos = Factory.GetInstance<IKitPA>().BuscarMedicamentoPA<KitMedicamentoPA>(co_kit);
                IList<Medicamento> medicamentostemp = new List<Medicamento>();

                foreach (KitMedicamentoPA kitmedicamento in kitmedicamentos)
                    medicamentostemp.Add(iMedicamento.BuscarPorCodigo<Medicamento>(kitmedicamento.CodigoMedicamento));

                var cons1 = from med in medicamentos where !medicamentostemp.Select(p => p.Codigo).Contains(med.Codigo) select med;
                medicamentos = cons1.ToList();
            }

            IList<KitMedicamentoPA> itensfuturos = this.RetornaMedicamentosASeremIncluidos();

            var cons2 =
                from med in medicamentos
                where
                    !itensfuturos.Select(p => p.Medicamento.Codigo).ToList().Contains(med.Codigo)
                select med;
            medicamentos = cons2.ToList();

            GridView_MedicamentosDisponiveis.DataSource = medicamentos; //lm.OrderBy(p => p.Nome).ToList();
            GridView_MedicamentosDisponiveis.DataBind();
        }

        ///// <summary>
        ///// Seleciona o kit para edição
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnSelectedIndexChanged_SelecionarKit(object sender, EventArgs e)
        //{
        //    OnClick_Cancelar(sender, e);

        //    KitPA kit = Factory.GetInstance<IKitPA>().BuscarPorCodigo<KitPA>(int.Parse(GridView_Kits.DataKeys[GridView_Kits.SelectedIndex]["Codigo"].ToString()));
        //    ViewState["co_kit"] = kit.Codigo;
        //    TextBox_Nome.Text = kit.Nome;

        //    //if (k.Status == 'I')
        //    //{
        //    //    RadioButton_Ativo.Checked = false;
        //    //    RadioButton_Inativo.Checked = true;
        //    //}

        //    CarregaItensKitDisponiveis(kit.Codigo);
        //    CarregaItensKitIncluidos(kit.Codigo);

        //    CarregaMedicamentosKitDisponiveis(kit.Codigo);
        //    CarregaMedicamentosKitIncluidos(kit.Codigo);
        //}

        /// <summary>
        /// Valida se os itens do kit estão de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_ValidaItensKit(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;

            if (ViewState["co_kit"] != null) //Edição
            {
                if (RetornaMedicamentosASeremExcluidos().Count() == Factory.GetInstance<IKitPA>().BuscarMedicamentoPA<KitMedicamentoPA>(int.Parse(ViewState["co_kit"].ToString())).Count()
                    && RetornaMedicamentosASeremIncluidos().Count() == 0)
                {
                    CustomValidator_ItensKit.ErrorMessage = "Para salvar este kit é necessário incluir pelo menos um Medicamento";
                    e.IsValid = false; return;
                }
                else
                {
                    if (RetornaMedicamentosASeremExcluidos().Where(p => p.MedicamentoPrincipal).FirstOrDefault() != null &&
                        RetornaMedicamentosASeremIncluidos().Where(p => p.MedicamentoPrincipal).FirstOrDefault() == null)
                    {
                        CustomValidator_ItensKit.ErrorMessage = "Para salvar este kit é necessário incluir pelo menos um Medicamento Principal.";
                        e.IsValid = false; return;
                    }
                    else
                    {
                        if (RetornaMedicamentosASeremExcluidos().Where(p => p.MedicamentoPrincipal).FirstOrDefault() == null &&
                        RetornaMedicamentosASeremIncluidos().Where(p => p.MedicamentoPrincipal).FirstOrDefault() != null)
                        {
                            CustomValidator_ItensKit.ErrorMessage = "Este kit já um possui um Medicamento Principal.";
                            e.IsValid = false; return;
                        }
                    }
                }

                if (RetornaItensASeremExcluidos().Count() == Factory.GetInstance<IKitPA>().BuscarItemPA<KitItemPA>(int.Parse(ViewState["co_kit"].ToString())).Count()
                    && RetornaItensASeremIncluidos().Count() == 0)
                {
                    CustomValidator_ItensKit.ErrorMessage = "Para salvar este kit é necessário incluir pelo menos um Item.";
                    e.IsValid = false; return;
                }
            }
            else //Cadastro
            {
                if (RetornaMedicamentosASeremIncluidos().Count() == 0)
                {
                    CustomValidator_ItensKit.ErrorMessage = "Para salvar este kit é necessário incluir pelo menos um Medicamento.";
                    e.IsValid = false; return;
                }
                else
                {
                    if (RetornaMedicamentosASeremIncluidos().Where(p => p.MedicamentoPrincipal).FirstOrDefault() == null)
                    {
                        CustomValidator_ItensKit.ErrorMessage = "Para salvar este kit é necessário incluir pelo menos um Medicamento Principal.";
                        e.IsValid = false; return;
                    }
                }

                if (RetornaItensASeremIncluidos().Count() == 0)
                {
                    CustomValidator_ItensKit.ErrorMessage = "Para salvar este kit é necessário incluir pelo menos um Item.";
                    e.IsValid = false; return;
                }
            }
        }

        /// <summary>
        /// Retorna a lista de itens do kit a serem excluídos
        /// </summary>
        /// <returns></returns>
        private IList<KitItemPA> RetornaItensASeremExcluidos()
        {
            IList<KitItemPA> kititens = new List<KitItemPA>();
            IKitPA iKit = Factory.GetInstance<IKitPA>();

            foreach (GridViewRow r in GridView_ItensIncluidos.Rows)
            {
                if (((CheckBox)r.FindControl("CheckBox_Retirar")).Checked)
                    kititens.Add(iKit.BuscarItemPA<KitItemPA>(int.Parse(ViewState["co_kit"].ToString())).Where(p => p.ItemPA.Codigo == int.Parse(GridView_ItensIncluidos.DataKeys[r.DataItemIndex]["CodigoItem"].ToString())).First());
            }

            return kititens;
        }

        /// <summary>
        /// Retorna a lista medicamentos do kit a serem excluídos
        /// </summary>
        /// <returns></returns>
        private IList<KitMedicamentoPA> RetornaMedicamentosASeremExcluidos()
        {
            IList<KitMedicamentoPA> kitmedicamentos = new List<KitMedicamentoPA>();
            IKitPA iKit = Factory.GetInstance<IKitPA>();

            foreach (GridViewRow r in GridView_MedicamentosIncluidos.Rows)
            {
                if (((CheckBox)r.FindControl("CheckBox_Retirar")).Checked)
                    kitmedicamentos.Add(iKit.BuscarMedicamentoPA<KitMedicamentoPA>(int.Parse(ViewState["co_kit"].ToString())).Where(p => p.CodigoMedicamento == int.Parse(GridView_MedicamentosIncluidos.DataKeys[r.DataItemIndex]["CodigoMedicamento"].ToString())).First());
            }

            return kitmedicamentos;
        }

        ///// <summary>
        ///// Paginação do gridview de kits existentes
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnPageIndexChanging_PaginacaoKits(object sender, GridViewPageEventArgs e)
        //{
        //    CarregaKits();
        //    GridView_Kits.PageIndex = e.NewPageIndex;
        //    GridView_Kits.DataBind();
        //}

        /// <summary>
        /// Cancela a inserção do item no kit do PA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarInsercaoItem(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_ItensDisponiveis.EditIndex = -1;
            CarregaItensKitDisponiveis(ViewState["co_kit"] != null ? int.Parse(ViewState["co_kit"].ToString()) : -1);
        }

        /// <summary>
        /// Abre o campo de quantidade para edição do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_InserirItem(object sender, GridViewEditEventArgs e)
        {
            GridView_ItensDisponiveis.EditIndex = e.NewEditIndex;
            CarregaItensKitDisponiveis(ViewState["co_kit"] != null ? int.Parse(ViewState["co_kit"].ToString()) : -1);
        }

        /// <summary>
        /// Insere o item no kit do PA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_SalvarItem(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow r = GridView_ItensDisponiveis.Rows[e.RowIndex];
            int codigoitem = int.Parse(GridView_ItensDisponiveis.DataKeys[e.RowIndex]["Codigo"].ToString());

            ItemPA itempa = Factory.GetInstance<IItemPA>().BuscarPorCodigo<ItemPA>(codigoitem);

            KitItemPA kiiitem = new KitItemPA();
            kiiitem.ItemPA = itempa;
            kiiitem.Quantidade = int.Parse(((TextBox)r.FindControl("TextBox_Quantidade")).Text);

            IList<KitItemPA> kititens = RetornaItensASeremIncluidos();
            kititens.Add(kiiitem);

            Session["itensaseremincluidos"] = kititens;

            GridView_ItensDisponiveis.EditIndex = -1;
            CarregaItensKitDisponiveis(ViewState["co_kit"] != null ? int.Parse(ViewState["co_kit"].ToString()) : -1);
            CarregaItensKitProximos(RetornaItensASeremIncluidos());
        }

        /// <summary>
        /// Exclui o item que faria parte do kit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_DeletarItemKitProximo(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex == 0 ? GridView_ProximosItens.PageIndex * GridView_ProximosItens.PageSize : (GridView_ProximosItens.PageIndex * GridView_ProximosItens.PageSize) + e.RowIndex;
            IList<KitItemPA> kititens = RetornaItensASeremIncluidos();
            kititens.RemoveAt(index);
            Session["itensaseremincluidos"] = kititens;
            CarregaItensKitProximos(RetornaItensASeremIncluidos());
            CarregaItensKitDisponiveis(ViewState["co_kit"] != null ? int.Parse(ViewState["co_kit"].ToString()) : -1);
        }

        /// <summary>
        /// Paginção do gridview de itens disponíveis para inserção
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoItensDisponiveis(object sender, GridViewPageEventArgs e)
        {
            CarregaItensKitDisponiveis(ViewState["co_kit"] != null ? int.Parse(ViewState["co_kit"].ToString()) : -1);
            GridView_ItensDisponiveis.PageIndex = e.NewPageIndex;
            GridView_ItensDisponiveis.DataBind();
        }

        /// <summary>
        /// Paginação do gridview dos próximos itens a serem incluídos no kit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoProximosItens(object sender, GridViewPageEventArgs e)
        {
            CarregaItensKitProximos(RetornaItensASeremIncluidos());
            GridView_ProximosItens.PageIndex = e.NewPageIndex;
            GridView_ProximosItens.DataBind();
        }

        /// <summary>
        /// Formata o gridview de próximos itens com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewProximosItens(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)((DataControlFieldCell)e.Row.Controls[2]).Controls[0];
                lb.OnClientClick = "javascript:return confirm('Tem certeza que deseja retirar este item da lista de próximas inclusões?');";
            }
        }

        /// <summary>
        /// Retorna a lista de itens a serem incluídos para este kit
        /// </summary>
        /// <returns></returns>
        private IList<KitItemPA> RetornaItensASeremIncluidos()
        {
            if (Session["itensaseremincluidos"] != null)
            {
                IList<KitItemPA> kititens = (List<KitItemPA>)Session["itensaseremincluidos"];
                IItemPA iItem = Factory.GetInstance<IItemPA>();

                foreach (KitItemPA item in kititens)
                    item.ItemPA = iItem.BuscarPorCodigo<ItemPA>(item.ItemPA.Codigo);

                return kititens;
            }

            return new List<KitItemPA>();
        }

        /// <summary>
        /// Formata o gridview de próximos medicamentos com o padrão especificado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewProximosMedicamentos(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = (LinkButton)((DataControlFieldCell)e.Row.Controls[3]).Controls[0];
                lb.OnClientClick = "javascript:return confirm('Tem certeza que deseja retirar este medicamento da lista de próximas inclusões?');";
            }
        }

        /// <summary>
        /// Paginação para o gridview de próximos medicamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoProximosMedicamentos(object sender, GridViewPageEventArgs e)
        {
            CarregaMedicamentosKitProximos(RetornaMedicamentosASeremIncluidos());
            GridView_ProximosMedicamentos.PageIndex = e.NewPageIndex;
            GridView_ProximosMedicamentos.DataBind();
        }

        /// <summary>
        /// Retira um medicamento das próximas inclusões
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_DeletarMedicamentoKitProximo(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex == 0 ? GridView_ProximosMedicamentos.PageIndex * GridView_ProximosMedicamentos.PageSize : (GridView_ProximosMedicamentos.PageIndex * GridView_ProximosMedicamentos.PageSize) + e.RowIndex;
            IList<KitMedicamentoPA> medicamentos = RetornaMedicamentosASeremIncluidos();
            medicamentos.RemoveAt(index);
            Session["medicamentosaseremincluidos"] = medicamentos;
            CarregaMedicamentosKitProximos(RetornaMedicamentosASeremIncluidos());
            CarregaMedicamentosKitDisponiveis(ViewState["co_kit"] != null ? int.Parse(ViewState["co_kit"].ToString()) : -1);
        }

        /// <summary>
        /// Paginação do gridview dos medicamentos disponíveis para inclusão
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoMedicamentosDisponiveis(object sender, GridViewPageEventArgs e)
        {
            CarregaMedicamentosKitDisponiveis(ViewState["co_kit"] != null ? int.Parse(ViewState["co_kit"].ToString()) : -1);
            GridView_MedicamentosDisponiveis.PageIndex = e.NewPageIndex;
            GridView_MedicamentosDisponiveis.DataBind();
        }

        /// <summary>
        /// Cancela a chamada para inclusão de um medicamento no kit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarInsercaoMedicamento(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_MedicamentosDisponiveis.EditIndex = -1;
            CarregaMedicamentosKitDisponiveis(ViewState["co_kit"] != null ? int.Parse(ViewState["co_kit"].ToString()) : -1);
        }

        /// <summary>
        /// Formula o gridview para inserção dos dados do medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_InserirMedicamento(object sender, GridViewEditEventArgs e)
        {
            GridView_MedicamentosDisponiveis.EditIndex = e.NewEditIndex;
            CarregaMedicamentosKitDisponiveis(ViewState["co_kit"] != null ? int.Parse(ViewState["co_kit"].ToString()) : -1);
        }

        /// <summary>
        /// Insere o medicamento na lista de próximas inclusões
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_SalvarMedicamento(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gridrow = GridView_MedicamentosDisponiveis.Rows[e.RowIndex];
            int codigomedicamento = int.Parse(GridView_MedicamentosDisponiveis.DataKeys[e.RowIndex]["Codigo"].ToString());

            KitMedicamentoPA kitmedicamento = new KitMedicamentoPA();
            kitmedicamento.CodigoMedicamento = codigomedicamento;
            kitmedicamento.Quantidade = int.Parse(((TextBox)gridrow.FindControl("TextBox_Quantidade")).Text);
            kitmedicamento.MedicamentoPrincipal = ((CheckBox)gridrow.FindControl("CheckBox_MedicamentoPrincipal")).Checked;

            IList<KitMedicamentoPA> kitmedicamentos = RetornaMedicamentosASeremIncluidos();

            if (kitmedicamentos.Where(p => p.MedicamentoPrincipal == true).FirstOrDefault() != null && kitmedicamento.MedicamentoPrincipal)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um item na lista de medicamentos a serem incluídos escolhido como Medicamento Principal.');", true);
            else
            {
                kitmedicamentos.Add(kitmedicamento);

                Session["medicamentosaseremincluidos"] = kitmedicamentos;

                GridView_MedicamentosDisponiveis.EditIndex = -1;
                CarregaMedicamentosKitDisponiveis(ViewState["co_kit"] != null ? int.Parse(ViewState["co_kit"].ToString()) : -1);
                CarregaMedicamentosKitProximos(RetornaMedicamentosASeremIncluidos());
            }
        }

        /// <summary>
        /// Formata o gridview de itens incluídos de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewItensIncluidos(object sender, GridViewRowEventArgs e)
        {
        }

        /// <summary>
        /// Formata o gridview de medicamentos incluídos de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewMedicamentosIncluidos(object sender, GridViewRowEventArgs e)
        {
        }
    }
}
