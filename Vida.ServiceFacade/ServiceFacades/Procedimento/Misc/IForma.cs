using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc
{
    public interface IForma: IServiceFacade
    {
        IList<T> BuscarPorGrupoSubGrupo<T>(string co_grupo, string co_subgrupo);

    }
}
