using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IAfastamentoEAS : IAgendamentoServiceFacade
    {
        //IList<T> ListaAfastamentoProfissional<T>(string id_unidade, string id_profissional);
        IList<T> BuscarAfastamentosPorUnidade<T>(string id_unidade);
        T VerificaAfastamentosNaData<T>(string cnes, DateTime data);
        T VerificaAfastamentosNaData<T>(string cnes, DateTime data_inicial, string data_final);
    }
}
