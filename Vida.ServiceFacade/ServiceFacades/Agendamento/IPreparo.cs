using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IPreparo : IServiceFacade
    {
       T BuscaPreparo<T>(int id_preparo);
       IList<T> BuscarPreparoPorProcedimento<T>(string id_procedimento);
    }
}
