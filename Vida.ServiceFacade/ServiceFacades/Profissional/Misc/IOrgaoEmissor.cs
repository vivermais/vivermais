﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc
{
    public interface IOrgaoEmissor : IServiceFacade
    {
        T BuscarOrgaoEmissorPorCategoria<T>(string id_categoria);
        //A BuscarPorCodigo<M, C, A>(M municipio_area, C codigo_area);
    }
}
