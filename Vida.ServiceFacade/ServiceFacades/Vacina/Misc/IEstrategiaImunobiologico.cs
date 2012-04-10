using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IEstrategiaImunobiologico : IVacinaServiceFacade
    {
        IList<T> BuscarPorVacina<T>(int co_vacina);
    }
}
