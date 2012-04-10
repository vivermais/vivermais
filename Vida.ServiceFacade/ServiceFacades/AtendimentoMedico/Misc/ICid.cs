using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc
{
    public interface ICid: IServiceFacade
    {
        IList<string> buscaGrupos();
        IList<T> buscaCids<T>(string codigo);
        IList<T> BuscarPorCodigoCID<T>(string codigo);
    }
}
