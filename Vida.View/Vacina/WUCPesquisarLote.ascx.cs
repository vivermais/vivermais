using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Vacina
{
    public partial class WUCPesquisarLote : System.Web.UI.UserControl
    {
        private IList<LoteVacina> lotespesquisados;
        public IList<LoteVacina> LotesPesquisados
        {
            get
            {
                if (Session[string.Format("LotesVacinaPesquisados_{0}", this.UniqueID)] != null)
                    return (IList<LoteVacina>)Session[string.Format("LotesVacinaPesquisados_{0}", this.UniqueID)];

                return new List<LoteVacina>(); 
            }
            set
            {
                Session[string.Format("LotesVacinaPesquisados_{0}",this.UniqueID)]
                    = value; 
            }
        }

        public GridView WUC_GridViewLote
        {
            get { return this.GridView_Lote; }
        }

        public Panel WUC_PanelLotesPesquisados
        {
            get { return this.Panel_LotesPesquisados; }
        }

        public DropDownList WUC_DropDownListVacina
        {
            get { return this.DropDownList_Vacina; }
        }

        public DropDownList WUC_DropDownListFabricante
        {
            get { return this.DropDownList_Fabricante; }
        }

        public CustomValidator WUC_CustomPesquisarLote
        {
            get { return this.CustomValidator_PesquisarLote; }
        }

        public TextBox WUC_TextBoxAplicacoes
        {
            get { return this.TextBox_Aplicacoes; }
        }

        public TextBox WUC_TextBoxLote
        {
            get { return this.TextBox_Lote; }
        }

        public TextBox WUC_TextBoxValidade
        {
            get { return this.TextBox_Validade; }
        }

        public LinkButton WUC_LnkPesquisar
        {
            get { return this.LnkPesquisar; }
        }

        public LinkButton WUC_LnkListarTodos
        {
            get { return this.LnkListarTodos; }
        }

        public int WUC_FabricanteSelecionadoPesquisa
        {
            get { return int.Parse(this.DropDownList_Fabricante.SelectedValue); }
        }

        public int WUC_VacinaSelecionadaPesquisa
        {
            get { return int.Parse(this.DropDownList_Vacina.SelectedValue); }
        }

        public int WUC_AplicacoesPesquisa
        {
            get
            {
                return string.IsNullOrEmpty(this.TextBox_Aplicacoes.Text) ? -1 :
                    int.Parse(this.TextBox_Aplicacoes.Text);
            }
        }

        public string WUC_LotePesquisa
        {
            get { return this.TextBox_Lote.Text; }
        }

        public DateTime WUC_ValidadePesquisa
        {
            get
            {
                return string.IsNullOrEmpty(this.TextBox_Validade.Text) ? DateTime.MinValue
                    : DateTime.Parse(this.TextBox_Validade.Text);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList_Vacina.DataSource = Factory.GetInstance<IVacina>().ListarTodos<ViverMais.Model.Vacina>().OrderBy(p => p.Nome).ToList();
                DropDownList_Vacina.DataBind();
                DropDownList_Vacina.Items.Insert(0, new ListItem("Selecione...", "-1"));

                DropDownList_Fabricante.DataSource = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<FabricanteVacina>().OrderBy(p => p.Nome).ToList();
                DropDownList_Fabricante.DataBind();
                DropDownList_Fabricante.Items.Insert(0, new ListItem("Selecione...", "-1"));
            }
        }

        protected void OnClick_Pesquisar(object sender, EventArgs e)
        {
            if (!this.CustomValidator_PesquisarLote.IsValid)
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + this.CustomValidator_PesquisarLote.ErrorMessage + "');", true);
        }

        protected void OnServerValidate_Pesquisa(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = false;

            if (DropDownList_Fabricante.SelectedValue != "-1" || DropDownList_Vacina.SelectedValue != "-1"
                || !string.IsNullOrEmpty(TextBox_Lote.Text) || !string.IsNullOrEmpty(TextBox_Validade.Text) || !string.IsNullOrEmpty(TextBox_Aplicacoes.Text))
                e.IsValid = true;
            else
                CustomValidator_PesquisarLote.ErrorMessage = "Informe pelo menos um dos seguintes campos para pesquisa: \\n\\n(1) Imunobiológico, \\n(2) Fabricante, \\n(3) Nº Aplicações, \\n(4) Lote, \\n(5) Data de Validade.";
        }

        /// <summary>
        /// Paginação do gridview de lotes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            GridView_Lote.DataSource = this.LotesPesquisados;
            GridView_Lote.DataBind();

            GridView_Lote.PageIndex = e.NewPageIndex;
            GridView_Lote.DataBind();
        }
    }
}