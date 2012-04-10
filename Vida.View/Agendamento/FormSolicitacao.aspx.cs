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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Drawing;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using System.Web.UI.MobileControls;
using ViverMais.ServiceFacade.ServiceFacades.Agregado;
using ViverMais.View.Agendamento.Helpers;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using System.IO;
using System.Text;
using ViverMais.BLL;
using ViverMais.ServiceFacade.ServiceFacades.Misc;
using System.Globalization;
using ViverMais.View.Helpers;


namespace ViverMais.View.Agendamento
{
    public partial class FormSolicitacao : System.Web.UI.Page
    {
        //protected void OnClick_btnFechar(object sender, EventArgs e)
        //{
        //    PanelMensagem.Visible = false;
        //    Session["LeuMsgSolicitacao"] = true;
        //}
        //protected void LinkButton2_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "javascript:GB_showFullScreen('Anexar Laudos','../FormUploadLaudos.aspx');", true);
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            WUCPesquisarPaciente1.GridView.SelectedIndexChanging += new GridViewSelectEventHandler(GridView_SelectedIndexChanged);
            if (!IsPostBack)
            {
                //Carrega os Dados Para Busca do CID                
                ddlGrupoCID.Items.Clear();
                IList<string> codcids = Factory.GetInstance<ICid>().ListarGrupos();
                ddlGrupoCID.Items.Add(new ListItem("Selecione...", "0"));
                foreach (string letra in codcids)
                    ddlGrupoCID.Items.Add(new ListItem(letra, letra));

                //Session.Remove("pacienteSelecionado");
                Session.Remove("ProfissionalSelecionado");
                Session.Remove("Laudos");
                Session.Remove("pactoAgregadoProcedCBO");
                Session.Remove("indexAgenda");
                Session.Remove("pactoReferenciaSaldo");
                //if (Session["LeuMsgHomePage"] != null)
                //    PanelMensagem.Visible = false;
                //else
                //    PanelMensagem.Visible = true;
                Usuario usuario = (Usuario)Session["Usuario"];
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "MOVIMENTACAO_SOLICITACAO_AMBULATORIAL", Modulo.AGENDAMENTO))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);

                Wizard1.ActiveStepIndex = 0;
                //Seto os Tipos de Procedimentos que a unidade pode Agendar. Se Regulados, Autorizados, Atendimento Básico, Agendado
                IList<ParametroTipoSolicitacaoEstabelecimento> parametros = Factory.GetInstance<ITipoSolicitacaoEstabelecimento>().BuscaPorEstabelecimento<ParametroTipoSolicitacaoEstabelecimento>(usuario.Unidade.CNES);
                if (parametros.Count != 0)
                {
                    for (int i = 0; i < parametros.Count; i++)
                    {
                        switch (parametros[i].TipoSolicitacao)
                        {
                            case "1":
                                rbtnTipoProcedimento.Items.FindByValue("1").Enabled = true;
                                break;
                            case "2":
                                rbtnTipoProcedimento.Items.FindByValue("2").Enabled = true;
                                break;
                            case "3":
                                rbtnTipoProcedimento.Items.FindByValue("3").Enabled = true;
                                break;
                            case "4":
                                rbtnTipoProcedimento.Items.FindByValue("4").Enabled = true;
                                break;
                        }
                    }
                }
            }

            //Essa opção é executada quando a solicitação é Salva e ele irá direcionar para a página de impressão Adequada
            if ((Session["id_solicitacao"] != null) && (Session["tipo_procedimento"] != null))
            {
                int tipoProcedimento = int.Parse(Session["tipo_procedimento"].ToString());
                int id_Solicitacao = int.Parse(Session["id_solicitacao"].ToString());

                //Regulado e Autorizado, Direciona para Impressão Autorização
                //Agendado e Atendimento Básico, Direciona para Impressão do Protocolo
                if ((tipoProcedimento == 1) || (tipoProcedimento == 2))//Se For Regulado ou Autorizado
                    Redirector.Redirect("ImpressaoProtocolo.aspx?id_solicitacao=" + id_Solicitacao, "_blank", "");
                else if ((tipoProcedimento == 3) || (tipoProcedimento == 4))//Se For Agendado ou Atendimento Básico
                    Redirector.Redirect("RelatorioSolicitacao.aspx?id_solicitacao=" + id_Solicitacao, "_blank", "");

                //Limpa a Sessão para guardar os dados da nova solicitacao
                Session.Remove("id_solicitacao");
                Session.Remove("tipo_procedimento");
                PanelConfirmaNovaSolicitacao.Visible = true;
            }


        }

        /// <summary>
        /// Confirma que o usuário quer solicitar um novo procedimento para o mesmo paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSim_Click(object sender, EventArgs e)
        {
            ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];
            Endereco endereco = EnderecoBLL.PesquisarPorPaciente(paciente);
            PreencheDadosPaciente(paciente, endereco);
            PanelExibePaciente.Visible = true;
            GeraNovaSolicitacaoParaPaciente(paciente, endereco);
        }

        /// <summary>
        /// Nega que o usuário quer solicitar um novo procedimento para o mesmo paciente e prossegue com os parametros normais
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNao_Click(object sender, EventArgs e)
        {
            PanelConfirmaNovaSolicitacao.Visible = false;
            Session.Remove("pacienteSelecionado");
        }

        /// <summary>
        /// Fecha a Panel de Confirmação de nova solicitação para o mesmo paciente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFechar_Click(object sender, EventArgs e)
        {
            PanelConfirmaNovaSolicitacao.Visible = false;
            Session.Remove("pacienteSelecionado");
        }

        void GeraNovaSolicitacaoParaPaciente(ViverMais.Model.Paciente paciente, Endereco endereco)
        {
            PanelConfirmaNovaSolicitacao.Visible = false;
            PreencheDadosPaciente(paciente, endereco);
            Wizard1.ActiveStepIndex = 1;
        }

        protected void tbxTelefoneContato_OnTextChanged(object sender, EventArgs e)
        {
            if ((tbxTelefoneContato.Text == "00000000") || (tbxTelefoneContato.Text == "11111111") || (tbxTelefoneContato.Text == "22222222") || (tbxTelefoneContato.Text == "33333333") || (tbxTelefoneContato.Text == "44444444")
                || (tbxTelefoneContato.Text == "55555555") || (tbxTelefoneContato.Text == "66666666") || (tbxTelefoneContato.Text == "77777777") || (tbxTelefoneContato.Text == "88888888") || (tbxTelefoneContato.Text == "99999999")
                || (tbxTelefoneContato.Text == "12345678") || (tbxTelefoneContato.Text.Length < 8))
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Telefone Inválido.');", true);
                return;
            }

        }

        protected void GridViewAgenda_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (Session["AgendasFiltradas"] == null)
                GridViewAgenda.DataSource = Session["Agendas"];
            else
                GridViewAgenda.DataSource = Session["AgendasFiltradas"];
            GridViewAgenda.PageIndex = e.NewPageIndex;
            GridViewAgenda.DataBind();
        }

        void PreencheDadosPaciente(ViverMais.Model.Paciente paciente, Endereco endereco)
        {
            lblNome.Text = paciente.Nome;
            lblNomeMae.Text = paciente.NomeMae;
            lblSexo.Text = paciente.Sexo == 'M' ? "Masculino" : "Feminino";
            lblRacaCor.Text = paciente.RacaCor.Descricao;
            lblDataNascimento.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
            try
            {
                PanelTelefoneContato.Visible = true;
                tbxDDD.Text = endereco.DDD;
                tbxTelefoneContato.Text = endereco.Telefone;
                tbxEmail.Text = paciente.Email;
                lblTelefone.Text = endereco.Telefone;
                lblEmail.Text = paciente.Email;
            }
            catch (NullReferenceException)
            {
                lblTelefone.Text = string.Empty;
                lblEmail.Text = string.Empty;
            }
            lblMunicipio.Text = MunicipioBLL.PesquisarPorCodigo(endereco.Municipio.Codigo).Nome;

            lblPaciente.Text = paciente.Nome;
            lblDataNascimentoPaciente.Text = paciente.DataNascimento.ToString("dd/MM/yyyy");
        }

        protected void GridView_SelectedIndexChanged(object sender, GridViewSelectEventArgs e)
        {
            ViverMais.Model.Paciente paciente = WUCPesquisarPaciente1.Paciente;
            Endereco endereco = EnderecoBLL.PesquisarPorPaciente(paciente);
            ViverMais.Model.Usuario usuario = (Usuario)Session["Usuario"];
            //Se o Municipio do Usuário do Sistema for diferente do Paciente ele não marca
            //Exceto se o usuário for de Salvador
            if (endereco != null)
            {
                if ((endereco.Municipio.Codigo) != (usuario.Unidade.MunicipioGestor.Codigo) && (usuario.Unidade.MunicipioGestor.Codigo != Municipio.SALVADOR))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Você não pode realizar solicitação para esse paciente, pois não pertence ao seu Município.');", true);
                    return;
                }
                Session["pacienteSelecionado"] = paciente;
                PreencheDadosPaciente(paciente, endereco);
                PanelExibePaciente.Visible = true;
                if (ddlProcedimento.Items.Count != 0)
                {
                    ddlProcedimento.Items.Clear();
                    ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
                    if (rbtnEspecialidade.Items.Count != 0)
                        rbtnEspecialidade.Items.Clear();
                }
                UpdatePanel1.Update();
                rbtnTipoProcedimento.ClearSelection();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O Paciente selecionado está sem endereço. Por favor, verifique seu cadastro!');", true);
                return;
            }
        }

        private bool RestricaoSexo(string sexoPaciente, string sexoProcedimento)
        {
            if (sexoPaciente == sexoProcedimento || sexoProcedimento == "I" || sexoProcedimento == "N")
            {
                return false;
            }
            return true;
        }

        private bool RestricaoIdade(int idadeMinimaProcedimento, int idadeMaximaProcedimento, DateTime dataNascimentoPaciente)
        {
            int idade_minima = 0;
            int idade_maxima = 0;
            int idade = (int)(((DateTime.Today - dataNascimentoPaciente).Days - 1) / 365.25);
            if (idadeMinimaProcedimento != 9999)
            {
                idade_minima = idadeMinimaProcedimento / 12;
            }
            if (idadeMaximaProcedimento != 9999)
            {
                idade_maxima = idadeMaximaProcedimento / 12;
            }
            if (idade >= idade_minima && idade <= idade_maxima)
                return false;
            else
                return true;
        }

        void IdentificaEColoreAsEspecialidadesQuePossuemVaga()
        {
            for (int i = 0; i < rbtnEspecialidade.Items.Count; i++)
            {
                if (rbtnEspecialidade.Items[i].Enabled == true)
                {
                    rbtnEspecialidade.Items[i].Attributes.CssStyle.Add("color", "#ffffff");
                    rbtnEspecialidade.Items[i].Attributes.CssStyle.Add("font-weight", "bold");
                    rbtnEspecialidade.Items[i].Attributes.CssStyle.Add("background-color", "#119944");
                    rbtnEspecialidade.Items[i].Attributes.CssStyle.Add("padding-right", "6px");
                }
            }
        }

        protected void ddlProcedimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProcedimento.SelectedValue != "0")
            {
                IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
                IProcedimentoAgregado iProcedimentoAgregado = Factory.GetInstance<IProcedimentoAgregado>();
                IPacto iPacto = Factory.GetInstance<IPacto>();
                IAmbulatorial iAmbulatorial = Factory.GetInstance<IAmbulatorial>();
                ViverMais.Model.Procedimento procedimento = iViverMais.BuscarPorCodigo<ViverMais.Model.Procedimento>(ddlProcedimento.SelectedValue);
                ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];

                if (procedimento.Codigo != Procedimento.CONSULTA_MEDICA_ATENCAO_BASICA && procedimento.Codigo != Procedimento.CONSULTA_MEDICA_ATENCAO_ESPECIALIZADA)
                {
                    IList<ViverMais.Model.Solicitacao> VerificaSolicitacao = Factory.GetInstance<ISolicitacao>().VerificaSolicitacao<ViverMais.Model.Solicitacao>(paciente.Codigo, ddlProcedimento.SelectedValue, rbtnTipoProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));
                    if (VerificaSolicitacao.Count >= procedimento.QtdMaximaExecucao)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento já solicitado para esse paciente!');", true);
                        ddlProcedimento.SelectedValue = "0";
                        rbtnEspecialidade.ClearSelection();
                        return;
                    }
                }

                if (procedimento.FinanciamentoProcedimento.Codigo != FinanciamentoProcedimento.FAEC)
                {
                    //Verifica se o Município do paciente possui Pacto, Exceto Salvador
                    Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
                    if (endereco.Municipio.Codigo != Municipio.SALVADOR)//Codigo de Salvador
                    {
                        if (rbtnTipoProcedimento.SelectedValue != Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.AGENDADO).ToString())//Verifico se é diferente de Agendado para poder fazer a crítica de Pacto quando o usuário selecionar o CBO
                        {
                            Pacto pacto = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(endereco.Municipio.Codigo);
                            if (pacto == null)
                            {
                                if (Factory.GetInstance<ISolicitacao>().RestricaoPactoAbrangencia(endereco.Municipio.Codigo, ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue))
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Não é possível realizar o Procedimento Selecionado. O Município " + endereco.Municipio.NomeSemUF.ToUpper() + " não possui Pacto para o Procedimento Selecionado.');", true);
                                    ddlProcedimento.SelectedValue = "0";
                                    return;
                                }
                            }
                            else if (RestricaoPactoMunicipio(endereco.Municipio, pacto, procedimento))
                            {
                                if (Factory.GetInstance<ISolicitacao>().RestricaoPactoAbrangencia(endereco.Municipio.Codigo, procedimento.Codigo, rbtnEspecialidade.SelectedValue))
                                {
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não é possível realizar o Procedimento Selecionado. O Município do Paciente não possui Pacto ou saldo suficiente para realizar o Procedimento!');", true);
                                    ddlProcedimento.SelectedValue = "0";
                                    return;
                                }
                            }
                        }
                    }
                }

                // Verifica se o procedimento pode ser realizado para o sexo do paciente
                if (!RestricaoSexo(paciente.Sexo.ToString(), procedimento.RestricaoSexo))
                {
                    //Verifica se a idade do paciente pode realizar o procedimento
                    if (!RestricaoIdade(procedimento.IdadeMinima, procedimento.IdadeMaxima, paciente.DataNascimento))
                    {
                        rbtnEspecialidade.Items.Clear();

                        //Verifica se o Procedimento é Agendado ou Atendimento Básico para poder mostrar os CBOs
                        if (rbtnTipoProcedimento.SelectedValue == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.AGENDADO).ToString() || rbtnTipoProcedimento.SelectedValue == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.ATENDIMENTOBASICO).ToString())
                        {
                            RequiredFieldValidator2.Enabled = true;//Habilita a Validação da seleção da Especialidade
                            PanelEspecialidade.Visible = true;

                            // Busca os CBOs do Procedimento Selecionado
                            IList<ViverMais.Model.CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<ViverMais.Model.CBO>(ddlProcedimento.SelectedValue);

                            //Verifico Os parametros da Unidade para limitar a Busca de Agendas de Acordo a Minimo e Máximos de dias definidos nos Parametros
                            Parametros parametro = Factory.GetInstance<IAgendamentoServiceFacade>().ListarTodos<ViverMais.Model.Parametros>().FirstOrDefault();
                            if (parametro != null)
                            {
                                //Carrega todas as especialidades
                                foreach (CBO cbo in cbos)
                                    if (rbtnEspecialidade.Items.FindByValue(cbo.Codigo) == null)
                                        rbtnEspecialidade.Items.Add(new ListItem(cbo.Nome, cbo.Codigo.ToString()));
                            }
                        }
                        else
                        {
                            RequiredFieldValidator2.Enabled = false;//Desabilita a Validação da seleção da Especialidade
                            PanelEspecialidade.Visible = false;
                        }
                    }
                    else
                    {
                        ddlProcedimento.SelectedValue = "0";
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Idade do Paciente não permite Agendamento para este Procedimento!');", true);
                        return;
                    }
                }
                else
                {
                    ddlProcedimento.SelectedValue = "0";
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Procedimento não realizado para esse sexo!');", true);
                    return;
                }
            }
            else
            {
                rbtnEspecialidade.Items.Clear();
            }
        }

        protected void OnClick_BuscarCID(object sender, EventArgs e)
        {
            ddlCID.Items.Clear();
            ddlCID.Items.Add(new ListItem("Selecione...", "0"));
            tbxCID.Text = tbxCID.Text.ToUpper();
            Cid cid = Factory.GetInstance<ICid>().BuscarPorCodigo<Cid>(tbxCID.Text);

            if (cid != null)
            {
                ListItem item = new ListItem(cid.Codigo + " - " + cid.Nome, cid.Codigo.ToString());
                ddlCID.Items.Add(item);
            }
            ddlCID.Focus();
        }

        private bool RestricaoPactoMunicipio(Municipio municipio, Pacto pacto, Procedimento procedimentoSelecionado)
        {
            PactoAgregadoProcedCBO pactoAgregadoProced = null;
            if (pacto.PactosAgregados.Count != 0)
            {
                //Busca os PactosAgregadosProcedimentosCBO primeiramente pelo CBO selecionado
                if (pacto.PactosAgregados.Where(p => p.Cbos.Count != 0
                    && p.Cbos.Select(t => t.Codigo).ToList().Contains(rbtnEspecialidade.SelectedValue)
                    && p.Procedimento != null
                    && p.Procedimento.Codigo == ddlProcedimento.SelectedValue
                    && p.Ativo == true
                    && p.Ano == DateTime.Now.Year
                    && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO)).ToList().Count == 0)
                {
                    //Irei Buscar Se existe alguma Pacto do Tipo PROCEDIMENTO Para o Procedimento Selecionado
                    if (pacto.PactosAgregados.Where(p => p.Procedimento != null
                        && p.Procedimento.Codigo == ddlProcedimento.SelectedValue
                        && p.Ativo == true
                        && p.Ano == DateTime.Now.Year
                        && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO)).ToList().Count == 0)
                    {
                        //Irei Buscar agora Pelo Agregado que tem o Procedimento Selecionado
                        Agregado agregado = Factory.GetInstance<IProcedimentoAgregado>().BuscaAgregadoPorProcedimento<Agregado>(ddlProcedimento.SelectedValue);
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
                                pactoAgregadoProced = pacto.PactosAgregados.Where(p => p.Agregado.Codigo == agregado.Codigo && p.Ano == DateTime.Now.Year && p.Ativo == true && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO)).ToList()[0];
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        pactoAgregadoProced = pacto.PactosAgregados.Where(p => p.Procedimento != null && p.Procedimento.Codigo == ddlProcedimento.SelectedValue && p.Ativo == true && p.Ano == DateTime.Now.Year && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO)).ToList()[0];
                    }
                }
                else
                {
                    pactoAgregadoProced = pacto.PactosAgregados.Where(p => p.Cbos.Count != 0 && p.Cbos.Select(t => t.Codigo).ToList().Contains(rbtnEspecialidade.SelectedValue) && p.Procedimento != null && p.Ativo == true && p.Ano == DateTime.Now.Year && p.Procedimento.Codigo == ddlProcedimento.SelectedValue && p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO)).ToList()[0];
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
                        if (pactoReferenciaSaldoMesAtual.ValorRestante < procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado)
                        {
                            //Verifico se o Pacto está Bloqueado por cota
                            if (pactoReferenciaSaldoMesAtual.PactoAgregadoProcedCBO.BloqueiaCota == Convert.ToInt32(PactoAgregadoProcedCBO.BloqueadoPorCota.NAO))
                            {
                                //Verifica o Percentual Adicional que poderá ultrapassar do pacto
                                Decimal valorComPercentual = pactoAgregadoProced.ValorMensal + ((pactoAgregadoProced.ValorMensal * pactoReferenciaSaldoMesAtual.PactoAgregadoProcedCBO.Percentual) / 100);
                                if (procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado + Decimal.Parse(pactoReferenciaSaldoMesAtual.ValorRestante.ToString()) > valorComPercentual)
                                {
                                    return true;
                                }
                                else
                                {
                                    Session["pactoAgregadoProcedCBO"] = pactoAgregadoProced;
                                    return false;
                                }
                            }
                            else // Se está Bloqueado Por Cota e não tem Saldo, irá alertar ao Usuário
                            {
                                return true;
                            }
                        }
                        else
                        {
                            Session["pactoAgregadoProcedCBO"] = pactoAgregadoProced;
                            Session["pactoReferenciaSaldo"] = pactoReferenciaSaldoMesAtual;
                            return false;
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

        //protected bool RestricaoPactoAbrangencia(Municipio municipio, Procedimento procedimentoSelecionado)
        //{
        //    //Retorna os Grupos de Abrangencia que o Municipio Faz Parte
        //    IList<GrupoAbrangencia> grupos = Factory.GetInstance<IGrupoAbrangencia>().ListarGrupoPorMunicipio<GrupoAbrangencia>(municipio.Codigo);

        //    //Percorre Todos Os Grupos
        //    foreach (GrupoAbrangencia grupo in grupos)
        //    {
        //        //Lista Os PactosAbrangencias desse Grupo
        //        IList<PactoAbrangencia> pactosAbrangencia = Factory.GetInstance<IPactoAbrangencia>().BuscarPactoAbrangenciaPorGrupoAbrangencia<PactoAbrangencia>(grupo.Codigo);
        //        foreach (PactoAbrangencia pactoAbrangencia in pactosAbrangencia)
        //        {
        //            PactoAbrangenciaAgregado pactoAbrangenciaAgregado = null;
        //            if (pactoAbrangencia.PactoAbrangenciaAgregado.Count != 0)
        //            {
        //            //    pacto.PactosAgregados.Where(p => p.Cbos.Count != 0
        //            //&& p.Cbos.Select(t => t.Codigo).ToList().Contains(rbtnEspecialidade.SelectedValue)
        //            //&& p.Procedimento != null
        //            //&& p.Procedimento.Codigo == ddlProcedimento.SelectedValue
        //            //&& p.Ativo == true
        //            //&& p.Ano == DateTime.Now.Year
        //            //&& p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO)).ToList().Count == 0)

        //                if (pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.cbAgregado.Codigo == agregado.Codigo
        //                        && p.Ativo == true
        //                        && p.Ano == DateTime.Now.Year
        //                        && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.AGREGADO)).ToList().Count != 0)
        //                    {

        //                //Irei Buscar agora Pelo Agregado que tem o Procedimento Selecionado
        //                Agregado agregado = Factory.GetInstance<IProcedimentoAgregado>().BuscaAgregadoPorProcedimento<Agregado>(procedimentoSelecionado.Codigo);
        //                if (agregado != null)
        //                {
        //                    if (pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Agregado.Codigo == agregado.Codigo
        //                        && p.Ativo == true
        //                        && p.Ano == DateTime.Now.Year
        //                        && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.AGREGADO)).ToList().Count != 0)
        //                    {
        //                        pactoAbrangenciaAgregado = pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Agregado.Codigo == agregado.Codigo
        //                        && p.Ativo == true
        //                        && p.Ano == DateTime.Now.Year
        //                        && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.AGREGADO)).ToList().FirstOrDefault();

        //                        //PactoAbrangenciaGrupoMunicipio pactoAbrangenciaGrupoMunicipio = Factory.GetInstance<IPactoAbrangenciaAgregado>().ListarPactoAbrangenciaGrupoMunicipioPorPactoAbrangenciaAgregado<PactoAbrangenciaGrupoMunicipio>(pactoAbrangenciaAgregado.Codigo).Where(p => p.Municipio.Codigo == municipio.Codigo).FirstOrDefault();

        //                        if (pactoAbrangenciaAgregado.ValorUtilizado + procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado <= pactoAbrangenciaAgregado.ValorPactuado)
        //                        {
        //                            Session["PactoAbrangencia"] = pactoAbrangenciaAgregado;
        //                            return false;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}


        protected void OnSelectedIndexChanged_BuscarCids(object sender, EventArgs e)
        {
            ddlCID.Items.Clear();
            ddlCID.Items.Add(new ListItem("Selecione...", "0"));

            IList<Cid> listaCID = Factory.GetInstance<ICid>().BuscarPorGrupo<Cid>(ddlGrupoCID.SelectedValue.ToString());
            foreach (Cid cid in listaCID)
            {
                ListItem item = new ListItem(cid.Codigo + " - " + cid.Nome, cid.Codigo.ToString());
                ddlCID.Items.Add(item);
            }

            ddlCID.Focus();
        }

        void TransfereSaldoPactoMesAnteriorParaMesAtual(PactoReferenciaSaldo mesAnterior, PactoReferenciaSaldo mesAtual)
        {
            mesAtual.ValorRestante += mesAnterior.ValorRestante;
            mesAnterior.TranferiuPactoMesSeguinte = Convert.ToInt32(PactoReferenciaSaldo.StatusTranferiuPactoMesSegunte.TRANSFERIU);
            Factory.GetInstance<IPactoReferenciaSaldo>().Salvar(mesAtual);
            Factory.GetInstance<IPactoReferenciaSaldo>().Salvar(mesAnterior);
        }

        protected void rbtnEspecialidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            //HelperCheckerTimeOfExecution.DefinirPontoInicial();
            ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];
            Procedimento procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(ddlProcedimento.SelectedValue);
            // Verifica se o paciente já possui uma solicitação para o procedimento selecionado e está Pendente
            //IList<ViverMais.Model.Solicitacao> VerificaSolicitacao = Factory.GetInstance<ISolicitacao>().VerificaSolicitacao<ViverMais.Model.Solicitacao>(paciente.Codigo, ddlProcedimento.SelectedValue, rbtnTipoProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue);
            //if (procedimento.Codigo == Procedimento.CONSULTA_MEDICA_ATENCAO_BASICA || procedimento.Codigo == Procedimento.CONSULTA_MEDICA_ATENCAO_ESPECIALIZADA)
            //{
            //    if (VerificaSolicitacao.Count > 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Paciente já agendado para esse procedimento!');", true);
            //        rbtnEspecialidade.ClearSelection();
            //        return;
            //    }
            //}

            if (procedimento.FinanciamentoProcedimento.Codigo != FinanciamentoProcedimento.FAEC)
            {
                #region Restrição de Pacto
                //Verifica se o Município do paciente possui Pacto, Exceto Salvador
                Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
                Procedimento procedimentoSelecionado = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(ddlProcedimento.SelectedValue);
                if (endereco != null)
                {
                    if (endereco.Municipio.Codigo != Municipio.SALVADOR)//Codigo de Salvador
                    {
                        Pacto pacto = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(endereco.Municipio.Codigo);
                        if (pacto == null)
                        {
                            if (Factory.GetInstance<ISolicitacao>().RestricaoPactoAbrangencia(endereco.Municipio.Codigo, procedimentoSelecionado.Codigo, rbtnEspecialidade.SelectedValue))
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Não Existe Pacto ou Saldo suficiente para o Municipio do Paciente!');", true);
                                rbtnEspecialidade.ClearSelection();
                                return;
                            }
                        }
                        else if (RestricaoPactoMunicipio(endereco.Municipio, pacto, procedimentoSelecionado))
                        {
                            if (Factory.GetInstance<ISolicitacao>().RestricaoPactoAbrangencia(endereco.Municipio.Codigo, procedimentoSelecionado.Codigo, rbtnEspecialidade.SelectedValue))
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Não Existe Pacto ou Saldo suficiente para o Municipio do Paciente!');", true);
                                rbtnEspecialidade.ClearSelection();
                                return;
                            }
                        }
                    }
                }
                #endregion
            }

            //Carrega os SubGrupos Vinculados a Especialidade e Procedimento
            ddlSubGrupo.Items.Clear();
            ddlSubGrupo.Items.Add(new ListItem("Selecione...", "0"));
            IList<SubGrupo> subGrupos = Factory.GetInstance<ISubGrupoProcedimentoCbo>().ListarSubGrupoPorProcedimentoECbo<SubGrupo>(ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, true);
            foreach (SubGrupo subGrupo in subGrupos)
                ddlSubGrupo.Items.Add(new ListItem(subGrupo.NomeSubGrupo, subGrupo.Codigo.ToString()));
        }

        protected void GridViewAgenda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UpdatePanel2.Update();
            //UpdateSelecaoProfissional.Update();
            //UpdatePanelProcedimento.Update();
            ddlProcedimento.Items.Clear();
            ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            //Caso A solicitação seja para Agendamento Básico, Só será permitido para Pacientes do Município de Salvador
            if (rbtnTipoProcedimento.SelectedValue == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.ATENDIMENTOBASICO).ToString())
            {
                ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];
                if (paciente != null)
                {
                    Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
                    if (endereco != null)
                    {
                        if (endereco.Municipio.Codigo != Municipio.SALVADOR)//Código Referente ao município de Salvador
                        {
                            rbtnTipoProcedimento.ClearSelection();
                            PanelSelecaoProfissional.Visible = false;

                            PanelCID.Visible = false;
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Este Tipo de Solicitação só poderá ser realizada para Pacientes que residam em Salvador!');", true);
                            return;
                        }
                    }
                }
            }

            if (rbtnTipoProcedimento.SelectedValue == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.AGENDADO).ToString())
                //Desabilita a Obrigatoriedade da Seleção do CID
                RequiredFieldValidator4.Enabled = false;
            else
                RequiredFieldValidator4.Enabled = true;
            //ddlProcedimento.Items.Clear();
            //ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
            rbtnEspecialidade.Items.Clear();
            IList<ViverMais.Model.TipoProcedimento> tipoprocedimentos = Factory.GetInstance<ITipoProcedimento>().ListarProcedimentosPorTipo<ViverMais.Model.TipoProcedimento>(rbtnTipoProcedimento.SelectedValue);
            if (tipoprocedimentos.Count != 0)
            {
                IList<Procedimento> procedimentos = new List<Procedimento>();
                IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
                foreach (ViverMais.Model.TipoProcedimento tipoprocedimento in tipoprocedimentos)
                {
                    ViverMais.Model.Procedimento procedimentoTipo = iViverMais.BuscarPorCodigo<ViverMais.Model.Procedimento>(tipoprocedimento.Procedimento);
                    procedimentos.Add(procedimentoTipo);
                }
                procedimentos = procedimentos.Distinct().OrderBy(p => p.Nome).ToList();
                foreach (Procedimento procedimento in procedimentos)
                    ddlProcedimento.Items.Add(new ListItem(procedimento.Codigo.ToString() + " - " + procedimento.Nome, procedimento.Codigo.ToString()));
            }



            //Agendamento Básico n precisa do Profissional Solicitante
            if (rbtnTipoProcedimento.SelectedValue != Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.ATENDIMENTOBASICO).ToString())
            {
                //Habilita a Busca do Profissional Solicitante
                PanelSelecaoProfissional.Visible = true;
                PanelCID.Visible = true;
                //UpdatePanelEspecialidade.Update();
            }
            else
            {
                PanelSelecaoProfissional.Visible = false;
                //Desabilita a Busca do CID
                PanelCID.Visible = false;
            }
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

            if (Wizard1.ActiveStepIndex == 2)//Se for Finalizado na 2(Autorizado Ou Atendimento Básico)
            {
                //Verifica se foi selecionado algum item Na gridView
                if (Session["indexAgenda"] == null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione uma Agenda');", true);
                    //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Selecione uma Agenda!')</script>");
                    Wizard1.ActiveStepIndex = 2;
                    return;
                }

                //ViverMais.Model.Paciente paciente1 = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];

                #region Salva Solicitação

                int indexAgenda = int.Parse(Session["indexAgenda"].ToString());
                //Salva a solicitação                    
                int codigo_agenda = int.Parse(GridViewAgenda.Rows[indexAgenda].Cells[10].Text);
                IProcedimentoAgregado iProcedimentoAgregado = Factory.GetInstance<IProcedimentoAgregado>();
                ViverMais.Model.Usuario usuario = (Usuario)Session["Usuario"];
                ViverMais.Model.Solicitacao solicitacao = new Solicitacao();
                ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];
                Procedimento procedimentoSelecionado = iViverMais.BuscarPorCodigo<Procedimento>(ddlProcedimento.SelectedValue);
                Endereco endereco = EnderecoBLL.PesquisarPorPaciente(paciente);
                solicitacao.ID_Paciente = paciente.Codigo;

                ////Trecho responsável por recarregar os objetos que não possuem código(chave primária)
                //if (paciente.MunicipioNascimento != null)
                //{
                //    paciente.MunicipioNascimento = iViverMais.BuscarPorCodigo<Municipio>(paciente.MunicipioNascimento.Codigo);
                //    if (paciente.MunicipioNascimento.UF != null)
                //        paciente.MunicipioNascimento.UF = iViverMais.BuscarPorCodigo<UF>(paciente.MunicipioNascimento.UF.Codigo);
                //}
                //if (paciente.MunicipioResidencia != null)
                //{
                //    paciente.MunicipioResidencia = iViverMais.BuscarPorCodigo<Municipio>(paciente.MunicipioResidencia.Codigo);
                //    if (paciente.MunicipioResidencia.UF != null)
                //        paciente.MunicipioResidencia.UF = iViverMais.BuscarPorCodigo<UF>(paciente.MunicipioResidencia.UF.Codigo);
                //}

                solicitacao.Data_Solicitacao = DateTime.Now;

                char tipoCota = char.Parse(Session["Tipo_Cota_Utilizada"].ToString());
                if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.REDE))
                    solicitacao.TipoCotaUtilizada = Convert.ToChar(Solicitacao.TipoCota.REDE);
                else if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.LOCAL))
                    solicitacao.TipoCotaUtilizada = Convert.ToChar(Solicitacao.TipoCota.LOCAL);
                else if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.DISTRITAL))
                    solicitacao.TipoCotaUtilizada = Convert.ToChar(Solicitacao.TipoCota.DISTRITAL);
                else if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.RESERVA_TECNICA))
                    solicitacao.TipoCotaUtilizada = Convert.ToChar(Solicitacao.TipoCota.RESERVA_TECNICA);

                ViverMais.Model.Agenda agenda = iViverMais.BuscarPorCodigo<ViverMais.Model.Agenda>(codigo_agenda);
                //solicitacao.Identificador = Factory.GetInstance<ISolicitacao>().GeraIdentificador<Agenda>(rbtnTipoProcedimento.SelectedValue, agenda);
                //solicitacao.Identificador = Identificador(rbtnTipoProcedimento.SelectedValue);
                solicitacao.EasSolicitante = usuario.Unidade.CNES;
                solicitacao.Prioridade = Convert.ToChar(Solicitacao.StatusPrioridade.BRANCO).ToString();
                solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString();
                solicitacao.CidSolicitante = ddlCID.SelectedValue == "0" ? null : ddlCID.SelectedValue;

                solicitacao.Qtd = 1;
                solicitacao.UsuarioSolicitante = usuario;
                //if (lblTelefone.Text == "")
                //{
                solicitacao.TelefoneContato = tbxDDD.Text + tbxTelefoneContato.Text;
                //Como o Paciente não tem telefone cadastrado, É feito uma atualização no telefone no cadastro
                endereco.DDD = tbxDDD.Text;
                endereco.Telefone = tbxTelefoneContato.Text;
                EnderecoBLL.Atualizar(endereco);
                if (tbxEmail.Text != "")
                {
                    paciente.Email = tbxEmail.Text;
                    PacienteBLL.AtualizarDadosPacienteSemLog(paciente);
                }


                //}
                //else
                //    solicitacao.TelefoneContato = lblTelefone.Text;
                //Para agendamento Básico o Profissional Solicitante não é Necessário
                if (rbtnTipoProcedimento.SelectedValue != Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.ATENDIMENTOBASICO).ToString())
                {
                    if (ddlProcedimento.SelectedValue == "0301010072")//Procedimento de Consulta Especializada
                    {
                        if (rbtnEspecialidade.SelectedValue != "225265" && rbtnEspecialidade.SelectedValue != "225250" && rbtnEspecialidade.SelectedValue != "225124" && rbtnEspecialidade.SelectedValue != "225125")//Cbo de Médico Oftalmo, Gineco, Pedriatria e Clínico
                        {
                            solicitacao.Id_ProfissionalSolicitante = WUCPesquisarProfissionalSolicitante1.ProfissionalSelecionado.Codigo;
                        }
                    }
                }
                //Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(solicitacao);
                #endregion



                bool salvarPactoReferencia = false;
                bool salvarPactoAbrangencia = false;
                PactoReferenciaSaldo pactoReferenciaSaldoMesAtual = null;
                PactoAbrangenciaAgregado pactoAbrangencia = null;

                #region Salva o Pacto Para o Município (Exceto Salvador)
                //Atualiza o Valor Restante do Pacto do Município
                if (endereco.Municipio.Codigo != Municipio.SALVADOR)//Codigo de Salvador
                {
                    if (procedimentoSelecionado.FinanciamentoProcedimento.Codigo != FinanciamentoProcedimento.FAEC)
                    {
                        Pacto pacto = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(endereco.Municipio.Codigo);
                        PactoAgregadoProcedCBO pactoAgregadoProcedCBO = (PactoAgregadoProcedCBO)Session["pactoAgregadoProcedCBO"];
                        pactoAbrangencia = (PactoAbrangenciaAgregado)Session["PactoAbrangencia"];
                        //PactoAbrangenciaAgregado pactoAbrangencia = (PactoAbrangenciaAgregado)Session["PactoAbrangenciaAgregado"];

                        //Decimal valorProcedimento = Decimal.Parse(procedimentoSelecionado.VL_SA.ToString().Insert(procedimentoSelecionado.VL_SA.ToString().Length - 2, ","));
                        //float valorProcedimentoFloat = float.Parse(procedimentoSelecionado.VL_SA.ToString().Insert(procedimentoSelecionado.VL_SA.ToString().Length - 2, ","));
                        if (pactoAgregadoProcedCBO != null)
                        {
                            IList<PactoReferenciaSaldo> pactosReferenciaSaldo = Factory.GetInstance<IPactoReferenciaSaldo>().BuscarPorPactoAgregado<PactoReferenciaSaldo>(pactoAgregadoProcedCBO.Codigo);
                            pactoReferenciaSaldoMesAtual = pactosReferenciaSaldo.Where(p => p.Mes == DateTime.Now.Month).ToList()[0];
                            DateTime dataMesAnterior = DateTime.Now.AddMonths(-1);

                            //Verifico se o saldo, somado com o procedimento selecionado será superior ao Teto mensal
                            if (Decimal.Parse(pactoReferenciaSaldoMesAtual.ValorRestante.ToString()) < procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado)
                            {
                                //Verifico se o Pacto está Bloqueado por cota
                                if (pactoReferenciaSaldoMesAtual.PactoAgregadoProcedCBO.BloqueiaCota == Convert.ToInt32(PactoAgregadoProcedCBO.BloqueadoPorCota.NAO))
                                {
                                    //Verifica o Percentual Adicional que poderá ultrapassar do pacto
                                    Decimal valorComPercentual = pactoAgregadoProcedCBO.ValorMensal + ((pactoAgregadoProcedCBO.ValorMensal * pactoReferenciaSaldoMesAtual.PactoAgregadoProcedCBO.Percentual) / 100);

                                    if (procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado + Decimal.Parse(pactoReferenciaSaldoMesAtual.ValorRestante.ToString()) > valorComPercentual)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Cota Excedida para o município " + pacto.Municipio.Nome + "!');", true);
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
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Cota Excedida para o município " + pacto.Municipio.Nome + "!');", true);
                                    rbtnEspecialidade.ClearSelection();
                                    return;
                                }
                            }
                            else
                            {
                                pactoReferenciaSaldoMesAtual.ValorRestante -= procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado;
                            }

                            solicitacao.PactoReferenciaSaldo = pactoReferenciaSaldoMesAtual;
                            salvarPactoReferencia = true;
                        }
                        else if (pactoAbrangencia != null)
                        {
                            //pactoAbrangenciaGrupoMunicipio.SaldoCota - float.Parse(procedimentoSelecionado.VL_SA.ToString());

                            pactoAbrangencia.ValorUtilizado += procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado;
                            //pactoAbrangenciaGrupoMunicipio.ValorUtilizado += valorProcedimentoDecimal;
                            salvarPactoAbrangencia = true;
                            //iViverMais.Salvar(pactoAbrangenciaGrupoMunicipio);
                            //Factory.GetInstance<ILogEventos>().Salvar(pactoAbrangenciaGrupoMunicipio);
                            solicitacao.PactoAbrangenciaAgregado = pactoAbrangencia;
                        }
                    }
                }
                #endregion
                
                Parametros parametro = iViverMais.ListarTodos<Parametros>().First();
                DateTime data_inicial = DateTime.Now.AddDays(parametro.Min_Dias);
                DateTime data_final = DateTime.Now.AddDays(parametro.Max_Dias);

                IList<ViverMais.Model.Solicitacao> VerificaSolicitacao = Factory.GetInstance<ISolicitacao>().VerificaSolicitacao<ViverMais.Model.Solicitacao>(paciente.Codigo, ddlProcedimento.SelectedValue, rbtnTipoProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));
                if (procedimentoSelecionado.Codigo == Procedimento.CONSULTA_MEDICA_ATENCAO_BASICA || procedimentoSelecionado.Codigo == Procedimento.CONSULTA_MEDICA_ATENCAO_ESPECIALIZADA)
                {
                    if (VerificaSolicitacao.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Paciente já agendado para esse procedimento!');", true);
                        Wizard1.ActiveStepIndex--;
                        rbtnEspecialidade.ClearSelection();
                        return;
                    }
                }
                else
                {
                    if (VerificaSolicitacao.Count >= procedimentoSelecionado.QtdMaximaExecucao)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Paciente já agendado para esse procedimento!');", true);
                        Wizard1.ActiveStepIndex--;
                        rbtnEspecialidade.ClearSelection();
                        ddlProcedimento.SelectedValue = "0";
                        return;
                    }
                }
                //if (VerificaSolicitacao != null && VerificaSolicitacao.Count != 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Paciente já agendado para esse procedimento!');", true);
                //    Wizard1.ActiveStepIndex--;
                //    rbtnEspecialidade.ClearSelection();
                //    return;
                //}
                try
                {
                    Factory.GetInstance<ISolicitacao>().SalvaSolicitacaoAgendadaAtendimentoBasico<Solicitacao, Agenda>(solicitacao, agenda, (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));
                }
                catch (Exception f)
                {
                    if (f.InnerException != null)
                    {
                        if (f.InnerException.Message.Contains("-20001"))
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A vaga já foi preenchida. Por favor, Selecione a especialidade desejada novamente!');", true);
                            rbtnEspecialidade.ClearSelection();
                            Session["indexAgenda"] = null;
                            Wizard1.ActiveStepIndex--;
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Ocorreu um erro: " + f.Message + "');", true);
                        rbtnEspecialidade.ClearSelection();
                        Session["indexAgenda"] = null;
                        Wizard1.ActiveStepIndex--;
                        return;
                    }
                }

                iViverMais.Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 30, solicitacao.Codigo.ToString()));
                if (salvarPactoAbrangencia)
                    iViverMais.Salvar(pactoAbrangencia);
                else if (salvarPactoReferencia)
                    iViverMais.Salvar(pactoReferenciaSaldoMesAtual);

                if (solicitacao.TipoCotaUtilizada == Convert.ToChar(Solicitacao.TipoCota.REDE))
                {
                    QuantidadeSolicitacaoRede quantidadeSolicitacoes = Factory.GetInstance<IQuantidadeSolicitacaoRede>().BuscaQuantidade<QuantidadeSolicitacaoRede>((DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00")), ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, agenda.Estabelecimento.CNES, int.Parse(ddlSubGrupo.SelectedValue));
                    if (quantidadeSolicitacoes != null)
                    {
                        quantidadeSolicitacoes.QtdSolicitacoes++;
                        iViverMais.Salvar(quantidadeSolicitacoes);
                    }
                }

                //Verifica se o Paciente está incluído na Lista de Procura
                //Caso exista Solicitacao Pendente, ele incrementa a qtd na Lista
                ListaProcura listaProcura = Factory.GetInstance<IListaProcura>().BuscaNaListaPorPacientePorProcedimento<ListaProcura>(solicitacao.ID_Paciente, solicitacao.Procedimento.Codigo).Where(l => l.Agendado == false).ToList().FirstOrDefault();
                if (listaProcura != null)
                {
                    listaProcura.Agendado = true;
                    listaProcura.DataUltimaProcura = DateTime.Now;
                    listaProcura.Quantidade++;
                    iViverMais.Salvar(listaProcura);
                }
                Session["id_solicitacao"] = solicitacao.Codigo;
                Session["tipo_procedimento"] = rbtnTipoProcedimento.SelectedValue;
                Response.Redirect("FormSolicitacao.aspx");
            }
            else if (Wizard1.ActiveStepIndex == 3)
                //Se for Finalizado na Autorização ele direciona para Salvar o Protocolo
                Protocolo();
        }

        protected bool PermiteRealizarProcedimentoMartagaoGesteira(DateTime data_nascimento_paciente)
        {
            int idade_minima = 0;
            int idade_maxima = 14;
            int idade = (int)(((DateTime.Today - data_nascimento_paciente).Days - 1) / 365.25);

            if (idade >= idade_minima && idade <= idade_maxima)
                return true;
            else
                return false;
        }

        protected void GridViewAgenda_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Selecionar")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
                int index = row.RowIndex;
                int codigo_agenda = int.Parse(GridViewAgenda.Rows[index].Cells[10].Text);
                Agenda agenda = iViverMais.BuscarPorCodigo<Agenda>(codigo_agenda);
                int indexSession;
                if (agenda != null)
                {
                    if (agenda.Estabelecimento.CNES == "0004278")//CNES do Hospital Martagão Gesteira
                    {
                        ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];
                        if (PermiteRealizarProcedimentoMartagaoGesteira(paciente.DataNascimento))
                        {
                            if (Session["indexAgenda"] == null)
                                Session["indexAgenda"] = index;
                            else
                            {
                                indexSession = int.Parse(Session["indexAgenda"].ToString());
                                GridViewAgenda.Rows[indexSession].BackColor = Color.FromArgb(238, 238, 238);
                            }
                            GridViewAgenda.Rows[index].BackColor = Color.FromArgb(169, 169, 169);
                            Session["indexAgenda"] = index;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Procedimento não permitido para esta idade no Hospital Martagão Gesteira!')", true);
                            Session["indexAgenda"] = null;
                            return;
                        }
                    }
                    else
                    {
                        if (Session["indexAgenda"] == null)
                            Session["indexAgenda"] = index;
                        else
                        {
                            indexSession = int.Parse(Session["indexAgenda"].ToString());
                            GridViewAgenda.Rows[indexSession].BackColor = Color.FromArgb(238, 238, 238);
                        }
                        GridViewAgenda.Rows[index].BackColor = Color.FromArgb(169, 169, 169);
                        Session["indexAgenda"] = index;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Não foi possível localizar a agenda selecionada. Por favor, entre em contato com o Administrador!')", true);
                    return;
                }
            }
        }

        void Agendas(List<Agenda> agend)
        {
            if (agend.Count != 0)
            {
                agend = agend.OrderBy(p => p.Data).ToList();
                GridViewAgenda.DataSource = agend;
                GridViewAgenda.DataBind();
                //this.lblMensagem.Visible = false;
                Session["Agendas"] = agend;
            }
            else
            {
                ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];

                //Busca na Lista para verificar se o paciente já está incluso
                ListaProcura listaProcura = Factory.GetInstance<IListaProcura>().BuscaNaListaPorPacientePorProcedimento<ListaProcura>(paciente.Codigo, ddlProcedimento.SelectedValue).Where(p => p.Agendado == false).FirstOrDefault();

                //Se o Paciente Já estiver na Lista de Procura e não localizou vaga anteriormente, atualizo a data da Ultima Procura e a Quantidade de Vezes que o Paciente Procurou O Procedimento
                if (listaProcura != null)
                {
                    listaProcura.DataUltimaProcura = DateTime.Now;
                    listaProcura.Quantidade++;
                    Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(listaProcura);
                }
                else //Caso ele não tenha procurado o procedimento nenhuma vez, será inserido na lista de Procura
                {
                    listaProcura = new ListaProcura();
                    listaProcura.Agendado = false;
                    listaProcura.DataInicial = DateTime.Now;
                    listaProcura.DataUltimaProcura = DateTime.Now;
                    listaProcura.Id_paciente = paciente.Codigo;
                    listaProcura.Id_procedimento = ddlProcedimento.SelectedValue;
                    listaProcura.Quantidade = 1;
                    Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(listaProcura);
                }

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não Existe Vaga Para a Especialidade Selecionada!');", true);
                rbtnEspecialidade.ClearSelection();
                ddlSubGrupo.Items.Clear();
                ddlSubGrupo.Items.Add(new ListItem("Selecione...", "0"));
                Wizard1.ActiveStepIndex--;
                //rbtnSubGrupo.Items.Clear();
                //this.lblMensagem.Visible = true;
                return;
            }
        }

        void Protocolo()
        {
            ArrayList laudos = (ArrayList)Session["Laudos"];
            //if (laudos == null || laudos.Count == 0)
            HttpFileCollection hfc = Request.Files;
            bool existeLaudo = false;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    existeLaudo = true;
                    break;
                }
            }
            if (!existeLaudo)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Você Deve Anexar Pelo Menos um Laudo.');", true);
                return;
            }
            ISolicitacao iSolicitacao = Factory.GetInstance<ISolicitacao>();
            IProcedimentoAgregado iProcedimentoAgregado = Factory.GetInstance<IProcedimentoAgregado>();
            ViverMais.Model.Procedimento procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<ViverMais.Model.Procedimento>(ddlProcedimento.SelectedValue);
            ViverMais.Model.Usuario usuario = (Usuario)Session["Usuario"];
            ViverMais.Model.Solicitacao solicitacao = new Solicitacao();
            ViverMais.Model.Paciente paciente1 = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];
            paciente1 = PacienteBLL.Pesquisar(paciente1.Codigo);
            Endereco endereco = EnderecoBLL.PesquisarPorPaciente(paciente1);

            if (paciente1 != null || usuario != null)
            {
                //Salva a solicitação (Protocolo)
                solicitacao.ID_Paciente = paciente1.Codigo;
                solicitacao.Data_Solicitacao = DateTime.Now;
                solicitacao.NumeroProtocolo = iSolicitacao.GeraProtocoloSolicitacao();
                solicitacao.Procedimento = procedimento;
                solicitacao.Observacao = tbxObservacao.Text;
                solicitacao.CidSolicitante = ddlCID.SelectedValue == "0" ? null : ddlCID.SelectedValue;
                solicitacao.UsuarioSolicitante = usuario;
                //if (lblTelefone.Text == "")
                //{
                solicitacao.TelefoneContato = tbxDDD.Text + tbxTelefoneContato.Text;
                //Como o Paciente não tem telefone cadastrado, É feito uma atualização no telefone no cadastro
                //if (endereco != null)
                //{
                endereco.DDD = tbxDDD.Text;
                endereco.Telefone = tbxTelefoneContato.Text;
                EnderecoBLL.Atualizar(endereco);
                if (tbxEmail.Text != "")
                {
                    paciente1.Email = tbxEmail.Text;
                    PacienteBLL.AtualizarDadosPacienteSemLog(paciente1);

                }
                //}
                //else
                //    solicitacao.TelefoneContato = lblTelefone.Text;

                solicitacao.EasSolicitante = usuario.Unidade.CNES;
                solicitacao.Prioridade = "4"; //Prioridade (Branco)
                solicitacao.Situacao = Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE).ToString();//Salva a Solicitação como Pendente
                solicitacao.Qtd = 1;

                if (WUCPesquisarProfissionalSolicitante1.ProfissionalSelecionado.Codigo != 0)
                    solicitacao.Id_ProfissionalSolicitante = WUCPesquisarProfissionalSolicitante1.ProfissionalSelecionado.Codigo;
                iSolicitacao.SalvaSolicitacaoReguladaAutorizada<ViverMais.Model.Solicitacao, HttpFileCollection>(solicitacao, hfc);
                Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 31, "ID: " + solicitacao.Codigo));

                //Salva os dados abaixo na sessão, para utilizar no Page_Load para imprimir o Protocolo ou a Chave
                Session["Laudos"] = null;
                Session["id_solicitacao"] = solicitacao.Codigo;
                Session["tipo_procedimento"] = rbtnTipoProcedimento.SelectedValue;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
                Response.Redirect("FormSolicitacao.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Sua sessão expirou! Faça login novamente.');", true);
                return;
            }
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            //Regulado e Autorizado, Direciona para Encaminhamento - Laudo
            //Agendado e Atendimento Básico, Direciona para Seleção da Agenda
            if (e.NextStepIndex == 2)//Especialidade
            {
                switch (rbtnTipoProcedimento.SelectedValue)
                {
                    case "1": //Regulado
                        Wizard1.ActiveStepIndex = 3;
                        break;
                    case "2"://Autorizado
                        Wizard1.ActiveStepIndex = 3;
                        break;
                    case "3"://Agendado
                        Wizard1.ActiveStepIndex = 2;
                        break;
                    case "4"://Agendamento Básico
                        Wizard1.ActiveStepIndex = 2;
                        break;
                }
            }

        }

        protected void Wizard1_ActiveStepChanged(object sender, EventArgs e)
        {
            if (Wizard1.ActiveStepIndex == 3)//Se for Finalizado na 3(Encaminhamento)
            {
                if (WUCPesquisarProfissionalSolicitante1.ProfissionalSelecionado == null)
                {
                    Wizard1.ActiveStepIndex = 1;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione um Profissional!');", true);
                    return;
                }
                if ((rbtnTipoProcedimento.SelectedValue == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.AGENDADO).ToString()) || (rbtnTipoProcedimento.SelectedValue == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.ATENDIMENTOBASICO).ToString()))//Se For Agendado ou Atendimento Básico
                {
                    if (GridViewAgenda.SelectedRow == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione uma Agenda!');", true);
                        Wizard1.ActiveStepIndex = 2;
                        return;
                    }
                }
            }
            else if (Wizard1.ActiveStepIndex == 1)//Verifico se o Usuário Selecionou um Paciente
            {
                if (Session["pacienteSelecionado"] == null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Você deve Selecionar um Paciente!');", true);
                    Wizard1.ActiveStepIndex = 0;
                    return;
                }
                //if (Session["LeuMsgSolicitacao"] == null)
                //    PanelMensagem.Visible = true;
                //else
                //    PanelMensagem.Visible = false;
            }
            else if (Wizard1.ActiveStepIndex == 2)
            {
                if (rbtnTipoProcedimento.SelectedValue != Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.ATENDIMENTOBASICO).ToString() && WUCPesquisarProfissionalSolicitante1.ProfissionalSelecionado == null)
                {
                    if (rbtnTipoProcedimento.SelectedValue == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.AGENDADO).ToString())
                    {
                        //Verifica se para o Procedimento de Consulta Especializada, os CBOs selecionados são de Médico
                        if (ddlProcedimento.SelectedValue == "0301010072")//Procedimento de Consulta Especializada
                        {
                            if (rbtnEspecialidade.SelectedValue != "225265" && rbtnEspecialidade.SelectedValue != "225250" && rbtnEspecialidade.SelectedValue != "225124" && rbtnEspecialidade.SelectedValue != "225125")//Cbo de Médico Oftalmo, Gineco, Pedriatria e Clínico
                            {
                                Wizard1.ActiveStepIndex = 1;
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione um Profissional!');", true);
                                return;
                            }
                        }
                        else
                        {
                            Wizard1.ActiveStepIndex = 1;
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione um Profissional!');", true);
                            return;
                        }
                    }
                    else
                    {
                        Wizard1.ActiveStepIndex = 1;
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Selecione um Profissional!');", true);
                        return;
                    }
                }
                ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];
                IList<ViverMais.Model.Solicitacao> VerificaSolicitacao = Factory.GetInstance<ISolicitacao>().VerificaSolicitacao<ViverMais.Model.Solicitacao>(paciente.Codigo, ddlProcedimento.SelectedValue, rbtnTipoProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));
                if (ddlProcedimento.SelectedValue == Procedimento.CONSULTA_MEDICA_ATENCAO_BASICA || ddlProcedimento.SelectedValue == Procedimento.CONSULTA_MEDICA_ATENCAO_ESPECIALIZADA)
                {
                    if (VerificaSolicitacao.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Paciente já agendado para esse procedimento!');", true);
                        rbtnEspecialidade.ClearSelection();
                        ddlSubGrupo.SelectedValue = "0";
                        Wizard1.ActiveStepIndex = 1;
                        return;
                    }
                }

                ViverMais.Model.Usuario usuario = (Usuario)Session["Usuario"];
                //List<Agenda> agendas = ((List<Agenda>)Session["Agenda_Vagas"]).Where(p => p.Cbo != null && p.Cbo.Codigo == rbtnEspecialidade.SelectedValue).ToList();


                IList<ViverMais.Model.Parametros> parametro = Factory.GetInstance<IAgendamentoServiceFacade>().ListarTodos<ViverMais.Model.Parametros>();
                if (parametro.Count != 0)
                {
                    IAmbulatorial iAmbulatorial = Factory.GetInstance<IAmbulatorial>();
                    ISolicitacao iSolicitacao = Factory.GetInstance<ISolicitacao>();
                    IParametroAgenda iParametroAgenda = Factory.GetInstance<IParametroAgenda>();
                    //Pego todas as Agendas do procedimento selecionado para o periodo informado
                    //Lista as Agendas Diponíveis para o Procedimento Solicitado nesse periodo
                    DateTime data_inicial = DateTime.Now.AddDays(parametro[0].Min_Dias);
                    DateTime data_final = DateTime.Now.AddDays(parametro[0].Max_Dias);

                    List<Agenda> agendasLocaisDistritaisERede = new List<Agenda>();//Lista Que será enviada para Carregar as agendas para o usuário

                    ParametroAgenda parametroLocal = null;

                    //Verifico se a unidade definiu parametros para o procedimento e especialidade selecionada
                    IList<ParametroAgenda> parametrosAgendaLocal = iParametroAgenda.BuscarParametros<ViverMais.Model.ParametroAgenda>(usuario.Unidade.CNES, ParametroAgenda.CONFIGURACAO_PROCEDIMENTO, ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));

                    //Verifica se a unidade parametrizou as agendas para o procedimento e especialidade selecionados
                    IList<ViverMais.Model.Agenda> agendaLocal = iAmbulatorial.ListarAgendasLocais<ViverMais.Model.Agenda>(ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, data_inicial, data_final, usuario.Unidade.CNES, int.Parse(ddlSubGrupo.SelectedValue));

                    if (agendaLocal.Count != 0)
                    {
                        
                        if (parametrosAgendaLocal.Count != 0)
                        {
                            parametroLocal = parametrosAgendaLocal.Where(p => p.TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.LOCAL)).ToList().FirstOrDefault();
                        }
                        else //Caso não tenha parametrizado o procedimento, utilizo o parametro por Unidade
                        {
                            parametroLocal = iParametroAgenda.BuscarParametrosPorTipo<ParametroAgenda>(usuario.Unidade.CNES, Convert.ToInt32(ParametroAgenda.TipoDeAgenda.LOCAL), ParametroAgenda.CONFIGURACAO_UNIDADE);
                        }

                        if (parametroLocal != null && parametroLocal.Percentual != 0)
                        {
                            //IList<ViverMais.Model.Agenda> agendaLocal = iAmbulatorial.ListarAgendasLocais<ViverMais.Model.Agenda>(procedimentoSelecionado.Codigo, rbtnEspecialidade.SelectedValue, data_inicial, data_final, usuario.Unidade.CNES);
                            int qtdSolicitacaoLocal = int.Parse(iSolicitacao.ListarSolicitacoesParametroLocal(DateTime.Now, ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, usuario.Unidade.CNES, data_inicial, data_final, Convert.ToChar(Solicitacao.TipoCota.LOCAL), int.Parse(ddlSubGrupo.SelectedValue)).ToString());
                            int somaTotalVagasLocal = agendaLocal.Sum(p => p.Quantidade);
                            int qtdDisponivelCotaLocal = (somaTotalVagasLocal * parametroLocal.Percentual) / 100;
                            if (qtdSolicitacaoLocal < qtdDisponivelCotaLocal)
                            {
                                Session["Tipo_Cota_Utilizada"] = Convert.ToChar(Solicitacao.TipoCota.LOCAL);
                                agendaLocal = agendaLocal.Where(p => p.QuantidadeAgendada < p.Quantidade).ToList();
                                agendasLocaisDistritaisERede.AddRange(agendaLocal);
                            }
                        }
                    }
                    //Verifica se a unidade do usuário está autorizada a solicitar procedimento para outras Unidades
                    if (iSolicitacao.UnidadePorSolicitarParaOutra(usuario.Unidade.CNES))
                    {
                        if (agendasLocaisDistritaisERede.Count == 0)
                        {
                            //Verifica o Parametro Distrital
                            if (usuario.Unidade.Bairro != null && usuario.Unidade.Bairro.Distrito != null)
                            {
                                ViverMais.Model.EstabelecimentoSaude[] unidadesDoDistrito = Factory.GetInstance<IParametroAgenda>().ListaEstabelecimentosComParametroDistrital<ViverMais.Model.EstabelecimentoSaude>(usuario.Unidade.Bairro.Distrito.Codigo).Where(p => p.CNES != usuario.Unidade.CNES).ToArray();
                                for (int i = 0; i < unidadesDoDistrito.Length; i++)
                                {
                                    IList<ParametroAgenda> parametrosAgendaDistrital = iParametroAgenda.BuscarParametros<ViverMais.Model.ParametroAgenda>(unidadesDoDistrito[i].CNES, ParametroAgenda.CONFIGURACAO_PROCEDIMENTO, ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));
                                    ParametroAgenda parametroDistrital;
                                    if (parametrosAgendaDistrital.Count != 0)
                                    {
                                        parametroDistrital = parametrosAgendaDistrital.Where(p => p.TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.DISTRITAL)).ToList().FirstOrDefault();
                                    }
                                    else
                                    {
                                        parametroDistrital = iParametroAgenda.BuscarParametrosPorTipo<ParametroAgenda>(unidadesDoDistrito[i].CNES, Convert.ToInt32(ParametroAgenda.TipoDeAgenda.DISTRITAL), ParametroAgenda.CONFIGURACAO_UNIDADE);
                                    }

                                    //Pego o Parametro Distrital para cada Unidade                            
                                    if (parametroDistrital != null && parametroDistrital.Percentual != 0)
                                    {
                                        //Filtro somente as Agendas Pertinentes a unidade da iteracao
                                        IList<ViverMais.Model.Agenda> agendaUnidadeDistrito = iAmbulatorial.ListarAgendasLocais<ViverMais.Model.Agenda>(ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, data_inicial, data_final, unidadesDoDistrito[i].CNES, int.Parse(ddlSubGrupo.SelectedValue));

                                        //Pego todas as Solicitacoes feitas para as agendas da unidade da iteracao
                                        if (agendaUnidadeDistrito.Count != 0)
                                        {
                                            int qtdSolicitacoesDistritais = int.Parse(iSolicitacao.ListarSolicitacoesParametroDistrital(DateTime.Now, ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, unidadesDoDistrito[i].CNES, usuario.Unidade.Bairro.Distrito.Codigo, data_inicial, data_final, Convert.ToChar(Solicitacao.TipoCota.DISTRITAL), int.Parse(ddlSubGrupo.SelectedValue)).ToString());
                                            int qtdTotalDasAgendas = agendaUnidadeDistrito.Sum(p => p.Quantidade);//Faço um somatorio da Quantidade de Todas as Agendas
                                            int qtdDisponivelParaDistrito = (qtdTotalDasAgendas * parametroDistrital.Percentual) / 100; // Verifico a quantidade de vagas que ficará disponível para o Distrito
                                            if (qtdSolicitacoesDistritais < qtdDisponivelParaDistrito)
                                            {
                                                Session["Tipo_Cota_Utilizada"] = Convert.ToChar(Solicitacao.TipoCota.DISTRITAL);
                                                agendaUnidadeDistrito = agendaUnidadeDistrito.Where(p => p.QuantidadeAgendada < p.Quantidade).ToList();
                                                agendasLocaisDistritaisERede.AddRange(agendaUnidadeDistrito);
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        //Se ainda não preencheu as vagas com a Cota Local ou Distrital, iremos para verificar na rede
                        if (agendasLocaisDistritaisERede.Count == 0)
                        {
                            //IList<Agenda> agendasRede;
                            ViverMais.Model.EstabelecimentoSaude[] estabelecimentos = iAmbulatorial.ListarUnidadesComAgendasDisponivelRede<ViverMais.Model.EstabelecimentoSaude>(ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, data_inicial, data_final, usuario.Unidade.CNES, int.Parse(ddlSubGrupo.SelectedValue)).Distinct(new GenericComparer<ViverMais.Model.EstabelecimentoSaude>("CNES")).ToArray();
                            IQuantidadeSolicitacaoRede iQuantidadeSolicitacaoRede = Factory.GetInstance<IQuantidadeSolicitacaoRede>();
                            for (int i = 0; i < estabelecimentos.Length; i++)
                            {
                                ViverMais.Model.EstabelecimentoSaude estabelecimento = estabelecimentos[i];
                                IList<ParametroAgenda> parametrosAgendaRede = iParametroAgenda.BuscarParametros<ViverMais.Model.ParametroAgenda>(estabelecimento.CNES, ParametroAgenda.CONFIGURACAO_PROCEDIMENTO, ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));
                                ParametroAgenda parametroRede;
                                //IList<ViverMais.Model.Agenda> agendaUnidadeRede;
                                if (parametrosAgendaRede.Count != 0)
                                {
                                    parametroRede = parametrosAgendaRede.Where(p => p.TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.REDE)).ToList().FirstOrDefault();
                                }
                                else
                                {
                                    parametroRede = iParametroAgenda.BuscarParametrosPorTipo<ParametroAgenda>(estabelecimento.CNES, Convert.ToInt32(ParametroAgenda.TipoDeAgenda.REDE), ParametroAgenda.CONFIGURACAO_UNIDADE);
                                }
                                if (parametroRede != null && parametroRede.Percentual != 0)
                                {
                                    IList<ViverMais.Model.Agenda> agendasRede = iAmbulatorial.ListarAgendasLocais<ViverMais.Model.Agenda>(ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, data_inicial, data_final, estabelecimento.CNES, int.Parse(ddlSubGrupo.SelectedValue));
                                    if (agendasRede.Count != 0)
                                    {
                                        int qtdSolicitacaoRede;
                                        QuantidadeSolicitacaoRede quantidadeSolicitacoes = iQuantidadeSolicitacaoRede.BuscaQuantidade<QuantidadeSolicitacaoRede>((DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00")), ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, estabelecimento.CNES, int.Parse(ddlSubGrupo.SelectedValue));
                                        if (quantidadeSolicitacoes != null)
                                        {
                                            qtdSolicitacaoRede = quantidadeSolicitacoes.QtdSolicitacoes;
                                        }
                                        else
                                        {
                                            quantidadeSolicitacoes = new QuantidadeSolicitacaoRede();
                                            quantidadeSolicitacoes.Estabelecimento = estabelecimento;
                                            quantidadeSolicitacoes.Competencia = (DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00"));
                                            quantidadeSolicitacoes.CBO = iQuantidadeSolicitacaoRede.BuscarPorCodigo<CBO>(rbtnEspecialidade.SelectedValue);
                                            quantidadeSolicitacoes.Procedimento = iQuantidadeSolicitacaoRede.BuscarPorCodigo<Procedimento>(ddlProcedimento.SelectedValue);
                                            quantidadeSolicitacoes.QtdSolicitacoes = int.Parse(iSolicitacao.ListarSolicitacoesParametroRede(DateTime.Now, ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, estabelecimento.CNES, data_inicial, data_final, Convert.ToChar(Solicitacao.TipoCota.REDE), int.Parse(ddlSubGrupo.SelectedValue)).ToString());
                                            if (ddlSubGrupo.SelectedValue != "0")
                                                quantidadeSolicitacoes.SubGrupo = iQuantidadeSolicitacaoRede.BuscarPorCodigo<SubGrupo>(int.Parse(ddlSubGrupo.SelectedValue));
                                            iQuantidadeSolicitacaoRede.Salvar(quantidadeSolicitacoes);
                                            qtdSolicitacaoRede = quantidadeSolicitacoes.QtdSolicitacoes;
                                        }
                                        //= int.Parse(iSolicitacao.ListarSolicitacoesParametroDistrital(DateTime.Now, procedimentoSelecionado.Codigo, rbtnEspecialidade.SelectedValue, unidadesDoDistrito[i].CNES, usuario.Unidade.Bairro.Distrito.Codigo, data_inicial, data_final, Convert.ToChar(Solicitacao.TipoCota.REDE)).ToString());
                                        int somaTotalVagasRede = agendasRede.Sum(p => p.Quantidade);
                                        int qtdDisponivelCotaRede = (somaTotalVagasRede * parametroRede.Percentual) / 100;
                                        if (qtdSolicitacaoRede < qtdDisponivelCotaRede)
                                        {
                                            Session["Tipo_Cota_Utilizada"] = Convert.ToChar(Solicitacao.TipoCota.REDE);
                                            agendasRede = agendasRede.Where(p => p.QuantidadeAgendada < p.Quantidade).ToList();
                                            agendasLocaisDistritaisERede.AddRange(agendasRede);
                                        }
                                    }
                                }
                            }
                        }

                        //Caso não tenha ainda preenchido com os parametros acima, verifico se o parametro de reserva técnica possui. (Somente para usuários da Regulação)
                        if (agendasLocaisDistritaisERede.Count == 0)
                        {
                            string[] nomesPerfis = usuario.Perfis.Select(p => p.Nome.ToUpper()).ToArray();

                            if (nomesPerfis.Contains("SUPERVISAO REGULACAO")) //Junior definiu que só a supervisão da Regulação poderá usar a reserva técnica
                            //if (usuario.Unidade.CNES == ViverMais.Model.EstabelecimentoSaude.CNES_SMS_SSA)
                            {
                                IList<Agenda> agendasReservaTecnica;
                                ViverMais.Model.EstabelecimentoSaude[] estabelecimentosReservaTecnica = iAmbulatorial.ListarUnidadesComAgendasDisponivelReservaTecnica<ViverMais.Model.EstabelecimentoSaude>(ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, data_inicial, data_final, int.Parse(ddlSubGrupo.SelectedValue)).Distinct(new GenericComparer<ViverMais.Model.EstabelecimentoSaude>("CNES")).ToArray();
                                //Para cadas Estabelecimento
                                for (int i = 0; i < estabelecimentosReservaTecnica.Length; i++)
                                {
                                    ViverMais.Model.EstabelecimentoSaude estabelecimentoReserva = estabelecimentosReservaTecnica[i];
                                    //Primeiro tenho que verificar se o estabelecimento da iteração parametrizou o percentual para este procedimento e especialidade
                                    //Em caso positico, utilizaremos esta parametrização e em caso negativo, utilizaremos a parametrização por unidade
                                    IList<ParametroAgenda> parametrosAgenda = iParametroAgenda.BuscarParametros<ViverMais.Model.ParametroAgenda>(estabelecimentoReserva.CNES, ParametroAgenda.CONFIGURACAO_PROCEDIMENTO, ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, (ddlSubGrupo.SelectedValue == "0" ? null : ddlSubGrupo.SelectedValue));
                                    ParametroAgenda parametroReservaTecnica = null;
                                    if (parametrosAgenda.Count != 0)
                                    {
                                        parametroReservaTecnica = parametrosAgenda.Where(p => p.TipoAgenda == Convert.ToInt32(ParametroAgenda.TipoDeAgenda.RESERVA_TECNICA)).ToList().FirstOrDefault();
                                    }
                                    else // Se ele não parametrizou o procedimento e CBO, pego o parametro Agenda da Unidade
                                    {
                                        parametroReservaTecnica = iParametroAgenda.BuscarParametrosPorTipo<ParametroAgenda>(estabelecimentoReserva.CNES, Convert.ToInt32(ParametroAgenda.TipoDeAgenda.RESERVA_TECNICA), ParametroAgenda.CONFIGURACAO_UNIDADE);
                                    }

                                    if (parametroReservaTecnica != null && parametroReservaTecnica.Percentual != 0)
                                    {
                                        agendasReservaTecnica = iAmbulatorial.ListarAgendasLocais<ViverMais.Model.Agenda>(ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, data_inicial, data_final, estabelecimentoReserva.CNES, int.Parse(ddlSubGrupo.SelectedValue));
                                        if (agendasReservaTecnica.Count != 0)
                                        {
                                            int qtdSolicitacoesReservaTecnica = int.Parse(iSolicitacao.ListarSolicitacoesParametroReservaTecnica(DateTime.Now, ddlProcedimento.SelectedValue, rbtnEspecialidade.SelectedValue, estabelecimentoReserva.CNES, data_inicial, data_final, Convert.ToChar(Solicitacao.TipoCota.RESERVA_TECNICA), int.Parse(ddlSubGrupo.SelectedValue)).ToString());
                                            int qtdTotalVagasReserva = agendasReservaTecnica.Sum(p => p.Quantidade);
                                            int qtdDisponivelReservaTecnica = (qtdTotalVagasReserva * parametroReservaTecnica.Percentual) / 100;// Verifico a quantidade de vagas que ficará disponível para a Regulação Marcar
                                            if (qtdSolicitacoesReservaTecnica < qtdDisponivelReservaTecnica)
                                            {
                                                Session["Tipo_Cota_Utilizada"] = Convert.ToChar(Solicitacao.TipoCota.RESERVA_TECNICA);
                                                agendasReservaTecnica = agendasReservaTecnica.Where(p => p.QuantidadeAgendada < p.Quantidade).ToList();
                                                agendasLocaisDistritaisERede.AddRange(agendasReservaTecnica);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Agendas(agendasLocaisDistritaisERede);
                }

            }
        }

        //protected void OnClick_btnFechar(object sender, EventArgs e)
        //{
        //    Session["LeuMsgHomePage"] = 1;
        //    PanelMensagem.Visible = false;
        //}
    }
}