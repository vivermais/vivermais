using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc
{
    public interface IRegistro : IServiceFacade
    {
        T BuscarPorCodigo<T>(int co_registro);
        IList<T> BuscarPorProcedimento<T>(string co_procedimento);
        IList<T> BuscarPorProcedimento<T>(string co_procedimento, int tipo);
        bool ProcedimentoExigeCid(string co_procedimento);
        //IList<T> BuscarPorGrupo<T>(string codigo);
        //IList<string> ListarGrupos();
    }
}
