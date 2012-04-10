using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc
{
    public interface IGrupoCBO: IViverMaisServiceFacade
    {
        T BuscarPorCBO<T>(string co_cbo);
    }
}
