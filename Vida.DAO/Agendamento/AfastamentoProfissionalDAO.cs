using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.DAO.FormatoDataOracle;

namespace ViverMais.DAO.Agendamento
{
    public class AfastamentoProfissionalDAO : AgendamentoServiceFacadeDAO, IAfastamentoProfissional
    {
        IList<T> IAfastamentoProfissional.ListaAfastamentoProfissional<T>(string id_unidade, string id_profissional)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.AfastamentoProfissional as afastamento";
            hql += " WHERE afastamento.Profissional.CPF = '" + id_profissional + "'";
            hql += " and afastamento.Unidade.CNES = '" + id_unidade + "'";
            hql += " order by afastamento.Data_Inicial desc";
            return Session.CreateQuery(hql).List<T>();
        }

        T IAfastamentoProfissional.BuscaAfastamentoProfissional<T>(string id_profissional, DateTime data_inicial, string data_final, string id_unidade)
        {
            string hql = "from ViverMais.Model.AfastamentoProfissional as afastamento";
            hql += " WHERE afastamento.Profissional.CPF = '" + id_profissional + "'";
            hql += " and afastamento.Data_Inicial='" + FormatoData.ConverterData(data_inicial, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and afastamento.Unidade.CNES = '" + id_unidade + "'";
            if (data_final != "")
                hql += " and afastamento.Data_Final='" + FormatoData.ConverterData(DateTime.Parse(data_final), FormatoData.nomeBanco.ORACLE) + "'";
            
            else
                hql += " and afastamento.Data_Final is null";
            
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IAfastamentoProfissional.VerificaExistenciaAfastamentoPeriodo<T>(string id_profissional, DateTime data_inicial, string data_final, string id_unidade)
        {
            string hql = "from ViverMais.Model.AfastamentoProfissional as afastamento";
            hql += " WHERE afastamento.Profissional.CPF = '" + id_profissional + "'";
            hql += " and afastamento.Data_Inicial >='" + FormatoData.ConverterData(data_inicial, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and afastamento.Unidade.CNES = '" + id_unidade + "'";
            if (data_final != "")
                hql += " and afastamento.Data_Final <='" + FormatoData.ConverterData(DateTime.Parse(data_final), FormatoData.nomeBanco.ORACLE) + "'";
            else
                hql += " and afastamento.Data_Final is null";
            
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IAfastamentoProfissional.VerificaAfastamentosNaData<T>(string cnes, DateTime data, string id_profissional)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.AfastamentoProfissional as afastamento";
            hql += " WHERE afastamento.Unidade.CNES = '" + cnes + "'";
            hql += " and afastamento.Profissional.CPF ='" + id_profissional + "'";
            hql += " and afastamento.Data_Inicial <'" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and (afastamento.Data_Final is null or afastamento.Data_Final >'" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "')";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}