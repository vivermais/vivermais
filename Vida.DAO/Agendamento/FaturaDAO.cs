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
//using ViverMais.ServiceFacade.ServiceFacades.Fatura;

namespace ViverMais.DAO.Agendamento
{
    public class FaturaDAO : AgendamentoServiceFacadeDAO, IFatura
    {
        #region IFatura Members

        public FaturaDAO()
        {

        }

        //T IFatura.BuscarDuplicidade<T>(string id_unidade, int competencia, string tipo)
        //{
        //    string hql = "FROM ViverMais.Model.Fatura fatura ";
        //    hql += " WHERE fatura.ID_Unidade ='" + id_unidade + "'";
        //    hql += " and fatura.Competencia ='" + competencia + "'";
        //    hql += " and fatura.Tipo ='" + tipo + "'";
        //    return Session.CreateQuery(hql).UniqueResult<T>();             
        //}

        T IFatura.BuscaPorUnidadeCompetenciaTipo<T>(string unidade, int competencia, string tipo)
        {
            string hql = "FROM ViverMais.Model.Fatura fatura ";
            hql += " WHERE fatura.Id_Unidade.CNES ='" + unidade + "'";
            hql += " and fatura.Competencia ='" + competencia + "'";
            hql += " and fatura.Tipo ='" + tipo + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IFatura.BuscarUltimaFatura<T>(string co_unidade, string tipo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Fatura fatura WHERE fatura.Id_Unidade.CNES='" + co_unidade + "' AND fatura.Tipo='" + tipo + "'";
            hql += " ORDER BY fatura.Data";
            return (T)(object)((IList<Fatura>)Session.CreateQuery(hql).List<Fatura>()).LastOrDefault();
        }

        #endregion

    }
}
