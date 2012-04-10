using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.Model;
using System.Collections;

namespace ViverMais.DAO.Agendamento
{
    public class UnidadeDAO : AgendamentoServiceFacadeDAO , IUnidade
    {
        #region IUnidade Members

        public UnidadeDAO()
        {

        }

        bool IUnidade.VerificaEstabelecimentoToleranteFeriado(string id_unidade)
        {
            string hql = string.Empty;
            hql += "from ViverMais.Model.Unidade unidade where unidade.CNES='" + id_unidade + "'";
            hql += " and unidade.IntoleranteFeriado = 0";
            return Session.CreateQuery(hql).List<Unidade>().Count != 0 ? true : false;
        }

        //IList IUnidade.VerificaEstabelecimentoToleranteFeriado(string id_unidade)
        //{
        //    string hql = string.Empty;
        //    hql += "from ViverMais.Model.Unidade unidade where unidade.ID_Unidade='" + id_unidade + "'";
        //    hql += " and unidade.IntoleranteFeriado = 0";
        //    return Session.CreateQuery(hql).List();
        //}
        #endregion
    }
}
