using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;

namespace ViverMais.DAO.Farmacia.Misc
{
    public class UsuarioDistritoFarmaciaDAO : FarmaciaServiceFacadeDAO, IUsuarioDistritoFarmacia
    {
        #region IUsuarioDistritoFarmacia Members

        T IUsuarioDistritoFarmacia.BuscarPorUsuario<T>(int codigoUsuario)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.UsuarioDistritoFarmacia as udf where udf.CodigoUsuario = " + codigoUsuario;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IUsuarioDistritoFarmacia.BuscarDistritoPorUsuario<T>(int codigoUsuario)
        {
            string hql = string.Empty;
            hql = "Select udf.CodigoDistrito from ViverMais.Model.UsuarioDistritoFarmacia as udf ";
            hql = hql + "where udf.CodigoUsuario = " + codigoUsuario;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IUsuarioDistritoFarmacia.BuscarUsuariosPorDistrito<T>(int codigoDistrito)
        {
            string hql = string.Empty;
            hql = "Select udf.CodigoUsuario from ViverMais.Model.UsuarioDistritoFarmacia as udf ";
            hql = hql + "where udf.CodigoDistrito = " + codigoDistrito;
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion
    }
}
