using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;

namespace ViverMais.DAO.Vacina.Misc
{
    public class MotivoMovimentoVacinaDAO : VacinaServiceFacadeDAO, IMotivoMovimentoVacina
    {
        IList<T> IMotivoMovimentoVacina.BuscarPorTipoMovimento<T>(int co_tipomovimento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.MotivoMovimentoVacina v WHERE v.TipoMovimento.Codigo=" + co_tipomovimento
                + " ORDER BY v.Nome";
            return Session.CreateQuery(hql).List<T>();
        }
    }
}
