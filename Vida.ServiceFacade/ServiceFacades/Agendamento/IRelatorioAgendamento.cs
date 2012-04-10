using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IRelatorioAgendamento : IAgendamentoServiceFacade
    {
        Hashtable AgendaPrestador(string cnesExecutante, string id_procedimento, string cpf_Profissional, DateTime periodoInicial, DateTime periodoFinal);
        Hashtable SolicitacaoAmbulatorial(string cnes, DateTime periodoInicial, DateTime periodoFinal, string codigo_usuario);
        Hashtable RelatorioVagas(DateTime periodoInicial, DateTime periodoFinal);
        Hashtable ExtratoPPI(string id_municipio, string tipoRelatorio);
        Hashtable RelatorioProducaoMedicoRegulador(DateTime periodoInicial, DateTime periodoFinal, int codigo_usuario, int id_perfil);
        Hashtable RelatorioAgendaMontadaPublicada<E,P,X,C>(int competencia, E estabelecimento, P profissional, X procedimento, C cbo);
        MemoryStream Log(DateTime dataInicial, DateTime dataFinal, string cartao_sus);
        MemoryStream RelatorioSolicitacoesDesmarcadas();
        //IList<T> AgendaPrestador<T>(string cnesExecutante, string id_procedimento, string cpf_Profissional, DateTime periodoInicial, DateTime periodoFinal);
        T GerarBPAAPAC<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite);
        T GerarBPAI<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite);
        T GerarBPAC<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite);
        Hashtable SolicitacaoDetalhada(string tipounidade, string cnes, string tipomunicipio, string municipio, DateTime periodoInicial, DateTime periodoFinal, string tipoProcedimento, string procedimento, string especialidade, string status, string paciente);
        Hashtable ListarQuantitativoDeProducao(string cnes, DateTime periodoInicial, DateTime PeriodoFinal, string tipomunicipio, string municipio, string tipoprocedimento, string procedimento, string especialidade);
        Hashtable ListarQuantitativoDeSolicitacao(string cnes, DateTime periodoInicial, DateTime periodoFinal, string tipomunicipio, string municipio, string tipoprocedimento, string procedimento, string especialidade);

    }
}
