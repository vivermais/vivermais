using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;

namespace ViverMais.DAO.Localidade
{
    public class LogradouroDAO : ViverMaisServiceFacadeDAO, ILogradouro
    {

        #region ILogradouro Members

        T ILogradouro.BuscarPorCEP<T>(long cep)
        {
            string hql = "from ViverMais.Model.Logradouro log where log.CEP=" + cep;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        #endregion
    }
}
