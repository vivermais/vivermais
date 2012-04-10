using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using ViverMais.DAL;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class LogViverMaisDAO:ADAO<LogViverMais>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(LogViverMais objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(LogViverMais objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryRemocao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryBuscarID()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaPorCodigo()
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(LogViverMais objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(Oracle.DataAccess.Client.OracleDataReader dataReader, LogViverMais objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(System.Data.DataRow drc, LogViverMais objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(Oracle.DataAccess.Client.OracleDataReader dataReader, LogViverMais objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(System.Data.DataRow dataRow, LogViverMais objeto)
        {
            throw new NotImplementedException();
        }

        public int QuantidadeDeCartoesPorUsuario(int codigoUsuario, DateTime dataInicio, DateTime dataFim)
        {   
            sqlText.Append("select count(*) ");
            sqlText.Append("from tb_pms_log_eventos ");
            sqlText.Append("where co_usuario = :CodigoUsuario ");
            sqlText.Append("and co_evento = 1 ");
            sqlText.Append("and data between :DataInicio and :DataFim ");

            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Int32));
            parametros.Add(new OracleParameter("DataInicio", OracleDbType.Date));
            parametros.Add(new OracleParameter("DataFim", OracleDbType.Date));

            parametros[0].Value = codigoUsuario;
            parametros[1].Value = dataInicio;
            parametros[2].Value = dataFim;

            object retorno = DALOracle.ExecuteScalar(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            return Convert.ToInt32(retorno);

        }
    }
}
