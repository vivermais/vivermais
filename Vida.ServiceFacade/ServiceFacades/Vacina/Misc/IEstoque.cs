﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IEstoque : IServiceFacade
    {
        T BuscarItemEstoque<T>(object co_lote, object co_sala);

        IList<T> BuscarItensEstoque<T>(int co_sala, int co_vacina, int co_fabricante, int qtdaplicacao);
    }
}
