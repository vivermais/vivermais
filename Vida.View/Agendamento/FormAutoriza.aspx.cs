﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.BLL;
using System.Drawing;
using System.IO;
using System.Text;

namespace ViverMais.View.Agendamento
{
    public partial class FormAutoriza : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MOVIMENTACAO_AUTORIZACAO_AMBULATORIAL", Modulo.AGENDAMENTO))
                {
                    lknAnterior.Enabled = false;
                    int id_solicitacao = int.Parse(Request.QueryString["id_solicitacao"]);
                    ViewState["id_solicitacao"] = id_solicitacao;
                    Solicitacao solicitacao = Factory.GetInstance<IAgendamentoServiceFacade>().BuscarPorCodigo<Solicitacao>(id_solicitacao);
                    if (solicitacao != null)
                    {
                        CarregaDadosDaSolicitacao(solicitacao);
                        CarregaEspecialidades(solicitacao.Procedimento.Codigo);
                        CarregaLaudosDaSolicitacao(solicitacao);
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        public byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;

            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

                // get total byte length of the file
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                // read entire file into buffer
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                // close file reader
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            return _Buffer;
        }


        void CarregaDadosDaSolicitacao(Solicitacao solicitacao)
        {
            lblDataSolicitacao.Text = solicitacao.Data_Solicitacao.ToShortDateString();
            DateTime data_solicitacao = solicitacao.Data_Solicitacao;
            rbtPrioridade.SelectedValue = solicitacao.Prioridade;
            rbtSituacao.SelectedValue = solicitacao.Situacao;
            lblObservacao.Text = String.IsNullOrEmpty(solicitacao.Observacao) ? "-" : solicitacao.Observacao;
            lblProcedimento.Text = solicitacao.Procedimento.Nome;
            ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
            //Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(solicitacao.ID_Paciente);
            lblNomePaciente.Text = paciente.Nome;
            lblCNS.Text = CartaoSUSBLL.ListarPorPaciente(paciente).FirstOrDefault().Numero;
            tbxJustificativa.Text = solicitacao.JustificativaIndeferimento;
            Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
            try
            {
                lblMunicipio.Text = endereco.Municipio.Nome;
            }
            catch (NullReferenceException)
            {
                lblMunicipio.Text = string.Empty;
            }

        }

        void CarregaEspecialidades(string Id_Procedimento)
        {
            TipoProcedimento tipoProcedimento = Factory.GetInstance<ITipoProcedimento>().BuscaTipoProcedimento<TipoProcedimento>(Id_Procedimento);

            // Busca os CBOs do Procedimento Selecionado
            IList<ViverMais.Model.CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(Id_Procedimento);
            IList<int> ContemVagas = new List<int>();
            bool existe = false;
            foreach (ViverMais.Model.CBO cbo in cbos)
            {
                if (cbo.CategoriaOcupacao != null)
                {
                    if (cbo.CategoriaOcupacao.Codigo == "1")//Se for médico
                    {
                        existe = true;
                        // Busca as vagas do CBO e Procedimento Selecionado
                        IList ag = Factory.GetInstance<IAmbulatorial>().BuscarVagas(cbo.Codigo, Id_Procedimento);
                        if (ag.Count > 0)
                        {
                            object[] item = (object[])ag[0];
                            int qtd = int.Parse(item[0].ToString());

                            int qtd_agendada = int.Parse(item[1].ToString());
                            //verifica se possui vagas para aquele CBO
                            if ((qtd - qtd_agendada) > 0)
                            {
                                rbtnEspecialidade.Items.Add(new ListItem(cbo.Nome, cbo.Codigo.ToString()));
                                ContemVagas.Add(rbtnEspecialidade.Items.Count - 1);
                                Session["Vagas"] = ContemVagas;
                                // Caso possua vaga a cor do CBO ficará Verde
                                ((ListItem)rbtnEspecialidade.Items.FindByValue(cbo.Codigo.ToString())).Attributes.CssStyle.Add("color", "#3b7d3b");
                            }
                            else
                            {
                                // Caso não possua vaga a cor do CBO ficara Vermelho
                                rbtnEspecialidade.Items.Add(new ListItem(cbo.Nome, cbo.Codigo.ToString()));
                                ((ListItem)rbtnEspecialidade.Items.FindByValue(cbo.Codigo.ToString())).Enabled = false;
                            }
                        }
                    }
                }
            }
            if (!existe)
                lblSemEspecialidade.Visible = true;
            else
                lblSemEspecialidade.Visible = false;

        }



