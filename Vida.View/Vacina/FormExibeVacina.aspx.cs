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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Vacina
{
    public partial class FormExibeVacina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_VACINA", Modulo.VACINA))
                {
                    this.GridView_Vacina.Columns.RemoveAt(1);
                    BoundField bound = new BoundField();
                    bound.HeaderText = "Nome";
                    bound.DataField = "Nome";
                    bound.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    this.GridView_Vacina.Columns.Insert(1, bound);
                    this.Lnk_Novo.Visible = false;
                }
                CarregaVacina();
            }
        }

        /// <summary>
        /// Carrega as vacinas cadastradas
        /// </summary>
        private void CarregaVacina()
        {
            GridView_Vacina.DataSource = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ViverMais.Model.Vacina>().OrderBy(p => p.Nome).ToList();
            GridView_Vacina.DataBind();
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaVacina();
            GridView_Vacina.PageIndex = e.NewPageIndex;
            GridView_Vacina.DataBind();
        }
    }
}
