using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;

namespace ViverMais.DAO.Profissional.Misc
{
    public class AreaDAO: ViverMaisServiceFacadeDAO, IArea
    {
        #region IArea
        A IArea.BuscarPorCodigo<M, C, A>(M municipio_area, C codigo_area) 
        {
            Municipio municipio = (Municipio)((object)municipio_area);
            string codigo       = (string)((object)codigo_area);

            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Area AS a WHERE a.Municipio.Codigo = '" + municipio.Codigo + "' AND a.Codigo = '" + codigo_area + "'";
            return Session.CreateQuery(hql).UniqueResult<A>();
        }
        #endregion
    }
}
