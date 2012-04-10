using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.DAO.Procedimento.Misc
{
    public class SubGrupoDAO: ViverMaisServiceFacadeDAO, ISubGrupo
    {
        #region ISubGrupo
            IList<T> ISubGrupo.BuscarPorGrupo<T>(string co_grupo) 
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.SubGrupoProcedimento AS sg WHERE sg.GrupoProcedimento.Codigo = '" + co_grupo + "'";
                return Session.CreateQuery(hql).List<T>();
            }
        #endregion

        public SubGrupoDAO() 
        {
        }
    }
}
