﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.Model;

namespace ViverMais.DAO.Vacina.Misc
{
    public class ItemInventarioVacinaDAO: VacinaServiceFacadeDAO, IItemInventarioVacina
    {
        IList<T> IItemInventarioVacina.BuscarItensInventario<T>(object co_lote, object co_sala)
        {
            string hql = "From ViverMais.Model.ItemInventarioVacina i Where"
            + " i.LoteVacina.Codigo = " + co_lote
            + " and i.Inventario.Sala.Codigo = " + co_sala;

            return Session.CreateQuery(hql).List<T>();
        }
    }
}
