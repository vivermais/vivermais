﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormExibeFabricante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_FABRICANTE",Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                    CarregaFabricantes();
            }
        }

        /// <summary>
        /// Carrega os fabricantes de medicamentos existentes
        /// </summary>
        private void CarregaFabricantes()
        {
            GridView_Fabricante.DataSource = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.FabricanteMedicamento>().OrderBy(p => p.Nome).ToList();
            GridView_Fabricante.DataBind();
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e) 
        {
            CarregaFabricantes();
            GridView_Fabricante.PageIndex = e.NewPageIndex;
            GridView_Fabricante.DataBind();
        }
    }
}
