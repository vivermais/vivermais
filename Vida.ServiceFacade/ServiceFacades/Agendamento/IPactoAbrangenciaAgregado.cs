using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IPactoAbrangenciaAgregado : IServiceFacade
    {
        IList<T> BuscaPorAgregado<T>(string id_agregado);
        T BuscarPorPactoAbrangenciaAgregado<T>(string id_agregado, string id_pactoabrangencia);
        IList<T> BuscaPorPactoAbrangencia<T>(int id_pactoabrangencia);
        IList<T> ListarPactoAbrangenciaGrupoMunicipioPorPactoAbrangenciaAgregado<T>(int id_pactoabrangencia);
        bool RestricaoPactoAbrangencia<M,P>(M municipio, P procedimentoSelecionado);
        IList<object> ListarTodosPactoAbrangenciaGrupoMunicipio();
    }
}