        protected void GridViewAgenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = GridViewAgenda.SelectedIndex;
            if (Session["IndexAgendaSelecionada"] == null)
            {
                GridViewAgenda.Rows[index].BackColor = Color.FromArgb(238, 244, 248);
                Session["IndexAgendaSelecionada"] = index;
            }
            else
            {
                int indexAntigo = int.Parse(Session["IndexAgendaSelecionada"].ToString());
                GridViewAgenda.Rows[indexAntigo].BackColor = Color.Transparent;
                GridViewAgenda.Rows[index].BackColor = Color.FromArgb(238, 244, 248);
                Session["IndexAgendaSelecionada"] = index;
            }

            int codigoAgenda = int.Parse(GridViewAgenda.Rows[index].Cells[10].Text);
            Agenda agenda = Factory.GetInstance<IAmbulatorial>().BuscarPorCodigo<Agenda>(codigoAgenda);
            Session["Agenda"] = agenda;
        }

        void CarregaAgendas(IList<Agenda> agendas)
        {
            GridViewAgenda.DataSource = agendas;
            GridViewAgenda.DataBind();
            Session["Agendas"] = agendas;
        }

        protected void GridViewAgenda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewAgenda.DataSource = Session["Agendas"];
            GridViewAgenda.PageIndex = e.NewPageIndex;
            GridViewAgenda.DataBind();

        }

        protected int CalculaIdade(DateTime dataatual, DateTime datanascimento)
        {
            int idade = dataatual.Year - datanascimento.Year;
            if (dataatual.Month < datanascimento.Month || (dataatual.Month == datanascimento.Month &&
                dataatual.Day < datanascimento.Day))
                idade--;
            return idade;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int id_solicitacao = int.Parse(ViewState["id_solicitacao"].ToString());
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            Solicitacao solicitacao = iAgendamento.BuscarPorCodigo<Solicitacao>(id_solicitacao);
            Procedimento procedimentoSelecionado = solicitacao.Procedimento;
            //Decimal valorProcedimentoDecimal = Decimal.Parse(procedimentoSelecionado.VL_SA.ToString().Insert(procedimentoSelecionado.VL_SA.ToString("000").Length - 2, ","));
            //float valorProcedimentoFloat = float.Parse(procedimentoSelecionado.VL_SA.ToString().Insert(procedimentoSelecionado.VL_SA.ToString("000").Length - 2, ","));

            bool salvarPactoReferencia = false;
            bool salvarPactoAbrangencia = false;
            PactoReferenciaSaldo pactoReferenciaSaldoMesAtual = null;
            PactoAbrangenciaAgregado pactoAbrangenciaAgregado = null;
            if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString())//Se a Solicitação já estiver autorizada
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "key", "alert('Esta Solicitação já está autorizada!');window.close(); window.opener.location.reload();", true);
                Session.Remove("indexAgenda");
                ViewState.Remove("id_solicitacao");
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Esta Solicitação já está autorizada');</script>");
                return;
            }
            SalvaProducaoMedicoRegulador(solicitacao);
            ViverMais.Model.Usuario usuario = (Usuario)Session["Usuario"];

            if (rbtSituacao.SelectedValue == Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE).ToString())
            {
                solicitacao.Prioridade = rbtPrioridade.SelectedValue;
                solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE).ToString();
            }
            else if (rbtSituacao.SelectedValue == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString())
            {
                int indexAgenda = 0;
                if (Session["indexAgenda"] == null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Se irá autorizar a Realização do Procedimento, Por Favor Selecione uma Agenda.');", true);
                    return;
                }
                else
                    indexAgenda = int.Parse(Session["indexAgenda"].ToString());

                int codigo_agenda = int.Parse(GridViewAgenda.Rows[indexAgenda].Cells[10].Text);
                ViverMais.Model.Agenda agenda = Factory.GetInstance<IAmbulatorial>().BuscarPorCodigo<Agenda>(codigo_agenda);
                if (agenda.Quantidade - agenda.QuantidadeAgendada <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A vaga já foi preenchida! Por favor, Selecione outra Agenda.');", true);
                    rbtnEspecialidade_SelectedIndexChanged(new object(), new EventArgs());
                    return;
                }

                IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

                ViverMais.Model.Paciente paciente1 = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
                //Procedimento procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(solicitacao.ID_Procedimento);
                //IList<ProcedimentoRegistro> pr = Factory.GetInstance<IRegistro>().BuscarPorProcedimento<ProcedimentoRegistro>(solicitacao.Procedimento.Codigo, Registro.APAC_PROC_PRINCIPAL);


                #region Salva o Pacto Para o Município (Exceto Salvador)

                //Atualiza o Valor Restante do Pacto do Município
                Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente1);

                if (endereco.Municipio.Codigo != Municipio.SALVADOR)//Codigo de Salvador
                {
                    if (solicitacao.Procedimento.FinanciamentoProcedimento.Codigo != FinanciamentoProcedimento.FAEC)
                    {
                        //Pego o PactoAgregadoProcedCBO caso na Restrição do Pacto Por Referencia ele Permitiu a realização do Procedimento
                        PactoAgregadoProcedCBO pactoAgregadoProcedCBO = (PactoAgregadoProcedCBO)Session["pactoAgregadoProcedCBO"];
                        pactoAbrangenciaAgregado = (PactoAbrangenciaAgregado)Session["PactoAbrangencia"];
                        if (pactoAgregadoProcedCBO != null)
                        {
                            IList<PactoReferenciaSaldo> pactosReferenciaSaldo = Factory.GetInstance<IPactoReferenciaSaldo>().BuscarPorPactoAgregado<PactoReferenciaSaldo>(pactoAgregadoProcedCBO.Codigo);
                            pactoReferenciaSaldoMesAtual = pactosReferenciaSaldo.Where(p => p.Mes == DateTime.Now.Month).ToList()[0];
                            DateTime dataMesAnterior = DateTime.Now.AddMonths(-1);



                            //Verifico se o saldo, somado com o procedimento selecionado será superior ao Teto mensal
                            if (pactoReferenciaSaldoMesAtual.ValorRestante < procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado + pactoReferenciaSaldoMesAtual.ValorRestante)
                            {
                                //Verifico se o Pacto está Bloqueado por cota
                                if (pactoReferenciaSaldoMesAtual.PactoAgregadoProcedCBO.BloqueiaCota == Convert.ToInt32(PactoAgregadoProcedCBO.BloqueadoPorCota.NAO))
                                {
                                    //Verifica o Percentual Adicional que poderá ultrapassar do pacto
                                    Decimal valorComPercentual = pactoAgregadoProcedCBO.ValorMensal + ((pactoAgregadoProcedCBO.ValorMensal * pactoReferenciaSaldoMesAtual.PactoAgregadoProcedCBO.Percentual) / 100);
                                    if (procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado + pactoReferenciaSaldoMesAtual.ValorRestante > valorComPercentual)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Cota Excedida para o município " + endereco.Municipio.Nome + "!');", true);
                                        rbtnEspecialidade.ClearSelection();
                                        return;
                                    }
                                    else
                                    {
                                        if (Pacto.SaldoAcumulativo == Convert.ToInt32(Pacto.StatusSaldoAcumulativo.SIM))
                                        {
                                            if (pactosReferenciaSaldo.Where(p => p.Mes == dataMesAnterior.Month).ToList().Count != 0)
                                            {
                                                PactoReferenciaSaldo pactoReferenciaSaldoMesAnterior = pactosReferenciaSaldo.Where(p => p.Mes == dataMesAnterior.Month).ToList()[0];
                                                if (pactoReferenciaSaldoMesAnterior.TranferiuPactoMesSeguinte == Convert.ToInt32(PactoReferenciaSaldo.StatusTranferiuPactoMesSegunte.NAOTRANSFERIU))
                                                {
                                                    TransfereSaldoPactoMesAnteriorParaMesAtual(pactoReferenciaSaldoMesAnterior, pactoReferenciaSaldoMesAtual);
                                                }
                                            }
                                        }
                                        pactoReferenciaSaldoMesAtual.ValorRestante -= procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado;
                                    }
                                }
                                else // Se está Bloqueado Por Cota e não tem Saldo, irá alertar ao Usuário
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Cota Excedida para o município " + endereco.Municipio.Nome + "!');", true);
                                    rbtnEspecialidade.ClearSelection();
                                    return;
                                }
                            }
                            else
                            {
                                pactoReferenciaSaldoMesAtual.ValorRestante -= procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado;
                            }
                            salvarPactoReferencia = true;
                            solicitacao.PactoReferenciaSaldo = pactoReferenciaSaldoMesAtual;
                        }
                        //Pego o PactoAbrangenciaAgregado caso na Restrição do Pacto Por Abrangencia ele Permitiu a realização do Procedimento
                        else if (pactoAbrangenciaAgregado != null)
                        {
                            if (!Factory.GetInstance<ISolicitacao>().RestricaoPactoAbrangencia(endereco.Municipio.Codigo, procedimentoSelecionado.Codigo, rbtnEspecialidade.SelectedValue))
                            {
                                solicitacao.PactoAbrangenciaAgregado = pactoAbrangenciaAgregado;
                                pactoAbrangenciaAgregado.ValorUtilizado += procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado;
                                salvarPactoAbrangencia = true;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Houve um Erro ao Salvar. Por favor, Selecione a Especialidade Novamente.');", true);
                            rbtnEspecialidade.ClearSelection();
                            Session.Remove("Agendas");
                            return;
                        }
                    }
                }
                #endregion

                solicitacao.Prioridade = rbtPrioridade.SelectedValue;
                solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString();
                //solicitacao.Agenda = agenda;
                //if (pr.Count != 0)//Se For APAC, pega o Identificador APAC
                //{
                //    solicitacao.Identificador = Factory.GetInstance<ISolicitacao>().GeraIdentificadorAPAC();
                //    if (solicitacao.Identificador == "0")
                //    {
                //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "key", "alert('O Sistema não possuiu mais faixa APAC Cadastrada!Cadastre uma nova Faixa ou entre em contato com a administração!');", true);
                //        //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Esta Solicitação já está autorizada');</script>");
                //        return;
                //    }
                //}
                solicitacao.UsuarioRegulador = usuario;
                
                try
                {
                    Factory.GetInstance<ISolicitacao>().AutorizaSolicitacaoReguladaAutorizada<Solicitacao, Agenda>(solicitacao, agenda);
                    iAgendamento.Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 29, "ID: " + solicitacao.Codigo));
                    
                }
                catch (Exception f)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Ocorreu um erro: " + f.Message + "');", true);
                    return;
                }
                
                Redirector.Redirect("RelatorioSolicitacao.aspx?id_solicitacao=" + solicitacao.Codigo, "_blank", "");
            }
            else if (rbtSituacao.SelectedValue == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AGENDAUTOMATICO).ToString())
            {
                if (Session["indexAgenda"] != null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Você não pode colocar a Solicitação como Ag. Automático e Selecionar uma Agenda.');", true);
                    return;
                }
                solicitacao.Prioridade = rbtPrioridade.SelectedValue;
                solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.AGENDAUTOMATICO).ToString();
                iAgendamento.Salvar(solicitacao);
            }
            else if (rbtSituacao.SelectedValue == Convert.ToChar(Solicitacao.SituacaoSolicitacao.INDEFERIDA).ToString())
            {
                if (tbxJustificativa.Text.Equals(String.Empty))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe a Justificativa para o Indeferimento da Solicitação!');", true);
                    return;
                }
                solicitacao.Prioridade = rbtPrioridade.SelectedValue;
                solicitacao.DataIndeferimento = DateTime.Now;
                solicitacao.UsuarioRegulador = (Usuario)Session["Usuario"];
                solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.INDEFERIDA).ToString();
                solicitacao.JustificativaIndeferimento = tbxJustificativa.Text;
                iAgendamento.Salvar(solicitacao);
                Redirector.Redirect("RelatorioIndeferimentoSolicitacao.aspx?id_solicitacao=" + solicitacao.Codigo, "_blank", "");
            }


            //iAgendamento.Salvar(solicitacao);
            if (salvarPactoAbrangencia)
                iAgendamento.Salvar(pactoAbrangenciaAgregado);
            else if (salvarPactoReferencia)
                iAgendamento.Salvar(pactoReferenciaSaldoMesAtual);
            iAgendamento.Inserir(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 31, "ID Solicitacao: " + solicitacao.Codigo.ToString()));
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');parent.parent.location = 'FormSolicitacoesPorPaciente.aspx?Codigo=" + solicitacao.Codigo.ToString() + "'; parent.parent.GB_hide();", true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "parent.parent.GB_hide();", true);
            //window.close(); window.opener.location.reload();", true);
            //SalvaProducaoMedicoRegulador(solicitacao);
            Session.Remove("indexAgenda");
            ViewState.Remove("id_solicitacao");

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "function", "history.back()", true);
        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            
        }

        void CarregaLaudosDaSolicitacao(Solicitacao solicitacao)
        {
            // Busca endereco da Imagem
            List<string> enderecos = new List<string>();
            ISolicitacao iSolicitacao = Factory.GetInstance<ISolicitacao>();
            IList<ViverMais.Model.Laudo> laudos = iSolicitacao.BuscaLaudos<ViverMais.Model.Laudo>(solicitacao.Codigo);
            if (laudos.Count != 0)
            {
                Session["LaudosSolicitacao"] = laudos;
                Session["indexLaudo"] = 0;
                if (laudos[0].Imagem != null)
                {
                    BlobImage1.BlobData = laudos[0].Imagem;
                    BlobImage1.MimeType = "image/jpg";
                    BlobImage1.DataBind();
                    Image1.Visible = false;
                }
                else
                {
                    Image1.ImageUrl = "laudos/" + laudos[0].Endereco;
                    Image1.Visible = true;
                    Image1.DataBind();
                    BlobImage1.Visible = false;
                }
                if (laudos.Count == 1)
                    lknProximo.Enabled = false;
            }
            else
            {
                lknAnterior.Enabled = false;
                lknProximo.Enabled = false;
            }
        }

        protected void btnProximo_Click(object sender, EventArgs e)
        {
            lknAnterior.Enabled = true;
            IList<ViverMais.Model.Laudo> laudos = (IList<ViverMais.Model.Laudo>)Session["LaudosSolicitacao"];
            int indiceLaudo = int.Parse(Session["indexLaudo"].ToString());

            // Busca a Imagem Proxima, se tiver
            if (indiceLaudo <= laudos.Count - 1)
            {
                indiceLaudo++;
                if (laudos[indiceLaudo].Imagem != null)
                {
                    BlobImage1.BlobData = laudos[indiceLaudo].Imagem;
                    BlobImage1.MimeType = "image/jpg";
                    BlobImage1.DataBind();
                    Image1.Visible = false;
                }
                else
                {
                    Image1.ImageUrl = "laudos/" + laudos[indiceLaudo].Endereco;
                    Image1.Visible = true;
                    Image1.DataBind();
                    BlobImage1.Visible = false;
                }
            }
            if ((indiceLaudo + 1) == laudos.Count)
            {
                lknProximo.Enabled = false;
            }
            Session["indexLaudo"] = indiceLaudo;
            
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            // Busca a Imagem anterior
            lknProximo.Enabled = true;
            IList<ViverMais.Model.Laudo> laudos = (IList<ViverMais.Model.Laudo>)Session["LaudosSolicitacao"];
            int indiceLaudo = int.Parse(Session["indexLaudo"].ToString());

            // Busca a Imagem Anterior, se tiver
            if (indiceLaudo > 0)
            {
                indiceLaudo--;
                if (laudos[indiceLaudo].Imagem != null)
                {
                    BlobImage1.BlobData = laudos[indiceLaudo].Imagem;
                    BlobImage1.MimeType = "image/jpg";
                    BlobImage1.DataBind();
                    Image1.Visible = false;
                }
                else
                {
                    Image1.ImageUrl = "laudos/" + laudos[indiceLaudo].Endereco;
                    Image1.Visible = true;
                    Image1.DataBind();
                    BlobImage1.Visible = false;
                }
            }
            if (indiceLaudo == 0)
            {
                lknAnterior.Enabled = false;
            }
            Session["indexLaudo"] = indiceLaudo;
        }

        protected void SalvaProducaoMedicoRegulador(Solicitacao solicitacao)
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            if (usuario != null)
            {
                ProducaoMedicoRegulador producaoMedicoRegulador = new ProducaoMedicoRegulador();
                //Verifica se ele alterou a prioridade da Solicitacao ou a Situação da Solicitacao
                if (solicitacao.Prioridade != rbtPrioridade.SelectedValue || solicitacao.Situacao != rbtSituacao.SelectedValue)
                {
                    producaoMedicoRegulador.Solicitacao = solicitacao;
                    producaoMedicoRegulador.DataAtualizacao = DateTime.Now;
                    producaoMedicoRegulador.Usuario = usuario;
                    producaoMedicoRegulador.PrioridadeSolicitacao = char.Parse(rbtPrioridade.SelectedValue);
                    producaoMedicoRegulador.StatusSolicitacao = char.Parse(rbtSituacao.SelectedValue);
                    Factory.GetInstance<IViverMaisServiceFacade>().Salvar(producaoMedicoRegulador);
                }
            }
        }



        protected void GridViewAgenda_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Selecionar")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = row.RowIndex;

                int codigo_agenda = int.Parse(GridViewAgenda.Rows[index].Cells[10].Text);
                ViverMais.Model.Agenda agenda = Factory.GetInstance<IAmbulatorial>().BuscarPorCodigo<Agenda>(codigo_agenda);
                Session["Agenda"] = agenda;
                //int index = GridViewAgenda.SelectedIndex;
                if (Session["indexAgenda"] == null)
                    Session["indexAgenda"] = index;
                else
                {
                    int indexSession = int.Parse(Session["indexAgenda"].ToString());
                    GridViewAgenda.Rows[indexSession].BackColor = Color.FromArgb(238, 238, 238);
                }
                GridViewAgenda.Rows[index].BackColor = Color.FromArgb(169, 169, 169);
                Session["indexAgenda"] = index;
            }
        }

        protected void rbtnEspecialidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ///////////////////////////
            //Dados para Inclusão do CBO que verificará a disponibilidade
            //de Vagas
            ///////////////////////////

            IAmbulatorial iAmbulatorial = Factory.GetInstance<IAmbulatorial>();
            int id_solicitacao = int.Parse(ViewState["id_solicitacao"].ToString());
            Solicitacao solicitacao = iAmbulatorial.BuscarPorCodigo<Solicitacao>(id_solicitacao);
            ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
            Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
            if (endereco != null)
            {
                //ViverMais.Model.Procedimento procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(solicitacao.ID_Procedimento);
                if (endereco.Municipio.Codigo != "292740")
                {
                    if (solicitacao.Procedimento.FinanciamentoProcedimento.Codigo != FinanciamentoProcedimento.FAEC)
                    {
                        if (RestricaoPactoMunicipio(endereco.Municipio, solicitacao.Procedimento))
                        {
                            // Fazer abrangencia
                            if (Factory.GetInstance<ISolicitacao>().RestricaoPactoAbrangencia(endereco.Municipio.Codigo, solicitacao.Procedimento.Codigo, rbtnEspecialidade.SelectedValue))
                            {
                                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Não Existe Pacto com Saldo suficiente para o Municipio do Paciente: '" + endereco.Municipio.NomeSemUF.ToUpper() + ");", true);
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Não é possível realizar o Procedimento Selecionado. O Município " + endereco.Municipio.NomeSemUF.ToUpper().Replace("'", "") + " não possui saldo suficiente para realizar o procedimento.');", true);
                                rbtnEspecialidade.ClearSelection();
                                return;
                            }
                        }
                    }
                }


                ViverMais.Model.Parametros parametro = iAmbulatorial.ListarTodos<ViverMais.Model.Parametros>()[0];

                DateTime data_inicial = DateTime.Now.AddDays(parametro.Min_Dias);
                DateTime data_final = DateTime.Now.AddDays(parametro.Max_Dias);

                //Lista as Agendas Diponíveis para o Procedimento Solicitado
                //Carrega os SubGrupos Vinculados a Especialidade e Procedimento
                ddlSubGrupo.Items.Clear();
                ddlSubGrupo.Items.Add(new ListItem("Selecione...", "0"));
                IList<SubGrupo> subGrupos = Factory.GetInstance<ISubGrupoProcedimentoCbo>().ListarSubGrupoPorProcedimentoECbo<SubGrupo>(solicitacao.Procedimento.Codigo, rbtnEspecialidade.SelectedValue, true);
                foreach (SubGrupo subGrupo in subGrupos)
                    ddlSubGrupo.Items.Add(new ListItem(subGrupo.NomeSubGrupo, subGrupo.Codigo.ToString()));

                //IList<ViverMais.Model.Agenda> agendas = iAmbulatorial.BuscarAgendaProcedimento<ViverMais.Model.Agenda>(solicitacao.Procedimento.Codigo, rbtnEspecialidade.SelectedValue, data_inicial, data_final, int.Parse(ddlSubGrupo.SelectedValue)).Where(p => p.Quantidade > p.QuantidadeAgendada).ToList();

                CarregaAgendas(iAmbulatorial.BuscarAgendaProcedimento<ViverMais.Model.Agenda>(solicitacao.Procedimento.Codigo, rbtnEspecialidade.SelectedValue, data_inicial, data_final, int.Parse(ddlSubGrupo.SelectedValue)).Where(p => p.Quantidade > p.QuantidadeAgendada).ToList());
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Exite Pendência com o Endereço do Paciente. Por favor, regularize a situação e tente novamente!');", true);
                return;
            }
        }

        protected void ddlSubGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            IAmbulatorial iAmbulatorial = Factory.GetInstance<IAmbulatorial>();
            ViverMais.Model.Parametros parametro = iAmbulatorial.ListarTodos<ViverMais.Model.Parametros>()[0];
            DateTime data_inicial = DateTime.Now.AddDays(parametro.Min_Dias);
            DateTime data_final = DateTime.Now.AddDays(parametro.Max_Dias);

            int id_solicitacao = int.Parse(ViewState["id_solicitacao"].ToString());
            Solicitacao solicitacao = iAmbulatorial.BuscarPorCodigo<Solicitacao>(id_solicitacao);
            IList<ViverMais.Model.Agenda> agendas = iAmbulatorial.BuscarAgendaProcedimento<ViverMais.Model.Agenda>(solicitacao.Procedimento.Codigo, rbtnEspecialidade.SelectedValue, data_inicial, data_final, int.Parse(ddlSubGrupo.SelectedValue)).Where(p => p.Quantidade > p.QuantidadeAgendada).ToList();
            Session["Agendas"] = agendas;
            GridViewAgenda.DataSource = agendas;
            GridViewAgenda.DataBind();
        }

        private bool RestricaoPactoMunicipio(Municipio municipio, Procedimento procedimentoSelecionado)
        {
            ViverMais.Model.Pacto pacto = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<ViverMais.Model.Pacto>(municipio.Codigo);
            //float valorProcedimento = float.Parse(procedimentoSelecionado.VL_SA.ToString().Insert(procedimentoSelecionado.VL_SA.ToString().Length - 2, ","));
            if (pacto != null)
            {
                PactoAgregadoProcedCBO pactoAgregadoProced = null;
                if (pacto.PactosAgregados.Count != 0)
                {
                    //Busca os PactosAgregadosProcedimentosCBO primeiramente pelo CBO selecionado
                    if (pacto.PactosAgregados.Where(p => p.Cbos.Count != 0
                        && p.Cbos.Select(t => t.Codigo).ToList().Contains(rbtnEspecialidade.SelectedValue)
                        && p.Procedimento != null
                        && p.Procedimento.Codigo == procedimentoSelecionado.Codigo
                        && p.Ativo == true
                        && p.Ano == DateTime.Now.Year
                        && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO)).ToList().Count == 0)
                    {
                        //Irei Buscar Se existe alguma Pacto do Tipo PROCEDIMENTO Para o Procedimento Selecionado
                        if (pacto.PactosAgregados.Where(p => p.Procedimento != null
                            && p.Procedimento.Codigo == procedimentoSelecionado.Codigo
                            && p.Ativo == true
                            && p.Ano == DateTime.Now.Year
                            && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO)).ToList().Count == 0)
                        {
                            //Irei Buscar agora Pelo Agregado que tem o Procedimento Selecionado
                            Agregado agregado = Factory.GetInstance<IProcedimentoAgregado>().BuscaAgregadoPorProcedimento<Agregado>(procedimentoSelecionado.Codigo);
                            if (agregado != null)
                            {
                                if (pacto.PactosAgregados.Where(p => p.Agregado.Codigo == agregado.Codigo
                                    && p.Ativo == true
                                    && p.Ano == DateTime.Now.Year
                                    && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO)).ToList().Count == 0)
                                {
                                    return true;
                                }
                                else
                                {
                                    pactoAgregadoProced = pacto.PactosAgregados.Where(p => p.Agregado.Codigo == agregado.Codigo && p.Ativo == true && p.Ano == DateTime.Now.Year && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO)).ToList()[0];
                                }
                            }
                        }
                        else
                        {
                            pactoAgregadoProced = pacto.PactosAgregados.Where(p => p.Procedimento != null && p.Procedimento.Codigo == procedimentoSelecionado.Codigo && p.Ativo == true && p.Ano == DateTime.Now.Year && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO)).ToList()[0];
                        }
                    }
                    else
                    {
                        pactoAgregadoProced = pacto.PactosAgregados.Where(p => p.Cbos.Count != 0 && p.Cbos.Select(t => t.Codigo).ToList().Contains(rbtnEspecialidade.SelectedValue) && p.Procedimento != null && p.Ativo == true && p.Ano == DateTime.Now.Year && p.Procedimento.Codigo == procedimentoSelecionado.Codigo && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO)).ToList()[0];
                        //pactoAgregadoProced = pacto.PactosAgregados.Where(p => p.Cbos.Count != 0 && p.Cbos.Contains(Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<CBO>(rbtnEspecialidade.SelectedValue)) && p.Procedimento != null && p.Procedimento.Codigo == procedimentoSelecionado.Codigo && p.Ativo == true && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO)).ToList()[0];
                    }
                }
                if (pactoAgregadoProced != null)
                {
                    //Pega o Valor do Pacto Mensal e verifica se o saldo permite realizar o procedimento
                    IList<PactoReferenciaSaldo> pactosReferenciaSaldo = Factory.GetInstance<IPactoReferenciaSaldo>().BuscarPorPactoAgregado<PactoReferenciaSaldo>(pactoAgregadoProced.Codigo);
                    if (pactosReferenciaSaldo.Count != 0)
                    {
                        PactoReferenciaSaldo pactoReferenciaSaldoMesAtual = pactosReferenciaSaldo.Where(p => p.Mes == DateTime.Now.Month).ToList().FirstOrDefault();
                        if (pactoReferenciaSaldoMesAtual != null)
                        {
                            //Verifico se o saldo, somado com o procedimento selecionado será superior ao Teto mensal
                            if (pactoReferenciaSaldoMesAtual.ValorRestante < procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado + pactoReferenciaSaldoMesAtual.ValorRestante)
                            {
                                //Verifico se o Pacto está Bloqueado por cota
                                if (pactoReferenciaSaldoMesAtual.PactoAgregadoProcedCBO.BloqueiaCota == Convert.ToInt32(PactoAgregadoProcedCBO.BloqueadoPorCota.NAO))
                                {
                                    //Verifica o Percentual Adicional que poderá ultrapassar do pacto
                                    Decimal valorComPercentual = pactoAgregadoProced.ValorMensal + ((pactoAgregadoProced.ValorMensal * pactoReferenciaSaldoMesAtual.PactoAgregadoProcedCBO.Percentual) / 100);
                                    if (procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado + pactoReferenciaSaldoMesAtual.ValorRestante > valorComPercentual)
                                    {
                                        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Cota Excedida para o município " + pacto.Municipio.Nome + "!');", true);
                                        //rbtnEspecialidade.ClearSelection();
                                        return true;
                                    }
                                    else
                                    {
                                        Session["pactoAgregadoProcedCBO"] = pactoAgregadoProced;
                                    }
                                }
                                else // Se está Bloqueado Por Cota e não tem Saldo, irá alertar ao Usuário
                                {
                                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Cota Excedida para o município " + pacto.Municipio.Nome + "!');", true);
                                    //rbtnEspecialidade.ClearSelection();
                                    return true;
                                }
                            }
                            else
                            {
                                Session["pactoReferenciaSaldo"] = pactoReferenciaSaldoMesAtual;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        void TransfereSaldoPactoMesAnteriorParaMesAtual(PactoReferenciaSaldo mesAnterior, PactoReferenciaSaldo mesAtual)
        {
            mesAtual.ValorRestante += mesAnterior.ValorRestante;
            mesAnterior.TranferiuPactoMesSeguinte = Convert.ToInt32(PactoReferenciaSaldo.StatusTranferiuPactoMesSegunte.TRANSFERIU);
            mesAnterior.ValorRestante = 0;
            Factory.GetInstance<IPactoReferenciaSaldo>().Salvar(mesAtual);
            Factory.GetInstance<IPactoReferenciaSaldo>().Salvar(mesAnterior);
        }
    }
}
