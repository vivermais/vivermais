﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.Relatorio
{
    public interface IRelatorioCNS
    {
        IList<T> PesquisarProducaoPorDistrito<T>(int id_distrito, DateTime data_inicial, DateTime data_final);
    }
}