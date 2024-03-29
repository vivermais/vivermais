﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;

namespace ViverMais.View.Urgencia
{
    public partial class FormImprimirProntuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                long temp;

                if (Request["co_prontuario"] != null && long.TryParse(Request["co_prontuario"].ToString(), out temp))
                    GerarPdf(long.Parse(Request["co_prontuario"].ToString()));
            }
        }

        /// <summary>
        /// Gera o histórico do prontuário informado
        /// </summary>
        /// <param name="co_prontuario">código do prontuário</param>
        private void GerarPdf(long co_prontuario)
        {
            try
            {
                ViverMais.Model.Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<ViverMais.Model.Prontuario>(co_prontuario);
                MemoryStream memo = new MemoryStream();

                Document doc = new Document();
                PdfWriter.GetInstance(doc, memo);
                doc.Open();

                //Dados do prontuário
                PdfPTable tabela = new PdfPTable(3);

                PdfPCell cell = new PdfPCell(new Phrase("Dados do Prontuário", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.BackgroundColor = new Color(0xC0, 0xC0, 0xC0);
                tabela.AddCell(cell);

                tabela.AddCell(new Phrase("Número do Prontuário", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                tabela.AddCell(new Phrase("Paciente", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                tabela.AddCell(new Phrase("Situação", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));

                tabela.AddCell(new Phrase(prontuario.NumeroToString, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL)));
                string nome_paciente = !string.IsNullOrEmpty(prontuario.Paciente.Nome) ? prontuario.Paciente.Nome : "Não Identificado";
                tabela.AddCell(new Phrase(nome_paciente, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL)));
                tabela.AddCell(new Phrase(prontuario.Situacao.Nome, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL)));

                doc.Add(tabela);

                //Dados do Acolhimento
                tabela = new PdfPTable(2);

                cell = new PdfPCell(new Phrase("Dados do Acolhimento", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                cell.BackgroundColor = new Color(0xC0, 0xC0, 0xC0);
                tabela.AddCell(cell);

                string tensao_arterial = !string.IsNullOrEmpty(prontuario.TensaoArterial) ? prontuario.TensaoArterial : " - ";
                tabela.AddCell(new Phrase("Tensão Arterial: " + tensao_arterial, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                string freqcard = !string.IsNullOrEmpty(prontuario.FrequenciaCardiaca) ? prontuario.FrequenciaCardiaca : " - ";
                tabela.AddCell(new Phrase("Frequência Cardíaca: " + freqcard, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                string freqresp = !string.IsNullOrEmpty(prontuario.FrequenciaRespiratoria) ? prontuario.FrequenciaRespiratoria : " - ";
                tabela.AddCell(new Phrase("Frequência Respiratória: " + freqresp, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));

                string temperatura = !string.IsNullOrEmpty(prontuario.Temperatura) ? prontuario.Temperatura : " - ";
                tabela.AddCell(new Phrase("Temperatura: " + temperatura, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                string hgt = !string.IsNullOrEmpty(prontuario.Hgt) ? prontuario.Hgt : " - ";
                tabela.AddCell(new Phrase("HGT: " + hgt, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                string acidente = prontuario.Acidente ? "SIM" : "NÃO";
                tabela.AddCell(new Phrase("Acidente: " + acidente, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));

                string dor_intensa = prontuario.DorIntensa ? "SIM" : "NÃO";
                tabela.AddCell(new Phrase("Dor Intensa: " + dor_intensa, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                string fratura = prontuario.Fratura ? "SIM" : "NÃO";
                tabela.AddCell(new Phrase("Fratura: " + fratura, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                string convulsao = prontuario.Convulsao ? "SIM" : "NÃO";
                tabela.AddCell(new Phrase("Convulsão: " + convulsao, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));

                string alergia = prontuario.Alergia ? "SIM" : "NÃO";
                tabela.AddCell(new Phrase("Alergia: " + alergia, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                string asma = prontuario.Asma ? "SIM" : "NÃO";
                tabela.AddCell(new Phrase("Asma: " + asma, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                string diarreia = prontuario.Diarreia ? "SIM" : "NÃO";
                tabela.AddCell(new Phrase("Diarréia: " + diarreia, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));

                doc.Add(tabela);

                //Dados da Consulta Médica
                tabela = new PdfPTable(2);

                cell = new PdfPCell(new Phrase("Dados - Consulta Médica", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                cell.BackgroundColor = new Color(0xC0, 0xC0, 0xC0);
                tabela.AddCell(cell);

                tabela.AddCell(new Phrase("Queixa", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                tabela.AddCell(new Phrase(!string.IsNullOrEmpty(prontuario.Queixa) ? prontuario.Queixa : " - ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                tabela.AddCell(new Phrase("Anamnese/Exame Físico", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                tabela.AddCell(new Phrase(!string.IsNullOrEmpty(prontuario.Anamnese) ? prontuario.Anamnese : " - ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                tabela.AddCell(new Phrase("Sumário de Alta", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                tabela.AddCell(new Phrase(!string.IsNullOrEmpty(prontuario.SumarioAlta) ? prontuario.SumarioAlta : " - ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                tabela.AddCell(new Phrase("Suspeita Diagnóstica", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));

                if (prontuario.CodigosCids != null && prontuario.CodigosCids.Count() > 0)
                {
                    PdfPTable tabela2 = new PdfPTable(1);
                    IList<Cid> suspeitadiagnostica = Factory.GetInstance<IProntuario>().ListarCids<Cid>(prontuario.CodigosCids.ToArray());

                    foreach (ViverMais.Model.Cid c in suspeitadiagnostica)
                    {
                        PdfPCell cell2 = new PdfPCell(new Phrase(c.Nome, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell2.Border = 0;
                        tabela2.AddCell(cell2);
                    }

                    tabela.AddCell(tabela2);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(" - ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    tabela.AddCell(cell);
                }

                doc.Add(tabela);

                ////Dados da Evolução Médica
                IList<EvolucaoMedica> evolucoesmedica = Factory.GetInstance<IEvolucaoMedica>().buscaPorProntuario<ViverMais.Model.EvolucaoMedica>(prontuario.Codigo);

                if (evolucoesmedica != null && evolucoesmedica.Count() > 0)
                {
                    foreach (EvolucaoMedica ev in evolucoesmedica)
                    {
                        tabela = new PdfPTable(2);

                        cell = new PdfPCell(new Phrase("Dados - Evolução Consulta Médica", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.Colspan = 2;
                        cell.Border = Rectangle.NO_BORDER;
                        cell.BackgroundColor = new Color(0xC0, 0xC0, 0xC0);
                        tabela.AddCell(cell);

                        tabela.AddCell(new Phrase("Data", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase(ev.Data.ToString("dd/MM/yyyy"), FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase("Profissional", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase(Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(ev.CodigoProfissional.Trim()).Nome, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase("Observação", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase(!string.IsNullOrEmpty(ev.Observacao) ? ev.Observacao : " - ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase("Suspeita Diagnóstica", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));

                        //if (ev.SuspeitaDiagnostica != null && ev.SuspeitaDiagnostica.Count() > 0)
                        //{
                        //    PdfPTable tabela2 = new PdfPTable(1);

                        //    foreach (Cid c in ev.SuspeitaDiagnostica)
                        //    {
                        //        PdfPCell cell2 = new PdfPCell(new Phrase(c.Nome, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        //        cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                        //        cell2.Border = 0;
                        //        tabela2.AddCell(cell2);
                        //    }

                        //    tabela.AddCell(tabela2);
                        //}
                        //else
                        //{
                            cell = new PdfPCell(new Phrase(" - ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            tabela.AddCell(cell);
                        //}

                        doc.Add(tabela);
                    }
                }
                else
                {
                    tabela = new PdfPTable(1);

                    cell = new PdfPCell(new Phrase("Dados - Evolução Consulta Médica", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.BackgroundColor = new Color(0xC0, 0xC0, 0xC0);
                    tabela.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" - ", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabela.AddCell(cell);

                    doc.Add(tabela);
                }


                ////Dados da Evolução Enfermagem
                IList<EvolucaoEnfermagem> evolucoesenfermagem = Factory.GetInstance<IEvolucaoEnfermagem>().buscaPorProntuario<ViverMais.Model.EvolucaoEnfermagem>(prontuario.Codigo);

                if (evolucoesenfermagem != null && evolucoesenfermagem.Count() > 0)
                {
                    foreach (EvolucaoEnfermagem ev in evolucoesenfermagem)
                    {
                        tabela = new PdfPTable(2);

                        cell = new PdfPCell(new Phrase("Dados - Evolução Consulta Enfermagem", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.Colspan = 2;
                        cell.Border = Rectangle.NO_BORDER;
                        cell.BackgroundColor = new Color(0xC0, 0xC0, 0xC0);
                        tabela.AddCell(cell);

                        tabela.AddCell(new Phrase("Data", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase(ev.Data.ToString("dd/MM/yyyy"), FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase("Profissional", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase(Factory.GetInstance<IProfissional>().BuscarPorCodigo<ViverMais.Model.Profissional>(ev.CodigoProfissional.Trim()).Nome, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase("Observação", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase(!string.IsNullOrEmpty(ev.Observacao) ? ev.Observacao : " - ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase("Aprazamento", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));
                        tabela.AddCell(new Phrase(!string.IsNullOrEmpty(ev.Aprazamento) ? ev.Aprazamento : " - ", FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)));

                        doc.Add(tabela);
                    }
                }
                else
                {
                    tabela = new PdfPTable(1);

                    cell = new PdfPCell(new Phrase("Dados - Evolução Consulta Enfermagem", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Colspan = 2;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.BackgroundColor = new Color(0xC0, 0xC0, 0xC0);
                    tabela.AddCell(cell);
                    cell = new PdfPCell(new Phrase(" - ", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD)));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabela.AddCell(cell);

                    doc.Add(tabela);
                }

                doc.Close();

                Response.ContentType = "Application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=prontuario.pdf");
                Response.BinaryWrite(memo.ToArray());
                Response.End();
            }
            catch (Exception f)
            {
                throw f;
            }
        }
    }
}
