﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc
{
    public interface IElencoMedicamento : IFarmaciaServiceFacade
    {
        IList<T> BuscarMedicamentosNaoContidosNoElenco<T>(int id_elenco);
        IList<T> BuscarSubElencosNaoContidosNoElenco<T>(int id_elenco);
        IList<T> BuscarElencosPorFarmacia<T>(int id_farmacia);
        //IList<T> BuscarPorNome<T>(string nome);
        IList<T> BuscarPorDescricao<T>(string nome);
        string ExcluirElencoMedicamento(int co_elenco);
    }
}
