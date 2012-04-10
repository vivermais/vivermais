using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc
{
    public interface ICBO : IViverMaisServiceFacade
    {
        IList<T> ListarCBOsPorProcedimento<T>(string id_procedimento);
        
        /// <summary>
        /// Lista os CBOs que fazem parte do Grupo. Ex: 2231, possui os CBOS 223115 - Clínico, 223110
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grupoCBO"></param>
        /// <returns></returns>
        IList<T> ListarPorGrupo<T>(string grupoCBO);
        IList<T> ListarCBOsSaude<T>();
        bool CBOPertenceMedico<T>(T cbo);
        bool CBOPertenceEnfermeiro<T>(T cbo);
        bool CBOPertenceAuxiliarTecnicoEnfermagem<T>(T cbo);
        IList<T> ListarCBOsTecnicosAuxiliarEnfermagem<T>();
    }
}
