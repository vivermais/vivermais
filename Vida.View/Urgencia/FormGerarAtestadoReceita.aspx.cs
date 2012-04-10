﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Data.SqlClient;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;
using ViverMais.BLL;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using System.Text;
using System.Threading;

namespace ViverMais.View.Urgencia
{
    public partial class FormGerarAtestadoReceita : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long temp;
                string[] modelos = { "prescription", "attested", "attendance" };

                if (Request["model"] != null && modelos.Contains(Request["model"].ToString())
                    && Request["co_evolucao"] != null && long.TryParse(Request["co_evolucao"].ToString(), out temp)
                    && Request["co_profissional"] != null && Request["cbo_profissional"] != null)
                {
                    IControlePrescricaoUrgence iControle = Factory.GetInstance<IControlePrescricaoUrgence>();
                    ViewState["co_profissional"] = Request["co_profissional"].ToString();
                    ViewState["cbo_profissional"] = Request["cbo_profissional"].ToString();

                    //if (Request["co_prontuario"] != null && long.TryParse(Request["co_prontuario"].ToString(), out temp))
                    //{
                    //    ViewState["co_prontuario"] = Request["co_prontuario"].ToString();

                    //    Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(long.Parse(Request["co_prontuario"].ToString()));
                    //    VinculoProfissional vinculo = Factory.GetInstance<IVinculo>().BuscarPorVinculoProfissional<VinculoProfissional>(prontuario.CodigoUnidade, Request["co_profissional"].ToString(), Request["cbo_profissional"].ToString()).FirstOrDefault();

                    //    this.PreencheCabecalho(prontuario.Paciente, prontuario.NumeroToString, prontuario.CodigoUnidade);
                    //    string dataatendimento = string.Empty;

                    //    if (prontuario.Data.ToString("dd/MM/yyyy") == prontuario.DataConsultaMedica.Value.ToString("dd/MM/yyyy"))
                    //        dataatendimento = prontuario.Data.ToString("dd/MM/yyyy") + " das " + prontuario.Data.ToString("HH:mm") + "h às " + prontuario.DataConsultaMedica.Value.ToString("HH:mm") + "h";
                    //    else
                    //        dataatendimento = prontuario.Data.ToString("dd/MM/yyyy") + " às " + prontuario.Data.ToString("HH:mm") + "h até o dia " + prontuario.DataConsultaMedica.Value.ToString("dd/MM/yyyy") + " às " + prontuario.DataConsultaMedica.Value.ToString("HH:mm") + "h";

                    //    switch (Request["model"].ToString())
                    //    {
                    //        case "attested":
                    //            this.MostrarAtestadoMedico(LabelPacienteCabecalho.Text, dataatendimento, RetornaStringCID(prontuario.CodigosCids), vinculo.Profissional.Nome, vinculo.RegistroConselho);
                    //            break;
                    //        case "prescription":
                    //            ControlePrescricaoUrgence controle = iControle.BuscarPorPrimeiraConsulta<ControlePrescricaoUrgence>(long.Parse(ViewState["co_prontuario"].ToString()));
                    //            this.MostrarReceitaMedica(vinculo.Profissional.Nome, vinculo.RegistroConselho, controle);
                    //            break;
                    //        default:
                    //            this.MostrarAtestadoComparecimento(LabelPacienteCabecalho.Text, dataatendimento, vinculo.Profissional.Nome, vinculo.RegistroConselho);
                    //            break;
                    //    }
                    //}
                    //else
                    //{
                        if (Request["co_evolucao"] != null && long.TryParse(Request["co_evolucao"].ToString(), out temp))
                        {
                            ViewState["co_evolucao"] = Request["co_evolucao"].ToString();

                            EvolucaoMedica evolucaomedica = Factory.GetInstance<IEvolucaoMedica>().BuscarPorCodigo<EvolucaoMedica>(long.Parse(Request["co_evolucao"].ToString()));
                            VinculoProfissional vinculo = Factory.GetInstance<IVinculo>().BuscarPorVinculoProfissional<VinculoProfissional>(evolucaomedica.Prontuario.CodigoUnidade, Request["co_profissional"].ToString(), Request["cbo_profissional"].ToString()).FirstOrDefault();

                            this.PreencheCabecalho(evolucaomedica.Prontuario.Paciente, evolucaomedica.Prontuario.NumeroToString, evolucaomedica.Prontuario.CodigoUnidade);
                            string dataatendimento = string.Empty;
                            if (evolucaomedica.Prontuario.Data.ToString("dd/MM/yyyy") == evolucaomedica.Data.ToString("dd/MM/yyyy"))
                                dataatendimento = evolucaomedica.Prontuario.Data.ToString("dd/MM/yyyy") + " das " + evolucaomedica.Prontuario.Data.ToString("HH:mm") + "h às " + evolucaomedica.Prontuario.DataConsultaMedica.Value.ToString("HH:mm") + "h";
                            else
                                dataatendimento = evolucaomedica.Prontuario.Data.ToString("dd/MM/yyyy") + " às " + evolucaomedica.Prontuario.Data.ToString("HH:mm") + "h até o dia " + evolucaomedica.Data.ToString("dd/MM/yyyy") + " às " + evolucaomedica.Data.ToString("HH:mm") + "h";

                            switch (Request["model"].ToString())
                            {
                                case "attested":
                                    this.MostrarAtestadoMedico(LabelPacienteCabecalho.Text, dataatendimento, string.Join(",",evolucaomedica.CodigosCids.ToArray()), vinculo.Profissional.Nome, vinculo.RegistroConselho);
                                    break;
                                case "prescription":
                                    ControlePrescricaoUrgence controle = iControle.BuscarPorEvolucaoMedica<ControlePrescricaoUrgence>(long.Parse(ViewState["co_evolucao"].ToString()));
                                    this.MostrarReceitaMedica(vinculo.Profissional.Nome, vinculo.RegistroConselho, controle);
                                    break;
                                default:
                                    this.MostrarAtestadoComparecimento(LabelPacienteCabecalho.Text, dataatendimento, vinculo.Profissional.Nome, vinculo.RegistroConselho);
                                    break;
                            }
                        }
                    //}
                }
            }
        }

        /// <summary>
        /// Prenche o cabeçalho do documento
        /// </summary>
        /// <param name="p"></param>
        /// <param name="numeroatendimento"></param>
        /// <param name="co_unidade"></param>
        private void PreencheCabecalho(PacienteUrgence pacienteurgencia, string numeroatendimento, string co_unidade)
        {
            if (!string.IsNullOrEmpty(pacienteurgencia.CodigoViverMais))
            {
                ViverMais.Model.Paciente paciente = PacienteBLL.PesquisarCompleto(pacienteurgencia.CodigoViverMais);
                //ViverMais.Model.Paciente pac = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(p.CodigoViverMais);
                LabelPacienteCabecalho.Text = paciente.Nome;
                IList<ViverMais.Model.CartaoSUS> cartoes = Factory.GetInstance<IPaciente>().ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo);
                LabelCNSCabecalho.Text = cartoes.Last().Numero;
                //Documento doc = Factory.GetInstance<IPaciente>().BuscarDocumento<Documento>("10", pac.Codigo);
                List<ViverMais.Model.Documento> documentos = DocumentoBLL.PesqusiarPorPaciente(paciente); //ipaciente.ListarDocumentos<ViverMais.Model.Documento>(paciente.Codigo);
                ViverMais.Model.Documento documento = (from _documento in documentos
                                                 where
                                                 int.Parse(_documento.ControleDocumento.TipoDocumento.Codigo) == 10
                                                 select _documento).FirstOrDefault();
                if (documento != null)
                    LabelRGCabecalho.Text = !string.IsNullOrEmpty(documento.Numero) ? documento.Numero : "-";
                else
                    LabelRGCabecalho.Text = "-";
            }
            else
            {
                LabelPacienteCabecalho.Text = string.IsNullOrEmpty(pacienteurgencia.Nome) ? "Não Identificado" : pacienteurgencia.Nome;
                LabelCNSCabecalho.Text = "-";
                LabelRGCabecalho.Text = "-";
            }

            ViverMais.Model.EstabelecimentoSaude unidade = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(co_unidade);
            LabelUnidadeCabecalho.Text = unidade.NomeFantasia;
            LabelCNESCabecalho.Text = unidade.CNES;
            LabelNumeroAtendimentoCabecalho.Text = numeroatendimento;
        }

        //private string RetornaStringMedicamentos(IList<PrescricaoMedicamento> lm)
        //{
        //    string medicamentos = string.Empty;

        //    foreach (PrescricaoMedicamento pm in lm)
        //        medicamentos += pm.ObjetoMedicamento.Nome + ",";

        //    if (!string.IsNullOrEmpty(medicamentos))
        //        medicamentos = medicamentos.Remove(medicamentos.Length - 1, 1);

        //    return medicamentos;
        //}

        private void MostrarReceitaMedica(string profissional, string crm, ControlePrescricaoUrgence controle)
        {
            PanelReceitaMedica.Visible = true;
            Label_Conteudo.Text = "Receita Médica";

            LabelMedicoReceita.Text = profissional;
            LabelCRMReceita.Text = crm;
            LabelDataReceitaMedica.Text = "Salvador, " + DateTime.Now.ToString("dd/MM/yyyy");

            if (controle != null)
            {
                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
                PrescricaoMedicamento[] medicamentos = iPrescricao.BuscarMedicamentos<PrescricaoMedicamento>(controle.Prescricao.Codigo).Where(p=>p.ObjetoMedicamento.EMedicamento == true).ToArray();

                if (medicamentos.Length > 0)
                {
                    StringBuilder textomedicamentos = new StringBuilder();
                    textomedicamentos.Append("<ul>");
                    
                    for(int i = 0; i < medicamentos.Length; i++)
                        textomedicamentos.Append("<li>" + medicamentos[i].ObjetoMedicamento.Nome + " </li>");
                    
                    textomedicamentos.Append("</ul>");

                    this.EditorMedicamentosReceita.Value = textomedicamentos.ToString();
                }
            }
        }

        //private void MostrarReceitaMedica(string medicamentos, string profissional, string crm)
        //{
        //    PanelReceitaMedica.Visible = true;
        //    Label_Conteudo.Text = "Receita Médica";

        //    TextBoxMedicamentosReceita.Text = medicamentos;

        //    LabelMedicoReceita.Text = profissional;
        //    LabelCRMReceita.Text = crm;
        //    LabelDataReceitaMedica.Text = "Salvador, " + DateTime.Now.ToString("dd/MM/yyyy");
        //}

        private void MostrarAtestadoComparecimento(string paciente, string periodoatendimento, string profissional, string crm)
        {
            PanelAtestadoComparecimento.Visible = true;
            Label_Conteudo.Text = "Declaração de Comparecimento";

            LabelPacienteAtestadoComparecimento.Text = paciente;
            LabelPeriodoAtestadoComparecimento.Text = periodoatendimento;
            LabelMedicoComparecimento.Text = profissional;
            LabelCRMComparecimento.Text = crm;

            LabelDataAtestadoComparecimento.Text = "Salvador, " + DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void MostrarAtestadoMedico(string paciente, string periodoatendimento, string cids, string profissional, string crm)
        {
            PanelAtestadoMedico.Visible = true;
            Label_Conteudo.Text = "Atestado Médico";

            LabelPacienteAtestadoMedico.Text = paciente;
            LabelHorarioAtestadoMedico.Text = periodoatendimento;
            CIDSAtestadoMedico.Text = cids;
            CIDSAtestadoMedico.DataBind();
            LabelMedicoAtestado.Text = profissional;
            LabelCRMAtestado.Text = crm;

            LabelDataAtestadoMedico.Text = "Salvador, " + DateTime.Now.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Gera o atestto ou receita para o prontuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Gerar(object sender, EventArgs e)
        {
            LinkButton gerar = (LinkButton)sender;
            IEvolucaoMedica iUrgencia = Factory.GetInstance<IEvolucaoMedica>();
            Prontuario prontuario = iUrgencia.BuscarPorCodigo<EvolucaoMedica>(long.Parse(ViewState["co_evolucao"].ToString())).Prontuario;
            Usuario usuario = (Usuario)Session["Usuario"];

            try
            {
                AtestadoReceitaUrgence atestadoreceita = null;

                switch (gerar.CommandArgument)
                {
                    case "atestadomedico": //12
                        if (string.IsNullOrEmpty(CIDSAtestadoMedico.Text))
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não é possível gerar o atestado médico pois não foi encontrado CID algum.');", true);
                            return;
                        }
                        else
                        {
                            atestadoreceita = new AtestadoReceitaUrgence(prontuario, DateTime.Now, ViewState["co_profissional"].ToString(), ViewState["cbo_profissional"].ToString(), RetornaConteudoAtestadoMedico(), iUrgencia.BuscarPorCodigo<TipoAtestadoReceita>(TipoAtestadoReceita.Atestado));
                            iUrgencia.Salvar(atestadoreceita);
                            iUrgencia.Inserir(new ViverMais.Model.LogUrgencia(DateTime.Now, usuario.Codigo, 12, "id prontuario: " + prontuario.Codigo));
                        }
                        break;
                    case "atestadocomparecimento": //41
                        if (!string.IsNullOrEmpty(this.EditorInformacaoAtestadoComparecimento.Value))
                        {
                            atestadoreceita = new AtestadoReceitaUrgence(prontuario, DateTime.Now, ViewState["co_profissional"].ToString(), ViewState["cbo_profissional"].ToString(), RetornaConteudoComparecimento(), iUrgencia.BuscarPorCodigo<TipoAtestadoReceita>(TipoAtestadoReceita.Comparecimento));
                            iUrgencia.Salvar(atestadoreceita);
                            iUrgencia.Inserir(new ViverMais.Model.LogUrgencia(DateTime.Now, usuario.Codigo, 41, "id prontuario: " + prontuario.Codigo));
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe o conteúdo para o atestado de comparecimento.');", true);
                            return;
                        }
                        break;
                    default: //13
                        if (!string.IsNullOrEmpty(EditorMedicamentosReceita.Value))
                        {
                            atestadoreceita = new AtestadoReceitaUrgence(prontuario, DateTime.Now, ViewState["co_profissional"].ToString(), ViewState["cbo_profissional"].ToString(), RetornaConteudoReceitaMedica(), iUrgencia.BuscarPorCodigo<TipoAtestadoReceita>(TipoAtestadoReceita.Receita));
                            iUrgencia.Salvar(atestadoreceita);
                            iUrgencia.Inserir(new ViverMais.Model.LogUrgencia(DateTime.Now, usuario.Codigo, 13, "id prontuario: " + prontuario.Codigo));
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe o conteúdo da receita.');", true);
                            return;
                        }
                        break;
                }

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "abrir", "window.open('FormImprimirAtestadoReceita.aspx?co_atestadoreceita=" + atestadoreceita.Codigo + "');javascript:parent.parent.GB_hide();", true);
            }
            catch (Exception f)
            {
                throw f;
            }
            HelperView.ExecutarPlanoContingencia(usuario.Codigo, prontuario.Codigo);
            //try
            //{
            //    StartBackgroundThread(delegate { this.ExecutarPlanoContingencia(usuario.Codigo, prontuario.Codigo); });
            //}
            //catch { }
        }

        ///// <summary>
        ///// Executa o plano de contingência
        ///// </summary>
        ///// <param name="co_usuario">código do usuário</param>
        ///// <param name="co_prontuario">código do prontuário</param>
        //public void ExecutarPlanoContingencia(int co_usuario, long co_prontuario)
        //{
        //    try
        //    {
        //        IProntuario iProntuario = Factory.GetInstance<IProntuario>();
        //        iProntuario.ExecutarPlanoContingencia(co_usuario, co_prontuario);
        //    }
        //    catch
        //    {
        //    }
        //}

        ///// <summary>
        ///// Função que executa um procedimento em background
        ///// </summary>
        ///// <param name="threadStart"></param>
        //public static void StartBackgroundThread(ThreadStart threadStart)
        //{
        //    if (threadStart != null)
        //    {
        //        Thread thread = new Thread(threadStart);
        //        thread.IsBackground = true;
        //        thread.Start();
        //    }
        //}

        private string RetornaConteudoComparecimento()
        {
            string texto = HttpUtility.HtmlEncode(this.EditorInformacaoAtestadoComparecimento.Value);
            string conteudo = "<p style=\"text-align: justify;\">Declaro que o(a) Sr(a) " + LabelPacienteAtestadoComparecimento.Text;
            conteudo += " compareceu nesta unidade de saúde no dia ";
            conteudo += LabelPeriodoAtestadoComparecimento.Text;
            conteudo += " para ";
            conteudo += texto + "</p>";
            return conteudo;
        }

        private string RetornaConteudoAtestadoMedico()
        {
            string conteudo = "<p style=\"text-align: justify;\">Atesto para devidos fins, que o(a) Sr(a) " + LabelPacienteAtestadoMedico.Text;
            conteudo += " compareceu nesta unidade de saúde no dia ";
            conteudo += LabelHorarioAtestadoMedico.Text;
            conteudo += " apresentando CID: " + CIDSAtestadoMedico.Text;
            conteudo += ". Necessita ficar afastado de suas atiViverMaisdes por um período de ";
            conteudo += TextBoxPeriodoAfastamentoAtestadoMedico.Text;
            conteudo += " dia(s) a partir desta data. </p>";
            return conteudo;
        }

        private string RetornaConteudoReceitaMedica()
        {
            string conteudo = HttpUtility.HtmlEncode(this.EditorMedicamentosReceita.Value);
            return conteudo;
        }
    }
}