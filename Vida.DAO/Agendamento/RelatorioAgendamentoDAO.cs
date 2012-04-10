using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using System.Collections;
//using Microsoft.Office.Interop.Excel;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using System.Data;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using System.Globalization;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using NPOI.HSSF.UserModel;
using System.IO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using NPOI.HSSF.Util;
using Oracle.DataAccess.Client;
using ViverMais.Model.Entities.ViverMais;
using ViverMais.BLL;
using NHibernate.Mapping;



namespace ViverMais.DAO.Agendamento
{
    public class RelatorioAgendamentoDAO : AgendamentoServiceFacadeDAO, IRelatorioAgendamento
    {
        Hashtable IRelatorioAgendamento.AgendaPrestador(string cnesExecutante, string id_procedimento, string cpf_Profissional, DateTime periodoInicial, DateTime periodoFinal)
        {
            Hashtable hash = new Hashtable();
            IViverMaisServiceFacade iViverMaisServiceFacade = Factory.GetInstance<IViverMaisServiceFacade>();
            System.Data.DataTable cabecalho = new System.Data.DataTable();
            cabecalho.Columns.Add("UnidadeExecutante");
            cabecalho.Columns.Add("Periodo");
            cabecalho.Columns.Add("QtdRegistros");
            //cabecalho.Columns.Add("DataFinal");
            //cabecalho.Columns.Add("Profissional");
            cabecalho.Columns.Add("Procedimento");

            //Preenche o Cabeçalho
            System.Data.DataRow row = cabecalho.NewRow();
            row["UnidadeExecutante"] = iViverMaisServiceFacade.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(cnesExecutante).NomeFantasia;
            row["Periodo"] = periodoInicial.ToString("dd/MM/yyyy") + " a " + periodoFinal.ToString("dd/MM/yyyy");
            //row["DataFinal"] = periodoFinal.ToString("dd/MM/yyyy");

            row["Procedimento"] = iViverMaisServiceFacade.BuscarPorCodigo<ViverMais.Model.Procedimento>(id_procedimento).Nome;

            //Preenche com os Dados
            IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().ListarSolicitacoesAgendaPrestador<Solicitacao>(cnesExecutante, id_procedimento, cpf_Profissional, periodoInicial, periodoFinal).OrderBy(p => p.Agenda.Data).ToList();

            row["QtdRegistros"] = solicitacoes.Count.ToString();
            cabecalho.Rows.Add(row);

            System.Data.DataTable corpo = new System.Data.DataTable();
            corpo.Columns.Add("DataAgenda");
            corpo.Columns.Add("Profissional");
            corpo.Columns.Add("CNS");
            corpo.Columns.Add("Nome");
            corpo.Columns.Add("DataNascimento");
            corpo.Columns.Add("Telefone");
            corpo.Columns.Add("Turno");
            corpo.Columns.Add("Situacao");

            foreach (Solicitacao solicitacao in solicitacoes)
            {
                System.Data.DataRow row2 = corpo.NewRow();
                //Se ele Selecionou um Profissional Específico
                if (cpf_Profissional != "0")
                    row2["Profissional"] = iViverMaisServiceFacade.BuscarPorCodigo<ViverMais.Model.Profissional>(cpf_Profissional).Nome.ToUpper();
                else
                    row2["Profissional"] = iViverMaisServiceFacade.BuscarPorCodigo<ViverMais.Model.Profissional>(solicitacao.Agenda.ID_Profissional.CPF).Nome.ToUpper();

                row2["DataAgenda"] = solicitacao.Agenda.Data.ToString("dd/MM/yyyy");
                ViverMais.Model.Paciente paciente = ViverMais.BLL.PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
                IList<CartaoSUS> cns = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(paciente.Codigo);
                long cartao = (from c in cns select long.Parse(c.Numero)).Min();
                row2["CNS"] = cartao.ToString();
                row2["Nome"] = paciente.Nome;
                row2["DataNascimento"] = paciente.DataNascimento.ToString("dd/MM/yyyy");
                Endereco endereco = ViverMais.BLL.EnderecoBLL.PesquisarPorPaciente(paciente);
                if (endereco != null)
                {
                    row2["Telefone"] = "(" + endereco.DDD + ") " + endereco.Telefone;
                }
                if (solicitacao.Agenda.Turno.ToString().Equals("M"))
                    row2["Turno"] = "Manhã";
                else if (solicitacao.Agenda.Turno.ToString().Equals("T"))
                    row2["Turno"] = "Tarde";
                else if (solicitacao.Agenda.Turno.ToString().Equals("N"))
                    row2["Turno"] = "Noite";

                //row2["Turno"] = solicitacao.Agenda.Turno == "M" ? "Manhã" : "Tarde";
                if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString())
                    row2["Situacao"] = "Autorizada";
                else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString())
                    row2["Situacao"] = "Confirmada";
                else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString())
                    row2["Situacao"] = "Desmarcada";
                corpo.Rows.Add(row2);
            }
            hash.Add("cabecalho", cabecalho);
            hash.Add("corpo", corpo);

            return hash;
        }

        Hashtable IRelatorioAgendamento.SolicitacaoAmbulatorial(string cnes, DateTime periodoInicial, DateTime periodoFinal, string codigo_usuario)
        {
            Hashtable hash = new Hashtable();

            System.Data.DataTable cabecalho = new System.Data.DataTable();
            cabecalho.Columns.Add("Periodo");
            cabecalho.Columns.Add("QtdRegistros");
            System.Data.DataRow row = cabecalho.NewRow();
            row["Periodo"] = periodoInicial.ToString("dd/MM/yyyy") + " a " + periodoFinal.ToString("dd/MM/yyyy");

            IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().ListarSolicitacoesDaUnidade<Solicitacao>(cnes, periodoInicial, periodoFinal, codigo_usuario);
            row["QtdRegistros"] = solicitacoes.Count.ToString();
            cabecalho.Rows.Add(row);


            System.Data.DataTable dados = new System.Data.DataTable();
            dados.Columns.Add("DataSolicitacao");
            dados.Columns.Add("UsuarioSolicitante");
            dados.Columns.Add("UnidadeSolicitante");
            dados.Columns.Add("CNS");
            dados.Columns.Add("NomePaciente");
            dados.Columns.Add("Procedimento");
            dados.Columns.Add("UnidadeExecutante");
            dados.Columns.Add("DataAgenda");
            dados.Columns.Add("Turno");
            dados.Columns.Add("Situacao");
            dados.Columns.Add("QtdRegistros");

            foreach (Solicitacao solicitacao in solicitacoes)
            {
                System.Data.DataRow row2 = dados.NewRow();
                ViverMais.Model.Paciente paciente = ViverMais.BLL.PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
                row2["DataSolicitacao"] = solicitacao.Data_Solicitacao.ToString("dd/MM/yyyy");
                row2["UsuarioSolicitante"] = solicitacao.UsuarioSolicitante == null ? "" : solicitacao.UsuarioSolicitante.Nome.ToUpper();
                row2["UnidadeSolicitante"] = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(solicitacao.EasSolicitante).NomeFantasia.ToUpper();
                IList<CartaoSUS> cns = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(paciente.Codigo);
                long cartao = (from c in cns select long.Parse(c.Numero)).Min();
                row2["CNS"] = cartao;
                row2["NomePaciente"] = paciente.Nome.ToUpper();
                if (solicitacao.Agenda != null)
                {
                    //Se o procedimento for Consulta Médica em Atenção Especializada (0301010072), Consulta Médica em Atenção Básica (0301010064) ou CONSULTA PRE-NATAL
                    if (solicitacao.Agenda.Procedimento.Codigo == "0301010072" || solicitacao.Agenda.Procedimento.Codigo == "0301010064" || solicitacao.Agenda.Procedimento.Codigo == "0301010110")
                        row2["Procedimento"] = solicitacao.Agenda.Procedimento.Nome + " - " + solicitacao.Agenda.Cbo.Nome;
                    else
                        row2["Procedimento"] = solicitacao.Agenda.Procedimento.Nome;
                    row2["UnidadeExecutante"] = solicitacao.Agenda.Estabelecimento.NomeFantasia.ToUpper();
                    row2["DataAgenda"] = solicitacao.Agenda.Data.ToString("dd/MM/yyyy");
                    row2["Turno"] = solicitacao.Agenda.Turno.ToUpper();
                }
                else
                {
                    row2["Procedimento"] = solicitacao.Procedimento.Nome.ToUpper();
                    row2["UnidadeExecutante"] = String.Empty;
                    row2["DataAgenda"] = String.Empty;
                    row2["Turno"] = String.Empty;
                }
                if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE).ToString())
                    row2["Situacao"] = "PENDENTE";
                else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.INDEFERIDA).ToString())
                    row2["Situacao"] = "INDEFERIDA";
                else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString())
                    row2["Situacao"] = "DESMARCADA";
                else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString())
                    row2["Situacao"] = "CONFIRMADA";
                else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString())
                    row2["Situacao"] = "AUTORIZADA";
                else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AGENDAUTOMATICO).ToString())
                    row2["Situacao"] = "AG. AUTOMATICO";
                dados.Rows.Add(row2);
            }

            hash.Add("cabecalho", cabecalho);
            hash.Add("corpo", dados);

            return hash;
        }

        Hashtable IRelatorioAgendamento.RelatorioVagas(DateTime periodoInicial, DateTime periodoFinal)
        {
            Hashtable hash = new Hashtable();


            IList<object> vagasporprocedimento = Factory.GetInstance<IAmbulatorial>().ListarVagasDisponiveis<object>(periodoInicial, periodoFinal);


            System.Data.DataTable dados = new System.Data.DataTable();
            dados.Columns.Add("Procedimento");
            dados.Columns.Add("Quantidade");
            if (vagasporprocedimento.Count != 0)
            {
                foreach (object vagas in vagasporprocedimento)
                {
                    object[] vaga = (object[])vagas;
                    System.Data.DataRow row2 = dados.NewRow();
                    row2["Procedimento"] = (string)vaga[0];
                    int quantidade = (int.Parse(vaga[1].ToString()) - int.Parse(vaga[2].ToString()));
                    if (quantidade > 0)
                    {
                        row2["Quantidade"] = quantidade;
                    }
                    else
                    {
                        row2["Quantidade"] = "--";
                    }
                    dados.Rows.Add(row2);

                }
            }

            hash.Add("corpo", dados);

            return hash;
        }

        Hashtable IRelatorioAgendamento.ExtratoPPI(string id_municipio, string tipoRelatorio)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

            Municipio municipio = iViverMais.BuscarPorCodigo<Municipio>(id_municipio);

            Hashtable hash = new Hashtable();
            System.Data.DataTable cabecalho = new System.Data.DataTable();
            cabecalho.Columns.Add("Municipio");
            cabecalho.Columns.Add("TipoExtrato");

            DataRow rowCabecalho = cabecalho.NewRow();
            rowCabecalho["Municipio"] = municipio.NomeSemUF;
            rowCabecalho["TipoExtrato"] = tipoRelatorio == "R" ? "Referência" : "Abrangência";
            cabecalho.Rows.Add(rowCabecalho);



            System.Data.DataTable dados = new System.Data.DataTable();
            dados.Columns.Add("TipoPacto");
            dados.Columns.Add("Agregado");
            dados.Columns.Add("Procedimento");
            dados.Columns.Add("CBO");
            dados.Columns.Add("ValorPactuado");
            dados.Columns.Add("ValorUtilizado");
            dados.Columns.Add("Saldo");

            if (tipoRelatorio == "R")//Referência
            {
                ViverMais.Model.Pacto pactoReferencia = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(id_municipio);
                if (pactoReferencia != null)
                {
                    if (pactoReferencia.PactosAgregados.Count != 0)
                    {
                        IList<PactoAgregadoProcedCBO> pactosReferenciaAtivos = pactoReferencia.PactosAgregados.Where(p => p.Ativo == true).ToList();
                        foreach (PactoAgregadoProcedCBO pactoProcedCBO in pactosReferenciaAtivos)
                        {
                            DataRow rowDados = dados.NewRow();

                            rowDados["Agregado"] = pactoProcedCBO.Agregado.Nome;
                            if (pactoProcedCBO.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO))
                                rowDados["TipoPacto"] = "AGREGADO";
                            else if (pactoProcedCBO.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO))
                            {
                                rowDados["TipoPacto"] = "PROCEDIMENTO";
                                rowDados["Procedimento"] = pactoProcedCBO.Procedimento.Nome;
                            }
                            else if (pactoProcedCBO.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO))
                            {
                                rowDados["TipoPacto"] = "CBO";
                                rowDados["Procedimento"] = pactoProcedCBO.Procedimento.Nome;
                                if (pactoProcedCBO.Cbos.Count == 1)//Caso tenha Pactuado somente para um CBO
                                    rowDados["CBO"] = pactoProcedCBO.Cbos.FirstOrDefault().Nome.ToUpper();
                                else if (pactoProcedCBO.Cbos.Count > 1)//Se Pactuou Por Grupo de CBO
                                {
                                    GrupoCBO grupoCBO = Factory.GetInstance<IGrupoCBO>().BuscarPorCBO<GrupoCBO>(pactoProcedCBO.Cbos.FirstOrDefault().Codigo);
                                    rowDados["CBO"] = grupoCBO.NomeGrupo.ToUpper();
                                }
                            }
                            double valor = double.Parse(pactoProcedCBO.ValorPactuado.ToString());
                            if (valor.ToString().Length <= 2)
                                valor = double.Parse(valor.ToString("000").Insert(valor.ToString("000").Length - 2, ","));
                            else
                                valor = double.Parse(valor.ToString().Insert(pactoProcedCBO.ValorPactuado.ToString().Length - 2, ","));

                            rowDados["ValorPactuado"] = valor.ToString("C", new CultureInfo("pt-BR"));

                            IList<PactoReferenciaSaldo> pactosMensal = Factory.GetInstance<IPactoReferenciaSaldo>().BuscarPorPactoAgregado<PactoReferenciaSaldo>(pactoProcedCBO.Codigo);
                            float somaSaldo = float.Parse(pactosMensal.Sum(p => p.ValorRestante).ToString());
                            float valorUtilizado = pactoProcedCBO.ValorPactuado - somaSaldo;
                            rowDados["ValorUtilizado"] = valorUtilizado;
                            rowDados["Saldo"] = somaSaldo.ToString();
                            dados.Rows.Add(rowDados);
                        }
                    }
                }
            }
            else if (tipoRelatorio == "A")
            {

            }

            return hash;
        }

        private HSSFCellStyle getEstiloCabecalhoRelatorioSolicitacao(HSSFWorkbook _doc, bool cabecalhorodape)
        {
            HSSFCellStyle estilo = _doc.CreateCellStyle();

            if (cabecalhorodape)
            {
                estilo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_40_PERCENT.index; //Cor de fundo
                estilo.FillPattern = 1; //Aplicando estilo para cor de fundo

                HSSFFont fonte = _doc.CreateFont();
                fonte.Color = HSSFColor.BLACK.index;
                fonte.Boldweight = 14;
                fonte.FontHeightInPoints = 14;
                fonte.FontName = "Times New Roman";
                estilo.SetFont(fonte);
            }

            estilo.Alignment = HSSFCellStyle.ALIGN_CENTER; //Alinhando horizontalmente
            estilo.BorderBottom = HSSFCellStyle.BORDER_MEDIUM; //Borda de baixo
            estilo.BottomBorderColor = HSSFColor.BLACK.index; //Cor da borda de baixo
            estilo.BorderLeft = HSSFCellStyle.BORDER_MEDIUM; //Borda da esquerda
            estilo.LeftBorderColor = HSSFColor.BLACK.index;//Cor da borda da esquerda
            estilo.BorderRight = HSSFCellStyle.BORDER_MEDIUM; //Borda da direita
            estilo.RightBorderColor = HSSFColor.BLACK.index;//Cor da borda da direita
            estilo.BorderTop = HSSFCellStyle.BORDER_MEDIUM; //Borda de cima
            estilo.TopBorderColor = HSSFColor.BLACK.index;//Cor da borda de cima

            return estilo;
        }

        MemoryStream IRelatorioAgendamento.RelatorioSolicitacoesDesmarcadas()
        {
            HSSFWorkbook documento = new HSSFWorkbook();
            HSSFSheet planilha = documento.CreateSheet("Relatorio de Desmarcações");

            int posLinha = 1; //Linha inicial do documento

            int inicioColumnOperador = 1, fimColumnOperador = 3;
            int inicioColumnUnidade = fimColumnOperador + 1, fimColumnUnidade = inicioColumnUnidade + 1;
            int inicioColumnData = fimColumnUnidade + 1, fimColumnData = inicioColumnData + 1;
            int inicioColumnNomePaciente = fimColumnData + 1, fimColumnNomePaciente = inicioColumnNomePaciente + 3;
            int inicioColumnJustificativa = fimColumnNomePaciente + 1, fimColumnJustificativa = inicioColumnJustificativa + 4;

            HSSFRow linha = planilha.CreateRow(posLinha);
            for (int i = inicioColumnOperador; i <= fimColumnJustificativa; i++)
                linha.CreateCell(i).CellStyle = getEstiloCabecalhoRelatorioSolicitacao(documento, true);

            //Mesclando celulas das colunas do cabeçalho
            planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnOperador, fimColumnOperador));
            planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnUnidade, fimColumnUnidade));
            planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnData, fimColumnData));
            planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnNomePaciente, fimColumnNomePaciente));
            planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnJustificativa, fimColumnJustificativa));

            //Setando valores para o cabeçalho
            linha.GetCell(inicioColumnOperador).SetCellValue("Operador");
            linha.GetCell(inicioColumnUnidade).SetCellValue("Unidade");
            linha.GetCell(inicioColumnData).SetCellValue("Data");
            linha.GetCell(inicioColumnNomePaciente).SetCellValue("Paciente");
            linha.GetCell(inicioColumnJustificativa).SetCellValue("Justificativa");

            posLinha++;

            IList<ViverMais.Model.LogAgendamento> logs = Session.CreateQuery("from ViverMais.Model.LogAgendamento log Where log.Evento = 13 and log.Valor like '%ID_SOLICITACAO:%'").List<ViverMais.Model.LogAgendamento>().OrderBy(p => p.Data).ToList();
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

            for (int j = 0; j < logs.Count; j++)
            {
                ViverMais.Model.LogAgendamento log = logs[j];

                int co_solicitacao = int.Parse(log.Valor.Split(':')[1].ToString());
                Solicitacao solicitacao = iViverMais.BuscarPorCodigo<Solicitacao>(co_solicitacao);
                if (solicitacao != null)
                {
                    ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
                    if (paciente != null)
                    {
                        linha = planilha.CreateRow(posLinha);
                        for (int i = inicioColumnOperador; i <= fimColumnJustificativa; i++)
                            linha.CreateCell(i).CellStyle = getEstiloCabecalhoRelatorioSolicitacao(documento, true);

                        planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnOperador, fimColumnOperador));
                        planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnUnidade, fimColumnUnidade));
                        planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnData, fimColumnData));
                        planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnNomePaciente, fimColumnNomePaciente));
                        planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnJustificativa, fimColumnJustificativa));

                        //Setando valores do relatório
                        linha.GetCell(inicioColumnOperador).SetCellValue(iViverMais.BuscarPorCodigo<Usuario>(log.CodigoUsuario).Nome);
                        linha.GetCell(inicioColumnUnidade).SetCellValue(iViverMais.BuscarPorCodigo<Usuario>(log.CodigoUsuario).UnidadeToString);
                        linha.GetCell(inicioColumnData).SetCellValue(log.Data.ToString("dd/MM/yyyy"));
                        linha.GetCell(inicioColumnNomePaciente).SetCellValue(paciente.Nome);
                        linha.GetCell(inicioColumnJustificativa).SetCellValue(solicitacao.JustificativaDesmarcar);
                    }
                }
                posLinha++;
            }
            //foreach (ViverMais.Model.LogAgendamento log in logs)
            //{
            //    //int co_solicitacao = int.Parse(log.Valor.Split(':')[1].ToString());
            //    //Solicitacao solicitacao = iViverMais.BuscarPorCodigo<Solicitacao>(co_solicitacao);
            //    //if (solicitacao != null)
            //    //{
            //    //    ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
            //    //    if (paciente != null)
            //    //    {
            //    //        linha = planilha.CreateRow(posLinha);
            //    //        for (int i = inicioColumnOperador; i <= fimColumnJustificativa; i++)
            //    //            linha.CreateCell(i).CellStyle = getEstiloCabecalhoRelatorioSolicitacao(documento, true);

            //    //        planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnOperador, fimColumnOperador));
            //    //        planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnUnidade, fimColumnUnidade));
            //    //        planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnData, fimColumnData));
            //    //        planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnNomePaciente, fimColumnNomePaciente));
            //    //        planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnJustificativa, fimColumnJustificativa));

            //    //        //Setando valores do relatório
            //    //        linha.GetCell(inicioColumnOperador).SetCellValue(iViverMais.BuscarPorCodigo<Usuario>(log.CodigoUsuario).Nome);
            //    //        linha.GetCell(inicioColumnUnidade).SetCellValue(iViverMais.BuscarPorCodigo<Usuario>(log.CodigoUsuario).UnidadeToString);
            //    //        linha.GetCell(inicioColumnData).SetCellValue(log.Data.ToString("dd/MM/yyyy"));
            //    //        linha.GetCell(inicioColumnNomePaciente).SetCellValue(paciente.Nome);
            //    //        linha.GetCell(inicioColumnJustificativa).SetCellValue(solicitacao.JustificativaDesmarcar);
            //    //    }
            //    //}
            //    //posLinha++;
            //}

            //Criando rodapé
            linha = planilha.CreateRow(posLinha);
            for (int i = inicioColumnOperador; i <= fimColumnJustificativa; i++)
                linha.CreateCell(i).CellStyle = getEstiloCabecalhoRelatorioSolicitacao(documento, true);

            //Setando valor do rodapé
            planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnOperador, fimColumnJustificativa));
            linha.GetCell(inicioColumnOperador).SetCellValue("Relatório referente ao dia " + DateTime.Now.ToString("dd/MM/yyyy") + ".");

            MemoryStream ms = new MemoryStream();
            documento.Write(ms);
            return ms;
        }


        MemoryStream IRelatorioAgendamento.Log(DateTime dataInicial, DateTime dataFinal, string cartao_sus)
        {
            HSSFWorkbook documento = new HSSFWorkbook();
            HSSFSheet planilha = documento.CreateSheet("Relatorio de Log");

            int posLinha = 1; //Linha inicial do documento

            //Criando Cabeçalho com colunas 'A' e 'B'

            //int xMinColuna = 1, xMaxColuna = 3;
            ////int yMinColuna = xMaxColuna + 1;
            //int yMaxColuna = yMinColuna + 4;


            int inicioColumnEvento = 1, fimColumnEvento = 3;
            int inicioColumnData = fimColumnEvento + 1, fimColumnData = inicioColumnData + 1;
            int inicioColumnNomeUsuario = fimColumnData + 1, fimColumnNomeUsuario = inicioColumnNomeUsuario + 3;

            HSSFRow linha = planilha.CreateRow(posLinha);
            for (int i = inicioColumnEvento; i <= fimColumnNomeUsuario; i++)
                linha.CreateCell(i).CellStyle = getEstiloCabecalhoRelatorioSolicitacao(documento, true);

            //Mesclando celulas das colunas do cabeçalho
            planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnEvento, fimColumnEvento));
            planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnData, fimColumnData));
            planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnNomeUsuario, fimColumnNomeUsuario));

            //Setando valores para o cabeçalho
            linha.GetCell(inicioColumnEvento).SetCellValue("Evento");
            linha.GetCell(inicioColumnData).SetCellValue("Data");
            linha.GetCell(inicioColumnNomeUsuario).SetCellValue("Nome Usuário");

            posLinha++;

            ViverMais.Model.Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCartaoSUS<Usuario>(cartao_sus).FirstOrDefault();
            if (usuario != null)
            {
                IList<ViverMais.Model.LogAgendamento> logs = Session.CreateQuery("from ViverMais.Model.LogAgendamento log Where log.CodigoUsuario=" + usuario.Codigo + " and log.Data between TO_DATE('" + dataInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI') and TO_DATE('" + dataFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')").List<ViverMais.Model.LogAgendamento>().OrderBy(p => p.Data).ToList();

                foreach (ViverMais.Model.LogAgendamento log in logs)
                {
                    linha = planilha.CreateRow(posLinha);
                    for (int i = inicioColumnEvento; i <= fimColumnNomeUsuario; i++)
                        linha.CreateCell(i).CellStyle = getEstiloCabecalhoRelatorioSolicitacao(documento, false);

                    planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnEvento, fimColumnEvento));
                    planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnData, fimColumnData));
                    planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnNomeUsuario, fimColumnNomeUsuario));

                    linha.GetCell(inicioColumnEvento).SetCellValue(Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.EventoAgendamento>(log.Evento).Nome);
                    linha.GetCell(inicioColumnData).SetCellValue(log.Data.ToString("dd/MM/yyyy HH:mm:ss"));
                    linha.GetCell(inicioColumnNomeUsuario).SetCellValue(usuario.Nome);

                    posLinha++;
                }

                //Criando rodapé
                linha = planilha.CreateRow(posLinha);
                for (int i = inicioColumnEvento; i <= fimColumnNomeUsuario; i++)
                    linha.CreateCell(i).CellStyle = getEstiloCabecalhoRelatorioSolicitacao(documento, true);

                //Setando valor do rodapé
                planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, inicioColumnEvento, fimColumnNomeUsuario));
                linha.GetCell(inicioColumnEvento).SetCellValue("Relatório referente ao dia " + DateTime.Now.ToString("dd/MM/yyyy") + ".");
            }
            MemoryStream ms = new MemoryStream();
            documento.Write(ms);
            return ms;
        }

        Hashtable IRelatorioAgendamento.RelatorioProducaoMedicoRegulador(DateTime periodoInicial, DateTime periodoFinal, int codigo_usuario, int id_perfil)
        {
            Hashtable hash = new Hashtable();

            System.Data.DataTable cabecalho = new System.Data.DataTable();
            cabecalho.Columns.Add("Periodo");
            cabecalho.Columns.Add("QtdRegistros");

            System.Data.DataRow row = cabecalho.NewRow();
            row["Periodo"] = periodoInicial.ToString("dd/MM/yyyy") + " a " + periodoFinal.ToString("dd/MM/yyyy");

            string hql = "Select producao.Usuario.Nome, count(producao) from ViverMais.Model.ProducaoMedicoRegulador producao, ViverMais.Model.Perfil AS p WHERE p IN ELEMENTS (producao.Usuario.Perfis)";
            hql += " and producao.DataAtualizacao between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')";
            hql += " and TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";

            if (codigo_usuario != 0)
            {
                hql += " and producao.Usuario.Codigo = " + codigo_usuario;
            }
            hql += " AND p IN (FROM ViverMais.Model.Perfil AS p2 WHERE p2.Codigo = " + id_perfil + ")";
            hql += " group by producao.Usuario.Nome";
            hql += " order by producao.Usuario.Nome";


            IList<object> producao = Session.CreateQuery(hql).List<object>();
            row["QtdRegistros"] = producao.Count;
            cabecalho.Rows.Add(row);
            System.Data.DataTable dados = new System.Data.DataTable();
            dados.Columns.Add("Usuario");
            dados.Columns.Add("Qtd");
            foreach (object produc in producao)
            {
                System.Data.DataRow row2 = dados.NewRow();
                object[] registro = (object[])produc;
                row2["Usuario"] = ((string)registro[0]).ToUpper();
                row2["Qtd"] = int.Parse(registro[1].ToString()).ToString();
                dados.Rows.Add(row2);
            }
            hash.Add("cabecalho", cabecalho);
            hash.Add("corpo", dados);
            return hash;
        }

        Hashtable IRelatorioAgendamento.RelatorioAgendaMontadaPublicada<E, P, X, C>(int competencia, E estabelecimentoParameter, P profissionalParameter, X procedimentoParameter, C cboParameter)
        {
            Hashtable hash = new Hashtable();

            ViverMais.Model.EstabelecimentoSaude estabelecimento = (ViverMais.Model.EstabelecimentoSaude)(object)estabelecimentoParameter;
            ViverMais.Model.Procedimento procedimento = (ViverMais.Model.Procedimento)(object)procedimentoParameter;
            ViverMais.Model.Profissional profissional = (ViverMais.Model.Profissional)(object)profissionalParameter;
            CBO cbo = (CBO)(object)cboParameter;

            System.Data.DataTable cabecalho = new System.Data.DataTable();
            cabecalho.Columns.Add("Competencia");
            cabecalho.Columns.Add("Estabelecimento");
            cabecalho.Columns.Add("Procedimento");
            cabecalho.Columns.Add("CBO");
            cabecalho.Columns.Add("Profissional");

            DataRow row = cabecalho.NewRow();
            row["Competencia"] = competencia.ToString();
            row["Estabelecimento"] = estabelecimento != null ? estabelecimento.NomeFantasia : "Não Informado";
            row["Procedimento"] = procedimento != null ? procedimento.Nome : "Não Informado";
            row["CBO"] = cbo != null ? cbo.Nome : "Não Informado";
            row["Profissional"] = profissional != null ? profissional.Nome : "Não Informado";
            cabecalho.Rows.Add(row);

            //System.Data.DataTable dados = new System.Data.DataTable();
            //dados.Columns.Add("Unidade");
            //dados.Columns.Add("Procedimento");
            //dados.Columns.Add("Especialidade");
            //dados.Columns.Add("Profissional");
            //dados.Columns.Add("QtdMontada");
            //dados.Columns.Add("QtdPublicada");

            System.Data.DataTable dados = Factory.GetInstance<IAmbulatorial>().RelatorioAgendaMontadaPublicada<ViverMais.Model.EstabelecimentoSaude, ViverMais.Model.Profissional, ViverMais.Model.Procedimento, CBO>(competencia, estabelecimento, profissional, procedimento, cbo);
            if (dados != null)
            {
                //foreach (object itens in relatorioAgendas)
                //{
                //    DataRow row2 = dados.NewRow();
                //    object[] item = (object[])itens;
                //    int quantidadeDisponibilizada = int.Parse(item[4].ToString());
                //    int quantidadePublicada = int.Parse(item[5].ToString());
                //    row2["Unidade"] = item[0].ToString();
                //    row2["Procedimento"] = item[1].ToString();
                //    row2["Especialidade"] = item[2].ToString();
                //    row2["Profissional"] = item[3].ToString();
                //    row2["QtdMontada"] = quantidadeDisponibilizada;
                //    row2["QtdPublicada"] = quantidadePublicada;
                //    dados.Rows.Add(row2);
                //}               

                //IList<ViverMais.Model.Agenda> agendas = (IList<ViverMais.Model.Agenda>)item[0];

                //OracleParameterCollection parameters = (OracleParameterCollection)relatorioAgendas;
                //foreach (OracleParameter itens in parameters)
                //{
                //    //itens.
                //    //object[] item = (object[])itens;

                //    //IList<ViverMais.Model.Agenda> agendas = (IList<ViverMais.Model.Agenda>)item[0];
                //    //int quantidadeDisponibilizada = int.Parse(item[4].ToString());
                //    //int quantidadePublicada = int.Parse(item[5].ToString());
                //    //DataRow row2 = dados.NewRow();
                //    //row2["Unidade"] = item[0].ToString();
                //    //row2["Procedimento"] = item[1].ToString();
                //    //row2["Especialidade"] = item[2].ToString();
                //    //row2["Profissional"] = item[3].ToString();
                //    //row2["QtdMontada"] = quantidadeDisponibilizada;
                //    //row2["QtdPublicada"] = quantidadePublicada;
                //    //dados.Rows.Add(row2);
                //}
            }

            hash.Add("cabecalho", cabecalho);
            hash.Add("dados", dados);
            return hash;
        }

        T IRelatorioAgendamento.GerarBPAAPAC<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite)
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IRegistro iRegistro = Factory.GetInstance<IRegistro>();
            IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().ListarSolicitacoesConfirmadasBPAApac<Solicitacao>(competencia, co_unidade, datainicio, datalimite);
            ArquivoAPAC arquivoAPAC = new ArquivoAPAC();
            arquivoAPAC.Competencia = competencia;
            arquivoAPAC.Unidade = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(co_unidade);
            foreach (Solicitacao solicitacao in solicitacoes)
            {
                APAC apac = new APAC();
                ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
                CartaoSUS cartaoSUSPaciente = CartaoSUSBLL.ListarPorPaciente(paciente).Min();
                apac.NumeroAPAC = solicitacao.Identificador;
                apac.DataValidadeInicial = solicitacao.Agenda.Data;
                Parametros parametros = iViverMais.ListarTodos<Parametros>().FirstOrDefault<Parametros>();
                apac.DataValidadeFinal = solicitacao.Agenda.Data.AddDays((double)parametros.Validade_Codigo);
                apac.EnderecoPaciente = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
                apac.CidCausasAssociadas = solicitacao.CidExecutante;
                apac.CnesUnidadeSolicitante = solicitacao.EasSolicitante;
                apac.Uf = solicitacao.Agenda.Estabelecimento.MunicipioGestor.UF;
                if (solicitacao.Agenda.ID_Profissional.CartaoSUS != null)
                    apac.CnsMedicoResponsavel = solicitacao.Agenda.ID_Profissional.CartaoSUS;
                else
                {
                    ViverMais.Model.Paciente paciente2 = PacienteBLL.PesquisarPorCPF(solicitacao.Agenda.ID_Profissional.CPF);
                    if (paciente2 != null)
                        apac.CnsMedicoResponsavel = CartaoSUSBLL.ListarPorPaciente(paciente2).Min<CartaoSUS>().Numero;
                    else
                        apac.CnsMedicoResponsavel = "               ";
                }
                apac.CpfProfissionalExecutante = solicitacao.Agenda.ID_Profissional.CPF;
                apac.NomeProfissionalExecutante = solicitacao.Agenda.ID_Profissional.Nome;
                apac.DescritivoProcedimentosRealizados.Add(new DescritivoProcedimentosRealizados(solicitacao.Agenda.Procedimento, solicitacao.Agenda.Cbo, solicitacao.CidExecutante, null, solicitacao.Qtd));
                apac.MotivoSaida = Convert.ToInt32(APAC.MoticoDeSaida.ALTA_CURADO);
                apac.DataAltaOuObito = solicitacao.Data_Confirmacao.Value;
                ViverMais.Model.Paciente paciente3 = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(solicitacao.UsuarioRegulador.CartaoSUS);
                Documento documento = DocumentoBLL.PesqusiarPorPaciente("02", paciente3);
                apac.CpfProfissionalAutorizador = documento.Numero;
                apac.NomeProfissionalAutorizador = paciente3.Nome;
                apac.IndicativoContinuacaoAPAC = Convert.ToInt32(APAC.TipoDeAPAC.INICIAL);
                apac.Paciente = paciente;
                apac.CartaoSUSPaciente = cartaoSUSPaciente;
                apac.CnsAutorizadorResponsavel = solicitacao.UsuarioRegulador.CartaoSUS;
                apac.NumeroProntuario = string.Empty;
                apac.CnesUnidadeSolicitante = solicitacao.EasSolicitante;
                apac.DataSolicitacao = solicitacao.Data_Solicitacao;
                apac.DataAutorizacao = solicitacao.Data_Confirmacao.Value;
                apac.CaraterAtendimento = Convert.ToInt32(APAC.CaraterDoAtendimento.ELETIVO).ToString("00");
                apac.NumeroApacAnterior = "0000000000000";
                apac.UnidadePrestadora = solicitacao.Agenda.Estabelecimento;
                apac.NumeroProntuario = solicitacao.Prontuario.ToString();
                apac.DataProcessamentoAPAC = solicitacao.Data_Confirmacao.Value;
                apac.TipoAtendimento = Convert.ToInt32(APAC.CaraterDoAtendimento.ELETIVO).ToString("00");
                apac.TipoAPAC = Convert.ToInt32(APAC.TipoDeAPAC.UNICA);
                if (apac.Paciente.Idade >= 18)
                    apac.NomeResponsavel = apac.Paciente.Nome;
                else
                    apac.NomeResponsavel = apac.Paciente.NomeMae;
                arquivoAPAC.Apacs.Add(apac);
            }
            return (T)(object)arquivoAPAC;
        }

        T IRelatorioAgendamento.GerarBPAI<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite)
        {
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            ISolicitacao iSolicitacao = Factory.GetInstance<ISolicitacao>();
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

            IRegistro iRegistro = Factory.GetInstance<IRegistro>();
            IList<ViverMais.Model.Solicitacao> solicitacoes = iSolicitacao.ListarSolicitacoesConfirmadas<Solicitacao>(competencia, co_unidade, datainicio, datalimite, Registro.BPA_INDIVIDUALIZADO.ToString("00")).Where(P => P.Procedimento.Codigo != "0204030030" && P.Procedimento.Codigo != "0204030188").ToList(); //Tratamento para não trazer os procedimentos de Mamografia


            ArquivoBPA arquivoBPA = new ArquivoBPA();
            arquivoBPA.Competencia = competencia;
            arquivoBPA.Unidade = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(co_unidade);
            arquivoBPA.Tipo = ViverMais.Model.BPA.INDIVIDUALIZADO;
            for (int i = 0; i < solicitacoes.Count; i++)
            {
                Solicitacao solicitacao = solicitacoes[i];
                BpaIndividualizado bpaIndividualizado = new BpaIndividualizado();
                ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
                ViverMais.Model.Profissional profissional = iViverMais.BuscarPorCodigo<ViverMais.Model.Profissional>(solicitacao.Agenda.ID_Profissional.CPF);

                bpaIndividualizado.CnsMedico = !string.IsNullOrEmpty(profissional.CartaoSUS) ? profissional.CartaoSUS : "               ";
                bpaIndividualizado.Cbo = solicitacao.Agenda.Cbo;
                bpaIndividualizado.DataAtendimento = solicitacao.Agenda.Data;
                bpaIndividualizado.Procedimento = solicitacao.Agenda.Procedimento;
                bpaIndividualizado.CnsPaciente = CartaoSUSBLL.ListarPorPaciente(paciente).Last().Numero;
                bpaIndividualizado.Cid = solicitacao.CidExecutante == null ? "    " : solicitacao.CidExecutante.Codigo;
                bpaIndividualizado.Quantidade = solicitacao.Qtd;
                bpaIndividualizado.NumeroAutorizacao = solicitacao.Identificador.ToString();
                Endereco endereco = EnderecoBLL.PesquisarPorPaciente(paciente);

                if (endereco != null && endereco.Municipio != null)
                    bpaIndividualizado.CodigoMunicipioResidencia = endereco.Municipio.Codigo;
                else
                    bpaIndividualizado.CodigoMunicipioResidencia = Municipio.SALVADOR;

                bpaIndividualizado.Paciente = paciente;

                arquivoBPA.Bpas.Add(bpaIndividualizado);
            }
            return (T)(object)arquivoBPA;
        }

        T IRelatorioAgendamento.GerarBPAC<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite)
        {
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            ISolicitacao iSolicitacao = Factory.GetInstance<ISolicitacao>();
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

            IRegistro iRegistro = Factory.GetInstance<IRegistro>();

            ArquivoBPA arquivoBPA = new ArquivoBPA();
            arquivoBPA.Competencia = competencia;
            arquivoBPA.Unidade = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(co_unidade);
            arquivoBPA.Tipo = ViverMais.Model.BPA.CONSOLIDADO;

            IList<object> registros = iSolicitacao.ListarRegistroParaBPAConsolidado<object>(competencia, co_unidade, datainicio, datalimite);
            foreach (object registro in registros)
            {
                object[] arrayRegistros = (object[])registro;
                string co_cbo = arrayRegistros[0].ToString();
                string co_procedimento = arrayRegistros[1].ToString();
                string idade = arrayRegistros[2].ToString();
                string quantidade = arrayRegistros[3].ToString();

                BpaConsolidado bpaConsolidado = new BpaConsolidado();
                bpaConsolidado.Cbo = iViverMais.BuscarPorCodigo<CBO>(co_cbo);
                bpaConsolidado.Procedimento = iViverMais.BuscarPorCodigo<ViverMais.Model.Procedimento>(co_procedimento);
                bpaConsolidado.Idade = int.Parse(idade);
                bpaConsolidado.Quantidade = int.Parse(quantidade);
                arquivoBPA.Bpas.Add(bpaConsolidado);
            }
            return (T)(object)arquivoBPA;
        }

        Hashtable IRelatorioAgendamento.SolicitacaoDetalhada(string tipounidade, string cnes, string tipomunicipio, string municipio, DateTime periodoInicial, DateTime periodoFinal, string tipoProcedimento, string procedimento, string especialidade, string status, string paciente)
        {
            Hashtable hash = new Hashtable();
            System.Data.DataTable cabecalho = new System.Data.DataTable();
            cabecalho.Columns.Add("Periodo");
            cabecalho.Columns.Add("DataGeracao");
            System.Data.DataRow row = cabecalho.NewRow();
            row["Periodo"] = periodoInicial.ToString("dd/MM/yyyy") + " a " + periodoFinal.ToString("dd/MM/yyyy");
            row["DataGeracao"] = DateTime.Now.ToString("dd/MM/yyyy");

            IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().ListaSolicitacoesDetalhadas<Solicitacao>(cnes, tipounidade, periodoInicial, periodoFinal, tipomunicipio, municipio, tipoProcedimento, procedimento, especialidade, status, paciente);

            cabecalho.Rows.Add(row);

            System.Data.DataTable dados = new System.Data.DataTable();
            dados.Columns.Add("DataSolicitacao");
            dados.Columns.Add("NomePaciente");
            dados.Columns.Add("CNS");
            dados.Columns.Add("Municipio");
            dados.Columns.Add("Procedimento");
            dados.Columns.Add("Especialidade");
            dados.Columns.Add("UnidadeSolicitante");
            dados.Columns.Add("UnidadeExecutante");
            dados.Columns.Add("DataConfirmacao");
            dados.Columns.Add("Status");
            dados.Columns.Add("Prioridade");
            if (solicitacoes.Count != 0)
            {
                foreach (Solicitacao solicitacao in solicitacoes)
                {
                    System.Data.DataRow row2 = dados.NewRow();
                    ViverMais.Model.Paciente pac = ViverMais.BLL.PacienteBLL.Pesquisar(solicitacao.ID_Paciente);
                    row2["DataSolicitacao"] = solicitacao.Data_Solicitacao.ToString("dd/MM/yyyy");
                    IList<CartaoSUS> cns = Factory.GetInstance<IPaciente>().ListarCartoesSUS<CartaoSUS>(pac.Codigo);
                    long cartao = (from c in cns select long.Parse(c.Numero)).Min();
                    row2["NomePaciente"] = pac.Nome.ToUpper();
                    row2["CNS"] = cartao;
                    row2["Municipio"] = solicitacao.UsuarioSolicitante.Unidade.MunicipioGestor.Nome;
                    row2["Procedimento"] = solicitacao.Procedimento.Nome;
                    //row2["Especialidade"] = solicitacao.Agenda.Cbo.Nome;
                    //ViverMais.Model.Procedimento proced = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<ViverMais.Model.Procedimento>(solicitacao.Procedimento.c);
                    row2["Procedimento"] = solicitacao.Procedimento.Nome;
                    if (solicitacao.Agenda != null)
                    {
                        row2["Especialidade"] = solicitacao.Agenda.Cbo.Nome;
                        row2["UnidadeExecutante"] = solicitacao.Agenda.Estabelecimento.NomeFantasia;
                    }
                    else
                    {
                        row2["Especialidade"] = "-";
                        row2["UnidadeExecutante"] = "-";
                    }

                    row2["UnidadeSolicitante"] = solicitacao.UsuarioSolicitante.Unidade.NomeFantasia;
                    row2["DataConfirmacao"] = solicitacao.Data_Confirmacao;
                    if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE).ToString())
                        row2["Status"] = "PENDENTE";
                    else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.INDEFERIDA).ToString())
                        row2["Status"] = "INDEFERIDA";
                    else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString())
                        row2["Status"] = "DESMARCADA";
                    else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString())
                        row2["Status"] = "CONFIRMADA";
                    else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString())
                        row2["Status"] = "AUTORIZADA";
                    else if (solicitacao.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AGENDAUTOMATICO).ToString())
                        row2["Status"] = "AG. AUTOMATICO";
                    if (solicitacao.Prioridade == "0")
                        row2["Prioridade"] = "~/Agendamento/img/prioridade-vermelho.png";
                    else if (solicitacao.Prioridade == "1")
                        row2["Prioridade"] = "~/Agendamento/img/prioridade-amarelo.png";
                    else if (solicitacao.Prioridade == "2")
                        row2["Prioridade"] = "~/Agendamento/img/prioridade-verde.png";
                    else if (solicitacao.Prioridade == "3")
                        row2["Prioridade"] = "~/Agendamento/img/prioridade-azul.png";
                    else if (solicitacao.Prioridade == "4")
                        row2["Prioridade"] = "~/Agendamento/img/prioridade-branco.png";
                    dados.Rows.Add(row2);
                }

                hash.Add("cabecalho", cabecalho);
                hash.Add("corpo", dados);
            }

            return hash;

        }

        Hashtable IRelatorioAgendamento.ListarQuantitativoDeProducao(string cnes, DateTime periodoInicial, DateTime periodoFinal, string tipomunicipio, string municipio, string tipoprocedimento, string procedimento, string especialidade)
        {
            Hashtable hash = new Hashtable();

            System.Data.DataTable cabecalho = new System.Data.DataTable();
            cabecalho.Columns.Add("Periodo");
            cabecalho.Columns.Add("DataGeracao");
            System.Data.DataRow row = cabecalho.NewRow();
            row["Periodo"] = periodoInicial.ToString("dd/MM/yyyy") + " a " + periodoFinal.ToString("dd/MM/yyyy");
            row["DataGeracao"] = DateTime.Now.ToString("dd/MM/yyyy");
            IList solicitacoes = Factory.GetInstance<ISolicitacao>().ListarQuantitativoDeProducao(cnes, periodoInicial, periodoFinal, tipomunicipio, municipio, tipoprocedimento, procedimento, especialidade);

            cabecalho.Rows.Add(row);


            System.Data.DataTable dados = new System.Data.DataTable();
            dados.Columns.Add("UnidadeExecutante");
            dados.Columns.Add("Procedimento");
            dados.Columns.Add("Especialidade");
            dados.Columns.Add("QuantidadePublicada");
            dados.Columns.Add("QuantidadeAgendado");
            dados.Columns.Add("QuantidadeConfirmado");
            dados.Columns.Add("QuantidadeNaoConfirmado");

            for (int i = 0; i < solicitacoes.Count; i++)
            {
                object[] itens = (object[])solicitacoes[i];
                System.Data.DataRow row2 = dados.NewRow();
                row2["UnidadeExecutante"] = itens[0].ToString();
                row2["Procedimento"] = itens[2].ToString();
                row2["Especialidade"] = itens[4].ToString();
                row2["QuantidadePublicada"] = itens[6].ToString();
                row2["QuantidadeAgendado"] = itens[7].ToString();
                IList confirmadas = Factory.GetInstance<ISolicitacao>().ListarQuantitativoDeSolicitacaoConfirmada(itens[1].ToString(), periodoInicial, periodoFinal, tipomunicipio, municipio, tipoprocedimento, itens[3].ToString(), itens[4].ToString());
                if (confirmadas.Count != 0)
                {
                    object[] itemconf = (object[])confirmadas[0];

                    row2["QuantidadeConfirmado"] = itemconf[3].ToString();
                    row2["QuantidadeNaoConfirmado"] = (int.Parse(itens[7].ToString()) - int.Parse(itemconf[3].ToString()));
                }
                else
                {
                    row2["QuantidadeConfirmado"] = "0";
                    row2["QuantidadeNaoConfirmado"] = itens[7].ToString();

                }
                dados.Rows.Add(row2);

            }


            hash.Add("cabecalho", cabecalho);
            hash.Add("corpo", dados);

            return hash;

        }

        Hashtable IRelatorioAgendamento.ListarQuantitativoDeSolicitacao(string cnes, DateTime periodoInicial, DateTime periodoFinal, string tipomunicipio, string municipio, string tipoprocedimento, string procedimento, string especialidade)
        {
            Hashtable hash = new Hashtable();

            System.Data.DataTable cabecalho = new System.Data.DataTable();
            cabecalho.Columns.Add("Periodo");
            cabecalho.Columns.Add("DataGeracao");
            System.Data.DataRow row = cabecalho.NewRow();
            row["Periodo"] = periodoInicial.ToString("dd/MM/yyyy") + " a " + periodoFinal.ToString("dd/MM/yyyy");
            row["DataGeracao"] = DateTime.Now.ToString("dd/MM/yyyy");
            IList solicitacoes = Factory.GetInstance<ISolicitacao>().ListarQuantitativoDeSolicitacao(cnes, periodoInicial, periodoFinal, tipomunicipio, municipio, tipoprocedimento, procedimento, especialidade);


            cabecalho.Rows.Add(row);


            System.Data.DataTable dados = new System.Data.DataTable();
            dados.Columns.Add("UnidadeSolicitante");
            dados.Columns.Add("Municipio");
            dados.Columns.Add("Procedimento");
            dados.Columns.Add("Especialidade");
            dados.Columns.Add("Quantidade");

            foreach (object[] solicitacao in solicitacoes)
            {
                System.Data.DataRow row2 = dados.NewRow();
                row2["UnidadeSolicitante"] = solicitacao[0].ToString();
                row2["Municipio"] = solicitacao[1].ToString();
                row2["Procedimento"] = solicitacao[2].ToString();
                if (solicitacao[3] != null)
                {
                    row2["Especialidade"] = solicitacao[3].ToString();
                }
                else
                {
                    row2["Especialidade"] = "-";

                }
                row2["Quantidade"] = solicitacao[4].ToString();
                dados.Rows.Add(row2);

            }

            hash.Add("cabecalho", cabecalho);
            hash.Add("corpo", dados);

            return hash;

        }

    }
}
