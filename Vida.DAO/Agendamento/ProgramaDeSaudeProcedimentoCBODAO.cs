using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;

namespace ViverMais.DAO.Agendamento
{
    public class ProgramaDeSaudeProcedimentoCBODAO : AgendamentoServiceFacadeDAO,IProgramaDeSaudeProcedimentoCBO
    {
        /// <summary>
        /// Lista os Procedimentos Por Programa de Saude
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id_programa"></param>
        /// <returns></returns>
        IList<T> IProgramaDeSaudeProcedimentoCBO.ListarPorPrograma<T>(int id_programa, bool ativo)
        {
            string hql = "FROM ViverMais.Model.ProgramaDeSaudeProcedimentoCBO progSaudeProcedimento";
            hql += " WHERE progSaudeProcedimento.ProgramaDeSaude.Codigo = " + id_programa;
            hql += " and progSaudeProcedimento.Ativo = " + (ativo ? 1 : 0).ToString();
            //hql += " group by progSaudeProcedimento.CodigoProcedimento";
            return this.Session.CreateQuery(hql).List<T>();
        }

        T IProgramaDeSaudeProcedimentoCBO.BuscaVinculo<T>(int id_programa, string id_procedimento)
        {
            string hql = "FROM ViverMais.Model.ProgramaDeSaudeProcedimentoCBO progSaudeProcedimento";
            hql += " WHERE progSaudeProcedimento.ProgramaDeSaude.Codigo = " + id_programa;
            hql += " AND progSaudeProcedimento.Procedimento.Codigo = '" + id_procedimento + "'";
            //hql += " AND progSaudeProcedimento.CodigoCBO = '" + id_cbo + "'";
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IProgramaDeSaudeProcedimentoCBO.RetornaCBOsDoPrograma_Procedimento<T>(int id_programa, string id_procedimento)
        {
            string hql = "Select progSaudeProcedimento.CodigoCBO from ViverMais.Model.ProgramaDeSaudeProcedimentoCBO progSaudeProcedimento";
            hql += " WHERE progSaudeProcedimento.ProgramaDeSaude.Codigo = " + id_programa;
            hql += " AND progSaudeProcedimento.CodigoProcedimento = '" + id_procedimento + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        void IProgramaDeSaudeProcedimentoCBO.DeletarProgramaDeSaudeProcedimentoCBO(int id_programa, string id_procedimento)
        {
            string hql = "DELETE from AGD_PROGRAMA_PROCEDIMENTO_CBO";
            hql += " where CO_PROGRAMA = " + id_programa;
            hql += " and CO_PROCEDIMENTO ='" + id_procedimento + "'";
            //hql += " and CO_OCUPACAO ='" + id_procedimento + "'";
            int rows = Session.CreateSQLQuery(hql).ExecuteUpdate();
        }

        void IProgramaDeSaudeProcedimentoCBO.DeletarProgramaDeSaudeProcedimentoCBOPorPrograma(int id_programa)
        {
            string hql = "DELETE from AGD_PROGRAMA_PROCEDIMENTO_CBO";
            hql += " where CO_PROGRAMA = " + id_programa;
            //hql += " and CO_PROCEDIMENTO ='" + id_procedimento + "'";
            //hql += " and CO_OCUPACAO ='" + id_procedimento + "'";
            Session.CreateSQLQuery(hql).ExecuteUpdate();
        }

        void IProgramaDeSaudeProcedimentoCBO.DeletarCbo(int id_programa, string id_procedimento, string id_cbo)
        {
            string hql = "DELETE from AGD_PROGRAMA_PROCEDIMENTO_CBO";
            hql += " where CO_PROGRAMA = " + id_programa;
            hql += " and CO_PROCEDIMENTO ='" + id_procedimento + "'";
            hql += " and CO_OCUPACAO ='" + id_cbo + "'";
            int rows = Session.CreateSQLQuery(hql).ExecuteUpdate();
        }
    }
}
