using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IFaixa : IAgendamentoServiceFacade
    {
        IList<T> BuscarFaixaAPAC<T>(int ano);
        T BuscarCodigoFaixa<T>(string faixa);

    }
}
