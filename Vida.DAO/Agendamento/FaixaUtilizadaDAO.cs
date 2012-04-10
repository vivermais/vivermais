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
    public class FaixaUtilizadaDAO : AgendamentoServiceFacadeDAO, IFaixaUtilizada
    {
        #region IFaixaUtilizada Members

        public FaixaUtilizadaDAO()
        {

        }

        IList<T> IFaixaUtilizada.BuscarFaixaUtilizada<T>(string faixaAPAC)
        {
            string hql = "from ViverMais.Model.FaixaUtilizada faixa_utilizada where faixa_utilizada.Faixa.Codigo ='"+faixaAPAC+"'";
            return Session.CreateQuery(hql).List<T>();
        }

        T IFaixaUtilizada.BuscaFaixaUtilizadaPorNumeroFaixa<T>(string numeroFaixa, string codigofaixa)
        { 
            string hql = "from ViverMais.Model.FaixaUtilizada faixa_utilizada where faixa_utilizada.Faixa_Utilizada = '"+numeroFaixa+"'";
            hql += " and faixa_utilizada.Faixa.Codigo ='" + codigofaixa + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }


        #endregion


     }
}
