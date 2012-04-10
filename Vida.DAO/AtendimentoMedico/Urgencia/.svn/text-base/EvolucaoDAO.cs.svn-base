using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace Vida.DAO.AtendimentoMedico.Urgencia
{
    public class EvolucaoDAO : UrgenciaServiceFacadeDAO, IEvolucao
    {
        #region
            IList<T> IEvolucao.buscaPorProntuario<T>(int co_prontuario) 
            {
                string hql = string.Empty;
                       hql = "from Vida.Model.Evolucao evolucao where evolucao.Prontuario.Codigo = " + co_prontuario;
                       hql+= " order by evolucao.Data desc";
                return Session.CreateQuery(hql).List<T>();
            }
        #endregion

        public EvolucaoDAO()
        {

        }
    }
}
