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
    public class LaudoDAO : AgendamentoServiceFacadeDAO, ILaudo
    {
        #region ILaudo Members

        public LaudoDAO()
        {

        }

        IList<T> ILaudo.BuscaPorSolicitacao<T>(string id_solicitacao)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.Laudo laudo";
            hql += " where laudo.Solicitacao.Codigo=" + id_solicitacao;
            return Session.CreateQuery(hql).List<T>();
        }

        T ILaudo.VerificaDuplicidade<T>(string nomeArquivo)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.Laudo laudo";
            hql += " where laudo.Endereco='" + nomeArquivo + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
        #endregion
    }
}
