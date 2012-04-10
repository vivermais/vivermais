using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;

namespace ViverMais.DAO.Agendamento
{
    public class GrupoAbrangenciaMunicipioDAO : ViverMaisServiceFacadeDAO, IGrupoAbrangenciaMunicipio
    {
        public GrupoAbrangenciaMunicipioDAO()
        {

        }

        T IGrupoAbrangenciaMunicipio.BuscarPelaChavePrimaria<T>(string id_grupoAbrangencia, string id_municipio)
        {
            string hql = "from ViverMais.Model.GrupoAbrangenciaMunicipio grupo";
            hql += " where grupo.Grupo.Codigo = '"+ id_grupoAbrangencia+"'";
            hql += " and grupo.Municipio.Codigo = '" + id_municipio + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}
