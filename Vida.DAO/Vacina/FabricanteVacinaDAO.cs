using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vida.ServiceFacade.ServiceFacades.Vacina.FabricanteVacina;

namespace Vida.DAO.Vacina.FabricanteVacina
{
    public class FabricanteVacinaDAO : VacinaServiceFacadeDAO, IFabricanteVacina
    {
        IList<T> IFabricanteVacina.BuscaPorNome<T>(string nome)
        {
            string hql = string.Empty;
            hql = "FROM Vida.Model.FabricanteVacina AS f WHERE f.Nome = '" + nome + "%' ";
            return Session.CreateQuery(hql).List<T>();
        }
    }
}
