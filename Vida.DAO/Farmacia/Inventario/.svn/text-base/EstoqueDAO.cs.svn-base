using System;
using NHibernate;
using Vida.Model;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using Vida.ServiceFacade.ServiceFacades.Farmacia.Inventario;

namespace Vida.DAO.Farmacia.Inventario
{
    public class EstoqueDAO : FarmaciaServiceFacadeDAO, IEstoque
    {
        IList<T> IEstoque.BuscarPorFarmacia<T>(int id_farmacia) 
        {
            string hql = string.Empty;
            hql = "FROM Vida.Model.Estoque AS e WHERE e.Farmacia.Codigo = " + id_farmacia;
            return Session.CreateQuery(hql).List<T>();
        }
    }
}
