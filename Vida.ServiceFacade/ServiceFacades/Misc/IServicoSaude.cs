using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Misc
{
    public interface IServicoSaude : IServiceFacade
    {
        IList<B> BuscarBairrosServicos<B,U>(IList<U> unidades);
        IList<E> BuscarEstabelecimentos<E>(int servico, string bairro);
    }
}
