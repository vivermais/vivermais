using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;


namespace ViverMais.DAO.Agendamento
{
    public class ProcedimentoAgregadoDAO : ViverMaisServiceFacadeDAO, IProcedimentoAgregado
    {
        #region IProcedimentoAgregado Members

        public ProcedimentoAgregadoDAO()
        {
            
        }

        /// <summary>
        /// Retorna uma Lista de ProcedimentosAgregados que Possuem o Agregado Informado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id_agregado"></param>
        /// <returns></returns>
        IList<T> IProcedimentoAgregado.BuscaPorAgregado<T>(string id_agregado)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.ProcedimentoAgregado pa";
            hql += " where pa.Agregado.Codigo="+ id_agregado;
            return Session.CreateQuery(hql).List<T>();
        }


        IList<T> IProcedimentoAgregado.ListarProcedimentosPorAgregado<T>(string id_agregado)
        {
            string hql = String.Empty;
            hql += "select pa.Procedimento from ViverMais.Model.ProcedimentoAgregado pa";
            hql += " where pa.Agregado.Codigo=" + id_agregado;
            hql += " order by pa.Procedimento.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        /// <summary>
        /// Retorna uma Lista com todos os agregados que possuem o procedimento
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id_procedimento"></param>
        /// <returns></returns>
        T IProcedimentoAgregado.BuscaAgregadoPorProcedimento<T>(string id_procedimento)
        {
            string hql = String.Empty;
            hql += "select pa.Agregado from ViverMais.Model.ProcedimentoAgregado pa";
            hql += " where pa.Procedimento.Codigo='" + id_procedimento+"'";
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
