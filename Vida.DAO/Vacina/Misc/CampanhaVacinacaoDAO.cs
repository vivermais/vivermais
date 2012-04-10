using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.Model;
using NHibernate.Criterion;

namespace ViverMais.DAO.Vacina.Misc
{
    public class CampanhaVacinacaoDAO : VacinaServiceFacadeDAO, ICampanhaVacinacao
    {

        #region ICampanhaVacinacao Members

        IList<T> ICampanhaVacinacao.BuscarPorAno<T>(int ano)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Campanha AS c WHERE TO_CHAR(c.DataInicio,'YYYY') = '" + ano + "'";
            return Session.CreateQuery(hql).List<T>();
            //return Session.CreateCriteria(typeof(Campanha))
            //    .Add(Expression.Eq("Ano", ano))
            //    .AddOrder(Order.Asc("Nome")).List<T>();
        }

        IList<T> ICampanhaVacinacao.BuscarItemCampanhaPorCampanha<T>(int codigoCampanha)
        {
            return Session.CreateQuery(
                "from ViverMais.Model.ItemCampanha item where item.Campanha.Codigo=" + codigoCampanha
                ).List<T>();
        }

        IList<T> ICampanhaVacinacao.BuscarCampanhasPorPeriodo<T>(DateTime datainicio, DateTime datafim)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Campanha AS c WHERE to_date(to_char(c.DataInicio,'DD/MM/YYYY'),'DD/MM/YYYY') >= to_date('" + datainicio.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')" +
                  " AND to_date(to_char(c.DataFim,'DD/MM/YYYY'),'DD/MM/YYYY') <= to_date('" + datafim.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')";
            return Session.CreateQuery(hql).List<T>();
        }

        T ICampanhaVacinacao.BuscarItemCampanha<T>(int co_itemvacina, int codigoCampanha)
        {
            return Session.CreateQuery(
                "from ViverMais.Model.ItemCampanha item where item.Campanha.Codigo = " + codigoCampanha + " and item.ItemVacina.Codigo = " + co_itemvacina
                ).UniqueResult<T>();
        }

        #endregion

    }
}
