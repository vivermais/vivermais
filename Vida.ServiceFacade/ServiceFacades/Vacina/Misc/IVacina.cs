﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc
{
    public interface IVacina : IServiceFacade
    {
        IList<T> BuscarPorUnidadeMedida<T>(int co_unidademedida);
        IList<T> BuscarDoses<T>(int co_vacina);
        IList<T> BuscarItensDoseVacina<T>(int co_vacina);
        IList<T> BuscarVacinasDoCalendario<T>();
        
        bool ValidarCadastroVacina(string codigocmadi, int co_vacina);

        void SalvarVacina<V, D, E, IV>(V _vacina, IList<D> _dosesvacina, IList<E> _estrategiasvacina, IList<IV> _itensvacina, int co_usuario);
    }
}
