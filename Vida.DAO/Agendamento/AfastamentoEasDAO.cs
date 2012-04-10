using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.DAO.FormatoDataOracle;

namespace ViverMais.DAO.Agendamento
{
    public class AfastamentoEasDAO : AgendamentoServiceFacadeDAO, IAfastamentoEAS
    {
        IList<T> IAfastamentoEAS.BuscarAfastamentosPorUnidade<T>(string id_unidade)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.AfastamentoEAS as afastamento";
            hql += " WHERE afastamento.ID_Unidade = '" + id_unidade + "'";
            hql += " order by afastamento.Data_Inicial desc";
            return Session.CreateQuery(hql).List<T>();
        }

        T IAfastamentoEAS.VerificaAfastamentosNaData<T>(string cnes, DateTime data)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.AfastamentoEAS as afastamento";
            hql += " WHERE afastamento.ID_Unidade = '" + cnes + "'";
            hql += " AND afastamento.Data_Inicial <='" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " AND afastamento.Data_Reativacao >='" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IAfastamentoEAS.VerificaAfastamentosNaData<T>(string cnes, DateTime data_inicial, string data_final)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.AfastamentoEAS as afastamento";
            hql += " WHERE afastamento.ID_Unidade = '" + cnes + "'";
            hql += " AND afastamento.Data_Inicial <='" + FormatoData.ConverterData(data_inicial, FormatoData.nomeBanco.ORACLE) + "'";
            if (data_final != "")
                hql += " AND afastamento.Data_Reativacao >='" + FormatoData.ConverterData(DateTime.Parse(data_final), FormatoData.nomeBanco.ORACLE) + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}