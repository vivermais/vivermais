﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface ITipoVinculacao : IServiceFacade
    {
        IList<T> BuscaPorVinculacao<T>(string id_vinculacao);
        T BuscaPorVinculacao<T>(string id_vinculacao, string id_TipoVinculo);
        //IList<T> BuscaPreparoProcedimento<T>(string id_procedimento);
    }
}
