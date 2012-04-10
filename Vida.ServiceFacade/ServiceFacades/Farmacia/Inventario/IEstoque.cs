using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.ServiceFacade.ServiceFacades.Farmacia.Inventario
{
    public interface IEstoque : IServiceFacade
    {
        IList<T> BuscarPorFarmacia<T>(int id_farmacia);
    }
}
