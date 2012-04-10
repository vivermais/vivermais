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
    public class ParametroAgendaEspecificaDAO : AgendamentoServiceFacadeDAO, IParametroAgendaEspecifica
    {
        #region IParametroAgendaEspecifica Members

        public ParametroAgendaEspecificaDAO()
        {

        }

        T IParametroAgendaEspecifica.BuscarDuplicidade<T>(int id_parametroagenda, string id_unidade)
        {
            string hql = "FROM ViverMais.Model.ParametroAgendaEspecifica parametro ";
            hql += " WHERE parametro.ParametroAgenda.Codigo ='" + id_parametroagenda + "'";
            hql += " and parametro.ID_Unidade ='" + id_unidade + "'";
            T resultados = Session.CreateQuery(hql).UniqueResult<T>();
            return resultados;
        }

        IList<T> IParametroAgendaEspecifica.BuscarParametros<T>(int id_parametroagenda)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ParametroAgendaEspecifica parametros WHERE parametros.ParametroAgenda.Codigo = '" + id_parametroagenda + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        T IParametroAgendaEspecifica.BuscarParametrosEspecifica<T>(int id_parametroagenda, int id_programa, string id_unidade)
        { 
            string hql = "FROM ViverMais.Model.ParametroAgendaEspecifica parametros ";
            hql += " WHERE parametros.ParametroAgenda.Codigo = '"+id_parametroagenda+"' ";
            if (id_programa != 0)
            {
                hql += " and parametros.ID_Programa = '" + id_programa + "'";
            }
            if (id_unidade != "")
            {
                hql += " and parametros.ID_Unidade = '" + id_unidade + "'";
            }
            T resultados = Session.CreateQuery(hql).UniqueResult<T>();
            return resultados;
        }

        #endregion
    }
}
