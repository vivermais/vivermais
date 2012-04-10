using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Vacina;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Drawing;

namespace ViverMais.View.Vacina
{
    public partial class FormMovimentacao : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InserirTrigger(this.WUC_PesquisarLote.WUC_LnkListarTodos.UniqueID, "Click", this.UpdatePanel_LotesPesquisados);
            this.InserirTrigger(this.WUC_PesquisarLote.WUC_LnkPesquisar.UniqueID, "Click", this.UpdatePanel_LotesPesquisados);

            this.WUC_PesquisarLote.WUC_LnkListarTodos.Click += new EventHandler(OnClick_ListarTodos);
            this.WUC_PesquisarLote.WUC_LnkPesquisar.Click += new EventHandler(OnClick_Pesquisar);

            if (!IsPostBack)
            {
                this.PreencheInformacoesIniciais((Usuario)Session["Usuario"]);
                this.WUC_PesquisarLote.WUC_PanelLotesPesquisados.Visible = false;

                DropDownList_EAS.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                DropDownList_SalaDestino.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
            }
        }

        private void InserirTrigger(string idcontrole, string nomeevento, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = idcontrole;
            trigger.EventName = nomeevento;
            updatepanel.Triggers.Add(trigger);
            MasterMain mm = (MasterMain)Master.Master;
            ((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(this.FindControl(idcontrole));
        }

        private void CarregaImunos(IList<ItemMovimentoVacina> imunos)
        {
            Session["imunos"] = imunos;
            this.GridView_Imunos.DataSource = imunos;
            this.GridView_Imunos.DataBind();
        }

        protected void OnClick_ListarTodos(object sender, EventArgs e)
        {
            ILoteVacina iLote = Factory.GetInstance<ILoteVacina>();
            this.Panel_LotesPesquisados.Visible = true;

            if (int.Parse(ViewState["co_situacao"].ToString()) == OperacaoMovimentoVacina.SAIDA)
                this.CarregaLotes(iLote.BuscarLotesQuantidadeDisponivel<LoteVacina>(int.Parse(ViewState["co_sala"].ToString())));
            else if (int.Parse(ViewState["co_situacao"].ToString()) == OperacaoMovimentoVacina.ENTRADA)
                this.CarregaLotes(iLote.ListarTodos<LoteVacina>());
        }

        private void CarregaLotes(IList<LoteVacina> _lotes)
        {
            IList<ItemMovimentoVacina> imunos = (IList<ItemMovimentoVacina>)Session["imunos"];
            var lotes = from _lote in _lotes where !imunos.Select(p => p.Lote.Codigo).Contains(_lote.Codigo) select _lote;
            Session["lotes"] = lotes.ToList();
            GridView_LotesPesquisados.DataSource = lotes.ToList();
            GridView_LotesPesquisados.DataBind();

            UpdatePanel_LotesPesquisados.Update();
        }

        protected void OnClick_Pesquisar(object sender, EventArgs e)
        {
            CustomValidator custom = this.WUC_PesquisarLote.WUC_CustomPesquisarLote;
            if (custom.IsValid)
            {
                ILoteVacina iLote = Factory.GetInstance<ILoteVacina>();
                this.Panel_LotesPesquisados.Visible = true;

                if (int.Parse(ViewState["co_situacao"].ToString()) == OperacaoMovimentoVacina.SAIDA)
                    this.CarregaLotes(iLote.BuscarLotesQuantidadeDisponivel<LoteVacina>(int.Parse(ViewState["co_sala"].ToString()),
                        this.WUC_PesquisarLote.WUC_LotePesquisa, this.WUC_PesquisarLote.WUC_ValidadePesquisa,
                        this.WUC_PesquisarLote.WUC_VacinaSelecionadaPesquisa,
                        this.WUC_PesquisarLote.WUC_FabricanteSelecionadoPesquisa, this.WUC_PesquisarLote.WUC_AplicacoesPesquisa));
                else if (int.Parse(ViewState["co_situacao"].ToString()) == OperacaoMovimentoVacina.ENTRADA)
                    this.CarregaLotes(iLote.BuscarLote<LoteVacina>(this.WUC_PesquisarLote.WUC_LotePesquisa,
                        this.WUC_PesquisarLote.WUC_ValidadePesquisa,
                        this.WUC_PesquisarLote.WUC_VacinaSelecionadaPesquisa,
                        this.WUC_PesquisarLote.WUC_FabricanteSelecionadoPesquisa,
                        this.WUC_PesquisarLote.WUC_AplicacoesPesquisa));
            }
        }

        private void PreencheInformacoesIniciais(Usuario usuario)
        {
            //long co_movimentacao;
            int co_sala;
            int co_tipo;
            int co_situacao;
            IVacinaServiceFacade iVacina = Factory.GetInstance<IVacinaServiceFacade>();
            IMotivoMovimentoVacina iMotivo = Factory.GetInstance<IMotivoMovimentoVacina>();
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            IList<ItemMovimentoVacina> imunos = new List<ItemMovimentoVacina>();

            SalaVacina sala = null;
            TipoMovimentoVacina tipomovimento = null;
            OperacaoMovimentoVacina operacaomovimento = null;

            //if (Request["co_movimento"] != null && long.TryParse(Request["co_movimento"].ToString(), out co_movimentacao))
            //{
            //}else{

            if (Request["co_sala"] != null && int.TryParse(Request["co_sala"].ToString(), out co_sala)
                && Request["co_tipo"] != null && int.TryParse(Request["co_tipo"].ToString(), out co_tipo) &&
                Request["co_situacao"] != null && int.TryParse(Request["co_situacao"].ToString(), out co_situacao))
            {
                if (this.VerificarPermissaoExecutarMovimentacao(usuario, co_tipo))
                {
                    IList<InventarioVacina> inventariosAbertos = Factory.GetInstance<IInventarioVacina>().BuscarPorSituacao<InventarioVacina>(Convert.ToChar(InventarioVacina.DescricaoSituacao.Aberto), co_sala);
                    if (inventariosAbertos.Count > 0)
                    {
                        Application["AcessoPagina"] = "Usuário, é necessário fechar o inventário da sala " + inventariosAbertos[0].Sala.Nome + " aberto em " + inventariosAbertos[0].DataInventario.ToString("dd/MM/yyyy") + " antes de continuar com a nova movimentação.";
                        Response.Redirect("FormAcessoNegado.aspx");
                        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, é necessário fechar o inventário aberto em " + inventariosAbertos[0].DataInventario.ToString("dd/MM/yyyy") + " antes de continuar com a nova movimentação.');location='FormEscolheDadosMovimentacao.aspx';", true);
                        //return;
                    }

                    ViewState["co_sala"] = co_sala;
                    ViewState["co_tipo"] = co_tipo;
                    ViewState["co_situacao"] = co_situacao;

                    sala = iVacina.BuscarPorCodigo<SalaVacina>(co_sala);
                    tipomovimento = iVacina.BuscarPorCodigo<TipoMovimentoVacina>(co_tipo);
                    operacaomovimento = iVacina.BuscarPorCodigo<OperacaoMovimentoVacina>(co_situacao);

                    if (co_tipo == TipoMovimentoVacina.PERDA)
                    {
                        DropDownList_Motivo.DataSource = iMotivo.BuscarPorTipoMovimento<MotivoMovimentoVacina>(co_tipo);
                        DropDownList_Motivo.DataBind();
                        this.InserirElementoDefault(this.DropDownList_Motivo);
                        this.Panel_MotivoMovimento.Visible = true;
                        this.CompareValidator_Motivo.Enabled = true;
                    }
                    else if (co_tipo == TipoMovimentoVacina.DEVOLUCAO)
                        this.RequiredFieldValidator_Observacao.Enabled = true;
                    else if (co_tipo == TipoMovimento.EMPRESTIMO || co_tipo == TipoMovimento.DOACAO || co_tipo == TipoMovimento.REMANEJAMENTO)
                    {
                        if (co_tipo != TipoMovimento.REMANEJAMENTO)
                        {
                            DropDownList_Motivo.DataSource = iMotivo.BuscarPorTipoMovimento<MotivoMovimentoVacina>(co_tipo);
                            DropDownList_Motivo.DataBind();
                            this.InserirElementoDefault(this.DropDownList_Motivo);
                            this.Panel_MotivoMovimento.Visible = true;
                            this.CompareValidator_Motivo.Enabled = true;

                            DropDownList_EAS.DataSource = iEstabelecimento.ListarTodos<ViverMais.Model.EstabelecimentoSaude>("NomeFantasia", true);
                            DropDownList_EAS.DataBind();
                            this.InserirElementoDefault(this.DropDownList_EAS);
                            this.Panel_EstabelecimentoSaude.Visible = true;
                            this.CompareValidator_EstabelecimentoSaude.Enabled = true;
                        }
                        else
                        {
                            DropDownList_SalaDestino.DataSource = iVacina.ListarTodos<SalaVacina>("Nome", true).Where(p => p.Codigo != sala.Codigo).ToList();
                            DropDownList_SalaDestino.DataBind();
                            this.InserirElementoDefault(this.DropDownList_SalaDestino);
                            this.Panel_SalaVacinaDestino.Visible = true;
                            this.CompareValidator_SalaDestino.Enabled = true;
                        }

                        this.Panel_ResponsavelEnvio.Visible = true;
                        this.RequiredFieldValidator_ResponsavelEnvio.Enabled = true;

                        this.Panel_ResponsavelRecebimento.Visible = true;
                        this.RequiredFieldValidator_ResponsavelRecebimento.Enabled = true;

                        this.Panel_DataEnvio.Visible = true;
                        this.RequiredFieldValidator_DataEnvio.Enabled = true;
                        this.CompareValidator_DataEnvioCheck.Enabled = true;
                        this.CompareValidator_DataEnvioCheck2.Enabled = true;

                        this.Panel_DataRecebimento.Visible = true;
                        this.RequiredFieldValidator_DataRecebimento.Enabled = true;
                        this.CompareValidator_DataRecebimentoCheck.Enabled = true;
                        this.CompareValidator_DataRecebimentoCheck2.Enabled = true;

                        this.CompareValidator_CompararDataEnvioRecebimento.Enabled = true;
                    }
                    else if (co_tipo == TipoMovimento.ACERTO_BALANCO)
                    {
                        this.Panel_Responsavel.Visible = true;
                        this.RequiredFieldValidator_Responsavel.Enabled = true;
                    }
                }
                else
                {
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                    //return;
                }
            }

            Label_SalaVacina.Text = sala != null ? sala.Nome : string.Empty;
            Label_TipoMovimentacao.Text = tipomovimento != null ? tipomovimento.Nome : string.Empty;

            if (operacaomovimento != null)
            {
                Label_SituacaoMovimento.Text = operacaomovimento.Nome;
                Panel_SituacaoMovimento.Visible = true;
            }

            long co_movimento = -1;

            //Edição para alterar as quantidades de uma movimentação do tipo remanejamento que ainda esteja aberta
            if (Request["co_movimento"] != null && long.TryParse(Request["co_movimento"].ToString(), out co_movimento))
            {
                this.Lnk_Cancelar.PostBackUrl = "~/Vacina/FormPesquisarMovimentacao.aspx";
                IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
                MovimentoVacina movimento = iMovimento.BuscarPorCodigo<MovimentoVacina>(co_movimento);

                if (movimento.TipoMovimento.Codigo == TipoMovimentoVacina.REMANEJAMENTO &&
                    iMovimento.BuscarRemanejamentoPorMovimentacao<RemanejamentoVacina>(co_movimento).Status == RemanejamentoVacina.ABERTO)
                {
                    ViewState["co_movimento"] = co_movimento;
                    ViewState["movimentoimpressao"] = co_movimento;
                    imunos = iMovimento.BuscarItensMovimentacao<ItemMovimentoVacina>(co_movimento);
                    this.Panel_LotesPesquisados.Visible = false;
                    this.WUC_PesquisarLote.Visible = false;
                    this.DropDownList_SalaDestino.Visible = false;
                    this.TextBox_ResponsavelEnvio.Visible = false;
                    this.TextBox_DataEnvio.Visible = false;
                    this.TextBox_ResponsavelRecebimento.Visible = false;
                    this.TextBox_DataRecebimento.Visible = false;
                    this.TextBox_Observacao.ReadOnly = true;
                    this.Lnk_Salvar.Visible = false;
                    this.LinkButton_ImprimirMovimento.Visible = true;

                    this.LabelSalaDestino.Text = movimento.SalaDestino.Nome;
                    this.LabelSalaDestino.Visible = true;
                    this.LabelResponsavelEnvio.Text = movimento.ResponsavelEnvio;
                    this.LabelResponsavelEnvio.Visible = true;
                    this.LabelResponsavelRecebimento.Text = movimento.ResponsavelRecebimento;
                    this.LabelResponsavelRecebimento.Visible = true;
                    this.LabelDataEnvio.Text = movimento.DataEnvio.Value.ToString("dd/MM/yyyy");
                    this.LabelDataEnvio.Visible = true;
                    this.LabelDataRecebimento.Text = movimento.DataRecebimento.Value.ToString("dd/MM/yyyy");
                    this.LabelDataRecebimento.Visible = true;
                    this.TextBox_Observacao.Text = movimento.Observacao;
                }
            }

            this.CarregaImunos(imunos);

            //if (co_movimento == -1) //Nova movimentação
            //    this.OnClick_ListarTodos(new object(), new EventArgs());
        }

        protected void OnRowDataBound_Imunos(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int columns = this.GridView_Imunos.Columns.Count;

                if (ViewState["co_movimento"] != null)
                {
                    this.GridView_Imunos.Columns[columns - 2].Visible = false;
                    this.GridView_Imunos.Columns[columns - 3].Visible = false;
                    
                    IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
                    //long co_movimento = long.Parse(ViewState["co_movimento"].ToString());
                    long co_lote = long.Parse(this.GridView_Imunos.DataKeys[e.Row.RowIndex]["CodigoLote"].ToString());
                    IList<ItemMovimentoVacina> imunos = (IList<ItemMovimentoVacina>)Session["imunos"];
                    ItemMovimentoVacina itemmovimento = imunos.Where(p=>p.Lote.Codigo == co_lote).First();
                    //ItemMovimentoVacina itemmovimento = iMovimento.BuscarItemMovimento<ItemMovimentoVacina>(co_movimento, co_lote);
                    //ItemRemanejamentoVacina itemremanejamento = iMovimento.BuscarItemRemanejamentoPorMovimentoLote<ItemRemanejamentoVacina>(co_movimento, co_lote);
                    LinkButton lbselect = (LinkButton)e.Row.Controls[8].Controls[0];

                    if (!itemmovimento.Editar)
                    //if (itemremanejamento.RecebimentoConfirmado || iMovimento.BuscarHistoricoAlteracaoItemMovimento<HistoricoItemMovimentoVacina>(itemmovimento.Codigo).Count()
                        //== HistoricoItemMovimentoVacina.LIMITE_ALTERACAO)
                    {
                        lbselect.Enabled = false;
                        lbselect.Text = "<img src=\"img/editar-not.jpg\" alt=\"Alteração indisponível.\" border=\"0\" />";
                    }
                }else
                    this.GridView_Imunos.Columns[columns - 1].Visible = false;
            }
        }

        protected void OnSelectedIndexChanging_Imunos(object sender, GridViewSelectEventArgs e)
        {
            this.Panel_AlterarQuantidadeItemMovimento.Visible = true;
            IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
            long co_lote = long.Parse(this.GridView_Imunos.DataKeys[e.NewSelectedIndex]["CodigoLote"].ToString());
            this.GridView_Imunos.SelectedRowStyle.BackColor = Color.LightGray;
            long co_movimento = long.Parse(ViewState["co_movimento"].ToString());
            ItemMovimentoVacina itemmovimento = iMovimento.BuscarItemMovimento<ItemMovimentoVacina>(co_movimento, co_lote);
            int numeroalteracoes = iMovimento.BuscarHistoricoAlteracaoItemMovimento<HistoricoItemMovimentoVacina>(itemmovimento.Codigo).Count();

            this.LabelQtdAlteracoes.Text = (numeroalteracoes + 1).ToString() + " de " + HistoricoItemMovimentoVacina.LIMITE_ALTERACAO.ToString();
            this.LabelImunoAlteracao.Text = itemmovimento.NomeVacina;
            this.LabelFabricanteAlteracao.Text = itemmovimento.NomeFabricante;
            this.LabelAplicacaoAlteracao.Text = itemmovimento.AplicacaoVacina.ToString();
            this.LabelLoteAlteracao.Text = itemmovimento.Identificacao;
            this.LabelValidadeAlteracao.Text = itemmovimento.DataValidade.ToString("dd/MM/yyyy");

            this.TextBox_Quantidade.Text = itemmovimento.Quantidade.ToString();
            this.TextBox_Motivo.Text = "";
            Session["itemMovimentoAlterar"] = itemmovimento;
        }

        protected void OnClick_AlterarQuantidadeItem(object sender, EventArgs e)
        {
            IEstoqueVacina iEstoque = Factory.GetInstance<IEstoqueVacina>();
            IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
            ItemMovimentoVacina itemmovimento = (ItemMovimentoVacina)Session["itemMovimentoAlterar"];
            ItemRemanejamentoVacina itemremanejamento = iMovimento.BuscarItemRemanejamentoPorMovimentoLote<ItemRemanejamentoVacina>(itemmovimento.Movimento.Codigo, itemmovimento.Lote.Codigo);
            int quantidadeestoque = iEstoque.QuantidadeDisponivelEstoque(itemmovimento.Lote.Codigo, itemmovimento.Movimento.Sala.Codigo);
            int quantidadealteracao = int.Parse(this.TextBox_Quantidade.Text);
            int diferencaalteracao = quantidadealteracao - itemmovimento.Quantidade;

            if (quantidadeestoque < diferencaalteracao)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, não é possível alterar a quantidade do imunobiológico nesta movimentação, pois a mesma é insuficiente no estoque. Quantidade solicitada para completar a alteração: " + diferencaalteracao + " e quantidade disponível: " + quantidadeestoque + ".');", true);
                return;
            }

            itemmovimento.Editar = iMovimento.AlterarQuantidadeItemMovimento<ItemMovimentoVacina, ItemRemanejamentoVacina, Usuario>(ref itemmovimento, itemremanejamento, quantidadealteracao, this.TextBox_Motivo.Text, (Usuario)Session["Usuario"]);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Quantidade alterada com sucesso.');", true);

            IList<ItemMovimentoVacina> imunos = (IList<ItemMovimentoVacina>)Session["imunos"];
            imunos[ItemMovimentoVacina.RetornaIndex(imunos, itemmovimento.Lote.Codigo)] = itemmovimento;
            Session["imunos"] = imunos;
            this.OnClick_CancelarAlteracaoItem(sender, e);
        }

        protected void OnClick_CancelarAlteracaoItem(object sender, EventArgs e)
        {
            this.Panel_AlterarQuantidadeItemMovimento.Visible = false;
            this.GridView_Imunos.SelectedIndex = -1;
            this.CarregaImunos((IList<ItemMovimentoVacina>)Session["imunos"]);
        }

        private void InserirElementoDefault(DropDownList dropdown)
        {
            dropdown.Items.Insert(0, new ListItem("Selecione...", "-1"));
        }

        private bool VerificarPermissaoExecutarMovimentacao(Usuario usuario, int co_tipomovimento)
        {
            ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();

            if (co_tipomovimento == TipoMovimentoVacina.PERDA && iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DOACAO", Modulo.VACINA))
                return true;
            else if (co_tipomovimento == TipoMovimentoVacina.DEVOLUCAO && iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DEVOLUCAO", Modulo.VACINA))
                return true;
            else if (co_tipomovimento == TipoMovimentoVacina.ACERTO_BALANCO && iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_ACERTO_BALANCO", Modulo.VACINA))
                return true;
            else if (co_tipomovimento == TipoMovimentoVacina.DOACAO && iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_DOACAO", Modulo.VACINA))
                return true;
            else if (co_tipomovimento == TipoMovimentoVacina.EMPRESTIMO && iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_EMPRESTIMO", Modulo.VACINA))
                return true;
            else if (co_tipomovimento == TipoMovimentoVacina.REMANEJAMENTO && iSeguranca.VerificarPermissao(usuario.Codigo, "REALIZAR_MOVIMENTACAO_REMANEJAMENTO", Modulo.VACINA))
                return true;

            return false;
        }

        protected void OnPageIndexChanging_Imunos(object sender, GridViewPageEventArgs e)
        {
            this.CarregaImunos((IList<ItemMovimentoVacina>)Session["imunos"]);
            GridView_Imunos.PageIndex = e.NewPageIndex;
            GridView_Imunos.DataBind();
        }

        protected void OnPageIndexChanging_Lotes(object sender, GridViewPageEventArgs e)
        {
            this.CarregaLotes((IList<LoteVacina>)Session["lotes"]);
            GridView_LotesPesquisados.PageIndex = e.NewPageIndex;
            GridView_LotesPesquisados.DataBind();
        }

        protected void OnRowCancelingEdit_Imunos(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_Imunos.EditIndex = -1;
            this.CarregaImunos((IList<ItemMovimentoVacina>)Session["imunos"]);
        }

        protected void OnRowEditing_Imunos(object sender, GridViewEditEventArgs e)
        {
            GridView_Imunos.EditIndex = e.NewEditIndex;
            this.CarregaImunos((IList<ItemMovimentoVacina>)Session["imunos"]);
        }

        protected void OnRowDeleting_Imunos(object sender, GridViewDeleteEventArgs e)
        {
            IList<ItemMovimentoVacina> imunos = (IList<ItemMovimentoVacina>)Session["imunos"];
            long co_lote = long.Parse(GridView_Imunos.DataKeys[e.RowIndex]["CodigoLote"].ToString());

            int remover = ItemMovimentoVacina.RetornaIndex(imunos, co_lote);
            imunos.RemoveAt(remover);
            this.CarregaImunos(imunos);
        }

        protected void OnRowUpdating_Imunos(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                IEstoqueVacina iEstoque = Factory.GetInstance<IEstoqueVacina>();
                int co_situacao = int.Parse(ViewState["co_situacao"].ToString());
                int co_sala = int.Parse(ViewState["co_sala"].ToString());
                long co_lote = long.Parse(GridView_Imunos.DataKeys[e.RowIndex]["CodigoLote"].ToString());
                int quantidadesolicitada = int.Parse(((TextBox)GridView_Imunos.Rows[e.RowIndex].FindControl("TextBox_Quantidade")).Text);
                IList<ItemMovimentoVacina> imunos = (IList<ItemMovimentoVacina>)Session["imunos"];

                if (co_situacao == OperacaoMovimentoVacina.SAIDA)
                {
                    int quantidadeestoque = iEstoque.QuantidadeDisponivelEstoque(co_lote, co_sala);

                    if (quantidadeestoque < quantidadesolicitada)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, não é possível incluir este lote nos itens da movimentação, pois a quantidade é insuficiente no estoque. Quantidade solicitada: " + quantidadesolicitada + " e quantidade disponível: " + quantidadeestoque + ".');", true);
                        return;
                    }
                }

                int atualizar = ItemMovimentoVacina.RetornaIndex(imunos, co_lote);

                ItemMovimentoVacina item = imunos[atualizar];
                item.Quantidade = quantidadesolicitada;
                imunos[atualizar] = item;

                GridView_Imunos.EditIndex = -1;
                this.CarregaImunos(imunos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void OnRowCancelingEdit_Lotes(object sender, GridViewCancelEditEventArgs e)
        {
            GridView_LotesPesquisados.EditIndex = -1;
            this.CarregaLotes((IList<LoteVacina>)Session["lotes"]);
        }

        protected void OnRowEditing_Lotes(object sender, GridViewEditEventArgs e)
        {
            GridView_LotesPesquisados.EditIndex = e.NewEditIndex;
            this.CarregaLotes((IList<LoteVacina>)Session["lotes"]);
        }

        protected void OnRowDataBound_Lotes(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (int.Parse(ViewState["co_situacao"].ToString()) == OperacaoMovimentoVacina.SAIDA)
                {
                    Label labelestoque = (Label)e.Row.FindControl("Label_QuantidadeEstoque");
                    long co_lote = long.Parse(this.GridView_LotesPesquisados.DataKeys[e.Row.RowIndex]["Codigo"].ToString());
                    int co_sala = int.Parse(ViewState["co_sala"].ToString());
                    IEstoqueVacina iEstoque = Factory.GetInstance<IEstoqueVacina>();
                    labelestoque.Text = iEstoque.QuantidadeDisponivelEstoque(co_lote, co_sala).ToString();
                }
                else
                    this.GridView_LotesPesquisados.Columns[5].Visible = false;
            }
        }

        protected void OnRowUpdating_Lotes(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                IEstoqueVacina iEstoque = Factory.GetInstance<IEstoqueVacina>();
                int co_situacao = int.Parse(ViewState["co_situacao"].ToString());
                int co_sala = int.Parse(ViewState["co_sala"].ToString());
                long co_lote = long.Parse(GridView_LotesPesquisados.DataKeys[e.RowIndex]["Codigo"].ToString());
                int quantidadesolicitada = int.Parse(((TextBox)GridView_LotesPesquisados.Rows[e.RowIndex].FindControl("TextBox_Quantidade")).Text);
                IList<ItemMovimentoVacina> imunos = (IList<ItemMovimentoVacina>)Session["imunos"];

                if (co_situacao == OperacaoMovimentoVacina.SAIDA)
                {
                    int quantidadeestoque = iEstoque.QuantidadeDisponivelEstoque(co_lote, co_sala);

                    if (quantidadeestoque < quantidadesolicitada)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, não é possível incluir este lote nos itens da movimentação, pois a quantidade é insuficiente no estoque. Quantidade solicitada: " + quantidadesolicitada + " e quantidade disponível: " + quantidadeestoque + ".');", true);
                        return;
                    }
                }

                ItemMovimentoVacina item = new ItemMovimentoVacina();
                item.Quantidade = quantidadesolicitada;
                item.Lote = iEstoque.BuscarPorCodigo<LoteVacina>(co_lote);
                imunos.Add(item);

                GridView_LotesPesquisados.EditIndex = -1;
                this.CarregaLotes((IList<LoteVacina>)Session["lotes"]);
                this.CarregaImunos(imunos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            try
            {
                IList<ItemMovimentoVacina> itens = (IList<ItemMovimentoVacina>)Session["imunos"];

                if (itens.Count() > 0)
                {
                    IVacinaServiceFacade iVacina = Factory.GetInstance<IVacinaServiceFacade>();
                    int co_tipo = int.Parse(ViewState["co_tipo"].ToString());
                    int co_situacao = int.Parse(ViewState["co_situacao"].ToString());
                    int co_sala = int.Parse(ViewState["co_sala"].ToString());
                    Usuario usuario = (Usuario)Session["Usuario"];

                    MovimentoVacina movimento = new MovimentoVacina();
                    movimento.Data = DateTime.Now;
                    movimento.Observacao = TextBox_Observacao.Text;
                    movimento.Sala = iVacina.BuscarPorCodigo<SalaVacina>(co_sala);
                    movimento.Operacao = iVacina.BuscarPorCodigo<OperacaoMovimentoVacina>(co_situacao);
                    movimento.TipoMovimento = iVacina.BuscarPorCodigo<TipoMovimentoVacina>(co_tipo);

                    if (this.Panel_SalaVacinaDestino.Visible == true)
                        movimento.SalaDestino = iVacina.BuscarPorCodigo<SalaVacina>(int.Parse(DropDownList_SalaDestino.SelectedValue));

                    if (this.Panel_MotivoMovimento.Visible == true)
                        movimento.Motivo = iVacina.BuscarPorCodigo<MotivoMovimentoVacina>(int.Parse(DropDownList_Motivo.SelectedValue));

                    if (this.Panel_EstabelecimentoSaude.Visible == true)
                        movimento.EstabelecimentoSaude = iVacina.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(DropDownList_EAS.SelectedValue);

                    if (this.Panel_Responsavel.Visible == true)
                        movimento.Responsavel = TextBox_Responsavel.Text.ToUpper();

                    if (this.Panel_ResponsavelEnvio.Visible == true)
                        movimento.ResponsavelEnvio = TextBox_ResponsavelEnvio.Text.ToUpper();
                    if (this.Panel_DataEnvio.Visible == true)
                        movimento.DataEnvio = DateTime.Parse(TextBox_DataEnvio.Text);

                    if (this.Panel_ResponsavelRecebimento.Visible == true)
                        movimento.ResponsavelRecebimento = TextBox_ResponsavelRecebimento.Text.ToUpper();
                    if (this.Panel_DataRecebimento.Visible == true)
                        movimento.DataRecebimento = DateTime.Parse(TextBox_DataRecebimento.Text);

                    IEstoqueVacina iEstoque = Factory.GetInstance<IEstoqueVacina>();
                    iEstoque.SalvarMovimentacao<MovimentoVacina, ItemMovimentoVacina>(movimento, itens, usuario.Codigo);
                    ViewState["movimentoimpressao"] = movimento.Codigo;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Movimento salvo com sucesso! Número de cadastro: " + movimento.Numero + ".');", true);

                    Lnk_Salvar.Visible = false;
                    LinkButton_ImprimirMovimento.Visible = true;
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, por favor inclua pelo menos um item para esta movimentação.');", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void OnClick_ImprimirMovimento(object sender, EventArgs e)
        {
            ReportDocument doc = new ReportDocument();
            doc = Factory.GetInstance<IRelatorioVacina>().ObterRelatorioMovimento(long.Parse(ViewState["movimentoimpressao"].ToString()));

            //Hashtable hash = Factory.GetInstance<IRelatorioVacina>().ObterRelatorioMovimento(long.Parse(ViewState["movimentoimpressao"].ToString()));

            //IMPRESSÃO DA MOVIMENTAÇÃO
            
            //doc.Load(Server.MapPath("RelatoriosCrystal/RelMovimento.rpt"));
            //doc.Database.Tables["corpo"].SetDataSource((DataTable)hash["corpo"]);
            //doc.Database.Tables["cabecalho"].SetDataSource((DataTable)hash["cabecalho"]);
            //doc.Subreports["RelHistoricoItemMovimentacao.rpt"].SetDataSource((DataTable)hash["historicoitens"]);

            Session["documentoImpressaoVacina"] = doc;
            Response.Redirect("FormMostrarRelatorioCrystalImpressao.aspx?nome_arquivo=movimento.pdf");

            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "imprimir", "window.open('FormMostrarRelatorioCrystalImpressao.aspx');", true);
            //IMPRESSÃO DA MOVIMENTAÇÃO
        }
    }
}
