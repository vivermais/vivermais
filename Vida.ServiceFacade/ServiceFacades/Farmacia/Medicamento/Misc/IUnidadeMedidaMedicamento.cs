using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc
{
    public interface IUnidadeMedidaMedicamento : IFarmaciaServiceFacade
    {
        T VerificaDuplicidadeUnidadePorSigla<T>(string sigla, int co_unidade);
    }
}
