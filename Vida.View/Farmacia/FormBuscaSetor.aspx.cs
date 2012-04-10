﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Text.RegularExpressions;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormBuscaSetor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_SETOR",Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    CarregaSetores();

                    if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "ASSOCIAR_SETOR_UNIDADE", Modulo.FARMACIA))
                        TabPanel_Dois.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Carrega os setores da cadastrados
        /// </summary>
        private void CarregaSetores()
        {
            GridView_Setor.DataSource = Factory.GetInstance<ISetor>().ListarTodos<Setor>().OrderBy(p => p.Nome).ToList();
            GridView_Setor.DataBind();
        }

        /// <summary>
        /// Pesquisa os setores de acordo com a descrição
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Pesquisar(object sender, EventArgs e) 
        {
            //Button bt = (Button)sender;
            //ISetor iS = Factory.GetInstance<ISetor>();

            //IList<Setor> ls = new List<Setor>();

            //ViewState["tipo_pesquisa"] = bt.CommandArgument;

            //if (bt.CommandArgument == "todos" )
            //    ls = iS.ListarTodos<Setor>().OrderBy(p => p.Nome).ToList();
            //else
            //{
            //    ViewState["setor"] = TextBox_Setor.Text;
            //    ls = iS.BuscarPorDescricao<Setor>(TextBox_Setor.Text);
            //}

            //GridView_Setor.DataSource = ls;
            //GridView_Setor.DataBind();
            //Panel_Resultado.Visible = true;
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e) 
        {
            CarregaSetores();
            //if (ViewState["tipo_pesquisa"].ToString() == "todos")
            //    GridView_Setor.DataSource = Factory.GetInstance<ISetor>().ListarTodos<Setor>().OrderBy(p => p.Nome).ToList();
            //else
            //    GridView_Setor.DataSource = Factory.GetInstance<ISetor>().BuscarPorDescricao<Setor>(ViewState["setor"].ToString());

            GridView_Setor.PageIndex = e.NewPageIndex;
            GridView_Setor.DataBind();
        }
    }
}