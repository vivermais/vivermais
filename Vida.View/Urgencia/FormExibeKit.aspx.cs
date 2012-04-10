﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Urgencia
{
    public partial class FormExibeKit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "CADASTRAR_KIT_PA", Modulo.URGENCIA))
                {
                    this.LinkButton_NovoRegistro.Visible = false;
                    this.GridView_Kits.Columns.RemoveAt(1);
                }
                this.ExibeKits();
            }
        }

        /// <summary>
        /// Carrega os kits cadastrados
        /// </summary>
        private void ExibeKits()
        {
            IList<KitPA> kits = Factory.GetInstance<IKitPA>().ListarTodos<KitPA>("Nome", true);
            Session["kits"] = kits;
            GridView_Kits.DataSource = kits;
            GridView_Kits.DataBind();
        }

        /// <summary>
        /// Paginação do gridview de kits existentes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_PaginacaoKits(object sender, GridViewPageEventArgs e)
        {
            GridView_Kits.DataSource = (IList<KitPA>)Session["kits"];
            GridView_Kits.DataBind();
            GridView_Kits.PageIndex = e.NewPageIndex;
            GridView_Kits.DataBind();
        }
    }
}
