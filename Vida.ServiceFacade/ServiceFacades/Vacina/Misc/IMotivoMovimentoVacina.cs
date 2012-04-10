using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IMotivoMovimentoVacina : IServiceFacade
    {
        IList<T> BuscarPorTipoMovimento<T>(int co_tipomovimento);
    }
}
