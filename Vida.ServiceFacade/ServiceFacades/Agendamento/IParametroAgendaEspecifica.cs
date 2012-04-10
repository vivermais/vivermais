using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IParametroAgendaEspecifica : IServiceFacade
    {
        T BuscarDuplicidade<T>(int id_parametroagenda, string id_unidade);
        IList<T> BuscarParametros<T>(int id_parametroagenda);
        T BuscarParametrosEspecifica<T>(int id_parametroagenda, int id_programa, string id_unidade);
    }
}
