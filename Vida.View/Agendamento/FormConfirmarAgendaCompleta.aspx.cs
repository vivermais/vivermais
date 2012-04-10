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
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Text.RegularExpressions;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.BLL;

namespace ViverMais.View.Agendamento
{
    public partial class FormConfirmarAgendaCompleta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ListaAgenda"] != null)
                    Session.Remove("ListaAgenda");

                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_REGISTRAR_EXECUCAO", Modulo.AGENDAMENTO))
                {
                    // Busca Validade do Codigo de Controle
                    Parametros parametros = Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.IAgendamentoServiceFacade>().ListarTodos<Parametros>().FirstOrDefault();
                    if (parametros == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Parametro Ambulatoriais não cadastrado !');", true);
                        return;
                    }

                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        void ClearDropDownList(List<DropDownList> drops)
        {
            foreach (DropDownList dropdown in drops)
            {
                dropdown.Items.Clear();
                dropdown.Items.Insert(0, new ListItem("Selecione...", "0"));
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            DateTime dataInicial = DateTime.Parse(tbxData_Inicial.Text);
            DateTime dataFinal = DateTime.Parse(tbxData_Final.Text);

            IList<Agenda> agendas = Factory.GetInstance<IAmbulatorial>().ListarAgendas<Agenda>(usuario.Unidade.CNES, null, (ddlProcedimento.SelectedValue == "0" ? null : ddlProcedimento.SelectedValue),
                (ddlProfissional.SelectedValue == "0" ? null : ddlProfissional.SelectedValue), dataInicial, dataFinal, (ddlCBO.SelectedValue == "0" ? null : ddlCBO.SelectedValue), (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));
            gridAgenda.DataSource = agendas;
            gridAgenda.DataBind();
            Session["ListaAgenda"] = agendas;
        }

        protected void chkBoxFalta_OnCheckedChanged(object sender, EventArgs e)
        {
            int index = int.Parse(Session["IndexSolicitacaoSelecionada"].ToString());
            string id_solicitacao = GridViewSolicitacoes.DataKeys[index].Value.ToString();
            Solicitacao solicitacao = Factory.GetInstance<ISolicitacao>().BuscarPorCodigo<Solicitacao>(int.Parse(id_solicitacao));
            TextBox tbxIdentificador = (TextBox)GridViewSolicitacoes.Rows[index].FindControl("tbxIdentificador");
            DropDownList ddlCID = (DropDownList)GridViewSolicitacoes.Rows[index].FindControl("ddlCID");
            //TextBox tbxCid = (TextBox)GridViewSolicitacoes.Rows[index].FindControl("tbxCID");
            CheckBox chkBox = (CheckBox)GridViewSolicitacoes.Rows[index].FindControl("chkBoxFalta");
            RequiredFieldValidator requiredFieldIdentificador = (RequiredFieldValidator)GridViewSolicitacoes.Rows[index].FindControl("RequiredFieldIdentificador");
            RequiredFieldValidator requiredFieldCID = (RequiredFieldValidator)GridViewSolicitacoes.Rows[index].FindControl("RequiredFieldCID");
            if (chkBox.Checked)
            {
                tbxIdentificador.Text = string.Empty;
                tbxIdentificador.Enabled = false;
                ddlCID.SelectedValue = "0";
                ddlCID.Enabled = false;
                requiredFieldIdentificador.Enabled = false;
            }
            else
            {
                tbxIdentificador.Enabled = true;
                ddlCID.Enabled = true;
                requiredFieldIdentificador.Enabled = true;

            }
        }

        protected void CustomValidatorCID_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;
            int index = int.Parse(Session["IndexSolicitacaoSelecionada"].ToString()); ;
            string id_solicitacao = GridViewSolicitacoes.DataKeys[index].Value.ToString();
            Solicitacao solicitacao = Factory.GetInstance<ISolicitacao>().BuscarPorCodigo<Solicitacao>(int.Parse(id_solicitacao));
            ICid iCid = Factory.GetInstance<ICid>();
            TextBox tbxCID = (TextBox)GridViewSolicitacoes.Rows[index].FindControl("tbxCID");
            Cid cid = iCid.BuscarPorCodigo<Cid>(tbxCID.Text);
            if (Factory.GetInstance<IRegistro>().ProcedimentoExigeCid(solicitacao.Agenda.Procedimento.Codigo))
            {
                if (String.IsNullOrEmpty(tbxCID.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O CID é Obrigatório!');", true);
                    e.IsValid = false;
                }
                else if (cid == null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O CID é Inválido!');", true);
                    e.IsValid = false;
                }
                else if (cid != null)
                {
                    if (!iCid.ExisteVinculoProcedimentoCid(solicitacao.Agenda.Procedimento.Codigo, cid.Codigo))//Caso o Cid Informado não tenha vínculo com o Procedimento
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O CID informado não tem vínculo com o procedimento. Por favor, informe um CID válido!');", true);
                        e.IsValid = false;
                    }
                    else if (!iCid.CidPermitidoParaSexo(tbxCID.Text, Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(solicitacao.ID_Paciente).Sexo.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O CID é Inválido para o Sexo do Paciente!');", true);
                        e.IsValid = false;
                    }
                }
            }
        }

        protected void gridAgenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int index = ;

            int id_agenda = int.Parse(gridAgenda.DataKeys[gridAgenda.SelectedIndex].Value.ToString());

            //int id_agenda = int.Parse(gridAgenda.SelectedDataKey.Value.ToString());

            DataTable tableSolicitacoes = new DataTable();
            tableSolicitacoes.Columns.Add("Codigo");
            tableSolicitacoes.PrimaryKey = new DataColumn[] { tableSolicitacoes.Columns["Codigo"] };
            tableSolicitacoes.Columns.Add("CNS");
            tableSolicitacoes.Columns.Add("PACIENTE");
            tableSolicitacoes.Columns.Add("Identificador");
            tableSolicitacoes.Columns.Add("CID", typeof(DropDownList));
            tableSolicitacoes.Columns.Add("Prontuario");
            tableSolicitacoes.Columns.Add("Faltoso", typeof(bool));
            tableSolicitacoes.Columns.Add("NomeSituacao");
            ISolicitacao iSolicitacao = Factory.GetInstance<ISolicitacao>();
            Agenda agenda = iSolicitacao.BuscarPorCodigo<Agenda>(id_agenda);
            VerificaEDesabilitaColunaAPAC(agenda);
            IPaciente iPaciente = Factory.GetInstance<IPaciente>();
            //Listo as Solicitações da Agenda Selecionada
            IList<Solicitacao> solicitacoes = iSolicitacao.ListaSolicitacoesDaAgenda<Solicitacao>(id_agenda).Where(solicitacao => solicitacao.Situacao != Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString()).ToList();
            for (int i = 0; i < solicitacoes.Count; i++)
            {
                DataRow row = tableSolicitacoes.NewRow();
                row["Codigo"] = solicitacoes[i].Codigo.ToString();
                row["CNS"] = iPaciente.ListarCartoesSUS<CartaoSUS>(solicitacoes[i].ID_Paciente).FirstOrDefault().Numero;
                ViverMais.Model.Paciente paciente = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(solicitacoes[i].ID_Paciente);
                row["PACIENTE"] = paciente.Nome;
                //ddlCID.SelectedValue =  solicitacoes[i].CidExecutante == null ? "0" : solicitacoes[i].CidExecutante.Codigo;

                if (solicitacoes[i].Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString())
                {
                    //ddlCID.Enabled = false;
                    row["Identificador"] = solicitacoes[i].Identificador;
                    row["Prontuario"] = solicitacoes[i].Prontuario.ToString();
                }
                else
                {
                    //ddlCID.Enabled = true;
                    row["Identificador"] = string.Empty;
                    row["Prontuario"] = string.Empty;
                }
                //row["CID"] = ddlCID;
                row["NomeSituacao"] = solicitacoes[i].NomeSituacao;
                if (solicitacoes[i].Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.FALTOSO).ToString())
                    row["Faltoso"] = true;
                else
                    row["Faltoso"] = false;
                tableSolicitacoes.Rows.Add(row);
            }

            GridViewSolicitacoes.DataSource = tableSolicitacoes;
            GridViewSolicitacoes.DataBind();
            Session["ListaSolicitacoes"] = tableSolicitacoes;
            DesabilitaBotaoEdicaoSolicitacoes();
            GridViewSolicitacoes.EditIndex = -1;
        }

