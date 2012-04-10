using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using NHibernate;
using ViverMais.Model;
using NHibernate.Criterion;
using ViverMais.DAO.FormatoDataOracle;

namespace ViverMais.DAO.Agendamento
{
    public class ListaProcuraDAO : AgendamentoServiceFacadeDAO, IListaProcura
    {
        public ListaProcuraDAO()
        {

        }

        #region IListaProcura Members

        IList<T> IListaProcura.BuscaNaListaPorPacientePorProcedimento<T>(string id_paciente, string id_procedimento)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.ListaProcura lista";
            hql += " where lista.Id_paciente = '" + id_paciente + "'";
            hql += " and lista.Id_procedimento = '" + id_procedimento + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IListaProcura.BuscaPorProcedimento<T>(string id_procedimento)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.ListaProcura lista";
            hql += " where lista.Id_procedimento = '" + id_procedimento + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IListaProcura.BuscaPorPeriodoPorProcedimento<T>(string periodoInicial, string periodoFinal, string id_procedimento)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(ListaProcura));
            criteria.Add(Expression.Between("DataInicial", FormatoData.ConverterData(DateTime.Parse(periodoInicial), FormatoData.nomeBanco.ORACLE), FormatoData.ConverterData(DateTime.Parse(periodoFinal), FormatoData.nomeBanco.ORACLE)));
            criteria.Add(Expression.Eq("Id_procedimento",id_procedimento));
            return criteria.List<T>();
        }

        IList<T> IListaProcura.BuscaPorAgendadoNaoAgendado<T>(string agendado)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.ListaProcura lista";
            if (agendado == "A")//Agendado
                hql += " where lista.Agendado = 1";
            else
                hql += " where lista.Agendado = 0";
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion
    }
}
