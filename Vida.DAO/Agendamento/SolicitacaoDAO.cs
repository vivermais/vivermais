﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.DAO.FormatoDataOracle;
using ViverMais.ServiceFacade.ServiceFacades;
using Oracle.DataAccess.Client;
using System.IO;
using NHibernate.Mapping;
using ViverMais.DAO.Helpers;


namespace ViverMais.DAO.Agendamento
{
    public class SolicitacaoDAO : AgendamentoServiceFacadeDAO, ISolicitacao
    {
        #region ISolicitacao Members

        Random randomico;

        public SolicitacaoDAO()
        {
            randomico = new Random();
        }

        bool ISolicitacao.RestricaoPactoAbrangencia(string co_municipio, string co_procedimentoSelecionado, string co_cbo)
        {
            //Retorna os Grupos de Abrangencia que o Municipio Faz Parte
            IList<GrupoAbrangencia> grupos = Factory.GetInstance<IGrupoAbrangencia>().ListarGrupoPorMunicipio<GrupoAbrangencia>(co_municipio);
            IPactoAbrangenciaAgregado iPactoAbrangenciaAgregado = Factory.GetInstance<IPactoAbrangenciaAgregado>();
            //Percorre Todos Os Grupos
            foreach (GrupoAbrangencia grupo in grupos)
            {
                //Lista Os PactosAbrangencias desse Grupo
                IList<PactoAbrangencia> pactosAbrangencia = Factory.GetInstance<IPactoAbrangencia>().BuscarPactoAbrangenciaPorGrupoAbrangencia<PactoAbrangencia>(grupo.Codigo);
                foreach (PactoAbrangencia pactoAbrangencia in pactosAbrangencia)
                {
                    PactoAbrangenciaAgregado pactoAbrangenciaAgregado = null;
                    if (pactoAbrangencia.PactoAbrangenciaAgregado.Count != 0)
                    {
                        //    pacto.PactosAgregados.Where(p => p.Cbos.Count != 0
                        //&& p.Cbos.Select(t => t.Codigo).ToList().Contains(rbtnEspecialidade.SelectedValue)
                        //&& p.Procedimento != null
                        //&& p.Procedimento.Codigo == ddlProcedimento.SelectedValue
                        //&& p.Ativo == true
                        //&& p.Ano == DateTime.Now.Year
                        //&& p.TipoPacto == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO)).ToList().Count == 0)

                        if (!string.IsNullOrEmpty(co_cbo))//Se ele informou o CBO
                        {
                            //Verifico se existe o pacto para o procedimento e CBO informado
                            if (pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Cbo != null
                                && p.Cbo.Codigo == co_cbo
                                && p.Procedimento != null
                                && p.Procedimento.Codigo == co_procedimentoSelecionado
                                && p.Ativo == true
                                && p.Ano == DateTime.Now.Year
                                && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.CBO)).ToList().Count != 0)
                            {
                                pactoAbrangenciaAgregado = pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Cbo != null
                                && p.Cbo.Codigo == co_cbo
                                && p.Procedimento != null
                                && p.Procedimento.Codigo == co_procedimentoSelecionado
                                && p.Ativo == true
                                && p.Ano == DateTime.Now.Year
                                && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.CBO)).ToList().FirstOrDefault();
                            }
                        }
                        if (pactoAbrangenciaAgregado == null)
                        {
                            if (!string.IsNullOrEmpty(co_procedimentoSelecionado))//Se ele ele informou o procedimento
                            {
                                //Verifico o Pacto por Procedimento
                                if (pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Procedimento != null
                                    && p.Procedimento.Codigo == co_procedimentoSelecionado
                                    && p.Ativo == true
                                    && p.Ano == DateTime.Now.Year
                                    && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.PROCEDIMENTO)).ToList().Count != 0)
                                {
                                    pactoAbrangenciaAgregado = pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Procedimento != null
                                    && p.Procedimento.Codigo == co_procedimentoSelecionado
                                    && p.Ativo == true
                                    && p.Ano == DateTime.Now.Year
                                    && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.PROCEDIMENTO)).ToList().FirstOrDefault();
                                }
                                else // Verifico pelo Agregado
                                {
                                    //Irei Buscar agora Pelo Agregado que tem o Procedimento Selecionado
                                    ViverMais.Model.Agregado agregado = Factory.GetInstance<IProcedimentoAgregado>().BuscaAgregadoPorProcedimento<ViverMais.Model.Agregado>(co_procedimentoSelecionado);
                                    if (agregado != null)
                                    {
                                        if (pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Agregado.Codigo == agregado.Codigo
                                            && p.Ativo == true
                                            && p.Ano == DateTime.Now.Year
                                            && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.AGREGADO)).ToList().Count != 0)
                                        {
                                            pactoAbrangenciaAgregado = pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Agregado.Codigo == agregado.Codigo
                                            && p.Ativo == true
                                            && p.Ano == DateTime.Now.Year
                                            && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.AGREGADO)).ToList().FirstOrDefault();
                                        }
                                    }
                                }
                            }
                        }
                        if (pactoAbrangenciaAgregado != null)
                        {
                            ViverMais.Model.Procedimento procedimentoSelecionado = iPactoAbrangenciaAgregado.BuscarPorCodigo<ViverMais.Model.Procedimento>(co_procedimentoSelecionado);
                            if (procedimentoSelecionado != null)
                            {
                                PactoAbrangenciaGrupoMunicipio pactoAbrangenciaGrupoMunicipio = iPactoAbrangenciaAgregado.ListarPactoAbrangenciaGrupoMunicipioPorPactoAbrangenciaAgregado<PactoAbrangenciaGrupoMunicipio>(pactoAbrangenciaAgregado.Codigo).Where(p => p.Municipio.Codigo == co_municipio).FirstOrDefault();
                                if (pactoAbrangenciaAgregado.ValorUtilizado + procedimentoSelecionado.ValorProcedimentoAmbulatorialFormatado <= pactoAbrangenciaAgregado.ValorPactuado)
                                {
                                    System.Web.HttpContext.Current.Session["PactoAbrangencia"] = pactoAbrangenciaAgregado;
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        void ISolicitacao.SalvaSolicitacaoReguladaAutorizada<S, H>(S solicitacao, H h)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    Solicitacao solicit = (Solicitacao)(object)solicitacao;
                    Session.Save(solicit);
                    System.Web.HttpFileCollection listaArquivos = (System.Web.HttpFileCollection)(object)h;

                    for (int i = 0; i < listaArquivos.Count; i++)//Pra cada arquivo de laudo
                    {
                        string nomeArquivo = listaArquivos[i].FileName.Substring(listaArquivos[i].FileName.LastIndexOf("\\") + 1, listaArquivos[i].FileName.Length - listaArquivos[i].FileName.LastIndexOf("\\") - 1);
                        string path = "~/Agendamento/laudos/";
                        string caminhoParaChecar = System.Web.HttpContext.Current.Server.MapPath("~/Agendamento/laudos/" + nomeArquivo);
                        if (File.Exists(caminhoParaChecar))//Verifica se o Arquivo existe no Diretório com o mesmo nome
                        {
                            int cont = 2;
                            string nomeTemporario = string.Empty;
                            while (File.Exists(caminhoParaChecar))//Enquanto Existir ele irá alterar o nome de acordo com o count.. Ex: 1nomeArquivo, 2nomeArquivo, 3nomeArquivo
                            {
                                nomeTemporario = cont.ToString() + nomeArquivo;
                                caminhoParaChecar = System.Web.HttpContext.Current.Server.MapPath(path) + nomeTemporario;
                                cont++;
                            }
                            nomeArquivo = nomeTemporario;
                        }
                        path += nomeArquivo;

                        //Salva laudo da solicitação
                        Laudo laudo = new Laudo();
                        Stream imagem = listaArquivos[i].InputStream;

                        //Converte a imagem em bytes
                        byte[] buffer = new byte[imagem.Length];
                        imagem.Read(buffer, 0, buffer.Length);

                        laudo.Imagem = buffer;
                        laudo.Endereco = nomeArquivo;
                        laudo.Solicitacao = solicit;
                        Session.Save(laudo);
                    }

                    //Verifica se o Paciente está incluído na Lista de Procura
                    ListaProcura listaProcura = Factory.GetInstance<IListaProcura>().BuscaNaListaPorPacientePorProcedimento<ListaProcura>(solicit.ID_Paciente, solicit.Procedimento.Codigo).Where(p => p.Agendado == false).ToList().FirstOrDefault();
                    if (listaProcura != null) //Caso exista Solicitacao Pendente, ele incrementa a qtd na Lista
                    {
                        listaProcura.DataUltimaProcura = DateTime.Now;
                        listaProcura.Quantidade++;
                        Session.Save(listaProcura);
                    }
                    Session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    Session.Transaction.Rollback();
                    throw e;
                }
            }
        }

        void ISolicitacao.AutorizaSolicitacaoReguladaAutorizada<S, A>(S solicit, A agd)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    ISolicitacao iSolicitacao = Factory.GetInstance<ISolicitacao>();
                    ViverMais.Model.Solicitacao solicitacao = (Solicitacao)(object)solicit;
                    ViverMais.Model.Agenda agenda = (ViverMais.Model.Agenda)(object)agd;
                    if (iSolicitacao.ExcedeuCota<ViverMais.Model.Agenda>(agenda))
                        throw new Exception("A Vaga foi preenchida. Por favor, Selecione a especialidade desejada novamente!");


                    string identificador;
                    if (Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc.IRegistro>().BuscarPorProcedimento<ProcedimentoRegistro>(solicitacao.Procedimento.Codigo, Registro.APAC_PROC_PRINCIPAL).Count != 0)//Se o Procedimento for APAC
                    {
                        identificador = iSolicitacao.GeraIdentificadorAPAC(agenda.Data.Year);
                        if (identificador == "0")
                            throw new Exception("O Sistema não possuiu mais faixa APAC Cadastrada! Cadastre uma nova Faixa ou entre em contato com a administração!");
                        //else if(iSolicitacao.BuscaDuplicidadeIdentificador<Solicitacao>(identificador) != null)
                    }
                    else
                    {
                        TipoProcedimento tipoProcedimento = Factory.GetInstance<ITipoProcedimento>().BuscaTipoProcedimento<TipoProcedimento>(agenda.Procedimento.Codigo);
                        identificador = iSolicitacao.GeraIdentificador<ViverMais.Model.Agenda>(tipoProcedimento.Tipo.ToString(), agenda);
                        while (iSolicitacao.BuscaDuplicidadeIdentificador<Solicitacao>(identificador) != null)
                            identificador = iSolicitacao.GeraIdentificador<ViverMais.Model.Agenda>(tipoProcedimento.Tipo.ToString(), agenda);
                    }

                    solicitacao.Identificador = identificador;
                    agenda.QuantidadeAgendada += 1;
                    solicitacao.Agenda = agenda;
                    Session.Update(solicitacao);
                    Session.Update(agenda);
                    Session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    Session.Transaction.Rollback();
                    throw e;
                }
            }
        }

        void ISolicitacao.SalvaSolicitacaoAgendadaAtendimentoBasico<S, A>(S solicit, A agd, string co_subgrupo)
        {
            using (Session.BeginTransaction())
            //using (Session.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    ViverMais.Model.Solicitacao solicitacao = (Solicitacao)(object)solicit;
                    ViverMais.Model.Agenda agenda = (ViverMais.Model.Agenda)(object)agd;

                    ISolicitacao iSolicitacao = Factory.GetInstance<ISolicitacao>();

                    IList<ViverMais.Model.Parametros> parametro = Factory.GetInstance<IAgendamentoServiceFacade>().ListarTodos<ViverMais.Model.Parametros>();
                    if (parametro.Count != 0)
                    {
                        DateTime data_inicial = DateTime.Now.AddDays(parametro[0].Min_Dias);
                        DateTime data_final = DateTime.Now.AddDays(parametro[0].Max_Dias);
                        if (iSolicitacao.ExcedeuCota<ViverMais.Model.Agenda>(solicitacao.TipoCotaUtilizada, agenda, data_inicial, data_final, co_subgrupo == null ? 0 : int.Parse(co_subgrupo)))
                        {
                            throw new Exception("O Percentual Total foi Atingido. Por favor, Selecione a especialidade desejada novamente!");
                        }
                    }

                    TipoProcedimento tipoProcedimento = Factory.GetInstance<ITipoProcedimento>().BuscaTipoProcedimento<TipoProcedimento>(agenda.Procedimento.Codigo);
                    string identificador = iSolicitacao.GeraIdentificador<ViverMais.Model.Agenda>(tipoProcedimento.Tipo.ToString(), agenda);
                    while (iSolicitacao.BuscaDuplicidadeIdentificador<Solicitacao>(identificador) != null)
                        identificador = iSolicitacao.GeraIdentificador<ViverMais.Model.Agenda>(tipoProcedimento.Tipo.ToString(), agenda);

                    solicitacao.Identificador = identificador;
                    solicitacao.Procedimento = agenda.Procedimento;
                    agenda.QuantidadeAgendada += 1;
                    //ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)(object)paci;

                    //ViverMais.Model.Endereco endereco = (ViverMais.Model.Endereco)(object)end;
                    solicitacao.Agenda = agenda;
                    Session.Save(solicitacao);
                    Session.Update(agenda);

                    Session.Transaction.Commit();
                    //return true;
                    //}
                    //else
                    //{
                    //    Session.Transaction.Rollback();
                    //    return false;
                    //}
                }
                catch (Exception e)
                {
                    Session.Transaction.Rollback();
                    throw e;
                }
            }
        }

        bool ISolicitacao.ExcedeuCota<A>(A agd)
        {
            ViverMais.Model.Agenda agenda = (ViverMais.Model.Agenda)(object)agd;
            IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().ListaSolicitacoesDaAgenda<Solicitacao>(agenda.Codigo).Where(p => p.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString() || p.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString() || p.Situacao == Convert.ToChar(Solicitacao.SituacaoSolicitacao.FALTOSO).ToString()).ToList();
            if (agenda.Quantidade <= solicitacoes.Count)
                return true;
            else
                return false;
        }

        bool ISolicitacao.ExcedeuCota<A>(char tipoCota, A agd, DateTime data_inicial, DateTime data_final, int co_subgrupo)
        {
            int cota = 0;
            if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.REDE))
                cota = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.REDE);
            else if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.LOCAL))
                cota = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.LOCAL);
            else if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.DISTRITAL))
                cota = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.DISTRITAL);
            else if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.RESERVA_TECNICA))
                cota = Convert.ToInt32(ParametroAgenda.TipoDeAgenda.RESERVA_TECNICA);
            ViverMais.Model.Agenda agenda = (ViverMais.Model.Agenda)(object)agd;

            IList<ParametroAgenda> parametrosAgenda = Factory.GetInstance<IParametroAgenda>().BuscarParametros<ViverMais.Model.ParametroAgenda>(agenda.Estabelecimento.CNES, ParametroAgenda.CONFIGURACAO_PROCEDIMENTO, agenda.Procedimento.Codigo, agenda.Cbo.Codigo, co_subgrupo.ToString());
            ParametroAgenda parametro = null;
            if (parametrosAgenda.Count != 0)
            {
                parametro = parametrosAgenda.Where(p => p.TipoAgenda == cota).ToList().FirstOrDefault();
            }
            else // Se ele não parametrizou o procedimento e CBO, pego o parametro Agenda da Unidade
            {
                parametro = Factory.GetInstance<IParametroAgenda>().BuscarParametrosPorTipo<ParametroAgenda>(agenda.Estabelecimento.CNES, cota, ParametroAgenda.CONFIGURACAO_UNIDADE);
            }

            IList<ViverMais.Model.Agenda> agendas = Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda.IAmbulatorial>().ListarAgendasLocais<ViverMais.Model.Agenda>(agenda.Procedimento.Codigo, agenda.Cbo.Codigo, data_inicial, data_final, agenda.Estabelecimento.CNES, co_subgrupo);

            Usuario usuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            int qtdSolicitacoes = 0;
            if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.REDE))
            {
                QuantidadeSolicitacaoRede quantidadeSolicitacoes = Factory.GetInstance<IQuantidadeSolicitacaoRede>().BuscaQuantidade<QuantidadeSolicitacaoRede>((DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00")), agenda.Procedimento.Codigo, agenda.Cbo.Codigo, agenda.Estabelecimento.CNES, co_subgrupo);
                if (quantidadeSolicitacoes != null)
                {
                    qtdSolicitacoes = quantidadeSolicitacoes.QtdSolicitacoes;
                }
                else
                {
                    qtdSolicitacoes = int.Parse(Factory.GetInstance<ISolicitacao>().ListarSolicitacoesParametroRede(DateTime.Now, agenda.Procedimento.Codigo, agenda.Cbo.Codigo, agenda.Estabelecimento.CNES, data_inicial, data_final, Convert.ToChar(Solicitacao.TipoCota.REDE), co_subgrupo).ToString());
                }
                //qtdSolicitacoes = int.Parse(Factory.GetInstance<ISolicitacao>().ListarSolicitacoesParametroRede(DateTime.Now, agenda.Procedimento.Codigo, agenda.Cbo.Codigo, agenda.Estabelecimento.CNES, data_inicial, data_final, Convert.ToChar(Solicitacao.TipoCota.REDE)).ToString());
            }
            else if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.DISTRITAL))
                qtdSolicitacoes = int.Parse(Factory.GetInstance<ISolicitacao>().ListarSolicitacoesParametroDistrital(DateTime.Now, agenda.Procedimento.Codigo, agenda.Cbo.Codigo, agenda.Estabelecimento.CNES, usuario.Unidade.Bairro.Distrito.Codigo, data_inicial, data_final, Convert.ToChar(Solicitacao.TipoCota.DISTRITAL), co_subgrupo).ToString());
            else if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.LOCAL))
                qtdSolicitacoes = int.Parse(Factory.GetInstance<ISolicitacao>().ListarSolicitacoesParametroLocal(DateTime.Now, agenda.Procedimento.Codigo, agenda.Cbo.Codigo, agenda.Estabelecimento.CNES, data_inicial, data_final, Convert.ToChar(Solicitacao.TipoCota.LOCAL), co_subgrupo).ToString());
            else if (tipoCota == Convert.ToChar(Solicitacao.TipoCota.RESERVA_TECNICA))
                qtdSolicitacoes = int.Parse(Factory.GetInstance<ISolicitacao>().ListarSolicitacoesParametroReservaTecnica(DateTime.Now, agenda.Procedimento.Codigo, agenda.Cbo.Codigo, agenda.Estabelecimento.CNES, data_inicial, data_final, Convert.ToChar(Solicitacao.TipoCota.RESERVA_TECNICA), co_subgrupo).ToString());

            int qtdTotalVagas = agendas.Sum(p => p.Quantidade);//Faço um somatorio da Quantidade de Todas as Agendas
            int qtdDisponivel = (qtdTotalVagas * parametro.Percentual) / 100; // Verifico a quantidade de vagas que ficará disponível para a Rede

            if (qtdSolicitacoes < qtdDisponivel)
                return false;
            else
                return true;
        }

        IList<T> ISolicitacao.ListarSolicitacoesAgendaPrestador<T>(string cnesExecutante, string id_procedimento, string cpf_Profissional, DateTime periodoInicial, DateTime periodoFinal)
        {
            string hql = "from ViverMais.Model.Solicitacao solicitacao where solicitacao.Agenda.Estabelecimento.CNES = '" + cnesExecutante + "'";
            hql += " and solicitacao.Agenda.Procedimento.Codigo = '" + id_procedimento + "'";
            hql += " and solicitacao.Agenda.Data between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
            + " and TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            hql += " and (solicitacao.Situacao = '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString() + "'";
            hql += " or solicitacao.Situacao = '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA).ToString() + "')";
            //hql += " and solicitacao.Agenda.Data <='" + FormatoData.ConverterData(periodoFinal, "Oracle") + "'";
            hql += " and solicitacao.Agenda.Publicada = 1";
            if (cpf_Profissional != "0")
                hql += " and solicitacao.Agenda.ID_Profissional.CPF = '" + cpf_Profissional + "'";

            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.BuscaSolicitacaoAgendadaPorPaciente<T>(string id_paciente)
        {
            DateTime hoje = DateTime.Today;
            string hql = "from ViverMais.Model.Solicitacao as solicitacao";
            hql += " WHERE solicitacao.ID_Paciente = '" + id_paciente + "'";
            hql += " and solicitacao.Situacao <> '5'";
            return Session.CreateQuery(hql).List<T>();
        }

        int ISolicitacao.QuantidadeTotalSolicitacoesPendentes(string prioridade, string cartaosus, string id_procedimento)
        {
            string hql = "FROM ViverMais.Model.Solicitacao sol ";
            hql += " WHERE sol.Situacao='1'";//Pendente

            if (prioridade != "")
                hql += " and sol.Prioridade ='" + prioridade + "'";

            if (cartaosus != "")
            {
                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(cartaosus);
                if (paciente != null)
                    hql += " and sol.ID_Paciente='" + paciente.Codigo + "'";
            }

            if (id_procedimento != "0")
                hql += " and sol.Procedimento.Codigo ='" + id_procedimento + "'";

            hql += " order by sol.Data_Solicitacao asc, sol.Prioridade";
            return Session.CreateQuery(hql).List().Count;
        }

        int ISolicitacao.QuantidadeTotalSolicitacoesPendentes(string prioridade, string cartaosus, string id_procedimento, int selecaoMunicipio, string municipio)
        {
            string hql = "Select sol FROM ViverMais.Model.Solicitacao sol, ViverMais.Model.EnderecoUsuario endereco where sol.ID_Paciente = endereco.CodigoPaciente and endereco.Excluido = 0";
            if (selecaoMunicipio == Solicitacao.rbtItemSalvador)
                hql += " and endereco.Endereco.Municipio.Codigo = '" + Municipio.SALVADOR + "'";
            else if (selecaoMunicipio == Solicitacao.rbtItemInterior)
                hql += " and endereco.Endereco.Municipio.Codigo <> '" + Municipio.SALVADOR + "'";
            else if (selecaoMunicipio == Solicitacao.rbtItemMunicipioEspecifico)
                hql += " and endereco.Endereco.Municipio.Codigo = '" + municipio.ToString() + "'";
            hql += " and sol.Situacao='1'";//Pendente

            if (prioridade != "")
                hql += " and sol.Prioridade ='" + prioridade + "'";

            if (cartaosus != "")
            {
                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(cartaosus);
                if (paciente != null)
                    hql += " and sol.ID_Paciente='" + paciente.Codigo + "'";
            }

            if (id_procedimento != "0")
                hql += " and sol.Procedimento.Codigo ='" + id_procedimento + "'";

            hql += " order by sol.Data_Solicitacao asc, sol.Prioridade";
            return Session.CreateQuery(hql).List().Count;
        }

        IList<T> ISolicitacao.ListarSolicitacoesPendentes<T>(string prioridade, string cartaosus, string id_procedimento, int pageIndex, int pageSize, int selecaoMunicipio, string municipio)
        {
            string hql = "Select sol FROM ViverMais.Model.Solicitacao sol, ViverMais.Model.EnderecoUsuario endereco where sol.ID_Paciente = endereco.CodigoPaciente and endereco.Excluido = '0'";
            if (selecaoMunicipio == Solicitacao.rbtItemSalvador)
                hql += " and endereco.Endereco.Municipio.Codigo = '" + Municipio.SALVADOR + "'";
            else if (selecaoMunicipio == Solicitacao.rbtItemInterior)
                hql += " and endereco.Endereco.Municipio.Codigo <> '" + Municipio.SALVADOR + "'";
            else if (selecaoMunicipio == Solicitacao.rbtItemMunicipioEspecifico)
                hql += " and endereco.Endereco.Municipio.Codigo = '" + municipio.ToString() + "'";

            hql += " and sol.Situacao = '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE) + "'";

            if (prioridade != "")
                hql += " and sol.Prioridade ='" + prioridade + "'";

            if (cartaosus != "")
            {
                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(cartaosus);
                if (paciente != null)
                    hql += " and sol.ID_Paciente='" + paciente.Codigo + "'";
            }

            if (id_procedimento != "0")
            {
                hql += " and sol.Procedimento.Codigo ='" + id_procedimento + "'";
            }
            hql += " order by sol.Data_Solicitacao asc, sol.Prioridade";
            return Session.CreateQuery(hql).SetFirstResult((pageSize * pageIndex)).SetMaxResults(pageSize).List<T>();
        }

        IList<T> ISolicitacao.ListarSolicitacoesPendentes<T>(string prioridade, string cartaosus, string id_procedimento, int pageIndex, int pageSize)
        {
            string hql = "Select sol FROM ViverMais.Model.Solicitacao sol";
            hql += " WHERE sol.Situacao='" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE) + "'";//Pendente";

            if (prioridade != "")
                hql += " and sol.Prioridade ='" + prioridade + "'";

            if (cartaosus != "")
            {
                ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().PesquisarPacientePorCNS<ViverMais.Model.Paciente>(cartaosus);
                if (paciente != null)
                    hql += " and sol.ID_Paciente='" + paciente.Codigo + "'";
            }

            if (id_procedimento != "0")
            {
                hql += " and sol.Procedimento.Codigo ='" + id_procedimento + "'";
            }
            hql += " order by sol.Data_Solicitacao asc, sol.Prioridade";
            return Session.CreateQuery(hql).SetFirstResult((pageSize * pageIndex)).SetMaxResults(pageSize).List<T>();
        }
        T ISolicitacao.BuscaDuplicidadeIdentificador<T>(string identificador)
        {
            string hql = "from ViverMais.Model.Solicitacao as solicitacao";
            hql += " WHERE solicitacao.Identificador = '" + identificador + "'";
            hql += " and TO_CHAR(solicitacao.Agenda.Data, 'MM') = '" + DateTime.Now.Month.ToString("00") + "'";
            hql += " and TO_CHAR(solicitacao.Agenda.Data,'YYYY') = '" + DateTime.Now.Year.ToString("0000") + "'";
            //hql += " AND solicitacao.Agenda.Competencia = " + DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00");
            return Session.CreateQuery(hql).List<T>().LastOrDefault();
        }


        IList<T> ISolicitacao.BuscaSolicitacaoPeloIdentificador<T>(string identificador)
        {
            string hql = "from ViverMais.Model.Solicitacao as solicitacao";
            hql += " WHERE solicitacao.Identificador = '" + identificador + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<object> ISolicitacao.ListarRegistroParaBPAConsolidado<T>(int competencia, string id_unidade, DateTime periodoInicial, DateTime periodoFinal)
        {
            string hql = string.Empty;
            hql += "select agd.co_ocupacao, agd.co_procedimento, trunc((months_between(sysdate, usu.dt_nascimento))/12) as idade, count(*) as qtd from agd_solicitacao sol";
            hql += " INNER JOIN agd_agenda agd on agd.co_agenda = sol.co_agenda";
            hql += " INNER JOIN tb_ms_usuario usu on usu.co_usuario = sol.co_paciente";
            hql += " INNER JOIN tb_pms_procedimento proced on proced.co_procedimento = agd.co_procedimento";
            hql += " INNER JOIN pms_cnes_lfces004 unidade on unidade.cnes = agd.cnes";
            hql += " INNER JOIN tb_pms_rl_proced_registro PROCED_REG on proced_reg.co_procedimento = proced.co_procedimento";
            hql += " INNER JOIN tb_pms_registro registro on registro.co_registro = proced_reg.co_registro";
            hql += " where agd.competencia = " + competencia;
            hql += " and agd.data between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
            + "AND TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            hql += " AND sol.situacao = '5'";
            hql += " and registro.co_registro = '01'";
            hql += " and agd.cnes = '" + id_unidade + "'";
            hql += " group by agd.co_ocupacao, agd.co_procedimento, trunc((months_between(sysdate, usu.dt_nascimento))/12)";
            hql += " order by agd.co_ocupacao, agd.co_procedimento, trunc((months_between(sysdate, usu.dt_nascimento))/12)";
            //hql = "select solicitacao FROM ViverMais.Model.Solicitacao as solicitacao, ViverMais.Model.ProcedimentoRegistro procedRegistro";
            //hql += " where procedRegistro.Procedimento.Codigo = solicitacao.Agenda.Procedimento.Codigo";
            //hql += " and procedRegistro.Registro.Codigo=''";
            //hql += " and solicitacao.Agenda.Competencia = '" + competencia + "'";
            //hql += " and solicitacao.Agenda.Estabelecimento.CNES = '" + id_unidade + "'";
            //hql += " and solicitacao.Agenda.Data between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
            //+ " and TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            //hql += " and solicitacao.Situacao='5'";//Situação 5 == Confirmada
            return Session.CreateSQLQuery(hql).List<object>();
        }

        IList<T> ISolicitacao.ListarSolicitacoesConfirmadasBPAApac<T>(int competencia, string id_unidade, DateTime periodoInicial, DateTime periodoFinal)
        {
            string hql = string.Empty;
            hql = "select solicitacao FROM ViverMais.Model.Solicitacao as solicitacao, ViverMais.Model.ProcedimentoRegistro as procedimentoRegistro ";
            //hql += " Inner Join solicitacao.Procedimento as procedimento";
            //hql += " Inner Join solicitacao.Procedimento as procedimento";
            //hql += " Inner Join ViverMais.Model.ProcedimentoRegistro as procedimentoRegistro procedimentoRegistro.Procedimento.Codigo = solicitacao.Procedimento.Codigo";
            hql += " WHERE solicitacao.Agenda.Competencia = '" + competencia + "'";
            hql += " and solicitacao.Agenda.Estabelecimento.CNES = '" + id_unidade + "'";
            hql += " and solicitacao.Agenda.Data between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
            + " and TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            hql += " and solicitacao.Situacao='5'";//Situação 5 == Confirmada
            hql += " and procedimentoRegistro.Procedimento.Codigo = solicitacao.Procedimento.Codigo";
            hql += " and procedimentoRegistro.Registro.Codigo='" + Registro.APAC_PROC_PRINCIPAL.ToString("00") + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.ListarSolicitacoesConfirmadas<T>(int competencia, string id_unidade, DateTime periodoInicial, DateTime periodoFinal, string tipoProcedimento)
        {
            string hql = string.Empty;
            hql = "Select solicitacao FROM ViverMais.Model.Solicitacao as solicitacao, ViverMais.Model.ProcedimentoRegistro as procedimentoRegistro";
            hql += " WHERE solicitacao.Agenda.Competencia = '" + competencia + "'";
            hql += " and solicitacao.Agenda.Estabelecimento.CNES = '" + id_unidade + "'";
            hql += " and solicitacao.Agenda.Data between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
            + " and TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            hql += " and solicitacao.Situacao='5'";//Situação 5 == Confirmada
            hql += " and procedimentoRegistro.Procedimento.Codigo = solicitacao.Procedimento.Codigo";
            hql += " and procedimentoRegistro.Registro.Codigo='" + tipoProcedimento + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.ListarSolicitacoesConfirmadas<T>(int competencia, string id_unidade, DateTime periodoInicial, DateTime periodoFinal)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Solicitacao as solicitacao";
            hql += " WHERE solicitacao.Agenda.Competencia = '" + competencia + "'";
            hql += " and solicitacao.Agenda.Estabelecimento.CNES = '" + id_unidade + "'";
            hql += " and solicitacao.Agenda.Data between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
            + " and TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            hql += " and solicitacao.Situacao='5'";//Situação 5 == Confirmada
            return Session.CreateQuery(hql).List<T>();
        }

        String ISolicitacao.GeraProtocoloSolicitacao()
        {
            Random rand = new Random();
            string letras = Convert.ToChar(rand.Next(65, 90)).ToString();
            int numeros = rand.Next(99999);
            String protocolo = letras.ToString() + numeros;
            return protocolo;
        }

        IList<object> ISolicitacao.ListaSolicitacoesAPAC()
        {
            string hql = string.Empty;
            hql += "select sol.co_solicitacao ,sol.identificador, estabelecimento.nome_fanta, paciente.no_usuario, paciente.no_mae, paciente.dt_nascimento, count(co_solicitacao) from agd_solicitacao sol";
            hql += " inner join agd_agenda agd on agd.co_agenda = sol.co_agenda";
            hql += " inner join pms_cnes_lfces004 estabelecimento on estabelecimento.cnes = agd.cnes";
            hql += " inner JOIN tb_pms_procedimento proced on proced.co_procedimento = agd.co_procedimento";
            hql += " inner join tb_pms_rl_proced_registro proced_reg on proced_reg.co_procedimento = proced.co_procedimento";
            hql += " inner join tb_pms_registro registro on registro.co_registro = proced_reg.co_registro";
            hql += " inner join tb_ms_usuario paciente on paciente.co_usuario = sol.co_paciente";
            //hql += " inner join tb_ms_usuario_cns_elos cartao on cartao.co_usuario = sol.co_paciente";
            hql += " where registro.co_registro = '06' and sol.identificador not like '29%'";
            hql += " group by sol.co_solicitacao ,sol.identificador,estabelecimento.nome_fanta, paciente.no_usuario, paciente.no_mae, paciente.dt_nascimento";
            hql += " order by sol.co_solicitacao ,sol.identificador,estabelecimento.nome_fanta, paciente.no_usuario, paciente.no_mae, paciente.dt_nascimento";
            return Session.CreateSQLQuery(hql).List<object>();
            /*--, sol.identificador, proced.no_procedimento, 
            --(case when tipo.tipo = 1 then 'REGULADO' when tipo.tipo = 2 then 'AUTORIZADO' when tipo.tipo = 3 then 'AGENDADO' when tipo.tipo = 4 then 'AT. BASICO' else 'SEM TIPO' end) as TIPO_PROCEDIMENTO,
            --(case when sol.SITUACAO = 2 then 'AUTORIZADA' when sol.SITUACAO = 4 then 'INDEFERIDA'  when sol.SITUACAO = 5 then 'CONFIRMADA' when sol.SITUACAO = 6 then 'DESMARCADA' ELSE sol.situacao  end) AS SITUACAO
            --and sol.identificador like '29%'
            --and (sol.situacao <> '6' and sol.situacao <> '5')
            order by proced.no_procedimento*/
        }

        String ISolicitacao.GeraIdentificadorAPAC(int ano)
        {

            string identificador_final = "";
            string co_unidade_federativa = "29";
            string year = ano.ToString().Substring(2, 2);
            string identificador = co_unidade_federativa + year + "2";
            //char letras;
            // Busca a faixa APAC Atual    
            ViverMais.Model.Faixa faixa = Factory.GetInstance<IFaixa>().BuscarFaixaAPAC<ViverMais.Model.Faixa>(ano).FirstOrDefault();
            //Busca todas as faixas utilizadas            IList<FaixaUtilizada> faixas_utilizada = Factory.GetInstance<IFaixaUtilizada>().BuscarFaixaUtilizada<ViverMais.Model.FaixaUtilizada>(faixa.Codigo.ToString());            // cria uma lista para gerar o número randomico            List<string> faixa_randomica = new List<string>();                        for (int i = int.Parse(faixa.FaixaInicial); i <= int.Parse(faixa.FaixaFinal); i++)//Pra cada arquivo de laudo            {
            //Busca todas as faixas utilizadas
            if (faixa != null)
            {
                IList<FaixaUtilizada> faixas_utilizada = Factory.GetInstance<IFaixaUtilizada>().BuscarFaixaUtilizada<ViverMais.Model.FaixaUtilizada>(faixa.Codigo.ToString());
                // cria uma lista para gerar o número randomico
                List<string> faixa_randomica = new List<string>();

                for (int i = int.Parse(faixa.FaixaInicial); i <= int.Parse(faixa.FaixaFinal); i++)//Pra cada arquivo de laudo
                {
                    // verifica se a faixa da apac já foi utilizada caso não seja inclui na lista da faixa randomica
                    if (!faixas_utilizada.Select(p => p.Faixa_Utilizada).ToList().Contains(i.ToString("0000000")))
                    {
                        faixa_randomica.Add(i.ToString("0000000"));
                    }
                }

                if (faixa_randomica.Count != 0)
                {
                    string valor_rand = faixa_randomica[randomico.Next(0, (faixa_randomica.Count() - 1))];
                    //string valor_rand = faixa_randomica.OrderBy(p => randomico.Next()).Take(faixa_randomica.Count()).FirstOrDefault();
                    //if (faixa != null)
                    //{
                    //Random randomico = new Random();
                    //string rand = "";
                    //rand = randomico.Next(int.Parse(faixa.FaixaInicial.ToString()), int.Parse(faixa.FaixaFinal.ToString())).ToString();
                    //if (rand.Length < 7)
                    //{
                    //    int qtd = (7 - rand.Length);
                    //    for (int j = 0; j < qtd; j++)
                    //    {
                    //        rand = "0" + rand;
                    //    }

                    //}
                    //                string identificador_faixa = identificador + rand;
                    string identificador_faixa = identificador + valor_rand;
                    string digito_verificador = "0";
                    digito_verificador = DigitoVerificador(identificador_faixa);
                    identificador_final = identificador_faixa + digito_verificador;
                    //while (identificador_final != "")
                    //{
                    //    Solicitacao solicitacao = Factory.GetInstance<ISolicitacao>().BuscaSolicitacaoPeloIdentificador<Solicitacao>(identificador_final).FirstOrDefault();
                    //    if (solicitacao != null)
                    //    {
                    //        rand = randomico.Next(int.Parse(faixa.FaixaInicial.ToString()), int.Parse(faixa.FaixaFinal.ToString())).ToString();
                    //        if (rand.Length < 7)
                    //        {
                    //            int qtd = (7 - rand.Length);
                    //            for (int j = 0; j < qtd; j++)
                    //            {
                    //                rand = "0" + rand;
                    //            }

                    //        }
                    //        identificador_faixa = "";
                    //        identificador_final = "";
                    //        identificador_faixa = identificador + rand;
                    //        digito_verificador = DigitoVerificador(identificador_faixa);
                    //        identificador_final = identificador_faixa + digito_verificador;
                    //    }
                    //    else
                    //    {
                    //        break;
                    //    }
                    //}
                    //}
                    //else
                    //{
                    //    return identificador_final = "0";
                    //}
                    ISolicitacao iSolicitacao = Factory.GetInstance<ISolicitacao>();
                    try
                    {
                        while (iSolicitacao.BuscaDuplicidadeIdentificador<Solicitacao>(identificador_final) != null)
                            identificador_final = iSolicitacao.GeraIdentificadorAPAC(DateTime.Now.Year);
                    }
                    catch
                    {
                        throw;
                    }

                    faixa.Quantidade_utilizada += 1;
                    Factory.GetInstance<IAgendamentoServiceFacade>().Salvar(faixa);
                    ViverMais.Model.FaixaUtilizada faixautilizada = new FaixaUtilizada();
                    faixautilizada.Faixa_Utilizada = valor_rand;
                    faixautilizada.Faixa = faixa;
                    Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(faixautilizada);
                    return identificador_final;
                }
                else
                {
                    return identificador_final = "0";
                }
            }
            else
            {
                return identificador_final = "0";
            }
        }

        string DigitoVerificador(string identificador)
        {
            long digito_verificador = 0;
            long valor = (long.Parse(identificador) / 11);
            long valor2 = valor * 11;
            digito_verificador = (long.Parse(identificador) - valor2);
            if (digito_verificador == 10)
            {
                digito_verificador = 0;
            }

            return digito_verificador.ToString();
        }

        //IList<T> ISolicitacao.ListaSolicitacoesSemIdentificador<T>()
        //{
        //    return Session.CreateQuery("from ViverMais.Model.Solicitacao solicitacao where solicitacao.Agenda IS NOT NULL and solicitacao.Identificador IS NULL").List<T>();

        //}


        String ISolicitacao.GeraIdentificador<A>(string tipoProcedimento, A agendaParameter)
        {

            string identificador = "";
            int numeros;

            ISolicitacao iSolicitacao = Factory.GetInstance<ISolicitacao>();
            ViverMais.Model.Agenda agenda = (ViverMais.Model.Agenda)(object)agendaParameter;

            //if (tipoProcedimento == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.AGENDADO).ToString() || tipoProcedimento == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.ATENDIMENTOBASICO).ToString())
            //{
            //ViverMais.Model.EstabelecimentoSaude unidade = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(agenda.Estabelecimento.CNES);


            //numeros = randomico.Next(99999);
            numeros = HelperRandomGenerator.NextIdentificador;
            identificador = agenda.Estabelecimento.CNES.Substring(0, 6) + agenda.Data.Month.ToString("00") + numeros;
            //}

            while (iSolicitacao.BuscaDuplicidadeIdentificador<Solicitacao>(identificador) != null)
                Factory.GetInstance<ISolicitacao>().GeraIdentificador<ViverMais.Model.Agenda>(tipoProcedimento, agenda);
            return identificador;
        }

        //String ISolicitacao.GeraIdentificador<A>(A agendaParameter)
        //{
        //    int numeros;
        //    string identificador = "";
        //    //char letras;

        //    ViverMais.Model.Agenda agenda = (ViverMais.Model.Agenda)(object)agendaParameter;

        //    ViverMais.Model.EstabelecimentoSaude unidade = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(agenda.Estabelecimento.CNES);

        //    //Gera o número Identificador
        //    //Random randomico = new Random();
        //    numeros = randomico.Next(99999);
        //    identificador = unidade.CNES.Substring(0, 6) + DateTime.Now.ToString("MM") + numeros;
        //    return identificador;
        //}

        IList<T> ISolicitacao.ListarSolicitacoesPorPaciente<T>(string id_paciente, string protocolo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Solicitacao as solicitacao";
            hql += " WHERE solicitacao.ID_Paciente='" + id_paciente + "'";
            if (protocolo != "")
                hql += " and solicitacao.NumeroProtocolo='" + protocolo + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.ListarSolicitacoesPorPacientePorCnes<T>(string id_paciente, string cnes, DateTime data)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Solicitacao as solicitacao";
            hql += " WHERE solicitacao.ID_Paciente='" + id_paciente + "'";
            hql += " and solicitacao.Agenda.Estabelecimento.CNES = '" + cnes + "'";
            hql += " and solicitacao.Agenda.Data = '" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "'";

            return Session.CreateQuery(hql).List<T>();
        }


        IList<T> ISolicitacao.ListarSolicitacoesConfirmadasPorPaciente<T>(string id_paciente)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Solicitacao as solicitacao";
            hql += " WHERE solicitacao.ID_Paciente='" + id_paciente + "'";
            hql += " and solicitacao.Situacao='5'";//Situação 5 == Solicitacao Confirmada
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.BuscaLaudos<T>(int id_solicitacao)
        {
            string hql = "FROM ViverMais.Model.Laudo laudo ";
            hql += " WHERE laudo.Solicitacao.Codigo ='" + id_solicitacao + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.VerificaSolicitacao<T>(string id_paciente, string id_procedimento, string tipo, string cbo, string subgrupo)
        {
            string hql = "Select sol FROM ViverMais.Model.Solicitacao sol ";
            hql += "left join sol.Agenda agd";
            hql += " WHERE sol.ID_Paciente = '" + id_paciente + "'";
            //Agendamento e Agendamento Básico
            if (tipo == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.AGENDADO).ToString() || tipo == Convert.ToInt32(TipoProcedimento.TiposDeProcedimento.ATENDIMENTOBASICO).ToString())
            {
                hql += " and sol.Agenda.Procedimento.Codigo ='" + id_procedimento + "'";
                if (!String.IsNullOrEmpty(subgrupo))
                    hql += " and sol.Agenda.SubGrupo.Codigo = " + subgrupo;
                else
                    hql += " and sol.Agenda.SubGrupo is null";
                hql += " and sol.Agenda.Data >= TO_DATE('" + FormatoData.ConverterData(DateTime.Now, FormatoData.nomeBanco.ORACLE) + "','DD/MM/YYYY')";
                if (!String.IsNullOrEmpty(cbo))
                    hql += " and sol.Agenda.Cbo.Codigo = '" + cbo + "'";

                hql += " and sol.Situacao = '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString() + "'";//Situação Pendente
            }
            else
            {
                //hql += " and sol.Procedimento.Codigo ='" + id_procedimento + "'";
                //hql += " and ((sol.Situacao = '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString()
                //    + "' and sol.Agenda.Data <= TO_DATE('" + FormatoData.ConverterData(DateTime.Now, "Oracle") + "','DD/MM/YYYY'))";
                //hql += " or (sol.Situacao = '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE).ToString() + "')";
                //hql += " or (sol.Situacao = '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.AGENDAUTOMATICO).ToString() + "'))";
                hql += " and sol.Procedimento.Codigo ='" + id_procedimento + "'";
                hql += " and ((sol.Situacao = '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString() + "'";
                hql += " and agd.Data >= TO_DATE('" + FormatoData.ConverterData(DateTime.Now, FormatoData.nomeBanco.ORACLE) + "','DD/MM/YYYY'))";
                hql += " or sol.Situacao = '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE).ToString() + "'";
                hql += " or sol.Situacao = '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.AGENDAUTOMATICO).ToString() + "')";
            }
            //hql += " and sol.Agenda.Data <= TO_DATE('" + FormatoData.ConverterData(DateTime.Now, "Oracle") + "','DD/MM/YYYY')";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.ListarSolicitacoesDoPactoAgregado<T>(int id_Pacto_Proced_Agreg)
        {
            string hql = "from ViverMais.Model.Solicitacao solicitacao";
            hql += " where solicitacao.PactoAgregadoProcedCBO.Codigo=" + id_Pacto_Proced_Agreg;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.BuscaSolicitacoesNaoConfirmadasNaoIndeferidasPorAgenda<T>(int id_agenda)
        {
            string hql = "FROM ViverMais.Model.Solicitacao solicitacao ";
            hql += " WHERE solicitacao.Agenda.Codigo =" + id_agenda;
            hql += " AND solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.CONFIRMADA) + "' AND solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.INDEFERIDA) + "'";
            hql += " AND solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA) + "'"; //Situação <> de Confirmada, Indeferida e Desmarcada
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.ListaSolicitacoesDaAgenda<T>(int id_agenda)
        {
            string hql = string.Empty;
            hql += "from ViverMais.Model.Solicitacao solicitacao";
            hql += " where solicitacao.Agenda.Codigo = '" + id_agenda + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        long ISolicitacao.ListaSolicitacoesDaAgenda(int id_agenda, bool excluirDesmarcadas)
        {
            string hql = string.Empty;
            hql += "select Count(solicitacao.Codigo) from ViverMais.Model.Solicitacao solicitacao";
            hql += " where solicitacao.Agenda.Codigo = '" + id_agenda + "'";
            if (excluirDesmarcadas)
                hql += " and solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString() + "'";
            return Session.CreateQuery(hql).UniqueResult<long>();
        }

        IList<T> ISolicitacao.ListaSolicitacoesUnidadeSolicitante<T>(string id_procedimento, string cbo, int competencia, string id_unidade)
        {
            string hql = string.Empty;
            hql += "from ViverMais.Model.Solicitacao solicitacao";
            hql += " where solicitacao.Procedimento.Codigo = '" + id_procedimento + "'";
            hql += " and solicitacao.EasSolicitante <> solicitacao.Agenda.ID_Unidade";
            hql += " and solicitacao.Agenda.Cbo.Codigo = '" + cbo + "'";
            hql += " and solicitacao.Agenda.Competencia =" + competencia;
            hql += " and solicitacao.Agenda.Estabelecimento.CNES ='" + id_unidade + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.ListaAgendaUnidadeLocal<T>(string id_procedimento, string cbo, int competencia, string id_unidade, DateTime data_inicial, DateTime data_final)
        {
            string hql = string.Empty;
            hql += "from ViverMais.Model.Agenda agenda";
            hql += " where agenda.Procedimento.Codigo = '" + id_procedimento + "'";
            hql += " and agenda.Cbo.Codigo = '" + cbo + "'";
            hql += " and agenda.Competencia =" + competencia;
            hql += " and agenda.Estabelecimento.CNES ='" + id_unidade + "'";
            hql += " and agenda.Quantidade > agenda.QuantidadeAgendada";
            hql += " and agenda.Data between '" + FormatoData.ConverterData(data_inicial, FormatoData.nomeBanco.ORACLE) + "'and'" + FormatoData.ConverterData(data_final, FormatoData.nomeBanco.ORACLE) + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.ListarSolicitacoesPendentesAutorizadasPorPaciente<T>(string co_paciente)
        {
            string hql = string.Empty;
            hql += "from ViverMais.Model.Solicitacao solicitacao";
            hql += " where solicitacao.ID_Paciente='" + co_paciente + "'";
            hql += " and (solicitacao.Situacao='" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.PENDENTE).ToString() + "'";
            hql += " or solicitacao.Situacao='" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.AUTORIZADA).ToString() + "'";
            hql += " or solicitacao.Situacao='" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.AGENDAUTOMATICO).ToString() + "')";
            hql += " order by solicitacao.Data_Solicitacao desc";
            //, solicitacao.Agenda.ID_Profissional.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.ListarSolicitacoesDaUnidade<T>(string cnes, DateTime periodoInicial, DateTime periodoFinal, string codigo_usuario)
        {
            string hql = "from ViverMais.Model.Solicitacao solicitacao";
            hql += " where solicitacao.Data_Solicitacao between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
            + " and TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            hql += " and solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA).ToString() + "'";
            if (cnes != "0")
                hql += " and solicitacao.EasSolicitante='" + cnes + "'";
            if (codigo_usuario != "0" && codigo_usuario != "")
                hql += " and solicitacao.UsuarioSolicitante.Codigo=" + int.Parse(codigo_usuario);
            hql += " order by solicitacao.Data_Solicitacao desc, solicitacao.UsuarioSolicitante";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.ListaSolicitacoesDetalhadas<T>(string cnes, string tipounidade, DateTime periodoInicial, DateTime PeriodoFinal, string tipomunicipio, string municipio, string tipoprocedimento, string procedimento, string especialidade, string status, string paciente)
        {
            string hql = "select solicitacao from ViverMais.Model.Solicitacao solicitacao, ViverMais.Model.TipoProcedimento TipoProcedimento";
            hql += " where solicitacao.Data_Solicitacao between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
                + " and TO_DATE('" + PeriodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            hql += " and TipoProcedimento.Procedimento = solicitacao.Procedimento.Codigo";
            if (!string.IsNullOrEmpty(paciente))
                hql += " and solicitacao.ID_Paciente ='" + paciente + "'";
            if ((tipounidade == "S") && (cnes != "0"))
                hql += " and solicitacao.EasSolicitante ='" + cnes + "'";
            if ((tipounidade == "E") && (cnes != "0"))
                hql += " and solicitacao.Agenda.Estabelecimento='" + cnes + "'";
            if (procedimento != "0")
                hql += " and solicitacao.Procedimento.Codigo ='" + procedimento + "'";
            if (especialidade != "0")
                hql += " and solicitacao.Agenda.Cbo ='" + especialidade + "'";
            if (status != "")
                hql += " and solicitacao.Situacao ='" + status + "'";
            if (tipomunicipio != "")
                if (tipomunicipio == "1")
                    hql += " and solicitacao.UsuarioSolicitante.Unidade.MunicipioGestor.Codigo= '292740'";
                else
                    hql += " solicitacao.UsuarioSolicitante.Unidade.MunicipioGestor.Codigo ='" + municipio + "'";
            if (tipoprocedimento != "")
                hql += "  and TipoProcedimento.Tipo ='" + tipoprocedimento + "'";
            hql += " order by solicitacao.Data_Solicitacao desc";

            return Session.CreateQuery(hql).List<T>();
        }

        IList ISolicitacao.ListarQuantitativoDeSolicitacaoConfirmada(string cnes, DateTime periodoInicial, DateTime PeriodoFinal, string tipomunicipio, string municipio, string tipoprocedimento, string procedimento, string especialidade)
        {
            string hql = "select  esta.nome_fanta as Unidade, proced.no_procedimento, cbo.no_ocupacao as Especialidade, COUNT(solicitacao.situacao) as confirmada";
            hql += " from agd_solicitacao solicitacao";
            hql += " INNER JOIN ngi.agd_agenda agd ON agd.co_agenda = solicitacao.co_agenda";
            hql += " INNER JOIN NGI.pms_cnes_lfces004 esta ON agd.cnes = esta.cnes";
            hql += " INNER JOIN NGI.tb_pms_ocupacao_proc cbo ON agd.co_ocupacao = cbo.co_ocupacao";
            hql += " INNER JOIN NGI.tb_pms_procedimento proced ON solicitacao.co_procedimento = proced.co_procedimento";
            hql += " INNER JOIN NGI.agd_tipoprocedimento tipoprocedimento ON solicitacao.co_procedimento = tipoprocedimento.co_procedimento";
            hql += " where agd.Data between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
                + "and TO_DATE('" + PeriodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            if (!string.IsNullOrEmpty(tipomunicipio))
                if (tipomunicipio == "1")
                    hql += " and esta.CODMUNGEST= '292740'";
                else
                    hql += " and esta.CODMUNGEST ='" + municipio + "'";
            if (cnes != "0")
                hql += " and agd.cnes='" + cnes + "'";
            if (procedimento != "0")
                hql += " and agd.CO_PROCEDIMENTO ='" + procedimento + "'";
            if (especialidade != "0")
                hql += " and agd.CO_OCUPACAO ='" + especialidade + "'";
            if (tipoprocedimento != "")
                hql += " and tipoprocedimento.tipo = '" + tipoprocedimento + "'";
            hql += " and solicitacao.situacao= '5'";
            hql += " group by  esta.nome_fanta,  proced.no_procedimento, cbo.no_ocupacao";
            hql += " order by  esta.nome_fanta, proced.no_procedimento, cbo.no_ocupacao";

            return Session.CreateSQLQuery(hql).List();

        }

        IList ISolicitacao.ListarQuantitativoDeProducao(string cnes, DateTime periodoInicial, DateTime PeriodoFinal, string tipomunicipio, string municipio, string tipoprocedimento, string procedimento, string especialidade)
        {
            string hql = "select esta.nome_fanta as Unidade, esta.cnes, proced.no_procedimento, proced.co_procedimento, cbo.no_ocupacao as Especialidade, cbo.co_ocupacao, SUM(case when agd.Publicada = 1 then agd.Quantidade else 0 end)  as QTD_PUBLICADA, SUM(case when agd.Publicada = 1 then agd.quantidade_agendada else 0 end) as Marcadas";
            hql += " from NGI.agd_agenda agd";
            hql += " INNER JOIN NGI.pms_cnes_lfces004 esta ON agd.cnes = esta.cnes";
            hql += " INNER JOIN NGI.tb_pms_procedimento proced ON agd.co_procedimento = proced.co_procedimento";
            hql += " INNER JOIN NGI.tb_pms_ocupacao_proc cbo ON agd.co_ocupacao = cbo.co_ocupacao";
            hql += " INNER JOIN NGI.agd_tipoprocedimento tipoprocedimento ON agd.co_procedimento = tipoprocedimento.co_procedimento";
            hql += " where agd.Data between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + "00:00','DD/MM/YYYY HH24:MI')"
                + "and TO_DATE('" + PeriodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            if (!string.IsNullOrEmpty(tipomunicipio))
                if (tipomunicipio == "1")
                    hql += " and esta.CODMUNGEST= '292740'";
                else
                    hql += " and esta.CODMUNGEST ='" + municipio + "'";
            if (cnes != "0")
                hql += " and agd.cnes='" + cnes + "'";
            if (procedimento != "0")
                hql += " and agd.CO_PROCEDIMENTO ='" + procedimento + "'";
            if (especialidade != "0")
                hql += " and agd.CO_OCUPACAO ='" + especialidade + "'";
            if (tipoprocedimento != "")
                hql += " and tipoprocedimento.tipo = '" + tipoprocedimento + "'";
            hql += " group by  esta.nome_fanta , esta.cnes, proced.no_procedimento, proced.co_procedimento, cbo.no_ocupacao , cbo.co_ocupacao";
            hql += " order by  esta.nome_fanta , esta.cnes, proced.no_procedimento, proced.co_procedimento, cbo.no_ocupacao, cbo.co_ocupacao";

            return Session.CreateSQLQuery(hql).List();
        }

        IList ISolicitacao.ListarQuantitativoDeSolicitacao(string cnes, DateTime periodoInicial, DateTime PeriodoFinal, string tipomunicipio, string municipio, string tipoprocedimento, string procedimento, string especialidade)
        {
            string hql = "select estabelecimento.nome_fanta, municipio.ds_municipio, procedimento.no_procedimento, cbo.no_ocupacao, count (solicitacao.co_solicitacao) ";
            hql += " from   agd_solicitacao solicitacao";
            hql += "   INNER JOIN pms_cnes_lfces004 estabelecimento ON solicitacao.cnes_solicitante = estabelecimento.cnes";
            hql += "   INNER JOIN tb_pms_procedimento procedimento ON solicitacao.co_procedimento = procedimento.co_procedimento";
            hql += "   LEFT JOIN agd_agenda agenda ON solicitacao.co_agenda = agenda.co_agenda";
            hql += "   LEFT JOIN tb_pms_ocupacao_proc cbo ON cbo.co_ocupacao = agenda.co_ocupacao";
            hql += "   INNER JOIN tb_ms_municipio municipio ON municipio.co_municipio = estabelecimento.codmungest";
            hql += "   INNER JOIN NGI.agd_tipoprocedimento tipoprocedimento ON solicitacao.co_procedimento = tipoprocedimento.co_procedimento";
            hql += "   where solicitacao.data_solicitacao BETWEEN TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + "00:00','DD/MM/YYYY HH24:MI')"
                + "and TO_DATE('" + PeriodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            if (!string.IsNullOrEmpty(tipomunicipio))
                if (tipomunicipio == "1")
                    hql += " and estabelecimento.CODMUNGEST= '292740'";
                else
                    hql += " and estabelecimento.CODMUNGEST ='" + municipio + "'";
            if (procedimento != "0")
                hql += " and solicitacao.CO_PROCEDIMENTO ='" + procedimento + "'";
            if (especialidade != "0")
                hql += " and agenda.CO_OCUPACAO ='" + especialidade + "'";
            if (tipoprocedimento != "")
                hql += " and tipoprocedimento.tipo = '" + tipoprocedimento + "'";
            hql += "   GROUP by estabelecimento.nome_fanta, municipio.ds_municipio, procedimento.no_procedimento,  cbo.no_ocupacao";
            hql += "   order by estabelecimento.nome_fanta, municipio.ds_municipio, procedimento.no_procedimento,  cbo.no_ocupacao";

            return Session.CreateSQLQuery(hql).List();

        }


        IList<T> ISolicitacao.ListarSolicitacoesPorPeriodo<T>(DateTime periodoInicial, DateTime periodoFinal)
        {
            string hql = "from ViverMais.Model.Solicitacao solicitacao";
            hql += " where solicitacao.Data_Solicitacao between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
            + " and TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            //if (cnes != "0")
            //    hql += " and solicitacao.EasSolicitante='" + cnes + "'";
            //if (codigo_usuario != "0" && codigo_usuario != "")
            //    hql += " and solicitacao.UsuarioSolicitante.Codigo=" + int.Parse(codigo_usuario);
            hql += " order by solicitacao.Data_Solicitacao asc";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.ListarSolicitacoesPorCompetencia<T>(DateTime data, string id_procedimento, string cbo)
        {
            //string hql = "Select solicitacao.Agenda.Codigo, solicitacao.EasSolicitante, solicitacao.TipoCotaUtilizada from ViverMais.Model.Solicitacao solicitacao";
            string hql = "from ViverMais.Model.Solicitacao solicitacao";
            hql += " where TO_CHAR(solicitacao.Data_Solicitacao,'MM') = '" + data.ToString("MM") + "'";
            hql += " and TO_CHAR(solicitacao.Data_Solicitacao,'YYYY') = '" + data.ToString("yyyy") + "'";
            hql += " and solicitacao.Procedimento.Codigo='" + id_procedimento + "'";
            //hql += " and solicitacao.TipoCotaUtilizada = '"+ tipoCotaUtilizada +"'";
            //hql += " and solicitacao.Agenda IS NOT NULL";
            hql += " and solicitacao.Agenda.Cbo.Codigo = '" + cbo + "'";
            hql += " and solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA) + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        object ISolicitacao.ListarSolicitacoesParametroLocal(DateTime data, string id_procedimento, string cbo, string cnesLocal, DateTime dataInicial, DateTime dataFinal, Char tipoCota, int sub_grupo)
        {
            string hql = "Select count(solicitacao.Codigo) from ViverMais.Model.Solicitacao solicitacao";
            if (sub_grupo != 0)
                hql += " where solicitacao.Agenda.Codigo IN (Select agenda.Codigo from ViverMais.Model.Agenda agenda where agenda.Estabelecimento.CNES = '" + cnesLocal + "' and agenda.SubGrupo.Codigo = " + sub_grupo + " and agenda.Procedimento.Codigo = '" + id_procedimento + "' and agenda.Cbo.Codigo = '" + cbo + "' and agenda.Bloqueada = 0 and agenda.Publicada = 1 and agenda.Data between '" + FormatoData.ConverterData(dataInicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(dataFinal, FormatoData.nomeBanco.ORACLE) + "')";
            else
                hql += " where solicitacao.Agenda.Codigo IN (Select agenda.Codigo from ViverMais.Model.Agenda agenda where agenda.Estabelecimento.CNES = '" + cnesLocal + "' and agenda.SubGrupo IS NULL and agenda.Procedimento.Codigo = '" + id_procedimento + "' and agenda.Cbo.Codigo = '" + cbo + "' and agenda.Bloqueada = 0 and agenda.Publicada = 1 and agenda.Data between '" + FormatoData.ConverterData(dataInicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(dataFinal, FormatoData.nomeBanco.ORACLE) + "')";
            hql += " and TO_CHAR(solicitacao.Data_Solicitacao,'MM') = '" + data.Month.ToString("00") + "'";
            hql += " and TO_CHAR(solicitacao.Data_Solicitacao,'YYYY') = '" + data.Year.ToString("0000") + "'";
            hql += " and solicitacao.TipoCotaUtilizada = '" + tipoCota + "'";
            hql += " and solicitacao.EasSolicitante = '" + cnesLocal + "'";
            hql += " and solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA) + "'";
            return Session.CreateQuery(hql).SetCacheable(true).UniqueResult<object>();
        }

        //object ISolicitacao.ListarSolicitacoesParametroDistrital1(DateTime data, string id_procedimento, string cbo, string cnes, int distrito, DateTime dataInicial, DateTime dataFinal, Char tipoCota)
        //{
        //    string hql = "select count(sol.Codigo) from agd_solicitacao sol ";
        //    hql += " INNER JOIN sol.Agenda agenda";
        //    hql += " INNER JOIN ViverMais.Model.EstabelecimentoSaude estabelecimento ON estabelecimento.cnes = sol.EasSolicitante";
        //    hql += " where sol.TipoCotaUtilizada = '" + tipoCota + "'";
        //    hql += " and solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA) + "'";
        //    hql += " and estabelecimento.Bairro.Distrito.Codigo = '" + distrito + "' AND es.CNES <> '0000000' and es.CNES <> '" + cnes + "')";
        //    //hql += " and sol.CNES_SOLICITANTE <> '" + cnesLocal + "'";
        //    hql += " and TO_CHAR(sol.Data_Solicitacao,'MM') = '" + data.Month.ToString("00") + "'";
        //    hql += " and TO_CHAR(sol.Data_Solicitacao,'YYYY') = '" + data.Year.ToString("0000") + "'";
        //    hql += " and agenda.Estabelecimento.CNES = '" + cnes + "' ";
        //    hql += " and agenda.Procedimento.Codigo = '" + id_procedimento + "' ";
        //    hql += " and agenda.Cbo.Codigo = '" + cbo + "' and agenda.Bloqueada = 0 and agenda.Publicada = 1 ";
        //    hql += " and agenda.Data between '" + FormatoData.ConverterData(dataInicial, "Oracle") + "' and '" + FormatoData.ConverterData(dataFinal, "Oracle") + "'";
        //    return Session.CreateSQLQuery(hql).SetCacheable(true).UniqueResult<object>();
        //}

        object ISolicitacao.ListarSolicitacoesParametroDistrital(DateTime data, string id_procedimento, string cbo, string cnes, int distrito, DateTime dataInicial, DateTime dataFinal, Char tipoCota, int subgrupo)
        {
            string hql = "Select count(solicitacao.Codigo) from ViverMais.Model.Solicitacao solicitacao";
            if (subgrupo == 0)
                hql += " where solicitacao.Agenda.Codigo IN (Select agenda.Codigo from ViverMais.Model.Agenda agenda where agenda.Estabelecimento.CNES = '" + cnes + "' and agenda.SubGrupo IS NULL and agenda.Procedimento.Codigo = '" + id_procedimento + "' and agenda.Cbo.Codigo = '" + cbo + "' and agenda.Bloqueada = 0 and agenda.Publicada = 1 and agenda.Data between '" + FormatoData.ConverterData(dataInicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(dataFinal, FormatoData.nomeBanco.ORACLE) + "')";
            else
                hql += " where solicitacao.Agenda.Codigo IN (Select agenda.Codigo from ViverMais.Model.Agenda agenda where agenda.Estabelecimento.CNES = '" + cnes + "' and agenda.SubGrupo.Codigo = " + subgrupo + " and agenda.Procedimento.Codigo = '" + id_procedimento + "' and agenda.Cbo.Codigo = '" + cbo + "' and agenda.Bloqueada = 0 and agenda.Publicada = 1 and agenda.Data between '" + FormatoData.ConverterData(dataInicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(dataFinal, FormatoData.nomeBanco.ORACLE) + "')";
            hql += " and solicitacao.EasSolicitante IN (Select es.CNES FROM ViverMais.Model.EstabelecimentoSaude es where es.Bairro.Distrito.Codigo = '" + distrito + "' AND es.CNES <> '0000000' and es.CNES <> '" + cnes + "')";
            hql += " and TO_CHAR(solicitacao.Data_Solicitacao,'MM') = '" + data.Month.ToString("00") + "'";
            hql += " and TO_CHAR(solicitacao.Data_Solicitacao,'YYYY') = '" + data.Year.ToString("0000") + "'";
            hql += " and solicitacao.TipoCotaUtilizada = '" + tipoCota + "'";
            hql += " and solicitacao.EasSolicitante <> '" + cnes + "'";
            hql += " and solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA) + "'";
            return Session.CreateQuery(hql).SetCacheable(true).UniqueResult<object>();
        }

        object ISolicitacao.ListarSolicitacoesParametroRede(DateTime data, string id_procedimento, string cbo, string cnesLocal, DateTime dataInicial, DateTime dataFinal, Char tipoCota, int subgrupo)
        {
            string hql = "Select count(solicitacao.Codigo) from ViverMais.Model.Solicitacao solicitacao";
            hql += " Inner Join solicitacao.Agenda agenda";
            hql += " where solicitacao.TipoCotaUtilizada = '" + tipoCota + "'";
            hql += " and agenda.Estabelecimento.CNES = '" + cnesLocal + "' ";
            hql += " and agenda.Procedimento.Codigo = '" + id_procedimento + "' ";
            hql += " and agenda.Cbo.Codigo = '" + cbo + "' and agenda.Bloqueada = 0 and agenda.Publicada = 1 ";
            if (subgrupo != 0)
                hql += " and agenda.SubGrupo.Codigo = " + subgrupo;
            else
                hql += " and agenda.SubGrupo is null ";
            hql += " and agenda.Data between '" + FormatoData.ConverterData(dataInicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(dataFinal, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and solicitacao.EasSolicitante <> '" + cnesLocal + "'";
            hql += " and solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA) + "'";
            hql += " and EXTRACT(MONTH from solicitacao.Data_Solicitacao) = '" + data.ToString("MM") + "'";
            hql += " and EXTRACT(YEAR from solicitacao.Data_Solicitacao) = '" + data.ToString("yyyy") + "'";
            return Session.CreateQuery(hql).SetCacheable(true).UniqueResult<object>();
        }

        object ISolicitacao.ListarSolicitacoesParametroReservaTecnica(DateTime data, string id_procedimento, string cbo, string cnesLocal, DateTime dataInicial, DateTime dataFinal, Char tipoCota, int co_subgrupo)
        {
            string hql = "Select count(solicitacao.Codigo) from ViverMais.Model.Solicitacao solicitacao";
            hql += " Inner Join solicitacao.Agenda agenda";
            hql += " where solicitacao.TipoCotaUtilizada = '" + tipoCota + "'";
            hql += " and agenda.Estabelecimento.CNES = '" + cnesLocal + "' ";
            if (co_subgrupo != 0)
                hql += " and agenda.SubGrupo.Codigo = " + cnesLocal;
            else
                hql += " and agenda.SubGrupo is null";
            hql += " and agenda.Procedimento.Codigo = '" + id_procedimento + "' ";
            hql += " and agenda.Cbo.Codigo = '" + cbo + "' and agenda.Bloqueada = 0 and agenda.Publicada = 1 ";
            hql += " and agenda.Data between '" + FormatoData.ConverterData(dataInicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(dataFinal, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and solicitacao.EasSolicitante <> '" + cnesLocal + "'";
            hql += " and solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA) + "'";
            hql += " and EXTRACT(MONTH from solicitacao.Data_Solicitacao) = '" + data.ToString("MM") + "'";
            hql += " and EXTRACT(YEAR from solicitacao.Data_Solicitacao) = '" + data.ToString("yyyy") + "'";
            return Session.CreateQuery(hql).SetCacheable(true).UniqueResult<object>();
        }

        bool ISolicitacao.UnidadePorSolicitarParaOutra(string cnes)
        {
            return Session.CreateQuery("from ViverMais.Model.ParametroAgendaUnidade parametro where parametro.SolicitaOutrasUnidades = 1 and parametro.Estabelecimento.CNES = '" + cnes + "'").List().Count == 0 ? false : true;
        }

        IList<T> ISolicitacao.ListarSolicitacoesPorCompetencia<T>(DateTime data, string id_procedimento)
        {
            string hql = "from ViverMais.Model.Solicitacao solicitacao";
            hql += " where TO_CHAR(solicitacao.Data_Solicitacao,'MM') = '" + data.ToString("00") + "'";
            hql += " and TO_CHAR(solicitacao.Data_Solicitacao,'YYYY') = '" + data.ToString("0000") + "'";
            hql += " and solicitacao.Procedimento.Codigo ='" + id_procedimento + "'";
            hql += " and solicitacao.Agenda IS NOT NULL";
            hql += " and solicitacao.Situacao <> '" + Convert.ToChar(Solicitacao.SituacaoSolicitacao.DESMARCADA) + "'";
            //= "+data.Month;
            //between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
            //+ " and TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            ////if (cnes != "0")
            ////    hql += " and solicitacao.EasSolicitante='" + cnes + "'";
            ////if (codigo_usuario != "0" && codigo_usuario != "")

            hql += " order by solicitacao.Data_Solicitacao asc";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISolicitacao.BuscaFaixaporIdentificador<T>(string faixa)
        {
            string hql = "from ViverMais.Model.Solicitacao solicitacao";
            hql += " where solicitacao.Identificador like '%" + faixa + "%'";
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion

    }
}
