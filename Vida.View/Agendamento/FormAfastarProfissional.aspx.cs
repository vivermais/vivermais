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
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using System.Drawing;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Agendamento
{
    public partial class FormAfastarProfissional : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_AFASTAR_PROFISSIONAL", Modulo.AGENDAMENTO))
                {
                    PanelGridviewListaProfissionais.Visible = false;
                    //PanelDadosAfastamento.Visible = false;
                    PanelVerificaExistenciaSolicitacao.Visible = false;
                    PanelExibeDadosProfissional.Visible = false;
                    lblSemRegistro.Visible = false;
                    // Lista todas as Categorias
                    IList<ViverMais.Model.CategoriaOcupacao> categorias = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<ViverMais.Model.CategoriaOcupacao>();
                    ddlCategoria.DataSource = categorias;
                    ddlCategoria.DataTextField = "Nome";
                    ddlCategoria.DataValueField = "Codigo";
                    ddlCategoria.DataBind();
                    ddlCategoria.SelectedValue = "1";
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void MarcarAgendaBloqueada()
        {
            //DataTable tableAgendas = (DataTable)(Session["Agendas"]);
            //int index = 0;

            //Codigo utilizado para resolver o problema das agendas bloqueadas na paginação. 
            //Quando o peão selecionar uma agenda, ele não selecioanr também nas outras páginas que estão com o mesmo índice
            for (int i = 0; i < GridviewAgendas.Rows.Count; i++)
            {
                //if (index == 10)
                //    index = 0;

                //if (i < GridviewAgendas.Rows.Count)
                //{ 
                int codigoAgenda = int.Parse(GridviewAgendas.Rows[i].Cells[0].Text);
                Agenda agenda = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Agenda>(codigoAgenda);

                //Agenda agenda = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Agenda>(id_agenda);
                if (agenda != null)
                {
                    if (agenda.Bloqueada)
                    {
                        GridviewAgendas.Rows[i].BackColor = Color.FromArgb(175, 82, 82);
                        GridviewAgendas.Rows[i].Cells[1].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        GridviewAgendas.Rows[i].Cells[2].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        GridviewAgendas.Rows[i].Cells[3].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        GridviewAgendas.Rows[i].Cells[4].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        GridviewAgendas.Rows[i].Cells[5].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        GridviewAgendas.Rows[i].Cells[6].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        GridviewAgendas.Rows[i].Cells[10].FindControl("btnBloqueioAgenda").Visible = false;
                    }
                }
                //else
                //    break;

                //index++;
            }
        }

        protected void btnAlteraProfissional_Click(object sender, EventArgs e)
        {
            PanelExibeDadosProfissional.Visible = false;
            PanelBuscaProfissional.Visible = true;
        }

        protected void btnPesquisarAgendas_Click(object sender, EventArgs e)
        {
            // Verifica se existem Consultas Agendadas para este profissional
            ViverMais.Model.EstabelecimentoSaude estabelecimento = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(lblCnes.Text.Trim());

            if (estabelecimento != null)
            {
                IList<ViverMais.Model.Agenda> agendas = Factory.GetInstance<IAmbulatorial>().VerificarAgendas<ViverMais.Model.Agenda>(estabelecimento.CNES, ddlProfissional.SelectedValue, tbxData_Inicial.Text, tbxData_Final.Text, ddlTurno.SelectedValue);
                GridviewAgendas.DataSource = agendas;
                GridviewAgendas.DataBind();
                //Verifica se existe Agenda para o Período informado
                if (agendas.Count != 0)
                {
                    Session["Agendas"] = agendas;
                    MarcarAgendaBloqueada();
                }
                PanelExibeAgenda.Visible = true;
            }
            else
            {
                lblNomeEstabelecimento.Text = "CNES INVÁLIDO!";
                tbxCNES.Text = "";

            }
        }

        protected void imgPesquisar_Click1(object sender, EventArgs e)
        {
            string cnes = tbxCNES.Text.Trim();
            IVinculo ivinculo = Factory.GetInstance<IVinculo>();

            // Monta lista de Profissionais ligados ao Vinculo do CNES
            ddlProfissional.Items.Clear();
            VinculoProfissional vinculoProfissional = ivinculo.BuscarVinculoProfissionalPorCnes<ViverMais.Model.VinculoProfissional>(tbxCNES.Text.Trim(), tbxConselho.Text, ddlCategoria.SelectedValue).FirstOrDefault();
            if (vinculoProfissional != null)
            {
                lblSemRegistro.Visible = false;
                DataTable table = new DataTable();
                table.Columns.Add("Codigo");
                table.Columns.Add("Nome");
                table.Columns.Add("Categoria");
                table.Columns.Add("RegistroConselho");
                DataRow row = table.NewRow();
                row[0] = vinculoProfissional.Profissional.CPF;
                row[1] = vinculoProfissional.Profissional.Nome;
                row[2] = vinculoProfissional.OrgaoEmissorRegistroConselho.CategoriaOcupacao.Nome;
                row[3] = vinculoProfissional.RegistroConselho;
                table.Rows.Add(row);
                GridViewListaProfissionais.DataSource = table;
                GridViewListaProfissionais.DataBind();
                PanelGridviewListaProfissionais.Visible = true;
            }
            else
            {
                PanelGridviewListaProfissionais.Visible = false;
                lblSemRegistro.Visible = true;
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Nenhum Vínculo Localizado para o Profissional Selecionado!');</script>");
                return;
            }

        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            PanelDadosDaAgenda.Visible = false;
        }

        protected void btnImprimeSolicitacoes_Click(object sender, EventArgs e)
        {
            string id_agenda = tbxCodigoAgenda.Text;
            Session["ImprimiuListaSolicitacoes"] = "0";
            Redirector.Redirect("RelatorioListaSolicitacoesAgenda.aspx?codigo=" + id_agenda, "_blank", "");

        }

        protected void GridViewListaProfissionais_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id_profissional = Convert.ToString(e.CommandArgument);
            ViverMais.Model.Profissional profissional = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Profissional>(id_profissional);

            ddlProfissional.Items.Add(new ListItem(profissional.Nome, profissional.CPF));
            PanelGridviewListaProfissionais.Visible = false;
            PanelVerificaExistenciaSolicitacao.Visible = true;
            lblCnes.Text = tbxCNES.Text.Trim();
            lblConselho.Text = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<CategoriaOcupacao>(ddlCategoria.SelectedValue).Nome;
            lblNomeProfissional.Text = profissional.Nome;
            lblNumeroConselho.Text = tbxConselho.Text.Trim();
            PanelBuscaProfissional.Visible = false;
            PanelExibeDadosProfissional.Visible = true;
        }

        protected void GridViewAgendas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int id_agenda = Convert.ToInt32(e.CommandArgument);
                Agenda agenda = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Agenda>(id_agenda);
                if (agenda != null)
                {
                    tbxCodigoAgenda.Text = agenda.Codigo.ToString();
                    lblNomeEstabelecimentoAgenda.Text = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(agenda.Estabelecimento.CNES).NomeFantasia;
                    lblProcedimentoAgenda.Text = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<Procedimento>(agenda.Procedimento.Codigo).Nome;

                    //Verifica pelo CBO, quais os profissionais da Unidade podem realizar o procedimento
                    IList<ViverMais.Model.VinculoProfissional> vinculo = Factory.GetInstance<IVinculo>().BuscarPorCNESCBO<ViverMais.Model.VinculoProfissional>(agenda.Estabelecimento.CNES, agenda.Cbo.Codigo);
                    ddlProfissionalAgenda.Items.Add(new ListItem("Selecione...", "-1"));
                    foreach (ViverMais.Model.VinculoProfissional f in vinculo)
                    {
                        ViverMais.Model.Profissional profissional = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Profissional>(f.Profissional.CPF);
                        if (profissional != null && (ddlProfissionalAgenda.Items.FindByValue(profissional.CPF) == null))
                        {
                            ddlProfissionalAgenda.Items.Add(new ListItem(profissional.Nome, profissional.CPF));
                        }
                    }

                    lblQtdAgenda.Text = agenda.Quantidade.ToString();
                    lblQtdAgendadaAgenda.Text = agenda.QuantidadeAgendada.ToString();
                    tbxDataAgenda.Text = agenda.Data.ToString("dd/MM/yyyy");
                    ddlTurnoAgenda.SelectedValue = agenda.Turno.ToUpper();
                    CarregaSolicitacoesAgenda(agenda);

                    PanelDadosDaAgenda.Visible = true;
                }
            }
            //else if (e.CommandName == "Bloqueio")
            //{
            //    int index = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow row = GridviewAgendas.Rows[index];

            //    int codigoAgenda = int.Parse(GridviewAgendas.Rows[index].Cells[0].Text);
            //    Agenda agenda = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Agenda>(codigoAgenda);
            //    agenda.Bloqueada = true;
            //    Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(agenda);
            //    row.BackColor = Color.FromArgb(175, 82, 82);
            //    row.Cells[1].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
            //    row.Cells[2].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
            //    row.Cells[3].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
            //    row.Cells[4].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
            //    row.Cells[5].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
            //    row.Cells[6].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
            //    row.Cells[9].FindControl("btnBloqueioAgenda").Visible = false;
            //    btnPesquisarAgendas_Click(new object(), new EventArgs());
            //}
        }

        void CarregaSolicitacoesAgenda(Agenda agenda)
        {
            IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().BuscaSolicitacoesNaoConfirmadasNaoIndeferidasPorAgenda<Solicitacao>(agenda.Codigo);
            if (solicitacoes.Count != 0)
            {
                DataTable table = new DataTable();
                table.Columns.Add("Codigo");
                table.PrimaryKey = new DataColumn[] { table.Columns["Codigo"] };//Define o código da Solicitação como chave primária, para facilitar a busca
                table.Columns.Add("Paciente");
                table.Columns.Add("CartaoSUS");
                table.Columns.Add("DataNascimento");
                table.Columns.Add("DataSolicitacao");

                foreach (Solicitacao solicitacao in solicitacoes)
                {
                    DataRow row = table.NewRow();
                    ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(solicitacao.ID_Paciente);
                    row[0] = solicitacao.Codigo.ToString();
                    IList<CartaoSUS> cartoes = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(solicitacao.ID_Paciente);
                    row[1] = paciente.Nome;
                    row[2] = cartoes[cartoes.Count - 1].Numero;

                    row[3] = paciente.DataNascimento.ToString("dd/MM/yyyy");
                    row[4] = solicitacao.Data_Solicitacao.ToString("dd/MM/yyyy");
                    table.Rows.Add(row);
                }
                lblSemSolicitacoes.Visible = false;
                //btnDesmarcaTodasSolicitacoes.Visible = true;
                btnImprimeSolicitacoes.Visible = true;
                GridviewSolicitacoesAgenda.DataSource = table;
                GridviewSolicitacoesAgenda.DataBind();
                GridviewSolicitacoesAgenda.Visible = true;
            }
            else
            {
                lblSemSolicitacoes.Visible = true;
                GridviewSolicitacoesAgenda.Visible = false;
                //btnDesmarcaTodasSolicitacoes.Visible = false;
                btnImprimeSolicitacoes.Visible = false;
            }
        }


        protected void GridViewSolicitacoesAgenda_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_DESMARCAR_AGENDAMENTO", Modulo.AGENDAMENTO))
            {
                IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                int id_solicitacao = Convert.ToInt32(e.CommandArgument.ToString());
                Solicitacao solicitacao = iAgendamento.BuscarPorCodigo<Solicitacao>(id_solicitacao);
                solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString();
                iAgendamento.Salvar(solicitacao);
                iAgendamento.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 13, solicitacao.Codigo.ToString()));

                Agenda agenda = solicitacao.Agenda;

                agenda.QuantidadeAgendada--;
                iAgendamento.Salvar(agenda);
                lblQtdAgendadaAgenda.Text = agenda.QuantidadeAgendada.ToString();
                int co_subgrupo = solicitacao.Agenda.SubGrupo != null ? solicitacao.Agenda.SubGrupo.Codigo : 0;
                QuantidadeSolicitacaoRede quantidadeSolicitacoes = Factory.GetInstance<IQuantidadeSolicitacaoRede>().BuscaQuantidade<QuantidadeSolicitacaoRede>((solicitacao.Data_Solicitacao.Year.ToString("0000") + solicitacao.Data_Solicitacao.Month.ToString("00")), solicitacao.Agenda.Procedimento.Codigo, solicitacao.Agenda.Cbo.Codigo, solicitacao.Agenda.Estabelecimento.CNES, co_subgrupo);
                if (quantidadeSolicitacoes != null)
                {
                    quantidadeSolicitacoes.QtdSolicitacoes--;
                    iAgendamento.Salvar(quantidadeSolicitacoes);
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('DADOS SALVOS COM SUCESSO!');", true);
                IList<ViverMais.Model.Agenda> agendas = Factory.GetInstance<IAmbulatorial>().VerificarAgendas<ViverMais.Model.Agenda>(solicitacao.Agenda.Estabelecimento.CNES, ddlProfissional.SelectedValue, tbxData_Inicial.Text, tbxData_Final.Text, ddlTurno.SelectedValue);
                GridviewAgendas.DataSource = agendas;
                GridviewAgendas.DataBind();
                Session["Agendas"] = agendas;
                CarregaSolicitacoesAgenda(solicitacao.Agenda);
                //string qtdAgendada = GridviewAgendas.Rows[GridviewAgendas.SelectedIndex].Cells[6].Text;
                //GridviewAgendas.Rows[GridviewAgendas.SelectedIndex].Cells[6].Text = (int.Parse(qtdAgendada) - 1).ToString();
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para desmarcar solicitações! Por favor, entre em contato com a administração.');", true);
            return;
        }

        protected void btnSalvar_Click1(object sender, EventArgs e)
        {
            //DataTable agendas = (DataTable)Session["Agendas"];
            //IList<ViverMais.Model.Agenda> agendas = (IList<ViverMais.Model.Agenda>)Session["Agendas"];
            ViverMais.Model.EstabelecimentoSaude estabelecimento = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(lblCnes.Text.Trim());
            IAmbulatorial iAmbulatorial = Factory.GetInstance<IAmbulatorial>();
            IList<ViverMais.Model.Agenda> agendas = iAmbulatorial.VerificarAgendas<ViverMais.Model.Agenda>(estabelecimento.CNES, ddlProfissional.SelectedValue, tbxData_Inicial.Text, tbxData_Final.Text, ddlTurno.SelectedValue);

            if (agendas.Count != 0)
            {
                for (int i = 0; i < agendas.Count; i++)
                {
                    //Verifica se a Agenda ainda possui Solicitações ou se está bloqueada
                    if (agendas[i].QuantidadeAgendada != 0)
                    //if ((int.Parse(GridviewAgendas.Rows[i].Cells[6].ToString()) != 0) || (int.Parse(GridviewAgendas.Rows[i].Cells[7].ToString()) != 1))
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi possível SALVAR. Ainda Existem solicitações autorizadas!');", true);
                        return;
                    }
                }
            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Antes de Salvar, Verifique se Existe Agenda para o Profissional!');", true);
            //    return;
            //}

            string id_unidade = lblCnes.Text.Trim();

            string id_profissional = ddlProfissional.SelectedValue;

            DateTime data_inicial = DateTime.Parse(tbxData_Inicial.Text);

            // Verifica se a data é anterior a data do dia
            DateTime hoje = DateTime.Today;
            if (data_inicial < hoje)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Data Inicial anterior à data atual!');</script>");
                return;
            }
            if (tbxData_Final.Text != "")
            {
                DateTime data_final = DateTime.Parse(tbxData_Final.Text);
                if (data_final < hoje)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Data Final anterior à data atual!');</script>");
                    return;
                }

                // Verifica se data final é anterior à data inicial
                if (data_final < data_inicial)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Datas imcompatíveis !');</script>");
                    return;
                }
            }

            // Criticar se já existe este afastamento
            AfastamentoProfissional afastamentoprofissional = Factory.GetInstance<IAfastamentoProfissional>().VerificaExistenciaAfastamentoPeriodo<AfastamentoProfissional>(ddlProfissional.SelectedValue, DateTime.Parse(tbxData_Inicial.Text), tbxData_Final.Text.Trim(), id_unidade);
            if (afastamentoprofissional != null)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Já existe Afastamento para o Período Informado!');</script>");
                return;
            }
            afastamentoprofissional = new AfastamentoProfissional();
            afastamentoprofissional.Data_Inicial = DateTime.Parse(tbxData_Inicial.Text);
            afastamentoprofissional.Motivo = tbxMotivo.Text;
            afastamentoprofissional.Profissional = iAmbulatorial.BuscarPorCodigo<ViverMais.Model.Profissional>(ddlProfissional.SelectedValue);
            afastamentoprofissional.Unidade = iAmbulatorial.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(lblCnes.Text.Trim());
            afastamentoprofissional.Obs = tbxObs.Text;
            if (tbxData_Final.Text != "")
            {
                afastamentoprofissional.Data_Final = DateTime.Parse(tbxData_Final.Text);
            }

            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            iAgendamento.Salvar(afastamentoprofissional);
            iAgendamento.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 8, afastamentoprofissional.Codigo.ToString()));

            //Bloqueia as Agendas
            for (int i = 0; i < agendas.Count; i++)
            {
                Agenda agd = agendas[i];
                agd.Bloqueada = true;
                iAgendamento.Salvar(agd);
                iAgendamento.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 52, agd.Codigo.ToString()));

            }
            Session["Agendas"] = null;
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados salvos com sucesso!');window.location='FormAfastarProfissional.aspx?id_fastamentoprofissional=" + afastamentoprofissional.Codigo + "'</script>");
        }

        protected void rblTipoAfastamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblTipoAfastamento.SelectedValue == "I")//Inderterminado
            {
                tbxData_Final.Enabled = false;
                RequiredFieldValidator5.Enabled = false;
            }
            else
            {
                tbxData_Final.Enabled = true;
                RequiredFieldValidator5.Enabled = true;
            }
        }

        protected void btnSalvaAgenda_Click(object sender, EventArgs e)
        {
            Agenda agenda = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Agenda>(int.Parse(tbxCNES.Text));
            if (DateTime.Parse(tbxDataAgenda.Text) < DateTime.Now)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A Data da Agenda informada não pode ser menor do que a Data Atual!');", true);
                return;
            }
            agenda.Data = DateTime.Parse(tbxDataAgenda.Text);
            agenda.ID_Profissional = Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(ddlProfissionalAgenda.SelectedValue);
            agenda.Turno = ddlTurnoAgenda.SelectedValue.ToUpper();
            Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(agenda);
        }

        protected void GridViewAgendas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridviewAgendas.PageIndex = e.NewPageIndex;
            if (Session["Agendas"] != null)
            {
                //IList<Agenda> agendas = (IList<Agenda>)Session["Agendas"];
                //DataTable table = (DataTable)(Session["Agendas"]);

                GridviewAgendas.DataSource = (IList<Agenda>)Session["Agendas"]; ;
                GridviewAgendas.DataBind();
                MarcarAgendaBloqueada();
            }
        }

        /// <summary>
        /// Desmarca Todas as Solicitações referentes a Agenda Selecionada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDesmarcaTodasSolicitacoes_Click(object sender, EventArgs e)
        {
            if (Session["ImprimiuListaSolicitacoes"] != null)
            {
                string id_agenda = tbxCodigoAgenda.Text;
                IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().BuscaSolicitacoesNaoConfirmadasNaoIndeferidasPorAgenda<Solicitacao>(int.Parse(id_agenda));
                if (solicitacoes.Count != 0)
                {
                    for (int i = 0; i < solicitacoes.Count; i++)
                    {
                        solicitacoes[i].Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString();
                        Factory.GetInstance<ISolicitacao>().Salvar(solicitacoes[i]);
                        int co_subgrupo = solicitacoes[i].Agenda.SubGrupo != null ? solicitacoes[i].Agenda.SubGrupo.Codigo : 0;
                        QuantidadeSolicitacaoRede quantidadeSolicitacoes = Factory.GetInstance<IQuantidadeSolicitacaoRede>().BuscaQuantidade<QuantidadeSolicitacaoRede>((solicitacoes[i].Data_Solicitacao.Year.ToString("0000") + solicitacoes[i].Data_Solicitacao.Month.ToString("00")), solicitacoes[i].Agenda.Procedimento.Codigo, solicitacoes[i].Agenda.Cbo.Codigo, solicitacoes[i].Agenda.Estabelecimento.CNES, co_subgrupo);
                        if (quantidadeSolicitacoes != null)
                        {
                            quantidadeSolicitacoes.QtdSolicitacoes--;
                            Factory.GetInstance<ISolicitacao>().Salvar(quantidadeSolicitacoes);
                        }

                        IList<ProcedimentoRegistro> pr = Factory.GetInstance<IRegistro>().BuscarPorProcedimento<ProcedimentoRegistro>(solicitacoes[i].Procedimento.Codigo, Registro.APAC_PROC_PRINCIPAL);
                        if (pr != null && pr.Count != 0)
                        {
                            string faixa = (solicitacoes[i].Identificador).Substring(5, 7);
                            ViverMais.Model.Faixa fx = Factory.GetInstance<IFaixa>().BuscarCodigoFaixa<Faixa>(faixa);
                            if (fx != null)
                            {
                                fx.Quantidade_utilizada -= 1;
                                Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(fx);
                            }
                        }
                    }
                    MarcarAgendaBloqueada();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Você deve imprimir a Lista de Solicitações!');", true);
                return;
            }
        }

        //protected void btnBloquearTodasAgendas_Click(object sender, EventArgs e)
        //{
        //    if (Session["Agendas"] != null)
        //    {
        //        DataTable table = (DataTable)Session["Agendas"];
        //        for (int i = 0; i < table.Rows.Count; i++)
        //        {
        //            //Pega a Agenda da Tabela
        //            string id_agenda = table.Rows[i][0].ToString();
        //            Agenda agenda = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Agenda>(int.Parse(id_agenda));
        //            if (agenda.Bloqueada == false)
        //            {
        //                agenda.Bloqueada = true;
        //                Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(agenda);
        //            }
        //        }
        //        MarcarAgendaBloqueada();
        //    }
        //}

    }
}
