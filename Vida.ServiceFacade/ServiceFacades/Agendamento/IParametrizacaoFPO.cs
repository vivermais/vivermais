﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IParametrizacaoFPO : IServiceFacade
    {
        IList<T> BuscaParametrizacaoPorUnidade<T>(string cnes, string procedimento);

    }
}
