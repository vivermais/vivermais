using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Inventario
{
    public interface IInventario : IServiceFacade
    {
        IList<T> BuscarPorSituacao<T>(char situacao, int id_farmacia);
        IList<T> BuscarPorFarmacia<T>(int id_farmacia);
        void AbrirInventario<T>(int co_farmacia, T inventario);
        void EncerrarInventario<T>(T inventario);
        IList<T> ListarItensInventario<T>(int co_inventario);
        T BuscarItemInventario<T>(int co_inventario, int co_lotemedicamento);
        Hashtable RelatorioInventario(int co_inventario, int tipo);
    }
}
