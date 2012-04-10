using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface ITipoProcedimento : IServiceFacade
    {
        T BuscaTipoProcedimento<T>(string id_procedimento);
        IList<T> BuscaProcedimentosPorPreparo<T>(int id_preparo);
        void RemoverTipoProcedimentoPorProcedimento(string id_procedimento);
        IList<T> ListarProcedimentosPorTipo<T>(string tipo);
    }
}
