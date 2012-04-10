using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IProntuarioMedico : IServiceFacade
    {
        IList<T> buscarPorProntuario<T>(int co_prontuario);
        void SalvarProntuarioMedico<T, V>(T ProntuarioMedico, bool OcupaVaga, bool DesocupaVaga, V VagaUrgencia);
    }
}
