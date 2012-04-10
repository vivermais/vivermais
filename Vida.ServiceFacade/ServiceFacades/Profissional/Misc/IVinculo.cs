using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc
{
    public interface IVinculo : IServiceFacade
    {
        IList<T> BuscarPorProfissional<T>(string co_profissional);
        IList<T> BuscarPorOcupacao<T>(string co_ocupacao);
        IList<T> BuscarPorCNESCBO<T>(string cnes, string co_cbo); //Revisada
        IList<T> BuscarCBOCnes<T>(string cnes);
        IList<T> BuscarVinculoPorCNES<T>(string cnes);
        IList<T> BuscarVinculoProfissionalPorCnes<T>(string cnes, string registroconselho, string categoria);
        object BuscarProfissionalPorNumeroConselho(string categoria, string numeroconselho, string nome);
        IList<T> BuscarPorVinculoProfissional<T>(string co_unidade, string co_profissional, string co_cbo); //Revisada
        IList<T> BuscarProfissionalPorCPF<T>(string CPF);
        IList<T> ListaVinculoProfissionaisPorUnidadeCBO<T>(string id_unidade, string cbo);
        T BuscarVinculoPorChavePrimaria<T>(string id_estabelecimento, string id_profissional, string id_CBO, string indicacaoVinculo, string tipoSusNaoSus);
        IList<T> BuscarCbosDaUnidade<T>(string cnes);
        void AtualizaVinculo<T>(T vinculo);
        IList<T> BuscarPorCNESCBO<T>(string cnes, string co_cbo, char status);
        IList<T> ListarProfissionaisPorUnidade<T>(string cnes);
        //IList<T> BuscarVinculoPorCNES<T>(string cnes);
    }
}
