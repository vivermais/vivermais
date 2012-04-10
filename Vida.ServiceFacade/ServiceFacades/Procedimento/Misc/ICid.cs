using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc
{
    public interface ICid: IServiceFacade
    {
        T BuscarPorCodigo<T>(string co_cid);
        IList<T> BuscarPorProcedimento<T>(string co_procedimento);
        IList<T> BuscarPorProcedimento<T>(string co_procedimento, string tp_sexo);
        IList<T> BuscarPorGrupo<T>(string codigo);
        IList<string> ListarGrupos();
        IList<T> BuscarPorGrupo<T>(string codigo, string co_procedimento);
        IList<T> BuscarPorCodigo<T>(string codigo, string co_procedimento);
        IList<T> BuscarPorInicioNome<T>(string nome);
        IList<T> BuscarPorInicioNome<T>(string co_procedimento, string nome);
        bool CidPermitidoParaSexo(string co_cid, string sexo);
        bool ExisteVinculoProcedimentoCid(string co_procedimento, string co_cid);
    }
}
