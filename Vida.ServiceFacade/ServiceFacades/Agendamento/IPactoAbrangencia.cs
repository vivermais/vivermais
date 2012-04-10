using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IPactoAbrangencia : IServiceFacade
    {
        T BuscarPactoPorGrupoAbrangencia<T>(string id_grupo);
        IList<T> BuscarPactoAbrangenciaPorGrupoAbrangencia<T>(string id_grupo);
    }
}
