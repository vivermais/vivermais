﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Localidade
{
    public interface IMunicipio : IServiceFacade
    {
        IList<T> ListarPorEstado<T>(string siglaEstado);
        IList<T> BuscarPorNomeDaBahia<T>(string nome);
    }
}
