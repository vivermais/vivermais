using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections;
using NHibernate.Criterion;

namespace ViverMais.DAO.Atendimento
{
    public class AtendimentoDAO : NHibernateDAO, IAtendimentoServiceFacade
    {
        public AtendimentoDAO() : base("ViverMais")
        {
        }

        private string teste()
        {
            return "";
        }

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

        T IServiceFacade.ReturnLastElementIncluded<T>(string primaryKey)
        {
            throw new NotImplementedException();
        }

        int IServiceFacade.BuscarProximoRegistro<T>(string primaryKey)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
