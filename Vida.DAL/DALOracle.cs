﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace ViverMais.DAL
{
    public class DALOracle
    {
        #region ConnectionString

        //gaia
        //private static string _connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.22.6.16)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ViverMais)));User Id=ngi;Password=salvador;";
        //morfeu
        //private static string _connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.22.6.21)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ngi)));User Id=ngi;Password=salvador;";        //urgence
        //private static string _connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.227.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ViverMais)));User Id=ngi;Password=salvador;";
        //producao
        //private static string _connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.20.12.44)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ngi)));User Id=ngi;Password=#Ng1s@3De$;";

        //ATENÇÃO
        //Alterar a conexão em ConexaoBancoSingle
        private static string _connectionString = ConexaoBancoSingle.conexao.Replace("SERVER", "Data Source").Replace("ADDRESS=", "ADDRESS_LIST=(ADDRESS=").Replace("))(CONNECT", ")))(CONNECT").Replace("uid", "User Id").Replace("pwd", "Password");

        /// <summary>
        /// Retorna a atual string de conexão utilizada no projeto
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
        }

        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// Método que executa queries de inserção e atualização de dados sem transações. 
        /// </summary>
        /// <param name="cmdType">Tipo do command</param>
        /// <param name="cmdText">Querie SQL</param>
        /// <param name="cmdParms">Array de parâmetros</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();

            using (OracleConnection conn = new OracleConnection(_connectionString))
            {
                try
                {
                    PrepareParameters(cmdParms);
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    conn.Close();
                    return val;
                }
                catch (Exception ex)
                {
                    conn.Close();
                    conn.Dispose();
                    throw (ex);
                }
            }

        }

        /// <summary>
        /// Idêntico ao anterior com a diferença de ter a transação.
        /// Caso a transação seja 'null' ela é instanciada e iniciada no método.
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(ref OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            if (trans == null)
            {
                OracleConnection conn = new OracleConnection(_connectionString);
                conn.Open();
                trans = conn.BeginTransaction();
            }

            OracleCommand cmd = new OracleCommand();

            PrepareParameters(cmdParms);
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText.Trim(), cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Idêntico ao anterior com a diferença de ter a transação.
        /// Caso a transação seja 'null' ela é instanciada e iniciada no método.
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="bindByName"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(ref OracleTransaction trans, CommandType cmdType, string cmdText, bool bindByName, params OracleParameter[] cmdParms)
        {
            if (trans == null)
            {
                OracleConnection conn = new OracleConnection(_connectionString);
                conn.Open();
                trans = conn.BeginTransaction();
            }
            OracleCommand cmd = new OracleCommand();
            cmd.BindByName = bindByName;

            PrepareParameters(cmdParms);
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        #endregion

        #region ExecuteReaderDs
        /// <summary>
        /// Método que executa queries de seleção retornando um dataset.
        /// Após a execução da querie e preenchimento do dataset a conexão é fechada.
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns>Dataset preenchido com os dados da seleção.</returns>
        public static DataSet ExecuteReaderDs(CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            DataSet ds = new DataSet();
            using (OracleConnection conn = new OracleConnection(_connectionString))
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    PrepareParameters(cmdParms);
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(ds);
                    cmd.Parameters.Clear();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    conn.Dispose();
                    throw (ex);
                }
            }
            return ds;
        }

        /// <summary>
        /// O mesmo que o anterior contudo recebe um objeto OracleTransaction.
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet ExecuteReaderDs(ref OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            DataSet ds = new DataSet();
            try
            {
                if (trans == null)
                {
                    OracleConnection conn = new OracleConnection(_connectionString);
                    conn.Open();
                    trans = conn.BeginTransaction();
                }
                OracleCommand cmd = new OracleCommand();
                PrepareParameters(cmdParms);
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(ds);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return ds;
        }
        #endregion

        #region PrepareCommand
        /// <summary>
        /// Passa para o command os parâmetros passados para os métodos de execução em banco.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    if (cmd.Parameters.Contains(parm))
                        cmd.Parameters[parm.ParameterName] = parm;
                    else
                        cmd.Parameters.Add(parm);
                }
            }
        }
        #endregion

        #region PrepareParameters
        private static void PrepareParameters(OracleParameter[] cmdParms)
        {
            if (cmdParms != null)
            {
                foreach (OracleParameter parm in cmdParms)
                {
                    if ((parm.DbType.Equals(DbType.AnsiString) || parm.DbType.Equals(DbType.AnsiStringFixedLength)) && parm.Value != DBNull.Value)
                    {
                        parm.Value = RemoveQuote(parm.Value.ToString());
                        if (parm.Value.ToString() == String.Empty)
                            parm.Value = DBNull.Value;
                    }
                }
            }
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// Executa queries em banco que retornam um valor específico como
        /// count, max, min etc...
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns>Objeto que deve ser convertido para o tipo esperado</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            OracleCommand cmd = new OracleCommand();

            object obj = new object();


            using (OracleConnection conn = new OracleConnection(_connectionString))
            {
                try
                {
                    PrepareParameters(cmdParms);
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                    obj = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    conn.Dispose();
                    throw (ex);
                }
            }


            return obj;
        }

        /// <summary>
        /// O mesmo que o anterior contudo recebe um objeto do tipo OracleTransaction.
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(ref OracleTransaction trans, CommandType cmdType, string cmdText, params OracleParameter[] cmdParms)
        {
            if (trans == null)
            {
                OracleConnection conn = new OracleConnection(_connectionString);
                conn.Open();
                trans = conn.BeginTransaction();
            }

            OracleCommand cmd = new OracleCommand();
            PrepareParameters(cmdParms);
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            object obj = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return obj;
        }
        #endregion

        #region RemoveQuote
        private static string RemoveQuote(string text)
        {
            text = text.Replace("'", String.Empty);
            text = text.Replace("\"", String.Empty);
            text = text.Replace("´", String.Empty);
            return text;
        }
        #endregion

    }
}
