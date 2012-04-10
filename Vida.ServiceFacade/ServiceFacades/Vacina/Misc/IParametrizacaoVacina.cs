using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IParametrizacaoVacina : IServiceFacade
    {
        IList<T> BuscarPorVacina<T>(int co_vacina);
        IList<T> BuscarPorDoseVacina<T>(int co_dosevacina, int co_vacina);
        IList<T> BuscaProximaDose<T>(int co_vacina, int co_dose);
        IList<T> BuscarPorDoseVacina<T>(int co_item);

        bool VerificarRequisitos(int co_vacina, int co_dose, int valor);
    }
}
