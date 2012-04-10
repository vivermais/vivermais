using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IFeriado : IServiceFacade
    {
        bool VerificaData(DateTime data);
    }
}
