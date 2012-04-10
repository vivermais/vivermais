using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Laboratorio;

namespace ViverMais.DAO.Laboratorio
{
    class SolicitanteExameLaboratorioDAO : LaboratorioServiceFacadeDAO, ISolicitanteExameLaboratorio
    {
        #region ISolicitanteExameLaboratorio Members

        public T BuscaPorNumeroConselho<T>(int numeroConselho)
        {
            string hql = "from ViverMais.Model.SolicitanteExameLaboratorio solicitante where solicitante.NumeroConselho = " + numeroConselho.ToString();
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }

        public List<T> BuscaPorNome<T>(string nome)
        {
            string hql = "from ViverMais.Model.SolicitanteExameLaboratorio solicitante where solicitante.Nome like '%" + nome + "%'";
            return this.Session.CreateQuery(hql).List<T>().ToList<T>();
        }

        #endregion
    }
}
