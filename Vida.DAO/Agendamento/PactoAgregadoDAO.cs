using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vida.Model;
using NHibernate;
using NHibernate.Criterion;
using Vida.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using Vida.ServiceFacade.ServiceFacades.Agendamento;


namespace Vida.DAO.Agendamento
{
    public class PactoAgregadoDAO : VidaServiceFacadeDAO, IPactoAgregado
    {
        #region IProcedimentoAgregado Members

        public PactoAgregadoDAO()
        {
            
        }

        /// <summary>
        /// Retorna uma Lista de ProcedimentosAgregados que Possuem o Agregado Informado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id_agregado"></param>
        /// <returns></returns>
        IList<T> IPactoAgregado.BuscaPorAgregado<T>(string id_agregado)
        {
            string hql = String.Empty;
            hql += "from Vida.Model.PactoAgregado pa";
            hql += " where pa.Agregado.Codigo="+ id_agregado;
            return Session.CreateQuery(hql).List<T>();
        }

        T IPactoAgregado.BuscarPorPactoAgregado<T>(string id_agregado, string id_pacto)
        {
            string hql = String.Empty;
            hql += "from Vida.Model.PactoAgregado pa";
            hql += " where pa.Agregado.Codigo=" + id_agregado;
            hql += " and pa.Pacto.Codigo=" + id_pacto;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        #endregion

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

        public void Deletar(object obj)
        {
            throw new NotImplementedException();
        }

        public IList<T> ListarTodos<T>()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
