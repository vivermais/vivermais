using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Senhador
{
    public interface IServicoSenhador : ISenhador
    {
        T BuscarPorNome<T>(string nome);
        bool VerificarServicoEstabecimento(int co_servico, string co_unidade);
        IList<T> BuscarServicoPorEstabelecimento<T>(string co_unidade);
    }

}
