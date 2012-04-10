using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Relatorio;
using NHibernate;
using ViverMais.Model;
using NHibernate.Criterion;
using System.Collections;

namespace ViverMais.DAO.Relatorio
{
    public class RelatorioCNSDAO : ViverMaisServiceFacadeDAO, IRelatorioCNS
    {
        #region IRelatorioCNS Members

        IList<T> IRelatorioCNS.PesquisarProducaoPorDistrito<T>(int id_distrito, DateTime data_inicial, DateTime data_final)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            criteria.Add(Expression.Ge("DataEmissao", data_inicial));
            criteria.Add(Expression.Le("DataEmissao", data_final));
            if (id_distrito != 0) 
            {

            }
            return criteria.List<T>();
        }

        #endregion
    }
}
