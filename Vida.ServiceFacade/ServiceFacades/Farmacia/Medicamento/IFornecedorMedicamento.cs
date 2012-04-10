using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento
{
    public interface IFornecedorMedicamento: IServiceFacade
    {
        bool ValidaCadastroFornecedor<T>(string cnpj, int co_fornecedor);
    }
}
