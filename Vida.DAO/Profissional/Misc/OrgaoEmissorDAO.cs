using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;

namespace ViverMais.DAO.Profissional.Misc
{
    public class OrgaoEmissorDAO: ViverMaisServiceFacadeDAO, IOrgaoEmissor
    {
        #region IOrgaoEmissorDAO
        T IOrgaoEmissor.BuscarOrgaoEmissorPorCategoria<T>(string id_categoria)
        {
            string hql = string.Empty;
            hql += "from ViverMais.Model.OrgaoEmissor oe";
            hql += " where oe.CategoriaOcupacao.Codigo= '"+ id_categoria+"'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
        #endregion
    }
}
