using System;
using NHibernate;
using Vida.Model;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace Vida.DAO.AtendimentoMedico.Urgencia
{
    public class TipoConsultorioDAO: UrgenciaServiceFacadeDAO, ITipoConsultorio
    {

        #region ITipoConsultorio Members

        T ITipoConsultorio.BuscarPorCor<T>(string cor) 
        {
            string hql = string.Empty;
            hql = "FROM Vida.Model.TipoConsultorio AS tc WHERE UPPER(tc.Cor) = '" + cor.ToUpper() + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        #endregion
    }
}
