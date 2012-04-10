using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.DAO.Procedimento.Misc
{
    public class GrupoCBODAO : ViverMaisServiceFacadeDAO, IGrupoCBO
    {
        T IGrupoCBO.BuscarPorCBO<T>(string co_cbo)
        {
            string hql = "select cbo.GrupoCBO from ViverMais.Model.CBO cbo where cbo.Codigo = '"+ co_cbo+"'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}
