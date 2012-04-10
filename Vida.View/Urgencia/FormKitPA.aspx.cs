using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.View.Urgencia
{
    public partial class FormKitPA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButton lnkPesquisarItem = this.WUCItemPA.WUC_LinkButtonPesquisarPorNome;
            lnkPesquisarItem.Click += new EventHandler(OnClick_PesquisarItem);
            this.InserirTrigger(lnkPesquisarItem.UniqueID, "Click", this.UpdatePanel_PesquisaItem);

            LinkButton lnkPesquisarMedicamento = this.WUC_Medicamento.WUC_LinkButtonPesquisarPorNome;
            lnkPesquisarMedicamento.Click +=new EventHandler(OnClick_PesquisarMedicamento);
            this.InserirTrigger(lnkPesquisarMedicamento.UniqueID, "Click", this.UpdatePanel_PesquisaMedicamento);

            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_KIT_PA", Modulo.URGENCIA))
                {
                    //this.WUC_Medicamento.WUC_Label_ChamadaPesquisa.Text = "Medicamento/Prescrição";
                    //this.WUC_Medicamento.WUC_RequiredFieldValidatorNomeMedicamento.ErrorMessage = "Informe o nome do(a) medicamento/prescrição.";
                    //this.WUC_Medicamento.WUC_RegularExpressionValidatorNomeMedicamento.ErrorMessage = "Informe pelo menos as três primeiras letras do(a) medicamento/prescrição.";

                    this.ItensInclusos = new List<KitItemPA>();
                    this.MedicamentosInclusos = new List<KitMedicamentoPA>();

                    if (Request["co_kit"] != null)
                    {
                        IKitPA iKit = Factory.GetInstance<IKitPA>();
                        ViewState["co_kit"] = Request["co_kit"].ToString();
                        KitPA kit = iKit.BuscarPorCodigo<KitPA>(int.Parse(ViewState["co_kit"].ToString()));

                        TextBox_Nome.Text = kit.Nome;
                        this.ItensInclusos = iKit.BuscarItemPA<KitItemPA>(kit.Codigo);
                        this.MedicamentosInclusos = iKit.BuscarMedicamentoPA<KitMedicamentoPA>(kit.Codigo);
                    }

                    this.CarregaItensInclusos();
                    this.CarregaMedicamentosInclusos();
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        private void InserirTrigger(string idcontrole, string nomeevento, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = idcontrole;
            trigger.EventName = nomeevento;
            updatepanel.Triggers.Add(trigger);
            MasterMain mm = (MasterMain)Master.Master;
            ((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(this.FindControl(idcontrole));
        }

        #region ITEM
        private IList<KitItemPA> ItensInclusos
        {
            set
            {
                Session["itensaseremincluidos"] = value;
            }

            get
            {
                return
                    Session["itensaseremincluidos"] != null ? (IList<KitItemPA>)Session["itensaseremincluidos"] : new List<KitItemPA>();
            }
        }

        protected void OnClick_PesquisarItem(object sender, EventArgs e)
        {
            this.WUCItemPA.Itens = (from i in
                                        this.WUCItemPA.Itens.Where(p => p.Status == ItemPA.ATIVO).ToList()
                                    where !this.ItensInclusos.Select(p => p.ItemPA.Codigo).Contains(i.Codigo)
                                    select i).ToList();
            this.Panel_ResultadoPesquisaItem.Visible = true;
            this.GridView_ItensDisponiveis.DataSource = this.WUCItemPA.Itens;
            this.GridView_ItensDisponiveis.DataBind();
            this.UpdatePanel_PesquisaItem.Update();
        }

        private void CarregaItensDisponiveis()
        {
            GridView_ItensDisponiveis.DataSource = this.WUCItemPA.Itens;
            GridView_ItensDisponiveis.DataBind();
        }

        private void CarregaItensInclusos()
        {
            GridViewItens.DataSource = this.ItensInclusos;
            GridViewItens.DataBind();
        }
        /// <summary>
        /// Paginção do gridview de itens disponíveis para inserção
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoItensDisponiveis(object sender, GridViewPageEventArgs e)
        {
            this.CarregaItensDisponiveis();
            GridView_ItensDisponiveis.PageIndex = e.NewPageIndex;
            GridView_ItensDisponiveis.DataBind();
        }

        /// <summary>
        /// Cancela a inserção do item no kit do PA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarInsercaoItem(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_ItensDisponiveis.EditIndex = -1;
            this.CarregaItensDisponiveis();
        }

        /// <summary>
        /// Abre o campo de quantidade para edição do item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_InserirItem(object sender, GridViewEditEventArgs e)
        {
            GridView_ItensDisponiveis.EditIndex = e.NewEditIndex;
            this.CarregaItensDisponiveis();
        }

        /// <summary>
        /// Insere o item no kit do PA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_InserirItem(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow r = GridView_ItensDisponiveis.Rows[e.RowIndex];
            int codigoitem = int.Parse(GridView_ItensDisponiveis.DataKeys[e.RowIndex]["Codigo"].ToString());
            IList<ItemPA> itensdisponiveis = this.WUCItemPA.Itens;
            ItemPA itempa = itensdisponiveis.Where(p => p.Codigo == codigoitem).First();

            KitItemPA kiiitem = new KitItemPA();
            kiiitem.KitPA = new KitPA();
            kiiitem.ItemPA = itempa;
            kiiitem.Quantidade = int.Parse(((TextBox)r.FindControl("TextBox_Quantidade")).Text);

            IList<KitItemPA> kititens = this.ItensInclusos;
            kititens.Add(kiiitem);
            this.ItensInclusos = kititens;
            
            itensdisponiveis.Remove(itempa);
            this.WUCItemPA.Itens = itensdisponiveis;

            GridView_ItensDisponiveis.EditIndex = -1;
            this.CarregaItensDisponiveis();
            this.CarregaItensInclusos();
        }

        protected void OnRowDeleting_Item(object sender, GridViewDeleteEventArgs e)
        {
            int co_item = int.Parse(this.GridViewItens.DataKeys[e.RowIndex]["CodigoItem"].ToString());
            IList<KitItemPA> itens = this.ItensInclusos;
            itens.Remove(itens.Where(p => p.CodigoItem == co_item).First());
            this.ItensInclusos = itens;

            this.CarregaItensInclusos();
        }
        #endregion

        #region MEDICAMENTO
        private void CarregaMedicamentosDisponiveis()
        {
            this.GridView_MedicamentosDisponiveis.DataSource = this.WUC_Medicamento.Medicamentos;
            this.GridView_MedicamentosDisponiveis.DataBind();
        }

        private void CarregaMedicamentosInclusos()
        {
            GridView_Medicamentos.DataSource = this.MedicamentosInclusos;
            GridView_Medicamentos.DataBind();
        }

        IList<KitMedicamentoPA> MedicamentosInclusos
        {
            set
            {
                Session["medicamentosaseremincluidos"] = value;
            }

            get
            {
                return Session["medicamentosaseremincluidos"] != null ?
                    (IList<KitMedicamentoPA>)Session["medicamentosaseremincluidos"] : new List<KitMedicamentoPA>();
            }
        }

        protected void OnClick_PesquisarMedicamento(object sender, EventArgs e)
        {
            this.WUC_Medicamento.Medicamentos = (from m in this.WUC_Medicamento.Medicamentos.Where(p=>p.EMedicamento)
                                                 where !this.MedicamentosInclusos.Select(p => p.CodigoMedicamento).ToList().Contains(m.Codigo)
                                                 select m).ToList();
            this.Panel_ResultadoMedicamento.Visible = true;
            this.CarregaMedicamentosDisponiveis();
            this.UpdatePanel_PesquisaMedicamento.Update();
        }

        /// <summary>
        /// Paginação do gridview dos medicamentos disponíveis para inclusão
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoMedicamentosDisponiveis(object sender, GridViewPageEventArgs e)
        {
            this.CarregaMedicamentosDisponiveis();
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
            this.CarregaMedicamentosDisponiveis();
        }

        /// <summary>
        /// Formula o gridview para inserção dos dados do medicamento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_InserirMedicamento(object sender, GridViewEditEventArgs e)
        {
            GridView_MedicamentosDisponiveis.EditIndex = e.NewEditIndex;
            this.CarregaMedicamentosDisponiveis();
        }

        /// <summary>
        /// Insere o medicamento na lista de próximas inclusões
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_InserirMedicamento(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gridrow = this.GridView_MedicamentosDisponiveis.Rows[e.RowIndex];
            int codigomedicamento = int.Parse(GridView_MedicamentosDisponiveis.DataKeys[e.RowIndex]["Codigo"].ToString());
            IList<Medicamento> medicamentos = this.WUC_Medicamento.Medicamentos;
            Medicamento medicamento = medicamentos.Where(p => p.Codigo == codigomedicamento).First();

            KitMedicamentoPA kitmedicamento = new KitMedicamentoPA();
            kitmedicamento.KitPA = new KitPA();
            kitmedicamento.CodigoMedicamento = codigomedicamento;
            kitmedicamento.Medicamento = medicamento;
            kitmedicamento.Quantidade = int.Parse(((TextBox)gridrow.FindControl("TextBox_Quantidade")).Text);
            kitmedicamento.MedicamentoPrincipal = ((CheckBox)gridrow.FindControl("CheckBox_MedicamentoPrincipal")).Checked;

            IList<KitMedicamentoPA> kitmedicamentos = this.MedicamentosInclusos;

            if (kitmedicamentos.Where(p => p.MedicamentoPrincipal == true).FirstOrDefault() != null && kitmedicamento.MedicamentoPrincipal)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um item na lista de medicamentos escolhido como medicamento principal.');", true);
            else
            {
                kitmedicamentos.Add(kitmedicamento);
                this.MedicamentosInclusos = kitmedicamentos;
                medicamentos.Remove(medicamento);
                this.WUC_Medicamento.Medicamentos = medicamentos;

                this.GridView_MedicamentosDisponiveis.EditIndex = -1;
                this.CarregaMedicamentosInclusos();
                this.CarregaMedicamentosDisponiveis();
            }
        }

        protected void OnRowDataBound_Medicamentos(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (this.MedicamentosInclusos.Where(p => p.MedicamentoPrincipal).FirstOrDefault() != null)
                    this.GridView_MedicamentosDisponiveis.Columns[1].Visible = false; //Só existe um medicamento principal para cada kit criado
                else
                    this.GridView_MedicamentosDisponiveis.Columns[1].Visible = true;
            }
        }

        protected void OnRowDeleting_Medicamento(object sender, GridViewDeleteEventArgs e)
        {
            int co_medicamento = int.Parse(GridView_Medicamentos.DataKeys[e.RowIndex]["CodigoMedicamento"].ToString());
            IList<KitMedicamentoPA> medicamentos = this.MedicamentosInclusos;
            KitMedicamentoPA medicamento = this.MedicamentosInclusos.Where(p=>p.CodigoMedicamento == co_medicamento).First();

            medicamentos.Remove(medicamento);
            this.MedicamentosInclusos = medicamentos;
            this.CarregaMedicamentosInclusos();
        }
        #endregion

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
                    KitPA kit = ViewState["co_kit"] != null ? iKit.BuscarPorCodigo<KitPA>(int.Parse(ViewState["co_kit"].ToString())) : new KitPA();
                    KitMedicamentoPA medicamentoPrincipal = MedicamentosInclusos.Where(p=>p.MedicamentoPrincipal).First();

                    if (!iKit.ValidaCadastroKit(kit.Codigo, medicamentoPrincipal.CodigoMedicamento))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, já existe um kit em que o medicamento principal é o: " + medicamentoPrincipal.Medicamento.Nome + "! Por favor, informe outro medicamento como principal.');", true);
                    else
                    {
                        kit.Nome = TextBox_Nome.Text;
                        iKit.SalvarKit<KitPA, KitItemPA, KitMedicamentoPA>(kit, this.ItensInclusos, this.MedicamentosInclusos);
                        iKit.Inserir(new LogUrgencia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 15, "id kit:" + kit.Codigo));
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Kit salvo com sucesso.');location='FormExibeKit.aspx';", true);
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

        /// <summary>
        /// Valida se os itens do kit estão de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_ValidaItensKit(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;
            IList<KitMedicamentoPA> medicamentos = this.MedicamentosInclusos;

            if (medicamentos.Count() == 0)
            {
                CustomValidator_ItensKit.ErrorMessage = "Para salvar este kit é necessário incluir pelo menos um medicamento!";
                e.IsValid = false;
                return;
            }
            else
            {
                if (medicamentos.Where(p => p.MedicamentoPrincipal).FirstOrDefault() == null)
                {
                    CustomValidator_ItensKit.ErrorMessage = "Para salvar este kit é necessário escolher um medicamento principal!";
                    e.IsValid = false;
                    return;
                }
            }

            if (this.ItensInclusos.Count() == 0)
            {
                CustomValidator_ItensKit.ErrorMessage = "Para salvar este kit é necessário incluir pelo menos um item!";
                e.IsValid = false;
                return;
            }
        }
    }
}
