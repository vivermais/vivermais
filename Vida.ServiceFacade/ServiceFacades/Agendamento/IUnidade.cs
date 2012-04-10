using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.Agendamento
{
    public interface IUnidade : IAgendamentoServiceFacade
    {
        bool VerificaEstabelecimentoToleranteFeriado(string id_unidade);
        //IList VerificaEstabelecimentoToleranteFeriado(string id_unidade);
    }
}
