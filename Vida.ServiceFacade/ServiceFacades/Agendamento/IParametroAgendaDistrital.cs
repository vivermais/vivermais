using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IParametroAgendaDistrital : IServiceFacade
    {
        T BuscarDuplicidade<T>(int id_parametroagenda, int id_distrito);
        IList<T> BuscarParametros<T>(int id_parametroagenda);
    }
}
