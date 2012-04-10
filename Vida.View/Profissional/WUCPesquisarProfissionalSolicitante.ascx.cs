﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using System.Drawing;
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Profissional
{
    public partial class WUCPesquisarProfissionalSolicitante : System.Web.UI.UserControl
    {
        public GridView GridView
        {
            get
            {
                return this.GridViewListaProfissionais;
            }
            //set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelExibeDados.Visible = false;
                PanelDados.Visible = true;
                lblSemPesquisa.Visible = false;
                //lblSemRegistro.Visible = false;
                IList<ViverMais.Model.CategoriaOcupacao> categorias = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.CategoriaOcupacao>();
                //Lista todas as ocupações
                foreach (ViverMais.Model.CategoriaOcupacao categoria in categorias)
                {
                    ddlCategoria.Items.Add(new ListItem(categoria.Nome, categoria.Codigo.ToString()));
                }
                ddlCategoria.SelectedValue = "1";//Define o Médico como Default
            }
            
        }

        //protected void lnkBtnPesquisar_Click(object sender, EventArgs e)
        //{
        //    //ViverMais.Model.Profissional profissional = Factory.GetInstance<IProfissional>().BuscaProfissionalPorVinculoCBO<ViverMais.Model.Profissional>(int.Parse(tbxConselho.Text), tbxCBO.Text, tbxCNES.Text);
        //    //Session["WUCProfissionais"] = pacientes;
        //    //GridViewPacientes.DataSource = pacientes;
        //    //GridViewPacientes.DataBind();
        //}

        protected void btnBuscarProfissional_Click(object sender, EventArgs e)
        {
            if (tbxNumConselho.Text.Equals(String.Empty) && (tbxNomeProfissionalPesquisa.Text.Equals(String.Empty)))
            {
                lblSemPesquisa.Visible = true;
                return;
            }
            else
            {
                lblSemPesquisa.Visible = false;
                IList<ProfissionalSolicitante> profSolicitante = Factory.GetInstance<IProfissionalSolicitante>().BuscaProfissionalPorNumeroConselhoECategoria<ProfissionalSolicitante>(ddlCategoria.SelectedValue, tbxNumConselho.Text, tbxNomeProfissionalPesquisa.Text.ToUpper(),0);
                GridViewListaProfissionais.DataSource = profSolicitante;
                GridViewListaProfissionais.DataBind();
                if (profSolicitante.Count != 0)
                {
                    //lblSemRegistro.Visible = false;
                    Session["Profissionais"] = profSolicitante;
                }
                //else
                //{
                //    //lblSemRegistro.Visible = true;
                //    return;
                //}
            }
        }

        public IList<ProfissionalSolicitante> profissionais
        {
            get { return (IList<ProfissionalSolicitante>)Session["Profissionais"]; }
        }

        public ProfissionalSolicitante ProfissionalSelecionado
        {
            get { return (ProfissionalSolicitante)Session["ProfissionalSelecionado"]; }

            set
            {
                ProfissionalSolicitante profissional = (ProfissionalSolicitante)value;
                Session["ProfissionalSelecionado"] = profissional;
                if (profissional != null)
                {
                    lblNome.Text = profissional.Nome;
                    lblOrgaoEmissor.Text = profissional.OrgaoEmissorRegistro.Nome;
                    lblNumeroConselho.Text = profissional.NumeroRegistro;
                }
            }

        }

        protected void GridViewListaProfissionais_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                GridViewListaProfissionais.SelectedRowStyle.BackColor = Color.LightGray;


                //Habilita o Panel para Exibir os Dados do Profissional Selecionado
                PanelExibeDados.Visible = true;
                PanelDados.Visible = false;

                string id_profissional = Convert.ToString(e.CommandArgument);
                ProfissionalSolicitante profSolicitante = Factory.GetInstance<IProfissionalSolicitante>().BuscarPorCodigo<ProfissionalSolicitante>(int.Parse(id_profissional));
                lblNome.Text = profSolicitante.Nome;
                lblNumeroConselho.Text = profSolicitante.NumeroRegistro;
                lblOrgaoEmissor.Text = profSolicitante.OrgaoEmissorRegistro.Nome;
                GridViewListaProfissionais.SelectedRowStyle.BackColor = Color.LightGray;
                Session["ProfissionalSelecionado"] = profSolicitante;
            }
        }
        protected void GridViewListaProfissionais_SelectedIndexChanged(object sender, EventArgs e)
        {
            //IList<ProfissionalSolicitante> profSolicitante = (IList<ProfissionalSolicitante>)Session["Profissionais"];
            //GridViewListaProfissionais.PageIndex = e.
            //GridViewListaProfissionais.DataSource = profissionais;
        }

        protected void GridViewListaProfissionais_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IList<ProfissionalSolicitante> profSolicitante = (IList<ProfissionalSolicitante>)Session["Profissionais"];
            GridViewListaProfissionais.PageIndex = e.NewPageIndex;
            GridViewListaProfissionais.DataSource = profissionais;
            GridViewListaProfissionais.DataBind();
        }

        /// <summary>
        /// Método para alterar o Profissional Selecionado e Refazer a Busca
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnModificarProfissional_Click(object sender, EventArgs e)
        {
            //Desabilita a exibição dos dados e permite a busca limpando os dados da busca
            PanelDados.Visible = true;
            PanelExibeDados.Visible = false;
            lblNome.Text = "";
            lblNumeroConselho.Text = "";
            lblOrgaoEmissor.Text = "";
            GridViewListaProfissionais.DataSource = null;
            GridViewListaProfissionais.DataBind();
            Session["ProfissionalSelecionado"] = null;
        }

        public string CategoriaOcupacao
        {
            get { return this.profissionais[0].OrgaoEmissorRegistro.Nome; }
        }

    }
}