﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IPactoReferenciaSaldo : IServiceFacade
    {
        IList<T> BuscarPorPactoAgregado<T>(int id_pacto_Agregado);
        T BuscarPorPactoAgregado<T>(int id_pacto_Agregado, int mes);
    }
}
