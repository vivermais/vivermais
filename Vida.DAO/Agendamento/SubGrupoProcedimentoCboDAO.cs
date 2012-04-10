using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;

namespace ViverMais.DAO.Agendamento
{
    public class SubGrupoProcedimentoCboDAO : AgendamentoServiceFacadeDAO, ISubGrupoProcedimentoCbo
    {
        IList<T> ISubGrupoProcedimentoCbo.ListarSubGrupoProcedimentoCbo<T>(bool ativo)
        {
            string hql = " from ViverMais.Model.SubGrupoProcedimentoCBO subGrupo";
            if (ativo == true)
                hql += " where subGrupo.Ativo = 1";
            else
                hql += " where subGrupo.Ativo = 0";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISubGrupoProcedimentoCbo.ListarSubGrupoPorProcedimentoECbo<T>(string co_procedimento, string co_ocupacao, bool ativo)
        {
            string hql = " Select subGrupoProcedimentoCBO.SubGrupo from ViverMais.Model.SubGrupoProcedimentoCBO subGrupoProcedimentoCBO";
            hql += " where subGrupoProcedimentoCBO.Procedimento.Codigo = '" + co_procedimento + "'";
            //hql += " and subGrupoProcedimentoCBO.SubGrupo.Codigo = " + co_subGrupo;
            hql += " and subGrupoProcedimentoCBO.Cbo.Codigo = '" + co_ocupacao + "'";
            if (ativo == true)
                hql += " and subGrupoProcedimentoCBO.Ativo = 1";
            else
                hql += " and subGrupoProcedimentoCBO.Ativo = 0";
            hql += " order by subGrupoProcedimentoCBO.SubGrupo.NomeSubGrupo";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISubGrupoProcedimentoCbo.BuscarSubGrupoProcedimentoCbo<T>(int co_subGrupo, string co_procedimento, string co_ocupacao)
        {
            string hql = " from ViverMais.Model.SubGrupoProcedimentoCBO subGrupo";
            hql += " where subGrupo.Procedimento.Codigo = '" + co_procedimento + "'";
            hql += " and subGrupo.SubGrupo.Codigo = "+ co_subGrupo;
            hql += " and subGrupo.Cbo.Codigo = '" + co_ocupacao + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        T ISubGrupoProcedimentoCbo.VerificaSeExisteAtivo<T>(int co_subGrupo, string co_procedimento, string co_ocupacao)
        {
            string hql = " from ViverMais.Model.SubGrupoProcedimentoCBO subGrupo";
            hql += " where subGrupo.Ativo = 1";
            hql += " and subGrupo.SubGrupo.Codigo = " + co_subGrupo;
            hql += " and subGrupo.Cbo.Codigo = '" + co_ocupacao + "'";
            hql += " and subGrupo.Procedimento.Codigo = '" + co_procedimento + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}
