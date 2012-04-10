using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IProcedimentoAgregado : IServiceFacade
    {
        IList<T> BuscaPorAgregado<T>(string id_agregado);
        IList<T> ListarProcedimentosPorAgregado<T>(string id_agregado);
        T BuscaAgregadoPorProcedimento<T>(string id_procedimento);
        //T BuscaPorVinculacao<T>(string id_vinculacao, string id_TipoVinculo);
        //IList<T> BuscaPreparoProcedimento<T>(string id_procedimento);
        //T BuscarVinculoPorChavePrimaria<T>(string id_estabelecimento, string id_profissional, string id_CBO, string indicacaoVinculo, string tipoSusNaoSus);
    }
}
