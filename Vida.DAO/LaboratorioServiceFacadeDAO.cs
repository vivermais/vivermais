using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.DAO
{
    public class LaboratorioServiceFacadeDAO : NHibernateDAO, ILaboratorioServiceFacade
    {
        public LaboratorioServiceFacadeDAO()
            : base("ViverMais")
        {
        }

        #region IServiceFacade Members

        public T BuscarPorCodigo<T>(object codigo) where T : new()
        {
            throw new NotImplementedException();
        }

        public void Atualizar(object obj)
        {
            throw new NotImplementedException();
        }

        public void Inserir(object obj)
        {
            throw new NotImplementedException();
        }

        public void Salvar(object obj)
        {
            throw new NotImplementedException();
        }

        void IServiceFacade.Salvar<T>(ref T obj)
        {
            this.Save<T>(ref obj);
        }

        public void Deletar(object obj)
        {
            throw new NotImplementedException();
        }

        public IList<T> ListarTodos<T>()
        {
            return this.Session.CreateCriteria(typeof(T)).List<T>();
        }

        public IList<T> ListarTodos<T>(string orderField, bool asc)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
