using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc
{
    public interface IFaixaEtaria: IServiceFacade
    {
        T buscaPorIdade<T>(int idade);
    }
}
