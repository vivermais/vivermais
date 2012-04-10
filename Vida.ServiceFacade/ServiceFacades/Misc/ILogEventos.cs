using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Misc
{
    public interface ILogEventos
    {
        void Salvar(Object log);
        IList<T> BuscarLog<T>(DateTime dataInicial, DateTime dataFinal, int evento);
    }
}
