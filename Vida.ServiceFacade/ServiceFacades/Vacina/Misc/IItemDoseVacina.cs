﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IItemDoseVacina: IServiceFacade
    {
        T BuscarItemDoseVacina<T>(int co_vacina, int co_dose);
    }
}
