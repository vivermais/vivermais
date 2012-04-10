using System;
using NHibernate;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;

namespace ViverMais.DAO.AtendimentoMedico.Misc
{
    public class FaixaEtariaDAO : UrgenciaServiceFacadeDAO, IFaixaEtaria
    {
        #region IFaixaEtaria
            T IFaixaEtaria.buscaPorIdade<T>(int idade) 
            {
                string hql = "from ViverMais.Model.FaixaEtaria faixaetaria ";
                hql += " where faixaetaria.Codigo > 0 and ";
                hql += idade + " > faixaetaria.Inicial and " + idade + " <= faixaetaria.Final ";
                return Session.CreateQuery(hql).UniqueResult<T>();
            }
        #endregion
    }
}
