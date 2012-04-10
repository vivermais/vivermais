using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;

namespace ViverMais.DAO.Agendamento
{
    public class FPODAO : AgendamentoServiceFacadeDAO, IFPO
    {
        #region IFpo Members

        public FPODAO()
        {

        }

        IList<T> IFPO.BuscarFPO<T>(string id_unidade, int competencia)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.FPO as fpo WHERE fpo.Estabelecimento.CNES = '" + id_unidade + "'";
            hql += " and fpo.Competencia = '" + competencia + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        T IFPO.BuscarFPO<T>(string id_unidade, int competencia, string procedimento)
        {
            string hql = "FROM ViverMais.Model.FPO fpo ";
            hql += " WHERE fpo.Estabelecimento.CNES='" + id_unidade + "'";
            hql+=" and fpo.Procedimento.Codigo ='" + procedimento + "'";
            hql += " and fpo.Competencia ='" + competencia + "'";

            T resultados = Session.CreateQuery(hql).UniqueResult<T>();
            return resultados;
        }

        T IFPO.BuscarCompetencia<T>(string unidade)
        {
            string hql = "Select max(fpo.Competencia) FROM ViverMais.Model.FPO fpo ";
            hql += " WHERE fpo.Estabelecimento.CNES = '" + unidade + "'";
            T resultados = Session.CreateQuery(hql).UniqueResult<T>();
            return resultados;
        }

        IList<T> IFPO.ListarUnidadesPorCompetencia<T>(int competencia)
        {
            string hql = "Select fpo.Estabelecimento.CNES from ViverMais.Model.FPO fpo where fpo.Competencia = " + competencia;
            return Session.CreateQuery(hql).List<T>();

        }

        IList<T> IFPO.ListarProcedimentosPorCompetenciaCNES<T>(int competencia, string cnes)
        {
            string hql = "Select fpo.Procedimento from ViverMais.Model.FPO fpo where fpo.Competencia = " + competencia + " and fpo.Estabelecimento.CNES = '" + cnes + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion



    }
}
