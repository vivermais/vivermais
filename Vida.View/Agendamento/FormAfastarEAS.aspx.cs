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
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using System.Drawing;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Agendamento
{
    public partial class FormAfastarEAS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaTriggersUpdatePanel();
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_AFASTAR_EAS", Modulo.AGENDAMENTO))
                {
                    if (Request.QueryString["id_afastamento"] != null)
                    {
                        int id_afastamento = int.Parse(Request.QueryString["id_afastamento"]);

                        IAgendamentoServiceFacade iagendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                        AfastamentoEAS afastamentoeas = iagendamento.BuscarPorCodigo<AfastamentoEAS>(id_afastamento);
                        ViverMais.Model.EstabelecimentoSaude estabelecimentoSaude = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(afastamentoeas.ID_Unidade);
                        ddlEstabelecimentoSaude.Items.Add(new ListItem(estabelecimentoSaude.NomeFantasia, estabelecimentoSaude.CNES));
                        tbxData_Inicial.Text = afastamentoeas.Data_Inicial.ToString("dd/MM/yyyy");
                        tbxData_Inicial.Enabled = false;
                        if (afastamentoeas.Data_Reativacao != null)
                        {
                            tbxData_Reativacao.Text = afastamentoeas.Data_Reativacao.Value.ToString("dd/MM/yyyy");
                            rblTipoAfastamento.Items[0].Selected = true;
                        }
                        else
                            rblTipoAfastamento.Items[1].Selected = true;

                        tbxMotivo.Text = afastamentoeas.Motivo;
                        tbxObs.Text = afastamentoeas.Obs;

                        string dataInicial = tbxData_Inicial.Text;
                        string dataReativacao = "";
                        if (tbxData_Reativacao.Text != "")
                            dataReativacao = tbxData_Reativacao.Text;

                        IList<Agenda> agendas = Factory.GetInstance<IAmbulatorial>().VerificarAgendas<Agenda>(ddlEstabelecimentoSaude.SelectedValue, "", dataInicial, dataReativacao, ddlTurno.SelectedValue);
                        DataTable tableAgendas = new DataTable();
                        if (agendas.Count != 0)
                        {
                            DataColumn c0 = new DataColumn("Codigo");
                            DataColumn c1 = new DataColumn("Procedimento");
                            DataColumn c2 = new DataColumn("Data");
                            DataColumn c3 = new DataColumn("QtdVagas");
                            DataColumn c4 = new DataColumn("QtdAgendada");
                            DataColumn c5 = new DataColumn("Bloqueada");
                            tableAgendas.Columns.Add(c0);
                            tableAgendas.Columns.Add(c1);
                            tableAgendas.Columns.Add(c2);
                            tableAgendas.Columns.Add(c3);
                            tableAgendas.Columns.Add(c4);
                            tableAgendas.Columns.Add(c5);

                            foreach (Agenda listaAgenda in agendas)
                            {
                                ViverMais.Model.Procedimento procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(listaAgenda.Procedimento.Codigo);
                                DataRow row = tableAgendas.NewRow();
                                row[0] = listaAgenda.Codigo;
                                row[1] = procedimento.Nome;
                                row[2] = listaAgenda.Data.ToString("dd/MM/yyyy");
                                row[3] = listaAgenda.Quantidade;
                                row[4] = listaAgenda.QuantidadeAgendada;
                                if (listaAgenda.Bloqueada)
                                    row[5] = "1";
                                else
                                    row[5] = "0";
                                tableAgendas.Rows.Add(row);
                            }
                            Session["listaAgenda"] = tableAgendas;
                            lblSemRegistro.Visible = false;
                        }
                        else
                            lblSemRegistro.Visible = true;


                        GridViewAgendas.DataSource = tableAgendas;
                        GridViewAgendas.DataBind();

                        LinhaMarcadaAgendasBloqueadas(tableAgendas);
                        PanelAgendas.Visible = true;
                        btnVisualizarAgendas.Visible = false;
                        btnBloquearTodasAgendas.Visible = false;
                        GridViewAgendas.Columns[6].Visible = false;
                        GridViewAgendas.Columns[7].Visible = false;
                        PanelEstabelecimentoSaude.Visible = false;
                        ddlEstabelecimentoSaude.Enabled = false;
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            if ((String)Session["VisualizouAgenda"] != "1")
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Você deve Visualizar as Agendas Primeiro.');", true);
                return;
            }
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            DateTime data_inicial = DateTime.Parse(tbxData_Inicial.Text);
            DateTime hoje = DateTime.Today;

            if (tbxData_Reativacao.Text != "")
            {
                DateTime data_reativacao = DateTime.Parse(tbxData_Reativacao.Text);
                if (data_reativacao < hoje)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Data anterior à data do dia.');", true);
                    return;
                }

                if (data_reativacao < data_inicial)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Data de Reativação é menor do que a Data Inicial.');", true);
                    return;
                }
            }

            DataTable tableAgendas = (DataTable)(Session["listaAgenda"]);
            if (tableAgendas != null)
            {
                for (int i = 0; i < tableAgendas.Rows.Count; i++)
                {
                    if (int.Parse(tableAgendas.Rows[i][4].ToString()) != 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi possível SAVAR. Há Solicitações marcadas!');", true);
                        return;
                    }

                    if (tableAgendas.Rows[i][5].ToString() != "1")
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi possível SAVAR. Há Agendas desbloqueadas');", true);
                        return;
                    }
                }
            }

            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();

            AfastamentoEAS afastamentoeas = null;
            if (Request.QueryString["id_afastamentoeas"] != null)
            {
                int id_afastamentoeas = int.Parse(Request.QueryString["id_afastamentoeas"]);
                afastamentoeas = iAgendamento.BuscarPorCodigo<AfastamentoEAS>(id_afastamentoeas);
            }
            else
            {
                if (data_inicial < hoje)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Data anterior à data do dia.');", true);
                    return;
                }
                //Criticar se já existe este afastamento
                AfastamentoEAS afastamento = Factory.GetInstance<IAfastamentoEAS>().VerificaAfastamentosNaData<AfastamentoEAS>(ddlEstabelecimentoSaude.SelectedValue, data_inicial, tbxData_Reativacao.Text);
                if (afastamento != null)
                {
                    //DateTime ultimaDataInicial = afastamento.Data_Inicial;
                    //DateTime ultimaDataFinal;
                    //if (afastamento.Data_Reativacao == null)
                    //    ultimaDataFinal = DateTime.Today;
                    //else
                    //    ultimaDataFinal = DateTime.Parse(afastamento.Data_Reativacao.Value.ToString("dd/MM/yyyy"));

                    //if ((data_inicial > ultimaDataInicial) && (data_inicial <= ultimaDataFinal))
                    //{
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Esse Estabelecimento de Saúde já está Afastado!');</script>");
                    return;
                    //}
                }
                afastamentoeas = new AfastamentoEAS();
            }
            afastamentoeas.Data_Inicial = DateTime.Parse(tbxData_Inicial.Text);
            afastamentoeas.Motivo = tbxMotivo.Text;
            afastamentoeas.ID_Unidade = ddlEstabelecimentoSaude.SelectedValue;
            afastamentoeas.Obs = tbxObs.Text;
            if (tbxData_Reativacao.Text != "")
                afastamentoeas.Data_Reativacao = DateTime.Parse(tbxData_Reativacao.Text);

            iAgendamento.Salvar(afastamentoeas);
            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 7, "id_afastamento_EAS:" + afastamentoeas.Codigo));
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados salvos com sucesso!');window.location='FormAfastarEAS.aspx?id_fastamentoeas=" + afastamentoeas.Codigo + "'</script>");
        }


        //protected void btnPesquisar_Click(object sender, EventArgs e)
        //{
        //    string id_unidade = ddlEstabelecimentoSaude.SelectedValue;
        //    IList<ViverMais.Model.AfastamentoEAS> afastamentos = Factory.GetInstance<IAfastamentoEAS>().BuscarAfastamentosPorUnidade<ViverMais.Model.AfastamentoEAS>(id_unidade);
        //    if (afastamentos.Count != 0)
        //    {
        //        DataTable table = new DataTable(); ;
        //        DataColumn c0 = new DataColumn("Codigo");
        //        DataColumn c1 = new DataColumn("Data_Inicial");
        //        DataColumn c2 = new DataColumn("Data_Reativacao");
        //        table.Columns.Add(c0);
        //        table.Columns.Add(c1);
        //        table.Columns.Add(c2);

        //        foreach (AfastamentoEAS af in afastamentos)
        //        {
        //            DataRow row = table.NewRow();
        //            row[0] = af.Codigo;
        //            row[1] = af.Data_Inicial.ToString("dd/MM/yyyy");
        //            row[2] = af.Data_Reativacao.Value == null ? "" : af.Data_Reativacao.Value.ToString("dd/MM/yyyy");
        //            table.Rows.Add(row);
        //        }

        //        //gridAfastamento.DataSource = table;
        //        //gridAfastamento.DataBind();
        //        //lblSemRegistros.Visible = false;
        //    }
        //    else
        //    {
        //        //lblSemRegistros.Visible = true;
        //    }

        //}

        protected void tbxCompetencia_TextChanged(object sender, EventArgs e)
        {
            tbxCNES.Focus();
        }

        protected void btnPesquisarEstabelecimento_Click(object sender, EventArgs e)
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

        protected void rblTipoAfastamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblTipoAfastamento.Items[0].Selected)
                tbxData_Reativacao.Enabled = true;
            else
            {
                tbxData_Reativacao.Enabled = false;
                tbxData_Reativacao.Text = "";
            }
        }

        protected void btnVisualizarAgendas_Click(object sender, EventArgs e)
        {
            string dataInicial = tbxData_Inicial.Text;
            string dataReativacao = "";
            if (tbxData_Reativacao.Text != "")
                dataReativacao = tbxData_Reativacao.Text;

            IList<Agenda> agendas = Factory.GetInstance<IAmbulatorial>().VerificarAgendas<Agenda>(ddlEstabelecimentoSaude.SelectedValue, "", dataInicial, dataReativacao, ddlTurno.SelectedValue);
            DataTable tableAgendas = new DataTable();
            Session["VisualizouAgenda"] = "1";
            if (agendas.Count != 0)
            {
                DataColumn c0 = new DataColumn("Codigo");
                DataColumn c1 = new DataColumn("Procedimento");
                DataColumn c2 = new DataColumn("Data");
                DataColumn c3 = new DataColumn("QtdVagas");
                DataColumn c4 = new DataColumn("QtdAgendada");
                DataColumn c5 = new DataColumn("Bloqueada");
                tableAgendas.Columns.Add(c0);
                tableAgendas.Columns.Add(c1);
                tableAgendas.Columns.Add(c2);
                tableAgendas.Columns.Add(c3);
                tableAgendas.Columns.Add(c4);
                tableAgendas.Columns.Add(c5);

                foreach (Agenda listaAgenda in agendas)
                {
                    ViverMais.Model.Procedimento procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(listaAgenda.Procedimento.Codigo);
                    DataRow row = tableAgendas.NewRow();
                    row[0] = listaAgenda.Codigo;
                    row[1] = procedimento.Nome;
                    row[2] = listaAgenda.Data.ToString("dd/MM/yyyy");
                    row[3] = listaAgenda.Quantidade;
                    row[4] = listaAgenda.QuantidadeAgendada;
                    if (listaAgenda.Bloqueada)
                        row[5] = "1";
                    else
                        row[5] = "0";
                    tableAgendas.Rows.Add(row);
                }
                Session["listaAgenda"] = tableAgendas;
                lblSemRegistro.Visible = false;
            }
            else
            {
                lblSemRegistro.Visible = true;
            }

            GridViewAgendas.DataSource = tableAgendas;
            GridViewAgendas.DataBind();

            LinhaMarcadaAgendasBloqueadas(tableAgendas);
            PanelAgendas.Visible = true;
        }

        protected void LinhaMarcadaAgendasBloqueadas(DataTable tableAgendas)
        {
            tableAgendas = (DataTable)(Session["listaAgenda"]);
            int index = 0;
            if (tableAgendas != null)
            {
                for (int i = GridViewAgendas.PageIndex * GridViewAgendas.PageSize; i < (10 * (GridViewAgendas.PageIndex + 1)); i++)
                {
                    if (i < tableAgendas.Rows.Count)
                    {
                        if (tableAgendas.Rows[i][5].ToString() == "1")
                        {
                            GridViewAgendas.Rows[index].BackColor = Color.DimGray;
                            GridViewAgendas.Rows[index].Cells[1].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                            GridViewAgendas.Rows[index].Cells[2].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                            GridViewAgendas.Rows[index].Cells[3].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                            GridViewAgendas.Rows[index].Cells[4].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                            GridViewAgendas.Rows[index].Cells[7].FindControl("btnBloquearAgenda").Visible = false;
                        }
                    }
                    else
                        break;

                    index++;
                }
            }
        }

        private void CarregaTriggersUpdatePanel()
        {
            if (GridViewAgendas.FindControl("btnSelecionarAgenda") != null)
            {
                AsyncPostBackTrigger trig = new AsyncPostBackTrigger();
                trig.ControlID = GridViewAgendas.FindControl("btnSelecionarAgenda").UniqueID;
                trig.EventName = "Click";
                UpdatePanelVisualizaAgenda.Triggers.Add(trig);
            }
        }

        protected void SelecionarAgenda(object sender, EventArgs e)
        {
            //PanelVisualizaAgenda.Visible = true;
            string i_index = ((LinkButton)sender).CommandArgument;
            int index;
            if (int.Parse(i_index) == 0)
                index = GridViewAgendas.PageIndex * GridViewAgendas.PageSize;
            else
                index = (GridViewAgendas.PageIndex * GridViewAgendas.PageSize) + int.Parse(i_index);

            int indexGrid = int.Parse(i_index);

            DataTable tableAgendas = (DataTable)(Session["listaAgenda"]);
            GridViewRow row = GridViewAgendas.Rows[indexGrid];
            Agenda agenda = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Agenda>(int.Parse(Server.HtmlDecode(row.Cells[0].Text)));
            Session["agenda"] = agenda;
            lbProcedimento.Text = Server.HtmlDecode(row.Cells[1].Text);
            lbData.Text = agenda.Data.ToString("dd/MM/yyyy");
            ViverMais.Model.Profissional profissional = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Profissional>(agenda.ID_Profissional);
            if (profissional != null)
                lbProfissional.Text = profissional.Nome;
            else
                lbProfissional.Text = "-";

            if (agenda.Turno == "M")
                lbTurno.Text = "Matutino";
            else
                lbTurno.Text = "Tarde";

            IList<Solicitacao> listaSolicitacoes = Factory.GetInstance<ISolicitacao>().BuscaSolicitacoesNaoConfirmadasNaoIndeferidasPorAgenda<Solicitacao>(agenda.Codigo);
            DataTable tableSolicitacoes = new DataTable();
            DataColumn c0 = new DataColumn("Codigo");
            DataColumn c1 = new DataColumn("CartaoSus");
            DataColumn c2 = new DataColumn("Paciente");
            DataColumn c3 = new DataColumn("DataNascimento");
            tableSolicitacoes.Columns.Add(c0);
            tableSolicitacoes.Columns.Add(c1);
            tableSolicitacoes.Columns.Add(c2);
            tableSolicitacoes.Columns.Add(c3);

            foreach (Solicitacao solicitacao in listaSolicitacoes)
            {
                IList<ViverMais.Model.CartaoSUS> cartoesSus = Factory.GetInstance<IPaciente>().ListarCartoesSUS<ViverMais.Model.CartaoSUS>(solicitacao.ID_Paciente);
                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(solicitacao.ID_Paciente);
                DataRow row2 = tableSolicitacoes.NewRow();
                row2[0] = solicitacao.Codigo.ToString();
                row2[1] = cartoesSus[cartoesSus.Count - 1].Numero;
                row2[2] = paciente.Nome;
                row2[3] = paciente.DataNascimento.ToString("dd/MM/yyyy");
                tableSolicitacoes.Rows.Add(row2);
            }

            DesabilitaBotoesDesmarcarSolicitacoes(tableSolicitacoes);
            GridViewSolicitacoes.DataSource = tableSolicitacoes;
            GridViewSolicitacoes.DataBind();
            Session["listaSolicitacao"] = tableSolicitacoes;
            ViewState["qtdSolicitacoesDesmarcadasPorAgenda"] = 0;
            ViewState["indexGridAgendas"] = indexGrid;
            Session["permiteDesmarcar"] = null;
        }

        protected void GridViewAgendas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewAgendas.PageIndex = e.NewPageIndex;
            if (Session["listaAgenda"] != null)
            {
                DataTable tableAgendas = (DataTable)(Session["listaAgenda"]);
                GridViewAgendas.DataSource = tableAgendas;
                GridViewAgendas.DataBind();
                LinhaMarcadaAgendasBloqueadas(tableAgendas);
            }
        }

        protected void GridViewSolicitacoes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (Session["permiteDesmarcar"] != null)
            {
                int index = int.Parse(e.CommandArgument.ToString()) == 0 ? GridViewSolicitacoes.PageIndex * GridViewSolicitacoes.PageSize : (GridViewSolicitacoes.PageIndex * GridViewSolicitacoes.PageSize) + int.Parse(e.CommandArgument.ToString());
                GridViewRow row = GridViewSolicitacoes.Rows[index];
                int id_solicitacao = int.Parse(Server.HtmlDecode(row.Cells[0].Text));
                DataTable tableSolicitacao = (DataTable)(Session["listaSolicitacao"]);
                if (e.CommandName == "Desmarcar")
                {
                    if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_DESMARCAR_AGENDAMENTO", Modulo.AGENDAMENTO))
                    {
                        IAgendamentoServiceFacade iagendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                        Solicitacao solicitacao = iagendamento.BuscarPorCodigo<Solicitacao>(id_solicitacao);

                        ILaudo iLaudo = Factory.GetInstance<ILaudo>();
                        IList<Laudo> laudos = iLaudo.BuscaPorSolicitacao<Laudo>(id_solicitacao.ToString());
                        //Deleta os Laudos Referente a solicitacao
                        if (laudos.Count != 0)
                        {
                            foreach (Laudo l in laudos)
                                iLaudo.Deletar(l);
                        }

                        // Vai subtrair 1 à Quantidade agendada
                        int id_agenda = solicitacao.Agenda.Codigo;
                        Agenda agenda = iagendamento.BuscarPorCodigo<Agenda>(id_agenda);
                        agenda.QuantidadeAgendada -= 1;
                        iagendamento.Salvar(agenda);

                        // Vai deletar logicamente a solicitacao
                        solicitacao.Situacao = "6";
                        iagendamento.Salvar(solicitacao);
                        iagendamento.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 13, "ID_SOLICITACAO:" + solicitacao.Codigo + " ID_AGENDA:" + solicitacao.Agenda.Codigo.ToString()));
                        tableSolicitacao.Rows.RemoveAt(index);

                        GridViewSolicitacoes.DataSource = tableSolicitacao;
                        GridViewSolicitacoes.DataBind();
                        Session["listaSolicitacao"] = tableSolicitacao;
                        ViewState["qtdSolicitacoesDesmarcadasPorAgenda"] = int.Parse(ViewState["qtdSolicitacoesDesmarcadasPorAgenda"].ToString()) + 1;
                        DesabilitaBotoesDesmarcarSolicitacoes(tableSolicitacao);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                                "alert('Solicitação desmarcada com sucesso!');", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para desmarcar solicitações! Por favor, entre em contato com a administração.');", true);
                    return;
                }
                
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                        "alert('É necessário imprimir antes de DESMARCAR uma solicitação.');", true);

        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            //PanelVisualizaAgenda.Visible = false;
            int qtdDesmarcada = int.Parse(ViewState["qtdSolicitacoesDesmarcadasPorAgenda"].ToString());
            int indexGridAgendas = int.Parse(ViewState["indexGridAgendas"].ToString());
            DataTable tableAgendas = (DataTable)Session["listaAgenda"];
            DataRow row = tableAgendas.Rows[indexGridAgendas];
            row[4] = int.Parse(row[4].ToString()) - qtdDesmarcada;
            GridViewAgendas.DataSource = tableAgendas;
            GridViewAgendas.DataBind();
            Session["listaAgenda"] = tableAgendas;
            LinhaMarcadaAgendasBloqueadas(tableAgendas);
        }

        protected void GridViewAgendas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "BloquearAgenda")
            {
                //int index = int.Parse(e.CommandArgument.ToString()) == 0 ? GridViewAgendas.PageIndex * GridViewAgendas.PageSize : (GridViewAgendas.PageIndex * GridViewAgendas.PageSize) + int.Parse(e.CommandArgument.ToString());
                int index = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = GridViewAgendas.Rows[index];
                int id_agenda = int.Parse(Server.HtmlDecode(row.Cells[0].Text));
                IAgendamentoServiceFacade iagendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                Agenda agenda = iagendamento.BuscarPorCodigo<Agenda>(id_agenda);
                agenda.Bloqueada = true;
                iagendamento.Salvar(agenda);
                row.BackColor = Color.DimGray;
                int indexTableAgenda = int.Parse(e.CommandArgument.ToString()) == 0 ? GridViewAgendas.PageIndex * GridViewAgendas.PageSize : (GridViewAgendas.PageIndex * GridViewAgendas.PageSize) + int.Parse(e.CommandArgument.ToString());
                DataTable tableAgenda = (DataTable)(Session["listaAgenda"]);
                tableAgenda.Rows[indexTableAgenda][5] = "1";
                GridViewAgendas.DataSource = tableAgenda;
                GridViewAgendas.DataBind();
                Session["listaAgenda"] = tableAgenda;
                GridViewAgendas.Rows[index].BackColor = Color.DimGray;
                GridViewAgendas.Rows[index].Cells[1].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                GridViewAgendas.Rows[index].Cells[2].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                GridViewAgendas.Rows[index].Cells[3].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                GridViewAgendas.Rows[index].Cells[4].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                                     "alert('Agenda bloqueada com sucesso!');", true);
            }
        }

        protected void btnBloquearTodasAgendas_Click(object sender, EventArgs e)
        {
            DataTable tableAgendas = (DataTable)(Session["listaAgenda"]);
            if (tableAgendas != null)
            {
                for (int i = 0; i < tableAgendas.Rows.Count; i++)
                {
                    int id_agenda = int.Parse(tableAgendas.Rows[i][0].ToString());
                    IAgendamentoServiceFacade iagendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                    Agenda agenda = iagendamento.BuscarPorCodigo<Agenda>(id_agenda);
                    agenda.Bloqueada = true;
                    iagendamento.Salvar(agenda);
                    tableAgendas.Rows[i][5] = "1";
                }
                GridViewAgendas.DataSource = tableAgendas;
                GridViewAgendas.DataBind();
                Session["listaAgenda"] = tableAgendas;
                LinhaMarcadaAgendasBloqueadas(tableAgendas);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                                         "alert('Agendas bloqueadas com sucesso!');", true);
            }
        }

        protected void btnDesmarcarTodasSolicitacoes_Click(object sender, EventArgs e)
        {
            if (Session["permiteDesmarcar"] != null)
            {
                DataTable tableSolicitacoes = (DataTable)(Session["listaSolicitacao"]);
                ViewState["qtdSolicitacoesDesmarcadasPorAgenda"] = tableSolicitacoes.Rows.Count;
                IAgendamentoServiceFacade iagendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                Agenda agendaSecao = (Agenda)(Session["agenda"]);
                Agenda agenda = iagendamento.BuscarPorCodigo<Agenda>(agendaSecao.Codigo);
                agenda.QuantidadeAgendada -= tableSolicitacoes.Rows.Count;
                iagendamento.Salvar(agenda);

                for (int i = 0; i < tableSolicitacoes.Rows.Count; i++)
                {
                    int id_solicitacao = int.Parse(tableSolicitacoes.Rows[i][0].ToString());
                    Solicitacao solicitacao = iagendamento.BuscarPorCodigo<Solicitacao>(id_solicitacao);

                    ILaudo iLaudo = Factory.GetInstance<ILaudo>();
                    IList<Laudo> laudos = iLaudo.BuscaPorSolicitacao<Laudo>(id_solicitacao.ToString());
                    //Deleta os Laudos Referente a solicitacao
                    if (laudos.Count != 0)
                    {
                        foreach (Laudo l in laudos)
                            iLaudo.Deletar(l);
                    }

                    solicitacao.Situacao = "6";
                    iagendamento.Salvar(solicitacao);

                    IList<ProcedimentoRegistro> pr = Factory.GetInstance<IRegistro>().BuscarPorProcedimento<ProcedimentoRegistro>(solicitacao.Procedimento.Codigo, Registro.APAC_PROC_PRINCIPAL);
                    if (pr != null && pr.Count != 0)
                    {
                        string faixa = (solicitacao.Identificador).Substring(5, 7);
                        ViverMais.Model.Faixa fx = Factory.GetInstance<IFaixa>().BuscarCodigoFaixa<Faixa>(faixa);
                        if (fx != null)
                        {
                            fx.Quantidade_utilizada -= 1;
                            Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(fx);
                        }
                    }
                }

                tableSolicitacoes.Rows.Clear();
                DesabilitaBotoesDesmarcarSolicitacoes(tableSolicitacoes);
                GridViewSolicitacoes.DataSource = tableSolicitacoes;
                GridViewSolicitacoes.DataBind();
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                                         "alert('Solicitações desmarcadas com sucesso!');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert",
                        "alert('É necessário imprimir antes de DESMARCAR uma solicitação.');", true);

        }

        protected void btnImprimirSolicitacoes_Click(object sender, EventArgs e)
        {
            Agenda agendaSecao = (Agenda)(Session["agenda"]);
            Session["permiteDesmarcar"] = 1;
            Redirector.Redirect("RelatorioListaSolicitacoesAgenda.aspx?codigo=" + agendaSecao.Codigo, "_blank", "");
        }

        protected void DesabilitaBotoesDesmarcarSolicitacoes(DataTable tableSolicitacoes)
        {
            if (tableSolicitacoes.Rows.Count == 0)
            {
                lbMensagemSolicitacoes.Visible = true;
                btnDesmarcarTodasSolicitacoes.Visible = false;
                btnImprimirSolicitacoes.Visible = false;
            }
            else
            {
                lbMensagemSolicitacoes.Visible = false;
                btnDesmarcarTodasSolicitacoes.Visible = true;
                btnImprimirSolicitacoes.Visible = true;
            }
        }
    }
}
