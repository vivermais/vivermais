using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.Model;

namespace ViverMais.DAO.Vacina.Misc
{
    public class ItemVacinaDAO : VacinaServiceFacadeDAO, IItemVacina
    {
        bool IItemVacina.PermiteExclusao(int co_item)
        {
            string hql = string.Empty;
            hql = "SELECT l.Codigo FROM ViverMais.Model.LoteVacina as l WHERE l.ItemVacina.Codigo = " + co_item;
            return Session.CreateQuery(hql).List().Count == 0 ? true : false;
        }

        IList<T> IItemVacina.ListarPorVacina<T>(int co_vacina)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemVacina as i WHERE i.Vacina.Codigo = " + co_vacina;
            //hql += " ORDER BY i.Vacina.Nome, i.FabricanteVacina.Nome, i.Aplicacao";

            var result = (from item in Session.CreateQuery(hql).List<ItemVacina>()
                          orderby item.Vacina.Nome, item.FabricanteVacina.Nome, item.Aplicacao
                          select item);

            return (IList<T>)(object)result.ToList();
        }

        //int IItemVacina.BuscarProximoRegistro()
        //{
        //    string hql = "SELECT MAX(item.Codigo) FROM ViverMais.Model.ItemVacina as item ";
        //    return Session.CreateQuery(hql).UniqueResult<int>() + 1;
        //}

        IList<T> IItemVacina.ListarVacinas<T>()
        {
            string hql = "SELECT DISTINCT i.Vacina FROM ViverMais.Model.ItemVacina i ORDER BY i.Vacina.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IItemVacina.ListarFabricantes<T>(int co_vacina)
        {
            string hql = "SELECT DISTINCT i.FabricanteVacina FROM ViverMais.Model.ItemVacina i WHERE i.Vacina.Codigo = " + co_vacina +
            " ORDER BY i.FabricanteVacina.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IItemVacina.ListarPorVacina<T>(int co_vacina, int co_fabricante)
        {
            string hql = "FROM ViverMais.Model.ItemVacina i WHERE i.Vacina.Codigo = " + co_vacina + 
                " AND i.FabricanteVacina.Codigo = " + co_fabricante + " ORDER BY i.Aplicacao";
            return Session.CreateQuery(hql).List<T>();
        }
    }
}
