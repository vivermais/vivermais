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
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.BLL;

namespace ViverMais.View.Agendamento
{
    public partial class FormRelListaProcura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "RELATORIO_LISTA_PROCURA", Modulo.AGENDAMENTO))
                {
                    PanelBuscaAgendado.Visible = false;
                    PanelBuscaCompetencia.Visible = false;
                    PanelBuscaMunicipio.Visible = false;
                    PanelBuscaPeriodo.Visible = false;
                    PanelExibeMunicipio.Visible = false;
                    lblSemRegistros.Visible = false;
                    CarregaTabela();

                    //Carrega os Procedimentos Parametrizados
                    IList<ViverMais.Model.TipoProcedimento> tipoprocedimentos = Factory.GetInstance<IAmbulatorial>().ListarTodos<ViverMais.Model.TipoProcedimento>("Procedimento", true);
                    if (tipoprocedimentos.Count != 0)
                    {
                        ddlProcedimento.Items.Clear();
                        ddlProcedimento.Items.Add(new ListItem("Selecione um Procedimento...", "0"));
                        IList<Procedimento> procedimentos = new List<Procedimento>();
                        foreach (ViverMais.Model.TipoProcedimento tipoprocedimento in tipoprocedimentos)
                        {
                            ViverMais.Model.Procedimento procedimento = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Procedimento>(tipoprocedimento.Procedimento);
                            procedimentos.Add(procedimento);
                            ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo.ToString()));
                        }
                        procedimentos = procedimentos.Distinct().OrderBy(p => p.Nome).ToList();
                        foreach (Procedimento procedimento in procedimentos)
                            ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo.ToString()));
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void rbtMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rbtMunicipio.SelectedValue)
            {
                case "2": PanelExibeMunicipio.Visible = true;
                    IList<Municipio> municipios = Factory.GetInstance<IMunicipio>().ListarPorEstado<Municipio>("BA");
                    ddlMunicipios.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (Municipio mun in municipios)
                    {
                        if (mun.Codigo != "292740")//Ele Retira Salvador da Lista
                            ddlMunicipios.Items.Add(new ListItem(mun.Nome, mun.Codigo));
                    }
                    break;
                default: PanelExibeMunicipio.Visible = false;
                    break;
            }
        }

        protected void CarregaTabela()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Nome Paciente");
            table.Columns.Add("Cartao SUS");
            table.Columns.Add("Municipio");
            table.Columns.Add("Procedimento");
            table.Columns.Add("Data Primeira Procura");
            table.Columns.Add("Data Ultima Procura");
            table.Columns.Add("Agendado");
            table.Columns.Add("Quantidade");
            Session["ListaProcura"] = table;
        }

        private void DefineModoPesquisa(string modo)
        {
            switch (modo)
            {
                case "M": //Por Município
                    PanelBuscaAgendado.Visible = false;
                    PanelBuscaCompetencia.Visible = false;
                    PanelBuscaPeriodo.Visible = false;
                    PanelExibeMunicipio.Visible = false;
                    PanelBuscaMunicipio.Visible = true;
                    break;
                case "P": //Por Período
                    PanelBuscaAgendado.Visible = false;
                    PanelBuscaCompetencia.Visible = false;
                    PanelBuscaMunicipio.Visible = false;
                    PanelExibeMunicipio.Visible = false;
                    PanelBuscaPeriodo.Visible = true;
                    break;
                //case "C": //Por Competencia
                //    PanelBuscaAgendado.Visible = false;
                //    PanelBuscaCompetencia.Visible = true;
                //    PanelBuscaMunicipio.Visible = false;
                //    PanelBuscaPeriodo.Visible = false;
                //    PanelExibeMunicipio.Visible = false;
                //    break;
                case "A": //Por Agendado ou Não Agendados
                    PanelBuscaAgendado.Visible = true;
                    PanelBuscaCompetencia.Visible = false;
                    PanelBuscaMunicipio.Visible = false;
                    PanelBuscaPeriodo.Visible = false;
                    PanelExibeMunicipio.Visible = false;
                    break;
            }

        }

        protected void rbtnModoPesquisa_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefineModoPesquisa(rbtnModoPesquisa.SelectedValue);
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            IList<ListaProcura> listaProcura;
            DataTable table = (DataTable)Session["ListaProcura"];
            switch (rbtnModoPesquisa.SelectedValue)
            {
                case "M": //Por Município
                    listaProcura = Factory.GetInstance<IListaProcura>().BuscaPorProcedimento<ListaProcura>(ddlProcedimento.SelectedValue);
                    if (listaProcura.Count != 0)
                    {
                        if (rbtMunicipio.SelectedValue == "2")//Caso seja um município específico
                        {
                            foreach (ListaProcura lp in listaProcura)
                            {
                                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(lp.Id_paciente);
                                Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
                                if (endereco.Municipio.Codigo == ddlMunicipios.SelectedValue)
                                {
                                    DataRow row = table.NewRow();
                                    row[0] = paciente.Nome;
                                    IList<CartaoSUS> cns = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(paciente.Codigo);
                                    long cartao = (from c in cns select long.Parse(c.Numero)).Min();
                                    row[1] = cartao.ToString(); 
                                    row[2] = endereco.Municipio.NomeSemUF;
                                    row[3] = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(lp.Id_procedimento).Nome;
                                    row[4] = lp.DataInicial.Date.ToString("dd/MM/yyyy");
                                    row[5] = lp.DataUltimaProcura.Date.ToString("dd/MM/yyyy");
                                    row[6] = lp.Agendado == true ? "SIM" : "NÃO";
                                    row[7] = lp.Quantidade.ToString();
                                    table.Rows.Add(row);
                                }
                            }

                        }
                        else if (rbtMunicipio.SelectedValue == "0")//Salvador
                        {
                            foreach (ListaProcura lp in listaProcura)
                            {
                                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(lp.Id_paciente);
                                Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
                                if (endereco.Municipio.Codigo == Municipio.SALVADOR)
                                {
                                    DataRow row = table.NewRow();
                                    row[0] = paciente.Nome;
                                    IList<CartaoSUS> cns = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(paciente.Codigo);
                                    long cartao = (from c in cns select long.Parse(c.Numero)).Min();
                                    row[1] = cartao.ToString();
                                    row[2] = endereco.Municipio.NomeSemUF;
                                    row[3] = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(lp.Id_procedimento).Nome;
                                    row[4] = lp.DataInicial.Date.ToString("dd/MM/yyyy");
                                    row[5] = lp.DataUltimaProcura.Date.ToString("dd/MM/yyyy");
                                    row[6] = lp.Agendado == true ? "SIM" : "NÃO";
                                    row[7] = lp.Quantidade.ToString();
                                    table.Rows.Add(row);
                                }
                            }
                        }
                        else //Interior - 1
                        {
                            foreach (ListaProcura lp in listaProcura)
                            {
                                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(lp.Id_paciente);
                                Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
                                if (endereco.Municipio.Codigo != Municipio.SALVADOR)
                                {
                                    DataRow row = table.NewRow();
                                    row[0] = paciente.Nome;
                                    IList<CartaoSUS> cns = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(paciente.Codigo);
                                    long cartao = (from c in cns select long.Parse(c.Numero)).Min();
                                    row[1] = cartao.ToString();
                                    row[2] = endereco.Municipio.NomeSemUF;
                                    row[3] = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(lp.Id_procedimento).Nome;
                                    row[4] = lp.DataInicial.Date.ToString("dd/MM/yyyy");
                                    row[5] = lp.DataUltimaProcura.Date.ToString("dd/MM/yyyy");
                                    row[6] = lp.Agendado == true ? "SIM" : "NÃO";
                                    row[7] = lp.Quantidade.ToString();
                                    table.Rows.Add(row);
                                }
                            }
                        }
                    }
                    break;
                case "P":
                    DateTime dataInicial, dataFinal = DateTime.MinValue;
                    bool successDataInicial, successDataFinal = false;

                    successDataInicial = DateTime.TryParse(tbxDataInicial.Text, out dataInicial);
                    successDataFinal = DateTime.TryParse(tbxDataFinal.Text, out dataFinal);

                    listaProcura = Factory.GetInstance<IListaProcura>().BuscaPorPeriodoPorProcedimento<ListaProcura>(tbxDataInicial.Text, tbxDataFinal.Text, ddlProcedimento.SelectedValue);
                    if (listaProcura.Count != 0)
                    {
                        foreach (ListaProcura lp in listaProcura)
                        {
                            ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(lp.Id_paciente);
                            Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
                            DataRow row = table.NewRow();
                            row[0] = paciente.Nome;
                            IList<CartaoSUS> cns = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(paciente.Codigo);
                            long cartao = (from c in cns select long.Parse(c.Numero)).Min();
                            row[1] = cartao.ToString();
                            row[2] = endereco.Municipio.NomeSemUF;
                            row[3] = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(lp.Id_procedimento).Nome;
                            row[4] = lp.DataInicial.Date.ToString("dd/MM/yyyy");
                            row[5] = lp.DataUltimaProcura.Date.ToString("dd/MM/yyyy");
                            row[6] = lp.Agendado == true ? "SIM" : "NÃO";
                            row[7] = lp.Quantidade.ToString();
                            table.Rows.Add(row);
                        }
                        Session["ListaProcura"] = table;
                    }
                    break;
                case "A": //Agendados ou não Agendados
                    listaProcura = Factory.GetInstance<IListaProcura>().BuscaPorAgendadoNaoAgendado<ListaProcura>(rbtAgendado.SelectedValue);
                    if (listaProcura.Count != 0)
                    {
                        foreach (ListaProcura lp in listaProcura)
                        {
                            ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(lp.Id_paciente);
                            Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
                            DataRow row = table.NewRow();
                            row[0] = paciente.Nome;
                            IList<CartaoSUS> cns = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(paciente.Codigo);
                            long cartao = (from c in cns select long.Parse(c.Numero)).Min();
                            row[1] = cartao.ToString();
                            row[2] = endereco.Municipio.NomeSemUF;
                            row[3] = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(lp.Id_procedimento).Nome;
                            row[4] = lp.DataInicial.Date.ToString("dd/MM/yyyy");
                            row[5] = lp.DataUltimaProcura.Date.ToString("dd/MM/yyyy");
                            row[6] = lp.Agendado == true ? "SIM" : "NÃO";
                            row[7] = lp.Quantidade.ToString();
                            table.Rows.Add(row);
                        }
                        Session["ListaProcura"] = table;
                    }
                    break;
            }
            if (table.Rows.Count != 0) // Se existir Registros na Tabela ele Direciona para a página de visualização
            {
                lblSemRegistros.Visible = false;
                Redirector.Redirect("RelatorioListaProcura.aspx", "_blank", "");
            }
            else
                lblSemRegistros.Visible = true;
        }

        protected void lknPesquisar_Click(object sender, EventArgs e)
        {

        }
    }
}