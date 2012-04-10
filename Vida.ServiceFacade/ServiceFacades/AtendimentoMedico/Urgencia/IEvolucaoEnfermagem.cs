using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IEvolucaoEnfermagem : IServiceFacade
    {
        IList<T> BuscarPorProntuario<T>(long co_prontuario);
    }
}
