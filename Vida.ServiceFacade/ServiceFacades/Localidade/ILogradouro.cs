using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Localidade
{
    public interface ILogradouro
    {
        T BuscarPorCEP<T>(long cep);
    }
}
