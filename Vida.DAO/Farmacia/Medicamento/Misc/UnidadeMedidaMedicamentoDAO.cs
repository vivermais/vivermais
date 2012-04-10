using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento.Misc;

namespace ViverMais.DAO.Farmacia.Medicamento.Misc
{
    public class UnidadeMedidaMedicamentoDAO : FarmaciaServiceFacadeDAO, IUnidadeMedidaMedicamento
    {
        #region IUnidadeMedidaMedicamento Members
            T IUnidadeMedidaMedicamento.VerificaDuplicidadeUnidadePorSigla<T>(string sigla, int co_unidade)
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.UnidadeMedidaMedicamento AS u WHERE u.Sigla = '" + sigla + "' AND u.Codigo <> " + co_unidade;
                return Session.CreateQuery(hql).UniqueResult<T>();
            }
        #endregion
    }
}
