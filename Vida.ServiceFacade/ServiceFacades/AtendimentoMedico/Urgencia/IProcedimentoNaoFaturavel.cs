﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IProcedimentoNaoFaturavel : IServiceFacade
    {
        bool ValidarCadastroProcedimento(long codigo, int id);

        IList<T> BuscarPorNome<T>(string nome);
    }
}