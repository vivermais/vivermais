using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Vacina;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Vacina
{
    public partial class FormExibeDoenca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_CAMPANHA"))
                //   ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                //else

                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_DOENCA_VACINA", Modulo.VACINA))
                {
                    this.GridView_Doenca.Columns.RemoveAt(0);
                    BoundField bound = new BoundField();
                    bound.HeaderText = "Nome";
                    bound.DataField = "Nome";
                    bound.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    this.GridView_Doenca.Columns.Insert(0, bound);
                    this.Lnk_Novo.Visible = false;
                }

                CarregaDoenca();
            }
        }

        /// <summary>
        /// Carrega as campanhas cadastrados
        /// </summary>
        private void CarregaDoenca()
        {
            GridView_Doenca.DataSource = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<Doenca>().OrderBy(p => p.Nome).ToList();
            GridView_Doenca.DataBind();
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaDoenca();
            GridView_Doenca.PageIndex = e.NewPageIndex;
            GridView_Doenca.DataBind();
        }
    }
}
