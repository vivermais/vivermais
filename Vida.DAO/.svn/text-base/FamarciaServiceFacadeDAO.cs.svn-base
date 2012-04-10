using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vida.ServiceFacade.ServiceFacades;
using NHibernate.Criterion;

namespace Vida.DAO
{
    public class FamarciaServiceFacadeDAO: NHibernateDAO, IFarmaciaServiceFacade
    {
        public FamarciaServiceFacadeDAO() : base("farmacia")
        {
        }

        #region IFarmaciaServiceFacade

        #endregion

        #region IServiceFacade Members

        T IServiceFacade.BuscarPorCodigo<T>(object codigo)
        {
            return this.FindByPrimaryKey<T>(codigo);
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

        IList<T> IServiceFacade.ListarTodos<T>()
        {
            return this.Session.CreateCriteria(typeof(T)).List<T>();
        }

        IList<T> IServiceFacade.ListarTodos<T>(string orderField, bool asc)
        {
            return this.Session.CreateCriteria(typeof(T)).AddOrder(asc ? Order.Asc(orderField) : Order.Desc(orderField)).List<T>();
        }

        #endregion
    }
}
