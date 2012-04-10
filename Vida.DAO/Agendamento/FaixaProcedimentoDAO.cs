using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using NHibernate.Mapping;

namespace ViverMais.DAO.Agendamento
{
    public class FaixaProcedimentoDAO : AgendamentoServiceFacadeDAO, IFaixaProcedimento
    {
        IList<T> IFaixaProcedimento.BuscaFaixa<T,A>(A faixas)
        {
            string hql = string.Empty;
            hql += "Select f from ViverMais.Model.Faixa f";
            hql += " where f.Codigo > 1";
            List<ViverMais.Model.FaixaProcedimento> faixas2 = (List<ViverMais.Model.FaixaProcedimento>)(object)faixas;
            
            foreach (object faixa in faixas2)
            {
                hql += "and f.Codigo <> '" + ((ViverMais.Model.FaixaProcedimento)faixa).Faixa.Codigo + "'";
            }
            //hql += " and faixaProcedimento.Id_Procedimento = '"+ id_procedimento+"'";
            //hql += " and faixaProcedimento.Id_Unidade='"+ cnes+"'";
            return Session.CreateQuery(hql).List<T>();
        }

        T IFaixaProcedimento.BuscarDeProcedimento<T>(int id_faixa)
        {
            string hql = string.Empty;
            hql += " From ViverMais.Model.FaixaProcedimento fp";
            hql += " where fp.Faixa.Codigo = '" + id_faixa + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}
