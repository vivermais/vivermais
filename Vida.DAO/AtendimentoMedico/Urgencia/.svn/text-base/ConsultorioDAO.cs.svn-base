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
    public class ConsultorioDAO: UrgenciaServiceFacadeDAO, IConsultorio
    {
        IList<T> IConsultorio.BuscarPorEstabelecimento<T>(string co_estabelecimento) 
        {
            string hql = string.Empty;
            hql = "FROM Vida.Model.Consultorio AS c WHERE c.CodigoUnidade = '" + co_estabelecimento + "'";
            return Session.CreateQuery(hql).List<T>();
        }
    }
}
