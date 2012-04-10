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
    public class FormaDAO: ViverMaisServiceFacadeDAO, IForma
    {
        #region IForm
            IList<T> IForma.BuscarPorGrupoSubGrupo<T>(string co_grupo, string co_subgrupo) 
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.FormaOrganizacaoProcedimento AS fop";

                if (!string.IsNullOrEmpty(co_grupo))
                    hql += " WHERE fop.GrupoProcedimento.Codigo = '" + co_grupo + "'";

                if (!string.IsNullOrEmpty(co_subgrupo) && !string.IsNullOrEmpty(co_grupo))
                    hql += " AND fop.SubGrupoProcedimento.Codigo = '" + co_subgrupo + "'";
                else
                    if (!string.IsNullOrEmpty(co_subgrupo))
                        hql += " WHERE fop.SubGrupoProcedimento.Codigo = '" + co_subgrupo + "'";

                return Session.CreateQuery(hql).List<T>();
            }
        #endregion
        public FormaDAO() 
        {
        }
    }
}
