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
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.DAO;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;

namespace ViverMais.View.Agendamento
{
    public partial class FormParametrizacaoFPO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PARAMETRIZACAO_FPO",Modulo.AGENDAMENTO))
                {
                    IViverMaisServiceFacade iProcedimento = Factory.GetInstance<IViverMaisServiceFacade>();
                    IList<ViverMais.Model.GrupoProcedimento> grupos = iProcedimento.ListarTodos<ViverMais.Model.GrupoProcedimento>();
                    ddlGrupo.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (ViverMais.Model.GrupoProcedimento grupo in grupos)
                    {
                        ddlGrupo.Items.Add(new ListItem(grupo.Nome, grupo.Codigo.ToString()));
                    }

                    PanelCnes.Visible = false;
                    PanelProcedimento.Visible = false;
                    CarregaListaParametrizacaoFPO();

                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }

              

        }

        void CarregaListaParametrizacaoFPO()
        {
            IList<ParametrizacaoFPO> parametrizacoes = Factory.GetInstance<IAgendamentoServiceFacade>().ListarTodos<ParametrizacaoFPO>();
            if (parametrizacoes.Count != 0)
            {
                DataTable table = new DataTable();
                table.Columns.Add("Codigo");
                table.Columns.Add("EstabelecimentoSaude");
                table.Columns.Add("ValorPrestador");
                table.Columns.Add("ValorSolicitante");
                table.Columns.Add("Procedimento");
                foreach (ViverMais.Model.ParametrizacaoFPO parametrizacao in parametrizacoes)
                {
                    DataRow row = table.NewRow();
                    row[0] = parametrizacao.Codigo.ToString();
                    IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
                    ViverMais.Model.EstabelecimentoSaude estabelecimento = iEstabelecimento.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(parametrizacao.Cnes);
                    row[1] = estabelecimento.NomeFantasia;
                    row[2] = parametrizacao.ValorPrestador.ToString();
                    row[3] = parametrizacao.ValorSolicitante.ToString();
                    if ((parametrizacao.Id_Procedimento != null) && (parametrizacao.Id_Procedimento != ""))
                    {
                        ViverMais.Model.Procedimento procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<ViverMais.Model.Procedimento>(parametrizacao.Id_Procedimento);
                        row[4] = procedimento.Nome;
                    }
                    else
                    {
                        row[4] = "-";
                    }
                    table.Rows.Add(row);

                }
                GridViewListaFPO.DataSource = table;
                GridViewListaFPO.DataBind();
            }

        }
        protected void rbtnTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbtnTipo.SelectedValue)
            {
                case "1":
                    PanelCnes.Visible = true;
                    tbxcnes1.Text = "";
                    lblcnes1.Text = "";
                    tbxPrestador1.Text = "";
                    tbxSolicitante1.Text = "";
                    ddlSubGrupo.Items.Clear();
                    ddlForma.Items.Clear();
                    ddlProcedimento.Items.Clear();
                    PanelProcedimento.Visible = false;
                    break;
                case "2":
                    PanelProcedimento.Visible = true;
                    tbxcnes.Text = "";
                    lblcnes.Text = "";
                    tbxPrestador.Text = "";
                    tbxSolicitante.Text = "";
                    PanelCnes.Visible = false;
                    break;
            }
        }

        protected void tbxcnes_TextChanged(object sender, EventArgs e)
        {

            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            ViverMais.Model.EstabelecimentoSaude estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxcnes.Text);
            if (estabelecimento != null)
            {
                tbxcnes.Text = estabelecimento.CNES;
                lblcnes.Text = estabelecimento.NomeFantasia;
                Session["unidade"] = estabelecimento.CNES;
            }
            else
            {
                tbxcnes.Text = null;
                lblcnes.Text = "Unidade não Cadastrada com esse CNES!";
            }

        }

        protected void tbxcnes1_TextChanged(object sender, EventArgs e)
        {
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            ViverMais.Model.EstabelecimentoSaude estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxcnes1.Text);
            if (estabelecimento != null)
            {
                tbxcnes1.Text = estabelecimento.CNES;
                lblcnes1.Text = estabelecimento.NomeFantasia;
                Session["unidade"] = estabelecimento.CNES;
            }
            else
            {
                tbxcnes1.Text = null;
                lblcnes1.Text = "Unidade não Cadastrada com esse CNES!";
            }
        }
        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubGrupo.Items.Clear();
            ISubGrupo iSubGrupo = Factory.GetInstance<ISubGrupo>();
            IList<ViverMais.Model.SubGrupoProcedimento> subgrupos = iSubGrupo.BuscarPorGrupo<ViverMais.Model.SubGrupoProcedimento>(ddlGrupo.SelectedValue);
            ddlSubGrupo.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.SubGrupoProcedimento subgrupo in subgrupos)
            {
                ddlSubGrupo.Items.Add(new ListItem(subgrupo.Nome, subgrupo.Codigo.ToString()));
            }

        }

        protected void ddlSubGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlForma.Items.Clear();
            IForma iForma = Factory.GetInstance<IForma>();
            IList<ViverMais.Model.FormaOrganizacaoProcedimento> formas = iForma.BuscarPorGrupoSubGrupo<ViverMais.Model.FormaOrganizacaoProcedimento>(ddlGrupo.SelectedValue, ddlSubGrupo.SelectedValue);
            ddlForma.Items.Add(new ListItem("Selecione...", "0"));
            foreach(ViverMais.Model.FormaOrganizacaoProcedimento forma in formas)
            {
                ddlForma.Items.Add(new ListItem(forma.Nome, forma.Codigo.ToString()));
            }

        }

        protected void ddlForma_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProcedimento.Items.Clear();
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IList<ViverMais.Model.Procedimento> procedimentos = iProcedimento.BuscarPorForma<ViverMais.Model.Procedimento>(ddlForma.SelectedValue);
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.Procedimento procedimento in procedimentos)
            {
                ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo));
            }
        }

        /// <summary>
        /// Evento utilizado para Incluir os parametros Por Procedimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Incluir_Click(object sender, EventArgs e)
        {
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            TipoProcedimento tipoprocedimento = Factory.GetInstance<ITipoProcedimento>().BuscaTipoProcedimento<TipoProcedimento>(ddlProcedimento.SelectedValue);
            if (tipoprocedimento == null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento não parametrizado!.');location='FormParametrizarFPO.aspx';", true);
                return;
            }
            IList<ParametrizacaoFPO> parametrizacao = Factory.GetInstance<IParametrizacaoFPO>().BuscaParametrizacaoPorUnidade<ParametrizacaoFPO>(tbxcnes1.Text, ddlProcedimento.SelectedValue);
            if (parametrizacao.Count == 0)
            {
                ViverMais.Model.ParametrizacaoFPO parametros = new ParametrizacaoFPO();
                parametros.Cnes = Session["unidade"].ToString();
                parametros.Id_Procedimento = ddlProcedimento.SelectedValue;
                parametros.TipoParametrizacao = Convert.ToChar(ParametrizacaoFPO.TiposDeParametrizacao.PROCEDIMENTO);
                parametros.ValorPrestador = int.Parse(tbxPrestador1.Text);
                parametros.ValorSolicitante = int.Parse(tbxSolicitante1.Text);
                parametros.Id_Procedimento = ddlProcedimento.SelectedValue;
                iAgendamento.Salvar(parametros);
                Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 40, "ID: "+ parametros.Codigo));

                ClientScript.RegisterClientScriptBlock(typeof(String), "ok",
                "<script type='text/javascript'>alert('Parametrização do FPO cadastrada com sucesso!');location='FormParametrizarFPO.aspx'</script>");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok",
                "<script type='text/javascript'>alert('Parametrização do FPO já cadastrado para esse Estabelecimento!');location='FormParametrizarFPO.aspx'</script>");
 
            }
        }

        /// <summary>
        /// Evento utilizado para Incluir os parametros Por CNES
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Incluir1_Click(object sender, EventArgs e)
        {
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            IList<ParametrizacaoFPO> parametrizacaoFPO = Factory.GetInstance<IParametrizacaoFPO>().BuscaParametrizacaoPorUnidade<ParametrizacaoFPO>(tbxcnes.Text, "");
            if (parametrizacaoFPO.Count == 0)
            {
                ViverMais.Model.ParametrizacaoFPO parametros = new ParametrizacaoFPO();
                parametros.Cnes = Session["unidade"].ToString();
                parametros.TipoParametrizacao = Convert.ToChar(ParametrizacaoFPO.TiposDeParametrizacao.CNES);
                parametros.ValorPrestador = int.Parse(tbxPrestador.Text);
                parametros.ValorSolicitante = int.Parse(tbxSolicitante.Text);
                iAgendamento.Salvar(parametros);
                Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 40, "ID: " + parametros.Codigo));


                ClientScript.RegisterClientScriptBlock(typeof(String), "ok",
                "<script type='text/javascript'>alert('Parametrização do FPO cadastrada com sucesso!');location='FormParametrizarFPO.aspx'</script>");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok","<script type='text/javascript'>alert('Parametrização do FPO já cadastrado para esse Estabelecimento!')</script>");
                return;
            }
        }

        protected void tbxPrestador1_TextChanged(object sender, EventArgs e)
        {
            int prestador = int.Parse(tbxPrestador1.Text);
            int total = 100;
            tbxSolicitante1.Text = (total - prestador).ToString();
        }

        protected void tbxPrestador_TextChanged(object sender, EventArgs e)
        {
            int prestador = int.Parse(tbxPrestador.Text);
            int total = 100;
            tbxSolicitante.Text = (total - prestador).ToString();

        }


        protected void GridViewListaFPO_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;
            Session["IndexSelecionado"] = index;
            GridViewListaFPO.EditIndex = e.NewEditIndex;
            TextBox tbxPrestador = (TextBox)GridViewListaFPO.Rows[index].FindControl("tbxPercentualPrestador");
            TextBox tbxSolicitante = (TextBox)GridViewListaFPO.Rows[index].FindControl("tbxPercentualSolicitante");
        }

        protected void GridViewListaFPO_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            GridViewRow linha = GridViewListaFPO.Rows[e.RowIndex];
            string id_parametro_fpo = linha.Cells[0].Text;
            string tbxPrestador = ((TextBox)linha.FindControl("tbxPercentualPrestador")).Text;
            string tbxSolicitante = ((TextBox)linha.FindControl("tbxPercentualSolicitante")).Text;
            int valor = (int.Parse(tbxPrestador) + int.Parse(tbxSolicitante));
            if (valor != 100)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A soma do percentual do prestador e do solicitante deve ser igual a 100.');location='FormParametrizarFPO.aspx';", true);
                return;
            }

            ParametrizacaoFPO parametrizacao_fpo = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<ParametrizacaoFPO>(int.Parse(id_parametro_fpo));
            if (parametrizacao_fpo != null)
            {
                parametrizacao_fpo.ValorPrestador = int.Parse(tbxPrestador.ToString());
                parametrizacao_fpo.ValorSolicitante = int.Parse(tbxSolicitante.ToString());
                Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(parametrizacao_fpo);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvas com Sucesso.');location='FormParametrizarFPO.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi possível alterar. Tente Novamente.');location='FormParametrizarFPO.aspx';", true);
                return;

            }

        }

        protected void GridViewListaFPO_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewListaFPO.EditIndex = -1;
            Session["IndexSelecionado"] = null;
            CarregaListaParametrizacaoFPO();
        }
    }
}
