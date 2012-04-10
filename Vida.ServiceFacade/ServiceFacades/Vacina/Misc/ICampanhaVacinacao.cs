using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface ICampanhaVacinacao : IVacinaServiceFacade
    {
        IList<T> BuscarPorAno<T>(int ano);
        IList<T> BuscarItemCampanhaPorCampanha<T>(int codigoCampanha);
        IList<T> BuscarCampanhasPorPeriodo<T>(DateTime datainicio, DateTime datafim);

        T BuscarItemCampanha<T>(int co_itemvacina, int codigoCampanha);
    }
}
