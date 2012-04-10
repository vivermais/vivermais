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
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;

namespace ViverMais.View.Agendamento
{
    public partial class FormRelatorioSolicitacaoDetalhado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_SOLICITACAO_DETALHADA", Modulo.AGENDAMENTO))
                { }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }

            LinkButton eas_pesquisarcnes = this.EAS.WUC_LinkButton_PesquisarCNES;
            LinkButton eas_pesquisarnome = this.EAS.WUC_LinkButton_PesquisarNomeFantasia;

            this.InserirTrigger(eas_pesquisarcnes.UniqueID, "Click", this.UpdatePanel_Unidade);
            this.InserirTrigger(eas_pesquisarnome.UniqueID, "Click", this.UpdatePanel_Unidade);

            eas_pesquisarcnes.Click += new EventHandler(OnClick_PesquisarCNES);
            eas_pesquisarnome.Click += new EventHandler(OnClick_PesquisarNomeFantasiaUnidade);

            if (!IsPostBack)
            {
                this.EAS.WUC_EstabelecimentosPesquisados = new List<ViverMais.Model.EstabelecimentoSaude>();
            }
        }

        private void InserirTrigger(string idcontrole, string nomeevento, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = idcontrole;
            trigger.EventName = nomeevento;
            updatepanel.Triggers.Add(trigger);
            //MasterMain mm = (MasterMain)Master.Master;
            //((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(this.FindControl(idcontrole));
        }

        protected void OnClick_PesquisarCNES(object sender, EventArgs e)
        {
         //   this.CarregaUnidade(this.EAS.WUC_EstabelecimentosPesquisados);
            ViverMais.Model.EstabelecimentoSaude unidade = this.EAS.WUC_EstabelecimentosPesquisados.FirstOrDefault();

            if (unidade != null)
            {
                this.ddlUnidade.Items.Clear();
                this.ddlUnidade.Items.Add(new ListItem(unidade.CNES + "-" + unidade.NomeFantasia, unidade.CNES));
                this.ddlUnidade.Items.Insert(0, new ListItem("Selecione...", "0"));
                this.ddlUnidade.SelectedValue = unidade.CNES;
                this.ddlUnidade.Focus();
                this.UpdatePanel_Unidade.Update();
            }

        }

        //private void CarregaUnidade(IList<ViverMais.Model.EstabelecimentoSaude> unidades)
        //{
        //    this.grid_EstabelecimentoSaude.DataSource = unidades;
        //    this.grid_EstabelecimentoSaude.DataBind();

        //    this.Panel_Unidade.Visible = true;
        //    this.UpdatePanel_Unidade.Update();
        //}

        ///// <summary>
        ///// Permite a paginação para o gridView de estabelecimentos
        ///// </summary>
        ///// <param name="sender">Objeto de envio</param>
        ///// <param name="e">Página de acesso para listagem</param>
        //protected void onPageEstabelecimento(object sender, GridViewPageEventArgs e)
        //{
        //    grid_EstabelecimentoSaude.DataSource = this.EAS.WUC_EstabelecimentosPesquisados;
        //    grid_EstabelecimentoSaude.DataBind();

        //    grid_EstabelecimentoSaude.PageIndex = e.NewPageIndex;
        //    grid_EstabelecimentoSaude.DataBind();
        //}

        ///// <summary>
        ///// Função que redireciona o usuário para a página de edição do estabelecimento escolhido.
        ///// </summary>
        ///// <param name="sender">Objeto de envio</param>
        ///// <param name="e">Comando escolhido no GridView para com o estabelecimento</param>
        ///// 
        //protected void OnRowCommand_VerificarAcao(object sender, GridViewCommandEventArgs e)
        //{
        //    string cnes = "";
        //    if (e.CommandName == "cn_visualizarEstabelecimento")
        //        cnes = (grid_EstabelecimentoSaude.DataKeys[int.Parse(e.CommandArgument.ToString())]["CNES"].ToString());
        //    ViverMais.Model.EstabelecimentoSaude eas = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(cnes);
        //    if (eas != null)
        //    {
        //        tbxCnes.Text = cnes;
        //        tbxUnidade.Text = eas.NomeFantasia;
        //    }

        //}

        protected void OnClick_PesquisarNomeFantasiaUnidade(object sender, EventArgs e)
        {
           // this.CarregaUnidade(this.EAS.WUC_EstabelecimentosPesquisados);
            IList<ViverMais.Model.EstabelecimentoSaude> unidades = this.EAS.WUC_EstabelecimentosPesquisados;
            this.ddlUnidade.Items.Clear();
            this.ddlUnidade.Items.Insert(0, new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.EstabelecimentoSaude unidade in unidades)
            {
                this.ddlUnidade.Items.Add(new ListItem(unidade.CNES + "-" + unidade.NomeFantasia, unidade.CNES));
                this.ddlUnidade.SelectedValue = unidade.CNES;
                //this.ddlUnidade.Focus();
                this.UpdatePanel_Unidade.Update();
            }
            this.UpdatePanel_Unidade.Update();

        }

        protected void rbtnTipoPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnTipoPesquisa.SelectedValue == "1")
            {
                PanelPeriodo.Visible = true;
                PanelUnidade.Visible = true;
                PanelProcedimento.Visible = true;
                PanelStatus.Visible = true;
                PanelMunicipio.Visible = true;
                PanelPaciente.Visible = false;
            }
            else
            {
                PanelPaciente.Visible = true;
                PanelPeriodo.Visible = true;
                PanelUnidade.Visible = false;
                PanelProcedimento.Visible = false;
                PanelStatus.Visible = false;
                PanelMunicipio.Visible = false;
            }
        }

        protected void rbtnTipoProcedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            IList<ViverMais.Model.TipoProcedimento> tipoprocedimentos = Factory.GetInstance<ITipoProcedimento>().ListarProcedimentosPorTipo<ViverMais.Model.TipoProcedimento>(rbtnTipoProcedimento.SelectedValue);
            if (tipoprocedimentos.Count != 0)
            {
                ddlProcedimento.Items.Clear();
                IList<Procedimento> procedimentos = new List<Procedimento>();
                IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
                foreach (ViverMais.Model.TipoProcedimento tipoprocedimento in tipoprocedimentos)
                {
                    ViverMais.Model.Procedimento procedimentoTipo = iViverMais.BuscarPorCodigo<ViverMais.Model.Procedimento>(tipoprocedimento.Procedimento);
                    procedimentos.Add(procedimentoTipo);
                }
                procedimentos = procedimentos.Distinct().OrderBy(p => p.Nome).ToList();
                ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
                foreach (Procedimento procedimento in procedimentos)
                    ddlProcedimento.Items.Add(new ListItem(procedimento.Codigo.ToString() + " - " + procedimento.Nome, procedimento.Codigo.ToString()));
            }
        }

        protected void ddlProcedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEspecialidade.Items.Clear();
            if (ddlProcedimento.SelectedValue != "0")
            {
                IList<ViverMais.Model.CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(ddlProcedimento.SelectedValue);
                ddlEspecialidade.Items.Add(new ListItem("Selecione...", "0"));
                foreach (CBO cbo in cbos)
                {
                    ddlEspecialidade.Items.Add(new ListItem(cbo.Codigo.ToString() + "-" + cbo.Nome, cbo.Codigo.ToString()));
                }
            }
        }

        protected void btnGeraRelatorio_Click(object sender, EventArgs e)
        {
            if (rbtnTipoPesquisa.SelectedValue == "2")
            {
                if (rbtnTipoMunicipio.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('É necessário Selecionar um Município.');", true);
                    return;

                }
                if (rbtnTipoMunicipio.SelectedValue == "2" && ddlMunicipios.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe o Município');", true);
                    return;

                }
            }
            if (tbxData_Final.Text =="" && tbxData_Inicial.Text == ""  && tbxCompetencia.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('É necessário que uma das opções de período esteja preenchida.');", true);
                return;
            }

            
            DateTime dataInicial;
            DateTime dataFinal;
            ViverMais.Model.Paciente paciente = null;
            if (tbxCompetencia.Text != "" && tbxData_Inicial.Text == "" && tbxData_Final.Text == "")
            {
                string competencia = tbxCompetencia.Text;
                int ultimodiacompetencia = System.DateTime.DaysInMonth(int.Parse(competencia.Substring(0, 4)), int.Parse(competencia.Substring(4, 2)));
                dataFinal = new DateTime(int.Parse(competencia.Substring(0, 4)), int.Parse(competencia.Substring(4, 2)), ultimodiacompetencia, 23, 59, 59);
                dataInicial = new DateTime(int.Parse(competencia.Substring(0, 4)), int.Parse(competencia.Substring(4, 2)), 1, 0, 0, 0);

            }               
            else
            {
                dataInicial = DateTime.Parse(tbxData_Inicial.Text);
                dataFinal = DateTime.Parse(tbxData_Final.Text);
            }
            if (dataFinal.Subtract(dataInicial).Days >= 31)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O Período tem que ser no máximo 31 dias.');", true);
                return;

            }
            if (tbxCartaoSUS.Text != "")
            {
                IPaciente ipaciente = Factory.GetInstance<IPaciente>();
                paciente = ipaciente.PesquisarPacientePorCNS<ViverMais.Model.Paciente>(tbxCartaoSUS.Text);
                if (paciente == null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert(''Paciente não cadastrado.');", true);
                    return;

                }
            }
            Hashtable hash = Factory.GetInstance<IRelatorioAgendamento>().SolicitacaoDetalhada(rbtTipoUnidade.SelectedValue.ToString(), ddlUnidade.SelectedValue.ToString(), rbtnTipoMunicipio.SelectedValue.ToString(), ddlMunicipios.SelectedValue, dataInicial, dataFinal, rbtnTipoProcedimento.SelectedValue.ToString(), ddlProcedimento.SelectedValue, ddlEspecialidade.SelectedValue, rbtStatus.SelectedValue.ToString(), paciente != null?paciente.Codigo : string.Empty);
            if (hash.Count != 0)
            {
                Session["HashSolicitacoes"] = hash;
                Redirector.Redirect("RelatorioSolicitacaoDetalhado.aspx", "_blank", "");
            }
            else 
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Nenhum registro encontrado!');", true);
                return;
            }
        }

        protected void rbtnTipoMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtnTipoMunicipio.SelectedValue == "2")
            {
                ddlMunicipios.Items.Clear();
                ddlMunicipios.Visible = true;
                IList<Municipio> municipios = Factory.GetInstance<IMunicipio>().ListarPorEstado<Municipio>("BA");
                ddlMunicipios.Items.Add(new ListItem("Selecione...", "0"));
                foreach (Municipio mun in municipios)
                {
                    if (mun.Codigo != "292740")//Ele Retira Salvador da Lista de Pacto
                        ddlMunicipios.Items.Add(new ListItem(mun.Nome, mun.Codigo));
                }
            }
            else
            {
                ddlMunicipios.ClearSelection();
                ddlMunicipios.Visible = false;
            }

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            rbtnTipoPesquisa.ClearSelection();
            tbxCartaoSUS.Text = "";
            ddlUnidade.ClearSelection();
            EAS.LimpaCamposPesquisa();
            rbtTipoUnidade.ClearSelection();
           // Panel_Unidade.Visible = false;
            rbtnTipoMunicipio.ClearSelection();
            ddlMunicipios.ClearSelection();
            tbxData_Inicial.Text = "";
            tbxData_Final.Text = "";
            tbxCompetencia.Text = "";
            rbtnTipoProcedimento.ClearSelection();
            ddlProcedimento.ClearSelection();
            ddlEspecialidade.ClearSelection();
            rbtStatus.ClearSelection();

        }

    }
}
