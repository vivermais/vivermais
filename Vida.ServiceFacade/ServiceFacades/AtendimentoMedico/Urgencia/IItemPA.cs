﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IItemPA : IServiceFacade
    {
        bool ValidaCadastroPorCodigoSIGTAP<T>(string codigosigtap, int co_item);

        IList<T> BuscarItem<T>(string nome);
    }
}
