using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IPacto : IServiceFacade
    {
        T BuscarPactoPorMunicipio<T>(string id_municipio);
        //void SalvarPacto<T,U,V>(T pacto, U pactoAgregadoProcedCBO, IList<V> pactoReferenciaSaldo);
        //T BuscaAgregadoPorPacto<T>(string id_agregado,string id_pacto);
        //T BuscaAgregadoPorPacto<T>(string id_pacto);
        //T VerificaExistenciaAgregado<T>(string id_agregado, string id_pacto);
        //T VerificaProcedimentoNoPacto<T>(string id_procedimento, string id_municipio);
    }
}
