using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.Model;

namespace ViverMais.DAO.Vacina.Misc
{
    public class ItemDoseVacinaDAO: VacinaServiceFacadeDAO, IItemDoseVacina
    {
        T IItemDoseVacina.BuscarItemDoseVacina<T>(int co_vacina, int co_dose)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemDoseVacina as item";
            hql += " WHERE item.Vacina.Codigo = " + co_vacina;
            hql += " AND item.DoseVacina.Codigo = " + co_dose;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}
