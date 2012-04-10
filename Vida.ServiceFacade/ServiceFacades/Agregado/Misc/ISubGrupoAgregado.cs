﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agregado.Misc
{
    public interface ISubGrupoAgregado : IServiceFacade
    {
       IList<T> BuscaPorGrupo<T>(string id_grupo);
       //IList<T> BuscarPreparoPorProcedimento<T>(string id_procedimento);
    }
}
