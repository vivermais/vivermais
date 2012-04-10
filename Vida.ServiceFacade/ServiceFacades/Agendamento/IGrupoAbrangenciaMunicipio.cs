using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IGrupoAbrangenciaMunicipio : IViverMaisServiceFacade
    {
        T BuscarPelaChavePrimaria<T>(string id_grupoAbrangencia, string id_municipio);
    }
}
