﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agregado
{
    public interface IAgregado : IServiceFacade
    {
       IList<T> BuscaPorSubGrupo<T>(string id_subgrupo);
    }
}
