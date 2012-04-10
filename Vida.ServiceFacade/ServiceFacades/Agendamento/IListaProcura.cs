using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IListaProcura : IAgendamentoServiceFacade
    {
        IList<T> BuscaNaListaPorPacientePorProcedimento<T>(string id_paciente, string id_procedimento);
        IList<T> BuscaPorProcedimento<T>(string id_procedimento);
        IList<T> BuscaPorPeriodoPorProcedimento<T>(string periodoInicial, string periodoFinal, string id_procedimento);
        IList<T> BuscaPorAgendadoNaoAgendado<T>(string agendado);
    }
}
