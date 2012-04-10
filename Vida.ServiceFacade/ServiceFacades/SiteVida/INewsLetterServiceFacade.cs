using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.SiteViverMais
{
    public interface INewsLetterServiceFacade : IServiceFacade
    {
       T BuscarEmailUsuario<T>(string Email);
       
    }
}
