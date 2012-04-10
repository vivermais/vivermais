using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;

namespace ViverMais.DAO.Agendamento
{
    public class PactoReferenciaSaldoDAO : ViverMaisServiceFacadeDAO, IPactoReferenciaSaldo
    {
        public PactoReferenciaSaldoDAO() { }

        IList<T> IPactoReferenciaSaldo.BuscarPorPactoAgregado<T>(int id_pacto_Agregado)
        {
            string hql = "from ViverMais.Model.PactoReferenciaSaldo pactoReferencia";
            hql += " where pactoReferencia.PactoAgregadoProcedCBO.Codigo = " + id_pacto_Agregado;
            return Session.CreateQuery(hql).List<T>();
        }

        T IPactoReferenciaSaldo.BuscarPorPactoAgregado<T>(int id_pacto_Agregado, int mes)
        {
            string hql = "from ViverMais.Model.PactoReferenciaSaldo pactoReferencia";
            hql += " where pactoReferencia.PactoAgregadoProcedCBO.Codigo = " + id_pacto_Agregado;
            hql += " and pactoReferencia.Mes = " + mes;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}
