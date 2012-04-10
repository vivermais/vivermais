using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IEvolucaoMedica : IServiceFacade
    {
        IList<T> BuscarPorProntuario<T>(long co_prontuario);
        T BuscarConsultaMedica<T>(long co_prontuario);
    }
}
