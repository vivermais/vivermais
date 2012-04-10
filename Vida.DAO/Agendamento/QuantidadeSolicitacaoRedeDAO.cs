using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.DAO.Agendamento
{
    public class QuantidadeSolicitacaoRedeDAO : AgendamentoServiceFacadeDAO, IQuantidadeSolicitacaoRede
    {
        public QuantidadeSolicitacaoRedeDAO() { }

        //object IQuantidadeSolicitacaoRede.BuscaQuantidadeSolicitacoes(string competencia, string co_procedimento, string co_cbo, string cnes)
        //{
        //    string hql = "quantidade.QtdSolicitacoes from ViverMais.Model.QuantidadeSolicitacaoRede quantidade";
        //    hql += " where quantidade.Competencia = '" + competencia + "'";
        //    hql += " and quantidade.Procedimento.Codigo = '" + co_procedimento + "'";
        //    hql += " and quantidade.CBO.Codigo = '" + co_cbo + "'";
        //    hql += " and quantidade.Estabelecimento.CNES = '" + cnes + "'";
        //    return Session.CreateQuery(hql).UniqueResult<object>();
        //}

        T IQuantidadeSolicitacaoRede.BuscaQuantidade<T>(string competencia, string co_procedimento, string co_cbo, string cnes, int co_subgrupo)
        {
            string hql = "from ViverMais.Model.QuantidadeSolicitacaoRede quantidade";
            hql += " where quantidade.Competencia = '" + competencia + "'";
            hql += " and quantidade.Procedimento.Codigo = '" + co_procedimento + "'";
            hql += " and quantidade.CBO.Codigo = '" + co_cbo + "'";
            hql += " and quantidade.Estabelecimento.CNES = '" + cnes + "'";
            if(co_subgrupo != 0)
                hql += " and quantidade.SubGrupo.Codigo = " + co_subgrupo;
            else
                hql += " and quantidade.SubGrupo is null";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}
