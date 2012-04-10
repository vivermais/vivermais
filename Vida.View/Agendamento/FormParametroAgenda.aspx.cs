using System;
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
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.Model.Entities.ViverMais;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using System.Drawing;

namespace ViverMais.View.Agendamento
{
    public partial class FormParametroAgenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButton eas_pesquisarcnes = this.WUC_PesquisarEstabelecimento1.WUC_LinkButton_PesquisarCNES;
            LinkButton eas_pesquisarnome = this.WUC_PesquisarEstabelecimento1.WUC_LinkButton_PesquisarNomeFantasia;

            eas_pesquisarcnes.Click += new EventHandler(OnClick_PesquisarCNES);
            eas_pesquisarnome.Click += new EventHandler(OnClick_PesquisarNomeFantasiaUnidade);

            this.InserirTrigger(eas_pesquisarcnes.UniqueID, "Click", this.UpdatePanel_Unidade);
            this.InserirTrigger(eas_pesquisarnome.UniqueID, "Click", this.UpdatePanel_Unidade);

            if (!IsPostBack)
            {
                GridViewHelper helper = new GridViewHelper(this.GridViewParametros);
                helper.RegisterGroup("CNES", true, true);
                helper.RegisterSummary("Percentual", SummaryOperation.Sum, "CNES");

                this.WUC_PesquisarEstabelecimento1.WUC_EstabelecimentosPesquisados = new List<ViverMais.Model.EstabelecimentoSaude>();
                //this.ddlUnidade.Attributes.Add("onmouseover", "javascript:showTooltip(this);");

                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PARAMETRIZACAO_AGENDA_AMBULATORIAL", Modulo.AGENDAMENTO))
                {
                    PanelPesquisaProcedimento.Visible = false;
                    PanelListaParametros.Visible = false;
                    PanelProcedimento.Visible = false;
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void OnClick_PesquisarCNES(object sender, EventArgs e)
        {
            ViverMais.Model.EstabelecimentoSaude unidade = this.WUC_PesquisarEstabelecimento1.WUC_EstabelecimentosPesquisados.FirstOrDefault();

            if (unidade != null)
            {
                this.ddlUnidade.Items.Clear();
                this.ddlUnidade.Items.Add(new ListItem(unidade.NomeFantasia, unidade.CNES));
                this.ddlUnidade.Items.Insert(0, new ListItem("Selecione...", "0"));
                this.ddlUnidade.SelectedValue = unidade.CNES;
                this.ddlUnidade.Focus();
                this.UpdatePanel_Unidade.Update();
            }
        }

        protected void OnClick_PesquisarNomeFantasiaUnidade(object sender, EventArgs e)
        {
            IList<ViverMais.Model.EstabelecimentoSaude> unidades = this.WUC_PesquisarEstabelecimento1.WUC_EstabelecimentosPesquisados;

            this.ddlUnidade.DataSource = unidades;
            this.ddlUnidade.DataBind();
            this.ddlUnidade.Items.Insert(0, new ListItem("Selecione...", "0"));

            this.ddlUnidade.Focus();
            this.UpdatePanel_Unidade.Update();
        }

        private void InserirTrigger(string idcontrole, string nomeevento, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = idcontrole;
            trigger.EventName = nomeevento;
            updatepanel.Triggers.Add(trigger);
        }

        protected void btnPesquisar_OnClick(object sender, EventArgs e)
        {
            if (rbtTipo.SelectedValue != String.Empty)
            {
                if (rbtTipo.SelectedValue.Equals(ParametroAgenda.CONFIGURACAO_UNIDADE))
                {
                    if (ddlUnidade.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Selecione o Estabelecimento!');", true);
                        return;
                    }

                }
                else if (rbtTipo.SelectedValue.Equals(ParametroAgenda.CONFIGURACAO_PROCEDIMENTO))
                {
                    if (ddlUnidade.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Selecione o Estabelecimento!');", true);
                        return;
                    }
                    else
                    {
                        if (ddlProcedimento.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Selecione o Procedimento!');", true);
                            return;
                        }
                        else if (ddlCBO.SelectedValue == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Informe a Especialidade!');", true);
                            return;
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Informe o Tipo de Configuração!');", true);
                return;
            }

            //if (tbxCnes.Text != "")
            //{
            //ViverMais.Model.EstabelecimentoSaude estabelecimento = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(tbxCnes.Text);
            //if (estabelecimento == null)
            //{
            //    lblCnes.Text = "CNES não cadastrado";
            //    lknSalvar.Enabled = false;
            //    tbxCnes.Focus();
            //    return;
            //}
            //else
            //{
            //tbxCnes.Text = estabelecimento.CNES;
            //lblCnes.Text = estabelecimento.NomeFantasia;
            lknSalvar.Enabled = true;

            //IList<ViverMais.Model.ParametroAgenda> parametros = Factory.GetInstance<IParametroAgenda>().BuscarParametros<ViverMais.Model.ParametroAgenda>(ddlUnidade.SelectedValue, rbtTipo.SelectedValue, ddlProcedimento.SelectedValue, ddlCBO.SelectedValue, int.Parse(ddlSubGrupo.SelectedValue));
            IList<ViverMais.Model.ParametroAgenda> parametros = Factory.GetInstance<IParametroAgenda>().BuscarParametros<ViverMais.Model.ParametroAgenda>(ddlUnidade.SelectedValue, rbtTipo.SelectedValue, ddlProcedimento.SelectedValue, ddlCBO.SelectedValue, (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));


            DataTable table = new DataTable();
            table.Columns.Add("Codigo");
            table.Columns.Add("Cnes");
            table.Columns.Add("Tipo");
            table.Columns.Add("Percentual", typeof(int));

            if (parametros.Count != 0)
            {
                for (int i = 0; i < parametros.Count; i++)
                {
                    ParametroAgenda ag = parametros[i];
                    if (i == 0)
                        if (ag.ParametroAgendaUnidade != null)
                            if (ag.ParametroAgendaUnidade.SolicitaOutrasUnidades)
                                rbtnVisualizaOutrasAgendas.SelectedValue = "S";
                            else
                                rbtnVisualizaOutrasAgendas.SelectedValue = "N";
                        else
                            rbtnVisualizaOutrasAgendas.ClearSelection();

                    DataRow row = table.NewRow();
                    row[0] = ag.Codigo;
                    row[1] = ddlUnidade.Items.FindByValue(ddlUnidade.SelectedValue).Text;
                    switch (ag.TipoAgenda)
                    {
                        case 1: row[2] = "Distrital";
                            break;
                        case 2: row[2] = "Específica";
                            break;
                        case 3: row[2] = "Local";
                            break;
                        case 4: row[2] = "Rede";
                            break;
                        case 5: row[2] = "Reserva Técnica";
                            break;
                    }
                    row[3] = ag.Percentual;
                    table.Rows.Add(row);
                }
            }
            GridViewParametros.DataSource = table;
            GridViewParametros.DataBind();
            //}
            //}
            PanelListaParametros.Visible = true;
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            int total = int.Parse(tbxPct_Local.Text) + int.Parse(tbxPct_Distrital.Text) + int.Parse(tbxPct_Rede.Text) + int.Parse(tbxPct_Reserva.Text);
            if (total != 100)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Soma dos percentuais deve ser 100!');", true);
                return;
            }

            IParametroAgenda iParametro = Factory.GetInstance<IParametroAgenda>();
            ParametroAgenda parametroagenda = new ParametroAgenda();

            ViverMais.Model.EstabelecimentoSaude eas = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(ddlUnidade.SelectedValue);

            //IList<ParametroAgenda> parametrosagenda = iParametro.BuscarParametros<ViverMais.Model.ParametroAgenda>(eas.CNES, rbtTipo.SelectedValue, ddlProcedimento.SelectedValue, ddlCBO.SelectedValue, int.Parse(ddlSubGrupo.SelectedValue));
            IList<ParametroAgenda> parametrosagenda = iParametro.BuscarParametros<ViverMais.Model.ParametroAgenda>(eas.CNES, rbtTipo.SelectedValue, ddlProcedimento.SelectedValue, ddlCBO.SelectedValue, (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));
            ParametroAgendaUnidade parametroAgendaUnidade = null;
            if (!String.IsNullOrEmpty(rbtnVisualizaOutrasAgendas.SelectedValue))
            {
                if (parametrosagenda.Where(param => param.ParametroAgendaUnidade != null).ToList().Count == 0)//Caso não tenha configurado se o Estabelecimento Pode Solicitar procedimento para outras unidades
                {
                    parametroAgendaUnidade = new ParametroAgendaUnidade();
                    parametroAgendaUnidade.Estabelecimento = eas;
                }
                else
                    parametroAgendaUnidade = parametrosagenda.Where(param => param.ParametroAgendaUnidade != null).ToList().Select(param => param.ParametroAgendaUnidade).ToList().FirstOrDefault();
                if (rbtTipo.SelectedValue == "U" && rbtnVisualizaOutrasAgendas.SelectedValue == "S")
                    parametroAgendaUnidade.SolicitaOutrasUnidades = true;
                else if (rbtTipo.SelectedValue == "U" && rbtnVisualizaOutrasAgendas.SelectedValue == "N")
                    parametroAgendaUnidade.SolicitaOutrasUnidades = false;
                iParametro.Salvar(parametroAgendaUnidade);

            }
            //Verifico se o tipo de Configuração é por Unidade
            if (rbtTipo.SelectedValue.Equals(ParametroAgenda.CONFIGURACAO_UNIDADE))
            {
                if (parametrosagenda.Count != 0)
                {
                    for (int i = 0; i < parametrosagenda.Count; i++)
                    {
                        if (parametrosagenda[i].TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.DISTRITAL))//Distrital
                        {
                            parametrosagenda[i].Percentual = int.Parse(tbxPct_Distrital.Text);
                        }
                        else if (parametrosagenda[i].TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.LOCAL))//Local
                        {
                            parametrosagenda[i].Percentual = int.Parse(tbxPct_Local.Text);
                        }
                        else if (parametrosagenda[i].TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.REDE)) //Rede
                        {
                            parametrosagenda[i].Percentual = int.Parse(tbxPct_Rede.Text);
                        }
                        else if (parametrosagenda[i].TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.RESERVA_TECNICA)) //Rede
                        {
                            parametrosagenda[i].Percentual = int.Parse(tbxPct_Reserva.Text);
                        }
                        if (parametroAgendaUnidade != null)
                            parametrosagenda[i].ParametroAgendaUnidade = parametroAgendaUnidade;
                        iParametro.Salvar(parametrosagenda[i]);
                        Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 51, parametrosagenda[i].Codigo.ToString()));
                    }

                    if (parametrosagenda.Count == 3)//Caso o parametro seja por unidade e não tenha definido ainda o percentual de reserva técnica, tem que salvar
                    {
                        ParametroAgenda parametro = new ParametroAgenda();
                        ParametroAgenda parametroComDados = parametrosagenda.FirstOrDefault();
                        parametro.Cnes = parametroComDados.Cnes;
                        parametro.TipoAgenda = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.RESERVA_TECNICA);
                        parametro.TipoConfiguracao = ParametroAgenda.CONFIGURACAO_UNIDADE;
                        parametro.Percentual = int.Parse(tbxPct_Reserva.Text);
                        if (parametroAgendaUnidade != null)
                            parametro.ParametroAgendaUnidade = parametroAgendaUnidade;
                        iParametro.Salvar(parametro);
                    }
                }
                else
                {
                    //Percentual Local
                    parametroagenda.TipoAgenda = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.LOCAL);
                    parametroagenda.Cnes = ddlUnidade.SelectedValue;
                    parametroagenda.Percentual = int.Parse(tbxPct_Local.Text);
                    parametroagenda.TipoConfiguracao = ParametroAgenda.CONFIGURACAO_UNIDADE;
                    if (parametroAgendaUnidade != null)
                        parametroagenda.ParametroAgendaUnidade = parametroAgendaUnidade;
                    iParametro.Salvar(parametroagenda);
                    Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 16, parametroagenda.Codigo.ToString()));

                    //Percentual Distrital            
                    parametroagenda = new ParametroAgenda();
                    parametroagenda.TipoAgenda = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.DISTRITAL);
                    parametroagenda.Cnes = ddlUnidade.SelectedValue;
                    parametroagenda.Percentual = int.Parse(tbxPct_Distrital.Text);
                    parametroagenda.TipoConfiguracao = ParametroAgenda.CONFIGURACAO_UNIDADE;
                    if (parametroAgendaUnidade != null)
                        parametroagenda.ParametroAgendaUnidade = parametroAgendaUnidade;
                    iParametro.Salvar(parametroagenda);
                    Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 16, parametroagenda.Codigo.ToString()));

                    //Percentual Rede
                    parametroagenda = new ParametroAgenda();
                    parametroagenda.TipoAgenda = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.REDE);
                    parametroagenda.Cnes = ddlUnidade.SelectedValue;
                    parametroagenda.Percentual = int.Parse(tbxPct_Rede.Text);
                    parametroagenda.TipoConfiguracao = ParametroAgenda.CONFIGURACAO_UNIDADE;
                    if (parametroAgendaUnidade != null)
                        parametroagenda.ParametroAgendaUnidade = parametroAgendaUnidade;
                    iParametro.Salvar(parametroagenda);
                    Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 16, parametroagenda.Codigo.ToString()));

                    //Percentual Reserva Técnica
                    parametroagenda = new ParametroAgenda();
                    parametroagenda.TipoAgenda = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.RESERVA_TECNICA);
                    parametroagenda.Cnes = ddlUnidade.SelectedValue;
                    parametroagenda.Percentual = int.Parse(tbxPct_Reserva.Text);
                    parametroagenda.TipoConfiguracao = ParametroAgenda.CONFIGURACAO_UNIDADE;
                    if (parametroAgendaUnidade != null)
                        parametroagenda.ParametroAgendaUnidade = parametroAgendaUnidade;
                    iParametro.Salvar(parametroagenda);
                    Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 16, parametroagenda.Codigo.ToString()));
                }

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
                btnPesquisar_OnClick(new object(), new EventArgs());
            }
            else if (rbtTipo.SelectedValue.Equals(ParametroAgenda.CONFIGURACAO_PROCEDIMENTO))
            {
                //Caso seja uma atualização dos parametros do procedimento e especialidade
                if (parametrosagenda.Count != 0)
                {
                    for (int i = 0; i < parametrosagenda.Count; i++)
                    {
                        if (parametrosagenda[i].TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.DISTRITAL))//Distrital
                        {
                            parametrosagenda[i].Percentual = int.Parse(tbxPct_Distrital.Text);
                        }
                        else if (parametrosagenda[i].TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.LOCAL))//Local
                        {
                            parametrosagenda[i].Percentual = int.Parse(tbxPct_Local.Text);
                        }
                        else if (parametrosagenda[i].TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.REDE)) //Rede
                        {
                            parametrosagenda[i].Percentual = int.Parse(tbxPct_Rede.Text);
                        }
                        else if (parametrosagenda[i].TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.RESERVA_TECNICA)) //Rede
                        {
                            parametrosagenda[i].Percentual = int.Parse(tbxPct_Reserva.Text);
                        }
                        if (parametroAgendaUnidade != null)
                            parametrosagenda[i].ParametroAgendaUnidade = parametroAgendaUnidade;
                        iParametro.Salvar(parametrosagenda[i]);
                        Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 51, parametrosagenda[i].Codigo.ToString()));
                    }
                }
                else // Caso não seja uma atualização de parametros, irá inserir novos
                {
                    Procedimento procedimento = iParametro.BuscarPorCodigo<Procedimento>(ddlProcedimento.SelectedValue);
                    CBO cbo = iParametro.BuscarPorCodigo<CBO>(ddlCBO.SelectedValue);
                    if (procedimento != null && cbo != null)
                    {
                        //Percentual Local
                        parametroagenda.TipoAgenda = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.LOCAL);
                        parametroagenda.Cnes = ddlUnidade.SelectedValue;
                        parametroagenda.Percentual = int.Parse(tbxPct_Local.Text);
                        parametroagenda.TipoConfiguracao = ParametroAgenda.CONFIGURACAO_PROCEDIMENTO;
                        parametroagenda.Procedimento = procedimento;
                        parametroagenda.Cbo = cbo;
                        parametroagenda.SubGrupo = iParametro.BuscarPorCodigo<SubGrupo>(int.Parse(ddlSubGrupo.SelectedValue));
                        if (parametroAgendaUnidade != null)
                            parametroagenda.ParametroAgendaUnidade = parametroAgendaUnidade;
                        iParametro.Salvar(parametroagenda);
                        Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 16, parametroagenda.Codigo.ToString()));

                        //Percentual Distrital            
                        parametroagenda = new ParametroAgenda();
                        parametroagenda.TipoAgenda = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.DISTRITAL);
                        parametroagenda.Cnes = ddlUnidade.SelectedValue;
                        parametroagenda.Percentual = int.Parse(tbxPct_Distrital.Text);
                        parametroagenda.TipoConfiguracao = ParametroAgenda.CONFIGURACAO_PROCEDIMENTO;
                        parametroagenda.Procedimento = procedimento;
                        parametroagenda.Cbo = cbo;
                        parametroagenda.SubGrupo = iParametro.BuscarPorCodigo<SubGrupo>(int.Parse(ddlSubGrupo.SelectedValue));
                        if (parametroAgendaUnidade != null)
                            parametroagenda.ParametroAgendaUnidade = parametroAgendaUnidade;
                        iParametro.Salvar(parametroagenda);
                        Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 16, parametroagenda.Codigo.ToString()));

                        //Percentual Rede
                        parametroagenda = new ParametroAgenda();
                        parametroagenda.TipoAgenda = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.REDE);
                        parametroagenda.Cnes = ddlUnidade.SelectedValue;
                        parametroagenda.Percentual = int.Parse(tbxPct_Rede.Text);
                        parametroagenda.TipoConfiguracao = ParametroAgenda.CONFIGURACAO_PROCEDIMENTO;
                        parametroagenda.Procedimento = procedimento;
                        parametroagenda.Cbo = cbo;
                        parametroagenda.SubGrupo = iParametro.BuscarPorCodigo<SubGrupo>(int.Parse(ddlSubGrupo.SelectedValue));
                        if (parametroAgendaUnidade != null)
                            parametroagenda.ParametroAgendaUnidade = parametroAgendaUnidade;
                        iParametro.Salvar(parametroagenda);
                        Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 16, parametroagenda.Codigo.ToString()));

                        //Percentual Reserva Técnica
                        parametroagenda = new ParametroAgenda();
                        parametroagenda.TipoAgenda = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.RESERVA_TECNICA);
                        parametroagenda.Cnes = ddlUnidade.SelectedValue;
                        parametroagenda.Percentual = int.Parse(tbxPct_Reserva.Text);
                        parametroagenda.TipoConfiguracao = ParametroAgenda.CONFIGURACAO_PROCEDIMENTO;
                        parametroagenda.Procedimento = procedimento;
                        parametroagenda.Cbo = cbo;
                        parametroagenda.SubGrupo = iParametro.BuscarPorCodigo<SubGrupo>(int.Parse(ddlSubGrupo.SelectedValue));
                        if (parametroAgendaUnidade != null)
                            parametroagenda.ParametroAgendaUnidade = parametroAgendaUnidade;
                        iParametro.Salvar(parametroagenda);
                        Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 16, parametroagenda.Codigo.ToString()));
                    }
                }
            }
            //btnPesquisar_OnClick(new object(), new EventArgs());
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');window.location='FormParametroAgenda.aspx';", true);
            //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados salvos com sucesso!');window.location='FormParametroAgenda.aspx'</script>");
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
                ddlProcedimento.Items.Clear();
                ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo));
                //Session["ProcedimentoSelecionado"] = procedimento;
                GridViewListaProcedimento.SelectedRowStyle.BackColor = Color.LightGray;
                PanelPesquisaProcedimento.Visible = false;

                // Busca os CBOs do Procedimento Selecionado
                IList<ViverMais.Model.CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(procedimento.Codigo);

                ddlCBO.Items.Clear();
                ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
                ddlSubGrupo.Items.Clear();
                ddlSubGrupo.Items.Add(new ListItem("Selecione...", "0"));
                //Carrega todas as especialidades
                foreach (CBO cbo in cbos)
                    ddlCBO.Items.Add(new ListItem(cbo.Nome, cbo.Codigo.ToString()));
                ddlCBO.Focus();
            }
        }

        protected void ddlCBO_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubGrupo.Items.Clear();
            //
            ddlSubGrupo.DataSource = Factory.GetInstance<ISubGrupoProcedimentoCbo>().ListarSubGrupoPorProcedimentoECbo<SubGrupo>(ddlProcedimento.SelectedValue, ddlCBO.SelectedValue, true);
            ddlSubGrupo.DataBind();
            ddlSubGrupo.Items.Insert(0, (new ListItem("Selecione...", "0")));
            ddlSubGrupo.Focus();
            //foreach (SubGrupo subGrupo in subGrupos)
            //    ddlSubGrupo.Items.Add(new ListItem(subGrupo.NomeSubGrupo, subGrupo.Codigo.ToString()));
        }

        protected void lnkBtnModificarProcedimento_Click(object sender, EventArgs e)
        {
            ddlProcedimento.Items.Clear();
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            ddlCBO.Items.Clear();
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
            PanelPesquisaProcedimento.Visible = true;
            PanelProcedimento.Visible = true;
        }

        protected void rbtTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtTipo.SelectedValue == "U")
            {
                PanelPesquisaProcedimento.Visible = false;
                PanelProcedimento.Visible = false;
                PanelVisualizarTodasAgendas.Visible = true;
                //  ddlProcedimento.Enabled = true;
            }
            else
            {
                PanelPesquisaProcedimento.Visible = true;
                PanelProcedimento.Visible = true;
                PanelVisualizarTodasAgendas.Visible = false;
                //  ddlProcedimento.Enabled = false;
            }
            tbxPct_Local.Text = String.Empty;
            tbxPct_Distrital.Text = String.Empty;
            tbxPct_Rede.Text = String.Empty;
            tbxPct_Reserva.Text = String.Empty;
            GridViewParametros.DataSource = null;
            GridViewParametros.DataBind();
        }

        protected void GridViewParametros_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dataTable = GridViewParametros.DataSource as DataTable;
            if (dataTable != null)
            {
                DataView dataView = new DataView(dataTable);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

                GridViewParametros.DataSource = dataView;
                GridViewParametros.DataBind();
            }
        }

        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }
    }
}
