using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento
{
    public interface IMedicamento : IFarmaciaServiceFacade
    {
        IList<T> BuscarMedicamentosPorUnidadeMedida<T>(int co_unidademedida);
        IList<T> BuscarElencos<T>(int co_medicamento);
        IList<T> BuscarSubElencos<T>(int co_medicamento);
        IList<T> BuscarPorDescricao<T>(int co_unidademedida, string nome, bool pertencearede);
        IList<T> BuscarPorDescricao<T>(string co_medicamento, string nome);
        T VerificaDuplicidadePorCodigo<T>(string codigosigm, int co_medicamento);
        T BuscarPorCodigoSIGM<T>(string codigosigm);
        string GerarCodigoMedicamentoUrgencia();
        IList<T> ListarTodosMedicamentos<T>();
        //string ExcluirElencoMedicamento(int co_elenco);
        //string ExcluirSubElencoMedicamento(int co_subelenco);
        IList<T> BuscarPorElenco<T>(int co_elenco);
    }
}
