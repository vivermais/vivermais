using System;
using NHibernate;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;

namespace Vida.DAO.AtendimentoMedico.Misc
{
    public class CidDAO: UrgenciaServiceFacadeDAO, ICid
    {
        #region ICid
        IList<string> ICid.buscaGrupos() 
        {
            string hql = string.Empty;
            hql = "select substring(c.CodCid,1,1) From Vida.Model.Cid c group by substring(c.CodCid,1,1) order by substring(c.CodCid,1,1) ";
            return Session.CreateQuery(hql).List<string>();
        }

        IList<T> ICid.buscaCids<T>(string codigo)
        {
            string hql = "From Vida.Model.Cid c Where c.Codigo > 0";
            codigo = codigo + "%";
            hql += " and c.CodCid like '" + codigo + "' ";
            hql += " Order by c.Nome ";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ICid.BuscarPorCodigoCID<T>(string codigo)
        {
            string hql = "From Vida.Model.Cid c Where c.Codigo > 0";
            hql += " and c.CodCid = '" + codigo + "' ";
            hql += " Order by c.Nome ";
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion
    }
}
