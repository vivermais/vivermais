using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.Model;

namespace ViverMais.DAO.Agendamento
{
    public class ProgramaDeSaudeUnidadeDAO : AgendamentoServiceFacadeDAO, IProgramaDeSaudeUnidade
    {
        #region IProgramaDeSaudeUnidade Members

        public ProgramaDeSaudeUnidadeDAO()
        {

        }

        T IProgramaDeSaudeUnidade.BuscarPorCodigo<T>(int codigoPrograma, string codigoUnidade)
        {
            string hql = "FROM ViverMais.Model.ProgramaDeSaudeUnidade progSaudeUnidade";
            hql += " WHERE progSaudeUnidade.ProgramaDeSaude.Codigo = " + codigoPrograma;
            hql += " AND progSaudeUnidade.Estabelecimento.CNES = '" + codigoUnidade + "'";
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }

        bool IProgramaDeSaudeUnidade.VerificaDuplicidade(int codigoPrograma, string codigoUnidade)
        {
            string hql = "FROM ViverMais.Model.ProgramaDeSaudeUnidade progSaudeUnidade";
            hql += " WHERE progSaudeUnidade.ProgramaDeSaude.Codigo = " + codigoPrograma;
            hql += " AND progSaudeUnidade.CodigoUnidade = '" + codigoUnidade + "'";
            return this.Session.CreateQuery(hql).UniqueResult() == null ? true : false;
        }

        //void IProgramaDeSaudeUnidade.DeletarProgramaDeSaudeUnidadePorProgramaSaude(int id_programa)
        //{
        //    string hql = "DELETE from AGD_PROGRAMASAUDE_UNIDADE";
        //    hql += " where CO_PROGRAMA = " + id_programa;
        //    //hql += " and CO_PROCEDIMENTO ='" + id_procedimento + "'";
        //    //hql += " and CO_OCUPACAO ='" + id_procedimento + "'";
        //    Session.CreateSQLQuery(hql).ExecuteUpdate();
        //}

        IList<T> IProgramaDeSaudeUnidade.ListarPorPrograma<T>(int codigoPrograma, bool ativo)
        {
            string hql = "FROM ViverMais.Model.ProgramaDeSaudeUnidade progSaudeUnidade";
            hql += " WHERE progSaudeUnidade.ProgramaDeSaude.Codigo = " + codigoPrograma + " and progSaudeUnidade.Ativo = " + (ativo ? 1 : 0);
            return this.Session.CreateQuery(hql).List<T>();
        }

        IList<T> IProgramaDeSaudeUnidade.ListaUnidadesPorPrograma<T>(int codigoPrograma, bool ativo)
        {
            string hql = "Select progSaudeUnidade.Estabelecimento FROM ViverMais.Model.ProgramaDeSaudeUnidade progSaudeUnidade";
            hql += " WHERE progSaudeUnidade.ProgramaDeSaude.Codigo = " + codigoPrograma + " and progSaudeUnidade.Ativo = " + (ativo ? 1 : 0);
            return this.Session.CreateQuery(hql).List<T>();
        }

        IList<T> IProgramaDeSaudeUnidade.ListaProgramasPorUnidade<T>(string cnes)
        {
            string hql = "FROM ViverMais.Model.ProgramaDeSaudeUnidade programasaudeunidade";
            hql += " WHERE programasaudeunidade.Estabelecimento.CNES ='" + cnes + "'";
            return this.Session.CreateQuery(hql).List<T>();
        }
        #endregion
    }
}
