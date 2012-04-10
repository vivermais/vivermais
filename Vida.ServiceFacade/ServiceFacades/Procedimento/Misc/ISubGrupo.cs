﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc
{
    public interface ISubGrupo: IServiceFacade
    {
        IList<T> BuscarPorGrupo<T>(string co_grupo);
    }
}
