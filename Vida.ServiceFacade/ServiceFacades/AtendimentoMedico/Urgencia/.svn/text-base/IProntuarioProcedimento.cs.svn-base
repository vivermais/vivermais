using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IProntuarioProcedimento : IServiceFacade
    {
        IList<T> BuscarPorProntuarioMedico<T>(int co_prontuariomedico);
        IList<T> BuscarPorProntuario<T>(int co_prontuario);
        T BuscarPorProntuarioMedicoEProcedimento<T>(int co_prontuariomedico, string co_procedimento);
    }
}
