﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IItemInventarioVacina: IServiceFacade
    {
        IList<T> BuscarItensInventario<T>(object co_lote, object co_sala);
    }
}
