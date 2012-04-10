﻿using System;
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
    public class PactoAgregadoProcedCboDAO : ViverMaisServiceFacadeDAO, IPactoAgregadoProcedCbo
    {
        #region IProcedimentoAgregado Members

        public PactoAgregadoProcedCboDAO()
        {
            
        }

        /// <summary>
        /// Retorna uma Lista de ProcedimentosAgregados que Possuem o Agregado Informado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id_agregado"></param>
        /// <returns></returns>
        IList<T> IPactoAgregadoProcedCbo.BuscaPorAgregado<T>(string id_agregado)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.PactoAgregadoProcedCBO pa";
            hql += " where pa.Agregado.Codigo="+ id_agregado;
            return Session.CreateQuery(hql).List<T>();
        }

        T IPactoAgregadoProcedCbo.BuscarPorPactoAgregado<T>(string id_agregado, string id_pacto)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.PactoAgregadoProcedCBO pa";
            hql += " where pa.Agregado.Codigo=" + id_agregado;
            hql += " and pa.Pacto.Codigo=" + id_pacto;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IPactoAgregadoProcedCbo.BuscaPorPacto<T>(int id_pacto)
        {
            string hql = string.Empty;
            hql += "from ViverMais.Model.PactoAgregadoProcedCBO pactoProcedCBO";
            hql += " where pactoProcedCBO.Pacto.Codigo = "+ id_pacto;
            //hql += "select DISTINCT pactoProcedCBO from ViverMais.Model.PactoAgregadoProcedCBO pactoProcedCBO, ViverMais.Model.Pacto pacto";
            //hql += " where pactoProcedCBO IN ELEMENTS (pacto.PactosAgregados)";
            //hql += " and pacto in (from ViverMais.Model.Pacto as p2 where p2.Codigo =" + id_pacto + ")";
            //hql += " and pacto.Codigo=" + id_pacto;
            return Session.CreateQuery(hql).List<T>();
        }

        //void IPactoAgregadoProcedCbo.InativarTodosOsPactos(int co_usuario_logado)
        //{
        //    string sqlQuery = "UPDATE rl_pms_pacto_agregado SET ATIVO = '0'";
        //    sqlQuery += ", DATAOPERACAO = TO_DATE('"+DateTime.Now.ToString("dd/MM/yyyy")+"','DD/MM/YYYY')";
        //    sqlQuery += ", CO_USUARIO="+co_usuario_logado;
        //    Session.CreateSQLQuery(sqlQuery).ExecuteUpdate();
        //}
        void IPactoAgregadoProcedCbo.InativarTodosOsPactos(int co_usuario_logado)
        {
            string sqlQuery = "UPDATE rl_pms_pacto_agregado SET ATIVO = 0";
            sqlQuery += ", DATAOPERACAO = TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')";
            sqlQuery += ", CO_USUARIO=" + co_usuario_logado;
            sqlQuery += " WHERE TIPOPACTO = 'C' and ativo= 1";
            Session.CreateSQLQuery(sqlQuery).ExecuteUpdate();
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
