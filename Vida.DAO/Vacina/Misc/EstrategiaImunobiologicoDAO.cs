using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;

namespace ViverMais.DAO.Vacina.Misc
{
    public class EstrategiaImunobiologicoDAO : VacinaServiceFacadeDAO, IEstrategiaImunobiologico
    {
        #region IEstrategiaImunobiologico Members

        IList<T> IEstrategiaImunobiologico.BuscarPorVacina<T>(int co_vacina)
        {
            string hql = string.Empty;
            hql = @"SELECT DISTINCT e FROM ViverMais.Model.Estrategia AS e, ViverMais.Model.Vacina AS v WHERE v IN ELEMENTS(e.Vacinas)
                    AND v.Codigo = " + co_vacina + " ORDER BY e.Descricao";

            return Session.CreateQuery(hql).List<T>();
        }

        #endregion
    }
}
