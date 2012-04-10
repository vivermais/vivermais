using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface ITipoSolicitacaoEstabelecimento : IServiceFacade
    {
        T BuscarTipoSolicitacaoPorEstabelecimento<T>(string cnes, string tipo);
        IList<T> BuscaPorEstabelecimento<T>(string cnes);
    }
}
