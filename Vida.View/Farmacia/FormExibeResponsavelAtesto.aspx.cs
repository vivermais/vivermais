using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;

namespace ViverMais.View.Farmacia
{
    public partial class FormExibeResponsavelAtesto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CarregaResponsaveis();
            }
        }

        /// <summary>
        /// Carrega os responsáveis cadastrados pelo atesto das notas fiscais
        /// </summary>
        private void CarregaResponsaveis()
        {
            GridView_Responsavel.DataSource = Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<ResponsavelAtesto>().OrderBy(p => p.Nome).ToList();
            GridView_Responsavel.DataBind();
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e) 
        {
            CarregaResponsaveis();
            GridView_Responsavel.PageIndex = e.NewPageIndex;
            GridView_Responsavel.DataBind();
        }
    }
}
