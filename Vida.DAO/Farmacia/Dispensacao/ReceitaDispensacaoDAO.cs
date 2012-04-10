using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Dispensacao;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;

namespace ViverMais.DAO.Farmacia.Dispensacao
{
    public class ReceitaDispensacaoDAO : FarmaciaServiceFacadeDAO, IReceitaDispensacao
    {
        #region IReceitaDispensacao Members

        IList<T> IReceitaDispensacao.BuscarReceitaPorPaciente<T>(string codigoPaciente)
        {
            string hql = string.Empty;
            hql = "FROM ReceitaDispensacao rd WHERE rd.CodigoPaciente = '" + codigoPaciente + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IReceitaDispensacao.BuscarMedicamentos<T>(long codigoReceita)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemReceitaDispensacao as item WHERE  item.Receita.Codigo = " + codigoReceita.ToString();
            return Session.CreateQuery(hql).List<T>();
        }

        T IReceitaDispensacao.ValidarCadastroReceita<T>(string co_paciente, string co_profissional, DateTime datareceita)
        {
            string hql = string.Empty;
            hql = "FROM ReceitaDispensacao AS rd WHERE rd.CodigoPaciente = '" + co_paciente + "'";
            hql += " AND rd.CodigoProfissional = '" + co_profissional + "'";
            hql += " AND to_char(rd.DataReceita,'dd/mm/yyyy') = '" + datareceita.ToString("dd/MM/yyyy") + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        #endregion
    }
}
