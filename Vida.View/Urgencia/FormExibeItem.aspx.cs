using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;

namespace ViverMais.View.Urgencia
{
    public partial class FormExibeItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_ITEM_PA", Modulo.URGENCIA))
                {
                    this.LinkButton_NovoRegistro.Visible = false;
                    this.gridItens.Columns.RemoveAt(3);
                }

                this.ExibeItens();
            }
        }

        /// <summary>
        /// Atualiza a lista de itens do pronto atendimento
        /// </summary>
        void ExibeItens()
        {
            IList<ViverMais.Model.ItemPA> itens = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.ItemPA>("CodigoSIGTAP", true);
            gridItens.DataSource = itens;
            gridItens.DataBind();
            Session["itens"] = itens;
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            gridItens.DataSource = (IList<ViverMais.Model.ItemPA>)Session["itens"];
            gridItens.DataBind();
            gridItens.PageIndex = e.NewPageIndex;
            gridItens.DataBind();
        }
    }
}
