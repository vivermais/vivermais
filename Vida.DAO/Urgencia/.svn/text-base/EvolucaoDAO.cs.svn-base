using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Vida.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace Vida.DAO.AtendimentoMedico.Urgencia
{
    public class EvolucaoDAO : NHibernateDAO, IEvolucao
    {
        public EvolucaoDAO() : base("urgencia")
        {

        }


        #region IServiceFacade Members

        T Vida.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        {
            //ICriteria criteria = Session.CreateCriteria(typeof(Vida.Model.Evolucao));
            //criteria.Add(Expression.Eq("Codigo", codigo.ToString()));
            //return (T)(object)criteria.UniqueResult<Vida.Model.Evolucao>();
            throw new NotImplementedException();
        }

        void Vida.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
        {
            throw new NotImplementedException();
        }

        IList<T> Vida.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
