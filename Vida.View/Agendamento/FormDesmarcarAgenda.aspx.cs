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
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.BLL;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Agendamento
{
    public partial class FormDesmarcarAgenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_DESMARCAR_AGENDAMENTO", Modulo.AGENDAMENTO))
                {
                    PanelExibeDadosAgendamento.Visible = false;
                    PanelExibeSolicitacoes.Visible = false;
                    if (Request.QueryString["id_solicitacao"] != null)
                    {
                        IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                        int id_solicitacao = int.Parse(Request.QueryString["id_solicitacao"]);
                        Solicitacao solicitacao = iAgendamento.BuscarPorCodigo<Solicitacao>(id_solicitacao);
                        CarregaDadosDaSolicitacao(solicitacao);
                        Session["id_solicitacao"] = id_solicitacao;
                        PanelExibeSolicitacoes.Visible = false;
                        PanelExibeDadosAgendamento.Visible = true;
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
            WUCPesquisarPaciente1.GridView.SelectedIndexChanging += new GridViewSelectEventHandler(GridView_SelectedIndexChanged);
            //            WUCPesquisarPaciente1.GridView.SelectedIndexChanged += new EventHandler(GridView_SelectedIndexChanged);
        }

        private void CarregaDadosDaSolicitacao(Solicitacao solicitacao)
        {


            if (solicitacao.Agenda != null)
            {
                lblDataAgenda.Text = solicitacao.Agenda.Data.ToShortDateString();
                lblEstabelecimento.Text = solicitacao.Agenda.Estabelecimento.NomeFantasia;
            }
            else
            {
                lblDataAgenda.Text = " - ";
                lblEstabelecimento.Text = " - ";
            }

            //ViverMais.Model.Procedimento procedimento = ;
            lblProcedimento.Text = solicitacao.Procedimento.Nome;
            lblCartaoSus.Text = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(solicitacao.ID_Paciente).FirstOrDefault().Numero;
            lblPaciente.Text = PacienteBLL.Pesquisar(solicitacao.ID_Paciente).Nome;

        }

        protected void GridView_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            PanelExibeSolicitacoes.Visible = true;
            ViverMais.Model.Paciente paciente = WUCPesquisarPaciente1.Paciente;
            string id_paciente = paciente.Codigo;

            IList<ViverMais.Model.Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().ListarSolicitacoesPendentesAutorizadasPorPaciente<Solicitacao>(id_paciente);
            if (solicitacoes.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Nenhum registro encontrado!')", true);
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Nenhum registro encontrado nestas condições !');</script>");
                lblSemRegistro.Visible = true;
                UpdatePanelPaciente.Update();
                UpdatePanelSolicitacoes.Update();
                return;
            }
            DataTable table = new DataTable();
            DataColumn c0 = new DataColumn("Codigo");
            DataColumn c1 = new DataColumn("Nome");
            DataColumn c2 = new DataColumn("Data_Agenda");
            DataColumn c3 = new DataColumn("Procedimento");
            DataColumn c4 = new DataColumn("UnidadeSolicitante");
            DataColumn c5 = new DataColumn("DataSolicitacao");

            table.Columns.Add(c0);
            table.Columns.Add(c1);
            table.Columns.Add(c2);
            table.Columns.Add(c3);
            table.Columns.Add(c4);
            table.Columns.Add(c5);

            foreach (Solicitacao solicitacao in solicitacoes)
            {

                DataRow row = table.NewRow();
                DateTime datanascimento = paciente.DataNascimento;

                //if (solicitacao.Agenda != null)
                //{
                lblSemRegistro.Visible = false;
                //ViverMais.Model.Procedimento procedimento = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Procedimento>(solicitacao.ID_Procedimento);
                row[0] = solicitacao.Codigo;
                row[1] = paciente.Nome;

                if (solicitacao.Agenda != null)
                {
                    row[2] = solicitacao.Agenda.Data.ToString("dd/MM/yyyy");
                    if ((solicitacao.Procedimento.Codigo == "0301010072") || (solicitacao.Procedimento.Codigo == "0301010064"))
                        row[3] = solicitacao.Agenda.Procedimento.Nome + " - " + solicitacao.Agenda.Cbo.Nome;
                    else
                        row[3] = solicitacao.Procedimento.Nome;
                }
                else
                {
                    row[2] = "Pendente";
                    row[3] = solicitacao.Procedimento.Nome;
                }


                //row[2] = solicitacao.Agenda != null ? solicitacao.Agenda.Data.ToString("dd/MM/yyyy") : string.Empty;
                //    if ((procedimento.Codigo == "0301010072") || (procedimento.Codigo == "0301010064"))
                //    {
                //        CBO cbo = Factory.GetInstance<ICBO>().BuscarPorCodigo<CBO>(solicitacao.Agenda.Cbo.Codigo);
                //        if (cbo != null)

                //            row[3] = procedimento.Nome + " - " + cbo.Nome;
                //    }
                //    else
                //    {
                //        row[3] = procedimento.Nome;
                //    }
                row[4] = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(solicitacao.EasSolicitante).NomeFantasia;
                row[5] = solicitacao.Data_Solicitacao.ToString("dd/MM/yyyy");
                table.Rows.Add(row);
                //}
            }
            PanelExibeSolicitacoes.Visible = true;
            gridSolicitacao.DataSource = table;
            gridSolicitacao.DataBind();
            Session["Solicitacoes"] = table;
            PanelExibeDadosAgendamento.Visible = false;
            UpdatePanelPaciente.Update();
            UpdatePanelSolicitacoes.Update();
        }

        protected void gridSolicitacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridSolicitacao.PageIndex = e.NewPageIndex;
            DataTable table = (DataTable)Session["Solicitacoes"];
            gridSolicitacao.DataSource = table;
            gridSolicitacao.DataBind();
        }

        protected void Salvar_Click(object sender, EventArgs e)
        {
            int id_solicitacao = int.Parse(Session["id_solicitacao"].ToString());
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            Solicitacao solicitacao = iAgendamento.BuscarPorCodigo<Solicitacao>(id_solicitacao);

            if (solicitacao.Agenda != null)
            {
                ViverMais.Model.Parametros parametro = iAgendamento.ListarTodos<ViverMais.Model.Parametros>().FirstOrDefault();
                DateTime dataLimite = solicitacao.Agenda.Data.AddDays(-parametro.Min_Dias_Cancela);
                if (DateTime.Now <= dataLimite)
                {
                    IList<ProcedimentoRegistro> pr = Factory.GetInstance<IRegistro>().BuscarPorProcedimento<ProcedimentoRegistro>(solicitacao.Procedimento.Codigo, Registro.APAC_PROC_PRINCIPAL);
                    if (pr != null && pr.Count != 0)
                    {
                        string faixa = (solicitacao.Identificador).Substring(5, 7);
                        ViverMais.Model.Faixa fx = Factory.GetInstance<IFaixa>().BuscarCodigoFaixa<Faixa>(faixa);
                        if (fx != null)
                        {
                            fx.Quantidade_utilizada -= 1;
                            iAgendamento.Salvar(fx);
                            ViverMais.Model.FaixaUtilizada fx_utilizada = Factory.GetInstance<IFaixaUtilizada>().BuscaFaixaUtilizadaPorNumeroFaixa<FaixaUtilizada>(faixa,fx.Codigo.ToString());
                            iAgendamento.Deletar(fx_utilizada);
                        }
                    }

                    solicitacao.Agenda.QuantidadeAgendada--;
                    iAgendamento.Salvar(solicitacao.Agenda);

                    if (solicitacao.TipoCotaUtilizada == Convert.ToChar(Solicitacao.TipoCota.REDE))
                    {
                        int co_subgrupo = solicitacao.Agenda.SubGrupo != null ? solicitacao.Agenda.SubGrupo.Codigo : 0;
                        QuantidadeSolicitacaoRede quantidadeSolicitacoes = Factory.GetInstance<IQuantidadeSolicitacaoRede>().BuscaQuantidade<QuantidadeSolicitacaoRede>((solicitacao.Data_Solicitacao.Year.ToString("0000") + solicitacao.Data_Solicitacao.Month.ToString("00")), solicitacao.Agenda.Procedimento.Codigo, solicitacao.Agenda.Cbo.Codigo, solicitacao.Agenda.Estabelecimento.CNES, co_subgrupo);
                        if (quantidadeSolicitacoes != null)
                        {
                            quantidadeSolicitacoes.QtdSolicitacoes--;
                            iAgendamento.Salvar(quantidadeSolicitacoes);
                        }
                    }

                    //Se a solicitação utilizou a cota de algum município, ele irá devolver o valor

                    //Decimal valorProcedimento = Decimal.Parse(procedimento.VL_SA.ToString().Insert(procedimento.VL_SA.ToString().Length - 2, ","));
                    if (solicitacao.PactoReferenciaSaldo != null)
                    {
                        //float valorProcedimento = float.Parse(solicitacao.Agenda.Procedimento.VL_SA.ToString().Insert(solicitacao.Agenda.Procedimento.VL_SA.ToString().Length - 2, ","));
                        PactoReferenciaSaldo pactoReferenciaSaldo = iAgendamento.BuscarPorCodigo<PactoReferenciaSaldo>(solicitacao.PactoReferenciaSaldo.Codigo);
                        pactoReferenciaSaldo.ValorRestante += solicitacao.Agenda.Procedimento.ValorProcedimentoAmbulatorialFormatado;
                        //pactoReferenciaSaldo.ValorRestante = pactoReferenciaSaldo.ValorRestante + solicitacao.Agenda.Procedimento.VL_SA;
                        iAgendamento.Salvar(pactoReferenciaSaldo);
                    }
                    // Se ele utilizou a cota de abrangencia, irá devolver o valor
                    else if (solicitacao.PactoAbrangenciaAgregado != null)
                    {
                        PactoAbrangenciaAgregado pactoAbrangenciaAgregado = iAgendamento.BuscarPorCodigo<PactoAbrangenciaAgregado>(solicitacao.PactoAbrangenciaAgregado.Codigo);
                        if (pactoAbrangenciaAgregado != null)
                        {
                            Decimal valorProcedimentoDecimal = Decimal.Parse(solicitacao.Agenda.Procedimento.VL_SA.ToString().Insert(solicitacao.Agenda.Procedimento.VL_SA.ToString().Length - 2, ","));
                            pactoAbrangenciaAgregado.ValorUtilizado -= valorProcedimentoDecimal;
                            iAgendamento.Salvar(pactoAbrangenciaAgregado);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O Agendamento não poderá ser desmarcado pois esta fora do prazo para o cancelamento!');", true);
                    //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('O Agendamento não poderá ser desmarcado pois esta fora do prazo para o cancelamento!');window.location='FormDesmarcarAgenda.aspx'</script>");
                    return;

                }
            }

            solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString();
            solicitacao.JustificativaDesmarcar = tbxJustificativa.Text;
            iAgendamento.Salvar(solicitacao);

            iAgendamento.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 13, "ID_SOLICITACAO:" + solicitacao.Codigo));



            //DateTime data_max = DateTime.Now.AddDays(-parametro[0].Min_Dias_Cancela);
            //DateTime data_max = DateTime.Parse(lblDataAgenda.Text);
            //data_max = data_max.AddDays(-parametro[0].Min_Dias_Cancela);
            //DateTime hoje = DateTime.Today;
            //if (hoje <= data_max)
            //{

            //IList<Laudo> laudos = Factory.GetInstance<ILaudo>().BuscaPorSolicitacao<Laudo>(id_solicitacao.ToString());
            ////Deleta os Laudos Referente a solicitacao
            //if (laudos.Count != 0)
            //{
            //    for (int i = 0; i < laudos.Count; i++)
            //        iagendamento.Deletar(laudos[i]);
            //}

            // Caso tenha agenda Vai subtrair 1 à Quantidade agendada


            //if (solicitacao.Agenda != null)
            //{
            //    Agenda agenda = solicitacao.Agenda;
            //    agenda.QuantidadeAgendada--;
            //    iagendamento.Salvar(agenda);
            //}

            //IList<ProcedimentoRegistro> pr = Factory.GetInstance<IRegistro>().BuscarPorProcedimento<ProcedimentoRegistro>(solicitacao.ID_Procedimento, Registro.APAC_PROC_PRINCIPAL);
            //if (pr != null && pr.Count != 0)
            //{
            //    string faixa = (solicitacao.Identificador).Substring(5, 7);
            //    ViverMais.Model.Faixa fx = Factory.GetInstance<IFaixa>().BuscarCodigoFaixa<Faixa>(faixa);
            //    if (fx != null)
            //    {
            //        fx.Quantidade_utilizada -= 1;
            //        iagendamento.Salvar(fx);
            //    }
            //}

            // Vai Desmarcar a solicitacao

            //solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString();
            //solicitacao.JustificativaDesmarcar = tbxJustificativa.Text;
            //iagendamento.Salvar(solicitacao);
            //iagendamento.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 13, "ID_SOLICITACAO:" + solicitacao.Codigo + " ID_AGENDA:" + solicitacao.Agenda.Codigo != "0" ? solicitacao.Agenda.Codigo.ToString() : "Sol.Pendente"));

            //if (solicitacao.TipoCotaUtilizada == Convert.ToChar(Solicitacao.TipoCota.REDE))
            //{
            //    QuantidadeSolicitacaoRede quantidadeSolicitacoes = Factory.GetInstance<IQuantidadeSolicitacaoRede>().BuscaQuantidade<QuantidadeSolicitacaoRede>((solicitacao.Data_Solicitacao.Year.ToString("0000") + solicitacao.Data_Solicitacao.Month.ToString("00")), solicitacao.Agenda.Procedimento.Codigo, solicitacao.Agenda.Cbo.Codigo, solicitacao.Agenda.Estabelecimento.CNES);
            //    if (quantidadeSolicitacoes != null)
            //    {
            //        quantidadeSolicitacoes.QtdSolicitacoes--;
            //        iagendamento.Salvar(quantidadeSolicitacoes);
            //    }
            //}

            //Procedimento procedimento = iagendamento.BuscarPorCodigo<Procedimento>(solicitacao.Agenda.Procedimento.Codigo);
            //if (procedimento != null)
            //{
            //    //Se a solicitação utilizou a cota de algum município, ele irá devolver o valor
            //    if (solicitacao.PactoReferenciaSaldo != null)
            //    {
            //        PactoReferenciaSaldo pactoReferenciaSaldo = iagendamento.BuscarPorCodigo<PactoReferenciaSaldo>(solicitacao.PactoReferenciaSaldo.Codigo);
            //        pactoReferenciaSaldo.ValorRestante = pactoReferenciaSaldo.ValorRestante + procedimento.VL_SA;
            //        iagendamento.Salvar(pactoReferenciaSaldo);
            //    }
            //    // Se ele utilizou a cota de abrangencia, irá devolver o valor
            //    else if (solicitacao.PactoAbrangenciaAgregado != null)
            //    {
            //        PactoAbrangenciaAgregado pactoAbrangenciaAgregado = iagendamento.BuscarPorCodigo<PactoAbrangenciaAgregado>(solicitacao.PactoAbrangenciaAgregado.Codigo);

            //        ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
            //        if (paciente != null)
            //        {
            //            Endereco endereco = EnderecoBLL.PesquisarPorPaciente(paciente);
            //            if (endereco != null)
            //            {
            //                PactoAbrangenciaGrupoMunicipio pactoAbrangenciaGrupoMunicipio = Factory.GetInstance<IPactoAbrangenciaAgregado>().ListarPactoAbrangenciaGrupoMunicipioPorPactoAbrangenciaAgregado<PactoAbrangenciaGrupoMunicipio>(pactoAbrangenciaAgregado.Codigo).Where(p => p.Municipio.Codigo == endereco.Municipio.Codigo).FirstOrDefault();
            //                Decimal valorProcedimento = Decimal.Parse(procedimento.VL_SA.ToString().Insert(procedimento.VL_SA.ToString().Length - 2, ","));
            //                pactoAbrangenciaGrupoMunicipio.ValorUtilizado -= valorProcedimento;
            //                iagendamento.Salvar(pactoAbrangenciaGrupoMunicipio);
            //            }
            //        }
            //    }
            //}

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Solicitação Desmarcada com Sucesso!'); location='FormDesmarcarAgenda.aspx';", true);
            //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Agenda desmarcada com sucesso!');window.location='FormDesmarcarAgenda.aspx'</script>");
            return;
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O Agendamento não poderá ser desmarcado pois esta fora do prazo para o cancelamento!');", true);
            //    //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('O Agendamento não poderá ser desmarcado pois esta fora do prazo para o cancelamento!');window.location='FormDesmarcarAgenda.aspx'</script>");
            //    return;

            //}
        }
    }
}