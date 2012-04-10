using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Misc;
using NHibernate;
using NHibernate.Criterion;

namespace ViverMais.DAO.Misc
{
    public class LogEventosDAO : ViverMaisServiceFacadeDAO, ILogEventos
    {
        #region ILogEventos Members

        void ILogEventos.Salvar(object log)
        {
            using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    session.Save(log);
                    session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    session.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        IList<T> ILogEventos.BuscarLog<T>(DateTime dataInicial, DateTime dataFinal, int evento) 
        {
            ICriteria criteria = Session.CreateCriteria(typeof(ViverMais.Model.LogViverMais));
            criteria.Add(Expression.Between("Data", dataInicial, dataFinal))
                .AddOrder(Order.Desc("Data"));
            if (evento != 0)
                criteria.Add(Expression.Eq("Evento", evento));
            return criteria.List<T>();
        }

        #endregion
    }
}
