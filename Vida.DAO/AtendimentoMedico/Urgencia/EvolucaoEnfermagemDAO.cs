using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class EvolucaoEnfermagemDAO : UrgenciaServiceFacadeDAO, IEvolucaoEnfermagem
    {
        #region
            IList<T> IEvolucaoEnfermagem.BuscarPorProntuario<T>(long co_prontuario) 
            {
                string hql = string.Empty;
                       hql = "from ViverMais.Model.EvolucaoEnfermagem evolucao where evolucao.Prontuario.Codigo = " + co_prontuario;
                       hql+= " order by evolucao.Data desc";
                return Session.CreateQuery(hql).List<T>();
            }
        #endregion

        public EvolucaoEnfermagemDAO()
        {

        }
    }
}
