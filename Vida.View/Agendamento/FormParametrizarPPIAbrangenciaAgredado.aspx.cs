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
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agregado.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Agregado;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using System.Globalization;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using System.Drawing;
using ViverMais.ServiceFacade.ServiceFacades.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;

namespace ViverMais.View.Agendamento
{
    public partial class FormParametrizarPPIAbrangenciaAgredado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PARAMETRIZACAO_PARAMETRIZAR_PPI_ABRANG", Modulo.AGENDAMENTO))
                {
                    GridViewPactosAtivos.PageIndexChanging += new GridViewPageEventHandler(GridViewPactosAtivos_PageIndexChanging);
                    GridViewPactosInativos.PageIndexChanging += new GridViewPageEventHandler(GridViewPactosInativos_PageIndexChanging);
                    //PanelAgregadosSelecionados.Visible = false;
                    //lblSemAgregado.Visible = false;
                    IGrupoAgregado iGrupoAgregado = Factory.GetInstance<IGrupoAgregado>();
                    IList<GrupoAbrangencia> gruposAbrangencia = Factory.GetInstance<IGrupoAbrangencia>().ListarGruposAtivos<GrupoAbrangencia>();
                    ddlGrupos.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (GrupoAbrangencia grupoAbrangencia in gruposAbrangencia)
                    {
                        ddlGrupos.Items.Add(new ListItem(grupoAbrangencia.NomeGrupo, grupoAbrangencia.Codigo));
                    }

                    //IList<GrupoAgregado> grupoAgregados = iGrupoAgregado.ListarTodos<GrupoAgregado>("Nome", true);
                    //ddlGrupoAgregados.Items.Add(new ListItem("Selecione", "0"));
                    //foreach (GrupoAgregado grupoAgregado in grupoAgregados)
                    //{
                    //    ddlGrupoAgregados.Items.Add(new ListItem(grupoAgregado.Nome, grupoAgregado.Codigo));
                    //}
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void GridViewPactosInativos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable table = (DataTable)Session["PactosInativos"];
            GridViewPactosInativos.DataSource = table;
            GridViewPactosInativos.PageIndex = e.NewPageIndex;
            GridViewPactosInativos.DataBind();
        }

