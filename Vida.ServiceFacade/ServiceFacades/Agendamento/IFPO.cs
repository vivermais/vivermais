using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IFPO : IServiceFacade
    {
        IList<T> BuscarFPO<T>(string id_unidade, int competencia);
        T BuscarFPO<T>(string id_unidade, int competencia, string procedimento);
        T BuscarCompetencia<T>(string unidade);
        IList<T> ListarUnidadesPorCompetencia<T>(int competencia);
        IList<T> ListarProcedimentosPorCompetenciaCNES<T>(int competencia, string cnes);
    }
}
