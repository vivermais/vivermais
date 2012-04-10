using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Localidade
{
    public interface IDistrito : IServiceFacade
    {
        IList<T> BuscarPorMunicipio<T>(string co_municipio);
    }
}
