using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Engine.Query;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;

namespace ViverMais.DAO.Agendamento
{
    public class PactoAbrangenciaDAO : ViverMaisServiceFacadeDAO, IPactoAbrangencia
    {
        #region IPactoAbrangencia Members

        public PactoAbrangenciaDAO()
        {
            
        }
        T IPactoAbrangencia.BuscarPactoPorGrupoAbrangencia<T>(string id_grupo)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.PactoAbrangencia pactoabrangencia";
            hql += " where pactoabrangencia.Grupo.Codigo=" + id_grupo;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IPactoAbrangencia.BuscarPactoAbrangenciaPorGrupoAbrangencia<T>(string id_grupo)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.PactoAbrangencia pactoabrangencia";
            hql += " where pactoabrangencia.Grupo.Codigo=" + id_grupo;
            return Session.CreateQuery(hql).List<T>();
        }
        
        #endregion
    }
}
