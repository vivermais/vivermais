using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.ServiceFacade.ServiceFacades.Vacina.FabricanteVacina
{
    public interface IFabricanteVacina: IServiceFacade
    {
        IList<T> BuscaPorNome<T>(string nome);
    }
}
