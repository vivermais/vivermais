using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using System.Collections;
using NHibernate.Criterion;
using System.Data;

namespace ViverMais.DAO
{
    public class NHibernateDAO
    {
        private ISession session;
        private DetachedCriteria detached;

        public ISession Session
        {
            get { return session; }
            set { session = value; }
        }

        public NHibernateDAO(string alias)
        {
            Session = NHibernateHttpHelper.GetCurrentSession(alias);
            //if (!this.Session.IsConnected)
            //    Session.Reconnect();
        }

        ~NHibernateDAO()
        {
            //if(session.IsConnected)
            //    session.Disconnect();
        }

        public virtual Object Insert(Object obj)
        {
            using (session.BeginTransaction())
            {
                try
                {
                    Object o = session.Save(obj);
                    session.Transaction.Commit();
                    return o;
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                    throw e;
                }
            }
        }

        public virtual void Update(Object obj)
        {
            using (session.BeginTransaction())
            {
                try
                {
                    if (!session.Contains(obj))
                    {
                        session.Update(Session.Merge(obj));
                    }
                    session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                    throw e;
                }
            }
        }

        public virtual void Save(Object obj)
        {
            //session.Clear();

            using (session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(obj);
                    //session.Flush();
                    session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                    throw e;
                }
            }
        }

        public virtual void Save<T>(ref T obj)
        {
            using (session.BeginTransaction())
            {
                try
                {
                    obj = (T)session.SaveOrUpdateCopy(obj);
                    session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                    throw e;
                }
            }
        }

        public virtual void Delete(Object obj)
        {
            using (session.BeginTransaction())
            {
                try
                {
                    session.Delete(session.Merge(obj));
                    session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                    throw e;
                }
            }
        }

        public IList<T> FindAll<T>()
        {
            IList<T> entities;
            entities = session.CreateCriteria(typeof(T)).List<T>();
            return entities;
        }

        public IList<T> FindAll<T>(string orderField, bool asc)
        {
            IList<T> entities;
            ICriteria criteria = session.CreateCriteria(typeof(T));
            if (asc)
                criteria.AddOrder(Order.Asc(orderField));
            else
                criteria.AddOrder(Order.Desc(orderField));
            entities = criteria.List<T>();
            return entities;
        }

        /// <summary>
        /// Retorna o registro da entidade mais recente
        /// </summary>
        /// <returns></returns>
        public T ReturnLastElementIncluded<T>(string primaryKey)
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            ICriteria criteria2 = session.CreateCriteria(typeof(T));
            object codigo = criteria.SetProjection(Projections.Max(primaryKey)).UniqueResult();


            return criteria2.Add(Expression.Eq(primaryKey, codigo)).UniqueResult<T>();
        }

        public int BuscarProximoRegistro<T>(string primaryKey)
        {
            string hql = "SELECT MAX (item." + primaryKey + ") From " + typeof(T) + " item ";

            return Convert.ToInt32(Session.CreateQuery(hql).UniqueResult()) + 1;
        }

        public T FindByPrimaryKey<T>(object pk) where T : new()
        {
            //return Session.CreateCriteria(typeof(T)).Add(Expression.Eq("Codigo", pk)).UniqueResult<T>();
            T retorno = default(T);
            try
            {
                retorno = session.Get<T>(pk);
            }
            catch (Exception e)
            {
                retorno = default(T);
                throw e;
            }
            return retorno;
        }

    }
}
