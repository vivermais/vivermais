using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.DAO.FormatoDataOracle
{
    public static class FormatoData
    {
        static string FormatoDataOracle = "dd/MM/yyyy";
        static string FormatoDataSql = "yyyy-MM-dd";

        public enum nomeBanco { ORACLE = '1', SQLSERVER = '2' };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="banco">"Oracle" para banco Oracle. "SqlServer" para banco Sql</param>
        /// <returns></returns>
        public static string ConverterData(DateTime data, nomeBanco nomeBanco)
        {
            if (nomeBanco.Equals(nomeBanco.ORACLE))
                return data.ToString(FormatoDataOracle);
            else
                return data.ToString(FormatoDataSql);
        }
    }
}
