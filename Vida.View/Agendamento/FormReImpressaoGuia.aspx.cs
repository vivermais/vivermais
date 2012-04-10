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
using ViverMais.View.Agendamento.Helpers;
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;

namespace ViverMais.View.Agendamento
{
    public partial class FormReImpressaoGuia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WUCPesquisarPaciente1.GridView.SelectedIndexChanging += new GridViewSelectEventHandler(GridView_SelectedIndexChanged);
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = WUCPesquisarPaciente1.GridView.UniqueID;
            trigger.EventName = "SelectedIndexChanging";
            up2.Triggers.Add(trigger);
            //WUCPesquisarPaciente1.GridView.SelectedIndexChanged += new EventHandler(GridView_SelectedIndexChanged);
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "REIMPRESSAO_GUIA", Modulo.AGENDAMENTO))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                    //PanelDadosPaciente.Visible = false;
                    //lblSemRegistros.Visible = false;
                }
            }
        }

        protected void GridViewSolicitacosPendentesAutorizadas_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSolicitacosPendentesAutorizadas.PageIndex = e.NewPageIndex;
            if (Session["Solicitacoes"] != null)
            {
                DataTable solicitacoes = (DataTable)(Session["Solicitacoes"]);
                GridViewSolicitacosPendentesAutorizadas.DataSource = solicitacoes;
                GridViewSolicitacosPendentesAutorizadas.DataBind();
                DesabilitaBotaoDeAcordoComSolicitacao();
            }
        }

        protected void GridView_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            ViverMais.Model.Paciente paciente = WUCPesquisarPaciente1.Paciente;
            Session["pacienteSelecionado"] = paciente;
            //WUCExibirPaciente1.Paciente = paciente;
            //WUCExibirPaciente1.Visible = true;
            //PanelDadosPaciente.Visible = true;
            //lblNomePaciente.Text = paciente.Nome;
            //lblNomeMae.Text = paciente.NomeMae;
            //lblDataNascimento.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
            
            DataTable table = new DataTable();
            table.Columns.Add("Codigo");
            table.Columns.Add("NomePaciente");
            table.Columns.Add("Procedimento");
            table.Columns.Add("DataSolicitacao");
            table.Columns.Add("Status");
            table.Columns.Add("Unidade");

            IList<Solicitacao> solicitacoes;
            Usuario usuario = (Usuario)Session["Usuario"];
            if (usuario.Unidade.RazaoSocial.ToUpper() == "SECRETARIA MUNICIPAL DE SAUDE DE SALVADOR")
                solicitacoes = Factory.GetInstance<ISolicitacao>().ListarSolicitacoesPorPaciente<Solicitacao>(paciente.Codigo, "");
            else
                solicitacoes = Factory.GetInstance<ISolicitacao>().ListarSolicitacoesPorPaciente<Solicitacao>(paciente.Codigo, "").Where(p=>p.EasSolicitante == usuario.Unidade.CNES).ToList();

            if ((solicitacoes.Count != 0) && (solicitacoes != null))
            {
                //lblSemRegistros.Visible = false;
 

                foreach (Solicitacao solicitacao in solicitacoes)
                {
                    DataRow row = table.NewRow();
                    row[0] = solicitacao.Codigo.ToString();
                    row[1] = paciente.Nome;
                    if (solicitacao.Procedimento != null)
                    {
                        //Procedimento proced = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(solicitacao.ID_Procedimento);
                        if ((solicitacao.Procedimento.Codigo == "0301010072") || (solicitacao.Procedimento.Codigo == "0301010064"))
                        {
                            CBO cbo = Factory.GetInstance<ICBO>().BuscarPorCodigo<CBO>(solicitacao.Agenda.Cbo.Codigo);
                            if (cbo != null)
                            {
                                row[2] = solicitacao.Procedimento.Nome + " - " + cbo.Nome;
                            }
                        }
                        else
                        {
                            row[2] = solicitacao.Procedimento.Nome;
                        }
                    }
                    row[3] = solicitacao.Data_Solicitacao.ToString("dd/MM/yyyy");
                    //row[4] = solicitacao.Status;

                    if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE).ToString())
                        row[4] = "Aguardando Avaliação";
                    else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString())
                        row[4] = "Autorizado";
                    else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AGENDAUTOMATICO).ToString())
                        row[4] = "Aguardando Autorização";
                    else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.INDEFERIDA).ToString())
                        row[4] = "indeferida";
                    else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString())
                        row[4] = "Executada";
                    else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString())
                        row[4] = "Desmarcada";

                    ViverMais.Model.EstabelecimentoSaude estabelecimento = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(solicitacao.EasSolicitante);
                    row[5] = estabelecimento.NomeFantasia;
                    table.Rows.Add(row);
                }
                GridViewSolicitacosPendentesAutorizadas.DataSource = table;
                GridViewSolicitacosPendentesAutorizadas.DataBind();
                Session["Solicitacoes"] = table;
                DesabilitaBotaoDeAcordoComSolicitacao();
            }
            else
            {
                GridViewSolicitacosPendentesAutorizadas.DataSource = table;
                GridViewSolicitacosPendentesAutorizadas.DataBind();
            //    //lblSemRegistros.Visible = true;
            //    PanelExibeSolicitacoes.Visible = false;
            }

            PanelExibeSolicitacoes.Visible = true;
            up2.Update();
        }

        protected void DesabilitaBotaoDeAcordoComSolicitacao()
        {
            DataTable tableSolicitacoes = (DataTable)Session["Solicitacoes"];
            int index = 0;
            if (tableSolicitacoes != null)
            {
                for (int i = GridViewSolicitacosPendentesAutorizadas.PageIndex * GridViewSolicitacosPendentesAutorizadas.PageSize; i < (10 * (GridViewSolicitacosPendentesAutorizadas.PageIndex + 1)); i++)
                {
                    if (i < tableSolicitacoes.Rows.Count)
                    {
                        Solicitacao solicitacao = Factory.GetInstance<ISolicitacao>().BuscarPorCodigo<Solicitacao>(int.Parse(tableSolicitacoes.Rows[i]["Codigo"].ToString()));
                        LinkButton botao;

                        if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE).ToString())
                        {
                            if (solicitacao.Identificador != null)
                            {
                                botao = (LinkButton)GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("ImprimeProtocolo");
                                if (botao != null)
                                    botao.Visible = false;
                            }
                            botao = (LinkButton)GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[7].FindControl("cmdImprimeAutorizacao");
                            if (botao != null)
                                botao.Visible = false;
                            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[9].FindControl("cmdImprimeJustificativa").Visible = false;
                        }
                        else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString())
                        {
                            if (Factory.GetInstance<ILaudo>().BuscaPorSolicitacao<Laudo>(solicitacao.Codigo.ToString()).Count != 0)//Se Existir Laudo Para A Solicitação
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = true;
                            else
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = false;

                            if (solicitacao.NumeroProtocolo != null)
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = true;
                            else
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = false;
                            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[9].FindControl("cmdImprimeJustificativa").Visible = false;
                        }
                        else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AGENDAUTOMATICO).ToString())
                        {
                            if (solicitacao.Identificador != null)
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = true;
                            else
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = false;
                            if (Factory.GetInstance<ILaudo>().BuscaPorSolicitacao<Laudo>(solicitacao.Codigo.ToString()).Count != 0)//Se Existir Laudo Para A Solicitação
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = true;
                            else
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = false;
                            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[7].FindControl("cmdImprimeAutorizacao").Visible = false;
                            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[9].FindControl("cmdImprimeJustificativa").Visible = false;
                        }
                        else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.INDEFERIDA).ToString())
                        {
                            if (solicitacao.NumeroProtocolo != null)
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = true;
                            if (Factory.GetInstance<ILaudo>().BuscaPorSolicitacao<Laudo>(solicitacao.Codigo.ToString()).Count != 0)//Se Existir Laudo Para A Solicitação
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = true;
                            else
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = false;
                            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[7].FindControl("cmdImprimeAutorizacao").Visible = false;
                            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[9].FindControl("cmdImprimeJustificativa").Visible = true;

                        }
                        else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString())
                        {
                            if (solicitacao.NumeroProtocolo != null)
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = true;
                            if (Factory.GetInstance<ILaudo>().BuscaPorSolicitacao<Laudo>(solicitacao.Codigo.ToString()).Count != 0)//Se Existir Laudo Para A Solicitação
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = true;
                            else
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = false;
                            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[9].FindControl("cmdImprimeJustificativa").Visible = false;
                        }
                        else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString())
                        {
                            if (solicitacao.NumeroProtocolo != null)
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = true;
                            if (Factory.GetInstance<ILaudo>().BuscaPorSolicitacao<Laudo>(solicitacao.Codigo.ToString()).Count != 0)//Se Existir Laudo Para A Solicitação
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = true;
                            else
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = false;
                            if(solicitacao.Agenda == null)
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[7].FindControl("cmdImprimeAutorizacao").Visible = false;
                            else
                                GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[7].FindControl("cmdImprimeAutorizacao").Visible = true;
                            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[9].FindControl("cmdImprimeJustificativa").Visible = false;
                        }
                        //switch (solicitacao.Situacao)
                        //{

                        //    case "1": //Pendente
                        //        if (solicitacao.Identificador != null)
                        //        {
                        //            botao = (LinkButton)GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("ImprimeProtocolo");
                        //            if(botao != null)
                        //                botao.Visible = false;
                        //        }

                        //        botao = (LinkButton)GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[7].FindControl("cmdImprimeAutorizacao");
                        //        if(botao != null)
                        //            botao.Visible = false;

                        //        break;
                        //    case "2": //Autorizada
                        //        if(Factory.GetInstance<ILaudo>().BuscaPorSolicitacao<Laudo>(solicitacao.Codigo.ToString()).Count != 0)//Se Existir Laudo Para A Solicitação
                        //            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = true;
                        //        else
                        //            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = false;

                        //        if(solicitacao.NumeroProtocolo != null)
                        //            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = true;
                        //        else
                        //            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = false;
                        //        break;
                        //    case "3": //Ag. Automático
                        //        if (solicitacao.Identificador != null)
                        //            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = true;
                        //        else
                        //            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[8].FindControl("cmdImprimeProtocolo").Visible = false;
                        //        if (Factory.GetInstance<ILaudo>().BuscaPorSolicitacao<Laudo>(solicitacao.Codigo.ToString()).Count != 0)//Se Existir Laudo Para A Solicitação
                        //            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = true;
                        //        else
                        //            GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[6].FindControl("cmdImprimeLaudo").Visible = false;
                        //        GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[7].FindControl("cmdImprimeAutorizacao").Visible = false;
                        //        break;
                        //}
                        //if (tableSolicitacoes.Rows[i][5].ToString() == "1")
                        //{
                        //    GridViewAgendas.Rows[index].BackColor = Color.DimGray;
                        //    GridViewAgendas.Rows[index].Cells[1].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        //    GridViewAgendas.Rows[index].Cells[2].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        //    GridViewAgendas.Rows[index].Cells[3].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        //    GridViewAgendas.Rows[index].Cells[4].Style.Add(HtmlTextWriterStyle.Color, "#FFFFFF");
                        //    GridViewSolicitacosPendentesAutorizadas.Rows[index].Cells[7].FindControl("btnBloquearAgenda").Visible = false;
                        //}
                    }
                    else
                        break;

                    index++;
                }
            }
        }

        protected void GridViewSolicitacosPendentesAutorizadas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id_solicitacao = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "ImprimeLaudo")
            {
                Redirector.Redirect("ExibeLaudo.aspx?id_solicitacao=" + id_solicitacao, "_blank", "");
            }
            else if (e.CommandName == "ImprimeAutorizacao")
            {
                Redirector.Redirect("RelatorioSolicitacao.aspx?id_solicitacao=" + id_solicitacao, "_blank", "");
            }
            else if (e.CommandName == "ImprimeProtocolo") //Impressão do Protocolo
            {
                Redirector.Redirect("ImpressaoProtocolo.aspx?id_solicitacao=" + id_solicitacao, "_blank", "");
            }
            else if (e.CommandName == "ImprimeJustificativa")
            {
                Redirector.Redirect("RelatorioIndeferimentoSolicitacao.aspx?id_solicitacao=" + id_solicitacao, "_blank", "");
            }
            Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 27, "ID_SOLICITACAO: " + id_solicitacao));

        }
    }
}
