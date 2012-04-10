using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vida.Model;
using NHibernate;
using NHibernate.Criterion;
using Vida.ServiceFacade.ServiceFacades.EstabelecimentoDeSaude;

namespace Vida.DAO.EstabelecimentoDeSaude
{
    public class EstabelecimentoDeSaudeDAO: NHibernateDAO, IEstabelecimentoDeSaude
    {
        #region IEstabelecimentoDeSaude Members
        
        public EstabelecimentoDeSaudeDAO() : base("vida")
        {
        }
        
        #endregion

        #region IServiceFacade Members

            T Vida.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
            {
                ICriteria criteria = Session.CreateCriteria(typeof(Vida.Model.EstabelecimentoSaude));
                criteria.Add(Expression.Eq("Codigo", codigo.ToString()));
                return (T)(object)criteria.UniqueResult<Vida.Model.EstabelecimentoSaude>();
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
