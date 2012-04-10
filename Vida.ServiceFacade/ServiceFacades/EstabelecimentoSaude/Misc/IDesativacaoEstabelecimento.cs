using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude.Misc
{
    public interface IDesativacaoEstabelecimento : IServiceFacade
    {
        T PesquisarPorMotivo<T>(string ds_motivo);
    }
}
