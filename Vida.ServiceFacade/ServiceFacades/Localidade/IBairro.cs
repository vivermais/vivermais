﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Localidade
{
    public interface IBairro : IServiceFacade
    {
        IList<T> ListarPorCidade<T>(string co_municipio);
        IList<T> BuscarPorNome<T>(string nome);
        IList<T> BuscarPorDistrito<T>(int co_distrito);
    }
}
