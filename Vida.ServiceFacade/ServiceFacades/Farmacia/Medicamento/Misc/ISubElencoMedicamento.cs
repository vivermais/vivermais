﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc
{
    public interface ISubElencoMedicamento : IFarmaciaServiceFacade
    {
        IList<T> BuscarMedicamentosNaoContidosNoSubElenco<T>(int id_subelenco);
        IList<T> BuscarElencosNaoContidosNoSubElenco<T>(int id_subelenco);
        IList<T> BuscarElencos<T>(int co_subelenco);
        //IList<T> BuscarPorNome<T>(string nome);
        IList<T> BuscarPorDescricao<T>(string nome);
        string ExcluirSubElencoMedicamento(int co_subelenco);
    }
}
