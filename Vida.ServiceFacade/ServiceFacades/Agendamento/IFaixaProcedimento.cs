using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IFaixaProcedimento : IAgendamentoServiceFacade
    {
        IList<T> BuscaFaixa<T,A>(A faixas);
        T BuscarDeProcedimento<T>(int id_faixa);

    }
}
