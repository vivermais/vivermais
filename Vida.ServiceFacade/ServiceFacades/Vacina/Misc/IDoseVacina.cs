using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IDoseVacina : IServiceFacade
    {
        IList<T> BuscarPorVacina<T>(int co_vacina);
    }
}
