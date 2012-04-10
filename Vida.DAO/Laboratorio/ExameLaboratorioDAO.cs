using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Laboratorio;

namespace ViverMais.DAO.Laboratorio
{
    public class ExameLaboratorioDAO : LaboratorioServiceFacadeDAO, IExameLaboratorio
    {
        #region IExameLaboratorio Members

        public List<T> BuscarPorNomeUsual<T>(string nomeUsual)
        {
            string hql = "from ViverMais.Model.ExameLaboratorio exame where exame.NomeUsual like '%" + nomeUsual + "%'";
            return this.Session.CreateQuery(hql).List<T>().ToList<T>();
        }



        public List<T> BuscarPorSg<T>(string mnemonico)
        {
            string hql = "from ViverMais.Model.ExameLaboratorio exame where exame.Sg like '%" + mnemonico + "%'";
            return this.Session.CreateQuery(hql).List<T>().ToList<T>();
        }

        #endregion
    }
}
