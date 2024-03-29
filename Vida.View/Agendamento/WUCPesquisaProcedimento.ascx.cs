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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Agendamento
{
    public partial class WUCPesquisaProcedimento : System.Web.UI.UserControl
    {
        public GridView GridView
        {
            get
            {
                return this.GridViewListaProcedimento;
            }
            //set;
        }

        public IList<Procedimento> procedimentos
        {
            get { return (IList<Procedimento>)Session["Procedimentos"]; }
        }

        public Procedimento ProcedimentoSelecionado
        {
            get { return (Procedimento)Session["ProcedimentoSelecionado"]; }

            set
            {
                Procedimento procedimento = (Procedimento)value;
                Session["ProcedimentoSelecionado"] = procedimento;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPesquisarProcedimento_Click(object sender, EventArgs e)
        {
            if (tbxCodigoProcedimento.Text.Trim() == String.Empty && tbxNomeProcedimento.Text == String.Empty)
            {
                lblSemPesquisa.Visible = true;
                return;
            }
            else
            {
                lblSemPesquisa.Visible = false;
                if (tbxCodigoProcedimento.Text.Trim() != String.Empty && tbxNomeProcedimento.Text != String.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Preencha apenas um critério de pesquisa!');", true);
                    return;
                }

                IList<ViverMais.Model.Procedimento> procedimentos = null;
                Procedimento procedimento = null;

                if (tbxNomeProcedimento.Text != String.Empty)
                {
                    procedimentos = Factory.GetInstance<IProcedimento>().BuscarPorNome<ViverMais.Model.Procedimento>(tbxNomeProcedimento.Text.ToUpper());

                }
                else if (tbxCodigoProcedimento.Text.Trim() != String.Empty)
                {
                    procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<ViverMais.Model.Procedimento>(tbxCodigoProcedimento.Text);
                    if (procedimento != null)
                    {
                        procedimentos = new List<Procedimento>();
                        procedimentos.Add(procedimento);
                    }
                }
                GridViewListaProcedimento.DataSource = procedimentos;
                GridViewListaProcedimento.DataBind();
                Session["Procedimentos"] = procedimentos;
            }
        }

        protected void GridViewListaProcedimento_OnPageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            GridViewListaProcedimento.DataSource = Session["Procedimentos"];
            GridViewListaProcedimento.DataBind();
            GridViewListaProcedimento.PageIndex = e.NewPageIndex;
        }

        protected void GridViewListaProcedimento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string co_procedimento = Convert.ToString(e.CommandArgument);
            Procedimento procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(co_procedimento);
            if (procedimento != null)
            {
                Session["ProcedimentoSelecionado"] = procedimento;
                //ddlProcedimento.Items.Clear();
                //ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo));
                ////Session["ProcedimentoSelecionado"] = procedimento;
                //GridViewListaProcedimento.SelectedRowStyle.BackColor = Color.LightGray;
                //PanelPesquisaProcedimento.Visible = false;

                //// Busca os CBOs do Procedimento Selecionado
                //IList<ViverMais.Model.CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(procedimento.Codigo);

                //ddlCBO.Items.Clear();
                //ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
                ////Carrega todas as especialidades
                //foreach (CBO cbo in cbos)
                //    ddlCBO.Items.Add(new ListItem(cbo.Nome, cbo.Codigo.ToString()));
                //ddlCBO.Focus();
            }
        }
    }
}