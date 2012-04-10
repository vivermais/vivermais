﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude
{
    public interface IEstabelecimentoSaude : IServiceFacade
    {
        T BuscarEstabelecimentoPorCNES<T>(string cnes);
        IList<T> BuscarEstabelecimentoPorNomeFantasia<T>(string nome);
        IList<T> BuscarUnidadeDistrito<T>(int distrito);
        IList<T> BuscarEstabelecimentoPorNaturezaOrganizacao<T>(string co_natureza);
        IList<T> ListarEstabelecimentosForaRedeMunicipal<T>();
        void ImportarEstabelecimento<T,A>(T xml, A importacao);
        IList<T> BuscarPorBairro<T>(int co_bairro);
    }
}