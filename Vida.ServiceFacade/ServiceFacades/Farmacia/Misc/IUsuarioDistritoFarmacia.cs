using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc
{
    public interface IUsuarioDistritoFarmacia : IServiceFacade
    {
        T BuscarDistritoPorUsuario<T>(int codigoUsuario);
        T BuscarPorUsuario<T>(int codigoUsuario);
        IList<T> BuscarUsuariosPorDistrito<T>(int codigoDistrito);
    }
}
