 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.DAL
{
    /// <summary>
    /// Classe responsável por unificar as características da cnexão com o banco
    /// </summary>
    public static class ConexaoBancoSingle
    {
        //SMS - Urgencia .. LocalHost
        //public static string conexao = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.227.1)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ViverMais)));User Id=ngi;Password=salvador;";

        //SMS-ViverMais01
        //public static string conexao = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.20.12.44)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ngi)));User Id=ngi;Password=#Ng1s@3De$;";
         
        //Morfeu
        public static string conexao = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.22.6.21)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ViverMais)));uid=ngi;pwd=salvador;";

        //Zeus
        //public static string conexao = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.22.16.15)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ViverMais)));uid=ngi;pwd=salvador;";
    }
}
