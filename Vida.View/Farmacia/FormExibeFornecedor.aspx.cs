﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormExibeFornecedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_FORNECEDOR",Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                    CarregaFornecedor();
            }
        }

        /// <summary>
        /// Carrega os fornecedores cadastrados
        /// </summary>
        private void CarregaFornecedor()
        {
            GridView_Fornecedor.DataSource = Factory.GetInstance<IFornecedorMedicamento>().ListarTodos<FornecedorMedicamento>().OrderBy(p => p.RazaoSocial).ToList();
            GridView_Fornecedor.DataBind();
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e) 
        {
            CarregaFornecedor();
            GridView_Fornecedor.PageIndex = e.NewPageIndex;
            GridView_Fornecedor.DataBind();
        }
    }
}
