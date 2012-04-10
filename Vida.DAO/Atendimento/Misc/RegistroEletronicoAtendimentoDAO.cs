﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Atendimento;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Senhador;
using ViverMais.DAO.FormatoDataOracle;

namespace ViverMais.DAO.Atendimento
{
    public class RegistroEletronicoAtendimentoDAO : AtendimentoDAO, IRegistroEletronicoAtendimento
    {


        #region IRegistroEletronicoAtendimento

        string IRegistroEletronicoAtendimento.GerarSenhaAtendimento(long co_registroEletronicoAtendimento)
        {
            throw new NotImplementedException();
        }

        string IRegistroEletronicoAtendimento.GerarSenhaAtendimento(int codigoPrioridadeSenha, long co_registroEletronicoAtendimento, int codigoServico)
        {
            //teste
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IRegistroEletronicoAtendimento iRegistroEletronicoAtendimento
                = Factory.GetInstance<IRegistroEletronicoAtendimento>();
            RegistroEletronicoAtendimento registroEletronicoAtendimento
                = iRegistroEletronicoAtendimento.BuscarPorCodigo<RegistroEletronicoAtendimento>(co_registroEletronicoAtendimento);

            //Random random = new Random();
            //string senhaAtendimento = random.Next(1000, 9999).ToString();
            SenhaSenhador senhaSenhador = Factory.GetInstance<IRegistroEletronicoAtendimento>().ResgatarSenhaSenhador<SenhaSenhador>(codigoServico, registroEletronicoAtendimento.UnidadeSaude.CNES, codigoPrioridadeSenha, registroEletronicoAtendimento.Paciente.Codigo);

            registroEletronicoAtendimento.SenhaSenhador = senhaSenhador;
            Session.Update(registroEletronicoAtendimento);
            Session.Transaction.Commit();
            if (string.IsNullOrEmpty(registroEletronicoAtendimento.SenhaAtendimento))
                return "O paciente não possui uma senha para o registro eletrônico de número: " + registroEletronicoAtendimento.NumeroToString + ".";
            else
                return registroEletronicoAtendimento.SenhaSenhador.Senha;
        }

        T IRegistroEletronicoAtendimento.ResgatarSenhaSenhador<T>(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente)
        {
            ISenhaSenhador iSenhaSenador = Factory.GetInstance<ISenhaSenhador>();
            SenhaSenhador senhaSenhador = iSenhaSenador.GerarSenhaAtendimentoPaciente<SenhaSenhador>(co_servico, co_estabelecimento, co_tiposenha, co_paciente);
            return (T)(object)senhaSenhador;
        }


        P IRegistroEletronicoAtendimento.BuscarRegistroAtendimentoAberto<U, P>(string co_pacienteViverMais, U unidade)
        {
            string hql = string.Empty;
            hql = @"FROM ViverMais.Model.RegistroEletronicoAtendimento r WHERE " +
            " r.Paciente.Codigo IS NOT NULL AND r.Paciente.Codigo = '" + co_pacienteViverMais + "' AND r.UnidadeSaude.CNES ='" + ((ViverMais.Model.EstabelecimentoSaude)(object)unidade).CNES + "'" +
            " AND (r.Situacao.Codigo = " + SituacaoRegistroEletronicoAtendimento.AGUARDANDO_ATENDIMENTO + ")";
            
            return Session.CreateQuery(hql).UniqueResult<P>();
        }

