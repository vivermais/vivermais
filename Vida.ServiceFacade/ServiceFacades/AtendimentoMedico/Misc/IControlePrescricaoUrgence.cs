using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc
{
    public interface IControlePrescricaoUrgence
    {
        T BuscarControlePorPrescricao<T>(long co_prescricao);
        T BuscarPorPrimeiraConsulta<T>(long co_prontuario);
        T BuscarPorEvolucaoMedica<T>(long co_evolucaomedica);
    }
}
