using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento
{
    public interface ILoteMedicamento : IFarmaciaServiceFacade
    {
        bool ValidaCadastroLote<T>(string lote, DateTime data_validade, int co_medicamento, int co_fabricante, int co_lote);
        IList<T> BuscarPorDescricao<T>(string lote, DateTime data_validade, int co_medicamento, int co_fabricante);
        IList<T> BuscarPorMedicamento<T>(int co_medicamento);
        IList<T> BuscarPorEstoqueAlmoxarifado<T>(int co_medicamento, int co_farmacia);
    }
}
