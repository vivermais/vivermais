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
    public class TipoProcedimentoDAO : AgendamentoServiceFacadeDAO, ITipoProcedimento
    {
        #region ITipoProcedimento Members

        public TipoProcedimentoDAO()
        {

        }

        T ITipoProcedimento.BuscaTipoProcedimento<T>(string id_procedimento)
        {
            string hql = "FROM ViverMais.Model.TipoProcedimento tipo ";
            hql += " WHERE tipo.Procedimento ='" + id_procedimento + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> ITipoProcedimento.BuscaProcedimentosPorPreparo<T>(int id_preparo)
        {
            string hql = String.Empty;
            hql += "select tipo from ViverMais.Model.TipoProcedimento tipo, ViverMais.Model.Preparo preparo";
            hql += " where preparo.Codigo=" + id_preparo;
            hql += " and preparo in elements(tipo.Preparos)";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ITipoProcedimento.ListarProcedimentosPorTipo<T>(string tipo)
        {
            string hql = "from ViverMais.Model.TipoProcedimento tp";
            hql += " where tp.Tipo= '" + tipo + "'";
            return Session.CreateQuery(hql).List<T>();
        }


        void ITipoProcedimento.RemoverTipoProcedimentoPorProcedimento(string id_procedimento)
        {
            string hql = "DELETE from AGD_PROCEDIMENTO_PREPARO";
            hql += " where CO_PROCEDIMENTO ='" + id_procedimento + "'";
            int rows = Session.CreateSQLQuery(hql).ExecuteUpdate();
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
