﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc
{
    public interface IEquipe : IServiceFacade
    {
        IList<T> BuscarPorProfissional<T>(string co_profissional);
        IList<T> BuscarPorOcupacao<T>(string co_ocupacao);
        E BuscarPorCodigo<A, C, E>(A area_equipe, C codigo_equipe);
        T BuscarProfissionalPorCategoriaNumeroConselho<T>(string co_categoria, string numeroconselho);
    }
}
