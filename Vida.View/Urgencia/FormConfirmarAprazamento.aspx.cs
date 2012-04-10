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
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.DAO;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using System.Globalization;
using System.Drawing;
using AjaxControlToolkit;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Urgencia
{
    public partial class FormConfirmarAprazamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UsuarioTecnico"] == null)
                {
                    Response.Redirect("FormAcessoNegado.aspx?opcao=5");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Profissional não identificado! Por favor, identifique o usuário.');location='Default.aspx';", true);
                }
                else
                {
                    UsuarioProfissionalUrgence usuarioprofissional = ((UsuarioProfissionalUrgence)Session["UsuarioTecnico"]);

                    if (Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(usuarioprofissional.Id_Usuario).UnidadeVinculo == ((Usuario)Session["Usuario"]).Unidade.CNES)
                    {
                        ICBO iCbo = Factory.GetInstance<ICBO>();
                        if (!iCbo.CBOPertenceAuxiliarTecnicoEnfermagem<CBO>(iCbo.BuscarPorCodigo<CBO>(usuarioprofissional.CodigoCBO)))
                        //if (!VerificaFuncionalidadesTecnicoEnfermeiro(usuarioprofissional.Id_Usuario))
                        {
                            Response.Redirect("FormAcessoNegado.aspx?opcao=7");
                            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, somente profissionais técnicos de enfermagem têm acesso a esta página!');location='Default.aspx';", true);
                        }
                        if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(usuarioprofissional.Id_Usuario, "CONFIRMAR_APRAZAMENTO", Modulo.URGENCIA))
                        {
                            Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão de acesso a esta página!');location='Default.aspx';", true);
                        }
                        UsuarioProfissionalUrgence upu = ((UsuarioProfissionalUrgence)Session["UsuarioTecnico"]);
                        ViewState["co_profissional"] = upu.Id_Profissional;
                        ViewState["cbo_profissional"] = upu.CodigoCBO;

                        ViewState["co_usuario"] = usuarioprofissional.Id_Usuario;
                        Session.Remove("UsuarioTecnico");
                    }
                    else
                        Response.Redirect("FormAcessoNegado.aspx?opcao=4");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, o seu profissional identificado não possui vínculo com unidade do usuário logado. Por favor, entrar em contato com a administração.');location='Default.aspx';", true);
                }

                long temp;

                if (Request["codigo"] != null && long.TryParse(Request["codigo"].ToString(), out temp))
                {
                    Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(temp);
                    ViewState["co_prontuario"] = prontuario.Codigo;

                    Factory.GetInstance<IPrescricao>().AtualizarStatusPrescricoesProntuario(prontuario.Codigo);
                    lblNumero.Text = prontuario.NumeroToString;
                    lblData.Text = prontuario.Data.ToString("dd/MM/yyyy");
                    lblPaciente.Text = prontuario.NomePacienteToString;
                    //string.IsNullOrEmpty(prontuario.Paciente.Nome) ? "Não Identificado" : prontuario.Paciente.Nome;

                    //if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
                    //{
                    //    ViverMais.Model.Paciente pacienteViverMais = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

                    //    if (pacienteViverMais != null)
                    //        lblPaciente.Text = pacienteViverMais.Nome;
                    //}

                    Prescricao p = Factory.GetInstance<IPrescricao>().RetornaPrescricaoVigente<Prescricao>(prontuario.Codigo);

                    if (p != null)
                    {
                        ViewState["co_prescricao"] = p.Codigo;
                        //Factory.GetInstance<IPrescricao>().AtualizarStatusItensAprazadosPrescricao<Prescricao>(p);

                        Label_Data.Text = p.Data.ToString("dd/MM/yyyy");
                        Label_Status.Text = p.DescricaoStatus;
                        Label_UltimaDataValida.Text = p.UltimaDataValida.ToString("dd/MM/yyyy HH:mm");

                        Factory.GetInstance<IPrescricao>().AtualizarStatusItensAprazadosPrescricao<Prescricao>(p, true, true, true);

                        CarregaMedicamentosAprazados(p.Codigo);
                        CarregaProcedimentosAprazados(p.Codigo);
                        CarregaProcedimentosNaoFaturaveisAprazados(p.Codigo);
                    }
                }
            }
        }

        protected void OnClick_VerMedicamentosAprazados(object sender, EventArgs e)
        {
            this.Panel_VerTabelaAprazamento.Visible = true;
            this.TextBox_DataPesquisaAprazamento.Text = "";
        }

        protected void OnClick_FecharPesquisaAprazamento(object sender, EventArgs e)
        {
            this.Panel_VerTabelaAprazamento.Visible = false;
        }

        protected void OnClick_PesquisarAprazamento(object sender, EventArgs e)
        {
            DateTime datapesquisa = DateTime.Parse(this.TextBox_DataPesquisaAprazamento.Text);
            long co_prescricao = long.Parse(ViewState["co_prescricao"].ToString());

            Session["documentoImpressaoUrgencia"] = Factory.GetInstance<IRelatorioUrgencia>().ObterRelatorioAprazados(co_prescricao, datapesquisa);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('FormImprimirCrystalReports.aspx?nomearquivo=aprazados.pdf');", true);
        }

        protected void OnClick_HistoricoMedico(object sender, EventArgs e)
        {
            this.DataList_HistoricoMedico.DataSource = Factory.GetInstance<IProntuario>().RetornaHistoricoEnfermagem(long.Parse(ViewState["co_prontuario"].ToString()));
            this.DataList_HistoricoMedico.DataBind();
            this.Panel_Evolucoes.Visible = true;
        }

        protected void OnClick_FecharHistoricoEvolucoes(object sender, EventArgs e)
        {
            this.Panel_Evolucoes.Visible = false;
        }

        protected void OnClick_Cancelar(object sender, EventArgs e)
        {
            TextBox_ObservacaoEvolucaoEnfermagem.Text = "";
        }

        /// <summary>
        /// Monta o datatable usado dentro do grid
        /// </summary>
        /// <returns></returns>
        protected DataTable ConfiguraDataTable(string parametro)
        {
            DataTable tabela = new DataTable();
            switch (parametro)
            {
                case "medicamento": { tabela.Columns.Add("Medicamento", typeof(string)); break; }
                case "procedimento": { tabela.Columns.Add("Procedimento", typeof(string)); break; }
                case "procedimentonaofaturavel": { tabela.Columns.Add("ProcedimentoNaoFaturavel", typeof(string)); break; }
            }

            tabela.Columns.Add("Horarios", typeof(string));
            return tabela;
        }

        /// <summary>
        /// Monta o grid
        /// </summary>
        /// <returns></returns>
        protected GridView ConfiguraGridView(string parametro)
        {
            GridView grid = new GridView();
            grid.Width = Unit.Percentage(100);

            grid.AutoGenerateColumns = false;
            BoundField bf1 = new BoundField();
            BoundField bf2 = new BoundField();
            switch (parametro)
            {
                case "medicamento": { bf1.DataField = "Medicamento"; bf1.HeaderText = "Medicamento/Prescrição"; break; }
                case "procedimento": { bf1.DataField = "Procedimento"; bf1.HeaderText = "Procedimento"; break; }
                case "procedimentonaofaturavel":
                    {
                        bf1.DataField = "ProcedimentoNaoFaturavel";
                        bf1.HeaderText = "Procedimento Não Faturável";
                        break;
                    }
            }

            bf1.ItemStyle.Width = Unit.Parse("350px");
            bf2.DataField = "Horarios";
            bf2.HeaderText = "Horários";
            //bf1.ItemStyle.Width = Unit.Parse("400px");
            grid.Columns.Add(bf1);
            grid.Columns.Add(bf2);

            return grid;
        }

        protected void btnSalvar_Click1(object sender, EventArgs e)
        {
            ViverMais.Model.UsuarioProfissionalUrgence usuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<ViverMais.Model.UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo);

            if (usuarioProfissional != null)
            {
                ViverMais.Model.Prontuario prontuario = Factory.GetInstance<IPrescricao>().BuscarPorCodigo<ViverMais.Model.Prescricao>(long.Parse(ViewState["co_prescricao"].ToString())).Prontuario;
                ViverMais.Model.EvolucaoEnfermagem evolucao = new ViverMais.Model.EvolucaoEnfermagem();

                evolucao.Prontuario = prontuario;
                evolucao.Observacao = TextBox_ObservacaoEvolucaoEnfermagem.Text;
                evolucao.Data = DateTime.Now;
                evolucao.CodigoProfissional = ViewState["co_profissional"].ToString();
                evolucao.CBOProfissional = ViewState["cbo_profissional"].ToString();
                evolucao.Profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(usuarioProfissional.Id_Profissional);

                try
                {
                    Factory.GetInstance<IEvolucaoEnfermagem>().Salvar(evolucao);
                    TextBox_ObservacaoEvolucaoEnfermagem.Text = "";
                    //Registro de Log
                    Factory.GetInstance<IUrgenciaServiceFacade>().Inserir(new LogUrgencia(DateTime.Now, int.Parse(ViewState["co_usuario"].ToString()), 16, "id evolucao:" + evolucao.Codigo));
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados salvos com sucesso!');", true);
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Profissional não identificado! Por favor, identifique o usuário.');", true);
        }

        #region Medicamentos

        private void CarregaMedicamentosAprazados(long co_prescricao)
        {
            IList<AprazamentoMedicamento> lista = Factory.GetInstance<IAprazamento>().BuscarAprazamentoMedicamento<AprazamentoMedicamento>(co_prescricao);
            //esta lista é usada para carregar os proximos horário da confirmação no grid de baixo
            IList<AprazamentoMedicamento> proximasconfirmacoes = new List<AprazamentoMedicamento>();
            int cont = 0;

            //seleciona uma lista com os dias que tem aprazamento
            var consulta = from objeto in lista
                           group objeto by objeto.Horario.Date into valor
                           select new { dia = valor.Key };

            foreach (var data in consulta)
            {
                AccordionPane pane = new AccordionPane();
                GridView grid = null;
                pane.HeaderContainer.Controls.Add(new LiteralControl(data.dia.Date.ToShortDateString()));
                pane.HeaderContainer.ID = "HeaderContainer" + cont;
                pane.ContentContainer.ID = "ContentContainer" + cont;
                pane.ID = "AccordionPaneMedicamento" + cont;
                cont++;

                //seleciona os diferentes medicamentos daquele dia 
                var consultaitem = from objeto in lista
                                   where objeto.Horario.Date == data.dia.Date
                                   group objeto by objeto.CodigoMedicamento into cod
                                   select new { codmed = cod.Key };

                DataTable tabela = ConfiguraDataTable("medicamento");
                DataRow linha;
                //preenche o datatable
                foreach (var item in consultaitem)
                {
                    linha = tabela.NewRow();
                    linha["Medicamento"] = Factory.GetInstance<IMedicamento>().BuscarPorCodigo<Medicamento>(item.codmed).Nome;
                    //pega os aprazamentos do medicamento para o dia atual
                    var consultamedicamento = from objeto in lista
                                              where objeto.Horario.Date == data.dia.Date
                                              && objeto.CodigoMedicamento == item.codmed
                                              select objeto;

                    Label labellinha = new Label();
                    string proximohorario = "-";
                    foreach (var itemaprazamento in consultamedicamento)
                    {
                        Label label = new Label();
                        label.Text = itemaprazamento.HoraAplicacao + " (" + (itemaprazamento.Status == (char)AprazamentoMedicamento.StatusItem.Finalizado
                            ? 'E' : itemaprazamento.Status).ToString() + ") ";
                        labellinha.Text += label.Text;

                        //pega o próximo horário - se for neste dia
                        if ((itemaprazamento.Status == (char)AprazamentoMedicamento.StatusItem.Bloqueado 
                            || itemaprazamento.Status == (char)AprazamentoMedicamento.StatusItem.Ativo) && proximohorario == "-")
                        {
                            proximasconfirmacoes.Add(itemaprazamento);
                        }
                    }

                    linha["Horarios"] = labellinha.Text;


                    tabela.Rows.Add(linha);
                }

                grid = ConfiguraGridView("medicamento");

                grid.DataSource = tabela;
                grid.DataBind();

                pane.ContentContainer.Controls.Add(grid);
                AccordionMedicamento.Panes.Add(pane);
                AccordionMedicamento.DataBind();
            }

            //salva uma lista com os proximos medicamentos a serem aprazados
            Session["proximasconfirmacoes"] = proximasconfirmacoes;
            CarregaProximasConfirmacoes();
        }

        void CarregaProximasConfirmacoes()
        {
            IList<AprazamentoMedicamento> proximos = (IList<AprazamentoMedicamento>)Session["proximasconfirmacoes"];
            if (proximos != null)
            {
                GridView_ConfirmarAprazamentoMedicamento.DataSource = proximos.OrderBy(p => p.Horario);
                GridView_ConfirmarAprazamentoMedicamento.DataBind();
            }
        }

        protected void GridView_ConfirmarAprazamentoMedicamento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ConfirmarAprazamento")
            {
                int index_medicamento = int.Parse(e.CommandArgument.ToString());

                //if (index_medicamento == 0)
                //{
                int codigomedicamento = int.Parse(GridView_ConfirmarAprazamentoMedicamento.DataKeys[index_medicamento]["CodigoMedicamento"].ToString());
                DateTime horario = DateTime.Parse(GridView_ConfirmarAprazamentoMedicamento.DataKeys[index_medicamento]["Horario"].ToString());

                long codigoprescricao = long.Parse(ViewState["co_prescricao"].ToString());

                AprazamentoMedicamento am = Factory.GetInstance<IAprazamento>().BuscarAprazamentoMedicamento<AprazamentoMedicamento>(codigoprescricao, codigomedicamento, horario);
                am.Status = Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado);
                am.HorarioConfirmacaoSistema = DateTime.Now;
                am.HorarioConfirmacao = DateTime.Now;
                am.CodigoProfissionalConfirmacao = ViewState["co_profissional"].ToString();
                am.CBOProfissionalConfirmacao = ViewState["cbo_profissional"].ToString();
                Factory.GetInstance<IUrgenciaServiceFacade>().Atualizar(am);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário de aprazamento confirmado com sucesso!');", true);
                CarregaMedicamentosAprazados(codigoprescricao);
                //}
                //else 
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, existe um procedimento para este medicamento que deve ser confirmado antes!');", true);
            }
        }

        protected void GridViewMedicamento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbconfirmar = (LinkButton)e.Row.FindControl("linkbtn_Confirmar");

                if (GridView_ConfirmarAprazamentoMedicamento.DataKeys[e.Row.RowIndex]["Status"].ToString() == "B")
                {
                    //LinkButton lbconfirmar = ((LinkButton)e.Row.FindControl("linkbtn_Confirmar"));
                    lbconfirmar.Text = "Bloqueado";
                    lbconfirmar.Enabled = false;
                    lbconfirmar.OnClientClick = "";
                }
                else
                {
                    //if (e.Row.RowIndex != 0)
                    //{
                    //    lbconfirmar.Enabled = false;
                    //    lbconfirmar.OnClientClick = "";
                    //}
                    //else
                    lbconfirmar.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }

        #endregion

        #region Procedimento

        private void CarregaProcedimentosAprazados(long co_prescricao)
        {
            IList<AprazamentoProcedimento> lista = Factory.GetInstance<IAprazamento>().BuscarAprazamentoProcedimento<AprazamentoProcedimento>(co_prescricao);
            //esta lista é usada para carregar os proximos horário da confirmação no grid de baixo
            IList<AprazamentoProcedimento> proximasconfirmacoes = new List<AprazamentoProcedimento>();
            int cont = 0;

            //seleciona uma lista com os dias que tem aprazamento
            var consulta = from objeto in lista
                           group objeto by objeto.Horario.Date into valor
                           select new { dia = valor.Key };

            foreach (var data in consulta)
            {
                AccordionPane pane = new AccordionPane();
                GridView grid = null;
                pane.HeaderContainer.Controls.Add(new LiteralControl(data.dia.Date.ToShortDateString()));
                pane.HeaderContainer.ID = "HeaderContainerProcedimento" + cont;
                pane.ContentContainer.ID = "ContentContainerProcedimento" + cont;
                pane.ID = "AccordionPaneProcedimento" + cont;
                cont++;

                //seleciona os diferentes medicamentos daquele dia 
                var consultaitem = from objeto in lista
                                   where objeto.Horario.Date == data.dia.Date
                                   group objeto by objeto.CodigoProcedimento into cod
                                   select new { codproc = cod.Key };


                DataTable tabela = ConfiguraDataTable("procedimento");
                DataRow linha;
                //preenche o datatable
                foreach (var item in consultaitem)
                {
                    linha = tabela.NewRow();
                    linha["Procedimento"] = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(item.codproc).Nome;
                    //pega os aprazamentos do procedimento para o dia atual
                    var consultaprocedimento = from objeto in lista
                                               where objeto.Horario.Date == data.dia.Date
                                               && objeto.CodigoProcedimento == item.codproc
                                               select objeto;

                    Label labellinha = new Label();
                    string proximohorario = "-";
                    foreach (var itemaprazamento in consultaprocedimento)
                    {
                        Label label = new Label();
                        label.Text = itemaprazamento.HoraAplicacao + " (" + (itemaprazamento.Status == (char)AprazamentoProcedimento.StatusItem.Finalizado
                            ? 'E' : itemaprazamento.Status).ToString() + ") ";
                        //if (itemaprazamento.Status == 'F')
                        //    //label.BackColor = Color.LightGreen;
                        //if (itemaprazamento.Status == 'B')
                        //    label.BackColor = Color.LightCoral;
                        labellinha.Text += label.Text;

                        //pega o próximo horário - se for neste dia
                        if ((itemaprazamento.Status == (char)AprazamentoProcedimento.StatusItem.Bloqueado
                            || itemaprazamento.Status == (char)AprazamentoProcedimento.StatusItem.Ativo) && proximohorario == "-")
                            proximasconfirmacoes.Add(itemaprazamento);
                    }

                    linha["Horarios"] = labellinha.Text;

                    tabela.Rows.Add(linha);
                }

                grid = ConfiguraGridView("procedimento");

                grid.DataSource = tabela;
                grid.DataBind();

                pane.ContentContainer.Controls.Add(grid);
                AccordionProcedimento.Panes.Add(pane);
            }

            //salva uma lista com os proximos procedimentos a serem aprazados
            Session["proximasconfirmacoesprocedimento"] = proximasconfirmacoes;
            CarregaProximasConfirmacoesProcedimento();

        }

        void CarregaProximasConfirmacoesProcedimento()
        {
            IList<AprazamentoProcedimento> proximos = (IList<AprazamentoProcedimento>)Session["proximasconfirmacoesprocedimento"];
            if (proximos != null)
            {
                GridView_ConfirmarAprazamentoProcedimento.DataSource = proximos.OrderBy(p => p.Horario);
                GridView_ConfirmarAprazamentoProcedimento.DataBind();
            }
        }

        protected void GridView_ConfirmarAprazamentoProcedimento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ConfirmarAprazamento")
            {
                IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
                int index_procedimento = int.Parse(e.CommandArgument.ToString());
                string codigoprocedimento = GridView_ConfirmarAprazamentoProcedimento.DataKeys[index_procedimento]["CodigoProcedimento"].ToString();

                if (iProcedimento.CBOExecutaProcedimento(codigoprocedimento, ViewState["cbo_profissional"].ToString()))
                {
                    DateTime horario = DateTime.Parse(GridView_ConfirmarAprazamentoProcedimento.DataKeys[index_procedimento]["Horario"].ToString());
                    long codigoprescricao = long.Parse(ViewState["co_prescricao"].ToString());

                    AprazamentoProcedimento ap = Factory.GetInstance<IAprazamento>().BuscarAprazamentoProcedimento<AprazamentoProcedimento>(codigoprescricao, codigoprocedimento, horario);
                    ap.Status = Convert.ToChar(AprazamentoProcedimento.StatusItem.Finalizado);
                    ap.CodigoProfissionalConfirmacao = ViewState["co_profissional"].ToString();
                    ap.CBOProfissionalConfirmacao = ViewState["cbo_profissional"].ToString();
                    ap.HorarioConfirmacaoSistema = DateTime.Now;
                    ap.HorarioConfirmacao = DateTime.Now;

                    Factory.GetInstance<IUrgenciaServiceFacade>().Atualizar(ap);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário de aprazamento confirmado com sucesso!');", true);
                    CarregaProcedimentosAprazados(codigoprescricao);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, de acordo com o seu CBO, você não tem permissão para executar este procedimento.');", true);
            }
        }

        protected void GridViewProcedimento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbconfirmar = (LinkButton)e.Row.FindControl("linkbtn_Confirmar");

                if (GridView_ConfirmarAprazamentoProcedimento.DataKeys[e.Row.RowIndex]["Status"].ToString() == "B")
                {
                    //LinkButton lbconfirmar = ();
                    lbconfirmar.Text = "Bloqueado";
                    lbconfirmar.Enabled = false;
                    lbconfirmar.OnClientClick = "";
                }
                else
                {
                    //if (e.Row.RowIndex != 0)
                    //{
                    //    lbconfirmar.Enabled = false;
                    //    lbconfirmar.OnClientClick = "";
                    //}
                    //else
                    lbconfirmar.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }

        #endregion

        #region Procedimento Não Faturável

        private void CarregaProcedimentosNaoFaturaveisAprazados(long co_prescricao)
        {
            IList<AprazamentoProcedimentoNaoFaturavel> lista = Factory.GetInstance<IAprazamento>().BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(co_prescricao);
            //esta lista é usada para carregar os proximos horário da confirmação no grid de baixo
            IList<AprazamentoProcedimentoNaoFaturavel> proximasconfirmacoes = new List<AprazamentoProcedimentoNaoFaturavel>();
            int cont = 0;

            //seleciona uma lista com os dias que tem aprazamento
            var consulta = from objeto in lista
                           group objeto by objeto.Horario.Date into valor
                           select new { dia = valor.Key };

            foreach (var data in consulta)
            {
                AccordionPane pane = new AccordionPane();
                GridView grid = null;
                pane.HeaderContainer.Controls.Add(new LiteralControl(data.dia.Date.ToShortDateString()));
                pane.HeaderContainer.ID = "HeaderContainerProcedimentoNaoFaturavel" + cont;
                pane.ContentContainer.ID = "ContentContainerProcedimentoNaoFaturavel" + cont;
                pane.ID = "AccordionPaneProcedimentoNaoFaturavel" + cont;
                cont++;

                //seleciona os diferentes pnfs daquele dia 
                var consultaitem = from objeto in lista
                                   where objeto.Horario.Date == data.dia.Date
                                   group objeto by objeto.CodigoProcedimento into cod
                                   select new { codproc = cod.Key };


                DataTable tabela = ConfiguraDataTable("procedimentonaofaturavel");
                DataRow linha;
                //preenche o datatable
                foreach (var item in consultaitem)
                {
                    linha = tabela.NewRow();
                    linha["ProcedimentoNaoFaturavel"] = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ProcedimentoNaoFaturavel>(item.codproc).Nome;
                    //pega os aprazamentos do procedimentonf para o dia atual
                    var consultaprocedimento = from objeto in lista
                                               where objeto.Horario.Date == data.dia.Date
                                               && objeto.CodigoProcedimento == item.codproc
                                               select objeto;

                    Label labellinha = new Label();
                    string proximohorario = "-";
                    foreach (var itemaprazamento in consultaprocedimento)
                    {
                        Label label = new Label();
                        label.Text = itemaprazamento.HoraAplicacao + " (" + (itemaprazamento.Status == (char)AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado
                            ? 'E' : itemaprazamento.Status).ToString() + ") ";
                        //if (itemaprazamento.Status == 'F')
                        //    //label.BackColor = Color.LightGreen;
                        //if (itemaprazamento.Status == 'B')
                        //    label.BackColor = Color.LightCoral;
                        labellinha.Text += label.Text;

                        //pega o próximo horário - se for neste dia
                        if ((itemaprazamento.Status == (char)AprazamentoProcedimentoNaoFaturavel.StatusItem.Bloqueado
                            || itemaprazamento.Status == (char)AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo) && proximohorario == "-")
                            proximasconfirmacoes.Add(itemaprazamento);
                    }

                    linha["Horarios"] = labellinha.Text;
                    tabela.Rows.Add(linha);
                }

                grid = ConfiguraGridView("procedimentonaofaturavel");
                grid.DataSource = tabela;
                grid.DataBind();

                pane.ContentContainer.Controls.Add(grid);
                AccordionProcedimentoNaoFaturavel.Panes.Add(pane);
            }
            //salva uma lista com os proximos procedimentosnf a serem aprazados
            Session["proximasconfirmacoesprocedimentonaofaturavel"] = proximasconfirmacoes;
            CarregaProximasConfirmacoesProcedimentoNaoFaturavel();
        }

        void CarregaProximasConfirmacoesProcedimentoNaoFaturavel()
        {
            IList<AprazamentoProcedimentoNaoFaturavel> proximos = (IList<AprazamentoProcedimentoNaoFaturavel>)Session["proximasconfirmacoesprocedimentonaofaturavel"];
            if (proximos != null)
            {
                GridView_ConfirmarAprazamentoProcedimentoNaoFaturavel.DataSource = proximos.OrderBy(p => p.Horario);
                GridView_ConfirmarAprazamentoProcedimentoNaoFaturavel.DataBind();
            }
        }

        protected void GridView_ConfirmarAprazamentoProcedimentoNaoFaturavel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ConfirmarAprazamento")
            {
                int index_procedimento = int.Parse(e.CommandArgument.ToString());

                int codigoprocedimento = int.Parse(GridView_ConfirmarAprazamentoProcedimentoNaoFaturavel.DataKeys[index_procedimento]["CodigoProcedimento"].ToString());
                DateTime horario = DateTime.Parse(GridView_ConfirmarAprazamentoProcedimentoNaoFaturavel.DataKeys[index_procedimento]["Horario"].ToString());

                long codigoprescricao = long.Parse(ViewState["co_prescricao"].ToString());

                AprazamentoProcedimentoNaoFaturavel ap = Factory.GetInstance<IAprazamento>().BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(codigoprescricao, codigoprocedimento, horario);
                ap.Status = Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado);
                ap.CodigoProfissionalConfirmacao = ViewState["co_profissional"].ToString();
                ap.CBOProfissionalConfirmacao = ViewState["cbo_profissional"].ToString();
                ap.HorarioConfirmacaoSistema = DateTime.Now;
                ap.HorarioConfirmacao = DateTime.Now;

                Factory.GetInstance<IUrgenciaServiceFacade>().Atualizar(ap);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Horário de aprazamento confirmado com sucesso!');", true);
                CarregaProcedimentosNaoFaturaveisAprazados(codigoprescricao);
            }
        }

        protected void GridViewProcedimentoNaoFaturavel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbconfirmar = (LinkButton)e.Row.FindControl("linkbtn_Confirmar");

                if (GridView_ConfirmarAprazamentoProcedimentoNaoFaturavel.DataKeys[e.Row.RowIndex]["Status"].ToString() == "B")
                {
                    //LinkButton lbconfirmar = ();
                    lbconfirmar.Text = "Bloqueado";
                    lbconfirmar.Enabled = false;
                    lbconfirmar.OnClientClick = "";
                }
                else
                {
                    //if (e.Row.RowIndex != 0)
                    //{
                    //    lbconfirmar.Enabled = false;
                    //    lbconfirmar.OnClientClick = "";
                    //}
                    //else
                    lbconfirmar.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }

        #endregion
    }
}