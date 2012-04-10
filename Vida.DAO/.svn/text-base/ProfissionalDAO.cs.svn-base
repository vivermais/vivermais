using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Vida.Model;
using Vida.ServiceFacade.ServiceFacades.Profissional;

namespace Vida.DAO.Profissional
{
    public class ProfissionalDAO : VidaServiceFacadeDAO, IProfissional
    {
        public ProfissionalDAO()
        {
        }

        #region IProfissional Members
            IList<E> IProfissional.BuscarPorEquipe<A, C, E>(A area_equipe, C codigo_equipe) 
            {
                Area area     = (Area)((object)area_equipe);
                string codigo = (string)((object)codigo_equipe);

                string hql = string.Empty;
                hql = "FROM Vida.Model.EquipeProfissional AS ep WHERE ep.Equipe.Area.Codigo = '" + area.Codigo + "' AND ep.Equipe.Area.Municipio.Codigo = '" + area.Municipio.Codigo + "' AND ep.Equipe.Codigo = '" + codigo + "'";
                return Session.CreateQuery(hql).List<E>();
            }

            T IProfissional.BuscaProfissionalPorVinculo<T>(int categoria, int numeroconselho)
            {
                string hql = "Select vinculo.Profissional";
                hql += " from Vida.Model.VinculoProfissional vinculo , Vida.Model.CBO cbo";
                hql += " where cbo.CategoriaOcupacao.Codigo = '"+categoria+"'";
                hql += " and vinculo.RegistroConselho = '" + numeroconselho + "'";
                hql += " and vinculo.CBO.Codigo = cbo.Codigo";
                IQuery query = Session.CreateQuery(hql);
                return query.UniqueResult<T>();                                     
            }


        #endregion
    }
}
