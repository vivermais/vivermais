﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IPacienteUrgence: IServiceFacade
    {
        IList<T> BuscarPorDescricao<T>(string descricao);

        P BuscarPorInicializacaoUnica<P>(string co_pacienteViverMais);
    }
}
