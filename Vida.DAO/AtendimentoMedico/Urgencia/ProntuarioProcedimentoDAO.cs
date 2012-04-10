using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace Vida.DAO.AtendimentoMedico.Urgencia
{
    public class ProntuarioProcedimentoDAO: UrgenciaServiceFacadeDAO, IProntuarioProcedimento
    {
        #region IProntuarioProcedimento

            IList<T> IProntuarioProcedimento.BuscarPorProntuarioMedico<T>(int co_prontuariomedico) 
            {
                string hql = string.Empty;
                hql = "FROM Vida.Model.ProntuarioProcedimento AS pp WHERE pp.ProntuarioMedico.Codigo = " + co_prontuariomedico;
                return Session.CreateQuery(hql).List<T>();
            }

            IList<T> IProntuarioProcedimento.BuscarPorProntuario<T>(int co_prontuario) 
            {
                string hql = string.Empty;
                hql = "FROM Vida.Model.ProntuarioProcedimento AS pp WHERE pp.ProntuarioMedico.Prontuario.Codigo = " + co_prontuario;
                hql += " ORDER BY pp.ProntuarioMedico.Data DESC";
                return Session.CreateQuery(hql).List<T>();
            }

            T IProntuarioProcedimento.BuscarPorProntuarioMedicoEProcedimento<T>(int co_prontuariomedico, string co_procedimento) 
            {
                string hql = string.Empty;
                hql = "FROM Vida.Model.ProntuarioProcedimento AS pp WHERE pp.ProntuarioMedico.Codigo = " + co_prontuariomedico + " AND pp.CodigoProcedimento = '" + co_procedimento + "'";
                return Session.CreateQuery(hql).UniqueResult<T>();
            }
        #endregion
    }
}