        T IRegistroEletronicoAtendimento.BuscarPorNumeroRegistroAtendimento<T>(long numero)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RegistroEletronicoAtendimento AS r WHERE r.Numero = " + numero;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IRegistroEletronicoAtendimento.BuscarPorNumeroRegistroAtendimento<T>(long numero, string co_unidade)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RegistroEletronicoAtendimento AS r WHERE r.Numero = " + numero;
            hql += " AND r.CodigoUnidade='" + co_unidade + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IRegistroEletronicoAtendimento.BuscarPorPacienteUnidade<T>(string codigoPaciente, string cnes)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RegistroEletronicoAtendimento AS r WHERE r.Paciente.Codigo = '" + codigoPaciente + "'";
            hql += " AND r.UnidadeSaude.CNES ='" + cnes + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IRegistroEletronicoAtendimento.BuscarPorPacienteUnidadeDataRecepcao<T>(string codigoPaciente, string cnes, DateTime dataRecepcao)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RegistroEletronicoAtendimento AS r WHERE r.Paciente.Codigo = '" + codigoPaciente + "'";
            hql += " AND r.UnidadeSaude.CNES ='" + cnes + "'";
            hql += " AND r.DataRecepcao ='" + FormatoData.ConverterData(dataRecepcao,FormatoData.nomeBanco.ORACLE) + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        string IRegistroEletronicoAtendimento.IniciarAtendimento<U>(U usuario, string co_pacienteViverMais)
        {
            IAtendimentoServiceFacade iAtendimento = Factory.GetInstance<IAtendimentoServiceFacade>();
            IPaciente iPacienteViverMais = Factory.GetInstance<IPaciente>();
            IRegistroEletronicoAtendimento iRegistroeletronicoAtendimento = Factory.GetInstance<IRegistroEletronicoAtendimento>();
            //IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();

            ViverMais.Model.Paciente pacienteViverMais = iPacienteViverMais.BuscarPorCodigo<ViverMais.Model.Paciente>(co_pacienteViverMais);

            Usuario usuarioAtendimento = (Usuario)(object)usuario;

            using (Session.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    //Caso ja exista um atendimento aberto é necessário finalizá-lo
                    ViverMais.Model.RegistroEletronicoAtendimento registroAtendimentoAberto = iRegistroeletronicoAtendimento.BuscarRegistroAtendimentoAberto<ViverMais.Model.EstabelecimentoSaude, ViverMais.Model.RegistroEletronicoAtendimento>(pacienteViverMais.Codigo, usuarioAtendimento.Unidade);
                    if (registroAtendimentoAberto != null)
                    {
                        registroAtendimentoAberto.Situacao = iAtendimento.BuscarPorCodigo<ViverMais.Model.SituacaoRegistroEletronicoAtendimento>(SituacaoRegistroEletronicoAtendimento.FINALIZADO);
                        registroAtendimentoAberto.DataFinalizacao = DateTime.Now;

                        Session.Update(Session.Merge(registroAtendimentoAberto));
                    }

                    ViverMais.Model.RegistroEletronicoAtendimento registroAtendimento = new ViverMais.Model.RegistroEletronicoAtendimento();

                    registroAtendimento.Paciente = pacienteViverMais;
                    registroAtendimento.Situacao = iAtendimento.BuscarPorCodigo<ViverMais.Model.SituacaoRegistroEletronicoAtendimento>(SituacaoRegistroEletronicoAtendimento.AGUARDANDO_ATENDIMENTO); //situacao 1 -> encaminha o paciente para a sala de acompanhamento

                    string codigoRegistroAtendimento = Session.CreateQuery("select max(p.Numero) from ViverMais.Model.RegistroEletronicoAtendimento p").UniqueResult<int>().ToString();

                    string mes = DateTime.Today.Month.ToString();
                    string ano = DateTime.Today.Year.ToString().Substring(2, 2);

                    if (mes.Length < 2)
                        mes = "0" + mes;

                    //gera uma nova senha para o inicio de cada mes 
                    if (codigoRegistroAtendimento.Length >= 4 && codigoRegistroAtendimento.ToString().Substring(0, 2) == ano && codigoRegistroAtendimento.ToString().Substring(2, 2) == mes)
                        registroAtendimento.Numero = int.Parse(codigoRegistroAtendimento) + 1;
                    else
                        registroAtendimento.Numero = int.Parse(ano + mes + "00001");

                    registroAtendimento.UnidadeSaude = Factory.GetInstance<IAtendimentoServiceFacade>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(usuarioAtendimento.Unidade.CNES);
                    registroAtendimento.DataRecepcao = DateTime.Now;

                    Session.Save(registroAtendimento);

                    //Session.Save(new ViverMais.Model.LogUrgencia(DateTime.Now, usuarioAtendimento.Codigo, 9, "id prontuario=" + prontuario.Codigo));

                    Session.Transaction.Commit();

                    return registroAtendimento.NumeroToString;
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        //string IRegistroEletronicoAtendimento.IniciarAtendimento<U>(U usuario, string co_pacienteViverMais, bool desacordado,
        //     string descricao, char? classificacaoAcolhimento)
        //{
        //    //ViverMais.Model.EstabelecimentoSaude unidadesaude = (ViverMais.Model.EstabelecimentoSaude)(object)UnidadeSaude;
        //    Usuario usuarioAtendimento = (Usuario)(object)usuario;
        //    IProntuario iProntuario = Factory.GetInstance<IProntuario>();
        //    IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
        //    IVagaUrgencia iVaga = Factory.GetInstance<IVagaUrgencia>();

        //    //int co_situacao = 1;

        //    using (Session.BeginTransaction(IsolationLevel.Serializable))
        //    {
        //        try
        //        {
        //            ViverMais.Model.PacienteUrgence paciente = new ViverMais.Model.PacienteUrgence();
        //            ViverMais.Model.Prontuario prontuario = new ViverMais.Model.Prontuario();

        //            if (classificacaoAcolhimento.HasValue)
        //                prontuario.ClassificacaoAcolhimento = classificacaoAcolhimento;

        //            //Vincula o código do ViverMais se tiver sido reconhecido pela biometria
        //            if (!string.IsNullOrEmpty(co_pacienteViverMais))
        //            {
        //                ViverMais.Model.Paciente pacienteViverMais = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(co_pacienteViverMais);
        //                paciente.Nome = pacienteViverMais.Nome;
        //                paciente.CodigoViverMais = pacienteViverMais.Codigo;
        //                paciente.Sexo = pacienteViverMais.Sexo;
        //                prontuario.Idade = pacienteViverMais.Idade;
        //                //iProntuario.CalculaIdade(DateTime.Today, pacienteViverMais.DataNascimento);
        //                prontuario.FaixaEtaria = Factory.GetInstance<IFaixaEtaria>().buscaPorIdade<ViverMais.Model.FaixaEtaria>(prontuario.Idade);

        //                ViverMais.Model.Prontuario prontuarioAberto = iProntuario.BuscarProntuarioAberto<ViverMais.Model.EstabelecimentoSaude, ViverMais.Model.Prontuario>(co_pacienteViverMais, usuarioAtendimento.Unidade);

        //                if (prontuarioAberto != null)
        //                {
        //                    prontuarioAberto.Situacao = iUrgencia.BuscarPorCodigo<ViverMais.Model.SituacaoAtendimento>(SituacaoAtendimento.FINALIZADO);
        //                    prontuarioAberto.DataFinalizacao = DateTime.Now;
        //                    Session.Update(Session.Merge(prontuarioAberto));

        //                    VagaUrgencia vagaOcupada = iVaga.BuscarPorProntuario<VagaUrgencia>(prontuarioAberto.Codigo);

        //                    if (vagaOcupada != null)
        //                    {
        //                        vagaOcupada.Prontuario = null;
        //                        Session.Update(vagaOcupada);
        //                    }
        //                }

        //                //if (string.IsNullOrEmpty(descricao))
        //                //{
        //                //    ViverMais.Model.PacienteUrgence paciente_urgence = Factory.GetInstance<IPacienteUrgence>().BuscarPorInicializacaoUnica<ViverMais.Model.PacienteUrgence>(pacienteViverMais.Codigo);

        //                //    if (paciente_urgence != null)
        //                //        paciente = paciente_urgence;
        //                //    else
        //                //        Session.Save(paciente);
        //                //}
        //                //else
        //                //{
        //                //    paciente.Descricao = descricao;
        //                //    Session.Save(paciente);
        //                //}
        //            }
        //            //else
        //            //{
        //            //    paciente.Descricao = descricao;
        //            //    Session.Save(paciente);
        //            //}

        //            paciente.Descricao = descricao;
        //            paciente.Simplificado = true;
        //            Session.Save(paciente);

        //            prontuario.Paciente = paciente;
        //            //prontuario.Numero = this.GerarNumeroProntuario();
        //            prontuario.Data = DateTime.Now;
        //            prontuario.CodigoUnidade = usuarioAtendimento.Unidade.CNES;

        //            if (desacordado)
        //                prontuario.Situacao = iUrgencia.BuscarPorCodigo<ViverMais.Model.SituacaoAtendimento>(SituacaoAtendimento.AGUARDANDO_ATENDIMENTO);
        //            else
        //                prontuario.Situacao = iUrgencia.BuscarPorCodigo<ViverMais.Model.SituacaoAtendimento>(SituacaoAtendimento.ATENDIMENTO_INICIAL);

        //            #region Número do prontuário
        //            string codigoprontuario = Session.CreateQuery("select max(p.Numero) from ViverMais.Model.Prontuario p").UniqueResult<int>().ToString();

        //            string mes = DateTime.Today.Month.ToString();
        //            string ano = DateTime.Today.Year.ToString().Substring(2, 2);

        //            if (mes.Length < 2)
        //                mes = "0" + mes;

        //            if (codigoprontuario.Length >= 4 && codigoprontuario.ToString().Substring(0, 2) == ano && codigoprontuario.ToString().Substring(2, 2) == mes)
        //                prontuario.Numero = int.Parse(codigoprontuario) + 1;
        //            else
        //                prontuario.Numero = int.Parse(ano + mes + "00001");
        //            #endregion

        //            if (prontuario.Situacao.Codigo == SituacaoAtendimento.AGUARDANDO_ATENDIMENTO)
        //            {
        //                prontuario.Desacordado = true;
        //                prontuario.ClassificacaoRisco = iUrgencia.BuscarPorCodigo<ViverMais.Model.ClassificacaoRisco>(ClassificacaoRisco.VERMELHO);

        //                CBO especialidadesAtendimento = iVaga.BuscarEspecialidadesAtendimento<EspecialidadeAtendimentoUrgence>(prontuario.CodigoUnidade).Where(p => p.EspecialidadePrincipal).Select(p => p.Especialidade).FirstOrDefault();

        //                if (especialidadesAtendimento == null)
        //                    throw new Exception("Não existe uma especialidade principal para o atendimento de um paciente desacordado na unidade " + usuarioAtendimento.Unidade.NomeFantasia + ". Favor, entrar em contato com a administração.");

        //                prontuario.EspecialidadeAtendimento = especialidadesAtendimento;
        //            }
        //            else
        //                prontuario.Desacordado = false;


        //            Session.Save(prontuario);
        //            Session.Save(new ViverMais.Model.LogUrgencia(DateTime.Now, usuarioAtendimento.Codigo, 9, "id prontuario=" + prontuario.Codigo));

        //            Session.Transaction.Commit();

        //            if (prontuario.Situacao.Codigo == SituacaoAtendimento.AGUARDANDO_ATENDIMENTO)
        //                this.AtualizarArquivoFilaAcolhimentoAtendimento(prontuario.CodigoUnidade, true);
        //            else
        //                this.AtualizarArquivoFilaAcolhimentoAtendimento(prontuario.CodigoUnidade, false);

        //            return prontuario.NumeroToString;
        //        }
        //        catch (Exception f)
        //        {
        //            Session.Transaction.Rollback();
        //            throw f;
        //        }
        //    }
        //}

        //T IRegistroEletronicoAtendimento.BuscarAcolhimento<T>(long co_prontuario)
        //{
        //    return Session.CreateQuery("FROM ViverMais.Model.AcolhimentoUrgence acolhimento WHERE acolhimento.Prontuario.Codigo=" + co_prontuario).UniqueResult<T>();
        //}
        //T IRegistroEletronicoAtendimento.BuscarPorCodigo<T>(long codigo)
        //{
        //    IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
        //    Prontuario prontuario = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<Prontuario>(codigo);

        //    if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
        //        prontuario.Paciente.PacienteViverMais = PacienteBLL.Pesquisar(prontuario.Paciente.CodigoViverMais);
        //    //Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

        //    return (T)(object)prontuario;
        //}

        //T IRegistroEletronicoAtendimento.BuscarProntuarioPacienteDesacordadoDesorientado<T>(int co_paciente)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE p.Paciente.Codigo = " + co_paciente;
        //    return Session.CreateQuery(hql).UniqueResult<T>();
        //}

        //void IRegistroEletronicoAtendimento.SalvarAcolhimento<T>(T _acolhimento)
        //{
        //    using (Session.BeginTransaction())
        //    {
        //        try
        //        {
        //            IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
        //            IVagaUrgencia iVaga = Factory.GetInstance<IVagaUrgencia>();

        //            AcolhimentoUrgence acolhimento = (AcolhimentoUrgence)(object)_acolhimento;
        //            ConfiguracaoPA configuracaoPA = iVaga.BuscarConfiguracaoPaPorUnidade<ConfiguracaoPA>(acolhimento.Prontuario.CodigoUnidade);

        //            if (configuracaoPA != null && configuracaoPA.FaseAcolhimento == ConfiguracaoPA.EM_IMPLANTACAO
        //                && acolhimento.Prontuario.Situacao.Codigo == SituacaoAtendimento.AGUARDANDO_ATENDIMENTO)
        //            {
        //                acolhimento.Prontuario.Situacao = iUrgencia.BuscarPorCodigo<SituacaoAtendimento>(SituacaoAtendimento.FINALIZADO);
        //                acolhimento.Prontuario.DataFinalizacao = DateTime.Now;
        //            }

        //            Session.Update(acolhimento.Prontuario);
        //            Session.Save(acolhimento);
        //            Session.Transaction.Commit();

        //            if (acolhimento.Prontuario.Situacao.Codigo == SituacaoAtendimento.AGUARDANDO_ATENDIMENTO)
        //                this.AtualizarArquivoFilaAcolhimentoAtendimento(acolhimento.Prontuario.CodigoUnidade, true);

        //            this.AtualizarArquivoFilaAcolhimentoAtendimento(acolhimento.Prontuario.CodigoUnidade, false);
        //        }
        //        catch (Exception ex)
        //        {
        //            Session.Transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}

        //void IRegistroEletronicoAtendimento.SalvarProntuario<P, A, EM, LP, LPNF, LM, EX, EXE>(int co_usuario, P _prontuario, A _acolhimento, EM _evolucaomedica, IList<LP> _procedimentos, IList<LPNF> _procedimentosnaofaturaveis,
        //    IList<LM> _medicamentos, IList<EX> _exames, IList<EXE> _exameseletivos, bool agendarprescricao, bool ocuparvaga, char tipovaga)
        //{
        //    ViverMais.Model.Prontuario prontuario = (ViverMais.Model.Prontuario)(object)_prontuario;
        //    ViverMais.Model.EvolucaoMedica evolucaomedica = (ViverMais.Model.EvolucaoMedica)(object)_evolucaomedica;
        //    AcolhimentoUrgence acolhimento = (AcolhimentoUrgence)(object)_acolhimento;
        //    IList<PrescricaoProcedimentoNaoFaturavel> procedimentosnaofaturaveis = (IList<PrescricaoProcedimentoNaoFaturavel>)(object)_procedimentosnaofaturaveis;
        //    IList<PrescricaoMedicamento> medicamentos = (IList<PrescricaoMedicamento>)(object)_medicamentos;
        //    IList<PrescricaoProcedimento> procedimentos = (IList<PrescricaoProcedimento>)(object)_procedimentos;
        //    IList<ProntuarioExame> exames = (IList<ProntuarioExame>)(object)_exames;
        //    IList<ProntuarioExameEletivo> exameseletivos = (IList<ProntuarioExameEletivo>)(object)_exameseletivos;

        //    Prescricao prescricao = null;
        //    //string codigoprofissional = string.Empty;
        //    //string cboprofissional = string.Empty;

        //    using (Session.BeginTransaction(IsolationLevel.Serializable))
        //    {
        //        try
        //        {
        //            UsuarioProfissionalUrgence usuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(co_usuario);

        //            //codigoprofissional = evolucaomedica != null ? evolucaomedica.CodigoProfissional : prontuario.ProfissionalMedico;
        //            //cboprofissional = evolucaomedica != null ? evolucaomedica.CBOProfissional : prontuario.CBOProfissionalMedico;

        //            if (prontuario.Situacao.Codigo == SituacaoAtendimento.ALTA_MEDICA || prontuario.Situacao.Codigo == SituacaoAtendimento.ALTA_PEDIDO
        //                || prontuario.Situacao.Codigo == SituacaoAtendimento.OBITO || prontuario.Situacao.Codigo == SituacaoAtendimento.EVASAO
        //                || prontuario.Situacao.Codigo == SituacaoAtendimento.TRANSFERENCIA) //Está finalizando para:. alta, alta pedida, óbito, regulado, evasão
        //                prontuario.DataFinalizacao = DateTime.Now;

        //            Session.SaveOrUpdateCopy(prontuario.Paciente);
        //            //Session.SaveOrUpdateCopy(prontuario.ClassificacaoRisco);
        //            prontuario = (Prontuario)Session.SaveOrUpdateCopy(prontuario);

        //            if (ocuparvaga)
        //            {
        //                VagaUrgencia vg = Factory.GetInstance<IVagaUrgencia>().VerificaDisponibilidadeVaga<VagaUrgencia>(tipovaga, prontuario.CodigoUnidade);

        //                if (vg != null)
        //                {
        //                    if (Factory.GetInstance<IVagaUrgencia>().BuscarPorProntuario<VagaUrgencia>(prontuario.Codigo) == null)
        //                    {
        //                        vg.Prontuario = prontuario;
        //                        Session.Update(vg);
        //                    }
        //                }
        //                else
        //                    throw new Exception("A vaga solicitada acaba de ser ocupada por outro paciente. Desculpe-nos pelo transtorno.");
        //            }
        //            else
        //            {
        //                VagaUrgencia vg = Factory.GetInstance<IVagaUrgencia>().BuscarPorProntuario<VagaUrgencia>(prontuario.Codigo);
        //                vg.Prontuario = null;
        //                Session.Update(vg);
        //            }

        //            if (evolucaomedica.PrimeiraConsulta)
        //            {
        //                if (Factory.GetInstance<IEvolucaoMedica>().BuscarConsultaMedica<EvolucaoMedica>(prontuario.Codigo) != null)
        //                    throw new Exception("O registro eletrônico de número: " + prontuario.NumeroToString + " já efetivou a primeira consulta médica.");
        //            }

        //            evolucaomedica = (EvolucaoMedica)Session.SaveOrUpdateCopy(evolucaomedica);

        //            if (evolucaomedica.PrimeiraConsulta && acolhimento != null)
        //                Session.SaveOrUpdateCopy(acolhimento);

        //            if (exames != null && exames.Count > 0)
        //            {
        //                foreach (ViverMais.Model.ProntuarioExame pex in exames)
        //                {
        //                    pex.Prontuario = prontuario;
        //                    ProntuarioExame prontuarioex = pex;
        //                    prontuarioex = (ProntuarioExame)Session.SaveOrUpdateCopy(prontuarioex);

        //                    ControleExameUrgence controle = new ControleExameUrgence();
        //                    //if (evolucaomedica != null)
        //                    controle.EvolucaoMedica = evolucaomedica;
        //                    //else
        //                    //    controle.AtendimentoMedico = prontuario;

        //                    controle.ProntuarioExame = prontuarioex;
        //                    Session.Save(controle);
        //                }
        //            }

        //            if (exameseletivos != null && exameseletivos.Count > 0)
        //            {
        //                foreach (ViverMais.Model.ProntuarioExameEletivo pex in exameseletivos)
        //                {
        //                    pex.Prontuario = prontuario;
        //                    ProntuarioExameEletivo prontuarioex = pex;
        //                    prontuarioex = (ProntuarioExameEletivo)Session.SaveOrUpdateCopy(prontuarioex);

        //                    ControleExameEletivoUrgence controle = new ControleExameEletivoUrgence();
        //                    //if (evolucaomedica != null)
        //                    controle.EvolucaoMedica = evolucaomedica;
        //                    //else
        //                    //    controle.AtendimentoMedico = prontuario;

        //                    controle.ProntuarioExame = prontuarioex;
        //                    Session.Save(controle);
        //                }
        //            }

        //            if ((procedimentosnaofaturaveis != null && procedimentosnaofaturaveis.Count() > 0) ||
        //                (medicamentos != null && medicamentos.Count() > 0) ||
        //                (procedimentos != null && procedimentos.Count() > 0))
        //            {
        //                prescricao = new Prescricao();
        //                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();

        //                if (agendarprescricao)
        //                {
        //                    prescricao.Status = Convert.ToChar(Prescricao.StatusPrescricao.Agendada);
        //                    prescricao.UltimaDataValida = iPrescricao.RetornaPrescricaoVigente<Prescricao>(prontuario.Codigo).UltimaDataValida.AddDays(1);
        //                    //iPrescricao.RetornaDataValidaPrescricao(prontuario.CodigoUnidade).AddDays(1);
        //                }
        //                else
        //                {
        //                    prescricao.Status = Convert.ToChar(Prescricao.StatusPrescricao.Valida);
        //                    prescricao.UltimaDataValida = DateTime.Now.AddDays(1);
        //                    //iPrescricao.RetornaDataValidaPrescricao(prontuario.CodigoUnidade);
        //                }

        //                prescricao.Data = DateTime.Now;
        //                prescricao.Prontuario = prontuario;
        //                prescricao.Profissional = usuarioProfissional.Id_Profissional;
        //                prescricao.CBOProfissional = usuarioProfissional.CodigoCBO;
        //                prescricao.DataVigencia = prescricao.UltimaDataValida;
        //                Session.Save(prescricao);

        //                ControlePrescricaoUrgence controle = new ControlePrescricaoUrgence();
        //                controle.Prescricao = prescricao;

        //                //if (evolucaomedica != null)
        //                controle.EvolucaoMedica = evolucaomedica;
        //                //else
        //                //    controle.AtendimentoMedico = prontuario;

        //                Session.Save(controle);

        //                IList<Prescricao> prescricoes = iPrescricao.BuscarPorProntuario<Prescricao>(prontuario.Codigo, Convert.ToChar(Prescricao.StatusPrescricao.Invalida));

        //                foreach (Prescricao presc in prescricoes)
        //                {
        //                    presc.Status = Convert.ToChar(Prescricao.StatusPrescricao.Suspensa);
        //                    Session.SaveOrUpdate(presc);
        //                }

        //                if (procedimentosnaofaturaveis != null && procedimentosnaofaturaveis.Count > 0)
        //                {
        //                    foreach (ViverMais.Model.PrescricaoProcedimentoNaoFaturavel procedimentonaofaturavel in procedimentosnaofaturaveis)
        //                    {
        //                        procedimentonaofaturavel.Prescricao = prescricao;
        //                        procedimentonaofaturavel.CodigoProfissional = usuarioProfissional.Id_Profissional;
        //                        procedimentonaofaturavel.CBOProfissional = usuarioProfissional.CodigoCBO;
        //                        procedimentonaofaturavel.Data = prescricao.Data;
        //                        procedimentonaofaturavel.Suspenso = false;
        //                        Session.SaveOrUpdateCopy(procedimentonaofaturavel);

        //                        if (procedimentonaofaturavel.ExecutarPrimeiroMomento)
        //                        {
        //                            AprazamentoProcedimentoNaoFaturavel aprazamento = new AprazamentoProcedimentoNaoFaturavel();
        //                            aprazamento.ProcedimentoNaoFaturavel = procedimentonaofaturavel.Procedimento;
        //                            aprazamento.Horario = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        //                            aprazamento.Status = Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado);
        //                            aprazamento.CodigoProfissional = prescricao.Profissional;
        //                            aprazamento.CBOProfissional = prescricao.CBOProfissional;
        //                            aprazamento.HorarioValidoPrescricao = prescricao.UltimaDataValida;
        //                            aprazamento.Prescricao = prescricao;
        //                            aprazamento.CBOProfissionalConfirmacao = prescricao.CBOProfissional;
        //                            aprazamento.CodigoProfissionalConfirmacao = prescricao.Profissional;
        //                            aprazamento.HorarioConfirmacao = DateTime.Now;
        //                            aprazamento.HorarioConfirmacaoSistema = DateTime.Now;

        //                            Session.Save(aprazamento);
        //                        }
        //                    }
        //                }

        //                if (medicamentos != null && medicamentos.Count() > 0)
        //                {
        //                    foreach (ViverMais.Model.PrescricaoMedicamento medicamento in medicamentos)
        //                    {
        //                        medicamento.Prescricao = prescricao;
        //                        medicamento.CodigoProfissional = usuarioProfissional.Id_Profissional;
        //                        medicamento.CBOProfissional = usuarioProfissional.CodigoCBO;
        //                        medicamento.Suspenso = false;
        //                        medicamento.Data = prescricao.Data;
        //                        Session.SaveOrUpdateCopy(medicamento);

        //                        if (medicamento.ExecutarPrimeiroMomento)
        //                        {
        //                            AprazamentoMedicamento aprazamento = new AprazamentoMedicamento();
        //                            aprazamento.CodigoMedicamento = medicamento.Medicamento;
        //                            aprazamento.Horario = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        //                            aprazamento.Status = Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado);
        //                            aprazamento.CodigoProfissional = prescricao.Profissional;
        //                            aprazamento.CBOProfissional = prescricao.CBOProfissional;
        //                            aprazamento.HorarioValidoPrescricao = prescricao.UltimaDataValida;
        //                            aprazamento.Prescricao = prescricao;
        //                            aprazamento.CBOProfissionalConfirmacao = prescricao.CBOProfissional;
        //                            aprazamento.CodigoProfissionalConfirmacao = prescricao.Profissional;
        //                            aprazamento.HorarioConfirmacao = DateTime.Now;
        //                            aprazamento.HorarioConfirmacaoSistema = DateTime.Now;

        //                            Session.Save(aprazamento);
        //                        }
        //                    }
        //                }

        //                if (procedimentos != null && procedimentos.Count() > 0)
        //                {
        //                    foreach (ViverMais.Model.PrescricaoProcedimento procedimento in procedimentos)
        //                    {
        //                        procedimento.Prescricao = prescricao;
        //                        procedimento.CodigoProfissional = usuarioProfissional.Id_Profissional;
        //                        procedimento.CBOProfissional = usuarioProfissional.CodigoCBO;
        //                        procedimento.Data = prescricao.Data;
        //                        procedimento.Suspenso = false;
        //                        Session.SaveOrUpdateCopy(procedimento);

        //                        if (procedimento.ExecutarPrimeiroMomento)
        //                        {
        //                            AprazamentoProcedimento aprazamento = new AprazamentoProcedimento();
        //                            aprazamento.CodigoProcedimento = procedimento.CodigoProcedimento;
        //                            aprazamento.Horario = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        //                            aprazamento.Status = Convert.ToChar(AprazamentoProcedimento.StatusItem.Finalizado);
        //                            aprazamento.CodigoProfissional = prescricao.Profissional;
        //                            aprazamento.CBOProfissional = prescricao.CBOProfissional;
        //                            aprazamento.HorarioValidoPrescricao = prescricao.UltimaDataValida;
        //                            aprazamento.Prescricao = prescricao;
        //                            aprazamento.CBOProfissionalConfirmacao = prescricao.CBOProfissional;
        //                            aprazamento.CodigoProfissionalConfirmacao = prescricao.Profissional;
        //                            aprazamento.HorarioConfirmacao = DateTime.Now;
        //                            aprazamento.HorarioConfirmacaoSistema = DateTime.Now;
        //                            aprazamento.CodigoCid = procedimento.CodigoCid;

        //                            Session.Save(aprazamento);
        //                        }
        //                    }
        //                }
        //            }

        //            Session.Transaction.Commit();

        //            if (evolucaomedica.PrimeiraConsulta)
        //                this.AtualizarArquivoFilaAcolhimentoAtendimento(prontuario.CodigoUnidade, true);
        //        }
        //        catch (Exception f)
        //        {
        //            Session.Transaction.Rollback();
        //            throw f;
        //        }
        //    }
        //}
        //void IRegistroEletronicoAtendimento.SalvarProntuario<P, A, EM, LP, LPNF, LM, EX, EXE>(int co_usuario, P _prontuario, A _acolhimento, EM _evolucaomedica, IList<LP> _procedimentos, IList<LPNF> _procedimentosnaofaturaveis,
        //    IList<LM> _medicamentos, IList<EX> _exames, IList<EXE> _exameseletivos, bool agendarprescricao)
        //{
        //    ViverMais.Model.Prontuario prontuario = (Prontuario)(object)_prontuario;
        //    IList<PrescricaoProcedimentoNaoFaturavel> procedimentosnaofaturaveis = (IList<PrescricaoProcedimentoNaoFaturavel>)(object)_procedimentosnaofaturaveis;
        //    IList<PrescricaoMedicamento> medicamentos = (IList<PrescricaoMedicamento>)(object)_medicamentos;
        //    IList<PrescricaoProcedimento> procedimentos = (IList<PrescricaoProcedimento>)(object)_procedimentos;
        //    IList<ProntuarioExame> exames = (IList<ProntuarioExame>)(object)_exames;
        //    EvolucaoMedica evolucaomedica = (EvolucaoMedica)(object)_evolucaomedica;
        //    AcolhimentoUrgence acolhimento = (AcolhimentoUrgence)(object)_acolhimento;
        //    IList<ProntuarioExameEletivo> exameseletivos = (IList<ProntuarioExameEletivo>)(object)_exameseletivos;

        //    using (Session.BeginTransaction(IsolationLevel.Serializable))
        //    {
        //        try
        //        {
        //            UsuarioProfissionalUrgence usuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(co_usuario);

        //            //string codigoprofissional = evolucaomedica != null ? evolucaomedica.CodigoProfissional : prontuario.ProfissionalMedico;
        //            //string cboprofissional = evolucaomedica != null ? evolucaomedica.CBOProfissional : prontuario.CBOProfissionalMedico;

        //            if (prontuario.Situacao.Codigo == SituacaoAtendimento.ALTA_PEDIDO || prontuario.Situacao.Codigo == SituacaoAtendimento.ALTA_MEDICA
        //                || prontuario.Situacao.Codigo == SituacaoAtendimento.OBITO || prontuario.Situacao.Codigo == SituacaoAtendimento.EVASAO
        //                || prontuario.Situacao.Codigo == SituacaoAtendimento.TRANSFERENCIA) //Está finalizando para:. alta, alta pedida, óbito, regulado, evasão
        //                prontuario.DataFinalizacao = DateTime.Now;

        //            Session.SaveOrUpdateCopy(prontuario.Paciente);
        //            //Session.SaveOrUpdateCopy(prontuario.ClassificacaoRisco);
        //            prontuario = (Prontuario)Session.SaveOrUpdateCopy(prontuario);

        //            if (evolucaomedica.PrimeiraConsulta)
        //            {
        //                if (Factory.GetInstance<IEvolucaoMedica>().BuscarConsultaMedica<EvolucaoMedica>(prontuario.Codigo) != null)
        //                    throw new Exception("O registro eletrônico de número: " + prontuario.NumeroToString + " já efetivou a primeira consulta médica.");
        //            }

        //            evolucaomedica = (EvolucaoMedica)Session.SaveOrUpdateCopy(evolucaomedica);

        //            if (evolucaomedica.PrimeiraConsulta && acolhimento != null)
        //                Session.SaveOrUpdateCopy(acolhimento);

        //            if (exames != null && exames.Count > 0)
        //            {
        //                foreach (ViverMais.Model.ProntuarioExame pex in exames)
        //                {
        //                    pex.Prontuario = prontuario;
        //                    ProntuarioExame prontuarioex = pex;
        //                    prontuarioex = (ProntuarioExame)Session.SaveOrUpdateCopy(prontuarioex);

        //                    ControleExameUrgence controle = new ControleExameUrgence();
        //                    //if (evolucaomedica != null)
        //                    controle.EvolucaoMedica = evolucaomedica;
        //                    //else
        //                    //    controle.AtendimentoMedico = prontuario;

        //                    controle.ProntuarioExame = prontuarioex;
        //                    Session.Save(controle);
        //                }
        //            }

        //            if (exameseletivos != null && exameseletivos.Count > 0)
        //            {
        //                foreach (ViverMais.Model.ProntuarioExameEletivo pex in exameseletivos)
        //                {
        //                    pex.Prontuario = prontuario;
        //                    ProntuarioExameEletivo prontuarioex = pex;
        //                    prontuarioex = (ProntuarioExameEletivo)Session.SaveOrUpdateCopy(prontuarioex);

        //                    ControleExameEletivoUrgence controle = new ControleExameEletivoUrgence();

        //                    //if (evolucaomedica != null)
        //                    controle.EvolucaoMedica = evolucaomedica;
        //                    //else
        //                    //    controle.AtendimentoMedico = prontuario;

        //                    controle.ProntuarioExame = prontuarioex;
        //                    Session.Save(controle);
        //                }
        //            }

        //            if ((procedimentosnaofaturaveis != null && procedimentosnaofaturaveis.Count() > 0) ||
        //                (medicamentos != null && medicamentos.Count() > 0) || (procedimentos != null && procedimentos.Count() > 0))
        //            {
        //                Prescricao prescricao = new Prescricao();
        //                IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();

        //                if (agendarprescricao)
        //                {
        //                    prescricao.Status = Convert.ToChar(Prescricao.StatusPrescricao.Agendada);
        //                    prescricao.UltimaDataValida = prescricao.UltimaDataValida = iPrescricao.RetornaPrescricaoVigente<Prescricao>(prontuario.Codigo).UltimaDataValida.AddDays(1);
        //                    //iPrescricao.RetornaDataValidaPrescricao(prontuario.CodigoUnidade).AddDays(1);
        //                }
        //                else
        //                {
        //                    prescricao.Status = Convert.ToChar(Prescricao.StatusPrescricao.Valida);
        //                    prescricao.UltimaDataValida = DateTime.Now.AddDays(1);
        //                    //iPrescricao.RetornaDataValidaPrescricao(prontuario.CodigoUnidade);
        //                }

        //                prescricao.Data = DateTime.Now;
        //                prescricao.Prontuario = prontuario;
        //                prescricao.Profissional = usuarioProfissional.Id_Profissional;
        //                prescricao.CBOProfissional = usuarioProfissional.CodigoCBO;
        //                prescricao.DataVigencia = prescricao.UltimaDataValida;
        //                Session.Save(prescricao);

        //                ControlePrescricaoUrgence controle = new ControlePrescricaoUrgence();
        //                controle.Prescricao = prescricao;

        //                //if (evolucaomedica != null)
        //                controle.EvolucaoMedica = evolucaomedica;
        //                //else
        //                //    controle.AtendimentoMedico = prontuario;

        //                Session.Save(controle);

        //                IList<Prescricao> prescricoes = iPrescricao.BuscarPorProntuario<Prescricao>(prontuario.Codigo, Convert.ToChar(Prescricao.StatusPrescricao.Invalida));
        //                //.Where(pt => pt.Status == Convert.ToChar(Prescricao.StatusPrescricao.Invalida)).ToList();
        //                foreach (Prescricao presc in prescricoes)
        //                {
        //                    presc.Status = Convert.ToChar(Prescricao.StatusPrescricao.Suspensa);
        //                    Session.SaveOrUpdate(presc);
        //                }

        //                if (procedimentosnaofaturaveis != null && procedimentosnaofaturaveis.Count > 0)
        //                {
        //                    foreach (ViverMais.Model.PrescricaoProcedimentoNaoFaturavel procedimentonaofaturavel in procedimentosnaofaturaveis)
        //                    {
        //                        procedimentonaofaturavel.Prescricao = prescricao;
        //                        procedimentonaofaturavel.CodigoProfissional = usuarioProfissional.Id_Profissional;
        //                        procedimentonaofaturavel.CBOProfissional = usuarioProfissional.CodigoCBO;
        //                        procedimentonaofaturavel.Data = prescricao.Data;
        //                        procedimentonaofaturavel.Suspenso = false;
        //                        Session.SaveOrUpdateCopy(procedimentonaofaturavel);

        //                        if (procedimentonaofaturavel.ExecutarPrimeiroMomento)
        //                        {
        //                            AprazamentoProcedimentoNaoFaturavel aprazamento = new AprazamentoProcedimentoNaoFaturavel();
        //                            aprazamento.ProcedimentoNaoFaturavel = procedimentonaofaturavel.Procedimento;
        //                            aprazamento.Horario = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        //                            aprazamento.Status = Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado);
        //                            aprazamento.CodigoProfissional = prescricao.Profissional;
        //                            aprazamento.CBOProfissional = prescricao.CBOProfissional;
        //                            aprazamento.HorarioValidoPrescricao = prescricao.UltimaDataValida;
        //                            aprazamento.Prescricao = prescricao;
        //                            aprazamento.CBOProfissionalConfirmacao = prescricao.CBOProfissional;
        //                            aprazamento.CodigoProfissionalConfirmacao = prescricao.Profissional;
        //                            aprazamento.HorarioConfirmacao = DateTime.Now;
        //                            aprazamento.HorarioConfirmacaoSistema = DateTime.Now;

        //                            Session.Save(aprazamento);
        //                        }
        //                    }
        //                }

        //                if (medicamentos != null && medicamentos.Count() > 0)
        //                {
        //                    foreach (ViverMais.Model.PrescricaoMedicamento medicamento in medicamentos)
        //                    {
        //                        medicamento.Prescricao = prescricao;
        //                        medicamento.CodigoProfissional = usuarioProfissional.Id_Profissional;
        //                        medicamento.CBOProfissional = usuarioProfissional.CodigoCBO;
        //                        medicamento.Data = prescricao.Data;
        //                        medicamento.Suspenso = false;
        //                        Session.SaveOrUpdateCopy(medicamento);

        //                        if (medicamento.ExecutarPrimeiroMomento)
        //                        {
        //                            AprazamentoMedicamento aprazamento = new AprazamentoMedicamento();
        //                            aprazamento.CodigoMedicamento = medicamento.Medicamento;
        //                            aprazamento.Horario = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        //                            aprazamento.Status = Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado);
        //                            aprazamento.CodigoProfissional = prescricao.Profissional;
        //                            aprazamento.CBOProfissional = prescricao.CBOProfissional;
        //                            aprazamento.HorarioValidoPrescricao = prescricao.UltimaDataValida;
        //                            aprazamento.Prescricao = prescricao;
        //                            aprazamento.CBOProfissionalConfirmacao = prescricao.CBOProfissional;
        //                            aprazamento.CodigoProfissionalConfirmacao = prescricao.Profissional;
        //                            aprazamento.HorarioConfirmacao = DateTime.Now;
        //                            aprazamento.HorarioConfirmacaoSistema = DateTime.Now;

        //                            Session.Save(aprazamento);
        //                        }
        //                    }
        //                }

        //                if (procedimentos != null && procedimentos.Count > 0)
        //                {
        //                    foreach (ViverMais.Model.PrescricaoProcedimento procedimento in procedimentos)
        //                    {
        //                        procedimento.Prescricao = prescricao;
        //                        procedimento.CodigoProfissional = usuarioProfissional.Id_Profissional;
        //                        procedimento.CBOProfissional = usuarioProfissional.CodigoCBO;
        //                        procedimento.Data = prescricao.Data;
        //                        procedimento.Suspenso = false;
        //                        Session.SaveOrUpdateCopy(procedimento);

        //                        if (procedimento.ExecutarPrimeiroMomento)
        //                        {
        //                            AprazamentoProcedimento aprazamento = new AprazamentoProcedimento();
        //                            aprazamento.CodigoProcedimento = procedimento.CodigoProcedimento;
        //                            aprazamento.Horario = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
        //                            aprazamento.Status = Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado);
        //                            aprazamento.CodigoProfissional = prescricao.Profissional;
        //                            aprazamento.CBOProfissional = prescricao.CBOProfissional;
        //                            aprazamento.HorarioValidoPrescricao = prescricao.UltimaDataValida;
        //                            aprazamento.Prescricao = prescricao;
        //                            aprazamento.CBOProfissionalConfirmacao = prescricao.CBOProfissional;
        //                            aprazamento.CodigoProfissionalConfirmacao = prescricao.Profissional;
        //                            aprazamento.HorarioConfirmacao = DateTime.Now;
        //                            aprazamento.HorarioConfirmacaoSistema = DateTime.Now;
        //                            aprazamento.CodigoCid = procedimento.CodigoCid;

        //                            Session.Save(aprazamento);
        //                        }
        //                    }
        //                }
        //            }

        //            Session.Transaction.Commit();

        //            if (evolucaomedica.PrimeiraConsulta)
        //                this.AtualizarArquivoFilaAcolhimentoAtendimento(prontuario.CodigoUnidade, true);
        //        }
        //        catch (Exception f)
        //        {
        //            Session.Transaction.Rollback();
        //            throw f;
        //        }
        //    }
        //}
        //void IRegistroEletronicoAtendimento.ExecutarPlanoContingencia(int co_usuario, long co_prontuario)
        //{
        //    try
        //    {
        //        IProntuario iProntuario = Factory.GetInstance<IProntuario>();
        //        Prontuario prontuario = iProntuario.BuscarPorCodigo<Prontuario>(co_prontuario);
        //        IRelatorioUrgencia iRelatorio = Factory.GetInstance<IRelatorioUrgencia>();

        //        ConfiguracaoContingenciaUrgence contigencia = Session.CreateQuery("FROM ViverMais.Model.ConfiguracaoContingenciaUrgence config WHERE config.Unidade.CNES='" + prontuario.CodigoUnidade + "'").UniqueResult<ConfiguracaoContingenciaUrgence>();

        //        if (contigencia != null)
        //        {
        //            ReportDocument relatorio = null;
        //            string pastaprincipal = prontuario.NumeroToString;

        //            if (prontuario.Paciente.PacienteViverMais != null)
        //            {
        //                string cartaosus = CartaoSUSBLL.ListarPorPaciente(prontuario.Paciente.PacienteViverMais)
        //                    .Select(p => long.Parse(p.Numero)).Min().ToString();

        //                pastaprincipal += "_" + cartaosus;
        //            }

        //            try
        //            {
        //                contigencia.CriarPastaPrincipal(pastaprincipal);
        //            }
        //            catch { }

        //            try
        //            {
        //                contigencia.CriarSubPastas(pastaprincipal);
        //            }
        //            catch { }

        //            try
        //            {
        //                relatorio = iRelatorio.ObterRelatorioAcolhimento(prontuario.Codigo);

        //                if (relatorio != null)
        //                    contigencia.SalvarDocumento(relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_ACOLHIMENTO + "/acolhimento.pdf");
        //            }
        //            catch { }


        //            try
        //            {
        //                byte[] documento = this.RertornarPdfAtestadosExames(iProntuario.RetornaDataTableAtestadosReceitas(co_prontuario), "Nenhum(a) atestado/receita encontrado.");

        //                contigencia.SalvarDocumento(documento, pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_ATESTADOSRECEITAS + "/atestadosreceitas.pdf");
        //            }
        //            catch
        //            {
        //                throw;
        //            }

        //            try
        //            {
        //                relatorio = iRelatorio.ObterRelatorioConsultaMedica(prontuario.Codigo);

        //                if (relatorio != null)
        //                    contigencia.SalvarDocumento(relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_CONSULTAMEDICA + "/consultamedica.pdf");
        //            }
        //            catch { }

        //            try
        //            {
        //                relatorio = iRelatorio.ObterRelatorioEvolucoesEnfermagem(prontuario.Codigo);

        //                if (relatorio != null)
        //                    contigencia.SalvarDocumento(relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_EVOLUCOESENFERMAGEM + "/evolucoesenfermagem.pdf");
        //            }
        //            catch { }

        //            try
        //            {
        //                relatorio = iRelatorio.ObterRelatorioEvolucoesMedicas(prontuario.Codigo);

        //                if (relatorio != null)
        //                    contigencia.SalvarDocumento(relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_EVOLUCOESMEDICAS + "/evolucoesmedicas.pdf");
        //            }
        //            catch { }

        //            try
        //            {
        //                byte[] documento = this.RertornarPdfAtestadosExames(iProntuario.RetornaDataTableExamesEletivos(co_prontuario), "Nenhum exame eletivo encontrado.");

        //                contigencia.SalvarDocumento(documento, pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_EXAMESELETIVOS + "/exameseletivos.pdf");
        //            }
        //            catch
        //            {
        //                throw;
        //            }

        //            try
        //            {
        //                relatorio = iRelatorio.ObterRelatorioExamesInternos(prontuario.Codigo);

        //                if (relatorio != null)
        //                    contigencia.SalvarDocumento(relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_EXAMESINTERNOS + "/examesinternos.pdf");
        //            }
        //            catch { }

        //            try
        //            {
        //                relatorio = iRelatorio.ObterRelatorioFichaAtendimento(prontuario.Numero);

        //                if (relatorio != null)
        //                    contigencia.SalvarDocumento(relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_FICHAATENDIMENTO + "/fichaatendimento.pdf");
        //            }
        //            catch { }

        //            try
        //            {
        //                relatorio = iRelatorio.ObterRelatorioPrescricoes(prontuario.Codigo);

        //                if (relatorio != null)
        //                    contigencia.SalvarDocumento(relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_PRESCRICOES + "/prescricoes.pdf");
        //            }
        //            catch { }

        //            try
        //            {
        //                relatorio = iRelatorio.ObterRelatorioAprazados(prontuario.Codigo);

        //                if (relatorio != null)
        //                    contigencia.SalvarDocumento(relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_APRAZAMENTOS + "/aprazamentos.pdf");
        //            }
        //            catch { }

        //            try
        //            {
        //                relatorio = iRelatorio.ObterRelatorioGeral(prontuario.Codigo, co_usuario);

        //                if (relatorio != null)
        //                    contigencia.SalvarDocumento(relatorio.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), pastaprincipal + "/" + ConfiguracaoContingenciaUrgence.DIR_RELATORIOGERAL + "/relatoriogeral.pdf");
        //            }
        //            catch { }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //IList<T> IRegistroEletronicoAtendimento.BuscarPorUnidade<T>(string co_unidade, int co_situacao)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario p WHERE p.CodigoUnidade = '" + co_unidade + "' AND p.Situacao.Codigo = " + co_situacao + " ORDER BY p.Data";
        //    IList<Prontuario> prontuarios = Session.CreateQuery(hql).List<Prontuario>();
        //    IPaciente iPaciente = Factory.GetInstance<IPaciente>();

        //    foreach (Prontuario prontuario in prontuarios)
        //    {
        //        if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
        //            prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);
        //    }

        //    return (List<T>)(object)prontuarios;
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarFilaAcompanhamento<T>(string co_unidade)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE p.Situacao.Codigo = " + SituacaoAtendimento.ATENDIMENTO_INICIAL.ToString() +
        //          " AND p.CodigoUnidade = '" + co_unidade + "' ORDER BY p.Data, p.Numero";

        //    IList<Prontuario> prontuarios = Session.CreateQuery(hql).List<Prontuario>();
        //    IPaciente iPaciente = Factory.GetInstance<IPaciente>();

        //    foreach (Prontuario prontuario in prontuarios)
        //    {
        //        if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
        //            prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);
        //    }

        //    return (IList<T>)(object)prontuarios;
        //}

        //IList<T> IRegistroEletronicoAtendimento.BuscarFilaAcompanhamento<T>(string co_unidade, char classificacaoacolhimento)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE p.Situacao.Codigo = " + SituacaoAtendimento.ATENDIMENTO_INICIAL.ToString() +
        //          " AND p.CodigoUnidade = '" + co_unidade + "' AND p.ClassificacaoAcolhimento IS NOT NULL AND p.ClassificacaoAcolhimento = '" +
        //          classificacaoacolhimento
        //          + "' ORDER BY p.Data, p.Numero";

        //    IList<Prontuario> prontuarios = Session.CreateQuery(hql).List<Prontuario>();
        //    IPaciente iPaciente = Factory.GetInstance<IPaciente>();

        //    foreach (Prontuario prontuario in prontuarios)
        //    {
        //        if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
        //            prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);
        //    }

        //    return (IList<T>)(object)prontuarios;
        //}

        //IList<T> IRegistroEletronicoAtendimento.BuscarFilaAtendimentoUnidade<T>(string co_unidade)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario prontuario WHERE prontuario.Situacao.Codigo = " + SituacaoAtendimento.AGUARDANDO_ATENDIMENTO +
        //                " AND prontuario.CodigoUnidade = '" + co_unidade +
        //                "'";
        //    return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarFilaAtendimentoUnidade<T>(string co_unidade, string co_especialidade)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario prontuario WHERE prontuario.Situacao.Codigo = " + SituacaoAtendimento.AGUARDANDO_ATENDIMENTO +
        //                " AND prontuario.CodigoUnidade = '" + co_unidade + "' AND prontuario.EspecialidadeAtendimento IS NOT NULL AND prontuario.EspecialidadeAtendimento.Codigo='" + co_especialidade + "'";
        //    //hql += " ORDER BY prontuario.ClassificacaoRisco.Ordem, prontuario.DataAcolhimento";
        //    IList<Prontuario> prontuarios = (IList<Prontuario>)(object)Session.CreateQuery(hql).List<T>();

        //    IPaciente iPaciente = Factory.GetInstance<IPaciente>();

        //    foreach (Prontuario prontuario in prontuarios)
        //    {
        //        if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
        //            prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);
        //    }

        //    return (IList<T>)(object)prontuarios.OrderBy(p => p.ClassificacaoRisco.Ordem).ThenByDescending(p => p.EsperaFilaAtendimento).ToList();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarProntuarioPacientesSemIdentificacao<T>(string co_unidade)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE ";
        //    hql += " p.CodigoUnidade='" + co_unidade + "'";
        //    hql += " AND (p.Paciente.CodigoViverMais IS NULL OR TRIM(p.Paciente.CodigoViverMais) = ' ')";
        //    hql += " ORDER BY p.Data";
        //    return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarProntuarioPacientesSemIdentificacao<T>(string co_unidade, int numero)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE ";
        //    hql += " p.CodigoUnidade='" + co_unidade + "'";
        //    hql += " AND (p.Paciente.CodigoViverMais IS NULL OR TRIM(p.Paciente.CodigoViverMais) = ' ')";
        //    hql += " AND p.Numero = " + numero;
        //    hql += " ORDER BY p.Data";
        //    return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarProntuarioPacientesSemIdentificacao<T>(string co_unidade, DateTime datainicialatendimento, DateTime datafinalatendimento)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE ";
        //    hql += " p.CodigoUnidade='" + co_unidade + "'";
        //    hql += " AND (p.Paciente.CodigoViverMais IS NULL OR TRIM(p.Paciente.CodigoViverMais) = ' ')";
        //    hql += " AND p.Data BETWEEN TO_DATE('" + datainicialatendimento.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI') ";
        //    hql += " AND TO_DATE('" + datafinalatendimento.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
        //    hql += " ORDER BY p.Data";

        //    return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarProntuarioPacientesIdentificados<T>(string co_unidade)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE ";
        //    hql += " p.CodigoUnidade='" + co_unidade + "'";
        //    hql += " AND (p.Paciente.CodigoViverMais IS NOT NULL OR TRIM(p.Paciente.CodigoViverMais) <> ' ')";
        //    //hql += " AND p.Paciente.Descricao IS NOT NULL";
        //    hql += " AND p.Paciente.Simplificado = 'Y'";
        //    hql += " ORDER BY p.Data";

        //    IList<Prontuario> prontuarios = Session.CreateQuery(hql).List<Prontuario>();
        //    IPaciente iPaciente = Factory.GetInstance<IPaciente>();

        //    foreach (Prontuario prontuario in prontuarios)
        //        prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

        //    return (IList<T>)(object)prontuarios;
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarProntuarioPacientesIdentificados<T>(string co_unidade, string co_pacienteViverMais)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE ";
        //    hql += " p.CodigoUnidade='" + co_unidade + "'";
        //    hql += " AND p.Paciente.CodigoViverMais IS NOT NULL AND p.Paciente.CodigoViverMais='" + co_pacienteViverMais + "'";
        //    //hql += " AND p.Paciente.Descricao IS NOT NULL";
        //    hql += " AND p.Paciente.Simplificado = 'Y'";
        //    hql += " ORDER BY p.Data";

        //    IList<Prontuario> prontuarios = Session.CreateQuery(hql).List<Prontuario>();
        //    IPaciente iPaciente = Factory.GetInstance<IPaciente>();

        //    foreach (Prontuario prontuario in prontuarios)
        //        prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

        //    return (IList<T>)(object)prontuarios;

        //    //return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarProntuarioPacientesIdentificados<T>(string co_unidade, int numero)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE ";
        //    hql += " p.CodigoUnidade='" + co_unidade + "'";
        //    hql += " AND (p.Paciente.CodigoViverMais IS NOT NULL OR TRIM(p.Paciente.CodigoViverMais) <> ' ')";
        //    //hql += " AND p.Paciente.Descricao IS NOT NULL";
        //    hql += " AND p.Paciente.Simplificado = 'Y'";
        //    hql += " AND p.Numero = " + numero;
        //    hql += " ORDER BY p.Data";

        //    IList<Prontuario> prontuarios = Session.CreateQuery(hql).List<Prontuario>();
        //    IPaciente iPaciente = Factory.GetInstance<IPaciente>();

        //    foreach (Prontuario prontuario in prontuarios)
        //        prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

        //    return (IList<T>)(object)prontuarios;

        //    //return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarProntuarioPacientesIdentificados<T>(string co_unidade, DateTime datainicialatendimento, DateTime datafinalatendimento)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE ";
        //    hql += " p.CodigoUnidade='" + co_unidade + "'";
        //    hql += " AND (p.Paciente.CodigoViverMais IS NOT NULL OR TRIM(p.Paciente.CodigoViverMais) <> ' ')";
        //    //hql += " AND p.Paciente.Descricao IS NOT NULL";
        //    hql += " AND p.Paciente.Simplificado = 'Y'";
        //    hql += " AND p.Data BETWEEN TO_DATE('" + datainicialatendimento.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI') ";
        //    hql += " AND TO_DATE('" + datafinalatendimento.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
        //    hql += " ORDER BY p.Data";

        //    IList<Prontuario> prontuarios = Session.CreateQuery(hql).List<Prontuario>();
        //    IPaciente iPaciente = Factory.GetInstance<IPaciente>();

        //    foreach (Prontuario prontuario in prontuarios)
        //        prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

        //    return (IList<T>)(object)prontuarios;

        //    //return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarPorDataAtendimento<T>(DateTime data)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE TO_CHAR(p.Data,'DD/MM/YYYY') = '" + data.ToString("dd/MM/yyyy") + "'";
        //    return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarPorDataAtendimento<T>(DateTime data, string co_unidade)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE TO_CHAR(p.Data,'DD/MM/YYYY') = '" + data.ToString("dd/MM/yyyy") + "'";
        //    hql += " AND p.CodigoUnidade ='" + co_unidade + "'";
        //    return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarPorSituacao<T>(int co_situacao)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE p.Situacao.Codigo = " + co_situacao;
        //    return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarPorSituacao<T>(int co_situacao, DateTime data, string co_unidade)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE p.Situacao.Codigo = " + co_situacao;
        //    hql += " AND to_char(p.Data,'DD/MM/YYYY') = '" + data.ToString("dd/MM/yyyy") + "'";
        //    hql += " AND p.CodigoUnidade='" + co_unidade + "'";
        //    return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarPorClassificacaoRisco<T>(int co_classificacao, DateTime data, string co_unidade)
        //{
        //    string hql = "from ViverMais.Model.Prontuario p where p.ClassificacaoRisco.Codigo = " + co_classificacao;
        //    hql += " AND to_char(p.Data,'DD/MM/YYYY') = '" + data.ToString("dd/MM/yyyy") + "' AND p.CodigoUnidade ='" + co_unidade + "'";
        //    return Session.CreateQuery(hql).List<T>();
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarPorPacienteViverMais<T>(string co_paciente)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE p.Paciente.CodigoViverMais IS NOT NULL AND p.Paciente.CodigoViverMais = '" + co_paciente + "'";
        //    hql += " ORDER BY p.Codigo";

        //    IList<Prontuario> prontuarios = (IList<Prontuario>)(object)Session.CreateQuery(hql).List<T>();
        //    IPaciente iPaciente = Factory.GetInstance<IPaciente>();

        //    foreach (Prontuario prontuario in prontuarios)
        //        prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

        //    return (IList<T>)prontuarios;
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarPorPacienteViverMais<T>(string co_paciente, string co_unidade)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.Prontuario AS p WHERE p.Paciente.CodigoViverMais IS NOT NULL AND p.Paciente.CodigoViverMais = '" + co_paciente + "' AND p.CodigoUnidade='" + co_unidade + "'";
        //    hql += " ORDER BY p.Codigo";

        //    IList<Prontuario> prontuarios = (IList<Prontuario>)(object)Session.CreateQuery(hql).List<T>();

        //    IPaciente iPaciente = Factory.GetInstance<IPaciente>();
        //    foreach (Prontuario prontuario in prontuarios)
        //        prontuario.Paciente.PacienteViverMais = iPaciente.BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);

        //    return (IList<T>)prontuarios;
        //}
        //IList<T> IRegistroEletronicoAtendimento.BuscarAtestadosReceitasPorProntuario<T>(long co_prontuario)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.AtestadoReceitaUrgence AS ar WHERE ar.Prontuario.Codigo = " + co_prontuario;
        //    return Session.CreateQuery(hql).List<T>();
        //}

        //private DataTable RetornarCorpoAtestadoReceitaExameEletivo()
        //{
        //    DataTable tab = new DataTable();
        //    tab.Columns.Add("Codigo", typeof(string));
        //    tab.Columns.Add("Paciente", typeof(string));
        //    tab.Columns.Add("CartaoSUS", typeof(string));
        //    tab.Columns.Add("RG", typeof(string));
        //    tab.Columns.Add("NumeroAtendimento", typeof(string));
        //    tab.Columns.Add("EstabelecimentoAtendimento", typeof(string));
        //    tab.Columns.Add("CNES", typeof(string));
        //    tab.Columns.Add("Conteudo", typeof(string));
        //    tab.Columns.Add("NomeProfissional", typeof(string));
        //    tab.Columns.Add("CRMProfissional", typeof(string));
        //    tab.Columns.Add("TipoDocumento", typeof(string));

        //    return tab;
        //}
        //private DataTable DataTableAtestadoReceita(DataTable tab, AtestadoReceitaUrgence ar)
        //{
        //    DataRow linha = tab.NewRow();
        //    string nomepaciente = string.Empty;
        //    string rg = string.Empty;
        //    string cartaosus = string.Empty;

        //    if (string.IsNullOrEmpty(ar.Prontuario.Paciente.CodigoViverMais))
        //    {
        //        nomepaciente = ar.Prontuario.Paciente.Nome;
        //        rg = " - ";
        //        cartaosus = " - ";
        //    }
        //    else
        //    {
        //        ViverMais.Model.Paciente paciente = PacienteBLL.PesquisarCompleto(ar.Prontuario.Paciente.CodigoViverMais);
        //        //ViverMais.Model.Paciente pac = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(ar.Prontuario.Paciente.CodigoViverMais);
        //        nomepaciente = paciente.Nome;
        //        List<ViverMais.Model.Documento> documentos = DocumentoBLL.PesqusiarPorPaciente(paciente); //ipaciente.ListarDocumentos<ViverMais.Model.Documento>(paciente.Codigo);
        //        ViverMais.Model.Documento documento = (from _documento in documentos
        //                                          where
        //                                          int.Parse(_documento.ControleDocumento.TipoDocumento.Codigo) == 10
        //                                          select _documento).FirstOrDefault();
        //        //Documento doc = Factory.GetInstance<IPaciente>().BuscarDocumento<Documento>("10", paciente.Codigo);

        //        if (documento != null)
        //            rg = !string.IsNullOrEmpty(documento.Numero) ? documento.Numero : " - ";
        //        else
        //            rg = " - ";

        //        IList<ViverMais.Model.CartaoSUS> cartoes = Factory.GetInstance<IPaciente>().ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo);
        //        cartaosus = cartoes.Last().Numero;
        //    }

        //    ViverMais.Model.EstabelecimentoSaude unidadesaude = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(ar.Prontuario.CodigoUnidade);

        //    linha["Codigo"] = ar.Codigo;
        //    linha["Paciente"] = nomepaciente;
        //    linha["RG"] = rg;
        //    linha["CartaoSUS"] = cartaosus;
        //    linha["CNES"] = unidadesaude.CNES;
        //    linha["NumeroAtendimento"] = ar.Prontuario.NumeroToString;
        //    linha["EstabelecimentoAtendimento"] = unidadesaude.NomeFantasia;

        //    linha["Conteudo"] = HttpUtility.HtmlDecode(ar.Conteudo);
        //    VinculoProfissional vinculo = Factory.GetInstance<IVinculo>().BuscarPorVinculoProfissional<VinculoProfissional>(ar.Prontuario.CodigoUnidade, ar.CodigoProfissional, ar.CBOProfissional).FirstOrDefault();
        //    linha["NomeProfissional"] = vinculo.Profissional.Nome;
        //    linha["CRMProfissional"] = string.IsNullOrEmpty(vinculo.RegistroConselho) ? "CRM Não Identificado" : vinculo.RegistroConselho;

        //    if (ar.TipoAtestadoReceita.Codigo == TipoAtestadoReceita.Atestado)
        //        linha["TipoDocumento"] = "Atestado";
        //    else if (ar.TipoAtestadoReceita.Codigo == TipoAtestadoReceita.Comparecimento)
        //        linha["TipoDocumento"] = "Comparecimento";
        //    else
        //        linha["TipoDocumento"] = "Receita";

        //    tab.Rows.Add(linha);

        //    return tab;
        //}
        //private byte[] RertornarPdfAtestadosExames(DataTable documentos, string msgDocumentoVazio)
        //{
        //    MemoryStream memory = new MemoryStream();
        //    Document document = new Document(PageSize.A5, 1, 1, 1, 1);
        //    PdfWriter.GetInstance(document, memory);
        //    document.Open();

        //    if (documentos.Rows.Count > 0)
        //    {
        //        int entreLinhasCorpoDocumento = 2;

        //        Font fonteLabelCabecalho = new Font(Font.NORMAL, 11f, Font.BOLD);
        //        Font fonteValorCabecalho = new Font(Font.NORMAL, 11f);
        //        Font fonteAssinatura = new Font(Font.NORMAL, 12f, Font.BOLD);
        //        Font fonteLocalData = new Font(Font.NORMAL, 12f);

        //        foreach (DataRow row in documentos.Rows)
        //        {
        //            document.NewPage();

        //            Paragraph pContentDocumento = new Paragraph(new Phrase(string.Empty));

        //            #region Imagem Cabeçalho
        //            string pathImagemCabecalho = string.Empty;

        //            switch (row["TipoDocumento"].ToString())
        //            {
        //                case "Atestado":
        //                    pathImagemCabecalho = "topo_atestado.jpg";
        //                    break;
        //                case "Receita":
        //                    pContentDocumento = new Paragraph(new Phrase("R/"));
        //                    pContentDocumento.Add(new Phrase("\n"));
        //                    pathImagemCabecalho = "topo_receita.jpg";
        //                    break;
        //                case "Comparecimento":
        //                    pathImagemCabecalho = "topo_comparecimento.jpg";
        //                    break;
        //                case "ExameEletivo":
        //                    pathImagemCabecalho = "topo_exames_eletivos.jpg";
        //                    break;
        //            }

        //            Image imagemCabecalho = Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~/Urgencia/img/" + pathImagemCabecalho));
        //            imagemCabecalho.Alignment = Element.ALIGN_CENTER;
        //            document.Add(imagemCabecalho);
        //            #endregion

        //            #region Dados Cabeçalho
        //            PdfPTable topoCabecalho = new PdfPTable(1);
        //            topoCabecalho.WidthPercentage = 100;
        //            topoCabecalho.HorizontalAlignment = Element.ALIGN_LEFT;

        //            topoCabecalho.AddCell(this.RetornarCelulaPdfFormatada(this.RetornarContentPdfFormatado("Nome: ", fonteLabelCabecalho, row["Paciente"].ToString(), fonteValorCabecalho), PdfPCell.NO_BORDER, Element.ALIGN_LEFT));
        //            topoCabecalho.AddCell(this.RetornarCelulaPdfFormatada(this.RetornarContentPdfFormatado("CNS: ", fonteLabelCabecalho, row["CartaoSUS"].ToString(), fonteValorCabecalho), PdfPCell.NO_BORDER, Element.ALIGN_LEFT));
        //            topoCabecalho.AddCell(this.RetornarCelulaPdfFormatada(this.RetornarContentPdfFormatado("RG: ", fonteLabelCabecalho, row["RG"].ToString(), fonteValorCabecalho), PdfPCell.NO_BORDER, Element.ALIGN_LEFT));
        //            topoCabecalho.AddCell(this.RetornarCelulaPdfFormatada(this.RetornarContentPdfFormatado("Unidade: ", fonteLabelCabecalho, row["EstabelecimentoAtendimento"].ToString(), fonteValorCabecalho), PdfPCell.NO_BORDER, Element.ALIGN_LEFT));
        //            topoCabecalho.AddCell(this.RetornarCelulaPdfFormatada(this.RetornarContentPdfFormatado("CNES: ", fonteLabelCabecalho, row["CNES"].ToString(), fonteValorCabecalho), PdfPCell.NO_BORDER, Element.ALIGN_LEFT));
        //            topoCabecalho.AddCell(this.RetornarCelulaPdfFormatada(this.RetornarContentPdfFormatado("Número de Atendimento: ", fonteLabelCabecalho, row["NumeroAtendimento"].ToString(), fonteValorCabecalho), PdfPCell.NO_BORDER, Element.ALIGN_LEFT));

        //            document.Add(topoCabecalho);
        //            #endregion

        //            #region Corpo Documento
        //            for (int i = 0; i < entreLinhasCorpoDocumento; i++)
        //                document.Add(new Paragraph(new Phrase("\n")));

        //            ArrayList corpoPdf = HTMLWorker.ParseToList(new StringReader(HttpUtility.HtmlDecode(row["Conteudo"].ToString()).Replace("&nbsp", "")), null);
        //            document.Add(pContentDocumento);

        //            for (int i = 0; i < corpoPdf.Count; i++)
        //                document.Add((IElement)corpoPdf[i]);

        //            for (int i = 0; i < entreLinhasCorpoDocumento; i++)
        //                document.Add(new Paragraph(new Phrase("\n")));
        //            #endregion

        //            #region Local e Data
        //            Paragraph pLocalData = new Paragraph(this.RetornarContentPdfFormatado(string.Empty, fonteLocalData, "Salvador, " + DateTime.Today.ToString("dd/MM/yyyy"), fonteLocalData));
        //            pLocalData.Alignment = Element.ALIGN_LEFT;

        //            document.Add(pLocalData);
        //            document.Add(new Paragraph(new Phrase("\n")));
        //            #endregion

        //            #region Assinatura Profissional
        //            PdfPTable rodapeAssinatura = new PdfPTable(1);
        //            rodapeAssinatura.WidthPercentage = 100;
        //            rodapeAssinatura.HorizontalAlignment = Element.ALIGN_CENTER;

        //            rodapeAssinatura.AddCell(this.RetornarCelulaPdfFormatada(this.RetornarContentPdfFormatado(string.Empty, fonteAssinatura, row["NomeProfissional"].ToString(), fonteAssinatura), PdfPCell.NO_BORDER, Element.ALIGN_CENTER));
        //            rodapeAssinatura.AddCell(this.RetornarCelulaPdfFormatada(this.RetornarContentPdfFormatado("CRM-BA: ", fonteAssinatura, row["CRMProfissional"].ToString(), fonteAssinatura), PdfPCell.NO_BORDER, Element.ALIGN_CENTER));

        //            document.Add(rodapeAssinatura);
        //            #endregion

        //            #region Imagem Rodapé
        //            Image imagemRodape = Image.GetInstance(System.Web.Hosting.HostingEnvironment.MapPath("~/Urgencia/img/rodape.jpg"));
        //            imagemRodape.Alignment = Element.ALIGN_CENTER;
        //            document.Add(imagemRodape);
        //            #endregion
        //        }
        //    }
        //    else
        //    {
        //        Paragraph pDocumentoVazio = new Paragraph(new Phrase(msgDocumentoVazio));
        //        pDocumentoVazio.Alignment = Element.ALIGN_CENTER;
        //        document.Add(pDocumentoVazio);
        //    }

        //    document.Close();

        //    return memory.ToArray();
        //}
        //private Phrase RetornarContentPdfFormatado(string label, Font fonteLabel, string valor, Font fonteValor)
        //{
        //    Phrase valorColuna = new Phrase();

        //    Chunk lbColuna = new Chunk(label, fonteLabel);
        //    valorColuna.Add(lbColuna);

        //    lbColuna = new Chunk(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(valor.ToLower()), fonteValor);
        //    valorColuna.Add(lbColuna);

        //    return valorColuna;
        //}
        //private PdfPCell RetornarCelulaPdfFormatada(Phrase content, int borda, int alinhamentoHorizontal)
        //{
        //    PdfPCell celula = new PdfPCell(content);
        //    celula.HorizontalAlignment = alinhamentoHorizontal;
        //    celula.Border = borda;

        //    return celula;
        //}
        ///// <summary>
        ///// Atualiza o arquivo da fila de atendimento ou acolhimento indicando que houve mudança
        ///// na mesma
        ///// </summary>
        ///// <param name="cnes">cnes da unidade</param>
        ///// <param name="arquivoAtendimento">atualizar arquivo de atendimento?</param>
        //private void AtualizarArquivoFilaAcolhimentoAtendimento(string cnes, bool arquivoAtendimento)
        //{
        //    try
        //    {
        //        string arquivo = "FilaAcolhimento";

        //        if (arquivoAtendimento)
        //            arquivo = "FilaAtendimento";

        //        var stream = new FileStream(HttpContext.Current.Request.MapPath("~/Urgencia/Documentos/" + arquivo + "/" + cnes + ".txt"), FileMode.Truncate, FileAccess.Write, FileShare.Write);
        //        TextWriter escrita = new StreamWriter(stream);
        //        escrita.WriteLine('S');
        //        escrita.Flush();
        //        stream.Close();
        //    }
        //    catch { }
        //}

        //DataTable IRegistroEletronicoAtendimento.RetornaHistoricoEnfermagem(long co_prontuario)
        //{
        //    IList<EvolucaoEnfermagem> evolucoes = Factory.GetInstance<IEvolucaoEnfermagem>().BuscarPorProntuario<EvolucaoEnfermagem>(co_prontuario).OrderByDescending(p => p.Data).ToList();

        //    DataTable tab = new DataTable();
        //    tab.Columns.Add("Profissional", typeof(string));
        //    tab.Columns.Add("CBO", typeof(string));
        //    tab.Columns.Add("Conteudo", typeof(string));
        //    tab.Columns.Add("Data", typeof(string));
        //    DataRow row;
        //    IProfissional iProfissional = Factory.GetInstance<IProfissional>();
        //    ICBO iCBO = Factory.GetInstance<ICBO>();

        //    foreach (EvolucaoEnfermagem ev in evolucoes)
        //    {
        //        row = tab.NewRow();
        //        row["Profissional"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(ev.CodigoProfissional).Nome;
        //        row["CBO"] = iCBO.BuscarPorCodigo<CBO>(ev.CBOProfissional).Nome;
        //        row["Conteudo"] = ev.Observacao;
        //        row["Data"] = ev.Data.ToString("dd/MM/yyyy HH:mm:ss");
        //        tab.Rows.Add(row);
        //    }

        //    return tab;
        //}
        //DataTable IRegistroEletronicoAtendimento.RetornaAtestadoReceita(long co_receitaatestado)
        //{
        //    return DataTableAtestadoReceita(RetornarCorpoAtestadoReceitaExameEletivo(), Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<AtestadoReceitaUrgence>(co_receitaatestado));
        //}
        //DataTable IRegistroEletronicoAtendimento.RetornaHistoricoMedico(long co_prontuario)
        //{
        //    Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(co_prontuario);
        //    IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
        //    IProfissional iProfissional = Factory.GetInstance<IProfissional>();
        //    ICBO iCBO = Factory.GetInstance<ICBO>();
        //    IEvolucaoMedica iEvolucao = Factory.GetInstance<IEvolucaoMedica>();

        //    DataTable tab = new DataTable();
        //    tab.Columns.Add("Profissional", typeof(string));
        //    tab.Columns.Add("CBO", typeof(string));
        //    tab.Columns.Add("Data", typeof(string));
        //    tab.Columns.Add("Conteudo", typeof(string));
        //    DataRow linha;

        //    IList<EvolucaoMedica> evolucoes = iEvolucao.BuscarPorProntuario<EvolucaoMedica>(prontuario.Codigo).OrderByDescending(p => p.Data).ToList();

        //    EvolucaoMedica consultamedica = iEvolucao.BuscarConsultaMedica<EvolucaoMedica>(prontuario.Codigo);

        //    if (consultamedica != null)
        //        evolucoes.Add(consultamedica);

        //    evolucoes = evolucoes.OrderBy(p => p.Data).ToList();

        //    foreach (EvolucaoMedica ev in evolucoes)
        //    {
        //        linha = tab.NewRow();
        //        linha["Profissional"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(ev.CodigoProfissional).Nome;
        //        linha["CBO"] = iCBO.BuscarPorCodigo<CBO>(ev.CBOProfissional).Nome;
        //        linha["Conteudo"] = ev.Observacao;
        //        linha["Data"] = ev.Data.ToString("dd/MM/yyyy HH:mm:ss");
        //        tab.Rows.Add(linha);
        //    }

        //    //linha = tab.NewRow();
        //    //linha["Data"] = prontuario.DataConsultaMedica.HasValue ? prontuario.DataConsultaMedica.Value.ToString("dd/MM/yyyy HH:mm:ss") : "-";
        //    //linha["Conteudo"] = string.IsNullOrEmpty(prontuario.Anamnese) ? " - " : prontuario.Anamnese;
        //    //linha["CBO"] = iCBO.BuscarPorCodigo<CBO>(prontuario.CBOProfissionalMedico).Nome;
        //    //linha["Profissional"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(prontuario.ProfissionalMedico).Nome;
        //    //tab.Rows.Add(linha);

        //    return tab;
        //}
        //DataTable IRegistroEletronicoAtendimento.RetornaHistoricoSuspeitaDiagnostica(long co_prontuario)
        //{
        //    Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(co_prontuario);
        //    IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
        //    IProfissional iProfissional = Factory.GetInstance<IProfissional>();
        //    ICBO iCBO = Factory.GetInstance<ICBO>();
        //    ICid iCid = Factory.GetInstance<ICid>();
        //    IEvolucaoMedica iEvolucao = Factory.GetInstance<IEvolucaoMedica>();

        //    DataTable tab = new DataTable();
        //    tab.Columns.Add("Profissional", typeof(string));
        //    tab.Columns.Add("CBO", typeof(string));
        //    tab.Columns.Add("Data", typeof(string));
        //    tab.Columns.Add("Conteudo", typeof(string));
        //    DataRow linha;

        //    IList<EvolucaoMedica> evolucoes = iEvolucao.BuscarPorProntuario<EvolucaoMedica>(prontuario.Codigo).OrderByDescending(p => p.Data).ToList();

        //    EvolucaoMedica consultamedica = iEvolucao.BuscarConsultaMedica<EvolucaoMedica>(prontuario.Codigo);

        //    if (consultamedica != null)
        //        evolucoes.Add(consultamedica);

        //    evolucoes = evolucoes.OrderBy(p => p.Data).ToList();

        //    StringBuilder builder;
        //    int i;

        //    foreach (EvolucaoMedica ev in evolucoes)
        //    {
        //        linha = tab.NewRow();
        //        linha["Profissional"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(ev.CodigoProfissional).Nome;
        //        linha["CBO"] = iCBO.BuscarPorCodigo<CBO>(ev.CBOProfissional).Nome;

        //        builder = new StringBuilder();
        //        i = 1;
        //        foreach (string codigocid in ev.CodigosCids)
        //        {
        //            Cid cid = iCid.BuscarPorCodigo<Cid>(codigocid);
        //            builder.Append("(" + i.ToString() + ") " + cid.Codigo + " - " + cid.Nome + Environment.NewLine);
        //            i++;
        //        }

        //        linha["Conteudo"] = builder.ToString();
        //        linha["Data"] = ev.Data.ToString("dd/MM/yyyy HH:mm:ss");
        //        tab.Rows.Add(linha);
        //    }

        //    //linha = tab.NewRow();
        //    //linha["Data"] = prontuario.DataConsultaMedica.HasValue ? prontuario.DataConsultaMedica.Value.ToString("dd/MM/yyyy HH:mm:ss") : "-";

        //    //builder = new StringBuilder();
        //    //i = 1;
        //    //foreach (string codigocid in prontuario.CodigosCids)
        //    //{
        //    //    Cid cid = iCid.BuscarPorCodigo<Cid>(codigocid);
        //    //    builder.Append("(" + i.ToString() + ") " + cid.Codigo + " - " + cid.Nome + Environment.NewLine);
        //    //    i++;
        //    //}

        //    //linha["Conteudo"] = builder.ToString();
        //    //linha["CBO"] = iCBO.BuscarPorCodigo<CBO>(prontuario.CBOProfissionalMedico).Nome;
        //    //linha["Profissional"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(prontuario.ProfissionalMedico).Nome;
        //    //tab.Rows.Add(linha);

        //    return tab;
        //}
        //DataTable IRegistroEletronicoAtendimento.RetornaDataTableAtestadosReceitas(long co_prontuario)
        //{
        //    IList<AtestadoReceitaUrgence> lista = Factory.GetInstance<IProntuario>().BuscarAtestadosReceitasPorProntuario<AtestadoReceitaUrgence>(co_prontuario);

        //    DataTable atestadoreceita = RetornarCorpoAtestadoReceitaExameEletivo();

        //    foreach (AtestadoReceitaUrgence ar in lista)
        //        atestadoreceita = DataTableAtestadoReceita(atestadoreceita, ar);

        //    return atestadoreceita;
        //}
        //DataTable IRegistroEletronicoAtendimento.RetornaDataTableExamesEletivos(long co_prontuario)
        //{
        //    DataTable tab = this.RetornarCorpoAtestadoReceitaExameEletivo();

        //    string nomepaciente = string.Empty;
        //    string cartaosus = string.Empty;
        //    string rg = string.Empty;
        //    string numeroatendimento = string.Empty;
        //    string estabelecimento = string.Empty;
        //    string cnes = string.Empty;

        //    Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(co_prontuario);
        //    numeroatendimento = prontuario.Numero.ToString();
        //    nomepaciente = prontuario.NomePacienteToString;

        //    if (prontuario.Paciente.PacienteViverMais != null)
        //    {
        //        //ViverMais.Model.Paciente pac = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);
        //        //nomepaciente = pac.Nome;

        //        List<ViverMais.Model.Documento> documentos = DocumentoBLL.PesqusiarPorPaciente(prontuario.Paciente.PacienteViverMais); //ipaciente.ListarDocumentos<ViverMais.Model.Documento>(paciente.Codigo);
        //        ViverMais.Model.Documento documento = (from _documento in documentos
        //                                          where
        //                                          int.Parse(_documento.ControleDocumento.TipoDocumento.Codigo) == 10
        //                                          select _documento).FirstOrDefault();

        //        if (documento != null)
        //            rg = !string.IsNullOrEmpty(documento.Numero) ? documento.Numero : " - ";
        //        else
        //            rg = " - ";

        //        IList<ViverMais.Model.CartaoSUS> cartoes = CartaoSUSBLL.ListarPorPaciente(prontuario.Paciente.PacienteViverMais);
        //        //Factory.GetInstance<IPaciente>().ListarCartoesSUS<ViverMais.Model.CartaoSUS>(prontuario.Paciente.PacienteViverMais.Codigo);
        //        cartaosus = cartoes.Last().Numero;
        //    }
        //    else
        //    {
        //        //nomepaciente = prontuario.Paciente.Nome;
        //        rg = " - ";
        //        cartaosus = " - ";
        //    }

        //    ViverMais.Model.EstabelecimentoSaude unidadesaude = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(prontuario.CodigoUnidade);
        //    estabelecimento = unidadesaude.NomeFantasia;
        //    cnes = unidadesaude.CNES;

        //    //IList<ControleExameEletivoUrgence> controlesatendimento = Factory.GetInstance<IExame>().BuscarControlesEletivosDoAtendimentoMedicamento<ControleExameEletivoUrgence>(prontuario.Codigo);
        //    //var itematendimento = from item in controlesatendimento
        //    //                      group item by item.AtendimentoMedico.Codigo
        //    //                          into result
        //    //                          select result;
        //    //;

        //    IList<ControleExameEletivoUrgence> controlesevolucao = Factory.GetInstance<IExame>().BuscarControlesEletivosDaEvolucaoMedica<ControleExameEletivoUrgence>(prontuario.Codigo);
        //    var itemevolucao = from item in controlesevolucao
        //                       group item by item.EvolucaoMedica.Codigo
        //                           into result
        //                           select result;

        //    Hashtable hash = new Hashtable();
        //    //hash.Add(0, itematendimento.ToList());
        //    hash.Add(0, itemevolucao.ToList());

        //    int qtd = hash.Count;
        //    IVinculo iVinculo = Factory.GetInstance<IVinculo>();

        //    for (int i = 0; i < qtd; i++)
        //    {
        //        foreach (var item in (IEnumerable<IGrouping<long, ControleExameEletivoUrgence>>)hash[i])
        //        {
        //            DataRow row = tab.NewRow();

        //            row["Codigo"] = item.Key.ToString();
        //            row["TipoDocumento"] = "ExameEletivo";
        //            row["NumeroAtendimento"] = numeroatendimento;
        //            row["EstabelecimentoAtendimento"] = estabelecimento;
        //            row["CNES"] = cnes;

        //            row["Paciente"] = nomepaciente;
        //            row["CartaoSUS"] = cartaosus;
        //            row["RG"] = rg;

        //            string conteudo = string.Empty;

        //            ProntuarioExameEletivo primeiroexame = item.First().ProntuarioExame;

        //            conteudo += "O médico abaixo solicitou no dia " + primeiroexame.Data.ToString("dd/MM/yyyy") + " a realização dos seguintes exames: </br>";

        //            conteudo += "<ul>";
        //            IList<ExameEletivo> exameseletivos = item.ToList().Select(p => p.ProntuarioExame.Exame).ToList();

        //            foreach (ExameEletivo exame in exameseletivos)
        //            {
        //                conteudo += "<li>";
        //                conteudo += exame.Descricao;
        //                conteudo += "</li>";
        //            }

        //            conteudo += "</ul>";
        //            row["Conteudo"] = conteudo;

        //            VinculoProfissional vinculo = iVinculo.BuscarPorVinculoProfissional<VinculoProfissional>(cnes, primeiroexame.Profissional, primeiroexame.CBOProfissional).FirstOrDefault();
        //            row["NomeProfissional"] = vinculo.Profissional.Nome;
        //            row["CRMProfissional"] = string.IsNullOrEmpty(vinculo.RegistroConselho) ? "CRM Não Identificado" : vinculo.RegistroConselho;

        //            tab.Rows.Add(row);
        //        }
        //    }

        //    return tab;
        //}
        //Hashtable IRegistroEletronicoAtendimento.RetornaHashtableProcedimentosFPO(string co_procedimento, int co_procedimentonaofaturavel, int competencia, string co_unidade)
        //{
        //    Hashtable hash = new Hashtable();
        //    DataRow row;

        //    DataTable cab = new DataTable();
        //    cab.Columns.Add("CODIGOUNIDADE", typeof(string));
        //    cab.Columns.Add("UNIDADE", typeof(string));
        //    cab.Columns.Add("COMPETENCIA", typeof(string));

        //    DataTable lproc = new DataTable();
        //    lproc.Columns.Add("CODIGOUNIDADE", typeof(string));
        //    lproc.Columns.Add("PROCEDIMENTO", typeof(string));
        //    lproc.Columns.Add("CODIGOPROCEDIMENTO", typeof(string));
        //    lproc.Columns.Add("PROGRAMADO", typeof(string));
        //    lproc.Columns.Add("EXECUTADO", typeof(string));

        //    DataTable lprocnf = new DataTable();
        //    lprocnf.Columns.Add("CODIGOUNIDADE", typeof(string));
        //    lprocnf.Columns.Add("PROCEDIMENTO", typeof(string));
        //    lprocnf.Columns.Add("EXECUTADO", typeof(string));

        //    row = cab.NewRow();
        //    row["CODIGOUNIDADE"] = co_unidade;
        //    row["COMPETENCIA"] = competencia;
        //    row["UNIDADE"] = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(co_unidade).NomeFantasia;
        //    cab.Rows.Add(row);

        //    IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
        //    IUrgenciaServiceFacade iUrgencia = Factory.GetInstance<IUrgenciaServiceFacade>();
        //    IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();

        //    if (co_procedimento != "-1")
        //    {
        //        IList<FPO> fpos = Factory.GetInstance<IFPO>().BuscarFPO<FPO>(co_unidade, competencia); //Procedimentos programados
        //        IList<Solicitacao> solicitacoes = Factory.GetInstance<ISolicitacao>().ListarSolicitacoesConfirmadas<Solicitacao>(competencia, co_unidade, DateTime.Now, DateTime.Now); //Procedimentos executados no CYGNUS
        //        IList<PrescricaoProcedimento> procedimentos = iPrescricao.ListarProcedimentosFPOUnidade<PrescricaoProcedimento>(false, co_unidade, competencia); //Procedimentos executados no URGENCE

        //        if (co_procedimento != "0")
        //            fpos = fpos.Where(p => p.ID_Procedimento == co_procedimento.ToString()).ToList();

        //        foreach (FPO fpo in fpos)
        //        {
        //            int qtdexecutado = solicitacoes.Where(p => p.Procedimento.Codigo == fpo.ID_Procedimento).Count() + procedimentos.Where(p => p.CodigoProcedimento == fpo.ID_Procedimento).Count();
        //            row = lproc.NewRow();
        //            row["CODIGOUNIDADE"] = co_unidade;
        //            row["CODIGOPROCEDIMENTO"] = fpo.ID_Procedimento;
        //            row["PROCEDIMENTO"] = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(fpo.ID_Procedimento).Nome;
        //            row["PROGRAMADO"] = fpo.QTD_Total;
        //            row["EXECUTADO"] = qtdexecutado;
        //            lproc.Rows.Add(row);
        //        }
        //    }

        //    if (co_procedimentonaofaturavel > -1)
        //    {
        //        IList<PrescricaoProcedimentoNaoFaturavel> procedimentosnaofaturaveis = iPrescricao.ListarProcedimentosFPOUnidade<PrescricaoProcedimentoNaoFaturavel>(true, co_unidade, competencia); //Procedimentos executados no URGENCE

        //        if (co_procedimentonaofaturavel != 0)
        //        {
        //            int count_procnf = procedimentosnaofaturaveis.Where(p => p.Procedimento.Codigo == co_procedimentonaofaturavel).Count();
        //            row = lprocnf.NewRow();
        //            row["CODIGOUNIDADE"] = co_unidade;
        //            row["PROCEDIMENTO"] = iUrgencia.BuscarPorCodigo<ViverMais.Model.ProcedimentoNaoFaturavel>(co_procedimentonaofaturavel).Nome;
        //            row["EXECUTADO"] = count_procnf;
        //            lprocnf.Rows.Add(row);

        //            //lprocnf.Add(new { PROCEDIMENTO = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ProcedimentoNaoFaturavel>(co_procedimentonaofaturavel).Nome, EXECUTADO = count_procnf });
        //        }
        //        else
        //        {
        //            var consulta = from pp in procedimentosnaofaturaveis
        //                           group pp by
        //                               pp.Procedimento.Codigo into pg
        //                           select new { Codigo = pg.Key, Quantidade = pg.Count() };

        //            foreach (var item in consulta)
        //            {
        //                row = lprocnf.NewRow();
        //                row["CODIGOUNIDADE"] = co_unidade;
        //                row["PROCEDIMENTO"] = iUrgencia.BuscarPorCodigo<ViverMais.Model.ProcedimentoNaoFaturavel>(item.Codigo).Nome;
        //                row["EXECUTADO"] = item.Quantidade;
        //                lprocnf.Rows.Add(row);

        //                //lprocnf.Add(new { CODIGOUNIDADE = co_unidade, PROCEDIMENTO = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ProcedimentoNaoFaturavel>(item.Codigo).Nome, EXECUTADO = item.Quantidade });
        //            }
        //        }
        //    }

        //    hash.Add("cabecalho", cab);
        //    hash.Add("proc", lproc);
        //    hash.Add("procnf", lprocnf);

        //    return hash;
        //}

        #endregion

        #region IRegistroEletronicoAtendimento Members

        public string IniciarAtendimento<U>(U usuario, string co_pacienteViverMais, char classificacaoAcolhimento)
        {
            throw new NotImplementedException();
        }

        public string IniciarAtendimento<U>(U usuario, string co_pacienteViverMais, bool desacordado, string descricao, char? classificacaoAcolhimento)
        {
            throw new NotImplementedException();
        }

        public string GerarSenhaAcolhimento(long co_prontuario)
        {
            throw new NotImplementedException();
        }

        public void SalvarAcolhimento<T>(T _acolhimento)
        {
            throw new NotImplementedException();
        }

        public void SalvarProntuario<P, A, EM, LP, LPNF, LM, EX, EXE>(int co_usuario, P _prontuario, A _acolhimento, EM _evolucaomedica, IList<LP> _procedimentos, IList<LPNF> _procedimentosnaofaturaveis, IList<LM> _medicamentos, IList<EX> _exames, IList<EXE> _exameseletivos, bool agendarprescricao, bool ocuparvaga, char tipovaga)
        {
            throw new NotImplementedException();
        }

        public void SalvarProntuario<P, A, EM, LP, LPNF, LM, EX, EXE>(int co_usuario, P _prontuario, A _acolhimento, EM _evolucaomedica, IList<LP> _procedimentos, IList<LPNF> _procedimentosnaofaturaveis, IList<LM> _medicamentos, IList<EX> _exames, IList<EXE> _exameseletivos, bool agendarprescricao)
        {
            throw new NotImplementedException();
        }

        public void ExecutarPlanoContingencia(int co_usuario, long co_prontuario)
        {
            throw new NotImplementedException();
        }

        public T BuscarPorCodigo<T>(long codigo)
        {
            throw new NotImplementedException();
        }

        public T BuscarAcolhimento<T>(long co_prontuario)
        {
            throw new NotImplementedException();
        }

        public P BuscarProntuarioAberto<U, P>(string co_pacienteViverMais, U unidade)
        {
            throw new NotImplementedException();
        }

        public T BuscarPorNumeroProntuario<T>(long numero)
        {
            throw new NotImplementedException();
        }

        public T BuscarPorNumeroProntuario<T>(long numero, string co_unidade)
        {
            throw new NotImplementedException();
        }

        public T BuscarProntuarioPacienteDesacordadoDesorientado<T>(int co_paciente)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarPorUnidade<T>(string co_unidade, int co_situacao)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarFilaAcompanhamento<T>(string co_unidade)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarFilaAcompanhamento<T>(string co_unidade, char classificacaoacolhimento)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarPorDataAtendimento<T>(DateTime data)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarPorDataAtendimento<T>(DateTime data, string co_unidade)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarPorSituacao<T>(int co_situacao)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarPorSituacao<T>(int co_situacao, DateTime data, string co_unidade)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarPorClassificacaoRisco<T>(int co_classificacao, DateTime data, string co_unidade)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarFilaAtendimentoUnidade<T>(string co_unidade)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarFilaAtendimentoUnidade<T>(string co_unidade, string co_especialidade)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarPorPacienteViverMais<T>(string co_paciente)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarPorPacienteViverMais<T>(string co_paciente, string co_unidade)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarAtestadosReceitasPorProntuario<T>(long co_prontuario)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarProntuarioPacientesSemIdentificacao<T>(string co_unidade)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarProntuarioPacientesSemIdentificacao<T>(string co_unidade, int numero)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarProntuarioPacientesSemIdentificacao<T>(string co_unidade, DateTime datainicialatendimento, DateTime datafinalatendimento)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarProntuarioPacientesIdentificados<T>(string co_unidade)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarProntuarioPacientesIdentificados<T>(string co_unidade, int numero)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarProntuarioPacientesIdentificados<T>(string co_unidade, DateTime datainicialatendimento, DateTime datafinalatendimento)
        {
            throw new NotImplementedException();
        }

        public IList<T> BuscarProntuarioPacientesIdentificados<T>(string co_unidade, string co_pacienteViverMais)
        {
            throw new NotImplementedException();
        }

        public DataTable RetornaAtestadoReceita(long co_receitaatestado)
        {
            throw new NotImplementedException();
        }

        public DataTable RetornaDataTableAtestadosReceitas(long co_prontuario)
        {
            throw new NotImplementedException();
        }

        public DataTable RetornaDataTableExamesEletivos(long co_prontuario)
        {
            throw new NotImplementedException();
        }

        public DataTable RetornaHistoricoEnfermagem(long co_prontuario)
        {
            throw new NotImplementedException();
        }

        public DataTable RetornaHistoricoMedico(long co_prontuario)
        {
            throw new NotImplementedException();
        }

        public DataTable RetornaHistoricoSuspeitaDiagnostica(long co_prontuario)
        {
            throw new NotImplementedException();
        }

        public Hashtable RetornaHashtableProcedimentosFPO(string co_procedimento, int co_procedimentonaofaturavel, int competencia, string co_unidade)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
