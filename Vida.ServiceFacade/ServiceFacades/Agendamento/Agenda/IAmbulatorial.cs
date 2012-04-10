﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda
{
    public interface IAmbulatorial : IServiceFacade
    {
        IList<T> ListarAgendas<T>(string id_unidade, string competencia, string co_procedimento, string co_profissional, DateTime dataInicial, DateTime dataFinal, string co_cbo, string co_subgrupo, string turno, bool bloqueada, bool publicada);
        IList<T> ListarAgendas<T>(string id_unidade, string competencia, string co_procedimento, string co_profissional, DateTime dataInicial, DateTime dataFinal, string co_cbo, string co_subgrupo);
        IList<T> ListarUnidadesComAgendasDisponivelReservaTecnica<T>(string id_procedimento, string cbo, DateTime data_inicial, DateTime data_final, int co_subgrupo);
        IList<T> ListarUnidadesComAgendasDisponivelRede<T>(string id_procedimento, string cbo, DateTime data_inicial, DateTime data_final, string cnesLocal, int co_subgrupo);
        IList<T> ListarAgendasParametroRede<T>(string id_procedimento, string cbo, DateTime data_inicial, DateTime data_final, string cnesLocal);
        IList<T> ListarAgendasLocais<T>(string id_procedimento, string cbo, DateTime data_inicial, DateTime data_final, string cnes, int co_subgrupo);
        //IList<T> BuscarCBOPorSubGrupo<T>(string subgrupo);
        IList<T> BuscarAgendaProcedimento<T>(string co_procedimento, string cbo, DateTime data_inicial, DateTime data_final, int co_subGrupo);
        System.Data.DataTable RelatorioAgendaMontadaPublicada<E, P, X, C>(int competencia, E estabelecimento, P profissional, X procedimento, C cbo);
        IList BuscarVagas(string cbo, string subgrupo);
        IList<T> BuscarAgendaProcedimento<T>(string subgrupo, string cbo, DateTime data_inicial, DateTime data_final);
        //IList<T> ListarProcedimentosPorTipo<T>(string tipo);
        IList<T> BuscarAgendas<T>(string id_unidade, int competencia, string co_procedimento, string co_profissional, string data, int co_subgrupo);
        IList<T> BuscarAgendas<T>(string id_unidade, int competencia, string co_procedimento, string co_profissional, string data);
        IList<T> VerificarAgendas<T>(string id_unidade, string id_profissional, string data_inicial, string data_final, string turno);
        T BuscaDuplicidade<T>(string id_unidade, int competencia, string co_procedimento, string co_profissional, DateTime data, string turno);
        T BuscaQtdAgendada<T>(int id_agenda);
        T BuscarQuantidadeOfertada<T>(string procedimento, int competencia);
        //object CotaLocal<T>(string id_procedimento, string id_unidade, string cbo, string competencia);
        object ListaTotalAgendasESolicitacoes<T>(string id_procedimento, string id_unidade, string cbo, string competencia);
        IList<object> ListarVagasDisponiveis<T>(DateTime periodoInicial, DateTime periodoFinal);
    }
}
