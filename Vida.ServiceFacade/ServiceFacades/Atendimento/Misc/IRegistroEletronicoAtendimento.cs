﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.Atendimento
{
    public interface IRegistroEletronicoAtendimento : IAtendimentoServiceFacade
    {
        string IniciarAtendimento<U>(U usuario, string co_pacienteViverMais);
        string IniciarAtendimento<U>(U usuario, string co_pacienteViverMais, bool desacordado, string descricao, char? classificacaoAcolhimento);
        string GerarSenhaAtendimento(long co_registroEletronicoAtendimento);
        string GerarSenhaAtendimento(int codigoPrioridadeSenha, long co_registroEletronicoAtendimento, int codigoServico);
        T ResgatarSenhaSenhador<T>(int co_servico, string co_estabelecimento, int co_tiposenha, string co_paciente);
        

        void SalvarAcolhimento<T>(T _acolhimento);
        void SalvarProntuario<P, A, EM, LP, LPNF, LM, EX, EXE>(int co_usuario, P _prontuario, A _acolhimento, EM _evolucaomedica, IList<LP> _procedimentos, IList<LPNF> _procedimentosnaofaturaveis, IList<LM> _medicamentos, IList<EX> _exames, IList<EXE> _exameseletivos, bool agendarprescricao, bool ocuparvaga, char tipovaga);
        void SalvarProntuario<P, A, EM, LP, LPNF, LM, EX, EXE>(int co_usuario, P _prontuario, A _acolhimento, EM _evolucaomedica, IList<LP> _procedimentos, IList<LPNF> _procedimentosnaofaturaveis, IList<LM> _medicamentos, IList<EX> _exames, IList<EXE> _exameseletivos, bool agendarprescricao);
        /// <summary>
        /// Executa o plano de contingência com o intuito de salvar, no modo background, documentos
        /// no formato pdf de acordo com cada uma das funcionalidades de atendimento para um paciente
        /// </summary>
        /// <param name="co_usuario">código do usuário responsável pela criação</param>
        /// <param name="co_prontuario">código do registro de atendimento</param>
        void ExecutarPlanoContingencia(int co_usuario, long co_prontuario);

        T BuscarPorCodigo<T>(long codigo);
        T BuscarAcolhimento<T>(long co_prontuario);
        P BuscarRegistroAtendimentoAberto<U, P>(string co_pacienteViverMais, U unidade);
        
        T BuscarPorNumeroRegistroAtendimento<T>(long numero);
        T BuscarPorNumeroRegistroAtendimento<T>(long numero, string co_unidade);
        T BuscarPorPacienteUnidade<T>(string codigoPaciente, string co_unidade);
        T BuscarPorPacienteUnidadeDataRecepcao<T>(string codigoPaciente, string cnes, DateTime dataRecepcao);

        T BuscarProntuarioPacienteDesacordadoDesorientado<T>(int co_paciente);

        IList<T> BuscarPorUnidade<T>(string co_unidade, int co_situacao);
        IList<T> BuscarFilaAcompanhamento<T>(string co_unidade);
        IList<T> BuscarFilaAcompanhamento<T>(string co_unidade, char classificacaoacolhimento);

        IList<T> BuscarPorDataAtendimento<T>(DateTime data);
        IList<T> BuscarPorDataAtendimento<T>(DateTime data, string co_unidade);
        IList<T> BuscarPorSituacao<T>(int co_situacao);
        IList<T> BuscarPorSituacao<T>(int co_situacao, DateTime data, string co_unidade);
        IList<T> BuscarPorClassificacaoRisco<T>(int co_classificacao, DateTime data, string co_unidade);
        IList<T> BuscarFilaAtendimentoUnidade<T>(string co_unidade);
        IList<T> BuscarFilaAtendimentoUnidade<T>(string co_unidade, string co_especialidade);
        IList<T> BuscarPorPacienteViverMais<T>(string co_paciente);
        IList<T> BuscarPorPacienteViverMais<T>(string co_paciente, string co_unidade);
        IList<T> BuscarAtestadosReceitasPorProntuario<T>(long co_prontuario);
        IList<T> BuscarProntuarioPacientesSemIdentificacao<T>(string co_unidade);
        IList<T> BuscarProntuarioPacientesSemIdentificacao<T>(string co_unidade, int numero);
        IList<T> BuscarProntuarioPacientesSemIdentificacao<T>(string co_unidade, DateTime datainicialatendimento, DateTime datafinalatendimento);
        IList<T> BuscarProntuarioPacientesIdentificados<T>(string co_unidade);
        IList<T> BuscarProntuarioPacientesIdentificados<T>(string co_unidade, int numero);
        IList<T> BuscarProntuarioPacientesIdentificados<T>(string co_unidade, DateTime datainicialatendimento, DateTime datafinalatendimento);
        IList<T> BuscarProntuarioPacientesIdentificados<T>(string co_unidade, string co_pacienteViverMais);

        //Relatórios
        DataTable RetornaAtestadoReceita(long co_receitaatestado);
        DataTable RetornaDataTableAtestadosReceitas(long co_prontuario);
        DataTable RetornaDataTableExamesEletivos(long co_prontuario);
        DataTable RetornaHistoricoEnfermagem(long co_prontuario);
        DataTable RetornaHistoricoMedico(long co_prontuario);
        DataTable RetornaHistoricoSuspeitaDiagnostica(long co_prontuario);

        Hashtable RetornaHashtableProcedimentosFPO(string co_procedimento, int co_procedimentonaofaturavel, int competencia, string co_unidade);
    }
}
