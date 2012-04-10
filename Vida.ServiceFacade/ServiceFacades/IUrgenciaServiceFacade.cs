using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades
{
    public interface IUrgenciaServiceFacade : IServiceFacade
    {
        void Insert(object obj);
    }
}
