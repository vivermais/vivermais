﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IPactoAgregado : IServiceFacade
    {
        
        IList<T> BuscaPorAgregado<T>(string id_agregado);
        T BuscarPorPactoAgregado<T>(string id_agregado, string id_pacto);
        //IList<T> ListaProcedimentos
        //T BuscaPorVinculacao<T>(string id_vinculacao, string id_TipoVinculo);
        //IList<T> BuscaPreparoProcedimento<T>(string id_procedimento);
        //T BuscarVinculoPorChavePrimaria<T>(string id_estabelecimento, string id_profissional, string id_CBO, string indicacaoVinculo, string tipoSusNaoSus);
    }
}