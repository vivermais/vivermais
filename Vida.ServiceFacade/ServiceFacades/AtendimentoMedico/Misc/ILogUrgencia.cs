﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc
{
    public interface ILogUrgencia: IServiceFacade
    {
        IList<T> BuscarPorUsuario<T>(int co_usuario);
        IList<T> BuscarPorEvento<T>(int co_evento);
        IList<T> BuscarPorData<T>(DateTime data);
    }
}