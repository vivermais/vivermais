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
    public class ParametroAgendaDAO : AgendamentoServiceFacadeDAO, IParametroAgenda
    {
        #region IParametroAgenda Members

        public ParametroAgendaDAO()
        {

        }

        //IList<T> IParametroAgenda.BuscarDuplicidade<T>(string id_unidade, string id_procedimento)
        //{
        //    string hql = "FROM ViverMais.Model.ParametroAgenda parametro ";
        //    hql += " WHERE parametro.Cnes ='" + id_unidade + "'";
        //    if (id_procedimento!="0")
        //    {
        //        hql += " and parametro.ID_Procedimento ='" + id_procedimento + "'";
        //    }

        //    IList<T> resultados = Session.CreateQuery(hql).List<T>();
        //    return resultados;
        //}

        //T IParametroAgenda.BuscarParametroAgendaUnidade(int co_parametroAgenda)
        //{
        //    string hql = "Select parametros. from ViverMais.Model.ParametroAgenda parametros WHERE parametros.Codigo = "+ co_parametroAgenda;
        //    return Session.CreateQuery(hql).UniqueResult<T>();
        //}
        IList<T> IParametroAgenda.ListaEstabelecimentosComParametroDistrital<T>(int id_distrito)
        {
            string hql = string.Empty;
            hql = "Select est from ViverMais.Model.EstabelecimentoSaude est, ViverMais.Model.ParametroAgenda parametro";
            hql += " where parametro.Cnes = est.CNES";
            //hql += " Inner Join ViverMais.Model.ParametroAgenda parametro ON parametro.Cnes = est.CNES";
            hql += " and est.Bairro.Distrito.Codigo = '" + id_distrito + "' and parametro.Percentual <> 0 and parametro.TipoAgenda = " + Convert.ToInt32(ParametroAgenda.TipoDeAgenda.DISTRITAL);
             
            
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IParametroAgenda.BuscarParametros<T>(string id_unidade, string tipo_configuracao, string co_procedimento, string co_cbo, string co_subgrupo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ParametroAgenda parametros WHERE parametros.Cnes = '" + id_unidade + "'";
            hql += " and parametros.TipoConfiguracao = '"+tipo_configuracao+"'";
            if (tipo_configuracao == ParametroAgenda.CONFIGURACAO_PROCEDIMENTO)
            {
                hql += " and parametros.Procedimento.Codigo = '" + co_procedimento + "'";
                hql += " and parametros.Cbo.Codigo = '" + co_cbo + "'";
                if (String.IsNullOrEmpty(co_subgrupo))
                    hql += " and parametros.SubGrupo is null";
                else
                    hql += " and parametros.SubGrupo.Codigo = " + co_subgrupo;
                
                //hql += " and parametros.SubGrupo.Codigo = " + co_subgrupo;
            }
            return Session.CreateQuery(hql).List<T>();
        }

        //IList<T> IParametroAgenda.BuscarParametros<T>(string id_unidade, string tipo_configuracao, string co_procedimento, string co_cbo, int co_subgrupo)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.ParametroAgenda parametros WHERE parametros.Cnes = '" + id_unidade + "'";
        //    hql += " and parametros.TipoConfiguracao = '" + tipo_configuracao + "'";
        //    if (tipo_configuracao == ParametroAgenda.CONFIGURACAO_PROCEDIMENTO)
        //    {
        //        hql += " and parametros.Procedimento.Codigo = '" + co_procedimento + "'";
        //        hql += " and parametros.Cbo.Codigo = '" + co_cbo + "'";
        //        hql += " and parametros.SubGrupo.Codigo = " + co_subgrupo;
        //    }
        //    return Session.CreateQuery(hql).List<T>();
        //}

        T IParametroAgenda.BuscarParametrosPorTipo<T>(string cnes, int tipoagenda, string tipo_configuracao)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ParametroAgenda parametros WHERE parametros.Cnes = '"+cnes+"'";
            hql += " and parametros.TipoAgenda="+tipoagenda;
            hql += " and parametros.TipoConfiguracao = '" + tipo_configuracao + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IParametroAgenda.BuscarUnidadesEspecificas<T>(string cnes, int tipoagenda)
        {
            string hql = string.Empty;
            hql += "FROM ViverMais.Model.ParametroAgendaEspecifica parametroagendaespecifica";
            hql += " where parametroagendaespecifica.ParametroAgenda.Cnes = '" + cnes + "'";
            hql += " and parametroagendaespecifica.ParametroAgenda.TipoAgenda = " + tipoagenda;
            //hql += " and parametroagenda.Codigo = parametroagendaespecifica.ParametroAgenda.Codigo";
            return Session.CreateQuery(hql).List<T>();
        }

        T IParametroAgenda.BuscarAgenda<T>(string unidade, DateTime data, string turno, string procedimento, int cmp, string profissional, string cbo)
        { 
            string hql = "from ViverMais.Model.Agenda agenda ";
            hql += " where agenda.Estabelecimento.CNES = '" + unidade + "'";
            hql += " and agenda.Data = '" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and agenda.Turno = '"+turno+"'";
            hql += " and agenda.Procedimento.Codigo = '" + procedimento + "'";
            hql += " and agenda.Competencia = '"+cmp+"'";
            hql += " and agenda.ID_Profissional = '"+profissional+"'";
            hql += " and agenda.Cbo.Codigo = '" + cbo + "'";

            return Session.CreateQuery(hql).UniqueResult<T>();

        }
        #endregion
    }
}
