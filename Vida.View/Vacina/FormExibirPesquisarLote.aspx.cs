using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Vacina
{
    public partial class FormExibirPesquisarLote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.WUCPesquisarLote.WUC_LnkListarTodos.Click += new EventHandler(OnClick_ListarTodos);
            this.WUCPesquisarLote.WUC_LnkPesquisar.Click += new EventHandler(OnClick_Pesquisar);

            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_LOTE_VACINA", Modulo.VACINA))
                {
                    this.WUCPesquisarLote.WUC_GridViewLote.Columns.RemoveAt(3);
                    BoundField bound = new BoundField();
                    bound.HeaderText = "Lote";
                    bound.DataField = "Identificacao";
                    bound.ItemStyle.Width = Unit.Pixel(100);
                    bound.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    this.WUCPesquisarLote.WUC_GridViewLote.Columns.Insert(3, bound);
                    this.Lnk_NovoLote.Visible = false;
                }

                //this.OnClick_ListarTodos(sender, e);
            }
        }

        private void OnClick_ListarTodos(object sender, EventArgs e)
        {
            IList<LoteVacina> lotes = Factory.GetInstance<ILoteVacina>().ListarTodos<LoteVacina>().OrderBy(p => p.ItemVacina.Vacina.Nome).ToList();
            this.WUCPesquisarLote.LotesPesquisados = lotes;
            this.WUCPesquisarLote.WUC_GridViewLote.DataSource = lotes;
            this.WUCPesquisarLote.WUC_GridViewLote.DataBind();
            this.WUCPesquisarLote.WUC_PanelLotesPesquisados.Visible = true;
        }

        private void OnClick_Pesquisar(object sender, EventArgs e)
        {
            if (this.WUCPesquisarLote.WUC_CustomPesquisarLote.IsValid)
            {
                IList<LoteVacina> lotespesquisados = Factory.GetInstance<ILoteVacina>().BuscarLote<LoteVacina>(this.WUCPesquisarLote.WUC_LotePesquisa, this.WUCPesquisarLote.WUC_ValidadePesquisa,
                    this.WUCPesquisarLote.WUC_VacinaSelecionadaPesquisa, this.WUCPesquisarLote.WUC_FabricanteSelecionadoPesquisa, this.WUCPesquisarLote.WUC_AplicacoesPesquisa);
                this.WUCPesquisarLote.LotesPesquisados = lotespesquisados;
                this.WUCPesquisarLote.WUC_GridViewLote.DataSource = lotespesquisados;
                this.WUCPesquisarLote.WUC_GridViewLote.DataBind();
                this.WUCPesquisarLote.WUC_PanelLotesPesquisados.Visible = true;
            }
        }
    }
}
