using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Laboratorio;

namespace ViverMais.DAO.Laboratorio
{
    class PacienteLaboratorioDAO : LaboratorioServiceFacadeDAO, IPacienteLaboratorio
    {
        #region IPacienteLaboratorio Members

        public T BuscaPorRG<T>(string rg)
        {
            string hql = "from ViverMais.Model.PacienteLaboratorio paciente where paciente.Rg = '" + rg + "'";
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }

        public T BuscaPorCartaoSus<T>(string numeroCartao)
        {
            string hql = "from ViverMais.Model.PacienteLaboratorio paciente where paciente.CartaoSus = '" + numeroCartao + "'";
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }

        #endregion
    }
}
