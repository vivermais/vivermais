using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;
using ViverMais.DAO.FormatoDataOracle;
using System.Data;
using System.Data.OracleClient;

namespace ViverMais.DAO.Agendamento.Agenda
{
    public class AmbulatorialDAO : AgendamentoServiceFacadeDAO, IAmbulatorial
    {
        #region IAmbulatorial Members

        public AmbulatorialDAO()
        {

        }

        IList<T> IAmbulatorial.ListarAgendas<T>(string id_unidade, string competencia, string co_procedimento, string co_profissional, DateTime dataInicial, DateTime dataFinal, string co_cbo, string co_subgrupo, string turno, bool bloqueada, bool publicada)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Agenda as agenda";
            hql += " WHERE agenda.Estabelecimento.CNES = '" + id_unidade + "'";

            if (!String.IsNullOrEmpty(competencia))
                hql += " and agenda.Competencia = '" + competencia + "'";
            if (!String.IsNullOrEmpty(co_procedimento))
                hql += " and agenda.Procedimento.Codigo= '" + co_procedimento + "'";
            if (!String.IsNullOrEmpty(co_profissional))
                hql += " and agenda.ID_Profissional.CPF = '" + co_profissional + "'";
            if (dataInicial != null && dataInicial != DateTime.MinValue && dataInicial != DateTime.MaxValue)
                hql += " and agenda.Data >= TO_DATE('" + dataInicial.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')";
            if (dataFinal != null && dataFinal != DateTime.MinValue && dataFinal != DateTime.MaxValue)
                hql += " and agenda.Data <= TO_DATE('" + dataFinal.ToString("dd/MM/yyyy") + "','DD/MM/YYYY')";
            if (!String.IsNullOrEmpty(co_cbo))
                hql += " and agenda.Cbo.Codigo= '" + co_cbo + "'";
            if (!String.IsNullOrEmpty(co_subgrupo))
                hql += " and agenda.SubGrupo.Codigo = " + co_subgrupo;
            if (!String.IsNullOrEmpty(turno))
                hql += "and agenda.Turno = '" + turno.ToUpper() + "'";
            if (bloqueada != null)
                hql += " and agenda.Bloqueada = " + (bloqueada ? "1": "0");
            if (publicada != null)
                hql += " and agenda.Publicada = " + (publicada ? "1" : "0");
            hql += " order by agenda.Data asc";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAmbulatorial.ListarAgendas<T>(string id_unidade, string competencia, string co_procedimento, string co_profissional, DateTime dataInicial, DateTime dataFinal, string co_cbo, string co_subgrupo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Agenda as agenda";
            hql += " WHERE agenda.Estabelecimento.CNES = '" + id_unidade + "'";

            if (!String.IsNullOrEmpty(competencia))
                hql += " and agenda.Competencia = '" + competencia + "'";
            if (!String.IsNullOrEmpty(co_procedimento))
                hql += " and agenda.Procedimento.Codigo= '" + co_procedimento + "'";
            if (!String.IsNullOrEmpty(co_profissional))
                hql += " and agenda.ID_Profissional.CPF = '" + co_profissional + "'";

            if (dataInicial != null && dataFinal != null)
                hql += " and agenda.Data between '" + FormatoData.ConverterData(dataInicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(dataFinal, FormatoData.nomeBanco.ORACLE) + "'";

            if (!String.IsNullOrEmpty(co_cbo))
                hql += "and agenda.Cbo.Codigo= '" + co_cbo + "'";

            if (!String.IsNullOrEmpty(co_subgrupo))
                hql += "and agenda.SubGrupo.Codigo = " + co_subgrupo;

            hql += " order by agenda.Data asc";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAmbulatorial.BuscarAgendas<T>(string id_unidade, int competencia, string co_procedimento, string co_profissional, string data, int co_subgrupo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Agenda as agenda";
            hql += " WHERE agenda.Estabelecimento.CNES = '" + id_unidade + "'";
            hql += " and agenda.Competencia = '" + competencia + "'";
            hql += " and agenda.Procedimento.Codigo= '" + co_procedimento + "'";
            if (co_profissional != "")
            {
                hql += " and agenda.ID_Profissional.CPF = '" + co_profissional + "'";
            }
            if (data != "")
            {
                hql += " and agenda.Data='" + FormatoData.ConverterData(DateTime.Parse(data), FormatoData.nomeBanco.ORACLE) + "'";
            }
            if (co_subgrupo != 0)
            {
                hql += "and agenda.SubGrupo.Codigo = " + co_subgrupo;
            }
            hql += " order by agenda.Data asc";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAmbulatorial.BuscarAgendas<T>(string id_unidade, int competencia, string co_procedimento, string co_profissional, string data)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Agenda as agenda";
            hql += " WHERE agenda.Estabelecimento.CNES = '" + id_unidade + "'";
            hql += " and agenda.Competencia = '" + competencia + "'";
            hql += " and agenda.Procedimento.Codigo= '" + co_procedimento + "'";
            if (co_profissional != "")
            {
                hql += " and agenda.ID_Profissional.CPF = '" + co_profissional + "'";
            }
            if (data != "")
            {
                hql += " and agenda.Data='" + FormatoData.ConverterData(DateTime.Parse(data), FormatoData.nomeBanco.ORACLE) + "'";
            }
            hql += " order by agenda.Data asc";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAmbulatorial.VerificarAgendas<T>(string id_unidade, string id_profissional, string data_inicial, string data_final, string turno)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.Agenda agenda";
            hql += " WHERE agenda.Estabelecimento.CNES = '" + id_unidade + "'";
            if (id_profissional != "")
            {
                hql += " and agenda.ID_Profissional.CPF = '" + id_profissional + "'";
            }
            hql += " and agenda.Data >='" + FormatoData.ConverterData(DateTime.Parse(data_inicial), FormatoData.nomeBanco.ORACLE) + "'";
            if (data_final != "")
            {
                hql += " and agenda.Data <='" + FormatoData.ConverterData(DateTime.Parse(data_final), FormatoData.nomeBanco.ORACLE) + "'";
            }
            if (turno != "0")
                hql += " and agenda.Turno ='" + turno + "'";
            hql += " order by agenda.Data";
            return Session.CreateQuery(hql).List<T>();
        }

        T IAmbulatorial.BuscaDuplicidade<T>(string id_unidade, int competencia, string co_procedimento, string co_profissional, DateTime data, string turno)
        {
            string hql = "from ViverMais.Model.Agenda as agenda";
            hql += " WHERE agenda.Estabelecimento.CNES = '" + id_unidade + "'";
            hql += " and agenda.Competencia = '" + competencia + "'";
            hql += " and agenda.Procedimento.Codigo = '" + co_procedimento + "'";
            hql += " and agenda.ID_Profissional.CPF = '" + co_profissional + "'";
            hql += " and agenda.Data='" + FormatoData.ConverterData(data, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and agenda.Turno='" + turno + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IAmbulatorial.BuscaQtdAgendada<T>(int id_agenda)
        {
            string hql = "Select count(solicitacao.Codigo) from ViverMais.Model.Solicitacao as solicitacao";
            hql += " WHERE solicitacao.Agenda.Codigo = '" + id_agenda + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IAmbulatorial.BuscarQuantidadeOfertada<T>(string procedimento, int competencia)
        {
            string hql = "select sum(agenda.Quantidade) ";
            hql += " from ViverMais.Model.Agenda agenda ";
            hql += " Where agenda.Procedimento.Codigo = '" + procedimento + "'";
            hql += " and agenda.Competencia = '" + competencia + "'";
            IQuery query = Session.CreateQuery(hql);
            return query.UniqueResult<T>();
        }

        IList IAmbulatorial.BuscarVagas(string cbo, string subgrupo)
        {
            string hql = "select sum(agenda.Quantidade),sum(agenda.QuantidadeAgendada)";
            hql += " from ViverMais.Model.Agenda agenda ";
            hql += " Where agenda.Cbo.Codigo= '" + cbo + "'";
            hql += " and agenda.Procedimento.Codigo = '" + subgrupo + "'";
            hql += " and agenda.Publicada = 1";
            hql += " and agenda.Bloqueada = 0";
            hql += " and agenda.Data >='" + FormatoData.ConverterData((DateTime.Now), FormatoData.nomeBanco.ORACLE) + "'"; ;
            IQuery query = Session.CreateQuery(hql);
            IList result = query.List();
            if (result.Count > 0)
            {
                object[] item = (object[])result[0];
                if (item[0] == null)
                {
                    ((object[])result[0])[0] = 0;
                    ((object[])result[0])[1] = 0;
                }
            }
            return result;
        }

        IList<T> IAmbulatorial.BuscarAgendaProcedimento<T>(string co_procedimento, string cbo, DateTime data_inicial, DateTime data_final, int co_subGrupo)
        {
            string hql = " from ViverMais.Model.Agenda agenda";
            hql += " where agenda.Procedimento.Codigo = '" + co_procedimento + "'";
            if (!String.IsNullOrEmpty(cbo))
                hql += " and agenda.Cbo.Codigo = '" + cbo + "'";
            hql += " and agenda.Data between '" + FormatoData.ConverterData(data_inicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(data_final, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and agenda.Bloqueada = 0";
            hql += " and agenda.Publicada = 1";
            if (co_subGrupo == 0)
                hql += " and agenda.SubGrupo is null";
            else
                hql += " and agenda.SubGrupo.Codigo = " + co_subGrupo;
            //hql += " and agenda.Quantidade > agenda.QuantidadeAgendada";
            hql += " order by agenda.Data asc";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAmbulatorial.ListarAgendasLocais<T>(string id_procedimento, string cbo, DateTime data_inicial, DateTime data_final, string cnes, int co_subgrupo)
        {
            string hql = " from ViverMais.Model.Agenda agenda";
            hql += " where agenda.Procedimento.Codigo = '" + id_procedimento + "'";
            if (!String.IsNullOrEmpty(cbo))
                hql += " and agenda.Cbo.Codigo = '" + cbo + "'";
            hql += " and agenda.Data between '" + FormatoData.ConverterData(data_inicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(data_final, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and agenda.Bloqueada = 0";
            hql += " and agenda.Publicada = 1";
            if (co_subgrupo == 0)
                hql += " and agenda.SubGrupo is null";
            else
                hql += " and agenda.SubGrupo.Codigo = " + co_subgrupo;
            hql += " and agenda.Estabelecimento.CNES = '" + cnes + "'";
            hql += " order by agenda.Data asc";
            return Session.CreateQuery(hql).List<T>();
        }


        IList<T> IAmbulatorial.ListarUnidadesComAgendasDisponivelRede<T>(string id_procedimento, string cbo, DateTime data_inicial, DateTime data_final, string cnesLocal, int co_subgrupo)
        {
            string hql = "Select est from ViverMais.Model.EstabelecimentoSaude est, ViverMais.Model.ParametroAgenda parametro";
            //string hql = "Select distinct e from ViverMais.Model.Agenda agenda, ViverMais.Model.EstabelecimentoSaude e";
            hql += " where parametro.Cnes = est.CNES and parametro.Percentual <> 0 and parametro.TipoAgenda = " + Convert.ToInt32(ParametroAgenda.TipoDeAgenda.REDE);
            hql += " and est.CNES IN (select agenda.Estabelecimento.CNES from ViverMais.Model.Agenda agenda where agenda.Procedimento.Codigo = '" + id_procedimento + "' ";
            if (!String.IsNullOrEmpty(cbo))
                hql += " and agenda.Cbo.Codigo = '" + cbo + "' ";
            hql += " and agenda.Data between '" + FormatoData.ConverterData(data_inicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(data_final, FormatoData.nomeBanco.ORACLE) + "'" +
            " and agenda.Bloqueada = 0" +
            " and agenda.Publicada = 1";
            if (co_subgrupo == 0)
                hql += " and agenda.SubGrupo is null";
            else
                hql += " and agenda.SubGrupo.Codigo = " + co_subgrupo;
            hql += " and agenda.Estabelecimento.CNES <> '" + cnesLocal + "')";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAmbulatorial.ListarUnidadesComAgendasDisponivelReservaTecnica<T>(string id_procedimento, string cbo, DateTime data_inicial, DateTime data_final, int co_subgrupo)
        {
            string hql = "Select est from ViverMais.Model.EstabelecimentoSaude est, ViverMais.Model.ParametroAgenda parametro";
            hql += " where parametro.Cnes = est.CNES and parametro.Percentual <> 0 and parametro.TipoAgenda = " + Convert.ToInt32(ParametroAgenda.TipoDeAgenda.RESERVA_TECNICA);
            hql += " and est.CNES IN (select agenda.Estabelecimento.CNES from ViverMais.Model.Agenda agenda where agenda.Procedimento.Codigo = '" + id_procedimento + "' " +
            " and agenda.Cbo.Codigo = '" + cbo + "' " +
            " and agenda.Data between '" + FormatoData.ConverterData(data_inicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(data_final, FormatoData.nomeBanco.ORACLE) + "'" +
            " and agenda.Bloqueada = 0";
            if (co_subgrupo == 0)
                hql += " and agenda.SubGrupo is null";
            else
                hql += " and agenda.SubGrupo.Codigo = " + co_subgrupo;
            hql += " and agenda.Publicada = 1)";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAmbulatorial.ListarAgendasParametroRede<T>(string id_procedimento, string cbo, DateTime data_inicial, DateTime data_final, string cnesLocal)
        {
            string hql = "Select agenda from ViverMais.Model.Agenda agenda";
            hql += " where agenda.Procedimento.Codigo = '" + id_procedimento + "'";
            hql += " and agenda.Cbo.Codigo = '" + cbo + "'";
            hql += " and agenda.Data between '" + FormatoData.ConverterData(data_inicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(data_final, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and agenda.Bloqueada = 0";
            hql += " and agenda.Publicada = 1";
            hql += " and agenda.SubGrupo is null";
            hql += " and agenda.Estabelecimento.CNES = '" + cnesLocal + "'";
            hql += " order by agenda.Data asc";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IAmbulatorial.BuscarAgendaProcedimento<T>(string subgrupo, string cbo, DateTime data_inicial, DateTime data_final)
        {
            string hql = " from ViverMais.Model.Agenda agenda";
            hql += " where agenda.Procedimento.Codigo = '" + subgrupo + "'";
            if (!String.IsNullOrEmpty(cbo))
                hql += " and agenda.Cbo.Codigo = '" + cbo + "'";
            hql += " and agenda.Data between '" + FormatoData.ConverterData(data_inicial, FormatoData.nomeBanco.ORACLE) + "' and '" + FormatoData.ConverterData(data_final, FormatoData.nomeBanco.ORACLE) + "'";
            hql += " and agenda.Bloqueada = 0";
            hql += " and agenda.Publicada = 1";
            hql += " order by agenda.Data asc";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<object> IAmbulatorial.ListarVagasDisponiveis<T>(DateTime periodoInicial, DateTime periodoFinal)
        {
            string hql = " select procedimento.Nome, sum(agenda.Quantidade), sum (agenda.QuantidadeAgendada)";
            hql += " from ViverMais.Model.Agenda agenda, ViverMais.Model.Procedimento procedimento, ViverMais.Model.TipoProcedimento tipoProcedimento";
            hql += " where agenda.Procedimento.Codigo = procedimento.Codigo";
            hql += " and procedimento.Codigo = tipoProcedimento.Procedimento";
            hql += " and agenda.Procedimento.Codigo = tipoProcedimento.Procedimento";
            hql += " and agenda.Bloqueada = 0";
            hql += " and agenda.Publicada = 1";
            hql += " and agenda.Data between TO_DATE('" + periodoInicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')";
            hql += " and TO_DATE('" + periodoFinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            hql += " and (tipoProcedimento.Tipo = '1' or tipoProcedimento.Tipo='2')";
            hql += " group by procedimento.Nome";
            hql += " order by procedimento.Nome";

            return Session.CreateQuery(hql).List<object>();
        }



        DataTable IAmbulatorial.RelatorioAgendaMontadaPublicada<E, P, X, C>(int competencia, E estabele, P prof, X proced, C cboParameter)
        {
            //List<object> valores = new List<object>();
            ViverMais.Model.EstabelecimentoSaude estabelecimento = (ViverMais.Model.EstabelecimentoSaude)(object)estabele;
            ViverMais.Model.Procedimento procedimento = (ViverMais.Model.Procedimento)(object)proced;
            ViverMais.Model.Profissional profissional = (ViverMais.Model.Profissional)(object)prof;
            CBO cbo = (CBO)(object)cboParameter;

            //string hql = "select esta.nome_fanta, proced.no_procedimento, cbo.no_ocupacao, prof.nome_prof, Sum(agd.Quantidade), SUM(agd.publicada)";
            //hql += " from NGI.agd_agenda agd, NGI.pms_cnes_lfces004 esta, NGI.tb_pms_procedimento proced, NGI.pms_cnes_lfces018 prof, NGI.tb_pms_ocupacao_proc cbo";
            //hql += " where agd.cnes = esta.cnes";
            //hql += " and proced.co_procedimento = agd.co_procedimento  and agd.co_procedimento = proced.co_procedimento";
            //hql += " and agd.cpf_prof = prof.cpf_prof and agd.co_ocupacao = cbo.co_ocupacao";
            //hql += " and agd.competencia = "+ competencia;
            //if(estabelecimento != null)
            //    hql += " and agd.CNES = '" + estabelecimento.CNES + "'";
            //if(procedimento != null)
            //    hql += " and agd.co_procedimento = '" + procedimento.Codigo + "'";
            //if(profissional != null)
            //    hql += " and agd.cpf_prof = '" + profissional.CPF + "'";
            //if(cbo != null)
            //    hql += " and agd.co_ocupacao = '" + cbo.Codigo + "'";
            //hql += " group by esta.nome_fanta, proced.no_procedimento, cbo.no_ocupacao, prof.nome_prof";
            //hql += " order by esta.nome_fanta, proced.no_procedimento, cbo.no_ocupacao, prof.nome_prof";
            //return Session.CreateSQLQuery(hql).List<object>();
            //string hql = "select est.NomeFantasia, proced.Nome, cbo.Nome, prof.Nome, Count(*), Sum(agd.Publicada)";
            //hql += " from ViverMais.Model.Agenda agd, ViverMais.Model.EstabelecimentoSaude est, ViverMais.Model.Procedimento proced, ViverMais.Model.CBO cbo, ViverMais.Model.Profissional prof";
            //hql += " where agd.Competencia = " + competencia;
            //hql += " and agd.Estabelecimento.CNES = est.CNES and agd.Procedimento.Codigo = proced.Codigo";
            //hql += " and agd.Cbo.Codigo = cbo.Codigo and agd.ID_Profissional.CPF = prof.CPF";
            //if (estabelecimento != null)
            //    hql += "agd.Estabelecimento.Cnes = '" + estabelecimento.CNES + "'";
            //if(procedimento != null)
            //    hql += "agd.Procedimento.Codigo = '" + procedimento.Codigo + "'";
            //if(profissional != null)
            //    hql += "agd.ID_Profissional.CPF = '" + profissional.CPF + "'";
            //if(cbo != null)
            //    hql += "agd.Cbo.Codigo = '" + cbo.Codigo + "'";
            //hql += " group by est.NomeFantasia, proced.Nome, cbo.Nome, prof.Nome";
            //return Session.CreateQuery(hql).List<object>();

            //object valores = new object();

            try
            {
                OracleCommand comando = new OracleCommand();
                comando.CommandText = "SP_AGD_REL_MONT__PUBL";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Connection = (OracleConnection)Session.Connection;
                comando.Parameters.Add("v_COMPETENCIA", OracleType.Number).Value = competencia;
                if (estabelecimento != null)
                    comando.Parameters.Add("v_CNES", OracleType.VarChar).Value = estabelecimento.CNES;
                if (profissional != null)
                    comando.Parameters.Add("v_CPF_PROF", OracleType.VarChar).Value = profissional.CPF;
                if (procedimento != null)
                    comando.Parameters.Add("v_PROCEDIMENTO", OracleType.VarChar).Value = procedimento.Codigo;
                if (cbo != null)
                    comando.Parameters.Add("v_CBO", OracleType.VarChar).Value = cbo.Codigo;
                try
                {
                    comando.Parameters.Add("v_RETORNO", OracleType.Cursor).Direction = ParameterDirection.Output;

                    OracleDataReader reader = comando.ExecuteReader();

                    //object objetos = comando.Parameters["v_RETORNO"].Value;
                    DataTable table = new DataTable();
                    table.Columns.Add("Unidade");
                    table.Columns.Add("Procedimento");
                    table.Columns.Add("Especialidade");
                    table.Columns.Add("Profissional");
                    table.Columns.Add("QtdMontada");
                    table.Columns.Add("QtdPublicada");

                    while (reader.Read())
                    {
                        DataRow row2 = table.NewRow();
                        row2["Unidade"] = reader["NOME_FANTA"].ToString(); //item[0].ToString();
                        row2["Procedimento"] = reader["NO_PROCEDIMENTO"].ToString();
                        //item[1].ToString();
                        row2["Especialidade"] = reader["no_ocupacao"].ToString();
                        //item[2].ToString();
                        row2["Profissional"] = reader["nome_prof"].ToString();
                        //item[3].ToString();
                        row2["QtdMontada"] = reader["QTD"].ToString();
                        //row2["QtdMontada"] = reader["QTD"].ToString();
                        //quantidadeDisponibilizada;
                        row2["QtdPublicada"] = reader["QTD_PUBLICADA"].ToString();//quantidadePublicada;
                        table.Rows.Add(row2);
                    }
                    ////reader.Read()
                    //valores.Add(comando.Parameters["v_NOME_FANTA"].Value);
                    //valores.Add(comando.Parameters["v_NOME_PROCED"].Value);
                    //valores.Add(comando.Parameters["vNO_CBO"].Value);
                    //valores.Add(comando.Parameters["vNO_PROFISSIONAL"].Value);
                    //valores.Add(comando.Parameters["vQTD_MONTADA"].Value);
                    //valores.Add(comando.Parameters["vQTD_PUBLICADA"].Value);


                    //valores = (comando.Parameters);
                    //    SetValue(comando.Parameters[4].Value, 0);
                    //valores.SetValue(comando.Parameters[5].Value, 1);
                    //valores.SetValue(comando.Parameters[5].Value, 2);
                    //conexaoOracle.Close();
                    reader.Close();
                    reader.Dispose();
                    comando.Dispose();
                    return table;
                }
                catch (OracleException e)
                {
                    throw e;
                }
                finally
                {
                    comando.Dispose();
                }

                //return valores;

            }
            catch (OracleException e)
            {
                throw e;
            }

        }

        object IAmbulatorial.ListaTotalAgendasESolicitacoes<T>(string id_procedimento, string id_unidade, string cbo, string competencia)
        {
            object[] valores = new object[2];

            try
            {
                using (OracleConnection conexaoOracle = new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.22.6.20)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=ViverMais)));User Id=ngi;Password=salvador;"))
                {
                    OracleCommand comando = new OracleCommand();
                    comando.CommandText = "SP_QTD_AGENDA_E_QTD_SOLICITADA";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Connection = conexaoOracle;
                    comando.Parameters.Add("vID_PROCEDIMENTO", OracleType.VarChar).Value = id_procedimento;
                    comando.Parameters.Add("vID_UNIDADE", OracleType.VarChar).Value = id_unidade;
                    comando.Parameters.Add("vCBO", OracleType.VarChar).Value = cbo;
                    comando.Parameters.Add("vCOMPETENCIA", OracleType.VarChar).Value = competencia;
                    comando.Parameters.Add("qntd", OracleType.Number).Direction = ParameterDirection.Output;
                    comando.Parameters.Add("qtdsolicitacoes", OracleType.Number).Direction = ParameterDirection.Output;
                    try
                    {
                        conexaoOracle.Open();
                        OracleDataReader reader = comando.ExecuteReader();
                        valores.SetValue(comando.Parameters[4].Value, 0);
                        valores.SetValue(comando.Parameters[5].Value, 1);
                        conexaoOracle.Close();
                        return valores;
                    }
                    catch (OracleException e)
                    {
                        throw e;
                    }
                }
            }
            catch (OracleException e)
            {
                throw e;
            }
        }

        #endregion

    }
}
