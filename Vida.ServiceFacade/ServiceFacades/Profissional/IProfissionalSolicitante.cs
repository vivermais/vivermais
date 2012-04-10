﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Profissional
{
    public interface IProfissionalSolicitante : IServiceFacade
    {
        //IList<E> BuscarPorEquipe<A,C,E>(A area_equipe, C codigo_equipe);
        //T BuscaProfissionalPorVinculo<T>(int categoria, int numeroconselho,string cnes);
        //IList<T> BuscaProfissionalPorVinculo<T>(int categoria, string numeroconselho, string nome);
        //T BuscaProfissionalPorVinculoCBO<T>(int numeroconselho, string cbo, string cnes);
        IList<T> BuscaProfissionalPorNumeroConselhoECategoria<T>(string categoria, string numeroconselho, string nome, int codigo);
        T BuscaUltimoRegistroIncluido<T>();
        //IList<T> BuscaVinculoPorNumeroConselhoECategoria<T>(int categoria, string numeroconselho, string nome);
    }
}
