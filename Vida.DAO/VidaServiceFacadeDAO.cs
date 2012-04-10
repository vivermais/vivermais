﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades;
using NHibernate.Criterion;

namespace ViverMais.DAO
{
    public class ViverMaisServiceFacadeDAO : NHibernateDAO, IViverMaisServiceFacade
    {
        public ViverMaisServiceFacadeDAO()
            : base("ViverMais")
        {

        }

        #region IServiceFacade Members

        T IServiceFacade.BuscarPorCodigo<T>(object codigo)
        {
            return this.FindByPrimaryKey<T>(codigo);
        }

        object ReturnLastElementIncluded(object obj, string primaryKey)
        {
            return this.ReturnLastElementIncluded(obj, primaryKey);
        }

        void IServiceFacade.Atualizar(object obj)
        {
            this.Update(obj);
        }

        void IServiceFacade.Inserir(object obj)
        {
            this.Insert(obj);
        }

        void IServiceFacade.Deletar(object obj)
        {
            this.Delete(obj);
        }

        void IServiceFacade.Salvar(object obj)
        {
            this.Save(obj);
        }

        void IServiceFacade.Salvar<T>(ref T obj)
        {
            this.Save<T>(ref obj);
        }

        IList<T> IServiceFacade.ListarTodos<T>()
        {
            return this.Session.CreateCriteria(typeof(T)).List<T>();
        }

        IList<T> IServiceFacade.ListarTodos<T>(string orderField, bool asc)
        {
            return this.Session.CreateCriteria(typeof(T)).AddOrder(asc ? Order.Asc(orderField) : Order.Desc(orderField)).List<T>();
        }

        #endregion



        #region IViverMaisServiceFacade Members

        void IViverMaisServiceFacade.Flush()
        {
            this.Session.Flush();
        }

        #endregion
    }
}
