﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface ISubTipoVinculacao : IServiceFacade
    {
        IList<T> BuscaPorTipoVinculacao<T>(string id_vinculacao, string id_tipoVinculacao);
        T BuscaPorTipoVinculacao<T>(string id_vinculacao, string id_tipoVinculacao, string id_SubTipoVinculacao);
        IList<T> BuscaPorTipoVinculacao<T>(string id_SubTipoVinculacao);
    }
}
