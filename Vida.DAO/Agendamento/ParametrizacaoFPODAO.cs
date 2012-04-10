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
    public class ParametrizacaoFPODAO : AgendamentoServiceFacadeDAO, IParametrizacaoFPO
    {
        #region IParametrizacaoFPO Members

        public ParametrizacaoFPODAO()
        {
        }
        IList<T> IParametrizacaoFPO.BuscaParametrizacaoPorUnidade<T>(string cnes, string procedimento)
        {
            string hql = "FROM ViverMais.Model.ParametrizacaoFPO parametrizacao ";
            hql += " WHERE parametrizacao.Cnes='"+cnes+"'";

            if (procedimento != "")
            {
                hql += " and parametrizacao.Id_Procedimento ='" + procedimento + "'";
            }
            return Session.CreateQuery(hql).List<T>();
        }

        #endregion
    }
}
