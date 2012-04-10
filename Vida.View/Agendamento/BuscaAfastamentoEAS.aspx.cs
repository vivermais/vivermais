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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class BuscaAfastamentoEAS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPesquisarEstabelecimento_Click(object sender, EventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_AFASTAR_EAS", Modulo.AGENDAMENTO))
            {
                if ((tbxCNES.Text == "") && (tbxDescricao.Text == ""))
                    lblResultado.Text = "Informe o CNES ou a Descrição para realizar a pesquisa.";
                else
                {
                    lblResultado.Text = "";
                    if (tbxCNES.Text != "")
                    {
                        ViverMais.Model.EstabelecimentoSaude estabelecimentoSaude = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxCNES.Text);
                        if (estabelecimentoSaude == null)
                            lblResultado.Text = "Estabelecimento de Saúde não encontrado.";
                        else
                        {
                            ddlEstabelecimentoSaude.Items.Clear();
                            ddlEstabelecimentoSaude.Items.Add(new ListItem(estabelecimentoSaude.NomeFantasia, estabelecimentoSaude.CNES.ToString()));
                        }
                    }
                    else
                    {
                        IList<ViverMais.Model.EstabelecimentoSaude> estabelecimentosSaude = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorNomeFantasia<ViverMais.Model.EstabelecimentoSaude>(tbxDescricao.Text.ToUpper());
                        ddlEstabelecimentoSaude.Items.Clear();
                        ddlEstabelecimentoSaude.Items.Add(new ListItem("Selecione...", "0"));
                        foreach (ViverMais.Model.EstabelecimentoSaude estabSaude in estabelecimentosSaude)
                            ddlEstabelecimentoSaude.Items.Add(new ListItem(estabSaude.NomeFantasia, estabSaude.CNES.ToString()));
                    }
                    ddlEstabelecimentoSaude.Focus();
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
        }

        protected void btnPesquisarAfastamento_Click(object sender, EventArgs e)
        {
            if (ddlEstabelecimentoSaude.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('É necessério pesquisar um Estabelecimento de Saúde!');", true);
                return;
            }
            IList<AfastamentoEAS> afastamentos = Factory.GetInstance<IAfastamentoEAS>().BuscarAfastamentosPorUnidade<AfastamentoEAS>(ddlEstabelecimentoSaude.SelectedValue);
            GridViewAfastamentos.DataSource = afastamentos;
            GridViewAfastamentos.DataBind();
            if ((tbxCNES.Text != "") && (tbxDescricao.Text == ""))
            {
                lbCNES.Text = tbxCNES.Text;
                lbEstabelecimentoSaude.Text = ddlEstabelecimentoSaude.SelectedItem.Text;
            }
            else
            {
                ViverMais.Model.EstabelecimentoSaude estabelecimento = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(ddlEstabelecimentoSaude.SelectedValue);
                lbCNES.Text = estabelecimento.CNES;
                lbEstabelecimentoSaude.Text = estabelecimento.NomeFantasia;
            }
            PanelAgendamento.Visible = true;
        }

        protected void GridViewAfastamentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarAfastamento")
            {
                int index = int.Parse(e.CommandArgument.ToString()) == 0 ? GridViewAfastamentos.PageIndex * GridViewAfastamentos.PageSize : (GridViewAfastamentos.PageIndex * GridViewAfastamentos.PageSize) + int.Parse(e.CommandArgument.ToString());
                GridViewRow row = GridViewAfastamentos.Rows[index];
                int id_afastamento = int.Parse(Server.HtmlDecode(row.Cells[0].Text));
                Response.Redirect("FormAfastarEAS.aspx?id_afastamento="+id_afastamento);
            }
        }
    }
}