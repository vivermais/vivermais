using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;

namespace ViverMais.DAO.Vacina.Misc
{
    public class DoseVacinaDAO: VacinaServiceFacadeDAO, IDoseVacina
    {
        IList<T> IDoseVacina.BuscarPorVacina<T>(int co_vacina)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.DoseVacina as d WHERE d.Vacina.Codigo = " + co_vacina;
            return Session.CreateQuery(hql).List<T>();
        }

        //int IDoseVacina.BuscarProximoRegistro()
        //{
        //    string hql = string.Empty;
        //    hql = "SELECT MAX(d.Codigo) FROM ViverMais.Model.DoseVacina as d";
        //    return Session.CreateQuery(hql).UniqueResult<T>() + 1;
        //}
    }
}
