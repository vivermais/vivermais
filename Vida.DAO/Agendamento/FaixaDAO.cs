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
using ViverMais.DAO.FormatoDataOracle;

namespace ViverMais.DAO.Agendamento
{
    public class FaixaDAO : AgendamentoServiceFacadeDAO, IFaixa
    {
        #region IFaixa Members

        public FaixaDAO()
        {

        }

        IList<T> IFaixa.BuscarFaixaAPAC<T>(int ano)
        {
            string hql = "from ViverMais.Model.Faixa faixa where faixa.Quantidade_utilizada < faixa.Quantitativo";
            hql += " and faixa.Ano_vigencia ='" + ano + "'";
            return Session.CreateQuery(hql).List<T>();

        }

        T IFaixa.BuscarCodigoFaixa<T>(string faixa)
        {
            string hql = "from ViverMais.Model.Faixa faixa where faixa.FaixaInicial <='" + faixa + "' and faixa.FaixaFinal >='" + faixa+"'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        #endregion

    }
}
