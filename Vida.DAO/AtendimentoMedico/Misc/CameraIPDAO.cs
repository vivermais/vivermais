using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using ViverMais.Model;

namespace ViverMais.DAO.AtendimentoMedico.Misc
{
    public class CameraIPDAO: UrgenciaServiceFacadeDAO, ICameraIP
    {
        IList<T> ICameraIP.BuscarPorUnidade<T>(string cnes) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.CameraIP AS c WHERE c.CNESUnidade='" + cnes + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        bool ICameraIP.ValidarCadastro(string endereco, int codigo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.CameraIP AS c WHERE c.Codigo <> " + codigo + " AND c.Endereco = '" + endereco + "'";
            return Session.CreateQuery(hql).List<CameraIP>().Count > 0 ? false : true;
        }
    }
}