        protected void GridViewSolicitacoes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Solicitacao solicitacao = Factory.GetInstance<ISolicitacao>().BuscarPorCodigo<Solicitacao>(int.Parse(((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString()));
                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(solicitacao.ID_Paciente);
                DropDownList ddlCID = (DropDownList)e.Row.Cells[3].FindControl("ddlCID");
                ddlCID.DataValueField = "Codigo";
                ddlCID.DataTextField = "DescricaoCodigoNome";
                IList<Cid> cids = Factory.GetInstance<ICid>().BuscarPorProcedimento<ProcedimentoCid>(solicitacao.Agenda.Procedimento.Codigo, paciente.Sexo.ToString()).Select(p => p.Cid).ToList();
                ddlCID.DataSource = cids;
                ddlCID.DataBind();
                ddlCID.Items.Insert(0, new ListItem("Selecione...", "0"));
                if (e.Row.RowIndex == GridViewSolicitacoes.EditIndex)
                {
                    if (GridViewSolicitacoes.EditIndex == -1)//Caso seja não seja edição
                        ddlCID.Enabled = false;
                    
                    else
                    {
                        if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString() || solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString() || solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.FALTOSO).ToString())
                        {
                            ddlCID.Enabled = false;
                        }
                        else
                            ddlCID.Enabled = true;
                    }
                }
                if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString())
                    if (solicitacao.CidExecutante != null)
                        ddlCID.SelectedValue = solicitacao.CidExecutante.Codigo;
            }
        }

        /// <summary>
        /// Paginação da Lista de Agendas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridAgenda_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridAgenda.SelectedIndex = -1;
            gridAgenda.EditIndex = -1;
            gridAgenda.DataSource = Session["ListaAgenda"];
            gridAgenda.PageIndex = e.NewPageIndex;
            gridAgenda.DataBind();
        }

        void RecarregaGrid()
        {
            GridViewSolicitacoes.DataSource = Session["ListaSolicitacoes"];
            GridViewSolicitacoes.DataBind();
        }

        protected void GridViewSolicitacoes_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSolicitacoes.DataSource = Session["ListaSolicitacoes"];
            GridViewSolicitacoes.PageIndex = e.NewPageIndex;
            GridViewSolicitacoes.DataBind();
        }

        public string RetornaHtmlComInformacoesAPAC(int id_solicitacao)
        {
            Solicitacao solicitacao = Factory.GetInstance<ISolicitacao>().BuscarPorCodigo<Solicitacao>(id_solicitacao);
            string html = "<p></p>";
            if (solicitacao.UsuarioRegulador != null)
            {
                html += "<p><span class=tooltipInfo>Médico Regulador: </span><br /><span class=tooltipDados>" + solicitacao.UsuarioRegulador.Nome + " </span></p>";
                html += "<p><span class=tooltipInfo>CNS: </span><br /><span class=tooltipDados>" + solicitacao.UsuarioRegulador.CartaoSUS + " </span></p>";
                Parametros parametrosAmbulatoriais = Factory.GetInstance<ISolicitacao>().ListarTodos<Parametros>().FirstOrDefault();
                if (parametrosAmbulatoriais != null)
                {
                    DateTime dataFinalAPAC = solicitacao.Agenda.Data.AddDays(parametrosAmbulatoriais.Validade_Codigo);
                    DateTime ultimaDiaDataFinalAPAC = new DateTime(dataFinalAPAC.Year, dataFinalAPAC.Month, 1).AddMonths(1).AddDays(-1);
                    html += "<p><span class=tooltipInfo>Validade: </span><br /><span class=tooltipDados>" + solicitacao.Agenda.Data.ToShortDateString() + " </span> até <span><b>" + ultimaDiaDataFinalAPAC.ToShortDateString() + "</b></span></p>";
                    html += "<p><span class=tooltipInfo>Prontuário: </span><br /><span class=tooltipDados>" + solicitacao.Prontuario.ToString() == "0" ? string.Empty : solicitacao.Prontuario.ToString() + " </span></p>";
                }
            }
            else
            {
                html += "<p><span class=tooltipInfo>Médico Regulador: </span class=tooltipDados><br /><span> -  </span></p>";
                html += "<p><span class=tooltipInfo>CNS: </span><br /><span class=tooltipDados> -  </span></p>";
                html += "<p><span class=tooltipInfo>Validade: </span><br /><span class=tooltipDados> - </span></p>";
                html += "<p><span class=tooltipInfo>Prontuário: </span><br /><span class=tooltipDados> -  </span></p>";
            }
            return html;
        }

        void VerificaEDesabilitaColunaAPAC(Agenda agenda)
        {
            //Verifica se o procedimento é do TIPO APAC
            if (Factory.GetInstance<IRegistro>().BuscarPorProcedimento<ProcedimentoRegistro>(agenda.Procedimento.Codigo, Registro.APAC_PROC_PRINCIPAL).Count != 0)
                //Se é APAC, Habilita a coluna na GridView de Solicitações para exibição dos Dados da APAC
                GridViewSolicitacoes.Columns[7].Visible = true;
            else
                GridViewSolicitacoes.Columns[7].Visible = false;
        }

        void DesabilitaBotaoEdicaoSolicitacoes()
        {
            for (int i = 0; i < GridViewSolicitacoes.Rows.Count; i++)
            {
                Image imgAPAC = (Image)GridViewSolicitacoes.Rows[i].Cells[7].FindControl("imgAPAC");

                if (GridViewSolicitacoes.Rows[i].Cells[6].Text == "CONFIRMADA" || GridViewSolicitacoes.Rows[i].Cells[6].Text == "FALTOSO")
                {
                    if (GridViewSolicitacoes.Rows[i].Cells[6].Text == "CONFIRMADA")
                    {
                        if (gridAgenda.Columns[7].Visible == true)//Se a coluna para exibição dos Dados da APAC estiver habilitada e a solicitação confirmada, habilito a visualização dos dados da APAC
                        {
                            imgAPAC.Visible = true;
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "ok" + imgAPAC.ClientID, "AplicarTooltipAPAC('" + imgAPAC.ClientID + "', '" + RetornaHtmlComInformacoesAPAC(int.Parse(GridViewSolicitacoes.DataKeys[i].Value.ToString())) + "');", true);
                        }
                        else
                            imgAPAC.Visible = false;
                    }
                    GridViewSolicitacoes.Rows[i].Cells[8].FindControl("btnEditarSolicitacao").Visible = false;
                }
                else
                    imgAPAC.Visible = false;

            }
        }

        void RecarregaGridViewSolicitacoes(Solicitacao solicitacao)
        {
            DataTable tableSolicitacoes = (DataTable)Session["ListaSolicitacoes"];
            DataRow row = tableSolicitacoes.Rows.Find(solicitacao.Codigo.ToString());
            if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString())
            {
                row["Identificador"] = solicitacao.Identificador;
                //Se Exigir CID
                //if (Factory.GetInstance<IRegistro>().ProcedimentoExigeCid(solicitacao.Agenda.Procedimento.Codigo))
                //    row["CID"] = solicitacao.CidExecutante.Nome;
                //else
                //    row["CID"] = string.Empty;

                //Se for APAC
                if (Factory.GetInstance<IRegistro>().BuscarPorProcedimento<ProcedimentoRegistro>(solicitacao.Agenda.Procedimento.Codigo, Registro.APAC_PROC_PRINCIPAL).Count != 0) // Se for APAC
                    row["Prontuario"] = solicitacao.Prontuario.ToString();
                else
                    row["Prontuario"] = string.Empty;
            }
            else
            {
                row["Identificador"] = string.Empty;
                row["CID"] = string.Empty;
                row["Prontuario"] = string.Empty;
            }

            row["NomeSituacao"] = solicitacao.NomeSituacao;
            if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.FALTOSO).ToString())
                row["Faltoso"] = true;
            else
                row["Faltoso"] = false;

            GridViewSolicitacoes.DataSource = tableSolicitacoes;
            GridViewSolicitacoes.DataBind();
            Session["ListaSolicitacoes"] = tableSolicitacoes;
            DesabilitaBotaoEdicaoSolicitacoes();
        }

        protected void GridViewSolicitacoes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;
            string id_solicitacao = GridViewSolicitacoes.DataKeys[index].Value.ToString();
            Solicitacao solicitacao = Factory.GetInstance<ISolicitacao>().BuscarPorCodigo<Solicitacao>(int.Parse(id_solicitacao));
            Session["IndexSolicitacaoSelecionada"] = index;
            GridViewSolicitacoes.EditIndex = e.NewEditIndex;
            CheckBox chkBox = (CheckBox)GridViewSolicitacoes.Rows[index].FindControl("chkBoxFalta");
            TextBox tbxIdentificador = (TextBox)GridViewSolicitacoes.Rows[index].FindControl("tbxIdentificador");
            //DropDownList ddlCID = (DropDownList)GridViewSolicitacoes.Rows[index].FindControl("ddlCID");
            //ddlCID.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
            //Carregad os CIDS Vinculados ao PRocedimento e ao Sexo do Paciente da Solicitação
            ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
            //ddlCID.DataSource = Factory.GetInstance<ICid>().BuscarPorProcedimento<ProcedimentoCid>(solicitacao.Agenda.Procedimento.Codigo, paciente.Sexo.ToString()).Select(p => p.Cid).OrderBy(cid => cid.Nome).ToList();
            if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString() || solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.FALTOSO).ToString())
            {
                tbxIdentificador.Enabled = false;
                chkBox.Enabled = false;
                //if (solicitacao.CidExecutante != null)
                //{
                //    ddlCID.SelectedValue = solicitacao.CidExecutante.Codigo;
                //}
                //else
                //{
                //    ddlCID.SelectedValue = "0";
                //    //ddlCID.Enabled = true;
                //}
                //ddlCID.DataBind();
                //ddlCID.Enabled = false;
            }
            else
            {
                tbxIdentificador.Enabled = true;
                chkBox.Enabled = true;
                //ddlCID.DataBind();
                //ddlCID.Enabled = true;
            }

            RecarregaGrid();
            UpdatePanelSolicitacaoes.Update();
        }

        protected void GridViewSolicitacoes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewSolicitacoes.EditIndex = -1;
            RecarregaGrid();
            Session.Remove("IndexSolicitacaoSelecionada");
        }

        protected void GridViewSolicitacoes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = int.Parse(Session["IndexSolicitacaoSelecionada"].ToString());
            string id_solicitacao = GridViewSolicitacoes.DataKeys[index].Value.ToString();
            Solicitacao solicitacao = Factory.GetInstance<ISolicitacao>().BuscarPorCodigo<Solicitacao>(int.Parse(id_solicitacao));
            TextBox tbxIdentificador = (TextBox)GridViewSolicitacoes.Rows[index].FindControl("tbxIdentificador");
            CheckBox chkBox = (CheckBox)GridViewSolicitacoes.Rows[index].FindControl("chkBoxFalta");
            DropDownList ddlCID = (DropDownList)GridViewSolicitacoes.Rows[index].FindControl("ddlCID");
            //TextBox tbxCID = (TextBox)GridViewSolicitacoes.Rows[index].FindControl("tbxCID");
            TextBox tbxProntuario = (TextBox)GridViewSolicitacoes.Rows[index].FindControl("tbxProntuario");
            if (!String.IsNullOrEmpty(tbxIdentificador.Text))
            {
                if (solicitacao.Identificador == tbxIdentificador.Text)
                {
                    solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString();
                    solicitacao.Data_Confirmacao = DateTime.Now;
                    if (Factory.GetInstance<IRegistro>().ProcedimentoExigeCid(solicitacao.Agenda.Procedimento.Codigo))
                    {
                        if (!String.IsNullOrEmpty(ddlCID.SelectedValue) && ddlCID.SelectedValue != "0")
                            solicitacao.CidExecutante = Factory.GetInstance<ICid>().BuscarPorCodigo<Cid>(ddlCID.SelectedValue);
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Cid é Obrigatório!');", true);
                            return;
                        }
                    }
                    //Se o Procedimento for APAC
                    if (Factory.GetInstance<IRegistro>().BuscarPorProcedimento<ProcedimentoRegistro>(solicitacao.Agenda.Procedimento.Codigo, Registro.APAC_PROC_PRINCIPAL).Count != 0) // Se for APAC
                    {
                        if (!String.IsNullOrEmpty(tbxProntuario.Text))
                            solicitacao.Prontuario = long.Parse(tbxProntuario.Text);
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Prontuário é Obrigatório!');", true);
                            return;
                        }
                    }
                    Factory.GetInstance<IAgendamentoServiceFacade>().Atualizar(solicitacao);
                    Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 11, solicitacao.Codigo.ToString()));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Identificador Inválido');", true);
                    return;
                }
            }
            else if (chkBox.Checked) // Se ele informar que o paciente não comparecer à consulta (Faltoso)
            {
                solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.FALTOSO).ToString();
                Factory.GetInstance<IAgendamentoServiceFacade>().Atualizar(solicitacao);
                Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 53, solicitacao.Codigo.ToString()));
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Você deve Informar a situação da solicitação!');", true);
                return;
            }
            GridViewSolicitacoes.EditIndex = -1;
            RecarregaGridViewSolicitacoes(solicitacao);
        }

        protected void tbxData_Final_TextChanged(object sender, EventArgs e)
        {
            DateTime dataFinal;
            if (!DateTime.TryParse(tbxData_Final.Text, out dataFinal))
                return;

            IFPO iFpo = Factory.GetInstance<IFPO>();
            //Vou buscar a FPO da Unidade e listar os Procedimentos que poderão ser selecionados
            Usuario usuario = (Usuario)Session["Usuario"];
            String[] codigosProcedimentos = iFpo.BuscarFPO<FPO>(usuario.Unidade.CNES, int.Parse((dataFinal.Year.ToString("0000") + dataFinal.Month.ToString("00")).ToString())).Select(fpo => fpo.Procedimento.Codigo).Distinct().ToArray();

            List<DropDownList> drops = new List<DropDownList>() { ddlCBO, ddlProfissional, ddlSubGrupo };
            ClearDropDownList(drops);

            List<Procedimento> procedimentos = new List<Procedimento>();

            foreach (String codigo in codigosProcedimentos)
                procedimentos.Add(iFpo.BuscarPorCodigo<Procedimento>(codigo));

            procedimentos = procedimentos.OrderBy(proced => proced.Nome).ToList();
            //ddlProcedimento.Items.Clear();
            foreach (Procedimento procedimento in procedimentos)
                if (ddlProcedimento.Items.FindByValue(procedimento.Codigo) == null)
                    ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo));

            if (ddlProcedimento.Items.FindByValue("0") == null)
                ddlProcedimento.Items.Insert(0, new ListItem("Selecione...", "0"));
            ddlProcedimento.Focus();
            ddlProcedimento.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        protected void tbxData_Inicial_TextChanged(object sender, EventArgs e)
        {
            //if (!compareDataInicial.IsValid)
            //    return;
            DateTime dataInicial;
            if (!DateTime.TryParse(tbxData_Inicial.Text, out dataInicial))
                return;


            IFPO iFpo = Factory.GetInstance<IFPO>();
            //Vou buscar a FPO da Unidade e listar os Procedimentos que poderão ser selecionados
            Usuario usuario = (Usuario)Session["Usuario"];
            String[] codigosProcedimentos = iFpo.BuscarFPO<FPO>(usuario.Unidade.CNES, int.Parse((dataInicial.Year.ToString("0000") + dataInicial.Month.ToString("00")).ToString())).Select(fpo => fpo.Procedimento.Codigo).Distinct().ToArray();

            List<DropDownList> drops = new List<DropDownList>() { ddlCBO, ddlProfissional, ddlSubGrupo };
            ClearDropDownList(drops);

            List<Procedimento> procedimentos = new List<Procedimento>();

            foreach (String codigo in codigosProcedimentos)
                procedimentos.Add(iFpo.BuscarPorCodigo<Procedimento>(codigo));

            procedimentos = procedimentos.OrderBy(proced => proced.Nome).ToList();
            ddlProcedimento.Items.Clear();
            foreach (Procedimento procedimento in procedimentos)
                ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo));
            ddlProcedimento.Items.Insert(0, new ListItem("Selecione...", "0"));
            tbxData_Final.Focus();
            ddlProcedimento.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        protected void ddlProcedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProcedimento.SelectedValue != "0")
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                IList<ViverMais.Model.CBO> cbosDoProcedimento = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(ddlProcedimento.SelectedValue);

                // Monta lista de CBO ligados ao Vinculo do CNES
                IList<CBO> cbosDoVinculo = Factory.GetInstance<IVinculo>().BuscarCbosDaUnidade<CBO>(usuario.Unidade.CNES).Distinct().ToList();
                ClearDropDownList(new List<DropDownList>() { ddlCBO, ddlProfissional, ddlSubGrupo });

                var intersecao = from result in cbosDoVinculo
                                 where
                                     cbosDoProcedimento.Select(p => p.Codigo).ToList().Contains(result.Codigo)
                                 select result;
                foreach (CBO cbo in intersecao)
                    ddlCBO.Items.Add(new ListItem(cbo.Nome, cbo.Codigo));

                ddlCBO.Focus();
                UpdatePanelCbo.Update();
            }
        }

        protected void ddlCBO_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Carrega os SubGrupos Vinculados a Especialidade e Procedimento
            IList<SubGrupo> subGrupos = Factory.GetInstance<ISubGrupoProcedimentoCbo>().ListarSubGrupoPorProcedimentoECbo<SubGrupo>(ddlProcedimento.SelectedValue, ddlCBO.SelectedValue, true);
            foreach (SubGrupo subGrupo in subGrupos)
                ddlSubGrupo.Items.Add(new ListItem(subGrupo.NomeSubGrupo, subGrupo.Codigo.ToString()));
            UpdatePanelSubGrupo.Update();
            List<DropDownList> drops = new List<DropDownList>() { ddlProfissional, ddlSubGrupo };
            ClearDropDownList(drops);
            Usuario usuario = (Usuario)Session["Usuario"];
            // Monta lista de Profissionais ligados ao Vinculo do CNES
            IList<ViverMais.Model.VinculoProfissional> vinculo = Factory.GetInstance<IVinculo>().BuscarPorCNESCBO<ViverMais.Model.VinculoProfissional>(usuario.Unidade.CNES, ddlCBO.SelectedValue.ToString()).Where(p => p.Status == Convert.ToChar(VinculoProfissional.DescricaStatus.Ativo).ToString()).ToList().Distinct().ToList();

            foreach (ViverMais.Model.VinculoProfissional f in vinculo)
                if (ddlProfissional.Items.FindByValue(f.Profissional.CPF) == null)
                    if (f.Profissional != null)
                        ddlProfissional.Items.Add(new ListItem(f.Profissional.Nome, f.Profissional.CPF));
            UpdatePanelProfissional.Update();
        }
    }
}