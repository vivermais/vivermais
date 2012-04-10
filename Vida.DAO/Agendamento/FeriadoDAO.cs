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
    public class FeriadoDAO : AgendamentoServiceFacadeDAO, IFeriado
    {
        #region IFeriado Members

        public FeriadoDAO()
        {

        }

        bool IFeriado.VerificaData(DateTime data)
        {
            String hql = "from ViverMais.Model.Feriado feriado where feriado.Data='" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "'";
            return Session.CreateQuery(hql).List<ViverMais.Model.Feriado>().Count != 0 ? true : false;

        }

        #endregion

    }
}
