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
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.BLL;
using System.Collections.Generic;

namespace ViverMais.View.Agendamento
{
    public partial class FormSolicitacoesPorPaciente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_AUTORIZACAO_AMBULATORIAL", Modulo.AGENDAMENTO))
                {
                    if (Request.QueryString["Codigo"] != null)
                    {
                        int co_solicitacao = int.Parse(Request.QueryString["Codigo"].ToString());
                        ViewState["Solicitacao"] = co_solicitacao;
                        Solicitacao solicitacao = Factory.GetInstance<ISolicitacao>().BuscarPorCodigo<Solicitacao>(co_solicitacao);
                        if (solicitacao != null)
                        {
                            ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
                            if (paciente != null)
                            {
                                CarregaDadosPaciente(paciente);
                                CarregaSolicitacoesPorPaciente(paciente);
                            }
                        }
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void CarregaDadosPaciente(ViverMais.Model.Paciente paciente)
        {
            lblNome.Text = paciente.Nome;
            lblSexo.Text = paciente.Sexo == 'M' ? "Masculino" : "Feminino";
            lblDataNascimento.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
            IList<CartaoSUS> cartoes = CartaoSUSBLL.ListarPorPaciente(paciente);
            if (cartoes.Count != 0)
            {
                for (int i = 0; i < cartoes.Count; i++)
                {
                    if (i == 0)
                        lblCNS.Text = cartoes[i].Numero;
                    else
                        lblCNS.Text += " / " + cartoes[i].Numero;
                }
            }

            Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
            if (endereco != null)
            {
                lblTelefone.Text = endereco.DDD + " " + endereco.Telefone;
                lblMunicipio.Text = endereco.Municipio.Nome;
            }
        }

        protected void CarregaSolicitacoesPorPaciente(ViverMais.Model.Paciente paciente)
        {
            IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().ListarSolicitacoesPorPaciente<Solicitacao>(paciente.Codigo, string.Empty).OrderBy(p=>p.Situacao).ToList();
            GridViewSolicitacoes.DataSource = solicitacoes;
            GridViewSolicitacoes.DataBind();
            Session["SolicitacoesPaciente"] = solicitacoes;
        }

        protected void GridViewSolicitacoes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IList<Solicitacao> solicitacoes = (IList<Solicitacao>)Session["SolicitacoesPaciente"];
            GridViewSolicitacoes.DataSource = solicitacoes;
            GridViewSolicitacoes.PageIndex = e.NewPageIndex;
            GridViewSolicitacoes.DataBind();
        }

        protected void GridViewSolicitacoes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if (e.Row.Cells[4].Text != "PENDENTE" && e.Row.Cells[4].Text != "AG. AUTOMÁTICO")
                    e.Row.FindControl("btnSelecionar").Visible = false;
        }

        protected void btnConcluir_Click(object sender, EventArgs e)
        {
            Session.Remove("SolicitacoesPaciente");
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "close", "window.close(); window.opener.location.reload();", true);
        }

        protected void GridViewSolicitacoes_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Selecionar")
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Autorização de Solicitação','../FormAutoriza.aspx?id_solicitacao=" + e.CommandArgument.ToString() + "');", true);
        }
    }
}
