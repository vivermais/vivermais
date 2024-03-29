﻿using System;
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
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using System.IO;

namespace ViverMais.View.Urgencia
{
    public partial class FormClassificacaoRisco : System.Web.UI.Page
    {
        protected void Page_Init()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                //if (!Factory.GetInstance<IRelatorioUrgencia>().PAAtivo(((Usuario)Session["Usuario"]).Unidade.CNES))
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, o Módulo Urgência ainda não está disponível para sua unidade! Por favor, procure a administração.');location='../Home.aspx';", true);

                CarregaClassificacoesRisco();
            }
        }

        /// <summary>
        /// Carrega os tipos de classificações existentes
        /// </summary>
        private void CarregaClassificacoesRisco()
        {
            IList<ViverMais.Model.ClassificacaoRisco> classificacoesrisco = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.ClassificacaoRisco>().OrderBy(tc => tc.Ordem).ToList();
            GridView_TipoConsultorio.DataSource = classificacoesrisco;
            GridView_TipoConsultorio.DataBind();
        }

        /// <summary>
        /// Formata o gridview de classificação de risco
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridView(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Image img = (Image)e.Row.FindControl("Imagem_Cor");
                img.ImageUrl = "~/Urgencia/img/" + img.ImageUrl;
            }
        }
    }
}