        protected void GridViewPactosAtivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable table = (DataTable)Session["PactosAtivos"];
            GridViewPactosAtivos.DataSource = table;
            GridViewPactosAtivos.PageIndex = e.NewPageIndex;
            GridViewPactosAtivos.DataBind();
        }

        /// <summary>
        /// Salva os Dados do Pacto para o Município Selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnSalvar_Click(object sender, EventArgs e)
        //{
        //    PactoAbrangenciaAgregado pactoAbrangenciaAgregado;
        //    PactoAbrangencia pactoabrangencia = Factory.GetInstance<IPactoAbrangencia>().BuscarPactoPorGrupoAbrangencia<PactoAbrangencia>(ddlGrupos.SelectedValue);
        //    GrupoAbrangencia grupo = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<GrupoAbrangencia>(ddlGrupos.SelectedValue);
        //    if (pactoabrangencia == null)//Se Não Existir Pacto Com Aquele Município ele gera um Novo Pacto
        //    {
        //        //Salva a Lista de Agregados que está na Sessão
        //        DataTable table = (DataTable)Session["AgregadoSelecionado"];
        //        if (table.Rows.Count != 0)
        //        {
        //            pactoabrangencia = new PactoAbrangencia();
        //            pactoabrangencia.Grupo = grupo;
        //            Factory.GetInstance<IPactoAbrangencia>().Inserir(pactoabrangencia);
        //            foreach (DataRow row in table.Rows)
        //            {
        //                pactoAbrangenciaAgregado = new PactoAbrangenciaAgregado();
        //                //pactoAbrangenciaAgregado.BloqueiaCota = ((bool)row["BloqueiaPorCota"]) == true ? 1 : 0;
        //                pactoAbrangenciaAgregado.PactoAbrangencia = pactoabrangencia;
        //                //pactoAbrangenciaAgregado.Percentual = int.Parse(row["Percentual"].ToString());
        //                pactoAbrangenciaAgregado.ValorPactuado = long.Parse(row["ValorPactuado"].ToString().Replace(",", "").Replace(".", ""));
        //                string tipoPacto = row["TipoPacto"].ToString();
        //                pactoAbrangenciaAgregado.TipoPacto = char.Parse(tipoPacto);
        //                switch (tipoPacto)
        //                {
        //                    case "P":
        //                        pactoAbrangenciaAgregado.Procedimento.Codigo = row["CodigoProcedimento"].ToString();
        //                        break;
        //                    case "C":
        //                        pactoAbrangenciaAgregado.Procedimento.Codigo = row["CodigoProcedimento"].ToString();
        //                        pactoAbrangenciaAgregado.Cbo.Codigo = row["CodigoCBO"].ToString();
        //                        break;
        //                }
        //                Factory.GetInstance<IPactoAbrangenciaAgregado>().Inserir(pactoAbrangenciaAgregado);
        //            }
        //            Factory.GetInstance<IPactoAbrangencia>().Salvar(pactoabrangencia);
        //        }
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');window.location='FormParametrizarPPIAgredado.aspx';", true);
        //        return;
        //    }
        //    else//Em Caso de Atualização
        //    {
        //        //Pega a Lista de Agregados que está na Sessão
        //        DataTable table = (DataTable)Session["AgregadoSelecionado"];
        //        if (table.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in table.Rows)
        //            {
        //                pactoAbrangenciaAgregado = new PactoAbrangenciaAgregado();
        //                //pactoAbrangenciaAgregado.BloqueiaCota = ((bool)row["BloqueiaPorCota"]) == true ? 1 : 0;
        //                pactoAbrangenciaAgregado.PactoAbrangencia = pactoabrangencia;
        //                //pactoAbrangenciaAgregado.Percentual = int.Parse(row["Percentual"].ToString());
        //                pactoAbrangenciaAgregado.ValorPactuado = long.Parse(row["ValorPactuado"].ToString().Replace(",", "").Replace(".", ""));
        //                pactoAbrangenciaAgregado.ValorRestante = pactoAbrangenciaAgregado.ValorPactuado;
        //                string tipoPacto = row["TipoPacto"].ToString();
        //                pactoAbrangenciaAgregado.TipoPacto = char.Parse(tipoPacto);
        //                switch (tipoPacto)
        //                {
        //                    case "P":
        //                        pactoAbrangenciaAgregado.Procedimento.Codigo = row["CodigoProcedimento"].ToString();
        //                        break;
        //                    case "C":
        //                        pactoAbrangenciaAgregado.Procedimento.Codigo = row["CodigoProcedimento"].ToString();
        //                        pactoAbrangenciaAgregado.Cbo.Codigo = row["CodigoCBO"].ToString();
        //                        break;
        //                }
        //                Factory.GetInstance<IPactoAbrangenciaAgregado>().Inserir(pactoAbrangenciaAgregado);

        //            }

        //        }
        //        else
        //        {
        //            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Informe um Agregado para o Município Selecionado!');</script>");
        //            return;
        //        }
        //    }
        //    Session["AgregadoSelecionado"] = null;
        //    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados Salvos com Sucesso!');window.location='FormParametrizarPPIAgredado.aspx'</script>");
        //    return;
        //}

        protected void rbtlTipoPacto_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.AGREGADO).ToString())
            //{
            //    PanelProcedimento.Visible = false;
            //    PanelCBO.Visible = false;
            //}

            //if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.PROCEDIMENTO).ToString())
            //{
            //    PanelProcedimento.Visible = true;
            //    PanelCBO.Visible = false;
            //    //CarregaProcedimentos();
            //}
            //if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.CBO).ToString())
            //{
            //    ///CarregaProcedimentos();
            //    PanelProcedimento.Visible = true;
            //    PanelCBO.Visible = true;
            //}
        }

        //void CarregaCBOs(string id_procedimento)
        //{
        //    IList<CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<CBO>(id_procedimento);
        //    ddlCBO.Items.Clear();
        //    ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
        //    foreach (CBO cbo in cbos)
        //        ddlCBO.Items.Add(new ListItem(cbo.Nome, cbo.Codigo));
        //    ddlCBO.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        //}

        //protected void btnAddAgregado_Click(object sender, EventArgs e)
        //{
        //    if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.AGREGADO).ToString())
        //        IncluiAgregadoNaTabelaAgregadoSelecionado(ddlAgregados.SelectedValue, (DataTable)Session["AgregadoSelecionado"], tbxValorPacto.Text);
        //    //if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.PROCEDIMENTO).ToString())
        //    //    IncluiPactoProcedimentoNaListaDeSelecionados(ddlAgregados.SelectedValue, ddlProcedimento.SelectedValue, (DataTable)Session["AgregadoSelecionado"], tbxValorPacto.Text, Convert.ToInt32(PactoAgregadoProcedCBO.BloqueadoPorCota.SIM), "");
        //    //if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.CBO).ToString())
        //    //    IncluiPactoCBONaListaDeSelecionados(ddlAgregados.SelectedValue, ddlProcedimento.SelectedValue, ddlCBO.SelectedValue, (DataTable)Session["AgregadoSelecionado"], tbxValorPacto.Text, Convert.ToInt32(PactoAgregadoProcedCBO.BloqueadoPorCota.SIM), "");

        //    lblSemAgregado.Visible = false;
        //    //DataTable table = (DataTable)Session["AgregadoSelecionado"];
        //    //GridViewPactosAtivos.DataSource = table;
        //    //GridViewPactosAtivos.DataBind();
        //    //GridViewAgregadosSelecionados.DataSource = table;
        //    //GridViewAgregadosSelecionados.DataBind();
        //    //PanelAgregadosSelecionados.Visible = true;
        //}

        //protected void ddlGrupoAgregados_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ISubGrupoAgregado iSubGrupoAgregado = Factory.GetInstance<ISubGrupoAgregado>();
        //    IList<SubGrupoAgregado> subGrupoAgregados = iSubGrupoAgregado.BuscaPorGrupo<SubGrupoAgregado>(ddlGrupoAgregados.SelectedValue);
        //    ddlSubGrupoAgregado.Items.Clear();
        //    ddlAgregados.Items.Clear();
        //    ddlSubGrupoAgregado.Items.Add(new ListItem("Selecione", "0"));
        //    ddlAgregados.Items.Add(new ListItem("Selecione", "0"));
        //    subGrupoAgregados = subGrupoAgregados.Distinct(new GenericComparer<SubGrupoAgregado>("Codigo")).ToList();
        //    foreach (SubGrupoAgregado subGrupoAgregado in subGrupoAgregados)
        //    {
        //        ddlSubGrupoAgregado.Items.Add(new ListItem(subGrupoAgregado.Nome, subGrupoAgregado.Codigo));
        //    }

        //}

        //protected void ddlSubGrupoAgregado_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    IAgregado iAgregado = Factory.GetInstance<IAgregado>();
        //    IList<Agregado> agregados = iAgregado.BuscaPorSubGrupo<Agregado>(ddlSubGrupoAgregado.SelectedValue);
        //    ddlAgregados.Items.Clear();
        //    ddlAgregados.Items.Add(new ListItem("Selecione", "0"));
        //    foreach (Agregado agregado in agregados)
        //    {
        //        ddlAgregados.Items.Add(new ListItem(agregado.Nome, agregado.Codigo));
        //    }
        //}

        /// <summary>
        /// Função que inicializa a Tabela que irá Obter a Lista Dos Agregados que foram selecionados pelo usuário
        /// </summary>
        /// <returns></returns>
        private DataTable CriaTabelaAgregadoSelecionado()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Codigo");
            table.Columns.Add("Ano");
            //table.PrimaryKey = new DataColumn[] { table.Columns["Codigo"] };//Define o código do agregado como chave primária, para facilitar a busca e exclusão
            table.Columns.Add("TipoPacto");
            table.Columns.Add("CodigoAgregado");
            table.Columns.Add("NomeAgregado");
            table.Columns.Add("CodigoProcedimento");
            table.Columns.Add("Procedimento");
            table.Columns.Add("CodigoCBO");
            table.Columns.Add("CBO");
            table.Columns.Add("ValorPactuado");
            table.Columns.Add("BloqueiaPorCota", typeof(bool));
            table.Columns.Add("Percentual");
            //Session["AgregadoSelecionado"] = table;
            return table;
        }

        /// <summary>
        /// Inclui um Pacto do Tipo CBO - Não Mais usado
        /// </summary>
        /// <param name="id_agregado"></param>
        /// <param name="id_procedimento"></param>
        /// <param name="id_cbo"></param>
        /// <param name="table"></param>
        /// <param name="valor"></param>
        /// <param name="bloqueioCota"></param>
        /// <param name="percentual"></param>
        //void IncluiPactoCBONaListaDeSelecionados(string id_agregado, string id_procedimento, string id_cbo, DataTable table, string valor, int bloqueioCota, string percentual)
        //{
        //    if (!VerificaSeExistePactoNaLista(rbtlTipoPacto.SelectedValue, id_agregado, id_procedimento, id_cbo))
        //    {
        //        PactoAbrangencia pactoabrangencia = Factory.GetInstance<IPactoAbrangencia>().BuscarPactoPorGrupoAbrangencia<PactoAbrangencia>(ddlGrupos.SelectedValue);
        //        if (pactoabrangencia == null)
        //        {
        //            pactoabrangencia = new PactoAbrangencia();
        //            pactoabrangencia.Grupo = Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(ddlGrupos.SelectedValue);
        //            Factory.GetInstance<IPactoAbrangencia>().Salvar(pactoabrangencia);
        //        }

        //        PactoAbrangenciaAgregado pactoAbrangenciaAgregado = new PactoAbrangenciaAgregado();
        //        pactoAbrangenciaAgregado.Agregado = Factory.GetInstance<IAgregado>().BuscarPorCodigo<Agregado>(id_agregado);
        //        pactoAbrangenciaAgregado.Procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(id_procedimento);
        //        pactoAbrangenciaAgregado.Cbo = Factory.GetInstance<ICBO>().BuscarPorCodigo<CBO>(id_cbo);
        //        pactoAbrangenciaAgregado.Ativo = true;
        //        //pactoAbrangenciaAgregado.BloqueiaCota = bloqueioCota;
        //        //pactoAbrangenciaAgregado.Percentual = percentual == "" ? 0 : int.Parse(percentual);
        //        pactoAbrangenciaAgregado.TipoPacto = Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.CBO);
        //        pactoAbrangenciaAgregado.ValorPactuado = long.Parse(valor.Replace(",", "").Replace(".", ""));
        //        pactoAbrangenciaAgregado.ValorRestante = pactoAbrangenciaAgregado.ValorPactuado;
        //        pactoAbrangenciaAgregado.PactoAbrangencia = pactoabrangencia;
        //        pactoAbrangenciaAgregado.DataPacto = DateTime.Now;
        //        pactoAbrangenciaAgregado.Usuario = (Usuario)Session["Usuario"];
        //        Factory.GetInstance<IViverMaisServiceFacade>().Inserir(pactoAbrangenciaAgregado);
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro Incluido com Sucesso!');", true);

        //    }
        //    CarregaPactosAtivosDoGrupo(ddlGrupos.SelectedValue);
        //}

        /// <summary>
        /// Inclui um Pacto do Tipo Procedimento - Não Mais usado 
        /// </summary>
        /// <param name="id_agregado"></param>
        /// <param name="id_procedimento"></param>
        /// <param name="table"></param>
        /// <param name="valor"></param>
        /// <param name="bloqueioCota"></param>
        /// <param name="percentual"></param>
        //void IncluiPactoProcedimentoNaListaDeSelecionados(string id_agregado, string id_procedimento, DataTable table, string valor, int bloqueioCota, string percentual)
        //{
        //    if (!VerificaSeExistePactoNaLista(rbtlTipoPacto.SelectedValue, id_agregado, id_procedimento, ""))
        //    {
        //        PactoAbrangencia pactoabrangencia = Factory.GetInstance<IPactoAbrangencia>().BuscarPactoPorGrupoAbrangencia<PactoAbrangencia>(ddlGrupos.SelectedValue);
        //        if (pactoabrangencia == null)
        //        {
        //            pactoabrangencia = new PactoAbrangencia();
        //            pactoabrangencia.Grupo = Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(ddlGrupos.SelectedValue);
        //            Factory.GetInstance<IPactoAbrangencia>().Inserir(pactoabrangencia);
        //        }

        //        PactoAbrangenciaAgregado pactoAbrangenciaAgregado = new PactoAbrangenciaAgregado();
        //        pactoAbrangenciaAgregado.Agregado = Factory.GetInstance<IAgregado>().BuscarPorCodigo<Agregado>(id_agregado);
        //        pactoAbrangenciaAgregado.Procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(id_procedimento);
        //        pactoAbrangenciaAgregado.Ativo = true;
        //        //pactoAbrangenciaAgregado.BloqueiaCota = bloqueioCota;
        //        //pactoAbrangenciaAgregado.Percentual = percentual == "" ? 0 : int.Parse(percentual);
        //        pactoAbrangenciaAgregado.TipoPacto = Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO);
        //        pactoAbrangenciaAgregado.ValorPactuado = long.Parse(valor.Replace(",", "").Replace(".", ""));
        //        pactoAbrangenciaAgregado.ValorRestante = pactoAbrangenciaAgregado.ValorPactuado;
        //        pactoAbrangenciaAgregado.PactoAbrangencia = pactoabrangencia;
        //        pactoAbrangenciaAgregado.DataPacto = DateTime.Now;
        //        pactoAbrangenciaAgregado.Usuario = (Usuario)Session["Usuario"];
        //        Factory.GetInstance<IViverMaisServiceFacade>().Inserir(pactoAbrangenciaAgregado);
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro Incluido com Sucesso.');", true);
        //    }
        //    CarregaPactosAtivosDoGrupo(ddlGrupos.SelectedValue);

        //}

        /// <summary>
        /// Esse método é utilizado para Incluir um Pacto do Tipo AGREGADO
        /// </summary>
        /// <param name="id_agregado"></param>
        /// <param name="table"></param>
        //private void IncluiAgregadoNaTabelaAgregadoSelecionado(string id_agregado, DataTable table, string valor)
        //{
        //    ////Verifica pela chave primária (Codigo) se o Item Selecionado já existe no DataTable de Agregados Selecionados
        //    //if (table.Rows.Find(id_agregado) == null)//Se não existir ele adiciona
        //    //{
        //    if (!VerificaSeExistePactoNaLista(rbtlTipoPacto.SelectedValue, id_agregado, "", ""))
        //    {
        //        PactoAbrangencia pactoabrangencia = Factory.GetInstance<IPactoAbrangencia>().BuscarPactoPorGrupoAbrangencia<PactoAbrangencia>(ddlGrupos.SelectedValue);
        //        if (pactoabrangencia == null)
        //        {
        //            pactoabrangencia = new PactoAbrangencia();
        //            pactoabrangencia.Grupo = Factory.GetInstance<IGrupoAbrangencia>().BuscarPorCodigo<GrupoAbrangencia>(ddlGrupos.SelectedValue);
        //            Factory.GetInstance<IPactoAbrangencia>().Salvar(pactoabrangencia);
        //        }

        //        PactoAbrangenciaAgregado pactoAbrangenciaAgregado = new PactoAbrangenciaAgregado();
        //        pactoAbrangenciaAgregado.Agregado = Factory.GetInstance<IAgregado>().BuscarPorCodigo<Agregado>(id_agregado);
        //        pactoAbrangenciaAgregado.Ativo = true;
        //        pactoAbrangenciaAgregado.TipoPacto = Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO);
        //        pactoAbrangenciaAgregado.ValorPactuado = float.Parse(valor.Replace("R$", ""));
        //        //pactoAbrangenciaAgregado.ValorRestante = pactoAbrangenciaAgregado.ValorPactuado;
        //        pactoAbrangenciaAgregado.PactoAbrangencia = pactoabrangencia;
        //        Factory.GetInstance<IViverMaisServiceFacade>().Inserir(pactoAbrangenciaAgregado);
        //        CarregaPactosAtivosDoGrupo(ddlGrupos.SelectedValue);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já Existe um Pacto do Tipo AGREGADO para o agregado Selecionado. Não Foi Possível Incluir.');", true);
        //        return;
        //    }

        //}

        protected void ddlGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CriaTabelaAgregadoSelecionado();
            CarregaPactosAtivosDoGrupo(ddlGrupos.SelectedValue);
            CarregaPactosInativosDoGrupo(ddlGrupos.SelectedValue);
        }

        //void CarregaProcedimentos()
        //{
        //    ddlProcedimento.Items.Clear();
        //    IList<Procedimento> procedimentos = Factory.GetInstance<IProcedimentoAgregado>().ListarProcedimentosPorAgregado<Procedimento>(ddlAgregados.SelectedValue);
        //    if (procedimentos.Count != 0)
        //    {
        //        ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
        //        foreach (Procedimento procedimento in procedimentos)
        //            ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo));
        //    }
        //}
        //protected void ddlProcedimento_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (rbtlTipoPacto.SelectedValue == "C")//Se o Tipo de Pacto Não seja Por CBO, não é necessário Listar os CBOS
        //        CarregaCBOs(ddlProcedimento.SelectedValue);
        //}

        private DataTable CriaTabelaPactosInativos()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Ano");
            table.Columns.Add("TipoPacto");
            table.Columns.Add("Codigo");
            table.Columns.Add("Agregado");
            table.Columns.Add("Procedimento");
            table.Columns.Add("CBO");
            table.Columns.Add("ValorPactuado");
            table.Columns.Add("BloqueiaPorCota", typeof(bool));
            table.Columns.Add("Percentual");
            table.Columns.Add("DataOperacao");
            table.Columns.Add("Operador");
            return table;
        }

        void CarregaPactosInativosDoGrupo(string id_grupo)
        {
            PactoAbrangencia pactoabrangencia = Factory.GetInstance<IPactoAbrangencia>().BuscarPactoPorGrupoAbrangencia<PactoAbrangencia>(ddlGrupos.SelectedValue);
            if (pactoabrangencia != null)
            {
                IList<PactoAbrangenciaAgregado> pactosInativos = Factory.GetInstance<IPactoAbrangenciaAgregado>().BuscaPorPactoAbrangencia<PactoAbrangenciaAgregado>(pactoabrangencia.Codigo).Where(p => p.Ativo == false).ToList();
                if (pactosInativos.Count != 0)
                {
                    lblSemRegistroInativo.Visible = false;
                    DataTable table = CriaTabelaPactosInativos();
                    foreach (PactoAbrangenciaAgregado pactoInativo in pactosInativos)
                    {
                        DataRow row = table.NewRow();
                        switch (pactoInativo.TipoPacto.ToString())
                        {
                            case "A": row["TipoPacto"] = "AGREGADO";
                                break;
                            case "P": row["TipoPacto"] = "PROCEDIMENTO";
                                break;
                            case "C": row["TipoPacto"] = "CBO";
                                break;
                        }
                        row["Ano"] = pactoInativo.Ano.ToString("0000");
                        row["Agregado"] = pactoInativo.Agregado.Nome;
                        row["Procedimento"] = pactoInativo.Procedimento == null ? "" : pactoInativo.Procedimento.Nome;
                        row["CBO"] = pactoInativo.Cbo == null ? "" : pactoInativo.Cbo.Nome;
                        row["ValorPactuado"] = pactoInativo.ValorPactuado.ToString("C2");
                        //String.Format("{0:C2}", Math.Round(pactoInativo.ValorPactuado, 2));

                        //double valor = double.Parse(pactoInativo.ValorPactuado.ToString().Insert(pactoInativo.ValorPactuado.ToString().Length -2,","));
                        //row["ValorPactuado"] = valor.ToString("C",new CultureInfo("pt-BR"));
                        //row["BloqueiaPorCota"] = pactoInativo.BloqueiaCota == 1 ? true : false;
                        //row["Percentual"] = pactoInativo.Percentual.ToString();
                        row["DataOperacao"] = pactoInativo.DataUltimaOperacao != DateTime.MinValue ? pactoInativo.DataUltimaOperacao.ToString("dd/MM/yyyy") : string.Empty;
                        row["Operador"] = pactoInativo.Usuario != null ? pactoInativo.Usuario.Login : string.Empty;
                        table.Rows.Add(row);
                    }
                    Session["PactosInativos"] = table;
                    GridViewPactosInativos.DataSource = table;
                    GridViewPactosInativos.DataBind();
                }
                else
                {
                    lblSemRegistroInativo.Visible = true;
                }
            }
            else
            {
                lblSemRegistroInativo.Visible = true;
            }
        }
        void CarregaPactosAtivosDoGrupo(string id_grupo)
        {
            PactoAbrangencia pactoabrangencia = Factory.GetInstance<IPactoAbrangencia>().BuscarPactoPorGrupoAbrangencia<PactoAbrangencia>(ddlGrupos.SelectedValue);
            if (pactoabrangencia != null)
            {
                IList<PactoAbrangenciaAgregado> pactosAtivos = Factory.GetInstance<IPactoAbrangenciaAgregado>().BuscaPorPactoAbrangencia<PactoAbrangenciaAgregado>(pactoabrangencia.Codigo).Where(p => p.Ativo == true).ToList();
                if (pactosAtivos.Count != 0)
                {
                    //lblSemAgregado.Visible = false;
                    lblSemRegistroAtivo.Visible = false;
                    DataTable table = CriaTabelaAgregadoSelecionado();
                    foreach (PactoAbrangenciaAgregado pactoAtivo in pactosAtivos)
                    {
                        DataRow row = table.NewRow();
                        row["Codigo"] = pactoAtivo.Codigo.ToString();
                        switch (pactoAtivo.TipoPacto.ToString())
                        {
                            case "A": row["TipoPacto"] = "AGREGADO";
                                break;
                            case "P": row["TipoPacto"] = "PROCEDIMENTO";
                                break;
                            case "C": row["TipoPacto"] = "CBO";
                                break;
                        }
                        row["Ano"] = pactoAtivo.Ano.ToString("0000");
                        row["CodigoAgregado"] = pactoAtivo.Agregado.Codigo;
                        row["NomeAgregado"] = pactoAtivo.Agregado.Nome;
                        if (pactoAtivo.Procedimento != null)
                        {
                            row["CodigoProcedimento"] = pactoAtivo.Procedimento.Codigo;
                            row["Procedimento"] = pactoAtivo.Procedimento.Nome;
                        }
                        else
                        {
                            row["CodigoProcedimento"] = string.Empty;
                            row["Procedimento"] = string.Empty;
                        }
                        if (pactoAtivo.Cbo != null)
                        {
                            row["CodigoCBO"] = pactoAtivo.Cbo.Codigo;
                            row["CBO"] = pactoAtivo.Cbo.Nome != "" ? pactoAtivo.Cbo.Nome : "";
                        }
                        else
                        {
                            row["CodigoCBO"] = string.Empty;
                            row["CBO"] = string.Empty;
                        }

                        //row["ValorPactuado"] = pactoAtivo.ValorPactuado.ToString("f");
                        //row["ValorPactuado"] = String.Format("{0:C2}", Math.Round(pactoAtivo.ValorPactuado, 2));
                        row["ValorPactuado"] = pactoAtivo.ValorPactuado.ToString("C2");
                        table.Rows.Add(row);
                    }
                    Session["PactosAtivos"] = table;
                    GridViewPactosAtivos.DataSource = table;
                    GridViewPactosAtivos.DataBind();
                }
                else
                {
                    GridViewPactosAtivos.DataSource = null;
                    GridViewPactosAtivos.DataBind();
                    //lblSemAgregado.Visible = true;
                    lblSemRegistroAtivo.Visible = true;
                    //PanelAgregadosSelecionados.Visible = false;
                }
            }
            else
            {
                GridViewPactosAtivos.DataSource = null;
                GridViewPactosAtivos.DataBind();
                //PanelAgregadosSelecionados.Visible = false;
                lblSemRegistroAtivo.Visible = true;
            }
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            PanelDadosDoPacto.Visible = false;
            ddlGrupos_SelectedIndexChanged(new object(), new EventArgs());
            Session.Remove("PactoAbrangenciaGrupoMunicipio");
            Session.Remove("PactoAbrangenciaAgregado");

        }

        //public bool VerificaSeExistePactoNaLista(string tipoPactoAVerificar, string id_agregado, string id_procedimento, string id_cbo)
        //{
        //    foreach (GridViewRow row in GridViewPactosAtivos.Rows)
        //    {
        //        string tipoPactoDoGrid = row.Cells[1].Text.Substring(0, 1).ToUpper();

        //        if (tipoPactoAVerificar == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.AGREGADO).ToString())
        //        {
        //            //Verifico Se é o mesmo tipo de Pacto
        //            if (id_agregado == row.Cells[2].Text)
        //            {
        //                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não Foi Possível Incluir o Pacto. Verifique se já existe um pacto para este Agregado.');", true);
        //                return true;
        //            }
        //        }
        //        //else if (tipoPactoAVerificar == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO).ToString())
        //        //{
        //        //    //Verifico se é o mesmo Agregado
        //        //    if ((id_agregado == row.Cells[2].Text) && (tipoPactoDoGrid == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO).ToString()))
        //        //    {
        //        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Existe Um Pacto do Tipo AGREGADO Para o Agregado Selecionado. Não Foi Possível Incluir!');", true);
        //        //        return true;
        //        //    }
        //        //    else
        //        //    {
        //        //        if ((id_agregado == row.Cells[2].Text) && (id_procedimento == row.Cells[4].Text))
        //        //        {
        //        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Existe Um Pacto do Tipo PROCEDIMENTO Para o Procedimento Selecionado. Não Foi Possível Incluir!');", true);
        //        //            return true;
        //        //        }
        //        //    }
        //        //    //}
        //        //}
        //        //else if (tipoPactoAVerificar == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO).ToString())
        //        //{
        //        //    if ((id_agregado == row.Cells[2].Text) && (tipoPactoDoGrid == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO).ToString()))
        //        //    {
        //        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Existe Um Pacto do Tipo AGREGADO Para o Agregado Selecionado. Não Foi Possível Incluir!');", true);
        //        //        return true;
        //        //    }
        //        //    else
        //        //    {
        //        //        //Verifico Se é o mesmo Procedimento
        //        //        if ((id_procedimento != row.Cells[4].Text) && (tipoPactoDoGrid == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO).ToString()))
        //        //        {
        //        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Existe Um Pacto do Tipo PROCEDIMENTO Para o Procedimento Selecionado. Não Foi Possível Incluir!');", true);
        //        //            return true;
        //        //        }
        //        //        else
        //        //        {
        //        //            //Verifico Se é o mesmo CBO
        //        //            if ((id_agregado == row.Cells[2].Text) && (id_procedimento != row.Cells[4].Text) && (id_cbo == row.Cells[6].Text))
        //        //            {
        //        //                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Existe Um Pacto do Tipo CBO Para o Cbo Selecionado. Não Foi Possível Incluir!');", true);
        //        //                return true;
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //    }
        //    return false;
        //}

        protected void GridViewPactosAtivos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewPactosAtivos.EditIndex = -1;
            Session["IndexSelecionado"] = null;
            CarregaPactosInativosDoGrupo(ddlGrupos.SelectedValue);
            CarregaPactosAtivosDoGrupo(ddlGrupos.SelectedValue);
        }

        //protected void ddlMunicipios_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    IList<PactoAbrangenciaGrupoMunicipio> pactoAbrangenciaGrupoMunicipio = (IList<PactoAbrangenciaGrupoMunicipio>)Session["PactoAbrangenciaGrupoMunicipio"];
        //    PactoAbrangenciaAgregado pactoAbrangenciaAgregado = (PactoAbrangenciaAgregado)Session["PactoAbrangenciaAgregado"];
        //    //float teste = pactoAbrangenciaGrupoMunicipio.Where(p => p.Municipio.Codigo == ddlMunicipios.SelectedValue && p.GrupoAbrangencia.Codigo == ddlGrupos.SelectedValue && p.PactoAbrangenciaAgregado.Codigo == pactoAbrangenciaAgregado.Codigo).FirstOrDefault().CotaFinanceira;
        //    tbxCotaFinanceira.Text = pactoAbrangenciaGrupoMunicipio.Where(p => p.Municipio.Codigo == ddlMunicipios.SelectedValue && p.GrupoAbrangencia.Codigo == ddlGrupos.SelectedValue && p.PactoAbrangenciaAgregado.Codigo == pactoAbrangenciaAgregado.Codigo).FirstOrDefault().CotaFinanceira.ToString("C", new CultureInfo("pt-BR"));
        //    tbxCotaFinanceira.Enabled = true;
        //}

        //protected void btnSalvarCota_OnClick(object sender, EventArgs e)
        //{
        //    if (ValidaSomatorioCota())
        //    {
        //        IList<PactoAbrangenciaGrupoMunicipio> pactosAbrangenciaGrupoMunicipio = (IList<PactoAbrangenciaGrupoMunicipio>)Session["PactoAbrangenciaGrupoMunicipio"];
        //        PactoAbrangenciaAgregado pactoAbrangenciaAgregado = (PactoAbrangenciaAgregado)Session["PactoAbrangenciaAgregado"];
        //        PactoAbrangenciaGrupoMunicipio pactoAbrangenciaGrupoMunicipio = pactosAbrangenciaGrupoMunicipio.Where(p => p.Municipio.Codigo == ddlMunicipios.SelectedValue && p.GrupoAbrangencia.Codigo == ddlGrupos.SelectedValue && p.PactoAbrangenciaAgregado.Codigo == pactoAbrangenciaAgregado.Codigo).FirstOrDefault();
        //        float valorCotaAlterada = float.Parse(tbxCotaFinanceira.Text.Trim().Replace("R$", string.Empty));
        //        pactoAbrangenciaGrupoMunicipio.CotaFinanceira = Decimal.Parse(valorCotaAlterada.ToString("C").Replace("R$", ""));
        //        //pactoAbrangenciaGrupoMunicipio.ValorUtilizado = Decimal.Parse(valorCotaAlterada.ToString("C").Replace("R$", ""));
        //        Factory.GetInstance<IPactoAbrangenciaAgregado>().Salvar(pactoAbrangenciaGrupoMunicipio);
        //        Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 41, "ID_PACTO: " + pactoAbrangenciaGrupoMunicipio.PactoAbrangenciaAgregado.Codigo + "ID_GRUPO: " + pactoAbrangenciaGrupoMunicipio.GrupoAbrangencia.Codigo + " ID_MUNICIPIO: " + pactoAbrangenciaGrupoMunicipio.Municipio.Codigo));
        //        //Factory.GetInstance<ILogEventos>().Salvar(pactoAbrangenciaGrupoMunicipio);
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
        //        ddlGrupos_SelectedIndexChanged(new object(), new EventArgs());
        //    }
        //}

        bool compare_float(float f1, float f2)
        {
            float precision = 0.01f;
            if (((f1 - precision) < f2) &&
            ((f1 + precision) > f2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //private bool ValidaSomatorioCota()
        //{
        //    IList<PactoAbrangenciaGrupoMunicipio> pactosAbrangenciaGrupoMunicipio = (IList<PactoAbrangenciaGrupoMunicipio>)Session["PactoAbrangenciaGrupoMunicipio"];
        //    PactoAbrangenciaAgregado pactoAbrangenciaAgregado = (PactoAbrangenciaAgregado)Session["PactoAbrangenciaAgregado"];

        //    PactoAbrangenciaGrupoMunicipio pactoAbrangenciaGrupoMunicipio = pactosAbrangenciaGrupoMunicipio.Where(p => p.Municipio.Codigo == ddlMunicipios.SelectedValue && p.GrupoAbrangencia.Codigo == ddlGrupos.SelectedValue && p.PactoAbrangenciaAgregado.Codigo == pactoAbrangenciaAgregado.Codigo).FirstOrDefault();

        //    Decimal tetoFinanceiro = pactoAbrangenciaAgregado.ValorPactuado;

        //    Decimal somatorioSaldoTodosMunicipios = pactosAbrangenciaGrupoMunicipio.Where(p => p.Municipio.Codigo != ddlMunicipios.SelectedValue).ToList().Sum(p => p.CotaFinanceira);
        //    //string valorSoma = somatorioSaldoTodosMunicipios.ToString();

        //    //IList<PactoAbrangenciaGrupoMunicipio> pactos = pactosAbrangenciaGrupoMunicipio.Where(p=>p.Municipio.Codigo != ddlMunicipios.SelectedValue).ToList();

        //    //Decimal soma = 0;
        //    //foreach (PactoAbrangenciaGrupoMunicipio p in pactos)
        //    //    soma += p.CotaFinanceira;

        //    //string[] s2 = soma.ToString("C",new CultureInfo("pt-BR")).Replace("R$","").Split(',');
        //    //string s = s2[0] + "," + s2[1].Substring(0,2);
        //    //float u = float.Parse(soma.ToString("N"));
        //    //float u2 = float.Parse(soma.ToString("C").Replace("R$",""));

        //    //float number = Single.Parse(soma.ToString("N"), new CultureInfo("pt-BR").NumberFormat);


        //    //Decimal a2 = Decimal.Parse(s);
        //    Decimal valorCotaNova = Decimal.Parse(tbxCotaFinanceira.Text.Trim().Replace("R$", string.Empty));

        //    //float valor = float.Parse(soma);

        //    if (somatorioSaldoTodosMunicipios + valorCotaNova <= tetoFinanceiro)
        //        return true;
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O somatórios das Cotas está diferente do Valor Pactuado para o Agregado. Por favor, Verifique!');", true);
        //        return false;
        //    }







        //    //float valorCotaAntiga = pactosAbrangenciaGrupoMunicipio.Where(p => p.Municipio.Codigo == ddlMunicipios.SelectedValue).FirstOrDefault().CotaFinanceira;


        //    //float saldoTemporario = tetoFinanceiro - valorCotaAntiga;

        //    //float saldoAtual = saldoTemporario + valorCotaNova;

        //    //if (!compare_float(saldoAtual,tetoFinanceiro))
        //    //{
        //    //    if (saldoAtual > tetoFinanceiro)
        //    //    {
        //    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O somatórios das Cotas está diferente do Valor Pactuado para o Agregado. Por favor, Verifique!');", true);
        //    //        return false;
        //    //    }
        //    //}
        //    //return true;



        //    //if ((saldoTemporario + valorCotaNova) < tetoFinanceiro || (saldoTemporario + valorCotaNova) == tetoFinanceiro)
        //    //    return true;
        //    //else
        //    //{
        //    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O somatórios das Cotas está diferente do Valor Pactuado para o Agregado. Por favor, Verifique!');", true);
        //    //    return false;
        //    //}

        //    //IList<PactoAbrangenciaGrupoMunicipio> lista2 = pactosAbrangenciaGrupoMunicipio.Where(p => p.Municipio.Codigo != ddlMunicipios.SelectedValue).ToList();
        //    //double somatorioDasCotas = 0d;

        //    //foreach (PactoAbrangenciaGrupoMunicipio iten in lista2)
        //    //    somatorioDasCotas = somatorioDasCotas + double.Parse(iten.CotaFinanceira.ToString("C3").Replace("R$",""));

        //    //float somatorioDasCotas = pactosAbrangenciaGrupoMunicipio.Where(p => p.Municipio.Codigo != ddlMunicipios.SelectedValue).ToList().Sum(p => p.CotaFinanceira);

        //    //if (somatorioDasCotas + valorCotaAlterada != pactoAbrangenciaAgregado.ValorPactuado)
        //    //{
        //    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O somatórios das Cotas está diferente do Valor Pactuado para o Agregado. Por favor, Verifique!');", true);
        //    //    return false;
        //    //}
        //    return false;
        //}

        protected void GridViewPactosAtivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Pega o Índice da Linha Selecionada
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            int index = row.RowIndex;
            int id_PactoAbrangenciaAgregado = int.Parse(GridViewPactosAtivos.Rows[index].Cells[0].Text);
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            PactoAbrangenciaAgregado pactoAbrangenciaAgregado = iViverMais.BuscarPorCodigo<PactoAbrangenciaAgregado>(id_PactoAbrangenciaAgregado);
            GrupoAbrangencia grupoAbrangencia = iViverMais.BuscarPorCodigo<GrupoAbrangencia>(ddlGrupos.SelectedValue);
            if (e.CommandName == "Inativar")
            {
                pactoAbrangenciaAgregado.Ativo = false;
                pactoAbrangenciaAgregado.DataUltimaOperacao = DateTime.Now;
                pactoAbrangenciaAgregado.Usuario = (Usuario)Session["Usuario"];
                Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoAbrangenciaAgregado);
                Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 41, "ID: " + pactoAbrangenciaAgregado.Codigo));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Pacto Inativado com Sucesso!');", true);
                CarregaPactosAtivosDoGrupo(ddlGrupos.SelectedValue);
                CarregaPactosInativosDoGrupo(ddlGrupos.SelectedValue);
            }
            else if (e.CommandName == "Parametrizar")
            {
                if (grupoAbrangencia != null)
                {
                    lblGrupoAbrangência.Text = grupoAbrangencia.NomeGrupo;
                    lblTotalValorPactuado.Text = "Valor do Pacto: " + pactoAbrangenciaAgregado.ValorPactuado.ToString("C2");
                    //String.Format("{0:C2}", Math.Round( pactoAbrangenciaAgregado.ValorPactuado, 2));

                    //IList<PactoAbrangenciaGrupoMunicipio> pactoAbrangenciaGrupoMunicipio = Factory.GetInstance<IPactoAbrangenciaAgregado>().ListarPactoAbrangenciaGrupoMunicipioPorPactoAbrangenciaAgregado<PactoAbrangenciaGrupoMunicipio>(pactoAbrangenciaAgregado.Codigo);
                    //if (pactoAbrangenciaGrupoMunicipio.Count != 0)
                    //{
                    //Mostro Municípios pertencentes ao grupo selecionado
                    List<Municipio> municipios = grupoAbrangencia.Municipios.OrderBy(p => p.Nome).ToList();
                    ddlMunicipios.DataSource = municipios;
                    ddlMunicipios.DataBind();

                    //Session["PactoAbrangenciaGrupoMunicipio"] = pactoAbrangenciaGrupoMunicipio;
                    //Session["PactoAbrangenciaAgregado"] = pactoAbrangenciaAgregado;
                    //ddlMunicipios_OnSelectedIndexChanged(new object(), new EventArgs());
                    //}
                }

                PanelDadosDoPacto.Visible = true;
                //PanelMunicipios.DataBind();
            }
        }

        protected void GridViewPactosAtivos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;
            Session["IndexSelecionado"] = index;
            GridViewPactosAtivos.EditIndex = e.NewEditIndex;
            CarregaPactosAtivosDoGrupo(ddlGrupos.SelectedValue);
            //PreencheOsTextBoxReferentesAoProcedimentoCBO(GridViewAgregadosSelecionados, index.ToString());
        }

        protected void GridViewPactosAtivos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            GridViewRow linha = GridViewPactosAtivos.Rows[e.RowIndex];
            string id_Pacto_Abrangencia_Agregado_Proced_cbo = linha.Cells[0].Text;
            //string percentual = ((TextBox)linha.FindControl("tbxPercentual")).Text;
            //bool checkeed = ((CheckBox)(linha.FindControl("chkBoxBloqueiaCota"))).Checked;
            PactoAbrangenciaAgregado pactoAbrangenciaAgregado = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<PactoAbrangenciaAgregado>(int.Parse(id_Pacto_Abrangencia_Agregado_Proced_cbo));
            if (pactoAbrangenciaAgregado != null)
            {
                //pactoAbrangenciaAgregado.Percentual = int.Parse(percentual);
                //pactoAbrangenciaAgregado.BloqueiaCota = checkeed == true ? 1 : 0;
                pactoAbrangenciaAgregado.DataUltimaOperacao = DateTime.Now;
                Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoAbrangenciaAgregado);
                Factory.GetInstance<ILogEventos>().Salvar(pactoAbrangenciaAgregado);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi Possível Alterar. Tente Novamente!');", true);
                return;
            }
            GridViewPactosAtivos.EditIndex = -1;
            CarregaPactosAtivosDoGrupo(ddlGrupos.SelectedValue);

        }

    }
}