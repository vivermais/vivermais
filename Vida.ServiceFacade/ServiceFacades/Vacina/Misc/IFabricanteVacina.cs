﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IFabricanteVacina: IServiceFacade
    {
        IList<T> BuscaPorNome<T>(string nome);
        IList<T> ListarFabricantesAtivos<T>();
        IList<T> ListarFabricantesPorVacina<T>(int co_vacina);

        bool ValidarCadastro<T>(int co_fabricante, string cnpj);
    }
}
