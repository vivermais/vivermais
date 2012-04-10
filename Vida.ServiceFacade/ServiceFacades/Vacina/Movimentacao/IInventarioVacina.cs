using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao
{
    public interface IInventarioVacina: IServiceFacade
    {
        IList<T> BuscarPorSalaVacina<T>(int co_salavacina);
        IList<T> BuscarPorSituacao<T>(char situacao, int co_salavacina);
        IList<T> ListarItensInventario<T>(int co_inventario);

        void AbrirInventario<T>(T inventario);
        void EncerrarInventario<T>(T inventario);

        int AbrirInventario(DateTime dataabertura, int co_sala);
        
        T BuscarItemInventario<T>(int co_inventario, long co_lotevacina);
    }
}
