using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Laboratorio
{
    public interface IPacienteLaboratorio : ILaboratorioServiceFacade
    {
        T BuscaPorRG<T>(string rg);
        T BuscaPorCartaoSus<T>(string numeroCartao);
    }
}
