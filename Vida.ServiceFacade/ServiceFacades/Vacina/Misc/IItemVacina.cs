﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IItemVacina : IServiceFacade
    {
        bool PermiteExclusao(int co_item);

        IList<T> ListarPorVacina<T>(int co_vacina);
        IList<T> ListarVacinas<T>();
        IList<T> ListarPorVacina<T>(int co_vacina, int co_fabricante);
        IList<T> ListarFabricantes<T>(int co_vacina);

        //int BuscarProximoRegistro();
    }
}
