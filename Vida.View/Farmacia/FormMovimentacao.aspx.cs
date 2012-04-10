﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Inventario;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormMovimentacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                bool permissao_doacao = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DOACAO", Modulo.FARMACIA);
                bool permissao_devolucao_paciente = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DEVOLUCAO_PACIENTE", Modulo.FARMACIA);
                bool permissao_emprestimo = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_EMPRESTIMO", Modulo.FARMACIA);
                bool permissao_perda = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_PERDA", Modulo.FARMACIA);
                bool permissao_remanejamento = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_REMANEJAMENTO", Modulo.FARMACIA);
                bool permissao_transferencia = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_TRANSFERENCIA_INTERNA", Modulo.FARMACIA);
                bool permissao_acerto_balanco = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_ACERTO_BALANCO", Modulo.FARMACIA);

                if (Request["co_movimento"] == null)
                {
                    if (permissao_acerto_balanco || permissao_devolucao_paciente || permissao_doacao 
                        || permissao_emprestimo || permissao_perda || permissao_remanejamento || permissao_transferencia)
                        CarredaDadosIniciais(); 
                    else 
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                }

                else
                {
                    if (Request["co_movimento"] != null && !Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PESQUISAR_MOVIMENTACAO", Modulo.FARMACIA))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                    else
                        CarredaDadosIniciais();
                }
            }
        }

        /// <summary>
        /// Carrega os dados inicias da movimentação sejam eles para cadastro ou visualização
        /// </summary>
        private void CarredaDadosIniciais()
        {
            int temp;

            if (Request["tipo"] != null && int.TryParse(Request["tipo"].ToString(), out temp) && Request["co_farmacia"] != null && int.TryParse(Request["co_farmacia"].ToString(), out temp))
            {
                ViewState["tipo"] = Request["tipo"].ToString();
                ViewState["co_farmacia"] = Request["co_farmacia"].ToString();

                if (Request["co_situacao"] == null)
                    HabilitaPanelMovimento(int.Parse(Request["tipo"].ToString()), int.Parse(Request["co_farmacia"].ToString()), -1);
                else
                {
                    if (int.Parse(Request["co_situacao"].ToString()) == TipoOperacaoMovimento.ENTRADA || int.Parse(Request["co_situacao"].ToString()) == TipoOperacaoMovimento.SAIDA)
                    {
                        ViewState["co_situacao"] = Request["co_situacao"].ToString();
                        HabilitaPanelMovimento(int.Parse(Request["tipo"].ToString()), int.Parse(Request["co_farmacia"].ToString()), int.Parse(Request["co_situacao"].ToString()));

                        //if (int.Parse(Request["tipo"].ToString()) == 2) //Doação
                        //{
                        //    if (int.Parse(Request["co_farmacia"].ToString()) == Convert.ToInt32(ViverMais.Model.Farmacia.QualFarmacia.Almoxarifado))
                        //        HabilitaPanelMovimento(int.Parse(Request["tipo"].ToString()), int.Parse(Request["co_farmacia"].ToString()), int.Parse(Request["co_situacao"].ToString()));
                        //    else
                        //        if (int.Parse(Request["co_situacao"].ToString()) == 2)
                        //            HabilitaPanelMovimento(int.Parse(Request["tipo"].ToString()), int.Parse(Request["co_farmacia"].ToString()), int.Parse(Request["co_situacao"].ToString()));
                        //}else
                        //    HabilitaPanelMovimento(int.Parse(Request["tipo"].ToString()), int.Parse(Request["co_farmacia"].ToString()), int.Parse(Request["co_situacao"].ToString()));
                    }
                }

                if (int.Parse(Request["tipo"].ToString()) == TipoMovimento.REMANEJAMENTO ||
                    int.Parse(Request["tipo"].ToString()) == TipoMovimento.PERDA ||
                    int.Parse(Request["tipo"].ToString()) == TipoMovimento.TRANSFERENCIA_INTERNA)
                {
                    this.MostrarMedicamentos(TipoOperacaoMovimento.SAIDA, int.Parse(Request["co_farmacia"].ToString()));
                }
                else
                    if (int.Parse(Request["tipo"].ToString()) == TipoMovimento.DEVOLUCAO_PACIENTE)
                        this.MostrarMedicamentos(TipoOperacaoMovimento.ENTRADA, int.Parse(Request["co_farmacia"].ToString()));
            }

            if (Request["co_situacao"] != null)
            {
                this.MostrarMedicamentos(int.Parse(Request["co_situacao"].ToString()), int.Parse(Request["co_farmacia"].ToString()));
                //if (int.Parse(Request["co_situacao"].ToString()) == TipoOperacaoMovimento.ENTRADA)
                //{
                //    List<Medicamento> medicamentos = Factory.GetInstance<IMedicamento>().ListarTodos<Medicamento>().OrderBy(p => p.Nome).ToList();
                //    DropDownList_MedicamentoMovimentacao.DataSource = medicamentos;
                //    DropDownList_MedicamentoMovimentacao.DataBind();
                //}
                //else
                //{
                //    if (int.Parse(Request["co_situacao"].ToString()) == TipoOperacaoMovimento.SAIDA)
                //    {
                //        IList<Estoque> itensEstoqueFarmacia = Factory.GetInstance<IEstoque>().BuscarPorFarmacia<Estoque>(Convert.ToInt32(Request["co_farmacia"]));
                //        var itensUnicosEstoqueFarmacia = from itemEstoqueFarmacia in itensEstoqueFarmacia group itemEstoqueFarmacia by itemEstoqueFarmacia.NomeMedicamento;
                //        List<Medicamento> lm = new List<Medicamento>();
                //        foreach (var primeiroItemEstoque in itensUnicosEstoqueFarmacia)
                //        {
                //            lm.Add(primeiroItemEstoque.ElementAt(0).Medicamento);
                //        }
                //        foreach (Medicamento m in lm)
                //            DropDownList_MedicamentoMovimentacao.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));
                //    }
                //}

                //switch (Request["co_situacao"])
                //{
                //    case "1": //Entrada
                //        List<Medicamento> medicamentos = Factory.GetInstance<IMedicamento>().ListarTodos<Medicamento>().OrderBy(p => p.Nome).ToList();
                //        DropDownList_MedicamentoMovimentacao.DataSource = medicamentos;
                //        DropDownList_MedicamentoMovimentacao.DataBind();
                //        break;

                //    case "2": //Saida
                //        IList<Estoque> itensEstoqueFarmacia = Factory.GetInstance<IEstoque>().BuscarPorFarmacia<Estoque>(Convert.ToInt32(Request["co_farmacia"]));
                //        var itensUnicosEstoqueFarmacia = from itemEstoqueFarmacia in itensEstoqueFarmacia group itemEstoqueFarmacia by itemEstoqueFarmacia.NomeMedicamento;
                //        List<Medicamento> lm = new List<Medicamento>();
                //        foreach (var primeiroItemEstoque in itensUnicosEstoqueFarmacia)
                //        {
                //            lm.Add(primeiroItemEstoque.ElementAt(0).Medicamento);
                //        }
                //        foreach (Medicamento m in lm)
                //            DropDownList_MedicamentoMovimentacao.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));
                //        break;
                //}
            }

            DropDownList_MedicamentoMovimentacao.Items.Insert(0, new ListItem("Selecione...", "-1", true));
            DropDownList_MedicamentoMovimentacao.Attributes.Add("onmouseover", "javascript:showTooltip(this);");

            Session.Remove("medicamentos_movimentacao");
            CarregaItensMovimentacao(new List<ItemMovimentacao>());

            if (Request["co_movimento"] != null && int.TryParse(Request["co_movimento"].ToString(), out temp)) //Visualizar somente informações
            {
                ViewState["co_movimento"] = Request["co_movimento"].ToString();
                PreencherInformacoesMovimento(int.Parse(Request["co_movimento"].ToString()));
            }
        }

        private void MostrarMedicamentos(int operacao, int co_farmacia)
        {
            if (operacao == TipoOperacaoMovimento.ENTRADA)
            {
                List<Medicamento> medicamentos = Factory.GetInstance<IMedicamento>().ListarTodos<Medicamento>().OrderBy(p => p.Nome).ToList();
                DropDownList_MedicamentoMovimentacao.DataSource = medicamentos;
                DropDownList_MedicamentoMovimentacao.DataBind();
            }
            else
            {
                if (operacao == TipoOperacaoMovimento.SAIDA) //Convert.ToInt32(Request["co_farmacia"])
                {
                    IList<Estoque> itensEstoqueFarmacia = Factory.GetInstance<IEstoque>().BuscarPorFarmacia<Estoque>(co_farmacia);
                    var itensUnicosEstoqueFarmacia = from itemEstoqueFarmacia in itensEstoqueFarmacia group itemEstoqueFarmacia by itemEstoqueFarmacia.NomeMedicamento;
                    List<Medicamento> lm = new List<Medicamento>();
                    foreach (var primeiroItemEstoque in itensUnicosEstoqueFarmacia)
                    {
                        lm.Add(primeiroItemEstoque.ElementAt(0).Medicamento);
                    }
                    foreach (Medicamento m in lm)
                        DropDownList_MedicamentoMovimentacao.Items.Add(new ListItem(m.Nome, m.Codigo.ToString()));
                }
            }
        }

        /// <summary>
        /// Preenche as informações da movimentação para visualização
        /// </summary>
        /// <param name="co_movimento">código do movimento</param>
        private void PreencherInformacoesMovimento(int co_movimento)
        {
            Movimento m = Factory.GetInstance<IMovimentacao>().BuscarPorCodigo<Movimento>(co_movimento);

            HabilitaPanelMovimento(m.TipoMovimento.Codigo, m.Farmacia.Codigo, m.TipoOperacaoMovimento != null ? m.TipoOperacaoMovimento.Codigo : -1);

            Session["medicamentos_movimentacao"] = Factory.GetInstance<IMovimentacao>().BuscarItensPorMovimento<ItemMovimentacao>(m.Codigo);
            CarregaItensMovimentacao((IList<ItemMovimentacao>)Session["medicamentos_movimentacao"]);

            Accordion_IncluirMedicamento.Enabled = true;
            AccordionPane_IncluirMedicamentos.Enabled = false;
            AccordionPane_MecicamentosCadastrados.Enabled = true;

            if (m.TipoMovimento.Codigo == TipoMovimento.DEVOLUCAO_PACIENTE)
            {
                TextBox_ObservacaoDevolucaoPaciente.Text = m.Observacao;
                ButtonSalvarDevolucaoPaciente.Visible = false;
            }
            else
            {
                if (m.TipoMovimento.Codigo == TipoMovimento.DOACAO)
                {
                    DropDownList_SituacaoDoacao.SelectedValue = m.TipoOperacaoMovimento.Codigo.ToString();
                    DropDownList_MotivoDoacao.SelectedValue = m.Motivo.Codigo.ToString();

                    DropDownList_EstabelecimentoAssistencialDoacao.SelectedValue = m.CodigoUnidade;
                    TextBox_ResponsavelEnvioDoacao.Text = m.Responsavel_Envio;
                    TextBox_ResponsavelRecebimentoDoacao.Text = m.Responsavel_Recebimento;
                    TextBox_DataEnvioDoacao.Text = m.Data_Envio.Value.ToString("dd/MM/yyyy");
                    TextBox_DataRecebimentoDoacao.Text = m.Data_Recebimento.Value.ToString("dd/MM/yyyy");
                    TextBox_ObservacaoDoacao.Text = m.Observacao;
                    ButtonSalvarDoacao.Visible = false;
                }
                else
                {
                    if (m.TipoMovimento.Codigo == TipoMovimento.EMPRESTIMO)
                    {
                        DropDownList_SituacaoEmprestimo.SelectedValue = m.TipoOperacaoMovimento.Codigo.ToString();

                        Session["medicamentos_movimentacao"] = Factory.GetInstance<IMovimentacao>().BuscarItensPorMovimento<ItemMovimentacao>(m.Codigo);
                        CarregaItensMovimentacao((IList<ItemMovimentacao>)Session["medicamentos_movimentacao"]);

                        DropDownList_MotivoEmprestimo.SelectedValue = m.Motivo.Codigo.ToString();
                        DropDownList_EstabelecimentoAssistencialEmprestimo.SelectedValue = m.CodigoUnidade;
                        TextBox_ResponsavelEnvioEmprestimo.Text = m.Responsavel_Envio;
                        TextBox_ResponsavelRecebimentoEmprestimo.Text = m.Responsavel_Recebimento;
                        TextBox_DataEnvioEmprestimo.Text = m.Data_Envio.Value.ToString("dd/MM/yyyy");
                        TextBox_DataRecebimentoEmprestimo.Text = m.Data_Recebimento.Value.ToString("dd/MM/yyyy");
                        TextBox_ObservacaoEmprestimo.Text = m.Observacao;
                        ButtonSalvarEmprestimo.Visible = false;
                    }
                    else
                    {
                        if (m.TipoMovimento.Codigo == TipoMovimento.PERDA)
                        {
                            DropDownList_MotivoPerda.SelectedValue = m.Motivo.Codigo.ToString();
                            TextBox_ObservacaoPerda.Text = m.Observacao;
                            ButtonSalvarPerda.Visible = false;
                        }
                        else
                        {
                            if (m.TipoMovimento.Codigo == TipoMovimento.REMANEJAMENTO)
                            {
                                DropDownList_FarmaciaDestinoRemanejamento.SelectedValue = m.Farmacia_Destino.Codigo.ToString();
                                TextBox_ResponsavelEnvioRemanejamento.Text = m.Responsavel_Envio;
                                TextBox_ResponsavelRecebimentoRemanejamento.Text = m.Responsavel_Recebimento;
                                TextBox_DataEnvioRemanejamento.Text = m.Data_Envio.Value.ToString("dd/MM/yyyy");
                                TextBox_DataRecebimentoRemanejamento.Text = m.Data_Recebimento.Value.ToString("dd/MM/yyyy");
                                TextBox_ObservacaoRemanejamento.Text = m.Observacao;
                                ButtonSalvarRemanejamento.Visible = false;
                            }
                            else
                            {
                                if (m.TipoMovimento.Codigo == TipoMovimento.TRANSFERENCIA_INTERNA)
                                {
                                    DropDownList_SetorDestinoTransferenciaInterna.SelectedValue = m.Setor_Destino.Codigo.ToString();
                                    TextBox_ResponsavelEnvioTransferenciaInterna.Text = m.Responsavel_Envio;
                                    TextBox_ResponsavelRecebimentoTransferenciaInterna.Text = m.Responsavel_Recebimento;
                                    TextBox_DataEnvioTransferenciaInterna.Text = m.Data_Envio.Value.ToString("dd/MM/yyyy");
                                    TextBox_DataRecebimentoTransferenciaInterna.Text = m.Data_Recebimento.Value.ToString("dd/MM/yyyy");
                                    TextBox_ObservacaoTransferenciaInterna.Text = m.Observacao;
                                    ButtonSalvarTransferenciaInterna.Visible = false;
                                }
                                else
                                {
                                    if (m.TipoMovimento.Codigo == TipoMovimento.ACERTO_BALANCO)
                                    {
                                        TextBox_DataAcertoBalanco.Text = m.Data.ToString("dd/MM/yyyy");
                                        TextBox_ResponsavelAcertoBalanco.Text = m.ResponsavelMovimento;
                                        TextBox_ObservacaoAcertoBalanco.Text = m.Observacao;
                                        ButtonSalvarAcertoBalanco.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //switch (m.TipoMovimento.Codigo)
            //{
            //    case 1:
            //        TextBox_ObservacaoDevolucaoPaciente.Text = m.Observacao;
            //        ButtonSalvarDevolucaoPaciente.Visible = false;
            //        break;

            //    case 2:
            //        DropDownList_SituacaoDoacao.SelectedValue = m.TipoOperacaoMovimento.Codigo.ToString();
            //        DropDownList_MotivoDoacao.SelectedValue = m.Motivo.Codigo.ToString();

            //        DropDownList_EstabelecimentoAssistencialDoacao.SelectedValue = m.CodigoUnidade;
            //        TextBox_ResponsavelEnvioDoacao.Text = m.Responsavel_Envio;
            //        TextBox_ResponsavelRecebimentoDoacao.Text = m.Responsavel_Recebimento;
            //        TextBox_DataEnvioDoacao.Text = m.Data_Envio.Value.ToString("dd/MM/yyyy");
            //        TextBox_DataRecebimentoDoacao.Text = m.Data_Recebimento.Value.ToString("dd/MM/yyyy");
            //        TextBox_ObservacaoDoacao.Text = m.Observacao;
            //        ButtonSalvarDoacao.Visible = false;
            //        break;

            //    case 3:
            //        DropDownList_SituacaoEmprestimo.SelectedValue = m.TipoOperacaoMovimento.Codigo.ToString();

            //        Session["medicamentos_movimentacao"] = Factory.GetInstance<IMovimentacao>().BuscarItensPorMovimento<ItemMovimentacao>(m.Codigo);
            //        CarregaItensMovimentacao((IList<ItemMovimentacao>)Session["medicamentos_movimentacao"]);

            //        DropDownList_MotivoEmprestimo.SelectedValue = m.Motivo.Codigo.ToString();
            //        DropDownList_EstabelecimentoAssistencialEmprestimo.SelectedValue = m.CodigoUnidade;
            //        TextBox_ResponsavelEnvioEmprestimo.Text = m.Responsavel_Envio;
            //        TextBox_ResponsavelRecebimentoEmprestimo.Text = m.Responsavel_Recebimento;
            //        TextBox_DataEnvioEmprestimo.Text = m.Data_Envio.Value.ToString("dd/MM/yyyy");
            //        TextBox_DataRecebimentoEmprestimo.Text = m.Data_Recebimento.Value.ToString("dd/MM/yyyy");
            //        TextBox_ObservacaoEmprestimo.Text = m.Observacao;
            //        ButtonSalvarEmprestimo.Visible = false;
            //        break;

            //    case 4:
            //        DropDownList_MotivoPerda.SelectedValue = m.Motivo.Codigo.ToString();
            //        TextBox_ObservacaoPerda.Text = m.Observacao;
            //        ButtonSalvarPerda.Visible = false;
            //        break;

            //    case 5:
            //        DropDownList_FarmaciaDestinoRemanejamento.SelectedValue = m.Farmacia_Destino.Codigo.ToString();
            //        TextBox_ResponsavelEnvioRemanejamento.Text = m.Responsavel_Envio;
            //        TextBox_ResponsavelRecebimentoRemanejamento.Text = m.Responsavel_Recebimento;
            //        TextBox_DataEnvioRemanejamento.Text = m.Data_Envio.Value.ToString("dd/MM/yyyy");
            //        TextBox_DataRecebimentoRemanejamento.Text = m.Data_Recebimento.Value.ToString("dd/MM/yyyy");
            //        TextBox_ObservacaoRemanejamento.Text = m.Observacao;
            //        ButtonSalvarRemanejamento.Visible = false;
            //        break;

            //    case 6:
            //        DropDownList_SetorDestinoTransferenciaInterna.SelectedValue = m.Setor_Destino.Codigo.ToString();
            //        TextBox_ResponsavelEnvioTransferenciaInterna.Text = m.Responsavel_Envio;
            //        TextBox_ResponsavelRecebimentoTransferenciaInterna.Text = m.Responsavel_Recebimento;
            //        TextBox_DataEnvioTransferenciaInterna.Text = m.Data_Envio.Value.ToString("dd/MM/yyyy");
            //        TextBox_DataRecebimentoTransferenciaInterna.Text = m.Data_Recebimento.Value.ToString("dd/MM/yyyy");
            //        TextBox_ObservacaoTransferenciaInterna.Text = m.Observacao;
            //        ButtonSalvarTransferenciaInterna.Visible = false;
            //        break;
            //    case 7:
            //        TextBox_DataAcertoBalanco.Text = m.Data.ToString("dd/MM/yyyy");
            //        TextBox_ResponsavelAcertoBalanco.Text = m.ResponsavelMovimento;
            //        TextBox_ObservacaoAcertoBalanco.Text = m.Observacao;
            //        ButtonSalvarAcertoBalanco.Visible = false;
            //        break;
            //}
        
        }

        /// <summary>
        /// Formata o gridvew de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridView(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["co_movimento"] != null)
                    GridView_MedicamentosMovimentacao.Columns[4].Visible = false;
            }
        }

        /// <summary>
        /// Salva a movimentação realizada pelo usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_SalvarMovimentacao(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            Movimento movimentacao = new Movimento();
            movimentacao.Data = DateTime.Now;
            movimentacao.Farmacia = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(int.Parse(ViewState["co_farmacia"].ToString()));
            movimentacao.TipoMovimento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<TipoMovimento>(int.Parse(ViewState["tipo"].ToString()));

            if (Factory.GetInstance<IInventario>().BuscarPorSituacao<Inventario>(Inventario.ABERTO, movimentacao.Farmacia.Codigo).Count() > 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A movimentação não pode ser concluída, pois existe um inventário ABERTO para esta farmácia que deve ser encerrado.');", true);
                return;
            }

            if (RetornaItensMovimentacao().Count() > 0)
            {
                switch (bt.CommandArgument)
                {
                    case "CommandArgument_DevolucaoPaciente":
                        movimentacao.Observacao = TextBox_ObservacaoDevolucaoPaciente.Text;
                        movimentacao.TipoOperacaoMovimento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<TipoOperacaoMovimento>(TipoOperacaoMovimento.ENTRADA);
                        break;

                    case "CommandArgument_Doacao":
                        movimentacao.Motivo = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<MotivoMovimento>(int.Parse(DropDownList_MotivoDoacao.SelectedValue));
                        movimentacao.TipoOperacaoMovimento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<TipoOperacaoMovimento>(int.Parse(ViewState["co_situacao"].ToString()));
                        movimentacao.Responsavel_Envio = TextBox_ResponsavelEnvioDoacao.Text;
                        movimentacao.Responsavel_Recebimento = TextBox_ResponsavelRecebimentoDoacao.Text;
                        movimentacao.Data_Envio = DateTime.Parse(TextBox_DataEnvioDoacao.Text);
                        movimentacao.Data_Recebimento = DateTime.Parse(TextBox_DataRecebimentoDoacao.Text);
                        movimentacao.Observacao = TextBox_ObservacaoDoacao.Text;
                        break;

                    case "CommandArgument_Emprestimo":
                        movimentacao.TipoOperacaoMovimento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<TipoOperacaoMovimento>(int.Parse(ViewState["co_situacao"].ToString()));
                        movimentacao.Motivo = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<MotivoMovimento>(int.Parse(DropDownList_MotivoEmprestimo.SelectedValue));
                        movimentacao.Responsavel_Envio = TextBox_ResponsavelEnvioEmprestimo.Text;
                        movimentacao.Responsavel_Recebimento = TextBox_ResponsavelRecebimentoEmprestimo.Text;
                        movimentacao.Data_Envio = DateTime.Parse(TextBox_DataEnvioEmprestimo.Text);
                        movimentacao.Data_Recebimento = DateTime.Parse(TextBox_DataRecebimentoEmprestimo.Text);
                        movimentacao.Observacao = TextBox_ObservacaoEmprestimo.Text;
                        break;

                    case "CommandArgument_Perda":
                        movimentacao.Motivo = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<MotivoMovimento>(int.Parse(DropDownList_MotivoPerda.SelectedValue));
                        movimentacao.Observacao = TextBox_ObservacaoPerda.Text;
                        movimentacao.TipoOperacaoMovimento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<TipoOperacaoMovimento>(TipoOperacaoMovimento.SAIDA);
                        break;

                    case "CommandArgument_Remanejamento":
                        movimentacao.Farmacia_Destino = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(int.Parse(DropDownList_FarmaciaDestinoRemanejamento.SelectedValue));
                        movimentacao.Responsavel_Envio = TextBox_ResponsavelEnvioRemanejamento.Text;
                        movimentacao.Responsavel_Recebimento = TextBox_ResponsavelRecebimentoRemanejamento.Text;
                        movimentacao.Data_Envio = DateTime.Parse(TextBox_DataEnvioRemanejamento.Text);
                        movimentacao.Data_Recebimento = DateTime.Parse(TextBox_DataRecebimentoRemanejamento.Text);
                        movimentacao.Observacao = TextBox_ObservacaoRemanejamento.Text;
                        movimentacao.TipoOperacaoMovimento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<TipoOperacaoMovimento>(TipoOperacaoMovimento.SAIDA);
                        break;

                    case "CommandArgument_TransferenciaInterna":
                        movimentacao.Setor_Destino = Factory.GetInstance<ISetor>().BuscarPorCodigo<ViverMais.Model.Setor>(int.Parse(DropDownList_SetorDestinoTransferenciaInterna.SelectedValue));
                        movimentacao.Responsavel_Envio = TextBox_ResponsavelEnvioTransferenciaInterna.Text;
                        movimentacao.Responsavel_Recebimento = TextBox_ResponsavelRecebimentoTransferenciaInterna.Text;
                        movimentacao.Data_Envio = DateTime.Parse(TextBox_DataEnvioTransferenciaInterna.Text);
                        movimentacao.Data_Recebimento = DateTime.Parse(TextBox_DataRecebimentoTransferenciaInterna.Text);
                        movimentacao.Observacao = TextBox_ObservacaoTransferenciaInterna.Text;
                        movimentacao.TipoOperacaoMovimento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<TipoOperacaoMovimento>(TipoOperacaoMovimento.SAIDA);
                        break;

                    case "CommandArgument_AcertoBalanco":
                        movimentacao.TipoOperacaoMovimento = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<TipoOperacaoMovimento>(int.Parse(ViewState["co_situacao"].ToString()));
                        movimentacao.ResponsavelMovimento = TextBox_ResponsavelAcertoBalanco.Text;
                        movimentacao.Observacao = TextBox_ObservacaoAcertoBalanco.Text;
                        break;
                }

                try
                {
                    Factory.GetInstance<IEstoque>().MovimentarEstoque<Movimento, ItemMovimentacao>(movimentacao, RetornaItensMovimentacao());
                    Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.SALVAR_MOVIMENTACAO, "id movimento: " + movimentacao.Codigo));

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Movimentação concluída com sucesso.');location='Default.aspx';", true);
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Para finalizar esta movimentação favor incluir pelo menos um medicamento.');", true);
        }

        /// <summary>
        /// Habilita o panel da movimentação de acordo com o seu tipo
        /// </summary>
        /// <param name="tipomovimento">tipo do movimento</param>
        private void HabilitaPanelMovimento(int tipomovimento, int co_farmacia, int co_situacao)
        {
            Label_Farmacia.Text = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(co_farmacia).Nome;
            ViewState.Remove("saida_medicamento");
            Session.Remove("medicamentos_movimentacao");
            CarregaItensMovimentacao(new List<ItemMovimentacao>());
            OnClick_CancelarInclusao(new object(), new EventArgs());
            Accordion_IncluirMedicamento.Visible = false;

            if (tipomovimento == TipoMovimento.DEVOLUCAO_PACIENTE)
            {
                Panel_DevolucaoPaciente.Visible = true;
                Accordion_IncluirMedicamento.Visible = true;
            }
            else
            {
                if (tipomovimento == TipoMovimento.DOACAO)
                {
                    Panel_Doacao.Visible = true;
                    Accordion_IncluirMedicamento.Visible = true;

                    if (co_farmacia != ViverMais.Model.Farmacia.ALMOXARIFADO)
                        ViewState["saida_medicamento"] = true;
                    else
                    {
                        if (co_situacao == TipoOperacaoMovimento.SAIDA)
                            ViewState["saida_medicamento"] = true;
                    }

                    CarregaDropDownMotivo(this.DropDownList_MotivoDoacao, Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<MotivoMovimento>().Where(p => p.TipoMovimento.Codigo == TipoMovimento.DOACAO).OrderBy(p => p.Nome).ToList());
                    CarregaEstabelecimentoAssistencial(this.DropDownList_EstabelecimentoAssistencialDoacao);
                    CarregaDropDownSituacao(this.DropDownList_SituacaoDoacao, Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<TipoOperacaoMovimento>().Where(p => p.Codigo == co_situacao).OrderBy(p => p.Descricao).ToList());
                }
                else
                {
                    if (tipomovimento == TipoMovimento.EMPRESTIMO)
                    {
                        Panel_Emprestimo.Visible = true;
                        Accordion_IncluirMedicamento.Visible = true;

                        if (co_situacao == TipoOperacaoMovimento.SAIDA)
                            ViewState["saida_medicamento"] = true;

                        IList<MotivoMovimento> lmm = co_situacao == TipoOperacaoMovimento.ENTRADA ? Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<MotivoMovimento>().Where(p => p.TipoMovimento.Codigo == TipoMovimento.EMPRESTIMO && p.Codigo != MotivoMovimento.ENVIO).OrderBy(p => p.Nome).ToList() : Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<MotivoMovimento>().Where(p => p.TipoMovimento.Codigo == TipoMovimento.EMPRESTIMO && p.Codigo != MotivoMovimento.SOLICITACAO).OrderBy(p => p.Nome).ToList();
                        CarregaDropDownMotivo(this.DropDownList_MotivoEmprestimo, lmm.ToList());
                        CarregaEstabelecimentoAssistencial(this.DropDownList_EstabelecimentoAssistencialEmprestimo);
                        CarregaDropDownSituacao(this.DropDownList_SituacaoEmprestimo, Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<TipoOperacaoMovimento>().Where(p => p.Codigo == co_situacao).OrderBy(p => p.Descricao).ToList());
                    }
                    else
                    {
                        if (tipomovimento == TipoMovimento.PERDA)
                        {
                            Panel_Perda.Visible = true;
                            Accordion_IncluirMedicamento.Visible = true;
                            ViewState["saida_medicamento"] = true;
                            CarregaDropDownMotivo(this.DropDownList_MotivoPerda, Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<MotivoMovimento>().Where(p => p.TipoMovimento.Codigo == TipoMovimento.PERDA).OrderBy(p => p.Nome).ToList());
                        }
                        else
                        {
                            if (tipomovimento == TipoMovimento.REMANEJAMENTO)
                            {
                                Panel_Remanejamento.Visible = true;
                                Accordion_IncluirMedicamento.Visible = true;
                                ViewState["saida_medicamento"] = true;
                                CarregaFarmaciaDestino(this.DropDownList_FarmaciaDestinoRemanejamento, co_farmacia);
                            }
                            else
                            {
                                if (tipomovimento == TipoMovimento.TRANSFERENCIA_INTERNA)
                                {
                                    Panel_TransferenciaInterna.Visible = true;
                                    Accordion_IncluirMedicamento.Enabled = true;
                                    ViewState["saida_medicamento"] = true;
                                    CarregaSetorDestino(this.DropDownList_SetorDestinoTransferenciaInterna, co_farmacia);
                                }
                                else
                                {
                                    Panel_AcertoBalanco.Visible = true;
                                    Accordion_IncluirMedicamento.Visible = true;

                                    if (co_situacao == TipoOperacaoMovimento.SAIDA)
                                        ViewState["saida_medicamento"] = true;

                                    TextBox_DataAcertoBalanco.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                }
                            }
                        }
                    }
                }
            }

            //    switch (tipomovimento)
            //    {
            //        case 1:
            //            Panel_DevolucaoPaciente.Visible = true;
            //            Accordion_IncluirMedicamento.Enabled = true;
            //            break;
            //        case 2:
            //            Panel_Doacao.Visible = true;
            //            Accordion_IncluirMedicamento.Enabled = true;

            //            if (co_farmacia != Convert.ToInt32(ViverMais.Model.Farmacia.QualFarmacia.Almoxarifado))
            //                ViewState["saida_medicamento"] = true;
            //            else
            //            {
            //                if (co_situacao == TipoOperacaoMovimento.SAIDA)
            //                    ViewState["saida_medicamento"] = true;
            //            }

            //            CarregaDropDownMotivo(this.DropDownList_MotivoDoacao,Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<MotivoMovimento>().Where(p => p.TipoMovimento.Codigo == TipoMovimento.DOACAO).OrderBy(p => p.Nome).ToList());
            //            CarregaEstabelecimentoAssistencial(this.DropDownList_EstabelecimentoAssistencialDoacao);
            //            CarregaDropDownSituacao(this.DropDownList_SituacaoDoacao, Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<TipoOperacaoMovimento>().Where(p => p.Codigo == co_situacao).OrderBy(p => p.Descricao).ToList());
            //            break;
            //        case 3:
            //            Panel_Emprestimo.Visible = true;
            //            Accordion_IncluirMedicamento.Enabled = true;

            //            if (co_situacao == TipoOperacaoMovimento.SAIDA)
            //                ViewState["saida_medicamento"] = true;

            //            IList<MotivoMovimento> lmm = co_situacao == TipoOperacaoMovimento.ENTRADA ? Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<MotivoMovimento>().Where(p => p.TipoMovimento.Codigo == TipoMovimento.EMPRESTIMO && p.Codigo != 8).OrderBy(p => p.Nome).ToList() : Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<MotivoMovimento>().Where(p => p.TipoMovimento.Codigo == TipoMovimento.EMPRESTIMO && p.Codigo != 6).OrderBy(p => p.Nome).ToList();
            //            CarregaDropDownMotivo(this.DropDownList_MotivoEmprestimo, lmm.ToList());
            //            CarregaEstabelecimentoAssistencial(this.DropDownList_EstabelecimentoAssistencialEmprestimo);
            //            CarregaDropDownSituacao(this.DropDownList_SituacaoEmprestimo, Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<TipoOperacaoMovimento>().Where(p => p.Codigo == co_situacao).OrderBy(p => p.Descricao).ToList());
            //            break;
            //        case 4:
            //            Panel_Perda.Visible = true;
            //            Accordion_IncluirMedicamento.Enabled = true;
            //            ViewState["saida_medicamento"] = true;
            //            CarregaDropDownMotivo(this.DropDownList_MotivoPerda, Factory.GetInstance<IFarmaciaServiceFacade>().ListarTodos<MotivoMovimento>().Where(p => p.TipoMovimento.Codigo == TipoMovimento.PERDA).OrderBy(p => p.Nome).ToList());
            //            break;
            //        case 5:
            //            Panel_Remanejamento.Visible = true;
            //            Accordion_IncluirMedicamento.Enabled = true;
            //            ViewState["saida_medicamento"] = true;
            //            CarregaFarmaciaDestino(this.DropDownList_FarmaciaDestinoRemanejamento, co_farmacia);
            //            break;
            //        case 6:
            //            Panel_TransferenciaInterna.Visible = true;
            //            Accordion_IncluirMedicamento.Enabled = true;
            //            ViewState["saida_medicamento"] = true;
            //            CarregaSetorDestino(this.DropDownList_SetorDestinoTransferenciaInterna, co_farmacia);
            //            break;
            //        case 7:
            //            Panel_AcertoBalanco.Visible = true;
            //            Accordion_IncluirMedicamento.Enabled = true;

            //            if (co_situacao == TipoOperacaoMovimento.SAIDA)
            //                ViewState["saida_medicamento"] = true;

            //            TextBox_DataAcertoBalanco.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //            break;
            //    }
        }

        private void CarregaDropDownSituacao(DropDownList dropsituacaco, IList<TipoOperacaoMovimento> lse)
        {
            if (dropsituacaco != null)
            {
                foreach (TipoOperacaoMovimento tom in lse)
                    dropsituacaco.Items.Add(new ListItem(tom.Descricao, tom.Codigo.ToString()));
            }
        }

        private void CarregaEstabelecimentoAssistencial(DropDownList dropestabelecimento)
        {
            if (dropestabelecimento != null)
            {
                dropestabelecimento.Items.Clear();
                dropestabelecimento.Items.Add(new ListItem("Selecione...", "-1"));
                IList<ViverMais.Model.EstabelecimentoSaude> les = Factory.GetInstance<IEstabelecimentoSaude>().ListarEstabelecimentosForaRedeMunicipal<ViverMais.Model.EstabelecimentoSaude>();

                foreach (ViverMais.Model.EstabelecimentoSaude es in les)
                    dropestabelecimento.Items.Add(new ListItem(es.NomeFantasia, es.CNES));
            }
        }

        private void CarregaDropDownMotivo(DropDownList dropmotivo, List<MotivoMovimento> lmm)
        {
            dropmotivo.Items.Clear();
            dropmotivo.Items.Add(new ListItem("Selecione...", "-1"));

            foreach (MotivoMovimento mm in lmm)
                dropmotivo.Items.Add(new ListItem(mm.Nome, mm.Codigo.ToString()));
        }

        private void CarregaFarmaciaDestino(DropDownList dropfarmaciadestino, int co_farmacia)
        {
            if (dropfarmaciadestino != null)
            {
                dropfarmaciadestino.Items.Clear();
                dropfarmaciadestino.Items.Add(new ListItem("Selecione...", "-1"));
                IList<ViverMais.Model.Farmacia> lf = Factory.GetInstance<IFarmacia>().ListarTodos<ViverMais.Model.Farmacia>().Where(p => p.Codigo != co_farmacia).OrderBy(p => p.Nome).ToList();

                foreach (ViverMais.Model.Farmacia f in lf)
                    dropfarmaciadestino.Items.Add(new ListItem(f.Nome, f.Codigo.ToString()));
            }
        }

        private void CarregaSetorDestino(DropDownList dropsetordestino, int co_farmacia)
        {
            if (dropsetordestino != null)
            {
                dropsetordestino.Items.Clear();
                dropsetordestino.Items.Add(new ListItem("Selecione...", "-1"));
                IList<ViverMais.Model.Setor> ls = Factory.GetInstance<ISetor>().BuscarPorEstabelecimento<Setor>(Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(co_farmacia).CodigoUnidade).OrderBy(p => p.Nome).ToList();

                foreach (ViverMais.Model.Setor s in ls)
                    dropsetordestino.Items.Add(new ListItem(s.Nome, s.Codigo.ToString()));
            }
        }

        /// <summary>
        /// Validação para a inclusão do medicamento na lista de movimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_IncluirMedicamento(object sender, EventArgs e)
        {
            if (ValidarInclusaoMedicamento(int.Parse(DropDownList_LoteMovimentacao.SelectedValue), RetornaItensMovimentacao()))
            {
                if (ProsseguirInclusaoItemMovimentacao(int.Parse(DropDownList_LoteMovimentacao.SelectedValue), int.Parse(TextBox_QuantidadeMedicamentoMovimentacao.Text), Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(int.Parse(ViewState["co_farmacia"].ToString())).Codigo))
                    //if (ProsseguirInclusaoItemMovimentacao(int.Parse(DropDownList_LoteMovimentacao.SelectedValue), int.Parse(TextBox_QuantidadeMedicamentoMovimentacao.Text), Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((Usuario)Session["Usuario"]).Codigo).Codigo))
                    IncluirMedicamento(RetornaItensMovimentacao(), int.Parse(DropDownList_LoteMovimentacao.SelectedValue), int.Parse(TextBox_QuantidadeMedicamentoMovimentacao.Text));
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não há quantidade suficiente no estoque para a saída deste medicamento no lote escolhido.Saldo atual: " + ViewState["saldo_atual_estoque"].ToString() + ". Quantidade solicitada: " + TextBox_QuantidadeMedicamentoMovimentacao.Text + ". Por favor, informe outra quantidade.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O medicamento descrito já se encontra na lista de itens incluídos.');", true);
        }

        /// <summary>
        /// Inclui o medicamento para a movimentação corrente
        /// </summary>
        /// <param name="llm">Lista atual de medicamentos</param>
        /// <param name="co_lote">código do lote para inclusão</param>
        /// <param name="qtd">quantidade para movimentação</param>
        private void IncluirMedicamento(IList<ItemMovimentacao> llm, int co_lote, int qtd)
        {
            try
            {
                ItemMovimentacao item = new ItemMovimentacao();
                item.LoteMedicamento = Factory.GetInstance<ILoteMedicamento>().BuscarPorCodigo<LoteMedicamento>(co_lote);
                item.Quantidade = qtd;
                llm.Add(item);
                Session["medicamentos_movimentacao"] = llm;
                CarregaItensMovimentacao(llm);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento incluído com sucesso.');", true);
                OnClick_CancelarInclusao(new object(), new EventArgs());
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Retorna a lista dos itens da corrente movimentação
        /// </summary>
        /// <returns></returns>
        private IList<ItemMovimentacao> RetornaItensMovimentacao()
        {
            return ((IList<ItemMovimentacao>)Session["medicamentos_movimentacao"]) != null ? ((IList<ItemMovimentacao>)Session["medicamentos_movimentacao"]) : new List<ItemMovimentacao>();
        }

        /// <summary>
        /// Carrega os itens da movimentação corrente
        /// </summary>
        /// <param name="llm"></param>
        private void CarregaItensMovimentacao(IList<ItemMovimentacao> llm)
        {
            GridView_MedicamentosMovimentacao.DataSource = llm;
            GridView_MedicamentosMovimentacao.DataBind();
        }

        /// <summary>
        /// Deleta o item do gridview de movimentação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting_Deletar(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = e.RowIndex == 0 ? (GridView_MedicamentosMovimentacao.PageIndex * GridView_MedicamentosMovimentacao.PageSize) : (GridView_MedicamentosMovimentacao.PageIndex * GridView_MedicamentosMovimentacao.PageSize) + e.RowIndex;

                IList<ItemMovimentacao> lim = RetornaItensMovimentacao();
                lim.RemoveAt(index);
                Session["medicamentos_movimentacao"] = lim;
                CarregaItensMovimentacao(lim);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento removido com sucesso.');", true);
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Habilita a edição do item da movimentação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing_Editar(object sender, GridViewEditEventArgs e)
        {
            GridView_MedicamentosMovimentacao.EditIndex = e.NewEditIndex;
            CarregaItensMovimentacao(RetornaItensMovimentacao());
        }

        /// <summary>
        /// Atualiza o item da movimentação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowUpdating_Alterar(object sender, GridViewUpdateEventArgs e)
        {
            int co_lote = int.Parse(GridView_MedicamentosMovimentacao.DataKeys[e.RowIndex]["CodigoLote"].ToString());
            int qtd_solicitada = int.Parse(((TextBox)GridView_MedicamentosMovimentacao.Rows[e.RowIndex].FindControl("TextBox_Quantidade")).Text);

            if (ProsseguirInclusaoItemMovimentacao(co_lote, qtd_solicitada, Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(int.Parse(ViewState["co_farmacia"].ToString())).Codigo))
            //if (ProsseguirInclusaoItemMovimentacao(co_lote, qtd_solicitada, Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((Usuario)Session["Usuario"]).Codigo).Codigo))
            {
                int index = e.RowIndex == 0 ? (GridView_MedicamentosMovimentacao.PageIndex * GridView_MedicamentosMovimentacao.PageSize) : (GridView_MedicamentosMovimentacao.PageIndex * GridView_MedicamentosMovimentacao.PageSize) + e.RowIndex;
                AlterarMedicamento(RetornaItensMovimentacao(), index, qtd_solicitada);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não há quantidade suficiente no estoque para a saída deste medicamento no lote escolhido.Saldo atual: " + ViewState["saldo_atual_estoque"].ToString() + ". Quantidade solicitada: " + qtd_solicitada + ". Por favor, informe outra quantidade.');", true);
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            CarregaItensMovimentacao(RetornaItensMovimentacao());
            GridView_MedicamentosMovimentacao.PageIndex = e.NewPageIndex;
            GridView_MedicamentosMovimentacao.DataBind();
        }

        /// <summary>
        /// Avalia se os dados do medicamento estão corretos para prosseguir com a sua inclusão na movimentação corrente
        /// </summary>
        /// <param name="co_lote">código do lote</param>
        /// <param name="qtd_solicitada">quantidade solicitada</param>
        /// <param name="co_farmacia">código da farmácia</param>
        /// <returns>retorna verdadeiro no caso de inclusão 'ok'</returns>
        private bool ProsseguirInclusaoItemMovimentacao(int co_lote, int qtd_solicitada, int co_farmacia)
        {
            bool temp;

            if (ViewState["saida_medicamento"] != null && bool.TryParse(ViewState["saida_medicamento"].ToString(), out temp) &&
                bool.Parse(ViewState["saida_medicamento"].ToString()))
            {
                Estoque es = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(co_farmacia, co_lote);
                if (es.QuantidadeEstoque < qtd_solicitada)
                {
                    ViewState["saldo_atual_estoque"] = es.QuantidadeEstoque;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Altera a quantidade do medicamento de movimentação
        /// </summary>
        /// <param name="lim">Lista atual de medicamentos para inclusão</param>
        /// <param name="index_item">index para atualização</param>
        /// <param name="qtd_solicitada">quantidade solicitada para o medicamento</param>
        private void AlterarMedicamento(IList<ItemMovimentacao> lim, int index_item, int qtd_solicitada)
        {
            try
            {
                GridView_MedicamentosMovimentacao.EditIndex = -1;
                lim[index_item].Quantidade = qtd_solicitada;
                CarregaItensMovimentacao(lim);
                Session["medicamentos_movimentacao"] = lim;

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Medicamento alterado com sucesso.');", true);
                OnClick_CancelarInclusao(new object(), new EventArgs());
            }
            catch (Exception f)
            {
                throw f;
            }
        }

        /// <summary>
        /// Cancela a edição do item da movimentação
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCancelingEdit_CancelarEdicao(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_MedicamentosMovimentacao.EditIndex = -1;
            CarregaItensMovimentacao(RetornaItensMovimentacao());
        }

        /// <summary>
        /// Valida a inclusão do medicamento para a movimentação corrente
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool ValidarInclusaoMedicamento(int co_lote, IList<ItemMovimentacao> llm)
        {
            if (llm == null || (llm != null && llm.Count() <= 0))
                return true;
            else
                return llm.Where(p => p.LoteMedicamento.Codigo == co_lote).FirstOrDefault() == null ? true : false;
        }

        /// <summary>
        /// Cancela a inclusão do medicamento para a movimentação corrente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarInclusao(object sender, EventArgs e)
        {
            if (DropDownList_MedicamentoMovimentacao.Items.Count > 0)
                DropDownList_MedicamentoMovimentacao.SelectedValue = DropDownList_MedicamentoMovimentacao.Items[0].Value;

            DropDownList_FabricanteMovimentacao.Items.Clear();
            DropDownList_FabricanteMovimentacao.Items.Add(new ListItem("Selecione...", "-1"));

            DropDownList_LoteMovimentacao.Items.Clear();
            DropDownList_LoteMovimentacao.Items.Add(new ListItem("Selecione...", "-1"));

            TextBox_QuantidadeMedicamentoMovimentacao.Text = "";
        }

        /// <summary>
        /// Carrega os fabricantes de acordo com o medicamento selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaFabricante(object sender, EventArgs e)
        {
            DropDownList_FabricanteMovimentacao.Items.Clear();
            DropDownList_FabricanteMovimentacao.Items.Add(new ListItem("Selecione...", "-1"));

            DropDownList_LoteMovimentacao.Items.Clear();
            DropDownList_LoteMovimentacao.Items.Add(new ListItem("Selecione...", "-1"));

            if (DropDownList_MedicamentoMovimentacao.SelectedValue != "-1")
            {
                IList<LoteMedicamento> llm = Factory.GetInstance<ILoteMedicamento>().BuscarPorDescricao<LoteMedicamento>("", DateTime.MinValue, int.Parse(DropDownList_MedicamentoMovimentacao.SelectedValue), -1);
                //IList<LoteMedicamento> llm = Factory.GetInstance<ILoteMedicamento>().BuscarPorEstoqueAlmoxarifado<LoteMedicamento>(Convert.ToInt32(DropDownList_MedicamentoMovimentacao.SelectedValue), Convert.ToInt32(Request["co_farmacia"]));
                var subconsulta = (from i in llm select i.Fabricante).Distinct();
                IEnumerable<FabricanteMedicamento> lf = subconsulta.Cast<FabricanteMedicamento>();

                if (lf.Count() > 0)
                {
                    foreach (FabricanteMedicamento f in lf)
                        DropDownList_FabricanteMovimentacao.Items.Add(new ListItem(f.Nome, f.Codigo.ToString()));
                }
            }
        }

        /// <summary>
        /// Carrega os lotes de acordo com o fabricante e medicamento selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaLote(object sender, EventArgs e)
        {
            bool temp;
            DropDownList_LoteMovimentacao.Items.Clear();
            DropDownList_LoteMovimentacao.Items.Add(new ListItem("Selecione...", "-1"));
            IList<LoteMedicamento> llm = null;

            if (DropDownList_MedicamentoMovimentacao.SelectedValue != "-1" && DropDownList_FabricanteMovimentacao.SelectedValue != "-1")
            {
                if (ViewState["saida_medicamento"] != null && bool.TryParse(ViewState["saida_medicamento"].ToString(), out temp) && bool.Parse(ViewState["saida_medicamento"].ToString()))
                {
                    IList<Estoque> le = Factory.GetInstance<IEstoque>().BuscarPorDescricao<Estoque>(Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(int.Parse(ViewState["co_farmacia"].ToString())).Codigo, int.Parse(DropDownList_FabricanteMovimentacao.SelectedValue), int.Parse(DropDownList_MedicamentoMovimentacao.SelectedValue), string.Empty).Where(p => p.QuantidadeEstoque > 0).ToList();
                    //IList<Estoque> le = Factory.GetInstance<IEstoque>().BuscarPorDescricao<Estoque>(Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia>(((Usuario)Session["Usuario"]).Codigo).Codigo, int.Parse(DropDownList_FabricanteMovimentacao.SelectedValue), int.Parse(DropDownList_MedicamentoMovimentacao.SelectedValue)).Where(p => p.QuantidadeEstoque > 0).ToList();
                    llm = new List<LoteMedicamento>();

                    foreach (Estoque es in le)
                        llm.Add(es.LoteMedicamento);
                }
                else
                    llm = Factory.GetInstance<ILoteMedicamento>().BuscarPorDescricao<LoteMedicamento>("", DateTime.MinValue, int.Parse(DropDownList_MedicamentoMovimentacao.SelectedValue), int.Parse(DropDownList_FabricanteMovimentacao.SelectedValue));

                foreach (LoteMedicamento lm in llm)
                    DropDownList_LoteMovimentacao.Items.Add(new ListItem(lm.Lote, lm.Codigo.ToString()));
            }
        }
    }
}
